using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Prism.Mvvm;
namespace WpfApp3.Models.DebugViewModel
{
    public class InterfaceProperties:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> SP_PortNames { get; set; }=new List<string>();
        public List<int> SP_BaudRate { get; set; } = new List<int>();
        public string CurrentPortName { get; set; } = "COM1";
        public int CurrentBaudRate { get; set; } = 9600;

        public bool IsWrap { get; set; }
        public bool IsEnter { get; set; }
        public bool IsHEX { get; set; }
        public int IntervalTime { get; set; } = 500;
        public string AddModelName { get; set; } = "Model1";
        public ObservableCollection<string> Models { get; set; } = new ObservableCollection<string>();
        public string CurrentModel { get; set; } = "Model1";
       public ObservableCollection<SP_SendData> SP_SendDatas { get; set; } = new ObservableCollection<SP_SendData>();
    }
}
