using CSGO_Config_Manager.Models;
using System.Collections.Generic;

namespace CSGO_Config_Manager.Interfaces
{
    internal interface IConfig
    {
        List<CVar> CVars { get; }

        string FilePath { get; }

        void Load();

        void Save();

        void Load(string filePath);

        void Save(string filePath);

        void Add(CVar cvar);

        void RemoveAt(int index);

        void SyncWith<CVars>(CVars cvars) where CVars : IEnumerable<CVar>;

        void GenerateDefault();

        bool Remove(CVar cvar);
    }
}
