namespace CSGO_Config_Generator.Data
{
    internal interface IConfig
    {
        void Read(string filePath);
        void Write(string filePath);
    }
}
