using CaseManagement.Administration.Entities;
using CaseManagement.Case.Entities;
using Microsoft.AspNet.SignalR;
using Serenity;
using Serenity.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CaseManagement.Notification.Hubs
{

   
    public class NotificationHub :Hub
    {
      
        public override Task OnConnected()
        {
            
            if (Authorization.IsLoggedIn)
            {
              /*  var fieldsUserSupportGroup = UserRow.Fields;
              List<int> groupIDs = null;
              using (var connection = SqlConnections.NewFor<UserSupportGroupRow>())
              {
                  //userSupportGroup = connection.List<UserSupportGroupRow>(new Criteria(fieldsUserSupportGroup.UserId) == Authorization.UserDefinition.Id).SingleOrDefault();
                  groupIDs = connection.List<UserSupportGroupRow>().Where(t => t.UserId == Convert.ToInt32(Authorization.UserDefinition.Id)).Select(t => (int)t.GroupId).ToList();
              }
                
              foreach (int currentGroupID in groupIDs)
              {
                Groups.Add(Context.ConnectionId, currentGroupID.ToString());
              }
                */
            }
            return base.OnConnected();
        }

        public void AddToRoom(string roomName)
        {
             Groups.Add(Context.ConnectionId , roomName);
        }

        public void RemoveFromRoom(string roomName)
        {
             Groups.Remove(Context.ConnectionId , roomName);
        }

        public void Send(string name, string message,string group )
        {
            // Call the addNewMessageToPage method to update clients.


            string DB_ImagePath;
            UserRow User = null;
            string DB_ProvinceName;
            using (var connection = SqlConnections.NewFor<UserRow>())
            {
                
                User = connection.List<UserRow>().Where(t => t.UserId == Int32.Parse(Authorization.UserDefinition.Id)).SingleOrDefault();
                DB_ImagePath = User.ImagePath;
            }

            using (var connection = SqlConnections.NewFor<ProvinceRow>())
            {
                ProvinceRow Province = null;
                if (User.ProvinceId != null)
                {
                    Province = connection.List<ProvinceRow>().Where(t => t.Id == User.ProvinceId).SingleOrDefault();
                    DB_ProvinceName = Province.Name;
                }
                else
                {
                    //Province = connection.List<ProvinceRow>().Where(t => t.Id == User.ProvinceId).SingleOrDefault();
                    DB_ProvinceName = " ";
                }
            }

            Clients.Group(group).addNewMessageToPage(name, message, DB_ImagePath, DB_ProvinceName);

            NotificationRow Notification = new NotificationRow();

            Notification.UserId = int.Parse(Authorization.UserId);
            Notification.Message = message;
            Notification.GroupId = int.Parse(group);
            Notification.InsertDate = DateTime.Now;
            //Notification.InsertDate

            using (var connection = SqlConnections.NewFor<NotificationRow>())
            {
                // retrieve the support groups to which the user belongs
                connection.Insert<NotificationRow>(Notification);
            }
           
        }
            
    }
}





