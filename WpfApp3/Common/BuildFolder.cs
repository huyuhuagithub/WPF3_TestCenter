using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeTest.Common.FileOperate;

namespace WpfApp3.Common
{
    public class BuildFolder
    {
        public BuildFolder()
        {
            FileUtil.OutputDir = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}AppData");//在当前目录下生成“AppData"文件夹；
            FileUtil.OutputDir = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}AppData"+"\\"+"JsonFile");//在当前目录下生成“AppData\JsonFile"文件夹；
        }
    }
}
