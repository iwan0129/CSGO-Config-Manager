namespace CSGO_Config_Manager.Data
{
    internal interface IConfig
    {
        void Read(string filePath);
        void Write(string filePath);
    }
}
