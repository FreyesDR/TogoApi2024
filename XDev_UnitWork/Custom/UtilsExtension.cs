using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Claims;
using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Custom
{
    public static class UtilsExtension
    {
        #region Constantes
        public static readonly string InternalErrorMessage = "Ocurrió un error interno en el servidor";
        internal static readonly string fieldRequired = "Campo es requerido";
        internal static readonly string propertyRequired = "La propiedad es requerida";
        internal static readonly string fieldMaxLength = "La longitud máxima es de {MaxLength} caracteres";
        internal static readonly string fieldLengthBetween = "Debe tener entre {MinLength} y {MaxLength} caracteres";
        internal static readonly string fieldLessThan = "El valor debe ser menor que {ComparisonValue}";
        internal static readonly string fieldGreaterThan = "El valor debe ser mayor que {ComparisonValue}";
        internal static readonly string fieldLessThanOrEqualTo = "El valor debe ser menor o igual a {ComparisonValue}";
        internal static readonly string fieldGreaterThanOrEqualTo = "El valor debe ser mayor o igual a {ComparisonValue}";
        internal static readonly string fieldLength = "Longitud de {MaxLength} caracteres";
        internal static readonly string fieldEmail = "Debe ser un correo electrónico válido";
        internal static readonly string fieldMinLength = "La longitud mínima es de {MinLength} caracteres";
        internal static readonly string fieldUrlInvalid = "Url incorrecta";
        internal static readonly string fieldDate = "No tiene el formato correcto YYYY-MM-DD";
        internal static readonly string fieldNotFormat = "No tiene el formato correcto";
        internal static readonly string fieldPrecisionScale = "No debe tener más de {ExpectedPrecision} dígitos en total, con margen para {ExpectedScale} decimales. Se encontraron {Digits} y {ActualScale} decimales";
        internal static readonly string fieldInclusiveBetween = "Debe estar entre {From} y {To}. Actualmente tiene un valor de {PropertyValue}";

        public static readonly string SectionKey = "Authentication:Schemes:Bearer:SigningKeys";
        public static readonly string SectionKeyIssuer = "Issuer";
        public static readonly string SectionKeyIssuerValue = "Value";
        public static readonly string IssuerOwn = "TogoApi-App";
        #endregion  

        private static readonly MethodInfo OrderByMethod = typeof(Queryable).GetMethods().Single(method =>
        method.Name == "OrderBy" && method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod = typeof(Queryable).GetMethods().Single(method =>
            method.Name == "OrderByDescending" && method.GetParameters().Length == 2);


        public static Guid GetGuid(this string str)
        {
            if (str.IsNullOrEmpty()) return Guid.Empty;

            if (Guid.TryParse(str, out Guid guid)) return guid;

            return Guid.Empty;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        #region Utilizades Http

        public static string GetCurrentUserId(IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var idClaim = contextAccessor.HttpContext.User.Identities.FirstOrDefault().Claims.Where(w => w.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                if (idClaim is not null)
                {
                    return idClaim.Value;
                }
                return Guid.Empty.ToString();
            }

            return string.Empty;
        }
        public static bool IsValidUri(this string str)
        {
            return Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out var uri);

        }

        public static Task DeleteFile(IWebHostEnvironment env, string filePath, string contenedor)
        {
            if (filePath.IsNullOrEmpty()) return Task.CompletedTask;

            var fileName = Path.GetFileName(filePath);
            var path = Path.Combine(env.WebRootPath, contenedor, fileName);

            if (File.Exists(path))
                File.Delete(path);

            return Task.CompletedTask;
        }

        public static async Task<string> SaveImage(IWebHostEnvironment env, IHttpContextAccessor contextAccessor, string contenedor, IFormFile file)
        {
            var fileSaveName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            var path = Path.Combine(env.WebRootPath, contenedor);

            var fullFile = Path.Combine(path, fileSaveName);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                await File.WriteAllBytesAsync(fullFile, memoryStream.ToArray());
            }

            var url = $"{contextAccessor.HttpContext.Request.Scheme}://{contextAccessor.HttpContext.Request.Host}";
            return Path.Combine(url, contenedor, fileSaveName).Replace("\\", "/");
        }

        public static async Task<string> SaveImage(IWebHostEnvironment env, IHttpContextAccessor contextAccessor, string contenedor, byte[] image, string id)
        {
            var fileSaveName = Guid.Parse(id).ToString("N") + ".bmp";
            var path = Path.Combine(env.WebRootPath, contenedor);

            var fullFile = Path.Combine(path, fileSaveName);

            await File.WriteAllBytesAsync(fullFile, image);

            var url = $"{contextAccessor.HttpContext.Request.Scheme}://{contextAccessor.HttpContext.Request.Host}";
            return Path.Combine(url, contenedor, fileSaveName).Replace("\\", "/");
        }

        public static async Task<bool> HostFound(string url)
        {
            using var client = new HttpClient();
            try
            {
                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.HostNotFound })
            {
                return false;
            }
            catch (HttpRequestException e) when (e.StatusCode.HasValue && (int)e.StatusCode.Value > 500)
            {
                return true;
            }
        }
        public static T GetValueOrDefault<T>(this HttpContext context, string key, T value) where T : IParsable<T>
        {
            string valor = context.Request.Query[key];

            if (valor.IsNullOrEmpty())
            {
                return value;
            }

            return T.Parse(valor!, null);
        }

        public static string GetValueOrDefault(this HttpContext context, string key, string value)
        {
            string valor = context.Request.Query[key];

            if (valor.IsNullOrEmpty())
            {
                return value;
            }

            return valor;
        }
        #endregion

        #region Utilizades Paginación
        private static IOrderedQueryable<T> CreateOrderBy<T>(this IQueryable<T> query, string fieldname, OrderDirection direction)
        {
            var parameter = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(parameter, fieldname);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, parameter);

            switch (direction)
            {
                case OrderDirection.ascending:
                    MethodInfo asc = OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
                    return (IOrderedQueryable<T>)asc.Invoke(null, new object[] { query, lambda });
                case OrderDirection.descending:
                    MethodInfo desc = OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
                    return (IOrderedQueryable<T>)desc.Invoke(null, new object[] { query, lambda });
                default:
                    MethodInfo def = OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
                    return (IOrderedQueryable<T>)def.Invoke(null, new object[] { query, lambda });
            }
        }


        public static IQueryable<T> CreateFilterAndOrder<T>(this IQueryable<T> query, PaginationDTO dto)
        {
            if (!dto.Filter.IsNullOrEmpty())
            {
                Expression<Func<T, bool>> expression = null;

                Expression filterExpression = null;
                var parameter = Expression.Parameter(typeof(T), "filter");

                JArray jArray = JArray.Parse(dto.Filter);

                foreach (JObject obj in jArray)
                {
                    if (!string.IsNullOrEmpty(JObject.Parse(obj.ToString()).First.First().ToString()))
                    {
                        var property = Expression.Property(parameter, JObject.Parse(obj.ToString()).First.Path);
                        var constant = Expression.Constant(JObject.Parse(obj.ToString()).First.First().ToString());
                        Expression comparison = null;

                        if (property.Type == typeof(string))
                        {
                            comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                        }

                        if (property.Type == typeof(Int16))
                        {
                            constant = Expression.Constant(Convert.ToInt16(JObject.Parse(obj.ToString()).First.First().ToString()));
                            comparison = Expression.Equal(property, constant);
                        }

                        if (property.Type == typeof(Int32))
                        {
                            constant = Expression.Constant(Convert.ToInt32(JObject.Parse(obj.ToString()).First.First().ToString()));
                            comparison = Expression.Equal(property, constant);
                        }

                        if (property.Type == typeof(Int64))
                        {
                            constant = Expression.Constant(Convert.ToInt64(JObject.Parse(obj.ToString()).First.First().ToString()));
                            comparison = Expression.Equal(property, constant);
                        }

                        if (property.Type == typeof(Boolean))
                        {
                            constant = Expression.Constant(JObject.Parse(obj.ToString()).First.First().ToString() == "0" ? false : true);
                            comparison = Expression.Equal(property, constant);
                        }

                        filterExpression = filterExpression == null ? comparison : Expression.And(filterExpression, comparison);

                    }
                }

                if (filterExpression != null)
                    expression = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);

                if (expression != null)
                    query = query.Where(expression);
            }

            if (!dto.SortField.IsNullOrEmpty())
                query = query.CreateOrderBy(dto.SortField, dto.SortOrder);

            return query;
        }

        public async static Task<List<TDto>> CreatePaging<T, TDto>(this IQueryable<T> query, PaginationDTO parameters, HttpContext httpContext)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<T, TDto>(); });
            config.AssertConfigurationIsValid();

            var itemsCount = await query.CountAsync();

            int totalPages = (int)Math.Ceiling((double)itemsCount / (double)parameters.PageSize);

            if (totalPages == 0)
                totalPages = 1;

            if (parameters.Page > totalPages)
                parameters.Page = totalPages;

            var list = await query.Skip((parameters.Page - 1) * parameters.PageSize).Take(parameters.PageSize).ToListAsync();

            httpContext.Response.Headers.Append("items", itemsCount.ToString());
            httpContext.Response.Headers.Append("pages", totalPages.ToString());
            httpContext.Response.Headers.Append("page", parameters.Page.ToString());

            return new Mapper(config).Map<List<TDto>>(list);
        }
        #endregion

        #region Número a letras
        public static string AmountToLetters(this decimal numberAsString)
        {
            string dec;

            var entero = Convert.ToInt64(Math.Truncate(numberAsString));
            var decimales = Convert.ToInt32(Math.Round((numberAsString - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = $" CON {decimales:0,0}/100";
            }
            else
            {
                dec = $" CON {decimales:0,0}/100";
            }
            var res = NumberToLetters(Convert.ToDouble(entero)) + dec;
            return res;
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private static string NumberToLetters(double value)
        {
            string num2Text; value = Math.Truncate(value);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UNO";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + NumberToLetters(value - 10);
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + NumberToLetters(value - 20);
            else if (value == 30) num2Text = "TREINTA";
            else if (value == 40) num2Text = "CUARENTA";
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            else if (value < 100) num2Text = NumberToLetters(Math.Truncate(value / 10) * 10) + " Y " + NumberToLetters(value % 10);
            else if (value == 100) num2Text = "CIEN";
            else if (value < 200) num2Text = "CIENTO " + NumberToLetters(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) num2Text = NumberToLetters(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) num2Text = "QUINIENTOS";
            else if (value == 700) num2Text = "SETECIENTOS";
            else if (value == 900) num2Text = "NOVECIENTOS";
            else if (value < 1000) num2Text = NumberToLetters(Math.Truncate(value / 100) * 100) + " " + NumberToLetters(value % 100);
            else if (value == 1000) num2Text = "MIL";
            else if (value < 2000) num2Text = "MIL " + NumberToLetters(value % 1000);
            else if (value < 1000000)
            {
                num2Text = NumberToLetters(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0)
                {
                    num2Text = num2Text + " " + NumberToLetters(value % 1000);
                }
            }
            else if (value == 1000000)
            {
                num2Text = "UN MILLON";
            }
            else if (value < 2000000)
            {
                num2Text = "UN MILLON " + NumberToLetters(value % 1000000);
            }
            else if (value < 1000000000000)
            {
                num2Text = NumberToLetters(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
                {
                    num2Text = num2Text + " " + NumberToLetters(value - Math.Truncate(value / 1000000) * 1000000);
                }
            }
            else if (value == 1000000000000) num2Text = "UN BILLON";
            else if (value < 2000000000000) num2Text = "UN BILLON " + NumberToLetters(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                num2Text = NumberToLetters(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
                {
                    num2Text = num2Text + " " + NumberToLetters(value - Math.Truncate(value / 1000000000000) * 1000000000000);
                }
            }
            return num2Text;
        }
        #endregion

        #region Rango de números
        public static long NumberNextRange(Guid id, string dbconnection)
        {
            using (SqlConnection cnn = new SqlConnection(dbconnection))
            {
                SqlCommand cmd = new SqlCommand("XSP_GEN_NEXT_NUMBER", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@id"].Value = id;

                cnn.Open();
                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        //public static long MySQLNumberNextRangeDte(Guid rangeid, string dbconnection)
        //{
        //    using (SqlConnection cnn = new SqlConnection(dbconnection))
        //    {
        //        SqlCommand cmd = new SqlCommand("XSP_GEN_NUMBER_RANGE_COMPANYDTE", cnn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@rngid", SqlDbType.UniqueIdentifier);
        //        cmd.Parameters["@rngid"].Value = rangeid;

        //        cnn.Open();
        //        return Convert.ToInt64(cmd.ExecuteScalar());
        //    }
        //}
        #endregion
    }
}
