using CSGO_Config_Manager.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSGO_Config_Manager.Data
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
            using FileStream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            byte[] data = new byte[fstream.Length];

            int offset = 0;

            int remaining = data.Length;

            int readBytes;

            int Read(int offset, int length)
            {
                return fstream.Read(data, offset, length);
            }

            while (remaining > 0)
            {
                if ((readBytes = Read(offset, remaining > 65536 ? 65536 : remaining)) > 0)
                {
                    remaining -= readBytes;

                    offset += readBytes;
                }
                else
                {
                    throw new Exception("Unable to read data");
                }
            }

            foreach (string cvarData in data.ConvertToStr().Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
            {
                CVars.Add(new CVar(cvarData));
            }
        }

        public void Write(string filePath)
        {
            using FileStream fstream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            byte[] data = CVars.ConvertToBytes();

            int offset = 0;

            int remaining = data.Length;

            void Write(int length)
            {
                fstream.Write(data, offset, length);

                remaining -= length;

                offset += length;
            }

            while (remaining > 0)
            {
                Write(remaining > 65536 ? 65536 : remaining);
            }
        }
    }
}
