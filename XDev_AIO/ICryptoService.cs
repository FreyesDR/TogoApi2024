using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDev_AIO
{
    internal interface ICryptoService
    {
        string Decrypt(string Value);
        string DecryptFile();
        string Encrypt(string Value);
    }
}
