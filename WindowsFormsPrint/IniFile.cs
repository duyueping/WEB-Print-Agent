/**
* @brief WEB-Print-Agent
*
* @author yueping du <duyueping@vip.qq.com>
* @since 2017-08-30 16:45:00
*/

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IniFile
{

    /// <summary> 
    /// ini文件读与写 
    /// </summary> 
    public class ClsIniFile
    {
        //文件INI名称 
        public string Path;
        ////声明读写INI文件的API函数 
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //类的构造函数，传递INI文件名 
        public ClsIniFile(string inipath) { Path = inipath; }
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="Section">配置节</param>
        /// <param name="Key">键名</param>
        /// <param name="Value">键值</param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.Path);
        }

        /// <summary>
        /// 读取制定INI文件键值
        /// </summary>
        /// <param name="Section">配置节</param>
        /// <param name="Key">键名</param>
        /// <returns></returns> 
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.Path);
            return temp.ToString();
        }
    }
}