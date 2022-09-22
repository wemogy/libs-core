namespace Wemogy.Core.Extensions
{
    public static class IntExtensions
    {
        public static string ToBinaryString(this int number, int? padMultiple = null)
        {
            string binaryString = string.Empty;

            if (number == 0)
            {
                binaryString = "0";
            }
            else
            {
                while (number > 0)
                {
                    var remainder = number % 2;
                    number /= 2;
                    binaryString = remainder + binaryString;
                }
            }

            if (!padMultiple.HasValue)
            {
                return binaryString;
            }

            while (binaryString.Length % padMultiple.Value != 0)
            {
                binaryString = "0" + binaryString;
            }

            return binaryString;
        }
    }
}
