using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using WpfApp3.Models;
using WeTest.Common.FileOperate;
using WpfApp3.Common;

namespace WpfApp3.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields
        private IRegionManager _regionManager;
        private IDialogService _dialogService;

        #endregion

        #region Propertys
        private LoginUser _loginUser = new LoginUser();
        public LoginUser LoginUser1
        {
            get { return _loginUser; }
            set { SetProperty(ref _loginUser, value); }
        }

        private List<LoginUser> _userDto = new List<LoginUser>();
        public List<LoginUser> UserDto
        {
            get { return _userDto; }
            set { SetProperty(ref _userDto, value); }
        }
        #endregion
        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService, LoginUser loginUser)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            new BuildFolder();//生成文件夹并给FileUtil.OutputDir属性赋值。
            JsonOperate operate = new JsonOperate();
            UserDto = operate.GetConfig<LoginUser>(FileUtil.OutputDir, "UsersConfig").values;
        }

        #region Commands

        private DelegateCommand _showTestView;
        public DelegateCommand ShowTestView =>
            _showTestView ?? (_showTestView = new DelegateCommand(() =>
            {
                if (_loginUser.LoginState != true)
                {
                    ExecuteUserLogin();
                    if (_loginUser.LoginState == true)
                    {
                        _regionManager.RequestNavigate("contentRegion", "TestView");
                    }
                }
                if (_loginUser.LoginState == true)
                {
                    _regionManager.RequestNavigate("contentRegion", "TestView");
                }
            }));

        private DelegateCommand _showDataViewA;
        public DelegateCommand ShowDataViewA =>
            _showDataViewA ?? (_showDataViewA = new DelegateCommand(() =>
            {
                ShowView("DataViewA");
            }));      

        private DelegateCommand _showSetterView;
        public DelegateCommand ShowSetterView =>
            _showSetterView ?? (_showSetterView = new DelegateCommand(() =>
            {
                ShowView("SetterView");                
            }));

        private DelegateCommand _showDebugView;
        public DelegateCommand ShowDebugView =>
            _showDebugView ?? (_showDebugView = new DelegateCommand(() =>
            {
                ShowView("DebugView");
            }));

        private DelegateCommand _userLogin;
        public DelegateCommand UserLogin =>
            _userLogin ?? (_userLogin = new DelegateCommand(ExecuteUserLogin));
        void ExecuteUserLogin()
        {
            var parameter = new DialogParameters();
            parameter.Add("loginList", UserDto);
            _dialogService.ShowDialog("UserLogin", parameter, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    LoginUser1 = r.Parameters.GetValue<LoginUser>("LoginStatus");//登录成功后获取登录状态。
                    _regionManager.RequestNavigate("contentRegion", "TestView");
                }
            });
        }
        private void ShowView(string viewName)
        {
            if (_loginUser.LoginState != true)
            {
                ExecuteUserLogin();
                if (_loginUser.UserRole == Level.Engineer)
                {
                    _regionManager.RequestNavigate("contentRegion", viewName);
                }
            }
            if (_loginUser.UserRole == Level.Engineer)
            {
                _regionManager.RequestNavigate("contentRegion", viewName);
            }
        }
        #endregion



    }
}
