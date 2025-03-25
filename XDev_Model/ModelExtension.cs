using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XDev_Model
{
    public static class ModelExtension
    {
		public static OperationBuilder<SqlOperation> CreateSPMaterialWareHouseInvoiceCancel(this MigrationBuilder migrationBuilder)
			=> migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[XSP_MATERIAL_WH_INVOICE_CANCEL]
@INVOICEID UNIQUEIDENTIFIER, @TIPOMOV NVARCHAR(1)
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRANSACTION
DECLARE @BRANCHID UNIQUEIDENTIFIER;
DECLARE @MATID UNIQUEIDENTIFIER;
DECLARE @WHID UNIQUEIDENTIFIER;
DECLARE @QTY DECIMAL(18,3);
DECLARE @STOCK DECIMAL(18,3);
DECLARE @LOCKEDSTOCK DECIMAL(18,3);
DECLARE @INTRANSITSTOCK DECIMAL(18,3);
DECLARE @SOLDSTOCK DECIMAL(18,3);

SELECT @BRANCHID = BranchId
FROM INVOICE WHERE Id = @INVOICEID;

DECLARE POSITION_CURSOR CURSOR FOR
SELECT MaterialId, WareHouseId, Quantity
FROM InvoicePosition WHERE InvoiceId = @INVOICEID AND MaterialTypeCode = 'B';

OPEN POSITION_CURSOR;

FETCH NEXT FROM POSITION_CURSOR
INTO @MATID, @WHID, @QTY

WHILE @@FETCH_STATUS = 0
BEGIN

	SELECT @STOCK = Stock, @SOLDSTOCK = SoldStock, @LOCKEDSTOCK = LockedStock, @INTRANSITSTOCK = InTransitStock
	FROM MaterialWareHouse WHERE MaterialId = @MATID AND BranchId = @BRANCHID AND WareHouseId = @WHID;

	IF @TIPOMOV = 'R'
		BEGIN
			UPDATE MaterialWareHouse SET SoldStock = @SOLDSTOCK - @QTY, Stock = @STOCK + @QTY
			WHERE MaterialId = @MATID AND BranchId = @BRANCHID AND WareHouseId = @WHID;
		END

	IF @TIPOMOV = 'I'
		BEGIN
			UPDATE MaterialWareHouse SET Stock = @STOCK - @QTY
			WHERE MaterialId = @MATID AND BranchId = @BRANCHID AND WareHouseId = @WHID;
		END

	FETCH NEXT FROM POSITION_CURSOR
	INTO @MATID, @WHID, @QTY
END

COMMIT TRANSACTION;

CLOSE POSITION_CURSOR;
DEALLOCATE POSITION_CURSOR;");

        public static OperationBuilder<SqlOperation> CreateSPMaterialWareHouseInvoice(this MigrationBuilder migrationBuilder)
		=> migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[XSP_MATERIAL_WH_INVOICE]
@INVOICEID UNIQUEIDENTIFIER, @TIPOMOV NVARCHAR(1)
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRANSACTION
DECLARE @BRANCHID UNIQUEIDENTIFIER;
DECLARE @MATID UNIQUEIDENTIFIER;
DECLARE @WHID UNIQUEIDENTIFIER;
DECLARE @QTY DECIMAL(18,3);
DECLARE @STOCK DECIMAL(18,3);
DECLARE @LOCKEDSTOCK DECIMAL(18,3);
DECLARE @INTRANSITSTOCK DECIMAL(18,3);
DECLARE @SOLDSTOCK DECIMAL(18,3);

SELECT @BRANCHID = BranchId
FROM INVOICE WHERE Id = @INVOICEID;

DECLARE POSITION_CURSOR CURSOR FOR
SELECT MaterialId, WareHouseId, Quantity
FROM InvoicePosition WHERE InvoiceId = @INVOICEID AND MaterialTypeCode = 'B';

OPEN POSITION_CURSOR;

FETCH NEXT FROM POSITION_CURSOR
INTO @MATID, @WHID, @QTY

WHILE @@FETCH_STATUS = 0
BEGIN

	SELECT @STOCK = Stock, @SOLDSTOCK = SoldStock, @LOCKEDSTOCK = LockedStock, @INTRANSITSTOCK = InTransitStock
	FROM MaterialWareHouse WHERE MaterialId = @MATID AND BranchId = @BRANCHID AND WareHouseId = @WHID;

	IF @TIPOMOV = 'R'
		BEGIN
			UPDATE MaterialWareHouse SET SoldStock = @SOLDSTOCK + @QTY, LockedStock = @LOCKEDSTOCK - @QTY
			WHERE MaterialId = @MATID AND BranchId = @BRANCHID AND WareHouseId = @WHID;
		END

	IF @TIPOMOV = 'I'
		BEGIN
			UPDATE MaterialWareHouse SET Stock = @STOCK + @QTY, InTransitStock = @INTRANSITSTOCK - @QTY
			WHERE MaterialId = @MATID AND BranchId = @BRANCHID AND WareHouseId = @WHID;
		END

	FETCH NEXT FROM POSITION_CURSOR
	INTO @MATID, @WHID, @QTY
END

COMMIT TRANSACTION;

CLOSE POSITION_CURSOR;
DEALLOCATE POSITION_CURSOR;");

		public static OperationBuilder<SqlOperation> CreateSPMaterialWareHouseSaleOrder(this MigrationBuilder migrationBuilder)
			=> migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[XSP_MATERIAL_WH_SALE_ORDER]
@MATID UNIQUEIDENTIFIER, @BRANCHID UNIQUEIDENTIFIER, @WHID UNIQUEIDENTIFIER,
@TIPOMOV VARCHAR(1), @QTY DECIMAL(18,3)
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRANSACTION
DECLARE @STOCK DECIMAL(18,3);
DECLARE @LOCKED DECIMAL(18,3);
DECLARE @INTRANSIT DECIMAL(18,3);

SELECT @STOCK = STOCK, @LOCKED = LockedStock, @INTRANSIT = InTransitStock FROM MaterialWareHouse
WHERE MaterialId = @MATID AND BranchId = @BRANCHID AND WareHouseId = @WHID;

IF @TIPOMOV = 'R'
	BEGIN
		UPDATE MaterialWareHouse SET Stock = @STOCK - @QTY, LockedStock = @LOCKED + @QTY
		WHERE MaterialId = @MATID AND BranchId = @BRANCHID AND WareHouseId = @WHID;
	END

IF @TIPOMOV = 'I'
	BEGIN
		UPDATE MaterialWareHouse SET InTransitStock = @INTRANSIT + @QTY
		WHERE MaterialId = @MATID AND BranchId = @BRANCHID AND WareHouseId = @WHID;
	END

COMMIT TRANSACTION;");

        /// <summary>
        /// SP Crear rango de número para Facturación Electrónica SV
        /// </summary>
        /// <param name="migrationBuilder"></param>
        /// <returns></returns>
        public static OperationBuilder<SqlOperation> CreateEBillingDocumentNextNumber(this MigrationBuilder migrationBuilder)
			=> migrationBuilder.Sql(@"

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fernando Reyes
-- Create date: 03/02/2025
-- Description:	Generar número de rango FE 
-- =============================================
CREATE PROCEDURE XSP_EBILLING_CO_NEXT_NUMBER
@coid uniqueidentifier, @doid uniqueidentifier
as
set transaction isolation level read committed
begin transaction
declare @ini bigint;
declare @fin bigint;
declare @curr bigint;			
declare @inc bigint;
declare @return bigint;
declare @reIni bit;
declare @nextY int;

select 			    
	@ini = RangeStart,
	@fin = RangeEnd,
	@return = [Current],
	@reIni = ReStartYear,
	@nextY = NextReStart
	from EBillingCompanyInvoice where CompanyId = @coid and InvoiceTypeId = @doid;

set @inc = @return + 1;

if @inc between @ini and @fin
	begin
		if @reIni = 1 and YEAR(GETDATE()) = @nextY
			begin
				update [dbo].[EBillingCompanyInvoice] 
					set [Current] = 2, NextReStart = @nextY + 1
					where CompanyId = @coid and InvoiceTypeId = @doid;		

				set @return = 1;
			end
		else
			begin
				update [dbo].[EBillingCompanyInvoice] set [Current] = @inc 
				where CompanyId = @coid and InvoiceTypeId = @doid;		
			end

		commit transaction
		Select @return
	end
else
	begin
		if @reIni = 1 
			begin
				update [dbo].[EBillingCompanyInvoice] 
					set [Current] = 2, NextReStart = YEAR(GETDATE()) + 1
				where CompanyId = @coid and InvoiceTypeId = @doid;		

				set @return = 1;

				commit transaction
				Select @return
			end
	    else
			begin
				rollback transaction;
				raiserror( 'Rango agotado', 16, 1 );
			end
	end
");

        /// <summary>
		/// Crear SP para generación de rango de números. Agregar al final de la migración [UP]
		/// </summary>
		/// <param name="migrationBuilder"></param>
		/// <returns></returns>
		public static OperationBuilder<SqlOperation> CreateSPGetNumberNext(this MigrationBuilder migrationBuilder)
        => migrationBuilder.Sql(@"
		 SET ANSI_NULLS ON
		 GO
		 SET QUOTED_IDENTIFIER ON
		 GO
		 -- =============================================
		 -- Author:		Fernando Reyes
		 -- Create date: 11/11/2024
		 -- Description:	Generar número de rango
		 -- =============================================
		 CREATE PROCEDURE XSP_GEN_NEXT_NUMBER         
		 @id uniqueidentifier
         as
         set transaction isolation level read committed
		 begin transaction            
			declare @ini bigint;
			declare @fin bigint;
            declare @curr bigint;			
			declare @inc bigint;
			declare @return bigint;

            select 			    
				@ini = NumStart,
				@fin = NumEnd,
				@return = NumCurrent
			from NumberRange where id = @id;

			set @inc = @return + 1;

			update NumberRange set NumCurrent = @inc where Id = @id;

			if @return between @ini and @fin
				begin
					commit transaction
					Select @return
				end
			else
				begin
					rollback transaction;
					raiserror( 'Rango agotado', 16, 1 );
				end
		");
    }
}
