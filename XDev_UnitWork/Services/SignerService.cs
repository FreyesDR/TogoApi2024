using Jose;
using System.Security.Cryptography;
using System.Xml.Serialization;
using XDev_UnitWork.Custom;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Services
{
	public class SignerService : ISignerService
	{
		public async Task<string> SignDocument(CertificadoMH cert, string key, string document)
		{
			return await Task.Run(() =>
			{
				
				if (cert is not null)
				{
					if (CheckPassword(cert, key))
					{
						return Sign(cert, document);
					}
					else throw new CustomTogoException("Firmador: Clave privada incorrecta");
				}
				else throw new CustomTogoException("Firmador: No se pudo cargar el certificado");
			});
		}

		private string Sign(CertificadoMH certificado, string texto)
		{
			try
			{
				// Convertir la clave privada a un objeto RSA
				byte[] privateKeyBytes = Convert.FromBase64String(certificado.PrivateKey.Encodied);
				using (RSA rsa = RSA.Create())
				{
					rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

					// Firmar el texto usando JWS
					string firma = JWT.Encode(texto, rsa, JwsAlgorithm.RS512);
					return firma;
				}
			}
			catch (Exception ex)
			{
				throw new CustomTogoException("Firmador: " + ex.Message);
			}
		}

		private bool CheckPassword(CertificadoMH certificado, string password)
		{
			Cryptographic cryptographic = new Cryptographic();

			string hashPassword = cryptographic.Encrypt(password, "SHA-512");
			return certificado.PrivateKey.Clave == hashPassword;
		}

		//private CertificadoMH CargarCertificado(string rutaCertificado)
		//{
		//	if (!File.Exists(rutaCertificado))
		//		throw new CustomTogoException("Firmador: Certificado no existe");

		//	try
		//	{
		//		// Leer el contenido del archivo .crt
		//		string contenido = File.ReadAllText(rutaCertificado);

		//		// Deserializar el contenido XML
		//		XmlSerializer serializer = new XmlSerializer(typeof(CertificadoMH));
		//		using (StringReader reader = new StringReader(contenido))
		//		{
		//			CertificadoMH certificado = (CertificadoMH)serializer.Deserialize(reader);
		//			return certificado;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new CustomTogoException("Firmador: " + ex.Message);
		//	}
		//}
	}

	[XmlRoot("CertificadoMH")]
	public class CertificadoMH
	{
		[XmlElement("_id")]
		public string Id { get; set; }

		[XmlElement("nit")]
		public string Nit { get; set; }

		[XmlElement("publicKey")]
		public ClavePublica PublicKey { get; set; }

		[XmlElement("privateKey")]
		public ClavePrivada PrivateKey { get; set; }

		[XmlElement("activo")]
		public bool Activo { get; set; }
	}

	public class ClavePublica
	{
		[XmlElement("keyType")]
		public string KeyType { get; set; }

		[XmlElement("algorithm")]
		public string Algorithm { get; set; }

		[XmlElement("encodied")]
		public string Encodied { get; set; }

		[XmlElement("format")]
		public string Format { get; set; }

		[XmlElement("clave")]
		public string Clave { get; set; }
	}

	public class ClavePrivada
	{
		[XmlElement("keyType")]
		public string KeyType { get; set; }

		[XmlElement("algorithm")]
		public string Algorithm { get; set; }

		[XmlElement("encodied")]
		public string Encodied { get; set; }

		[XmlElement("format")]
		public string Format { get; set; }

		[XmlElement("clave")]
		public string Clave { get; set; }
	}
}
