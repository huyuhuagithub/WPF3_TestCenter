using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models.DebugViewModel
{
    public class SP_SendData : BindableBase
    {

        private string _command;
        public string Command
        {
            get { return _command; }
            set { SetProperty(ref _command, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private bool _isCheck;
        public bool IsCheck
        {
            get { return _isCheck; }
            set { SetProperty(ref _isCheck, value); }
        }

        private bool _isHexSend;
        public bool IsHexSend
        {
            get { return _isHexSend; }
            set { SetProperty(ref _isHexSend, value); }
        }

       
    }
}
