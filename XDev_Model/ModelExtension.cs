using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XDev_Model
{
    public static class ModelExtension
    {
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
