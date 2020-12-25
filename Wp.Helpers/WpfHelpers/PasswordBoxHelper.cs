using System.Windows;
using System.Windows.Controls;

namespace Wp.Helpers.WpfHelpers
{
    /// <summary>
    /// PasswordBox控件帮助类
    /// </summary>
    public static class PasswordBoxHelper
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password",
            typeof(string), typeof(PasswordBoxHelper),
            new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        /// <summary>
        ///
        /// </summary>
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
            typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false, Attach));

        /// <summary>
        ///
        /// </summary>
        private static readonly DependencyProperty IsUpdatingProperty =
           DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
           typeof(PasswordBoxHelper));

        /// <summary>
        ///
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="value"></param>
        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="value"></param>
        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="value"></param>
        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;
            if (!GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Attach(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
                return;
            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }
            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
    }
}