using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class UserDB
    {
        private static List<Guid> userCenterIDs = new List<Guid>();
        public static List<Guid> UserCenterIDs
        {
            get
            {
                return userCenterIDs;
            }
            set
            {
                userCenterIDs = value;
            }
        }

        public static List<UserInfo> SearchUsers(
            string userName,
            string fullName,
            List<int> roleIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Users
                    .Where(t => ( t.IsDelete == false )&&
                                (string.IsNullOrWhiteSpace(userName) || t.UserName.Contains(userName)) &&
                                (string.IsNullOrWhiteSpace(fullName) || t.FirstName.Contains(fullName) || t.LastName.Contains(fullName)) &&
                                (roleIDs.Count == 0 || roleIDs.Contains(t.RoleID)))
                    .OrderBy(t => t.UserName)
                    .Select(t => new UserInfo
                    {
                        ID = t.ID,
                        UserName = t.UserName,
                        FullName = t.FirstName + " " + t.LastName,
                        Role = t.Role.Name,
                        LastLoginDate = Date.GetPersianDate(t.LastLoginDate, Date.DateStringType.DateTime)
                    })
                    .ToList();
            }
        }

        public static User GetUserByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Users
                    .Where(t => t.ID == id && t.IsDelete == false)
                    .SingleOrDefault();
            }
        }

        public static string GetUserFullName(int? id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                UserInfo user = new UserInfo();
                if (id != null)
                {
                    user = context.Users.Where(t => t.ID == id)
                                    .Select(t => new UserInfo { FullName = t.FirstName + " " + t.LastName }).SingleOrDefault();
                    if (user != null)
                        return user.FullName;
                    else
                        return "";
                }
                else
                    return "";                
            }
        }

        public static List<UserCenter> GetUserCentersByUserId(int uersID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.UserCenters
                    .Where(t => t.UserID == uersID && t.User.IsDelete == false)
                    .ToList();
            }
        }

        public static Folder.User GetUserByUserName(string username)
        {
            using (Folder.FolderDataContext context = new Folder.FolderDataContext())
            {
                return context.Users
                    .Where(t => t.Username.ToLower() == username.ToLower())
                    .SingleOrDefault();
            }
        }

        public static User GetUserbyUserName(string username)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Users.Where(t => t.UserName == username && t.IsDelete == false).SingleOrDefault();
            }
        }

    }
}
