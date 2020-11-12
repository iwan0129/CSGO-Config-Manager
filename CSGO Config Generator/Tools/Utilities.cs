using CSGO_Config_Generator.Data;
using CSGO_Config_Generator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSGO_Config_Generator.Tools
{
    internal static class Utilities
    {
        public static void AddCVars<CVars>(this IList<SettingPreview> settings, CVars cvars) where CVars: IEnumerable<CVar>
        {
            foreach (CVar cvar in cvars)
            {
                if (cvar.Value != null)
                {
                    settings.Add(new SettingPreview(cvar));
                }
            }
        }

        public static void AddCVars(this IList<SettingPreview> settings, params CVar[] cvars)
        {
            foreach (CVar cvar in cvars)
            {
                if (cvar.Value != null)
                {
                    settings.Add(new SettingPreview(cvar));
                }
            }
        }

        public static string ConvertToStr(this byte[] data)
        {
            char[] charData = new char[data.Length];

            int offset = 0;

            for (; offset < data.Length; offset++)
            {
                charData[offset] = (char)data[offset];
            }

            return new string(charData, 0, offset);
        }

        public static byte[] ConvertToBytes<CVars>(this CVars cvars) where CVars: IEnumerable<CVar>
        {
            StringBuilder strBuilder = new StringBuilder();

            int offset = 0;

            int length = cvars.Count() - 1;

            foreach (CVar cvar in cvars)
            {
                if (offset++ < length)
                {
                    strBuilder.Append(cvar).Append("\n");
                }
                else
                {
                    strBuilder.Append(cvar);
                }
            }

            byte[] data = new byte[strBuilder.Length];

            for (offset = 0; offset < strBuilder.Length; offset++)
            {
                data[offset] = (byte)strBuilder[offset];
            }

            return data;
        }
    }
}
