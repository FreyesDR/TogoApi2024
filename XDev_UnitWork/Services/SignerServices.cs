using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Sockets;
using XDev_UnitWork.DTO.FeSv;
using XDev_UnitWork.Custom;
using System.Xml.Serialization;

namespace XDev_UnitWork.Services
{
    public static class SignerServices
    {        
		public static async Task<string> SignDocument(IHttpClientFactory httpClientFactory, string url, SignerJsonDTO jsondto)
        {
            string dteSigned = string.Empty;
            using var client = httpClientFactory.CreateClient();

            try
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("/firmardocumento/", jsondto);
                var strReturn = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var jObject = JsonConvert.DeserializeObject<JObject>(strReturn);
                    if (jObject["status"].ToString() == "OK")
                    {
                        dteSigned = jObject["body"].ToString();
                    }
                    else
                    {
                        ResponseFirmadorDTO dtores = JsonConvert.DeserializeObject<ResponseFirmadorDTO>(strReturn);

                        var retmsg = string.Empty;
                        if (dtores.body.mensaje is JArray)
                        {
                            retmsg = string.Join('|', dtores.body.mensaje);
                        }
                        else
                            retmsg = dtores.body.mensaje;

                        throw new Exception($"Firmador: {retmsg}");
                    }
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        throw new Exception("Valide las credenciales de acceso al firmador");

                    ResponseFirmadorDTO dtores = JsonConvert.DeserializeObject<ResponseFirmadorDTO>(strReturn);

                    var retmsg = string.Empty;
                    if (dtores.body != null)
                    {
                        if (dtores.body.mensaje is JArray)
                        {
                            retmsg = string.Join('|', dtores.body.mensaje);
                        }
                        else
                            retmsg = dtores.body.mensaje;

                        throw new Exception($"Firmador: {retmsg}");
                    }
                    else
                        throw new Exception($"Firmador: {dtores.status}");
                }
            }
            catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.HostNotFound })
            {
                throw new CustomTogoException("Firmador: Host no encontrado");
            }
            catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.ConnectionRefused })
            {
                throw new CustomTogoException("Firmador: No se puede conectar con el Host");
            }
            catch (HttpRequestException e) when (e.StatusCode.HasValue && (int)e.StatusCode.Value > 500)
            {
                throw new CustomTogoException("Firmador: Error desconocido");
            }

            return dteSigned;
        }
    }

}
