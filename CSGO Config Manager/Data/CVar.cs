using System;

namespace CSGO_Config_Manager.Data
{
    internal struct CVar
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public CVar(string name, object value)
        {
            Name = name;

            Value = value?.ToString();
        }

        public CVar(string data)
        {
            string[] dataTypes = data.Split(new[] { " " }, 2, StringSplitOptions.RemoveEmptyEntries);

            Name = dataTypes[0];

            Value = dataTypes.Length > 1
                ? dataTypes[1].Replace("\"", null)
                : null;
        }

        public static bool operator ==(CVar cvar1, CVar cvar2)
        {
            return cvar1.Name == cvar2.Name
                && cvar1.Value == cvar2.Value;
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
            return $"{Name} {(!string.IsNullOrEmpty(Value) ? $"\"{Value}\"" : null)}";
        }
    }
}
