using System;

namespace CSGO_Config_Manager.Data
{
    internal struct CVar
    {
        public string Name;
        public string Value;

        public CVar(string name, object value)
        {
            Name = name;

            Value = value?.ToString();
        }

        public CVar(string data)
        {
            string[] dataTypes = data.Split(new[] { " " }, 2, StringSplitOptions.None);

            Name = dataTypes[0];

            if (dataTypes.Length == 2)
            {
                Value = dataTypes[1].Replace("\"", null).Replace("\t", null);
            }
            else
            {
                Value = null;
            }
        }

        public static bool operator==(CVar cvar1, CVar cvar2)
        {
            return cvar1.Name == cvar2.Name && cvar1.Value == cvar2.Value;
        }

        public static bool operator !=(CVar cvar1, CVar cvar2)
        {
            return cvar1 != cvar2;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            if (Value != null)
            {
                return $"{Name} \"{Value}\"";
            }

            return $"{Name}\n";
        }
    }
}
