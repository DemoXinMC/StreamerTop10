using System.Buffers.Text;

namespace Top10Streams.Data
{
    public static class Transmit
    {
        public static string EncodeIntList(IEnumerable<int> list)
        {
            string base64 = string.Empty;

            foreach (var item in list)
            {
                base64 += item.ToString() + "|";
            }

            base64 = base64.TrimEnd('|');

            return EncodeString(base64);
        }

        public static List<int> DecodeIntList(string encodedBytes)
        {
            var text = DecodeString(encodedBytes);
            var nums = text.Split("|");

            List<int> output = [];

            foreach(var num in nums)
            {
                int parsed;
                if(int.TryParse(num, out parsed))
                    output.Add(parsed);
            }

            return output;
        }

        public static string EncodeString(string text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string DecodeString(string encodedBytes)
        {
            var decodedBytes = Convert.FromBase64String(encodedBytes);
            return System.Text.Encoding.UTF8.GetString(decodedBytes);
        }
    }
}
