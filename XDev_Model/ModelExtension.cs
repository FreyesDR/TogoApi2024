using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XDev_Model
{
    public static class ModelExtension
    {
		public static OperationBuilder<SqlOperation> CreateSPMaterialWareHouseInvoiceCancel(this MigrationBuilder migrationBuilder)
			=> migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION public.xsp_material_wh_invoice_cancel(
    p_invoiceid UUID,
    p_tipomov   VARCHAR
)
RETURNS VOID AS $$
DECLARE
    v_branchid       UUID;
    rec_pos          RECORD;
    v_stock          NUMERIC(18,3);
    v_soldstock      NUMERIC(18,3);
    v_lockedstock    NUMERIC(18,3);
    v_intransitstock NUMERIC(18,3);
BEGIN
    -- Obtener sucursal de la factura
    SELECT ""BranchId""
    INTO v_branchid
    FROM ""Invoice""
    WHERE ""Id"" = p_invoiceid;

    -- Iterar posiciones de materiales tipo 'Bien'
    FOR rec_pos IN
        SELECT ""MaterialId"", ""WareHouseId"", ""Quantity""
        FROM ""InvoicePosition""
        WHERE ""InvoiceId"" = p_invoiceid
          AND ""MaterialTypeCode"" = 'B'
    LOOP
        -- Bloquear fila para concurrencia y leer stocks
        SELECT ""Stock"", ""SoldStock"", ""LockedStock"", ""InTransitStock""
        INTO v_stock, v_soldstock, v_lockedstock, v_intransitstock
        FROM ""MaterialWareHouse""
        WHERE ""MaterialId""   = rec_pos.""MaterialId""
          AND ""BranchId""     = v_branchid
          AND ""WareHouseId""  = rec_pos.""WareHouseId""
        FOR UPDATE;

        IF p_tipomov = 'R' THEN
            -- Cancelación de venta: revertir SoldStock y devolver a Stock
            UPDATE ""MaterialWareHouse""
            SET ""SoldStock"" = v_soldstock   - rec_pos.""Quantity"",
                ""Stock""     = v_stock       + rec_pos.""Quantity""
            WHERE ""MaterialId""   = rec_pos.""MaterialId""
              AND ""BranchId""     = v_branchid
              AND ""WareHouseId""  = rec_pos.""WareHouseId"";

        ELSIF p_tipomov = 'I' THEN
            -- Cancelación de tránsito: reducir Stock
            UPDATE ""MaterialWareHouse""
            SET ""Stock"" = v_stock - rec_pos.""Quantity""
            WHERE ""MaterialId""   = rec_pos.""MaterialId""
              AND ""BranchId""     = v_branchid
              AND ""WareHouseId""  = rec_pos.""WareHouseId"";
        END IF;
    END LOOP;
END;
$$ LANGUAGE plpgsql;");

        public static OperationBuilder<SqlOperation> CreateSPMaterialWareHouseInvoice(this MigrationBuilder migrationBuilder)
		=> migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION public.xsp_material_wh_invoice(
    p_invoiceid UUID,
    p_tipomov   VARCHAR
)
RETURNS VOID AS $$
DECLARE
    v_branchid      UUID;
    rec_pos         RECORD;
    v_stock         NUMERIC(18,3);
    v_lockedstock   NUMERIC(18,3);
    v_intransitstock NUMERIC(18,3);
    v_soldstock     NUMERIC(18,3);
BEGIN
    -- Obtener la sucursal de la factura
    SELECT ""BranchId""
    INTO v_branchid
    FROM ""Invoice""
    WHERE ""Id"" = p_invoiceid;

    -- Procesar cada posición de material tipo ""Bien""
    FOR rec_pos IN
        SELECT ""MaterialId"", ""WareHouseId"", ""Quantity""
        FROM ""InvoicePosition""
        WHERE ""InvoiceId"" = p_invoiceid
          AND ""MaterialTypeCode"" = 'B'
    LOOP
        -- Bloquear fila para concurrencia y obtener stocks
        SELECT ""Stock"", ""SoldStock"", ""LockedStock"", ""InTransitStock""
        INTO v_stock, v_soldstock, v_lockedstock, v_intransitstock
        FROM ""MaterialWareHouse""
        WHERE ""MaterialId""  = rec_pos.""MaterialId""
          AND ""BranchId""    = v_branchid
          AND ""WareHouseId"" = rec_pos.""WareHouseId""
        FOR UPDATE;

        IF p_tipomov = 'R' THEN
            -- Movimiento de venta: aumentar SoldStock y disminuir LockedStock
            UPDATE ""MaterialWareHouse""
            SET ""SoldStock""   = v_soldstock   + rec_pos.""Quantity"",
                ""LockedStock"" = v_lockedstock - rec_pos.""Quantity""
            WHERE ""MaterialId""  = rec_pos.""MaterialId""
              AND ""BranchId""    = v_branchid
              AND ""WareHouseId"" = rec_pos.""WareHouseId"";

        ELSIF p_tipomov = 'I' THEN
            -- Movimiento de devolución: aumentar Stock y disminuir InTransitStock
            UPDATE ""MaterialWareHouse""
            SET ""Stock""          = v_stock         + rec_pos.""Quantity"",
                ""InTransitStock"" = v_intransitstock - rec_pos.""Quantity""
            WHERE ""MaterialId""  = rec_pos.""MaterialId""
              AND ""BranchId""    = v_branchid
              AND ""WareHouseId"" = rec_pos.""WareHouseId"";
        END IF;
    END LOOP;
END;
$$ LANGUAGE plpgsql;");

		public static OperationBuilder<SqlOperation> CreateSPMaterialWareHouseSaleOrder(this MigrationBuilder migrationBuilder)
			=> migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION public.xsp_material_wh_sale_order(
    p_matid     UUID,
    p_branchid  UUID,
    p_whid      UUID,
    p_tipomov   VARCHAR,
    p_qty       NUMERIC(18,3)
)
RETURNS VOID AS $$
DECLARE
    v_stock      NUMERIC(18,3);
    v_locked     NUMERIC(18,3);
    v_intransit  NUMERIC(18,3);
BEGIN
    -- Bloquear la fila para concurrencia
    SELECT ""Stock"", ""LockedStock"", ""InTransitStock""
    INTO v_stock, v_locked, v_intransit
    FROM ""MaterialWareHouse""
    WHERE ""MaterialId""   = p_matid
      AND ""BranchId""     = p_branchid
      AND ""WareHouseId""  = p_whid
    FOR UPDATE;

    -- Ajustar según tipo de movimiento
    IF p_tipomov = 'R' THEN
        -- Reserva: decrementa stock y aumenta stock bloqueado
        UPDATE ""MaterialWareHouse""
        SET ""Stock""       = v_stock - p_qty,
            ""LockedStock"" = v_locked + p_qty
        WHERE ""MaterialId""   = p_matid
          AND ""BranchId""     = p_branchid
          AND ""WareHouseId""  = p_whid;

    ELSIF p_tipomov = 'I' THEN
        -- En tránsito: incrementa stock en tránsito
        UPDATE ""MaterialWareHouse""
        SET ""InTransitStock"" = v_intransit + p_qty
        WHERE ""MaterialId""   = p_matid
          AND ""BranchId""     = p_branchid
          AND ""WareHouseId""  = p_whid;
    END IF;
END;
$$ LANGUAGE plpgsql;");

        /// <summary>
        /// SP Crear rango de número para Facturación Electrónica SV
        /// </summary>
        /// <param name="migrationBuilder"></param>
        /// <returns></returns>
        public static OperationBuilder<SqlOperation> CreateEBillingDocumentNextNumber(this MigrationBuilder migrationBuilder)
			=> migrationBuilder.Sql(@"

CREATE OR REPLACE FUNCTION public.xsp_ebilling_co_next_number(
    coid UUID,
    doid UUID
)
RETURNS BIGINT AS $$
DECLARE
    v_ini      BIGINT;
    v_fin      BIGINT;
    v_curr     BIGINT;
    v_inc      BIGINT;
    v_return   BIGINT;
    v_reIni    BOOLEAN;
    v_nextY    INTEGER;
BEGIN
    -- Bloquea la fila para evitar concurrencia
    SELECT ""RangeStart"", ""RangeEnd"", ""Current"", ""ReStartYear"", ""NextReStart""
    INTO v_ini, v_fin, v_curr, v_reIni, v_nextY
    FROM ""EBillingCompanyInvoice""
    WHERE ""CompanyId"" = coid
      AND ""InvoiceTypeId"" = doid
    FOR UPDATE;

    v_inc := v_curr + 1;

    IF v_inc BETWEEN v_ini AND v_fin THEN
        IF v_reIni AND EXTRACT(YEAR FROM CURRENT_DATE) = v_nextY THEN
            UPDATE ""EBillingCompanyInvoice""
            SET ""Current""     = 2,
                ""NextReStart"" = v_nextY + 1
            WHERE ""CompanyId"" = coid
              AND ""InvoiceTypeId"" = doid;
            v_return := 1;
        ELSE
            UPDATE ""EBillingCompanyInvoice""
            SET ""Current"" = v_inc
            WHERE ""CompanyId"" = coid
              AND ""InvoiceTypeId"" = doid;
            -- Devuelve el valor anterior al incremento
            v_return := v_curr;
        END IF;
    ELSE
        IF v_reIni THEN
            UPDATE ""EBillingCompanyInvoice""
            SET ""Current""     = 2,
                ""NextReStart"" = EXTRACT(YEAR FROM CURRENT_DATE) + 1
            WHERE ""CompanyId"" = coid
              AND ""InvoiceTypeId"" = doid;
            v_return := 1;
        ELSE
            RAISE EXCEPTION 'Rango agotado';
        END IF;
    END IF;

    RETURN v_return;
END;
$$ LANGUAGE plpgsql;
");

        /// <summary>
		/// Crear SP para generación de rango de números. Agregar al final de la migración [UP]
		/// </summary>
		/// <param name="migrationBuilder"></param>
		/// <returns></returns>
		public static OperationBuilder<SqlOperation> CreateSPGetNumberNext(this MigrationBuilder migrationBuilder)
        => migrationBuilder.Sql(@"
		 
		 CREATE OR REPLACE FUNCTION public.xsp_gen_next_number(
    p_id UUID
)
RETURNS BIGINT AS $$
DECLARE
    v_ini      BIGINT;
    v_fin      BIGINT;
    v_curr     BIGINT;
    v_inc      BIGINT;
    v_return   BIGINT;
BEGIN
    -- Bloquea la fila para evitar concurrencia
    SELECT ""NumStart"", ""NumEnd"", ""NumCurrent""
    INTO v_ini, v_fin, v_curr
    FROM ""NumberRange""
    WHERE ""Id"" = p_id
    FOR UPDATE;

    -- Calcula siguiente valor y actualiza
    v_inc := v_curr + 1;
    UPDATE ""NumberRange""
    SET ""NumCurrent"" = v_inc
    WHERE ""Id"" = p_id;

    -- Verifica rango y devuelve
    IF v_curr BETWEEN v_ini AND v_fin THEN
        RETURN v_curr;
    ELSE
        -- Si está fuera de rango, deshace la actualización y lanza error
        RAISE EXCEPTION 'Rango agotado';
    END IF;
END;
$$ LANGUAGE plpgsql;
		");
    }
}
