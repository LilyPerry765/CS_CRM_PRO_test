using CRM.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.Website.UserControl
{
    public partial class RequestDashboard : System.Web.UI.UserControl
    {
        #region Properties

        private bool _IsLoaded = false;
        SqlDependency depend;
        private List<RequestTitleInfo> requestInboxInfos = new List<RequestTitleInfo>();

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_IsLoaded)
                return;
            LoadData();
            Listen();
        }

        public void MyOnChanged(object sender, SqlNotificationEventArgs e)
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
                //CRM.Application.Views.TaskbarNotificationForm taskbarNotification = new TaskbarNotificationForm();
                //taskbarNotification.TitleStepTextBlock.Text = ChangeRequestTitleInfo[0].RequestTitle + "-" + ChangeRequestTitleInfo[0].RequestDetails.StepTitle;
                //taskbarNotification.CountTextBlock.Text = "تعداد در خواست ها :" + ChangeRequestTitleInfo[0].RequestDetails.Count;
                ////  Logger.WriteInfo("show TaskbarNotificationForm");
                //taskbarNotification.Show();
            }

            depend.OnChange -= new OnChangeEventHandler(MyOnChanged);
            Listen();
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            int requestCount = 0;
            requestInboxInfos = RequestDB.GetRequestsStepsGroupInfo();

            DashboardCounterContainer.Controls.Clear();

            List<String> RequestTitleInfo = requestInboxInfos.Select(t => t.RequestTitle).Distinct().ToList();

            for (int i = 0; i < RequestTitleInfo.Count; i++)
            {
                requestCount = 0;
                List<RequestTitleInfo> requestInboxInfo = requestInboxInfos.Where(t => t.RequestTitle == RequestTitleInfo[i]).ToList();
                UserControl.DashboardCounter dashboardCounter = LoadControl("~/UserControl/DashboardCounter.ascx") as UserControl.DashboardCounter;
                dashboardCounter.Header = RequestTitleInfo[i];
                dashboardCounter.ColorNumber = i + 1;
                List<DashboardCounterDetails> tempDetailControl = new List<DashboardCounterDetails>();

                for (int j = 0; j < requestInboxInfo.Count; j++)
                {
                    RequestInboxInfo item = requestInboxInfo[j].RequestDetails;
                    UserControl.DashboardCounterDetails dashboardCounterDetail = LoadControl("~/UserControl/DashboardCounterDetails.ascx") as UserControl.DashboardCounterDetails;

                    dashboardCounterDetail.Header = item.StepTitle; ;
                    dashboardCounterDetail.StepID = item.StepID;

                    dashboardCounterDetail.LastItemDate = item.LastRequestDate;
                    dashboardCounterDetail.Count = item.Count;
                    requestCount += item.Count;
                    dashboardCounterDetail.ColorNumber = dashboardCounter.ColorNumber;

                    dashboardCounter.Controls.Add(dashboardCounterDetail);
                    tempDetailControl.Add(dashboardCounterDetail);

                    _IsLoaded = true;
                }
                dashboardCounter.MainControls = tempDetailControl;
                dashboardCounter.RequestCount = requestCount;
                DashboardCounterContainer.Controls.Add(dashboardCounter);
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
                        using (SqlCommand cmd = new SqlCommand("SELECT [ID],[StatusID],[EndDate] FROM [dbo].[Request]", conn))
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
            catch {}
        }
        #endregion
    }
}