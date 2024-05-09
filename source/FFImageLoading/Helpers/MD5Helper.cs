using System.Text;
using System.Security.Cryptography;
using FFImageLoading.Extensions;

namespace FFImageLoading.Helpers
{
    public class MD5Helper : IMD5Helper
    {
        public string MD5(Stream stream)
        {
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                var bytes = hashProvider.ComputeHash(stream);
                return BitConverter.ToString(bytes)?.ToSanitizedKey();
            }
        }

        public string MD5(string input)
        {
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                var bytes = hashProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes)?.ToSanitizedKey();
            }
        }
    }
}
