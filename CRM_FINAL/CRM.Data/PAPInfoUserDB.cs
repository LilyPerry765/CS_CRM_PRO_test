using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Transactions;

namespace CRM.Data
{
    public static class PAPInfoUserDB
    {
        public static List<PAPInfoUserInfo> SearchPAPInfoUserbyPAPID(int papInfoId, List<int> cityIDs, string fullName, string userName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoUsers
                    .Where(t => (t.PAPInfoID == papInfoId) &&
                                (cityIDs.Count == 0 || cityIDs.Contains(t.CityID)) &&
                                (string.IsNullOrWhiteSpace(fullName) || t.User.FirstName.Contains(fullName)|| t.User.LastName.Contains(fullName)) &&
                                (string.IsNullOrWhiteSpace(userName) || t.User.UserName.Contains(userName)))
                    .OrderBy(t => t.ID)
                    .Select(t => new PAPInfoUserInfo
                    {
                        ID = t.ID,
                        PAPInfoID = t.PAPInfoID,
                        PAPInfo = t.PAPInfo.Title,
                        CityID = t.CityID,
                        CityName = t.City.Name,
                        Fullname = t.User.FirstName+ t.User.LastName,
                        Username = t.User.UserName,
                        Password = t.Password,
                        Email = t.Email,
                        InstallRequestNo = t.InstallRequestNo,
                        DischargeRequestNo = t.DischargeRequestNo,
                        ExchangeRequestNo=t.ExchangeRequestNo,
                        FeasibilityNo = t.FeasibilityNo,
                        IsEnable = t.IsEnable
                    })
                    .ToList();
            }
        }

        public static List<PAPInfoUserInfo> SearchPAPInfoUser(List<int> papInfoIDs, List<int> cityIDs, string fullName, string userName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoUsers
                    .Where(t => (papInfoIDs.Count == 0 || papInfoIDs.Contains(t.PAPInfoID)) &&
                                (cityIDs.Count == 0 || cityIDs.Contains(t.CityID)) &&
                                (string.IsNullOrWhiteSpace(fullName) || t.User.FirstName.Contains(fullName) || t.User.LastName.Contains(fullName)) &&
                                (string.IsNullOrWhiteSpace(userName) || t.User.UserName.Contains(userName)))
                    .OrderBy(t => t.ID)
                    .Select(t => new PAPInfoUserInfo
                    {
                        ID = t.ID,
                        PAPInfoID = t.PAPInfoID,
                        PAPInfo = t.PAPInfo.Title,
                        CityID = t.CityID,
                        CityName = t.City.Name,
                        Fullname = t.User.FirstName + t.User.LastName,
                        Username = t.User.UserName,
                        Password = t.Password,
                        Email = t.Email,
                        InstallRequestNo = t.InstallRequestNo,
                        DischargeRequestNo = t.DischargeRequestNo,
                        ExchangeRequestNo = t.ExchangeRequestNo,
                        FeasibilityNo = t.FeasibilityNo,
                        IsEnable = t.IsEnable
                    })
                    .ToList();
            }
        }

        public static PAPInfoUserInfo GetPAPInfoUserByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoUsers
                    .Where(t => t.ID == id)
                    .Select(t => new PAPInfoUserInfo
                    {
                        ID = t.ID,
                        PAPInfoID = t.PAPInfoID,
                        PAPInfo = t.PAPInfo.Title,
                        CityID = t.CityID,
                        CityName = t.City.Name,
                        Fullname = t.User.FirstName + t.User.LastName,
                        Username = t.User.UserName,
                        Password = t.Password,
                        Email = t.Email,
                        InstallRequestNo = t.InstallRequestNo,
                        DischargeRequestNo = t.DischargeRequestNo,
                        ExchangeRequestNo=t.ExchangeRequestNo,
                        FeasibilityNo = t.FeasibilityNo,
                        IsEnable = t.IsEnable
                    })
                    .SingleOrDefault();
            }
        }

        public static PAPInfoUser GetPAPUserByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoUsers
                    .Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static void SavePAPInfoUser(PAPInfoUser papUser, User user)
        {
            if (papUser.ID != 0)
            {
                papUser.Detach();
                DB.Save(papUser, false);

                user.ID = papUser.ID;
                user.Detach();
                DB.Save(user, false);

                return;
            }
            else
            {
                user.Detach();
                DB.Save(user);

                papUser.ID = user.ID;
                papUser.Detach();
                DB.Save(papUser, true);
            }

            Folder.User folderUser = SecurityDB.GetOrCreateFolderUser(user, papUser.Password, true);
            
            Folder.Role folderRole = SecurityDB.GetOrCreateFolderRole(RoleDB.GetRoleNameByID(user.RoleID));

            using (Folder.FolderDataContext context = new Folder.FolderDataContext())
            {
                IEnumerable<Folder.UserRole> folderUserRoles = context.UserRoles.Where(t => t.UserID == folderUser.ID && t.Role.Parent.Name == "CRM");
                context.UserRoles.DeleteAllOnSubmit(folderUserRoles);

                Folder.UserRole folderUserRole = new Folder.UserRole
                {
                    UserID = folderUser.ID,
                    RoleID = folderRole.ID,
                    Allow = true
                };
                context.UserRoles.InsertOnSubmit(folderUserRole);
                context.SubmitChanges();
            }
        }

        public static PAPInfoUser GetPAPInfoUserByUserName(string userName, string password)
        {
            using (MainDataContext context = new MainDataContext())
            {
               PAPInfoUser user= context.PAPInfoUsers.Where(t => t.User.UserName == userName && t.Password == password).SingleOrDefault();

               if (user != null)
                   return user;
               else
                   return null;
            }
        }

        
    }
}
