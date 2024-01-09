using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TaskbarNotificationForm.xaml
    /// </summary>
    public partial class TaskbarNotificationForm : Window
    {
        public TaskbarNotificationForm()
        {
            InitializeComponent();
            SetPositionWindows();
            startTimer();
            
        }

        private bool closeCompleted = false;


        private void FormFadeOut_Completed(object sender, EventArgs e)
        {
            closeCompleted = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (!closeCompleted)
            {
                FormFadeOut.Begin();
                e.Cancel = true;
            }
        }

        private void startTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer(); //a new DispatcherTimer
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5); //set the interval 1 second
            dispatcherTimer.Start(); //start the timer
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick); //set the timer tick
        }

        void dispatcherTimer_Tick(object sender, EventArgs e) //timer tick
        {
                this.Close(); //here is your instruction: for examplse this.Close() will close your mainwindow
        }

        private void SetPositionWindows()
        {
            Left = SystemParameters.WorkArea.Width - Width - 10;
            Top = SystemParameters.WorkArea.Height - Height - 10;
        }
    }
}
