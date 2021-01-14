using CSGO_Config_Manager.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        public void SyncWith<CVars>(CVars cvars) where CVars : notnull, IEnumerable<CVar>
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
            Load(FilePath);
        }

        public void Load(string filePath)
        {
            if (filePath != FilePath)
            {
                FilePath = filePath;
            }

            using FileStream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            StringBuilder strBuilder = new StringBuilder();

            byte[] buffer = new byte[14456];

            int offset = 0;

            int remaining = (int)fstream.Length;

            int readBytes;

            while (remaining > 0)
            {
                if ((readBytes = fstream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    strBuilder.Append(buffer.ConvertToChars(0, readBytes));

                    offset += readBytes;

                    remaining -= readBytes;
                }
            }

            foreach (string cvarData in strBuilder.ToString().Split(new[] { "\n", "\r" }, 
                StringSplitOptions.RemoveEmptyEntries))
            {
                CVars.Add(new CVar(cvarData));
            }
        }

        public void Save()
        {
            Save(FilePath);
        }

        public void Save(string filePath)
        {
            using FileStream fstream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            if (fstream.Length > 0)
            {
                fstream.SetLength(0);
            }

            byte[] data = ToArray();

            int offset = 0;

            int remaining = data.Length;

            while (remaining > 0)
            {
                int currentChunk = remaining > 14456 ? 14456 : remaining;

                fstream.Write(data, offset, currentChunk);

                remaining -= currentChunk;

                offset += currentChunk;
            }
        }

        public byte[] ToArray()
        {
            StringBuilder strBuilder = new StringBuilder();

            int offset = 0;

            int length = CVars.Count - 1;

            foreach (CVar cvar in CVars)
            {
                if (offset++ < length)
                {
                    strBuilder.Append(cvar).Append("\n");
                }
                else
                {
                    strBuilder.Append(cvar);
                }
            }

            byte[] data = new byte[strBuilder.Length];

            for (offset = 0; offset < strBuilder.Length; offset++)
            {
                data[offset] = (byte)strBuilder[offset];
            }

            return data;
        }

        public void GenerateDefault()
        {
            if (CVars.Count > 0)
            {
                CVars.Clear();
            }

            CVars = new List<CVar>()
            {
                new CVar("viewmodel_fov", 68),
                new CVar("viewmodel_offset_x", 2.5),
                new CVar("viewmodel_offset_y", 0),
                new CVar("viewmodel_offset_z", -1.5),
                new CVar("viewmodel_recoil", 0),
                new CVar("viewmodel_presetpos", 3),
                new CVar("cl_viewmodel_shift_left_amt", 1.5),
                new CVar("cl_viewmodel_shift_right_amt", 0.75),
                new CVar("cl_bob_lower_amt", 21),
                new CVar("cl_bobamt_lat", 0.33),
                new CVar("cl_bobamt_vert", 0.14),
                new CVar("cl_bobcycle", 0.98),
                new CVar("cl_autowepswitch", 0),
                new CVar("cl_autohelp", 0),
                new CVar("cl_showhelp", 0),
                new CVar("cl_cmdrate", 128),
                new CVar("cl_updaterate", 128),
                new CVar("cl_interp_ratio", 1),
                new CVar("cl_interp", 0),
                new CVar("cl_lagcompensation", 1),
                new CVar("cl_predict", 1),
                new CVar("cl_predictweapons", 1),
                new CVar("cl_forcepreload", 1),
                new CVar("tickrate", 128),
                new CVar("m_rawinput", 1),
                new CVar("m_mouseaccel1", 0),
                new CVar("m_mouseaccel2", 0),
                new CVar("fps_max", 0),
                new CVar("r_dynamic", 1),
                new CVar("r_drawtracers_firstperson", 0),
                new CVar("r_eyegloss", 0),
                new CVar("r_eyemove", 0),
                new CVar("r_eyeshift_x", 0),
                new CVar("r_eyeshift_y", 0),
                new CVar("r_eyeshift_z", 0),
                new CVar("r_eyesize", 0),
                new CVar("snd_mix_async", 1),
                new CVar("snd_mixahead", 1),
                new CVar("snd_stream", 1),
                new CVar("joystick", 1),
                new CVar("joystick_force_disabled", 1),
                new CVar("joystick_force_disabled_set_from_options", 1),
                new CVar("host_writeconfig", null),
            };
        }
    }
}