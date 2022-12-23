using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using WeTest.Common.FileOperate;
using WpfApp3.Common;
using WpfApp3.Models.DebugViewModel;
using WpfApp3.Mvvm;
using WeTest.Common.Helpers.Json;
using WeTest.Common.EquipmentDrives.SerialPortDrives;
using System.Threading;
using System.Windows.Navigation;
using System.Windows.Markup;
using WeTest.Common.Converts;
using System.Collections;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Management;

namespace WpfApp3.ViewModels
{
    public class DebugViewViewModel : BindableBase
    {
        #region Fields
        private SerialPort serialPort;
        private int prevRowIndex;
        CancellationTokenSource _TokeSource;
        #endregion

        #region Propertys

        private bool _canContinueSend = true;
        public bool CanContinueSend
        {
            get { return _canContinueSend; }
            set { SetProperty(ref _canContinueSend, value); }
        }

        private bool _isSPOpen = true;
        public bool IsSPOpen
        {
            get { return _isSPOpen; }
            set { SetProperty(ref _isSPOpen, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                SetProperty(ref _message, value);
                //ConvertDisplay(HEXDisplay);
            }
        }

        private ObservableCollection<string> _sP_PortNames = new ObservableCollection<string>();
        public ObservableCollection<string> SP_PortNames
        {
            get
            {
                return _sP_PortNames;
            }
            set { SetProperty(ref _sP_PortNames, value); }
        }

        private string _currentSPPort;
        public string CurrentSPPort
        {
            get { return _currentSPPort; }
            set
            {
                SetProperty(ref _currentSPPort, value);
            }
        }

        private ObservableCollection<int> _sP_BaudRates = new ObservableCollection<int>();
        public ObservableCollection<int> SP_BaudRates
        {
            get
            {
                var BaudRates = new int[] { 9600, 1200, 2400, 4800, 14400, 19200, 38400, 56000, 57600, 115200 };
                _sP_BaudRates.AddRange(BaudRates);
                CurrentBaudRate = BaudRates[0];
                return _sP_BaudRates;
            }
            set { SetProperty(ref _sP_BaudRates, value); }
        }

        private int _currentBaudRate;
        public int CurrentBaudRate
        {
            get { return _currentBaudRate; }
            set { SetProperty(ref _currentBaudRate, value); }
        }

        private bool _isEnter = true;
        public bool IsEnter
        {
            get { return _isEnter; }
            set { SetProperty(ref _isEnter, value); }
        }

        private bool _isWrap;
        public bool IsWrap
        {
            get { return _isWrap; }
            set { SetProperty(ref _isWrap, value); }
        }

        private ObservableCollection<string> _models = new ObservableCollection<string>();
        public ObservableCollection<string> Models
        {
            get { return _models; }
            set { SetProperty(ref _models, value); }
        }

        private string _currentModel;
        public string CurrentModel
        {
            get { return _currentModel; }
            set
            {
                SetProperty(ref _currentModel, value);
                SP_Propertys.Clear();
                var fileInfos = FileUtil.OutputDir.GetFiles().Where(Ex => Ex.Extension == ".json" && Ex.Name.Replace(".json", "") == CurrentModel).ToList();
                var fileinfo = FileUtil.GetFileInfo(fileInfos.FirstOrDefault().Name);
                var jsonstr = File.ReadAllText(fileinfo.FullName);
                var sendDatalist = NewtonJsonHelper.JsonToObjList<SP_SendData>(jsonstr);
                //if (sendDatalist != null)
                //{
                SP_Propertys?.AddRange(sendDatalist);
                //}

            }
        }

        private string _addModelName = "Model1";
        public string AddModelName
        {
            get { return _addModelName; }
            set { SetProperty(ref _addModelName, value); }
        }

        private ObservableCollection<SP_SendData> _sP_Propertys = new ObservableCollection<SP_SendData>();
        public ObservableCollection<SP_SendData> SP_Propertys
        {
            get { return _sP_Propertys; }
            set { SetProperty(ref _sP_Propertys, value); }
        }

        private int _intervalTime = 500;
        public int IntervalTime
        {
            get { return _intervalTime; }
            set { SetProperty(ref _intervalTime, value); }
        }

        private bool _HEXDisplay;
        public bool HEXDisplay
        {
            get { return _HEXDisplay; }
            set
            {
                SetProperty(ref _HEXDisplay, value);
                ConvertDisplay(value);

            }
        }

        private SP_SendData _currentSendData = new SP_SendData();
        public SP_SendData CurrentSendData
        {
            get { return _currentSendData; }
            set { SetProperty(ref _currentSendData, value); }
        }
        private void ConvertDisplay(bool value)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                if (value)
                {
                    Message = StringConvert.StringToHexArray(Message);
                }
                else
                {
                    Message = StringConvert.HexStringToASCII(Message);
                }
            }
        }
        #endregion

        #region Commands

        //保存数据命令
        private DelegateCommand<string> _saveDataCommand; public DelegateCommand<string> SaveDataCommand =>
            _saveDataCommand ?? (_saveDataCommand = new DelegateCommand<string>(ExecuteSaveDataCommand)); void ExecuteSaveDataCommand(string fileName)
        {
            FileUtil.OutputDir = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}AppData" + "\\ModelsFile");
            JsonOperate jsonOperate123 = new JsonOperate();
            jsonOperate123.WriteJsonFile(FileUtil.OutputDir.FullName + $"\\{CurrentModel}" + ".json", SP_Propertys);
        }
        //清除显示信息命令
        private DelegateCommand _clearDataCommand; public DelegateCommand ClearDataCommand =>
            _clearDataCommand ?? (_clearDataCommand = new DelegateCommand(() => Message = ""));

        //连续发送命令
        private DelegateCommand _continuedSendCommand; public DelegateCommand ContinuedSendCommand =>
            _continuedSendCommand ?? (_continuedSendCommand = new DelegateCommand(ExecuteContinuedSendCommand, () => { return !IsSPOpen && CanContinueSend; })).ObservesProperty(() => CanContinueSend).ObservesProperty(() => IsSPOpen); void ExecuteContinuedSendCommand()
        {
            CanContinueSend = false;
            var data = SP_Propertys.Where(s => s.IsCheck == true);//确定数据是选择可以发送的
            _TokeSource = new CancellationTokenSource();
            Task sendTask = new Task(() =>
            {
                while (!_TokeSource.IsCancellationRequested)
                {
                    foreach (var item in data)
                    {
                        if (!_TokeSource.IsCancellationRequested)
                        {
                            SendDataMethod(item);
                            Thread.Sleep(IntervalTime);
                        }
                        else
                        {
                            break;
                        }

                    }
                }
            }, _TokeSource.Token);
            sendTask.Start();
        }

        //添加Model命令
        private DelegateCommand _addModelCommand; public DelegateCommand AddModelCommand =>
            _addModelCommand ?? (_addModelCommand = new DelegateCommand(ExecuteAddModelCommand)); void ExecuteAddModelCommand()
        {
            var filePath = FileUtil.OutputDir.FullName + "\\" + AddModelName + ".json";
            if (!Models.Contains(AddModelName))
            {
                var fileinfo = FileUtil.GetFileInfo($"{AddModelName}.json");
                if (!fileinfo.Exists)
                {
                    FileStream newfilestream = fileinfo.Create();
                    newfilestream.Dispose();
                }
                //SP_SendData sP_Send = new SP_SendData();
                //File.WriteAllText(filePath, JsonConvert.SerializeObject(sP_Send, Formatting.Indented));
                Models.Add(AddModelName);
                CurrentModel = AddModelName;
            }
            else
            {
                CurrentModel = AddModelName;
                MessageBox.Show($"{AddModelName}已存在!", "提示", MessageBoxButton.OKCancel);
            }

        }

        //打开串口命令
        private DelegateCommand _OpenSpCommand; public DelegateCommand OpenSpCommand =>
            _OpenSpCommand ?? (_OpenSpCommand = new DelegateCommand(ExecuteOpenSpCommand).ObservesCanExecute(() => IsSPOpen)); void ExecuteOpenSpCommand()
        {
            try
            {
                if (IsSPOpen)//true为没有打开串口
                {
                    serialPort = new SerialPort(CurrentSPPort, CurrentBaudRate, Parity.None, 8, StopBits.One);
                    if (serialPort.IsOpen != true)
                    {
                        serialPort.Open();
                        serialPort.DataReceived += (s, e) =>
                        {
                            //Thread.Sleep(300);
                            //按bytes 接收的是按字符将字符在转成 byte
                            if (!HEXDisplay)
                            {
                                char[] readCharBuffer = new char[serialPort.BytesToRead];
                                serialPort.Read(readCharBuffer, 0, serialPort.BytesToRead);
                                Message += string.Join("", readCharBuffer);
                            }
                            else
                            {
                                byte[] readByteBuffer = new byte[serialPort.BytesToRead];
                                serialPort.Read(readByteBuffer, 0, serialPort.BytesToRead);
                                Message += StringConvert.ByteArrayToHexString(readByteBuffer);//Sample字节数组到 "47 4B 53 30 37 35 32 39 31 30 36 34 41 32 38 30 30 0D 0A"
                            }
                        };
                    }
                    IsSPOpen = false;//串口已打开
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //关闭串口命令
        private DelegateCommand _closeSpCommand; public DelegateCommand CloseSpCommand =>
            _closeSpCommand ?? (_closeSpCommand = new DelegateCommand(ExecuteCloseSpCommand, () => { return !IsSPOpen; })).ObservesProperty(() => IsSPOpen); void ExecuteCloseSpCommand()
        {
            try
            {
                serialPort?.Close();
                IsSPOpen = true;
                CanContinueSend = true;
                _TokeSource?.Cancel();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //发送单条命令
        private DelegateCommand<SP_SendData> _sendDataCommand; public DelegateCommand<SP_SendData> SendDataCommand =>
          _sendDataCommand ?? (_sendDataCommand = new DelegateCommand<SP_SendData>(SendDataMethod, (s) => { return !IsSPOpen && CanContinueSend; })).ObservesProperty(() => IsSPOpen).ObservesProperty(() => CanContinueSend); private void SendDataMethod(SP_SendData command)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(command.Command) && command != null)
                {
                    var enter = IsEnter ? "\n" : "";
                    var wrap = IsWrap ? "\r" : "";
                    if (command.IsHexSend)
                    {
                        var tempData = StringConvert.HexStringToByteArray(command.Command);
                        serialPort.Write(tempData, 0, tempData.Length);
                    }
                    else
                    {
                        serialPort.Write(command.Command + wrap + enter);
                    }
                }
                else
                {
                    MessageBox.Show("发送数据不能为空", "提示!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //删除行命令
        private DelegateCommand<SP_SendData> _deleteRowCommand; public DelegateCommand<SP_SendData> DeleteRowCommand =>
     _deleteRowCommand ?? (_deleteRowCommand = new DelegateCommand<SP_SendData>(ExecuteDeleteRowCommand)); void ExecuteDeleteRowCommand(SP_SendData parameter)
        {
            var tt = SP_Propertys.Select((s, index) => (s == parameter, index1: index)).Where(item => item.Item1 == true).FirstOrDefault();//筛选当前选中行在集合中的位置
            SP_Propertys.RemoveAt(tt.index1);//在指定位置删除一行新数据。
        }
        //添加行命令
        private DelegateCommand<SP_SendData> _addRowCommand; public DelegateCommand<SP_SendData> AddRowCommand =>
     _addRowCommand ?? (_addRowCommand = new DelegateCommand<SP_SendData>(ExecuteAddRowCommand)); void ExecuteAddRowCommand(SP_SendData parameter)
        {
            var tt = SP_Propertys.Select((s, index) => (s == parameter, index1: index)).Where(item => item.Item1 == true).FirstOrDefault();//筛选当前选中行在集合中的位置
            SP_Propertys.Insert(tt.index1, new SP_SendData { });//在指定位置添加一行新数据。
        }

        //DataGrid鼠标移动命令
        private DelegateCommand<MouseEventArgs> _mouseMoveCommand; public DelegateCommand<MouseEventArgs> MouseMoveCommand =>
            _mouseMoveCommand ?? (_mouseMoveCommand = new DelegateCommand<MouseEventArgs>(ExecuteMouseMoveCommand)); void ExecuteMouseMoveCommand(MouseEventArgs parameter)
        {
            try
            {
                DataGrid dataGrid = parameter.Source as DataGrid;
                prevRowIndex = GetDataGridItemCurrentRowIndex(parameter.GetPosition, dataGrid);
                if (parameter.RightButton == MouseButtonState.Pressed && dataGrid.SelectedItem != null)
                {
                    DragDrop.DoDragDrop(dataGrid,
                                         dataGrid.SelectedItem,
                                         DragDropEffects.Move);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "move");
            }
        }

        //DataGridDrop命令

        //DataGrid DragEnter
        private DelegateCommand<DragEventArgs> _dragEnterCommand;
        public DelegateCommand<DragEventArgs> DragEnterCommand =>
            _dragEnterCommand ?? (_dragEnterCommand = new DelegateCommand<DragEventArgs>(ExecuteDragEnter));

        void ExecuteDragEnter(DragEventArgs parameter)
        {
            var dataGrid = parameter.Data.GetData(".txt") ;
            if (dataGrid==null)
            {
                parameter.Effects = DragDropEffects.None;
                parameter.Handled = true;
            }
        }
        private DelegateCommand<DragEventArgs> _dropCommand; public DelegateCommand<DragEventArgs> DropCommand =>
           _dropCommand ?? (_dropCommand = new DelegateCommand<DragEventArgs>(ExecuteDropCommand)); void ExecuteDropCommand(DragEventArgs parameter)
        {
            DataGrid dataGrid = parameter.Source as DataGrid;
            if (dataGrid == null)
                return;
            if (prevRowIndex < 0)//拖到的起始位置小于0
                return;
            int index = this.GetDataGridItemCurrentRowIndex(parameter.GetPosition, dataGrid);

            //The current Rowindex is -1 (No selected)
            if (index < 0)
                return;
            //If Drag-Drop Location are same
            if (index == prevRowIndex)
                return;
            //If the Drop Index is the last Row of DataGrid(
            // Note: This Row is typically used for performing Insert operation)
            if (index == dataGrid.Items.Count - 1)
            {
                MessageBox.Show("This row-index cannot be used for Drop Operations");
                return;
            }
            SP_Propertys.Move(prevRowIndex, index);
        }
        //串口选择 DropDownOpened
        private DelegateCommand<EventArgs> _comPortDropDownOpenedCommand; public DelegateCommand<EventArgs> ComPortDropDownOpenedCommand =>
            _comPortDropDownOpenedCommand ?? (_comPortDropDownOpenedCommand = new DelegateCommand<EventArgs>(ExecuteComPortDropDownOpenedCommand)); void ExecuteComPortDropDownOpenedCommand(EventArgs parameter)
        {
            //https://blog.csdn.net/Mad_Geek/article/details/105732811
            //        1. 开始-运行-输入：wbemtest 回车
            //2. 单击"连接", 输入：root\cimv2 回车; 或者ROOT\SecurityCenter 
            SP_PortNames.Clear();
            string queryStr = "SELECT * FROM Win32_PnPEntity WHERE PNPClass = " + @"""Ports""";
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", queryStr);
            List<string> DevicesID = new List<string>();
            foreach (ManagementObject service in managementObjectSearcher.Get())
            {
                //HardwareID 
                //string[] caption = (string[])service.GetPropertyValue("Name");//硬件ID是唯一，不可以变的值，他是字符串数组类型。
                var DeviceID = service.GetPropertyValue("Caption").ToString();
                //如何获取字符串小括号中的值（），思路：先获取左  "（"  字符串的位置,      然后获取  ")"在字符串的位置，再用Substring()截取字符
                var tempstr = DeviceID.Select((s, index) => (Lstr: s == '(', index1: index, Rstr: s == ')')).Where(l => l.Lstr == true || l.Rstr == true);

                //var tempstr1 = DeviceID.Select((s, index) => new { Lstr = s == '(', index1 = index, Rstr = s == ')' }).Where(l => l.Lstr == true || l.Rstr == true);

                DevicesID.Add(DeviceID.Substring(tempstr.ToArray()[0].index1 + 1, tempstr.ToArray()[1].index1 - tempstr.ToArray()[0].index1 - 1));
            }
            DevicesID.OrderBy(s => s).ToList().ForEach(d => SP_PortNames.Add(d));
            CurrentSPPort = SP_PortNames[0];
        }
        //串口选择 DropDownClosed
        private DelegateCommand<EventArgs> _comPortDropDownClosedCommand; public DelegateCommand<EventArgs> ComPortDropDownClosedCommand =>
            _comPortDropDownClosedCommand ?? (_comPortDropDownClosedCommand = new DelegateCommand<EventArgs>(ExecuteComPortDropDownClosedCommand)); void ExecuteComPortDropDownClosedCommand(EventArgs parameter)
        {
            CloseSpCommand.Execute();
            OpenSpCommand.Execute();
        }
        #endregion

        #region Private Methods
        private void GetModelsNameAndPortsName()
        {
            Models.Clear();//清除itemsSource 会调用SelectedItems 的set方法
            var fileInfos = FileUtil.OutputDir.GetFiles().Where(Ex => Ex.Extension == ".json").ToList();//查找指定路径下的.json文件。
            fileInfos?.ForEach(fi => Models.Add(fi.Name.Replace(".json", "")));//添加文件名字到Models
            CurrentModel = Models[0];
            SerialPort.GetPortNames().OrderBy(s => s).ToList().ForEach((s) => _sP_PortNames.Add(s));//添加电脑COM名到_sP_PortNames
            CurrentSPPort = _sP_PortNames[0];
        }
        #region 关于Drop方法
        private int GetDataGridItemCurrentRowIndex(Func<IInputElement, Point> pos, DataGrid dataGrid)
        {
            int curIndex = -1;
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                DataGridRow itm = GetDataGridRowItem(i, dataGrid);//
                if (itm != null)
                {
                    if (IsTheMouseOnTargetRow(itm, pos))
                    {
                        curIndex = i;
                        break;
                    }
                }
            }
            return curIndex;
        }
        private bool IsTheMouseOnTargetRow(DataGridRow theTarget, Func<IInputElement, Point> pos)
        {
            Rect posBounds = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point theMousePos = pos(theTarget);
            return posBounds.Contains(theMousePos);
        }
        private DataGridRow GetDataGridRowItem(int index, DataGrid dataGrid)
        {
            if (dataGrid.ItemContainerGenerator.Status
                    != GeneratorStatus.ContainersGenerated)
                return null;

            return dataGrid.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }
        #endregion
        #endregion
        public DebugViewViewModel(IRegionManager regionManager)
        {
            FileUtil.OutputDir = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}AppData" + "\\ModelsFile");
            GetModelsNameAndPortsName();
        }

    }
}
