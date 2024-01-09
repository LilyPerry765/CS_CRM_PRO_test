using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Enterprise;

namespace CRM.Application.UserControls
{
    public partial class StatusBar : UserControl
    {
        private string _Message;

        public StatusBar()
        {
            InitializeComponent();
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
            MessageIcon.Visibility = Visibility.Collapsed;
            _Message = string.Empty;
        }

        private void MessagePanel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show(_Message);
            this.HideMessage();
        }

    }
}