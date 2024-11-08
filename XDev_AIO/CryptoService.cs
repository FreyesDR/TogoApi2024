using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XDev_AIO
{
    internal class CryptoService : ICryptoService
    {
        string rsak = "<RSAKeyValue><Modulus>wTnUtX66hkCng+dMuVy7upDsJEhECDP19Blwl9Wmv4PBwCgGypiIQqtWGROQ2wXMOvsr1Z5dPTyeqBARxUXbMOPA9dYcnN5csR4c4undqB67uQom3iy15geHZEARN14GkdPOcWMy1UD4Yo9toq8nPa38znBlPbLmNJCwgQbVWmk=</Modulus><Exponent>AQAB</Exponent><P>8yKT5wHMERer195prMcmVNc8O10Y44OFkITAE9kEcUpEnT3kdtofNw7oVl+omNxcwU/O0BGQvLWf0/cxyKqtDw==</P><Q>y3Mzeyyy3EkamtpA/w9OwZGKIsgvD2NNf9L3z52WztGYNolgt2G1TKFnfEUhYFdWSZP+3vJ/iC/IBG0BfclxBw==</Q><DP>kIXBPNOIpbBYaVy/nABU/KFkIDVakgKf5iPCuQmK5nyrBHzBzRQi8etel9kMsrBqQk5aNLv8OPANLUYGxa0OaQ==</DP><DQ>IhUrg7AKSrMIfPahOtlypSTfklIc6CQHoKlUEHjjAcTFDiXXiQEaYTsM3hmqrLwU3YCoiurvCH6QsIUeYdc0Uw==</DQ><InverseQ>zUid10wwImQR6sevAA+n77JhLK3lIGwDiCTKd7HO4d1nqbxqKThBr49K5I21cLB+MbjtytqT2/rOjsGsBDqv/A==</InverseQ><D>twTK1/ooPF9jC8n63xvZ6LW6JgeZANgOn920yuwgXAg1bYe8HwtpDRTyoR/qNbqBHmCnvVeZ4xXXBULrQhqPqFEhXIeMRCnswvMCI0+FTC4XOyn2+s7Yt5MQQccY3sRrenODV7j3OGkQqkbMSo/VFTy0wwVnwTsPYNkz1PViZF0=</D></RSAKeyValue>";
        RSACryptoServiceProvider _rsa;

        public CryptoService()
        {
            _rsa = new RSACryptoServiceProvider();
            _rsa.FromXmlString(rsak);
        }

        public string Encrypt(String Value)
        {
            Aes aes = Aes.Create();


            try
            {

                byte[] _btKey = Encrypt(aes.Key);
                byte[] _btIV = Encrypt(aes.IV);
                byte[] _EncryptKeys = new byte[_btKey.Length + _btIV.Length];

                _btKey.CopyTo(_EncryptKeys, 0);
                _btIV.CopyTo(_EncryptKeys, _btKey.Length);

                byte[] _toEncrypt = Encoding.UTF8.GetBytes(Value);
                byte[] _Encriptado = (aes.CreateEncryptor()).TransformFinalBlock(_toEncrypt, 0, _toEncrypt.Length);

                byte[] _return = new byte[_EncryptKeys.Length + _Encriptado.Length];

                _EncryptKeys.CopyTo(_return, 0);
                _Encriptado.CopyTo(_return, _EncryptKeys.Length);

                return Convert.ToBase64String(_return);


            }
            catch (Exception ex) { return ""; }
        }

        public string Decrypt(String Value)
        {
            Aes aes = Aes.Create();

            try
            {
                byte[] _toDecrypt = Convert.FromBase64String(Value);
                byte[] _EncryptKeys = new byte[256];
                byte[] _dataEncrypted = new byte[_toDecrypt.Length - 256];

                Array.Copy(_toDecrypt, _EncryptKeys, 256);
                Array.Copy(_toDecrypt, 256, _dataEncrypted, 0, _dataEncrypted.Length);

                byte[] _keyEncrypt = new byte[128];
                Array.Copy(_EncryptKeys, _keyEncrypt, 128);

                byte[] _ivEncrypt = new byte[128];
                Array.Copy(_EncryptKeys, 128, _ivEncrypt, 0, 128);

                byte[] _Key = Decrypt(_keyEncrypt);
                byte[] _IV = Decrypt(_ivEncrypt);

                aes.Key = _Key;
                aes.IV = _IV;


                return Encoding.UTF8.GetString((aes.CreateDecryptor()).TransformFinalBlock(_dataEncrypted, 0, _dataEncrypted.Length));

            }
            catch (Exception ex) { return ""; }
        }

        private byte[] Encrypt(byte[] Valor)
        {
            RSACryptoServiceProvider myrsa = new RSACryptoServiceProvider();
            myrsa.FromXmlString(rsak);
            try
            {
                return myrsa.Encrypt(Valor, false);
            }
            catch (Exception ex) { return null; }
        }

        private byte[] Decrypt(byte[] Valor)
        {
            RSACryptoServiceProvider myrsa = new RSACryptoServiceProvider();
            myrsa.FromXmlString(rsak);
            try
            {
                return myrsa.Decrypt(Valor, false);
            }
            catch (Exception ex) { return null; }
        }

        public string DecryptFile()
        {
            string result = "";
            // Create instance of Aes for
            // symmetric decryption of the data.
            Aes aes = Aes.Create();

            // Create byte arrays to get the length of
            // the encrypted key and IV.
            // These values were stored as 4 bytes each
            // at the beginning of the encrypted package.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            

            string codeBase = Assembly.GetAssembly(typeof(CryptoService)).Location;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);


            using (var inFs = new FileStream(Path.GetDirectoryName(path) + "\\WebConfig.enc", FileMode.Open, FileAccess.Read))
            {
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Read(LenK, 0, 3);
                inFs.Seek(4, SeekOrigin.Begin);
                inFs.Read(LenIV, 0, 3);

                // Convert the lengths to integer values.
                int lenK = BitConverter.ToInt32(LenK, 0);
                int lenIV = BitConverter.ToInt32(LenIV, 0);

                // Determine the start position of
                // the cipher text (startC)
                // and its length(lenC).
                int startC = lenK + lenIV + 8;
                int lenC = (int)inFs.Length - startC;

                // Create the byte arrays for
                // the encrypted Aes key,
                // the IV, and the cipher text.
                byte[] KeyEncrypted = new byte[lenK];
                byte[] IV = new byte[lenIV];

                // Extract the key and IV
                // starting from index 8
                // after the length values.
                inFs.Seek(8, SeekOrigin.Begin);
                inFs.Read(KeyEncrypted, 0, lenK);
                inFs.Seek(8 + lenK, SeekOrigin.Begin);
                inFs.Read(IV, 0, lenIV);

                //Directory.CreateDirectory(DecrFolder);
                // Use RSACryptoServiceProvider
                // to decrypt the AES key.
                byte[] KeyDecrypted = _rsa.Decrypt(KeyEncrypted, false);

                // Decrypt the key.
                ICryptoTransform transform = aes.CreateDecryptor(KeyDecrypted, IV);

                
                using (var outStreamDecrypted =
                        new CryptoStream(inFs, transform, CryptoStreamMode.Read))
                {
                    using (StreamReader decryptReader = new(outStreamDecrypted))
                    {
                        result = decryptReader.ReadToEnd();

                    }
                }

                inFs.Close();
                
            }

            return result;
        }
    }
}
