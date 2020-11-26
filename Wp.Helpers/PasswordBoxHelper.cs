using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Wp.Helpers
{
    /// <summary>

    /// Password 绑定功能
    /// </summary>
    //public static class PasswordBoxHelper
    //{
    //    public static readonly DependencyProperty PasswordProperty =
    //        DependencyProperty.RegisterAttached("Password",
    //        typeof(string), typeof(PasswordBoxHelper),
    //        new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

    //    public static readonly DependencyProperty AttachProperty =
    //        DependencyProperty.RegisterAttached("Attach",
    //        typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false, Attach));

    //    private static readonly DependencyProperty IsUpdatingProperty =
    //       DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
    //       typeof(PasswordBoxHelper));

    //    public static void SetAttach(DependencyObject dp, bool value)
    //    {
    //        dp.SetValue(AttachProperty, value);
    //    }

    //    public static bool GetAttach(DependencyObject dp)
    //    {
    //        return (bool)dp.GetValue(AttachProperty);
    //    }

    //    public static string GetPassword(DependencyObject dp)
    //    {
    //        return (string)dp.GetValue(PasswordProperty);
    //    }

    //    public static void SetPassword(DependencyObject dp, string value)
    //    {
    //        dp.SetValue(PasswordProperty, value);
    //    }

    //    private static bool GetIsUpdating(DependencyObject dp)
    //    {
    //        return (bool)dp.GetValue(IsUpdatingProperty);
    //    }

    //    private static void SetIsUpdating(DependencyObject dp, bool value)
    //    {
    //        dp.SetValue(IsUpdatingProperty, value);
    //    }

    //    private static void OnPasswordPropertyChanged(DependencyObject sender,
    //        DependencyPropertyChangedEventArgs e)
    //    {
    //        PasswordBox passwordBox = sender as PasswordBox;
    //        passwordBox.PasswordChanged -= PasswordChanged;
    //        if (!(bool)GetIsUpdating(passwordBox))
    //        {
    //            passwordBox.Password = (string)e.NewValue;
    //        }
    //        passwordBox.PasswordChanged += PasswordChanged;
    //    }

    //    private static void Attach(DependencyObject sender,
    //        DependencyPropertyChangedEventArgs e)
    //    {
    //        PasswordBox passwordBox = sender as PasswordBox;
    //        if (passwordBox == null)
    //            return;
    //        if ((bool)e.OldValue)
    //        {
    //            passwordBox.PasswordChanged -= PasswordChanged;
    //        }
    //        if ((bool)e.NewValue)
    //        {
    //            passwordBox.PasswordChanged += PasswordChanged;
    //        }
    //    }

    //    private static void PasswordChanged(object sender, RoutedEventArgs e)
    //    {
    //        PasswordBox passwordBox = sender as PasswordBox;
    //        SetIsUpdating(passwordBox, true);
    //        SetPassword(passwordBox, passwordBox.Password);
    //        SetIsUpdating(passwordBox, false);
    //    }
    //}
    //Step1
    //xmlns:pw="clr-namespace:ToWin.ArmorBreaker.BLL.Helpers;assembly=ToWin.ArmorBreaker.BLL"
    //    Step2
    //        <PasswordBox  Grid.Column="3"

    //                                  FontSize="20"

    //                                  materialDesign:HintAssist.Hint="请输入密码"

    //                                  materialDesign:TextFieldAssist.HasClearButton="True"

    //                                  materialDesign:TextFieldAssist.UnderlineBrush="{StaticResource PrimaryTertiarySolidColorBrushStyle}"

    //                                  Foreground="{StaticResource PrimaryFourthSolidColorBrushStyle}"

    //                                  materialDesign:HintAssist.HintOpacity=".26"

    //                                  pw:PasswordBoxHelper.Attach="True"

    //                                  pw:PasswordBoxHelper.Password="{Binding PWD, Mode=TwoWay }" />
}