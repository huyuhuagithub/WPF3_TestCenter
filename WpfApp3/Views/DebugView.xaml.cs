using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Models.DebugViewModel;
using WpfApp3.ViewModels;

namespace WpfApp3.Views
{

  
    
    /// <summary>
    /// DebugView.xaml 的交互逻辑
    /// </summary>
    public partial class DebugView : UserControl
    {
        //private DebugViewViewModel debugViewViewModel;
       

        public DebugView()
        {
            InitializeComponent();
            //IRegionManager regionManager = new RegionManager();
            //debugViewViewModel = new DebugViewViewModel(regionManager);
            //this.DataContext = debugViewViewModel;
        }
        //private void ComPort_DropDownOpened(object sender, EventArgs e)
        //{

        //    //https://blog.csdn.net/Mad_Geek/article/details/105732811
        //    //        1. 开始-运行-输入：wbemtest 回车
        //    //2. 单击"连接", 输入：root\cimv2 回车; 或者ROOT\SecurityCenter 
        //    debugViewViewModel.SP_PortNames.Clear();
        //    string queryStr = "SELECT * FROM Win32_PnPEntity WHERE PNPClass = " + @"""Ports""";
        //    ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", queryStr);
        //    List<string> DevicesID = new List<string>();
        //    foreach (ManagementObject service in managementObjectSearcher.Get())
        //    {
        //        //HardwareID 
        //        //string[] caption = (string[])service.GetPropertyValue("Name");//硬件ID是唯一，不可以变的值，他是字符串数组类型。
        //        var DeviceID = service.GetPropertyValue("Caption").ToString();
        //        //如何获取字符串小括号中的值（），思路：先获取左  "（"  字符串的位置,      然后获取  ")"在字符串的位置，再用Substring()截取字符
        //        var tempstr = DeviceID.Select((s, index) => (Lstr: s == '(', index1: index, Rstr: s == ')')).Where(l => l.Lstr == true || l.Rstr == true);

        //        //var tempstr1 = DeviceID.Select((s, index) => new { Lstr = s == '(', index1 = index, Rstr = s == ')' }).Where(l => l.Lstr == true || l.Rstr == true);

        //        DevicesID.Add(DeviceID.Substring(tempstr.ToArray()[0].index1 + 1, (tempstr.ToArray()[1].index1 - tempstr.ToArray()[0].index1) - 1));
        //    }
        //    DevicesID.OrderBy(s => s).ToList().ForEach(d => debugViewViewModel.SP_PortNames.Add(d));
        //    debugViewViewModel.CurrentSPPort = debugViewViewModel.SP_PortNames[0];
        //}

        //private void ComPort_DropDownClosed(object sender, EventArgs e)
        //{
        //    debugViewViewModel.CloseSpCommand.Execute();
        //    debugViewViewModel.OpenSpCommand.Execute();
        //}

        //private void DataDisplyGrid_MouseMove(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        DataGrid dataGrid = sender as DataGrid;
        //        prevRowIndex = prevRowIndex = GetDataGridItemCurrentRowIndex(e.GetPosition);
        //        if (e.LeftButton == MouseButtonState.Pressed && dataGrid.SelectedItem != null)
        //        {
        //            DragDrop.DoDragDrop(dataGrid,
        //                                 dataGrid.SelectedItem,
        //                                 DragDropEffects.Move);
        //        }
        //    }
        //    catch (Exception E)
        //    {
        //        MessageBox.Show(E.Message);
        //    }
          
        //}

        //private void DataDisplyGrid_Drop(object sender, DragEventArgs e)
        //{
        //    DataGrid dataGrid = sender as DataGrid;
        //    if (prevRowIndex < 0)//拖到的起始位置小于0
        //        return;
        //    int index = this.GetDataGridItemCurrentRowIndex(e.GetPosition);

        //    //The current Rowindex is -1 (No selected)
        //    if (index < 0)
        //        return;
        //    //If Drag-Drop Location are same
        //    if (index == prevRowIndex)
        //        return;
        //    //If the Drop Index is the last Row of DataGrid(
        //    // Note: This Row is typically used for performing Insert operation)
        //    if (index == dataGrid.Items.Count - 1)
        //    {
        //        MessageBox.Show("This row-index cannot be used for Drop Operations");
        //        return;
        //    }
        //    debugViewViewModel.SP_Propertys.Move(prevRowIndex, index);      
        //}

        //private int GetDataGridItemCurrentRowIndex(Func<IInputElement, Point> pos)
        //{
        //    int curIndex = -1;
        //    for (int i = 0; i < DataDisplyGrid.Items.Count; i++)
        //    {
        //        DataGridRow itm = GetDataGridRowItem(i);//
        //        if (IsTheMouseOnTargetRow(itm, pos))
        //        {
        //            curIndex = i;
        //            break;
        //        }
        //    }
        //    return curIndex;
        //}

        //private bool IsTheMouseOnTargetRow(DataGridRow theTarget, Func<IInputElement, Point> pos)
        //{
        //    Rect posBounds = VisualTreeHelper.GetDescendantBounds(theTarget);
        //    Point theMousePos = pos(theTarget);
        //    return posBounds.Contains(theMousePos);
        //}

        //private DataGridRow GetDataGridRowItem(int index)
        //{
        //    if (DataDisplyGrid.ItemContainerGenerator.Status
        //            != GeneratorStatus.ContainersGenerated)
        //        return null;

        //    return DataDisplyGrid.ItemContainerGenerator.ContainerFromIndex(index)
        //                                                    as DataGridRow;
        //}
    }
}
