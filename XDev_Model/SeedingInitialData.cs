using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XDev_Model
{
    public class SeedingInitialData
    {
        const string password = "AQAAAAIAAYagAAAAEG3VrHMqyIN4gVB/lVaj6OGcuVKSCx3EJhfna64rRTI/0qlORLppSj2xkzKyeCrNKA==";
        const string rolId = "1b9a01aa-4f39-4e6c-8012-9f0a894ea01b";
        const string userId = "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5";
        const string sv = "e4100766-8988-47c0-97aa-34be5e88bb44";

        const string sv01 = "d1d544bd-87bb-4938-b706-a4f759634855";
        const string sv02 = "e304c87c-6253-421f-9b34-31f32bf334af";
        const string sv03 = "a5dd1706-916c-4fa2-9d00-20c1b7fa60a8";
        const string sv04 = "cee1a53c-864a-4067-a321-07e14a672536";
        const string sv05 = "9db618b7-d112-4e22-bed7-43ed14c930cf";
        const string sv06 = "60873ca6-590b-4f63-b680-a4fc38338de5";
        const string sv07 = "06bea8a8-5c85-4523-87fd-dde6b981d2fd";
        const string sv08 = "a673111d-5d43-4ef9-bd8a-e22c0d626ad3";
        const string sv09 = "581ff0b4-7fb5-4702-8ff8-e040a93aa669";
        const string sv10 = "d7647a83-a047-426b-976d-77ec7b62e9f2";
        const string sv11 = "e6651357-535d-489c-9d58-0fd514df9c08";
        const string sv12 = "68792ae9-a79d-4c01-b0b8-fdd27d90eee8";
        const string sv13 = "0cabeef6-210c-4e02-b826-4d0dd12435ca";
        const string sv14 = "4bb49b77-bb90-41b2-91fd-52badd37c844";

        public static void Seed(ModelBuilder modelBuilder)
        {
            var roladmin = new ApplicationRole()
            {
                Id = rolId,
                Name = "admin",
                RoleName = "Administrador de Sistema",
                NormalizedName = "ADMIN"
            };

            var useradmin = new ApplicationUser()
            {
                Id = userId,
                UserName = "admin@avalink.com",
                NormalizedUserName = "ADMIN@AVALINK.COM",
                Email = "admin@avalink.com",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@AVALINK.COM",
                Name = "Administrador de Sistema",
                PasswordHash = password,
                Active = true,
            };

            modelBuilder.Entity<ApplicationRole>().HasData(roladmin, new ApplicationRole { Id = "0da061ae-e10f-4516-a727-59363d4fbacc", Name = "user", RoleName = "Usuario", NormalizedName = "USER" });
            modelBuilder.Entity<ApplicationUser>().HasData(useradmin);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = rolId, UserId = userId });

            // Tipos de sociedad
            modelBuilder.Entity<CompanyType>().HasData(new CompanyType { Id = Guid.Parse("cf579d9d-18f9-432b-806a-4cad7311fb38"), Code = "N", Name = "Persona Natural", ConcurrencyStamp = "cf579d9d-18f9-432b-806a-4cad7311fb38" },
                                                       new CompanyType { Id = Guid.Parse("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"), Code = "J", Name = "Jurídica", ConcurrencyStamp = "099a35d5-fddf-486d-b3b7-ba8011b1a7ff" });

            modelBuilder.Entity<BranchType>().HasData(new BranchType { Id = Guid.Parse("7ca98566-a39c-41fc-bd63-237dd34eb344"), Code = "01", Name = "Sucursal/Agencia", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                      new BranchType { Id = Guid.Parse("ee69a655-5b95-4597-99be-9cb35ed2bd50"), Code = "02", Name = "Casa matriz", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                      new BranchType { Id = Guid.Parse("34b9aa6b-657e-4d57-8bd2-41ba2a8ebc49"), Code = "04", Name = "Bodega", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                      new BranchType { Id = Guid.Parse("1190eda6-4aec-493b-876f-c834f8c0da9b"), Code = "07", Name = "Predio y/o patio", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                      new BranchType { Id = Guid.Parse("2baf6241-db40-4291-8e80-6652f239783a"), Code = "20", Name = "Otro", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" });

            // Tipos de dirección
            modelBuilder.Entity<AddressType>().HasData(new AddressType { Id = Guid.Parse("f79873a7-7f73-4f29-ad1b-b345265a9738"), Code = "DO", Name = "Domicilio", ConcurrencyStamp = "f79873a7-7f73-4f29-ad1b-b345265a9738" },
                                                       new AddressType { Id = Guid.Parse("b880fa62-d045-40f6-8a80-71abeaffa289"), Code = "FE", Name = "Facturación", ConcurrencyStamp = "b880fa62-d045-40f6-8a80-71abeaffa289" },
                                                       new AddressType { Id = Guid.Parse("ca17d52b-7221-4476-82c4-ef77b8203d37"), Code = "DL", Name = "Entrega", ConcurrencyStamp = "ca17d52b-7221-4476-82c4-ef77b8203d37" });

            // Tipos de Socios            
            modelBuilder.Entity<PartnerType>().HasData(new PartnerType { Id = Guid.Parse("cf579d9d-18f9-432b-806a-4cad7311fb38"), Code = "P", Name = "Persona", ConcurrencyStamp = "cf579d9d-18f9-432b-806a-4cad7311fb38" },
                                                       new PartnerType { Id = Guid.Parse("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"), Code = "O", Name = "Organización", ConcurrencyStamp = "099a35d5-fddf-486d-b3b7-ba8011b1a7ff" });

            // Personalización socios
            modelBuilder.Entity<PartnerFeatures>().HasData(new PartnerFeatures { Id = Guid.Parse("7ca98566-a39c-41fc-bd63-237dd34eb344"), NumType = 0, ConcurrencyStamp = "7ca98566-a39c-41fc-bd63-237dd34eb344" });

            // Roles socios            
            modelBuilder.Entity<PartnerRole>().HasData(new PartnerRole { Id = Guid.Parse("cf579d9d-18f9-432b-806a-4cad7311fb38"), Code = "D", Name = "Cliente (Deudor)", ConcurrencyStamp = "cf579d9d-18f9-432b-806a-4cad7311fb38" },
                                                       new PartnerRole { Id = Guid.Parse("099a35d5-fddf-486d-b3b7-ba8011b1a7ff"), Code = "A", Name = "Proveedor (Acreedor)", ConcurrencyStamp = "099a35d5-fddf-486d-b3b7-ba8011b1a7ff" },
                                                       new PartnerRole { Id = Guid.Parse("0f8d6ce6-6177-4892-843a-11e2af8aa134"), Code = "C", Name = "Contacto", ConcurrencyStamp = "0f8d6ce6-6177-4892-843a-11e2af8aa134" });

            // Tipos documento de identificación
            modelBuilder.Entity<IDType>().HasData(new IDType { Id = Guid.Parse("3e4d4e92-7932-4310-8cda-39b8bdba8d07"), Code = "NIT", AltCode= "36", Name = "NIT", ConcurrencyStamp = "3e4d4e92-7932-4310-8cda-39b8bdba8d07" },
                                                  new IDType { Id = Guid.Parse("acae8706-50e1-4296-aacd-bd1d59b946d1"), Code = "DUI", AltCode = "13", Name = "DUI", ConcurrencyStamp = "acae8706-50e1-4296-aacd-bd1d59b946d1" },
                                                  new IDType { Id = Guid.Parse("429ff286-4d34-42c6-8e6b-254a8d4fa79e"), Code = "OTH", AltCode = "37", Name = "Otro", ConcurrencyStamp = "429ff286-4d34-42c6-8e6b-254a8d4fa79e" },
                                                  new IDType { Id = Guid.Parse("132a6258-f433-4345-9da1-cc9cfd0b64ac"), Code = "PAS", AltCode = "03", Name = "Pasaporte", ConcurrencyStamp = "132a6258-f433-4345-9da1-cc9cfd0b64ac" },
                                                  new IDType { Id = Guid.Parse("822ac932-46ab-4588-8f82-fbafb14e27eb"), Code = "CR", AltCode = "02", Name = "Carnet Residente", ConcurrencyStamp = "822ac932-46ab-4588-8f82-fbafb14e27eb" },
                                                  new IDType { Id = Guid.Parse("7ae985b6-e461-4a66-b028-badf1f16f9f0"), Code = "NRC", AltCode = "", Name = "Número Registro Contribuyente", ConcurrencyStamp = "7ae985b6-e461-4a66-b028-badf1f16f9f0" });

            // Moneda
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = Guid.Parse("cf579d9d-18f9-432b-806a-4cad7311fb38"), Code = "USD", Name = "Dólar Estadounidense", ConcurrencyStamp = "cf579d9d-18f9-432b-806a-4cad7311fb38" });

            // Tipos de Material
            modelBuilder.Entity<MaterialType>().HasData(new MaterialType { Id = Guid.Parse("0f8d6ce6-6177-4892-843a-11e2af8aa134"), Code = "B", Name = "Bienes", ConcurrencyStamp = "0f8d6ce6-6177-4892-843a-11e2af8aa134" },
                                                        new MaterialType { Id = Guid.Parse("691f3725-3ef8-434b-aecf-b663791cc501"), Code = "S", Name = "Servicios", ConcurrencyStamp = "691f3725-3ef8-434b-aecf-b663791cc501" });

            // Personalización materiales
            modelBuilder.Entity<MaterialFeatures>().HasData(new MaterialFeatures { Id = Guid.Parse("7ca98566-a39c-41fc-bd63-237dd34eb344"), NumType = 0, ConcurrencyStamp = "7ca98566-a39c-41fc-bd63-237dd34eb344" });

            // Unidades de medida
            modelBuilder.Entity<UnitMeasure>().HasData(new UnitMeasure { Id = Guid.Parse("07f1cfbc-2b69-4c76-910c-e1c38eeaa9fc"), Code = "UN", AltCode = "59", Name = "Unidad", ConcurrencyStamp = "07f1cfbc-2b69-4c76-910c-e1c38eeaa9fc" });

            // Politicas
            modelBuilder.Entity<Policy>().HasData(new Policy { Id = Guid.Parse("3cefd509-9d98-4295-bb45-0b7624b13b3d"), Name = "Lectura" },
                                                  new Policy { Id = Guid.Parse("5fdca958-95a0-4656-bc19-688d60d4ffe6"), Name = "Creación" },
                                                  new Policy { Id = Guid.Parse("35efe9e5-c406-492c-bc8f-af5589bad426"), Name = "Actualización" },
                                                  new Policy { Id = Guid.Parse("db8f9a1e-39f4-4ce8-bbb7-479843ee9ef3"), Name = "Eliminación" },
                                                  new Policy { Id = Guid.Parse("81630d88-7dd9-4100-bc3b-85dc36b8ae9c"), Name = "Listado" });

            // Facturación Eletrónica
            modelBuilder.Entity<EBilling>().HasData(new EBilling { Id = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), 
                                                                   Code = "MHSV",
                                                                   UrlTest= "https://apitest.dtes.mh.gob.sv", 
                                                                   UrlProd= "https://api.dtes.mh.gob.sv", 
                                                                   UrlSigner= "http://localhost:8113",                                                                    
                                                                   Name = "Ministerio de Hacienda de El Salvador", 
                                                                   ConcurrencyStamp = "63be85df-6805-41c3-beb2-f6a44db746f6" });

            // Facturación Eletrónica - Documentos
            modelBuilder.Entity<EBillingDocument>().HasData(new EBillingDocument { EBillingId = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), Id = Guid.Parse("6d339ff2-e58c-4eba-95e9-dd0df0d1abbe"), Code = "01", Name = "Factura", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                           new EBillingDocument { EBillingId = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), Id = Guid.Parse("d4a32bf7-a3dd-4623-81f4-5a9994e6c9d0"), Code = "03", Name = "Comprobante crédito fiscal", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                           //new EBillingDocument { Id = Guid.Parse("0ec19780-5f12-4b29-b862-8af8aa59e99d"), Code = "04", Name = "Nota de remisión", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                           new EBillingDocument { EBillingId = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), Id = Guid.Parse("1dc5c31b-adb6-40b7-8523-5a91ecaea5a3"), Code = "05", Name = "Nota de crédito", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                           new EBillingDocument { EBillingId = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), Id = Guid.Parse("9ca389cf-65aa-4b54-bb1b-0080efaa6cb2"), Code = "06", Name = "Nota de débito", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                           //new EBillingDocument { Id = Guid.Parse("a27592f5-9804-4627-a06b-8dc04f196e96"), Code = "07", Name = "Comprobante de retención", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                           //new EBillingDocument { Id = Guid.Parse("183736f4-7079-4662-bffa-062c5b27eda8"), Code = "08", Name = "Comprobante de liquidación", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                           //new EBillingDocument { Id = Guid.Parse("165f0029-a831-4032-8c2e-8cc107858c6e"), Code = "09", Name = "Documento contable de liquidación", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },                                                           
                                                           //new EBillingDocument { Id = Guid.Parse("87a90d09-33a8-4ef6-a0cb-9aa94d6d32d7"), Code = "14", Name = "Factura sujeto excluido", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                           //new EBillingDocument { Id = Guid.Parse("dff8fa11-50ad-4ae6-b15f-38a32fa7507a"), Code = "15", Name = "Comprobante donación", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" },
                                                           new EBillingDocument { EBillingId = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), Id = Guid.Parse("1f0e23ab-5e6b-4fca-936e-fb1ea18b40af"), Code = "11", Name = "Factura exportador", ConcurrencyStamp = "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a" });

            // Facturación Eletrónica - Impuestos
            modelBuilder.Entity<EBillingTax>().HasData(new EBillingTax { EBillingId = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), Id = Guid.Parse("6b1fe653-82f8-43f5-a674-c4d5cba0b1b9"), TaxCode = "20", TaxName = "Impuesto al valor agregado", ConcurrencyStamp = "6b1fe653-82f8-43f5-a674-c4d5cba0b1b9" },
                                                       new EBillingTax { EBillingId = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), Id = Guid.Parse("5714c5db-4033-4c8f-a425-13186fb02220"), TaxCode = "C3", TaxName = "Impuesto al valor agregado (exportaciones)", ConcurrencyStamp = "5714c5db-4033-4c8f-a425-13186fb02220" },
                                                       new EBillingTax { EBillingId = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), Id = Guid.Parse("845ee146-38ca-4041-90f4-b8411274fa15"), TaxCode = "59", TaxName = "Turismo: por alojamiento", ConcurrencyStamp = "845ee146-38ca-4041-90f4-b8411274fa15" },
                                                       new EBillingTax { EBillingId = Guid.Parse("63be85df-6805-41c3-beb2-f6a44db746f6"), Id = Guid.Parse("8507b0ca-b579-4c2a-90fc-d8ec516ba909"), TaxCode = "71", TaxName = "Turismo: salida del país por vía aérea", ConcurrencyStamp = "8507b0ca-b579-4c2a-90fc-d8ec516ba909" });

            // Metodos de Pago
            modelBuilder.Entity<PaymentCondition>().HasData(new PaymentCondition { Id = Guid.Parse("b3d138d6-0f3d-48be-a96a-e9d6f3922c05"), Code = "D00", Name = "Contado", Tipo = "1", Plazo = "", PlazoCount = 0, ConcurrencyStamp = "b3d138d6-0f3d-48be-a96a-e9d6f3922c05" },
                                                            new PaymentCondition { Id = Guid.Parse("5fd46367-0c46-4fe0-b648-29dafd49b80c"), Code = "D08", Name = "Crédito 8 días", Tipo = "2", Plazo = "01", PlazoCount = 8, ConcurrencyStamp = "5fd46367-0c46-4fe0-b648-29dafd49b80c" },
                                                            new PaymentCondition { Id = Guid.Parse("b500120a-72f0-47cf-b4a0-edfd4fca7abb"), Code = "D15", Name = "Crédito 15 días", Tipo = "2", Plazo = "01", PlazoCount = 15, ConcurrencyStamp = "b500120a-72f0-47cf-b4a0-edfd4fca7abb" },
                                                            new PaymentCondition { Id = Guid.Parse("c1484b56-6de2-46fb-97f8-a22aae14480d"), Code = "D30", Name = "Crédito 30 días", Tipo = "2", Plazo = "01", PlazoCount = 30, ConcurrencyStamp = "c1484b56-6de2-46fb-97f8-a22aae14480d" });

            // Medios de Pago
            modelBuilder.Entity<MeanOfPayment>().HasData(new MeanOfPayment { Id = Guid.Parse("0674f1dd-e567-4ddd-90d6-500f01aaed2e"), Code = "01", Name = "Billetes y monedas", ConcurrencyStamp = "0674f1dd-e567-4ddd-90d6-500f01aaed2e" },
                                                         new MeanOfPayment { Id = Guid.Parse("76c955a3-0c97-4f18-846f-2b0765ce3a66"), Code = "02", Name = "Tarjeta de débito", ConcurrencyStamp = "76c955a3-0c97-4f18-846f-2b0765ce3a66" },
                                                         new MeanOfPayment { Id = Guid.Parse("ba614885-ea45-463a-8e3f-7e894902f74f"), Code = "03", Name = "Tarjeta de crédito", ConcurrencyStamp = "ba614885-ea45-463a-8e3f-7e894902f74f" },
                                                         new MeanOfPayment { Id = Guid.Parse("b5b02924-fd31-4790-a220-3218698aac6e"), Code = "04", Name = "Cheque", ConcurrencyStamp = "b5b02924-fd31-4790-a220-3218698aac6e" },
                                                         new MeanOfPayment { Id = Guid.Parse("199b7755-1265-4cda-a1e3-535c867bac21"), Code = "05", Name = "Transferencia-Depósito Bancario", ConcurrencyStamp = "199b7755-1265-4cda-a1e3-535c867bac21" },                                                         
                                                         new MeanOfPayment { Id = Guid.Parse("0f6abca4-6b6a-4d13-8d32-761097171581"), Code = "08", Name = "Dinero electrónico", ConcurrencyStamp = "0f6abca4-6b6a-4d13-8d32-761097171581" },
                                                         new MeanOfPayment { Id = Guid.Parse("2084c2e1-5b24-4c79-bdab-ef75e18d559c"), Code = "09", Name = "Monedero electrónico", ConcurrencyStamp = "2084c2e1-5b24-4c79-bdab-ef75e18d559c" },                                                         
                                                         new MeanOfPayment { Id = Guid.Parse("8f305418-ec29-4231-a0fc-1ff523933f68"), Code = "11", Name = "Bitcoin", ConcurrencyStamp = "8f305418-ec29-4231-a0fc-1ff523933f68" },
                                                         new MeanOfPayment { Id = Guid.Parse("a4dd8f5b-cd62-4157-9912-130b8ffccf27"), Code = "12", Name = "Otras criptomonedas", ConcurrencyStamp = "a4dd8f5b-cd62-4157-9912-130b8ffccf27" },
                                                         new MeanOfPayment { Id = Guid.Parse("99b7591d-c073-4506-87d1-f01363444b66"), Code = "13", Name = "Cuentas por pagar del receptor", ConcurrencyStamp = "a4dd8f5b-cd62-4157-9912-130b8ffccf27" },
                                                         new MeanOfPayment { Id = Guid.Parse("0ddf88a1-d603-4ec3-80e2-de7fccc4a1c4"), Code = "14", Name = "Giro bancario", ConcurrencyStamp = "a4dd8f5b-cd62-4157-9912-130b8ffccf27" });

            // País
            modelBuilder.Entity<Country>().HasData(new Country() { Id = Guid.Parse(sv), Code = "SV", Name = "El Salvador", ConcurrencyStamp = sv });

            // Departamentos - Regiones
            modelBuilder.Entity<Region>().HasData(new Region() { Id = Guid.Parse(sv02), Code = "02", Name = "Santa Ana", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv01), Code = "01", Name = "Ahuachapán", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv03), Code = "03", Name = "Sonsonate", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv05), Code = "05", Name = "La Libertad", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv04), Code = "04", Name = "Chalatenango", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv06), Code = "06", Name = "San Salvador", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv09), Code = "09", Name = "Cabañas", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv07), Code = "07", Name = "Cuscatlán", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv08), Code = "08", Name = "La Paz", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv10), Code = "10", Name = "San Viciente", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv11), Code = "11", Name = "Usulután", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv13), Code = "13", Name = "Morazán", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv12), Code = "12", Name = "San Miguel", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv },
                                                  new Region() { Id = Guid.Parse(sv14), Code = "14", Name = "La Unión", CountryId = Guid.Parse(sv), ConcurrencyStamp = sv });

            // Municipios - Ahuachapán
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("18f034f3-6f14-42ab-b136-0b45f4d69041"), Code = "13", Name = "Ahuachapán Norte", RegionId = Guid.Parse(sv01), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("dd502b5c-1e53-4f54-8076-0f083ed753e2"), Code = "14", Name = "Ahuachapán Centro", RegionId = Guid.Parse(sv01), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("5ca38eb9-4aa7-4781-b63f-6ccb8756ee0d"), Code = "15", Name = "Ahuachapán Sur", RegionId = Guid.Parse(sv01), ConcurrencyStamp = sv });

            // Municipios - Santa Ana
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("18104203-f67f-403c-bc4d-57e4b2a99660"), Code = "14", Name = "Santa Ana Norte", RegionId = Guid.Parse(sv02), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("618a98fd-1398-4d51-aa21-4da10282aab3"), Code = "15", Name = "Santa Ana Centro", RegionId = Guid.Parse(sv02), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("16b8e71d-b065-422b-80e8-5f34b0ab8325"), Code = "16", Name = "Santa Ana Este", RegionId = Guid.Parse(sv02), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("0ccf047f-a16a-4df2-84f1-19e8ff66a478"), Code = "17", Name = "Santa Ana Oeste", RegionId = Guid.Parse(sv02), ConcurrencyStamp = sv });

            // Municipios - Sonsonate - 03
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("a159243d-f033-420f-a5fb-bc6ff14990a8"), Code = "17", Name = "Sonsonate Norte", RegionId = Guid.Parse(sv03), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("4f0336a6-af12-40eb-9e31-feae9ed39ad1"), Code = "18", Name = "Sonsonate Centro", RegionId = Guid.Parse(sv03), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("e6505b36-f339-4f4d-a89f-4c7f94ec9a69"), Code = "19", Name = "Sonsonate Este", RegionId = Guid.Parse(sv03), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("6170dfff-3bfd-4761-af82-f429615e6e2a"), Code = "20", Name = "Sonsonate Oeste", RegionId = Guid.Parse(sv03), ConcurrencyStamp = sv });

            // Municipios - Chalatenango - 04
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("55be3620-bbc8-448a-9414-cf072ada0e66"), Code = "34", Name = "Chalatenango Norte", RegionId = Guid.Parse(sv04), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("7fb4517c-3bfb-4a18-adde-4fb1ecffaddc"), Code = "35", Name = "Chalatenango Centro", RegionId = Guid.Parse(sv04), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("a675251d-7724-4518-854c-63702c3d9ad7"), Code = "35", Name = "Chalatenango Sur", RegionId = Guid.Parse(sv04), ConcurrencyStamp = sv });

            // Municipios - La Libertad - 05
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("c9e803de-0b24-44f7-817a-ee573f916fc8"), Code = "23", Name = "La Libertad Norte", RegionId = Guid.Parse(sv05), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("07da0c22-42c9-4c6e-94c0-1ed0a77338a6"), Code = "24", Name = "La Libertad Centro", RegionId = Guid.Parse(sv05), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("b4570640-2ced-4b69-9b11-ee627d837f54"), Code = "25", Name = "La Libertad Oeste", RegionId = Guid.Parse(sv05), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("344e3a42-bc56-4312-a1dc-3291ae8b960c"), Code = "26", Name = "La Libertad Este", RegionId = Guid.Parse(sv05), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("482bc1a5-d37c-4d8f-8fb5-12645a257372"), Code = "27", Name = "La Libertad Costa", RegionId = Guid.Parse(sv05), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("250d6f4a-b0dd-4cf2-820f-b687a48d5411"), Code = "28", Name = "La Libertad Sur", RegionId = Guid.Parse(sv05), ConcurrencyStamp = sv });

            // Municipios - San Salvador - 06
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("b2fb4766-2c8e-496e-baa8-1ea23198eb6f"), Code = "20", Name = "San Salvador Norte", RegionId = Guid.Parse(sv06), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("c4ec20e4-9827-4763-9d26-b794d07edad4"), Code = "21", Name = "San Salvador Oeste", RegionId = Guid.Parse(sv06), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("48950ab6-a7cb-4dd3-b3b8-07d206d6edee"), Code = "22", Name = "San Salvador Este", RegionId = Guid.Parse(sv06), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("c9bb2770-5bb3-40ae-a13b-49c1bb1756fe"), Code = "23", Name = "San Salvador Centro", RegionId = Guid.Parse(sv06), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("86dc9ec6-18fc-4b6c-80d9-c9958b47ff09"), Code = "24", Name = "San Salvador Sur", RegionId = Guid.Parse(sv06), ConcurrencyStamp = sv });

            // Municipios - Cuscatlán - 07
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("9ce12c0c-ec42-45e3-ace0-3068108ecfdc"), Code = "17", Name = "Cuscatlán Norte", RegionId = Guid.Parse(sv07), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("1e445660-55ed-48f5-94da-bc82a39a19a7"), Code = "18", Name = "Cuscatlán Sur", RegionId = Guid.Parse(sv07), ConcurrencyStamp = sv });

            // Municipios - La Paz - 08
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("d9e42bc5-6735-4970-9e07-da3ee30846b1"), Code = "23", Name = "La Paz Oeste", RegionId = Guid.Parse(sv08), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("b1eb55d2-861b-4e7b-ab23-87e5cad5589e"), Code = "24", Name = "La Paz Centro", RegionId = Guid.Parse(sv08), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("89cc4d95-696b-41d9-b6ac-8bdf16edc5c4"), Code = "25", Name = "La Paz Este", RegionId = Guid.Parse(sv08), ConcurrencyStamp = sv });

            // Municipios - Cabañas - 09
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("64c92aba-4806-4c3b-8d81-7eeb330ed99f"), Code = "10", Name = "Cabañas Oeste", RegionId = Guid.Parse(sv09), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("a51b63d3-833b-470c-8d45-55ab7813a5b6"), Code = "11", Name = "Cabañas Este", RegionId = Guid.Parse(sv09), ConcurrencyStamp = sv });

            // Municipios - San Vicente - 10
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("629daa24-d3cc-479a-b6df-ec82f3c9eb59"), Code = "14", Name = "San Vicente Norte", RegionId = Guid.Parse(sv10), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("f0e33c30-a55b-47af-a4a7-394838d9b5e7"), Code = "15", Name = "San Vicente Sur", RegionId = Guid.Parse(sv10), ConcurrencyStamp = sv });

            // Municipios - Usulután - 11
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("1b37faab-df87-41a5-83fb-849964858948"), Code = "24", Name = "Usulután Norte", RegionId = Guid.Parse(sv11), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("5e6d2441-9e29-4ca0-b280-c172b77c21c0"), Code = "25", Name = "Usulután Este", RegionId = Guid.Parse(sv11), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("077fe4b8-1725-43af-8f60-b5ab6a37fa1d"), Code = "26", Name = "Usulután Oeste", RegionId = Guid.Parse(sv11), ConcurrencyStamp = sv });

            // Municipios - San Miguel - 12
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("62f027d6-a995-4f75-8db9-661d32c6f350"), Code = "21", Name = "San Miguel Norte", RegionId = Guid.Parse(sv12), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("fbe21ebf-4ecf-4065-8026-9eccc562df68"), Code = "22", Name = "San Miguel Centro", RegionId = Guid.Parse(sv12), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("96170536-93df-453b-9449-ac0ba51773b2"), Code = "23", Name = "San Miguel Oeste", RegionId = Guid.Parse(sv12), ConcurrencyStamp = sv });

            // Municipios - Morazán - 13
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("ce1dd042-4fbd-4ffc-8742-2d370a434afb"), Code = "27", Name = "Morazán Norte", RegionId = Guid.Parse(sv13), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("9b8604b8-26b2-446c-9756-32f8754f1b17"), Code = "28", Name = "Morazán Sur", RegionId = Guid.Parse(sv13), ConcurrencyStamp = sv });

            // Municipios - La Unión - 14
            modelBuilder.Entity<City>().HasData(new City() { Id = Guid.Parse("8e9b24f6-20cd-4aa0-9948-c4751496f015"), Code = "19", Name = "La Unión Norte", RegionId = Guid.Parse(sv14), ConcurrencyStamp = sv },
                                                new City() { Id = Guid.Parse("698f82d6-e58b-494f-ac81-167bda60f538"), Code = "20", Name = "La Unión Sur", RegionId = Guid.Parse(sv14), ConcurrencyStamp = sv });
        }
    }
}
