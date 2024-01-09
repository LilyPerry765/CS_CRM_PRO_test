using CaseManagement.Notification.Hubs;
using CaseManagement.Notification.Hubs;
using Microsoft.AspNet.SignalR;
using Serenity.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Hosting;
using System.Linq;
using System.Collections.Generic;
//using log4net;
using System.Configuration;

namespace CaseManagement.Notification
{
    public class BackgroundNotifier : IRegisteredObject
    {
        //static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IHubContext _notificationHub;
        private Timer _timer;
        private static DataTable dtPreviousNotifications;

        public BackgroundNotifier()
        {
            _notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

            StartTimer();
        }

        private void StartTimer()
        {
            var delayStartby = 5000;
            var repeatEvery = 10000;

            int parsedInterval;
            if (int.TryParse(ConfigurationManager.AppSettings["NotificationsIntervalMsec"], out parsedInterval))
            {
                repeatEvery = parsedInterval;
            }

            _timer = new Timer(BroadcastNotificationToClients, null, delayStartby, repeatEvery);
        }

        private void BroadcastNotificationToClients(object state)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(SqlConnections.GetConnectionString("Default").ConnectionString);
                sqlConnection.Open();
                DataTable dtNotifications = new DataTable();
                SqlCommand sqlCmdNotifications = new SqlCommand("SELECT GroupId,Id, FROM Notifications ORDER BY GroupId,Id", sqlConnection);
                SqlDataAdapter sqlDaNotifications = new SqlDataAdapter(sqlCmdNotifications);
                sqlDaNotifications.Fill(dtNotifications);

                if (dtPreviousNotifications == null)
                {
                    dtPreviousNotifications = dtNotifications;
                    return;
                }

                DataTable dtNotificationsDistinctSupportGroup = dtNotifications.DefaultView.ToTable(true, "GroupId");

                foreach (DataRow drNotificationsDistinctSupportGroup in dtNotificationsDistinctSupportGroup.Rows)
                {
                    DataRow[] drArrayPreviousNotifications = dtPreviousNotifications.Select("GroupId = " + drNotificationsDistinctSupportGroup["GroupId"]);
                    DataRow[] drArrayNotifications = dtNotifications.Select("GroupId = " + drNotificationsDistinctSupportGroup["GroupId"]);
                   var drArrayNewNotifications= drArrayNotifications.Except(drArrayPreviousNotifications);
                   //var drArrayNewNotifications = drArrayNotifications.Except(drArrayPreviousNotifications, new NotificationComparer());
                //
                   if (drArrayNewNotifications.Count() > 0)
                   {
                       // it will always be a single element list
                       List<string> g = new List<string>();
                       g.Add(drNotificationsDistinctSupportGroup["GroupId"].ToString());
                       // notify every new tickets to the assigned support group
                       foreach (DataRow tmpDR in drArrayNewNotifications)
                       {
                           _notificationHub.Clients.Groups(g).globalNotification("New ticket in Inbox" /*+ (Convert.IsDBNull(tmpDR["SiteName"]) ? "." : " from site " + tmpDR["SiteName"].ToString() + ".")*/);
                            //   log.Debug("Successfully notified ticket " + tmpDR["Id"].ToString() + " to group " + g.First() + ".");
                       }
                   }
                }
                // clear status
                dtPreviousNotifications = dtNotifications;
            }
            catch (Exception ex)
            {
                //log.Error("Unable to send notifications.", ex);
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        public void Stop(bool immediate)
        {
            _timer.Dispose();

            HostingEnvironment.UnregisterObject(this);
        }

    }

    class NotificationComparer : IEqualityComparer<DataRow[]>
    {
        public bool Equals(DataRow[] x, DataRow[] y)
        {
            //return x.Numf == y.Numf;
            return x == y;
        }
        public int GetHashCode(DataRow[] codeh)
        {
            return 0;
        }
    }
}