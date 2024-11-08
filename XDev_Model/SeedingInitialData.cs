using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;

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

            // Tipos documento de identificación
            modelBuilder.Entity<IDType>().HasData(new IDType { Id = Guid.Parse("3e4d4e92-7932-4310-8cda-39b8bdba8d07"), Code = "36", Name = "NIT", ConcurrencyStamp = "3e4d4e92-7932-4310-8cda-39b8bdba8d07" },
                                                  new IDType { Id = Guid.Parse("acae8706-50e1-4296-aacd-bd1d59b946d1"), Code = "13", Name = "DUI", ConcurrencyStamp = "acae8706-50e1-4296-aacd-bd1d59b946d1" },
                                                  new IDType { Id = Guid.Parse("429ff286-4d34-42c6-8e6b-254a8d4fa79e"), Code = "37", Name = "Otro", ConcurrencyStamp = "429ff286-4d34-42c6-8e6b-254a8d4fa79e" },
                                                  new IDType { Id = Guid.Parse("132a6258-f433-4345-9da1-cc9cfd0b64ac"), Code = "03", Name = "Pasaporte", ConcurrencyStamp = "132a6258-f433-4345-9da1-cc9cfd0b64ac" },
                                                  new IDType { Id = Guid.Parse("822ac932-46ab-4588-8f82-fbafb14e27eb"), Code = "02", Name = "Carnet Residente", ConcurrencyStamp = "822ac932-46ab-4588-8f82-fbafb14e27eb" });

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
