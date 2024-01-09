using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLSellerGroupDB
    {
        public static List<ADSLSellerGroup> SearchADSLSellerGroups(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerGroups
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)))
                    .OrderBy(t => t.Title)
                    .ToList();
            }
        }

        public static ADSLSellerGroup GetADSLSellerGroupByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerGroups
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLSellerGroupsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerGroups
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<ADSLSellerAgent> SearchADSLSellerAgents(
            List<int> availableCityIDs,
            List<int> cityIDs,
            List<int> groupIDs,
            string title,
            string telephone,
            string mobile,
            string address,
            bool? isActive)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgents
                     .Where(t => (availableCityIDs.Contains((int)t.CityID)) &&
                                 (cityIDs.Count == 0 || cityIDs.Contains((int)t.CityID)) &&
                                 (groupIDs.Count == 0 || groupIDs.Contains(t.GroupID)) &&
                                 (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                 (string.IsNullOrWhiteSpace(telephone) || t.TelephoneNo.Contains(telephone)) &&
                                 (string.IsNullOrWhiteSpace(mobile) || t.MobileNo.Contains(mobile)) &&
                                 (string.IsNullOrWhiteSpace(address) || t.Address.Contains(address)) &&
                                 (!isActive.HasValue || isActive == t.IsActive))
                      .OrderBy(t => t.Title).ToList();
            }
        }

        public static ADSLSellerAgent GetADSLSellerAgentByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgents
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static ADSLSellerAgentUser GetADSLSellerAgentUserByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static int GetADSLSellerAgentIDByUserID(int userID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLSellerAgentUser sellerAgentUser = context.ADSLSellerAgentUsers.Where(t => t.ID == userID).SingleOrDefault();

                if (sellerAgentUser != null)
                    return sellerAgentUser.SellerAgentID;
                else
                    return 0;
            }
        }

        public static List<CheckableItem> GetADSLSellerAgentCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgents
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLSellerAgentCheckablebyCityID(int cityID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgents.Where(t => t.CityID == cityID)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<CheckableItem> GetADSLSellerAgentCheckablebyCityIDs(List<int> cityIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgents.Where(t =>
                    (cityIDs.Count == 0 || cityIDs.Contains((int)t.CityID)))
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static ADSLSellerAgentUserInfo GetSellerAgentUserByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers
                    .Where(t => t.ID == id)
                    .Select(t => new ADSLSellerAgentUserInfo
                    {
                        ID = t.ID,
                        SellerAgentID = t.SellerAgentID,
                        SellerAgentTitle = t.ADSLSellerAgent.Title,
                        Fullname = t.User.FirstName + t.User.LastName,
                        Username = t.User.UserName,
                        Password = t.Password,
                        Email = t.Email,
                        IsEnable = t.IsEnable,
                        IsAdmin = (t.IsAdmin != null) ? (bool)t.IsAdmin : false
                    })
                    .SingleOrDefault();
            }
        }

        public static void SaveSellerAgentUser(ADSLSellerAgentUser sellerUser, User user, List<int> selectedCenterIDs)
        {
            if (sellerUser.ID != 0)
            {
                sellerUser.Detach();
                DB.Save(sellerUser, false);

                user.ID = sellerUser.ID;
                user.Detach();
                DB.Save(user, false);

                using (MainDataContext context = new MainDataContext())
                {
                    context.ExecuteCommand("DELETE FROM UserCenter WHERE UserID = {0}", user.ID);

                    List<UserCenter> userCenters = new List<UserCenter>();
                    foreach (int centerID in selectedCenterIDs)
                    {
                        userCenters.Add(new UserCenter
                        {
                            UserID = user.ID,
                            CenterID = centerID
                        });
                    }
                    context.UserCenters.InsertAllOnSubmit(userCenters);
                    context.SubmitChanges();

                    Folder.User folderUser1 = SecurityDB.GetOrCreateFolderUser(user, sellerUser.Password, true);
                }

                return;
            }
            else
            {
                user.Detach();
                DB.Save(user);

                sellerUser.ID = user.ID;
                sellerUser.Detach();
                DB.Save(sellerUser, true);
            }

            Folder.User folderUser = SecurityDB.GetOrCreateFolderUser(user, sellerUser.Password, true);

            using (MainDataContext context = new MainDataContext())
            {
                context.ExecuteCommand("DELETE FROM UserCenter WHERE UserID = {0}", user.ID);

                List<UserCenter> userCenters = new List<UserCenter>();
                foreach (int centerID in selectedCenterIDs)
                {
                    userCenters.Add(new UserCenter
                    {
                        UserID = user.ID,
                        CenterID = centerID
                    });
                }
                context.UserCenters.InsertAllOnSubmit(userCenters);
                context.SubmitChanges();
            }

            Folder.Role folderRole = SecurityDB.GetOrCreateFolderRole(DB.SearchByPropertyName<Role>("ID", user.RoleID).SingleOrDefault().Name);

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

        public static List<ADSLSellerAgentUserInfo> SearchSellerAgentUserbySellerAgentID(int sellerAgentId, string fullName, string userName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers
                    .Where(t => (t.SellerAgentID == sellerAgentId) &&
                                (string.IsNullOrWhiteSpace(fullName) || t.User.FirstName.Contains(fullName) || t.User.LastName.Contains(fullName)) &&
                                (string.IsNullOrWhiteSpace(userName) || t.User.UserName.Contains(userName)))
                    .OrderBy(t => t.ID)
                    .Select(t => new ADSLSellerAgentUserInfo
                    {
                        ID = t.ID,
                        SellerAgentID = t.SellerAgentID,
                        SellerAgentTitle = t.ADSLSellerAgent.Title,
                        Fullname = t.User.FirstName + t.User.LastName,
                        Username = t.User.UserName,
                        Password = t.Password,
                        Email = t.Email,
                        IsEnable = t.IsEnable,
                        IsAdmin = (t.IsAdmin != null) ? (bool)t.IsAdmin : false,
                        CreditCash = t.CreditCash.ToString(),
                        CreditCashUse = t.CreditCashUse.ToString(),
                        CreditCashRemain = t.CreditCashRemain.ToString()
                    })
                    .ToList();
            }
        }

        public static List<ADSLSellerAgentUserInfo> SearchSellerAgentUser(List<int> sellerAgentIDs, string fullName, string userName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers
                    .Where(t => (sellerAgentIDs.Count == 0 || sellerAgentIDs.Contains(t.SellerAgentID)) &&
                                (string.IsNullOrWhiteSpace(fullName) || t.User.FirstName.Contains(fullName) || t.User.LastName.Contains(fullName)) &&
                                (string.IsNullOrWhiteSpace(userName) || t.User.UserName.Contains(userName)))
                    .OrderBy(t => t.ID)
                    .Select(t => new ADSLSellerAgentUserInfo
                    {
                        ID = t.ID,
                        SellerAgentID = t.SellerAgentID,
                        SellerAgentTitle = t.ADSLSellerAgent.Title,
                        Fullname = t.User.FirstName + t.User.LastName,
                        Username = t.User.UserName,
                        Password = t.Password,
                        Email = t.Email,
                        IsEnable = t.IsEnable,
                        IsAdmin = (t.IsAdmin != null) ? (bool)t.IsAdmin : false,
                        CreditCash = t.CreditCash.ToString(),
                        CreditCashUse = t.CreditCashUse.ToString(),
                        CreditCashRemain = t.CreditCashRemain.ToString()
                    })
                    .ToList();
            }
        }

        public static List<ADSLSellerAgentUserInfo> SearchSellerAgentUserbyUserID(int userID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers
                    .Where(t => (t.ID == userID))
                    .Select(t => new ADSLSellerAgentUserInfo
                    {
                        ID = t.ID,
                        SellerAgentID = t.SellerAgentID,
                        SellerAgentTitle = t.ADSLSellerAgent.Title,
                        Fullname = t.User.FirstName + t.User.LastName,
                        Username = t.User.UserName,
                        Password = t.Password,
                        Email = t.Email,
                        IsEnable = t.IsEnable,
                        IsAdmin = (t.IsAdmin != null) ? (bool)t.IsAdmin : false,
                        CreditCash = t.CreditCash.ToString(),
                        CreditCashUse = t.CreditCashUse.ToString(),
                        CreditCashRemain = t.CreditCashRemain.ToString()
                    })
                    .ToList();
            }
        }

        public static List<ADSLSellerAgentUserInfo> SearchSellerAgentUserbyCityIDs(List<int> cityIDs, string fullName, string userName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers
                    .Where(t => (cityIDs.Contains((int)t.ADSLSellerAgent.CityID)) &&
                                (string.IsNullOrWhiteSpace(fullName) || t.User.FirstName.Contains(fullName) || t.User.LastName.Contains(fullName)) &&
                                (string.IsNullOrWhiteSpace(userName) || t.User.UserName.Contains(userName)))
                    .Select(t => new ADSLSellerAgentUserInfo
                    {
                        ID = t.ID,
                        SellerAgentID = t.SellerAgentID,
                        SellerAgentTitle = t.ADSLSellerAgent.Title,
                        Fullname = t.User.FirstName + t.User.LastName,
                        Username = t.User.UserName,
                        Password = t.Password,
                        Email = t.Email,
                        IsEnable = t.IsEnable,
                        IsAdmin = (t.IsAdmin != null) ? (bool)t.IsAdmin : false,
                        CreditCash = t.CreditCash.ToString(),
                        CreditCashUse = t.CreditCashUse.ToString(),
                        CreditCashRemain = t.CreditCashRemain.ToString()
                    })
                    .ToList();
            }
        }

        public static long GetADSLSellerAgentCreditRemainbyUserID(int userID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return Convert.ToInt64(context.ADSLSellerAgentUsers.Where(t => t.ID == userID).SingleOrDefault().ADSLSellerAgent.CreditCashRemain);
            }
        }

        public static long GetADSLSellerAgentUserCreditRemainbyUserID(int userID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return Convert.ToInt64(context.ADSLSellerAgentUsers.Where(t => t.ID == userID).SingleOrDefault().CreditCashRemain);
            }
        }

        public static List<int> GetADSLSellerAgentUserIDs()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers.Select(t => t.ID).ToList();
            }
        }

        public static int GetADSLSellerGroupTypebyUserID(int userID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLSellerAgentUser sellerAgentUser = context.ADSLSellerAgentUsers.Where(t => t.ID == userID).SingleOrDefault();

                if (sellerAgentUser != null)
                    return (int)sellerAgentUser.ADSLSellerAgent.ADSLSellerGroup.Type;
                else
                    return 0;
            }
        }

        public static List<CheckableItem> GetADSLSellerAgentUsersCheckableByADSlSellerAgentID(List<int> ADSLSellerAgentIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers.Where(t =>
                    (ADSLSellerAgentIDs.Count == 0 || ADSLSellerAgentIDs.Contains(t.SellerAgentID))).Select(t => new CheckableItem
                    {
                        Name = t.User.FirstName + " " + t.User.LastName,
                        ID = t.ID,
                        IsChecked = false
                    }).ToList();
            }
        }

        public static List<ADSLSellerAgentUser> GetADSLSellerAgentUsersbyAgentID(int agentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUsers.Where(t => t.SellerAgentID == agentID).ToList();
            }
        }

        public static List<CheckableItem> GetADSLSellerAgentCheckablebyCityIDsAndGroupIDs(List<int> cityIDs, List<int> groupIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgents.Where(t =>
                    (cityIDs.Count == 0 || cityIDs.Contains((int)t.CityID))
                    && (groupIDs.Count == 0 || groupIDs.Contains((int)t.ADSLSellerGroup.ID)))
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static bool IsSellerAgentUserPaidbyRequestID(long requestID, int userID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLSellerAgentUserCredit credit = context.ADSLSellerAgentUserCredits.Where(t => t.RequestID == requestID && t.UserID == userID).SingleOrDefault();

                if (credit != null)
                    return false;
                else
                    return true;
            }
        }

        public static List<ADSLSellerAgentUserCreditInfo> SearchADSLSellerAgentUserCredit(int userID, string requestID, string telephoneNo, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUserCredits.Where(t => t.UserID == userID &&
                                                          (string.IsNullOrWhiteSpace(requestID) || t.RequestID.ToString().Contains(requestID)) &&
                                                          (string.IsNullOrWhiteSpace(telephoneNo) || t.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                                          (!fromDate.HasValue || t.InsertDate >= fromDate) &&
                                                          (!toDate.HasValue || t.InsertDate <= toDate))
                                                          .Select(t => new ADSLSellerAgentUserCreditInfo
                                                          {
                                                              RequestID = t.RequestID.ToString(),
                                                              TelephoneNo = t.Request.TelephoneNo.ToString(),
                                                              RequestType = t.Request.RequestType.Title,
                                                              Cost = t.Cost.ToString(),
                                                              Date = Date.GetPersianDate(t.InsertDate, Date.DateStringType.Short)
                                                          }).ToList();
            }
        }

        public static List<ADSLSellerAgentUserRechargeInfo> SearchADSLSellerAgentUserRecharge(int userID, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLSellerAgentUserRecharges.Where(t => t.SellerAgentUserID == userID &&
                                                                  (!fromDate.HasValue || t.InsertDate >= fromDate) &&
                                                                  (!toDate.HasValue || t.InsertDate <= toDate))
                                                                  .Select(t => new ADSLSellerAgentUserRechargeInfo
                                                                  {
                                                                      User = t.User.FirstName + " " + t.User.LastName,
                                                                      Cost = t.Cost.ToString(),
                                                                      Date = Date.GetPersianDate(t.InsertDate, Date.DateStringType.Short)
                                                                  }).ToList();
            }
        }

        public static ADSLSellerAgentUserCredit GetCreditbyRequestID(long requestID)
        {
            using (MainDataContext context =new MainDataContext())
            {
                ADSLSellerAgentUserCredit credit = context.ADSLSellerAgentUserCredits.Where(t => t.RequestID == requestID).SingleOrDefault();

                if (credit != null)
                    return credit;
                else
                    return null;
            }
        }
    }
}
