using System.IO;
using System.Text;

namespace BinaryFormatterVunerabilities.Extensions
{
    public static class StringExtensions
    {
        public static MemoryStream ToMemoryStream(this string str)
        {
            // Using is safer in most instances.
            byte[] byteArray = Encoding.ASCII.GetBytes(str);
            return new MemoryStream(byteArray);
        }
    }
}
