namespace CSGO_Config_Manager.Utilities
{
    internal static class Utilities
    {
        public static string ConvertToChars(this byte[] data, int offset, int length)
        {
            char[] charData = new char[length - offset];

            for (; offset < charData.Length; offset++)
            {
                charData[offset] = (char)data[offset];
            }

            return new string(charData, 0, offset);
        }
    }
}
