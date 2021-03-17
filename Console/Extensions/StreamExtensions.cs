using System.IO;


namespace BinaryFormatterVunerabilities.Extensions
{
    public static class StreamExtensions
    {
        private static string ToString(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

    }
}
