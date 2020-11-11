using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_Config_Generator.Data
{
    internal class Config : IConfig, IDisposable
    {
        private bool Disposed;

        public List<CVar> CVars { get; private set; }

        public string FilePath { get; set; }

        public Config(string filePath = null)
        {
            CVars = new List<CVar>();

            FilePath = filePath;
        }

        public Config(string filePath, IEnumerable<CVar> cvars)
        {
            CVars = cvars.ToList();
        }

        public Config(IEnumerable<CVar> cvars)
        {
            CVars = cvars.ToList();
        }

        public void Add(CVar cvar)
        {
            CVars.Add(cvar);
        }

        public void Clear()
        {
            if (CVars.Count > 0)
            {
                CVars.Clear();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                Disposed = true;

                if (disposing)
                {
                    Clear();
                }

                CVars = null;
            }
        }

        ~Config()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public void Read(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Write(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
