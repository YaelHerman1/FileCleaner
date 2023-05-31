using System.Text;

namespace FileCleaner.Utils
{
    public static class GeneralUtils
    {
        public static string DecodeBytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
