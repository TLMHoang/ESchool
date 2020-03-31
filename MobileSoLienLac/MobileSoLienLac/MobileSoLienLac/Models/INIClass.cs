using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MobileSoLienLac.Models
{
    public class INIClass
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,
            string val,
            string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath);

        public void Write(string section, string key, string value, string file)
        {
            WritePrivateProfileString(section, key, value.ToLower(), file);
        }

        public string Read(string section, string key, string file)
        {
            StringBuilder SB = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", SB, 255, file);
            return SB.ToString();
        }
    }
}
