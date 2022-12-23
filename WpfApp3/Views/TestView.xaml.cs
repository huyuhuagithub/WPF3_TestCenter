using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeTest.Common.FileOperate;
using WpfApp3.Common;
using WpfApp3.Models;
using WpfApp3.Models.DebugViewModel;

namespace WpfApp3.Views
{
    /// <summary>
    /// viewA.xaml 的交互逻辑
    /// </summary>
    public partial class TestView : UserControl
    {
        public TestView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginUser loginUser = new LoginUser()
            {
                UserName = "huyuhua",
                UserRole = Level.Engineer,
                LoginState = true,
                Password = "je",
            };
            SP_SendData sP_Send = new SP_SendData()
            {
                Command = "open",
                Description = "打开",
                IsCheck = true,
                IsHexSend = true,


            };
            FileUtil.OutputDir = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}AppData" + "\\" + "JsonFile");


            //JsonOperate jsonOperate123 = new JsonOperate();
            //jsonOperate123.WriteJsonFile(@"d:\movie.json", tt);



            FileUtil.OutputDir = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}AppData" + "\\ModelsFile");
            SP_SendData sP_SendData = new SP_SendData()
            {
                Command = "open",
                Description = "打开",
                IsHexSend = true,
                IsCheck = true,
            };

            var tt = new List<SP_SendData>(); tt.Add(sP_Send); tt.Add(sP_Send);
            JsonOperate jsonOperate123 = new JsonOperate();
            //jsonOperate123.WriteJsonFile(FileUtil.OutputDir.FullName+"\\Model1.json", tt);
        }

        private void CommonClickHandler(object sender, RoutedEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            switch (feSource.Name)
            {
                case "YesButton":
                    // do something here ...
                   
                    break;
                case "NoButton":
                    // do something ...
                    break;
                case "CancelButton":
                    // do something ...
                    break;
            }
            e.Handled = true;
        }
    }
}
