using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DynamicCall.WhereDll;
using Prism.Commands;
using Prism.Mvvm;
using Unity.Injection;


namespace WpfApp3.ViewModels
{
    public class DataViewAViewModel : BindableBase
    {
        #region Property

        #endregion

        #region Commands
        private DelegateCommand<DragEventArgs> _dropCommand;
        public DelegateCommand<DragEventArgs> DropCommand =>
            _dropCommand ?? (_dropCommand = new DelegateCommand<DragEventArgs>(ExecuteCommandName));

        void ExecuteCommandName(DragEventArgs parameter)
        {
            var CurrentDomainPath = AppDomain.CurrentDomain.BaseDirectory + "DllPath";
            var FileDropList = (string[])parameter.Data.GetData(DataFormats.FileDrop);
            
             
            foreach (var FilePath in FileDropList)
            {
                //var tempstr1 = FilePath.Select((s, index) => new { Lstr = s == '\\', index1 = index }).Where(a => a.Lstr == true).Last();
                if (FilePath.Contains(".dll"))
                {
                    var tempstr2 = FilePath.Select((s, index) => (Lstr: s == '\\', index1: index)).Where(a => a.Lstr == true).Last();
                    string fname = FilePath.Substring(tempstr2.index1 + 1);
                    string path = CurrentDomainPath + "\\" + fname;
                    try
                    {
                        File.Copy(FilePath, path, true);
                        WhereDllClass whereDll = new WhereDllClass();
                        var t = whereDll.ReflectionTest(path);
                       
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                   
                }

            }





            //File.Copy(t[0], tt + "\\"+fname);

        }

        #endregion
    }
}
