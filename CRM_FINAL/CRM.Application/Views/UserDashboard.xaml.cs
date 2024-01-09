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
using Enterprise;
using CRM.Data;
using System.Collections.ObjectModel;
using System.Threading;
using System.Data.SqlClient;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using Stimulsoft.Report;
using Stimulsoft.Base;
using System.Timers;

namespace CRM.Application.Views
{
    public partial class UserDashboard : Local.TabWindow, Folder.IFolderForm
    {
        #region Properties

        private bool _IsLoaded = false;
        SqlDependency depend;
        private List<RequestTitleInfo> requestInboxInfos = new List<RequestTitleInfo>();


        #endregion

        #region Constructors

        public UserDashboard()
        {
            InitializeComponent();
        }


        #endregion

        #region Methods

        public void Initialize(Folder.FolderFormHelper helper)
        {
            helper.Refresh += new EventHandler<Folder.RefreshEventArgs>(helper_Refresh);
        }

        private void Search()
        {
        }

        private void LoadData()
        {
            //Failure117DB.CheckFailureRequestStatus();

            int requestCount = 0;
            requestInboxInfos = RequestDB.GetRequestsStepsGroupInfo();

            DashboardCounterContainer.Children.Clear();

            List<String> requestTitleInfos = requestInboxInfos.Select(t => t.RequestTitle).Distinct().ToList();

            for (int i = 0; i < requestTitleInfos.Count; i++)
            {
                requestCount = 0;
                List<RequestTitleInfo> requestInboxInfo = requestInboxInfos.Where(t => t.RequestTitle == requestTitleInfos[i]).ToList();
                UserControls.DashboardCounter parentControl = new UserControls.DashboardCounter();
                parentControl.Header = requestTitleInfos[i];

                parentControl.ColorNumber = i + 1;

                parentControl.Margin = new Thickness(5);
                for (int j = 0; j < requestInboxInfo.Count; j++)
                {
                    RequestInboxInfo item = requestInboxInfo[j].RequestDetails;

                    //if (item.RequestTypeID == (byte)DB.RequestType.Failure117 && item.StepID == (byte)DB.RequestStepFailure117.Archived)
                    //{
                    //    Failure117DB.CheckFailureRequestStatus();
                    //}

                    UserControls.DashboardCounterDetails control = new UserControls.DashboardCounterDetails();

                    control.Header = item.StepTitle;
                    control.Tag = item.StepID;
                    control.LastItemDate = item.LastRequestDate;
                    control.Count = item.Count;
                    requestCount += item.Count;
                    control.ColorNumber = parentControl.ColorNumber;
                    control.MouseUp += new MouseButtonEventHandler(DashboardCounter_MouseUp);
                    parentControl.GridMiddleStackPanel.Children.Add(control);
                    _IsLoaded = true;
                }

                parentControl.RequestCount = requestCount;

                DashboardCounterContainer.Children.Add(parentControl);
            }
        }

        public void Listen()
        {
            try
            {
                depend = null;
                using (MainDataContext context = new MainDataContext())
                {
                    using (SqlConnection conn = new SqlConnection(context.Connection.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT [ID],[StatusID] FROM [dbo].[Request]", conn))
                        {
                            depend = new SqlDependency(cmd);
                            SqlDependency.Start(context.Connection.ConnectionString);
                            depend.OnChange += new OnChangeEventHandler(MyOnChanged);
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteInfo(ex.Message);
            }
        }

        #endregion

        #region Event Handlers

        public void MyOnChanged(object sender, SqlNotificationEventArgs e)
        {


            Dispatcher.BeginInvoke((Action)delegate()
            {

                List<RequestTitleInfo> oldRequestTitleInfos = requestInboxInfos.ToList();
                LoadData();
                List<RequestTitleInfo> newRequestTitleInfos = requestInboxInfos.ToList();


                ////
                List<RequestTitleInfo> insertRequestTitleInfo = newRequestTitleInfos.Where(t => !oldRequestTitleInfos.Any(d => d.RequestTitle == t.RequestTitle && d.RequestDetails.StepTitle == t.RequestDetails.StepTitle)).ToList();
                List<RequestTitleInfo> updateRequestTitleInfo = newRequestTitleInfos.Where(t => oldRequestTitleInfos.Any(d => d.RequestTitle == t.RequestTitle && d.RequestDetails.StepTitle == t.RequestDetails.StepTitle && d.RequestDetails.Count < t.RequestDetails.Count)).ToList();
                List<RequestTitleInfo> ChangeRequestTitleInfo = insertRequestTitleInfo.Union(updateRequestTitleInfo).ToList();

                ////



                if (ChangeRequestTitleInfo.Count > 0 && (DB.CurrentUser.UserConfig != null && DB.CurrentUser.UserConfig.PupupNotification == true))
                {


                    CRM.Application.Views.TaskbarNotificationForm taskbarNotification = new TaskbarNotificationForm();
                    taskbarNotification.TitleStepTextBlock.Text = ChangeRequestTitleInfo[0].RequestTitle + "-" + ChangeRequestTitleInfo[0].RequestDetails.StepTitle;
                    taskbarNotification.CountTextBlock.Text = "تعداد در خواست ها :" + ChangeRequestTitleInfo[0].RequestDetails.Count;
                    //  Logger.WriteInfo("show TaskbarNotificationForm");
                    taskbarNotification.Show();


                }

            }
            );

            depend.OnChange -= new OnChangeEventHandler(MyOnChanged);

            Listen();
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_IsLoaded)
                return;

            LoadData();

            if (DB.CurrentUser.UserConfig != null && DB.CurrentUser.UserConfig.DashboardAutoUpdate == true)
            {
                Listen();
            }
            else
            {
                System.Timers.Timer aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

                double? minutes = DB.CurrentUser.UserConfig == null ? 5 : DB.CurrentUser.UserConfig.DashboardUpdateTime;
                if (minutes == 0 || minutes == null) minutes = 5;

                aTimer.Interval = (double)(minutes * 60 * 1000);
                aTimer.Enabled = true;
            }
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)delegate()
              {
                LoadData();
              });
        }

        private void DashboardCounter_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RequestsInbox requestsInbox = new Views.RequestsInbox();
            requestsInbox.SelectedStepID = (int)(sender as UserControls.DashboardCounterDetails).Tag;
            requestsInbox.RequestTypeID = Data.RequestStepDB.GetRequestStepByID(requestsInbox.SelectedStepID).RequestTypeID;
            
            //TODO:rad 13950227
            //چون منو آیتم چاپ برای گزارش های دارای الگو میباشد
            //در کد کلاس زیر متدها بر اساس یک مرحله از یک روال پیاده سازی شده اند
            //RequestsInbox.SelectedStepID 
            //وقتی کلاس بالا از طریق کارتابل درخواست باز شود ، کاربر میتواند چند مرحله را انتخاب و جستجو را اعمال کند
            //در این حالت منو آیتم چاپ به خطا میخورد
            //لذا منو آیتم چاپ فقط باید در حالتی نمایش داده شود که کلاس بالا از طریق داشبورد باز شود یعنی یک مرحله خاص از یک روال خاص
            requestsInbox.PrintMenuItem.Visibility = Visibility.Visible;
            
            Folder.Console.Navigate(requestsInbox, (sender as UserControls.DashboardCounterDetails).Header);
        }

        private void helper_Refresh(object sender, Folder.RefreshEventArgs e)
        {
            LoadData();
        }

        #endregion
    }
}
