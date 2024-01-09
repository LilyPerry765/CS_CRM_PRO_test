using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Enterprise;

namespace CRM.Application.UserControls
{
    public partial class ExtendedStatusBar : UserControl
    {
        /// <summary>
        /// چنانچه در یک شی از کلاس جاری برای نمایش دیتا به صورت لیست استفاده شود 
        /// مسلماً عملیات فرخوانی دیتای مربوطه ممکن است زمانبر باشد و باید به نوعی به کابر وضعیت بارگذاری دیتا اطلاع رسانی شود
        /// برای پیاده سازی این موضوع کنترلی تحت عنوان زیر ایجاد کردم
        /// ListBasedUserControlProgressBar
        /// قابل مشاهده بودن این کنترل مشروط به مقدار پراپرتی ذیل میباشد
        /// if ShowProgressBar = true then ListBasedUserControlProgressBar.Visibility = Visible
        /// if ShowProgressBar = false then ListBasedUserControlProgressBar.Visibility = Collapsed
        /// البته به صورت پیش فرض کنترل بالا نمایش داده نمیشود
        /// </summary>
        public bool ShowProgressBar
        {
            get { return (bool)GetValue(ShowProgressBarProperty); }
            set { SetValue(ShowProgressBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowProgressBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowProgressBarProperty =
            DependencyProperty.Register("ShowProgressBar", typeof(bool), typeof(ExtendedStatusBar), new PropertyMetadata(false));



        private string _Message;

        public ExtendedStatusBar()
        {
            InitializeComponent();
            ListBasedUserControlProgressBar.DataContext = this;
        }

        public void ShowSuccessMessage(string message)
        {
            MessageLabel.Text = _Message = message;

            MessageLabel.Foreground = Brushes.Green;
            MessageIcon.Source = Helper.GetBitmapImage("accept_16x16.png");

            //Helper.ToggleControlFade(MessagePanel, 10000);
        }

        public void ShowWarningsMessage(string message)
        {
            MessageLabel.Text = _Message = message;

            MessageLabel.Foreground = Brushes.OrangeRed;
            MessageIcon.Source = Helper.GetBitmapImage("warning_16x16.png");

            //Helper.ToggleControlFade(MessagePanel, 10000);
        }

        public void ShowErrorMessage(string message, Exception ex)
        {
            MessageLabel.Text = string.Empty;

            MessageLabel.Text = message;

            MessageLabel.Foreground = Brushes.Red;
            MessageIcon.Visibility = Visibility.Visible;
            MessageIcon.Source = Helper.GetBitmapImage("error_16x16.png");

            MessageIcon.ToolTip = ex.Message;

            //Helper.ToggleControlFade(MessagePanel, 10000);

            Logger.Write(ex, message);

            _Message = ex.Message;
        }

        public void HideMessage()
        {
            MessageLabel.Text = string.Empty;
            MessageIcon.Source = null;
            MessageLabel.Foreground = Brushes.Black;
            //MessageIcon.Visibility = Visibility.Collapsed;
        }

        private void MessagePanel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show(_Message);
            this.HideMessage();
        }

    }
}