using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WeTest.Common.FileOperate;
using WeTest.Common.Helpers.Json;
using WeTest.Common.User;
using WeTest.DAL.SQLite;
using WpfApp3.Common;
using WpfApp3.Models;
using WpfApp3.Mvvm;
using WpfApp3.Views;

namespace WpfApp3.ViewModels
{
    public class UserLoginViewModel : RegionViewModelBase
    {

        #region Fields
        private string _account = "admin", _passworld;
        #endregion

        #region Propertys

        private List<LoginUser> _loginUsers = new List<LoginUser>();
        public List<LoginUser> LoginUsers
        {
            get { return _loginUsers; }
            set { SetProperty(ref _loginUsers, value); }
        }

        private List<string> _userName = new List<string>();
        public List<string> UserNames
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _promptText;
        public string PromptText
        {
            get { return _promptText; }
            set { SetProperty(ref _promptText, value); }
        }
        public string Account { get => _account; set { SetProperty(ref _account, value); } }
        public string Passworld { get => _passworld; set { SetProperty(ref _passworld, value); } }
        #endregion

        #region Commands
        private DelegateCommand<PasswordBox> _loginCommand;
        public DelegateCommand<PasswordBox> LoginCommand { get => _loginCommand = new DelegateCommand<PasswordBox>(LoginAction); }
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand { get => _cancelCommand = new DelegateCommand(CancelAction); }
        #endregion

        #region diglogService 
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            LoginUsers = parameters.GetValue<List<LoginUser>>("loginList");
            UserNames = LoginUsers.Select(user => user.UserName).ToList();
        }
        
        #endregion
        private void CancelAction()
        {
            RequestCloseAction(new DialogResult(ButtonResult.Cancel, null));
        }

        private void LoginAction(PasswordBox pd)
        {
            try
            {
                var loginUser = LoginUsers.Where(user => user.UserName == Account && user.Password == pd.Password).FirstOrDefault();
                if (loginUser != null)
                {
                    var parameter = new DialogParameters();
                    parameter.Add("LoginStatus", loginUser);
                    RequestCloseAction(new DialogResult(ButtonResult.OK, parameter));
                }
                PromptText = "Account, Passworld Error!";
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        public UserLoginViewModel(IRegionManager regionManager) :
            base(regionManager)
        {
            Title = "LoginWindow";
        }
    }
}
