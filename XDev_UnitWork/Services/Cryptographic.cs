using System.Security.Cryptography;
using System.Text;

namespace XDev_UnitWork.Services
{
	public class Cryptographic
	{
		public string Encrypt(string input, string algorithm)
		{
			byte[] hashBytes;

			// Seleccionar el algoritmo de hash
			switch (algorithm.ToUpper())
			{
				case "SHA-256":
					using (SHA256 sha256 = SHA256.Create())
					{
						hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
					}
					break;

				case "SHA-512":
					using (SHA512 sha512 = SHA512.Create())
					{
						hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
					}
					break;

				default:
					throw new ArgumentException("Algoritmo no válido", nameof(algorithm));
			}

			// Convertir el hash a una cadena hexadecimal
			return BytesToHex(hashBytes);
		}

		private static string BytesToHex(byte[] hash)
		{
			StringBuilder hexString = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				string hex = hash[i].ToString("x2");
				hexString.Append(hex);
			}
			return hexString.ToString();
		}
	}
}
