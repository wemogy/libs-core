namespace Wemogy.Core.Extensions
{
    public static class LongExtensions
    {
        public static string HumanizeBytes(this long bytes)
        {
            string[] suffix = { "Bytes", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            if (i >= suffix.Length)
            {
                // fallback to TB
                i = suffix.Length - 1;
            }

            return $"{dblSByte:0.##} {suffix[i]}";
        }
    }
}
