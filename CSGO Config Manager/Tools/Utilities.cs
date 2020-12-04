using CSGO_Config_Manager.Data;
using CSGO_Config_Manager.Models;
using System.Collections.Generic;

namespace CSGO_Config_Manager.Tools
{
    internal static class Utilities
    {
        public static void AddVariables(this IList<VariablePreview> variables, IConfig config)
        {
            foreach (CVar cvar in config.CVars)
            {
                variables.Add(new VariablePreview(cvar));
            }
        }

        public static void AddVariables(this IList<VariablePreview> variables, params CVar[] cvars)
        {
            foreach (CVar cvar in cvars)
            {
                variables.Add(new VariablePreview(cvar));
            }
        }

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
