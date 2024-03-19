using System.IO.Compression;
using System.Text;

namespace Investors.Shared.Infrastructure.Utils
{
    public static class JsonUtils
    {
        public static string DecompressGZipJson(string compressedBase64)
        {
            // Convertir la cadena base64 a un arreglo de bytes
            byte[] compressedBytes = Convert.FromBase64String(compressedBase64);

            using (MemoryStream memory = new MemoryStream())
            {
                // Cargar los bytes comprimidos en la memoria
                memory.Write(compressedBytes, 0, compressedBytes.Length);
                // Establecer la posición de la memoria al inicio
                memory.Position = 0;

                using (GZipStream gZipStream = new GZipStream(memory, CompressionMode.Decompress))
                {
                    using (MemoryStream decompressedMemory = new MemoryStream())
                    {
                        gZipStream.CopyTo(decompressedMemory);
                        decompressedMemory.Position = 0;
                        using (StreamReader reader = new StreamReader(decompressedMemory, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}