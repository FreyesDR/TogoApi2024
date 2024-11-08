using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDev_AIO
{
    public static class Utils
    {
        public static string GetConfig(string key)
        {
            ICryptoService cryptoService = new CryptoService();

            return cryptoService.DecryptFile();
        }

        public static string Encrypt(string key, string value)
        {
            ICryptoService cryptoService = new CryptoService();
            return cryptoService.Encrypt(value);
        }

        public static string Decrypt(string key, string value)
        {
            ICryptoService cryptoService = new CryptoService();
            return cryptoService.Decrypt(value);
        }
    }
}
