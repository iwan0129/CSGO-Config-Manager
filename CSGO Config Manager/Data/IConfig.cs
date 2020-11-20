using System.Collections.Generic;

namespace CSGO_Config_Manager.Data
{
    internal interface IConfig
    {
        void Load();
        void Save();
        void Load(string filePath);
        void Save(string filePath);
        void Add(CVar cvar);
        bool Remove(CVar cvar);
        void RemoveAt(int index);
        void SyncWith<CVars>(CVars cvars) where CVars : IEnumerable<CVar>;
    }
}
