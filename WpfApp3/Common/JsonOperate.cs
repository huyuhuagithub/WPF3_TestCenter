using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeTest.Common.FileOperate;
using WeTest.Common.Helpers.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using WpfApp3.Models.DebugViewModel;

namespace WpfApp3.Common
{
    public class JsonOperate
    {
        /// <summary>
        /// path 为包含完整的file名。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="listData"></param>
        /// <exception cref="Exception"></exception>
        public void WriteJsonFile<T>(string path, ObservableCollection<T> listData)
        {
            try
            {

                //if (listData!=null)
                //{
                //File.WriteAllText(FileName, NewtonJsonHelper.ObjToJsonString(listData));
                //将Json文件以字符串的形式保存
                File.WriteAllText(path, JsonConvert.SerializeObject(listData, Formatting.Indented));
                //string output = JsonConvert.SerializeObject(listData);
                //StreamWriter sw = new StreamWriter(path);
                //sw.Write(output);
                //sw.Close();
                //}
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public (List<T> values, string FileName) GetConfig<T>(DirectoryInfo di, string fileName)
        {
            //UsersConfig
            var FileInfos = di.GetFiles().Where(Ex => Ex.Extension == ".json" && Ex.Name.Contains(fileName)).ToList();//获取指定路径.json

            var fileinfo = FileUtil.GetFileInfo(FileInfos.FirstOrDefault().Name);

            var jsonstr = File.ReadAllText(fileinfo.FullName);
            //var sendDatalist = NewtonJsonHelper.JsonToObjList<T>(jsonstr);

            return ((List<T>)NewtonJsonHelper.JsonToObjList<T>(jsonstr), fileinfo.Name);
        }
        public InterfaceProperties GetConfigList()
        {
            //UsersConfig
            InterfaceProperties @interface = new InterfaceProperties();
            FileUtil.OutputDir = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}AppData" + "\\ModelsFile");//指定路径           
            var FileInfos = FileUtil.OutputDir.GetFiles().Where(Ex => Ex.Extension == ".json").ToList();//获取指定路径所有.json文件

            FileInfos.ForEach(fi =>
            {
                @interface.Models.Add(fi.Name);
                var jsonstr = File.ReadAllText(fi.FullName);
                var sendDatalist = NewtonJsonHelper.JsonToObjList<SP_SendData>(jsonstr);
                @interface.SP_SendDatas.AddRange(sendDatalist);                
            });


            return @interface;
        }

    }
}
