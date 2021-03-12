using CSGO_Config_Manager.Data;
using CSGO_Config_Manager.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace CSGO_Config_Manager.Utilities
{
    internal static class VariableUtilities
    {
        public static void AddVariables(this IList<VariablePreview> variables, IConfig config)
        {
            foreach (CVar cvar in config.CVars)
            {
                variables.Add(new(cvar));
            }
        }

        public static void AddVariables(this IList<VariablePreview> variables, params CVar[] cvars)
        {
            foreach (CVar cvar in cvars)
            {
                variables.Add(new(cvar));
            }
        }
    }
}
