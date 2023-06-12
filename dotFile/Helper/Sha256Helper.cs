using System.Security.Cryptography;
using System.Text;

namespace dotFile.Helper
{
    public static class Sha256Helper
    {
        public static string GetHashString(byte[] bytesFile)
        {
            StringBuilder stringBuilder = new();
            SHA256 mySHA256 = SHA256.Create();
            
            mySHA256.ComputeHash(bytesFile).ToList().ForEach(a => stringBuilder.AppendFormat("{0:x2}", a));            

            return stringBuilder.ToString();
        }
    }
}
