using CSGO_Config_Manager.Data;
using CSGO_Config_Manager.Models;
using System.Collections.Generic;

namespace CSGO_Config_Manager.Tools
{
    internal static class Utilities
    {
        public static void AddVariables<CVars>(this IList<VariablePreview> settings, CVars cvars) where CVars: notnull, IEnumerable<CVar>
        {
            foreach (CVar cvar in cvars)
            {
                settings.Add(new VariablePreview(cvar));
            }
        }

        public static void AddVariables(this IList<VariablePreview> settings, params CVar[] cvars)
        {
            foreach (CVar cvar in cvars)
            {
                settings.Add(new VariablePreview(cvar));
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
    }
}
