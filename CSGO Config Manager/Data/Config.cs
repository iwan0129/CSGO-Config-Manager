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

        public string FilePath { get; private set; }

        public Config(string filePath = null, IEnumerable<CVar> cvars = null)
        {
            FilePath = filePath;

            CVars = cvars?.ToList() ?? new List<CVar>();
        }

        public void Add(CVar cvar)
        {
            CVars.Add(cvar);
        }

        public bool Remove(CVar cvar)
        {
            return CVars.Remove(cvar);
        }

        public void RemoveAt(int index)
        {
            CVars.RemoveAt(index);
        }

        public void Clear()
        {
            if (CVars.Count > 0)
            {
                CVars.Clear();
            }
        }

        public void SyncWith<CVars>(CVars cvars) where CVars: notnull, IEnumerable<CVar>
        {
            this.CVars = cvars.ToList();
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

        public void Load()
        {
            using FileStream fstream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);

            byte[] data = new byte[fstream.Length];

            int offset = 0;

            int remaining = data.Length;

            int Read(int bytes)
            {
                int readBytes = fstream.Read(data, offset, bytes);

                remaining -= readBytes;

                offset += readBytes;

                return readBytes;
            }

            while (remaining > 0)
            {
                if (Read(remaining > 65536 ? 65536 : remaining) == 0)
                {
                    throw new Exception("Unable to read data");
                }
            }

            foreach (string cvarData in data.ConvertToStr().Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
            {
                CVars.Add(new CVar(cvarData));
            }
        }
     
        public void Save()
        {
            using FileStream fstream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            if (fstream.Length > 0)
            {
                fstream.SetLength(0);
            }

            byte[] data = CVars.ConvertToBytes();

            int offset = 0;

            int remaining = data.Length;

            void Write(int bytes)
            {
                fstream.Write(data, offset, bytes);

                remaining -= bytes;

                offset += bytes;
            }

            while (remaining > 0)
            {
                Write(remaining > 65536 ? 65536 : remaining);
            }
        }

        public void Load(string filePath)
        {
            FilePath = filePath;

            using FileStream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            byte[] data = new byte[fstream.Length];

            int offset = 0;

            int remaining = data.Length;

            int Read(int bytes)
            {
                int readBytes = fstream.Read(data, offset, bytes);

                remaining -= readBytes;

                offset += readBytes;

                return readBytes;
            }

            while (remaining > 0)
            {
                if (Read(remaining > 65536 ? 65536 : remaining) == 0)
                {
                    throw new Exception("Unable to read data");
                }
            }

            foreach (string cvarData in data.ConvertToStr().Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
            {
                CVars.Add(new CVar(cvarData));
            }
        }

        public void Save(string filePath)
        {
            using FileStream fstream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            if (fstream.Length > 0)
            {
                fstream.SetLength(0);
            }

            byte[] data = CVars.ConvertToBytes();

            int offset = 0;

            int remaining = data.Length;

            void Write(int bytes)
            {
                fstream.Write(data, offset, bytes);

                remaining -= bytes;

                offset += bytes;
            }

            while (remaining > 0)
            {
                Write(remaining > 65536 ? 65536 : remaining);
            }
        }
    }
}