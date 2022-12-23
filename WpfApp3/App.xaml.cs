using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WeTest.DAL.DTOs;
using WpfApp3.Common;
using WpfApp3.Models;
using WpfApp3.ViewModels;
using WpfApp3.Views;
namespace WpfApp3
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        //private IDialogService _dialogService;
       
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //视图定位，将view 的datacontext 绑定到ViewModel上。
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
            ViewModelLocationProvider.Register<DataViewA, DataViewAViewModel>();
            ViewModelLocationProvider.Register<TestView, TestViewViewModel>();
            ViewModelLocationProvider.Register<SetterView, SetterViewViewModel>();
            ViewModelLocationProvider.Register<DebugView, DebugViewViewModel>();

            //RegisterNavigation
            containerRegistry.RegisterForNavigation<TestView>();
            containerRegistry.RegisterForNavigation<DataViewA>();
            containerRegistry.RegisterForNavigation<SetterView>();
            containerRegistry.RegisterForNavigation<DebugView>();
            //RegisterDialog
            containerRegistry.RegisterDialog<UserLogin>();
            //RegisterType
        

        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
          
            //_dialogService = Container.Resolve<IDialogService>();
            //IRegionManager regionManager = Container.Resolve<IRegionManager>();   
        }
    }
}
