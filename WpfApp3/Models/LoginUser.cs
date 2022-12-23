using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class LoginUser : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string UserName { get; set; }
        public string Password { get; set; }
        public Level UserRole { get; set; }
        public bool LoginState { get; set; }
    }
    public enum Level
    {
        Operater,
        Engineer
    }
}
