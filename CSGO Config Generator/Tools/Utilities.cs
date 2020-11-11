using CSGO_Config_Generator.Data;
using CSGO_Config_Generator.Models;
using System.Collections.Generic;

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

    }
}
