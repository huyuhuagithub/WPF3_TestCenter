   M S B A M L   `   `   `  ????  WpfApp3    WpfApp3.Views.UserLogin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      Z VPrism.Unity.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      $	??500??5	      $	??350??5
      #  Prism.Services.Dialogs.Dialog    WindowStyle  5      ?? 5   
    ?? 
TargetType" ?6   .?5      ??  ?? Property ?@ WindowStartupLocation ?? Value$ CenterScreen=?6C   ?? 5       ?@?
ResizeMode$ NoResize=?6+   ?? 5       ?@?ShowInTaskbar .  6.   ?? 5       ?@?SizeToContent$ WidthAndHeight=?6.   ?? 5       ?@?Icon$ ..\backgroud.jpeg??6%   5      .??5      ?  ? RowDefinitions 5   
   ?? 5      ?? 5      ?? 5      .+?5   
   ? $??1??6    ? ColumnDefinitions 5       ?? 5!      $??3* ?6#   ?? 5"      $??7* ?6#    5$      ?? 5%      ?? 5&      ?? 5'      $??20 ?6    .+?5)      ?? $??Right=?5*      $??Center=?5+      
???   ?$??账号名：??5-      ?? 5.      $??1??5/      $	??200??50      $??25??51      $??5,0,0,0q?52      $
??Left=?53      $??Center=?54      ??55      ?? *	UserNames+ ?? SelectedItem 56      ?? *	Account+?? 57      $??1??58      $??Right=?59      $??Center=?5:      
???   ?$??	密码：??5<      G? 5=      -   5         G?Name$ pd??5>      $??1??5?      $??1??5@      $	??200??5A      $??25??5B      $??5,0,0,0q?5C      $
??Left=?5D      $??Center=?5E       G? Password$	 223??5F      ?? 5G      $??2??5H      $??2??5I      
???  ????5K      ?? *
PromptText+5L      ?? 5N   
   $??2??5O      $??Center=?5P      $??Center=?5Q      	 ?? Orientation$	 
Horizontal=?5R      .+?5S      ?? $	??100??5T      $??0,0,10,0q?5U      $??登录??5X      ??5V      ?? *LoginCommand+??5W      ?? 
 ?? ElementName$
 pd??5X      ?? 5Y      $	??100??5Z      $??取消??5\      ??5[      ?? *CancelCommand+5\      5]      5^      5_      