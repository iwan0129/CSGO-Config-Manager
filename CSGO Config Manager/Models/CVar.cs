using System;

namespace CSGO_Config_Manager.Models
{
    internal class CVar
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public CVar()
        {

        }

        public CVar(string name, object value)
        {
            Name = name;

            Value = value?.ToString();
        }

        public CVar(string data)
        {
            bool isComment = data[0] == '/' && data[1] == '/';

            string[] dataTypes = data.Split(new[] { " " }, isComment ? 1 : 3, StringSplitOptions.RemoveEmptyEntries);

            int valueOffset = dataTypes.Length - 1;

            Name = dataTypes.Length == 3
                ? $"{dataTypes[0]} {dataTypes[1]}"
                : dataTypes[0];

            Value = dataTypes.Length > 1
                ? dataTypes[valueOffset].Replace("\"", null)
                : null;
        }

        ~CVar()
        {
            Name = Value = null;
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
