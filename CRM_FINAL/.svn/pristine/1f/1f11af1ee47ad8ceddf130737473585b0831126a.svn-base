using Enterprise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class SecurityDB
    {
        public const string _FolderParentMenu = "CRM";

        public enum ResourceType : byte
        {
            Container = 1,
            Menu = 2,
            Item = 3
        }

        #region User

        public static User GetUserByID(int UserID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Users
                    .Where(t => t.ID == UserID)
                    .SingleOrDefault();
            }
        }

        public static List<int> GetUserCitiyIDs(int UserID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.UserCenters
                    .Where(t => t.UserID == UserID)
                    .Select(t => t.ID)
                    .ToList();
            }
        }

        public static List<int> GetUserCenterIDs(int UserID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.UserCenters
                    .Where(t => t.UserID == UserID)
                    .Select(t => t.CenterID)
                    .ToList();
            }
        }

        public static bool UserExists(string username)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Users
                    .Where(t => t.UserName == username)
                    .Count() == 1;
            }
        }

        public static User GetUserbyUserName(string userName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                User user = context.Users.Where(t => t.UserName.ToLower() == userName.ToLower()).SingleOrDefault();

                if (user != null)
                    return user;
                else
                    return null;
            }
        }

        public static int GetUsersCount()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Users.Count();
            }
        }

        public static List<User> GetUsersList(bool showSpecialUsers = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Users
                    .Where(t => showSpecialUsers || t.UserName.ToLower() != "system")
                    .ToList();
            }
        }

        public static List<Resource> GetResources()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Resources.ToList();
            }
        }

        public static Resource GetResourceByName(string name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Resources
                    .Where(t => t.Name.ToLower() == name.ToLower())
                    .SingleOrDefault();
            }
        }

        public static bool ResourceExists(string name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Resources
                    .Where(t => t.Name.ToLower() == name.ToLower())
                    .Count() == 1;
            }
        }

        public static void SaveUser(User user, List<int> selectedCenterIDs, string password, string roleName)
        {
            user.Detach();
            DB.Save(user);

            Folder.User folderUser = SecurityDB.GetOrCreateFolderUser(user, password, true);

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

            Folder.Role folderRole = GetOrCreateFolderRole(roleName);

            using (Folder.FolderDataContext context = new Folder.FolderDataContext())
            {
                IEnumerable<Folder.UserRole> folderUserRoles = context.UserRoles.Where(t => t.UserID == folderUser.ID && t.Role.Parent.Name == _FolderParentMenu);
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

        public static bool FolderUserExists(string username)
        {
            using (Folder.FolderDataContext folderContext = new Folder.FolderDataContext())
            {
                return folderContext.Users.Where(t => t.Username == username).Count() == 1;
            }
        }

        public static Folder.User GetOrCreateFolderUser(User user, string password = "", bool forceSave = false)
        {
            Folder.User folderUser;

            using (Folder.FolderDataContext context = new Folder.FolderDataContext())
            {
                folderUser = context.Users.Where(t => t.Username == user.UserName).SingleOrDefault();

                if (folderUser == null)
                {
                    folderUser = new Folder.User();
                    folderUser.ID = Guid.NewGuid();
                    folderUser.Username = user.UserName;
                    folderUser.Password = Folder.Cryptography.Encrypt(password);
                    folderUser.Fullname = user.FirstName + " " + user.LastName;
                    folderUser.IsEnable = true;
                    folderUser.Email = user.Email;

                    folderUser.CreationDate = DB.GetServerDate();
                    context.Users.InsertOnSubmit(folderUser);
                }

                else if (forceSave)
                {
                    if (!string.IsNullOrEmpty(password))
                        folderUser.Password = Folder.Cryptography.Encrypt(password);
                    folderUser.Fullname = user.FirstName + " " + user.LastName;
                    folderUser.IsEnable = true;
                    folderUser.Email = user.Email;
                }

                context.SubmitChanges();

            }

            return folderUser;
        }

        public static Folder.User GetFolderUser(string username)
        {
            using (Folder.FolderDataContext folderContext = new Folder.FolderDataContext())
            {
                return folderContext.Users.Where(t => t.Username == username).SingleOrDefault();
            }
        }

        public static bool WebAuthentication(string username, string password)
        {
            try
            {
                Logger.WriteView("Authenticating user:{0}", username);

                using (Folder.FolderDataContext context = new Folder.FolderDataContext())
                {
                    if (password == null)
                    {
                        var user = context.Users.FirstOrDefault(u => u.Username == username && u.IsEnable);
                        if (user != null)
                            return true;
                        else
                        {
                            Logger.WriteView("User '{0}' not authenticated!", username);
                            return false;
                        }
                    }
                    else
                    {
                        string codedPass = Folder.Cryptography.Encrypt(password);
                        var user = context.Users.FirstOrDefault(u => u.Username == username && (u.Password == null || u.Password == password || u.Password == codedPass) && u.IsEnable);
                        if (user != null)
                            return true;
                        else
                        {
                            Logger.WriteView("User '{0}' not authenticated!", username);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return false;
            }
        }

        #endregion User

        #region Role

        public static Role GetRole(int roleID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Roles
                    .Where(t => t.ID == roleID)
                    .SingleOrDefault();
            }
        }

        public static List<Role> SearchRole(string name, bool showSpecialRoles = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Roles
                    .Where(t => (showSpecialRoles || t.IsVisible)
                            && (string.IsNullOrEmpty(name) || t.Name.Contains(name))
                           )
                    .ToList();
            }
        }

        public static List<RoleInfo> GetRolesInfo()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Roles
                    .Where(t => t.IsVisible)
                    .Select(t => new RoleInfo()
                    {
                        ID = t.ID,
                        Name = t.Name,
                        Description = t.Description,
                        UesrCount = context.Users.Where(u => u.RoleID == t.ID).Count()
                    }
                            )
                    .ToList();
            }
        }

        public static void SaveRole(Role role, List<ResourceCheckable> selectedResources, /*List<int> selectedSubscriberTypeIDs,*/ List<int> selectedReportTemplateIDs, /*,List<int> selectedCallDetailTypeIDs*/ List<int> selectedRequestStepIDs)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (MainDataContext context = new MainDataContext())
                {
                    role.IsVisible = true;

                    if (role.ID > 0)
                    {
                        role.Detach();
                        context.Roles.Attach(role);
                        context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, role);
                    }
                    else
                    {
                        context.Roles.InsertOnSubmit(role);
                    }

                    context.SubmitChanges();

                    context.ExecuteCommand("DELETE FROM RoleResource WHERE RoleID = {0}", role.ID);
                    //context.ExecuteCommand("DELETE FROM RoleSubscriberType WHERE RoleID = {0}", role.ID);
                    context.ExecuteCommand("DELETE FROM RoleReportTemplate WHERE RoleID = {0}", role.ID);
                    //context.ExecuteCommand("DELETE FROM RoleCallDetailType WHERE RoleID = {0}", role.ID);
                    context.ExecuteCommand("DELETE FROM RoleRequestStep WHERE RoleID = {0}", role.ID);

                    #region Resources

                    List<RoleResource> roleResources = new List<RoleResource>();
                    foreach (ResourceCheckable resource in selectedResources)
                    {
                        roleResources.Add(new RoleResource
                        {
                            RoleID = role.ID,
                            ResourceID = resource.ID,
                            IsEditable = resource.IsEditable
                        });
                    }
                    context.RoleResources.InsertAllOnSubmit(roleResources);

                    #endregion


                    //#region SubscriberType

                    //List<RoleSubscriberType> roleSubscriberTypes = new List<RoleSubscriberType>();
                    //foreach (int actionID in selectedSubscriberTypeIDs)
                    //{
                    //    roleSubscriberTypes.Add(new RoleSubscriberType
                    //    {
                    //        RoleID = role.ID,
                    //        SubscriberTypeID = actionID
                    //    });
                    //}
                    //context.RoleSubscriberTypes.InsertAllOnSubmit(roleSubscriberTypes);

                    //#endregion

                    #region ReportTemplate

                    List<RoleReportTemplate> roleReportTemplates = new List<RoleReportTemplate>();
                    foreach (int actionID in selectedReportTemplateIDs)
                    {
                        roleReportTemplates.Add(new RoleReportTemplate
                        {
                            RoleID = role.ID,
                            ReportTemplateID = actionID
                        });
                    }
                    context.RoleReportTemplates.InsertAllOnSubmit(roleReportTemplates);

                    #endregion

                    //#region CallDetailType

                    //List<RoleCallDetailType> roleCallDetailTypes = new List<RoleCallDetailType>();
                    //foreach (int actionID in selectedCallDetailTypeIDs)
                    //{
                    //    roleCallDetailTypes.Add(new RoleCallDetailType
                    //    {
                    //        RoleID = role.ID,
                    //        CallDetailTypeID = actionID
                    //    });
                    //}
                    //context.RoleCallDetailTypes.InsertAllOnSubmit(roleCallDetailTypes);

                    //#endregion


                    #region ReportTemplate

                    List<RoleRequestStep> roleRequestStep = new List<RoleRequestStep>();
                    foreach (int actionID in selectedRequestStepIDs)
                    {
                        roleRequestStep.Add(new RoleRequestStep
                        {
                            RoleID = role.ID,
                            RequestStepID = actionID
                        });
                    }
                    context.RoleRequestSteps.InsertAllOnSubmit(roleRequestStep);

                    #endregion

                    context.SubmitChanges();
                }

                scope.Complete();
            }
        }

        public static void SaveRole(Role role, List<int> selectedWebServicesId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (MainDataContext context = new MainDataContext())
                {
                    role.IsVisible = true;

                    if (role.ID > 0)
                    {
                        role.Detach();
                        context.Roles.Attach(role);
                        context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, role);
                    }
                    else
                    {
                        context.Roles.InsertOnSubmit(role);
                    }
                    context.SubmitChanges();

                    context.ExecuteCommand("DELETE FROM RoleWebService WHERE RoleID = {0}", role.ID);

                    List<RoleWebService> roleWebServiceList = new List<RoleWebService>();
                    foreach (int webServiceId in selectedWebServicesId)
                    {
                        roleWebServiceList.Add(new RoleWebService
                        {
                            RoleID = role.ID,
                            WebServiceID = webServiceId
                        });
                    }

                    context.RoleWebServices.InsertAllOnSubmit(roleWebServiceList);
                    context.SubmitChanges();
                }

                scope.Complete();
            }
        }

        public static Folder.Role GetOrCreateFolderRole(string roleName)
        {
            using (Folder.FolderDataContext context = new Folder.FolderDataContext())
            {
                Folder.Role folderParentRole = context.Roles.Where(t => t.Name == _FolderParentMenu && t.ParentID == Guid.Empty).FirstOrDefault();
                if (folderParentRole == null)
                {
                    folderParentRole = new Folder.Role
                    {
                        ID = Guid.NewGuid(),
                        Name = _FolderParentMenu,
                        ParentID = Guid.Empty,
                        Type = 1
                    };
                    context.Roles.InsertOnSubmit(folderParentRole);
                    context.SubmitChanges();
                }

                Folder.Role folderRole = context.Roles.Where(t => t.Name == roleName && t.ParentID == folderParentRole.ID).FirstOrDefault();
                if (folderRole == null)
                {
                    folderRole = new Folder.Role
                    {
                        ID = Guid.NewGuid(),
                        Name = roleName,
                        ParentID = folderParentRole.ID,
                        Type = 1
                    };
                    context.Roles.InsertOnSubmit(folderRole);
                    context.SubmitChanges();
                }

                return folderRole;
            }

        }

        public static void CreateFolderRole(List<FolderMenu> folderMenuList)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (Folder.FolderDataContext context = new Folder.FolderDataContext())
                {
                    Folder.Role folderParentRole = context.Roles.Where(t => t.Name == _FolderParentMenu && t.ParentID == Guid.Empty).FirstOrDefault();
                    if (folderParentRole == null)
                    {
                        folderParentRole = new Folder.Role
                        {
                            ID = Guid.NewGuid(),
                            Name = _FolderParentMenu,
                            ParentID = Guid.Empty,
                            Type = 1
                        };
                        context.Roles.InsertOnSubmit(folderParentRole);
                        context.SubmitChanges();
                    }

                    List<Folder.Role> menuRoles = context.Roles.Where(t => t.Type == 4).ToList();
                    context.Roles.DeleteAllOnSubmit(menuRoles);
                    context.SubmitChanges();

                    Folder.Role folderRole;
                    foreach (FolderMenu menu in folderMenuList)
                    {
                        folderRole = new Folder.Role
                        {
                            ID = Guid.NewGuid(),
                            Name = menu.ClassName + ".View",
                            ParentID = folderParentRole.ID,
                            Type = 4
                        };
                        context.Roles.InsertOnSubmit(folderRole);

                        folderRole = new Folder.Role
                        {
                            ID = Guid.NewGuid(),
                            Name = menu.ClassName + ".Edit",
                            ParentID = folderParentRole.ID,
                            Type = 4
                        };
                        context.Roles.InsertOnSubmit(folderRole);
                    }
                    context.SubmitChanges();

                }
                scope.Complete();
            }

        }


        public static void SaveFolderMenuRoles(Role role, List<string> selectedMenuClassNames)
        {
            Folder.Role folderRole = GetOrCreateFolderRole(role.Name);
            using (Folder.FolderDataContext context = new Folder.FolderDataContext())
            {
                List<Folder.RoleMember> roleMembersToBeDeleted = context.RoleMembers.Where(t => t.ParentID == folderRole.ID && t.Role.Type == 4).ToList();
                context.RoleMembers.DeleteAllOnSubmit(roleMembersToBeDeleted);

                List<Guid> roleIDsToBeAdded = context.Roles.Where(t => t.Type == 4 && selectedMenuClassNames.Contains(t.Name.Substring(0, t.Name.LastIndexOf('.')))).Select(t => t.ID).ToList();

                List<Folder.RoleMember> roleMembersToBeAdded = new List<Folder.RoleMember>();

                foreach (Guid id in roleIDsToBeAdded)
                {
                    roleMembersToBeAdded.Add(new Folder.RoleMember
                    {
                        ParentID = folderRole.ID,
                        RoleID = id
                    });
                }

                context.RoleMembers.InsertAllOnSubmit(roleMembersToBeAdded);

                context.SubmitChanges();
            }
        }

        //public static List<CheckableItem> GetSubscriberTypesCheckable(int roleId)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.SubscriberTypes
        //                  .Select(t => new CheckableItem()
        //                  {
        //                      ID = t.ID,
        //                      IsChecked = context.RoleSubscriberTypes.Where(r => r.SubscriberTypeID == t.ID).Select(r => r.RoleID).Contains(roleId),
        //                      Name = t.Name
        //                  }
        //                          )
        //                  .ToList();
        //    }
        //}


        //public static List<CheckableItem> GetCallDetailTypesCheckable(int roleId)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.CallDetailTypes
        //                  .Select(t => new CheckableItem()
        //                  {
        //                      ID = t.ID,
        //                      IsChecked = context.RoleCallDetailTypes.Where(r => r.CallDetailTypeID == t.ID).Select(r => r.RoleID).Contains(roleId),
        //                      Name = t.Name
        //                  }
        //                          )
        //                  .ToList();
        //    }
        //}

        public static List<CheckableItem> GetReportTemplatesCheckable(int roleId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ReportTemplates
                              .OrderBy(t => t.Category)
                              .ThenBy(t => t.Title)
                              .Select(t => new CheckableItem()
                                            {
                                                ID = t.ID,
                                                IsChecked = context.RoleReportTemplates.Where(r => r.ReportTemplateID == t.ID).Select(r => r.RoleID).Contains(roleId),
                                                Name = t.Category + " - " + t.Title
                                            }
                                      )
                              .ToList();
            }
        }

        public static List<CheckableItem> GetRequestStepCheckable(int roleId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RequestSteps
                          .OrderBy(t => t.RequestType.Title)
                          .Select(t => new CheckableItem()
                          {
                              ID = t.ID,
                              IsChecked = context.RoleRequestSteps.Where(r => r.RequestStepID == t.ID).Select(r => r.RoleID).Contains(roleId),
                              Name = t.RequestType.Title + " : " + t.StepTitle
                          })
                          .ToList();
            }
        }

        public static List<Folder.UserRole> GetUserRolesByFolderUser(Folder.User folderUser)
        {
            using (Folder.FolderDataContext context = new Folder.FolderDataContext())
            {
                //return context.UserRoles.Where(t => t.UserID == folderUser.ID && (t.Role.Parent.Name == _FolderParentMenu)).ToList();
                //return context.UserRoles.Where(t => t.UserID == folderUser.ID && (t.Role.Parent.Name == _FolderParentMenu || t.Role.ParentID == "00000000-0000-0000-0000-000000000000")).ToList();
                return context.UserRoles.Where(t => t.UserID == folderUser.ID).ToList();
            }
        }

        #endregion Role

        #region Resources

        public static List<Resource> GetRoleResources(int roleID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RoleResources
                    .Where(t => t.RoleID == roleID)
                    .Select(t => t.Resource)
                    .ToList();
            }
        }

        public static List<int> GetRoleResourcesIDs(int roleID, bool isEditable = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RoleResources
                    .Where(t => t.RoleID == roleID && (isEditable == false || t.IsEditable == isEditable))
                    .Select(t => t.ResourceID)
                    .ToList();
            }
        }

        public static List<ResourceCheckable> GetHierarchyResources(int roleID, bool showRoot = false)
        {
            List<int> selectedResources = new List<int>();
            List<int> editableResources = new List<int>();

            if (roleID > 0)
            {
                selectedResources = GetRoleResourcesIDs(roleID);
                editableResources = GetRoleResourcesIDs(roleID, true);
            }

            return GetChildResources((showRoot) ? null : (int?)1, selectedResources, editableResources);
        }

        private static List<ResourceCheckable> GetChildResources(int? nodeID, List<int> selectedResources, List<int> editableResources)
        {
            using (var context = new MainDataContext())
            {
                return context.Resources
                    .Where(t => t.ParentID == nodeID || (nodeID == null && !t.ParentID.HasValue))
                    .Select(t => new ResourceCheckable()
                    {
                        ID = t.ID,
                        IsChecked = selectedResources.Contains(t.ID),
                        Name = t.Name,
                        IsEditable = editableResources.Contains(t.ID),
                        Description = t.Description,
                        ChildResource = GetChildResources(t.ID, selectedResources, editableResources)
                    }
                            )
                            .ToList();
            }
        }
        public static List<int> GetAllResourcesIDs()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Resources
                        .Select(t => t.ID)
                        .ToList();
            }
        }

        #endregion Resources

        #region Prerefrences

        //public void SaveUserPreferences(Schema.Preferences preferences, Guid UserID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        context.ExecuteCommand("UPDATE [User] SET Preferences = {0} WHERE ID = {1}", Common.Xml.Serialize<Schema.Preferences>(preferences), UserID);
        //    }
        //}

        //public Schema.Preferences GetUserPreferences(Guid UserID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        string preferences = context.Users
        //            .Where(t => t.ID == UserID)
        //            .Select(t => t.Preferences)
        //            .SingleOrDefault();

        //        if (string.IsNullOrEmpty(preferences)) return null;

        //        return Common.Xml.Deserialize<Schema.Preferences>(preferences);
        //    }
        //}

        #endregion Prerefrences

        #region Menu

        public static List<FolderMenu> GetMenuCheckable(int roleID)
        {
            List<string> currentRoleClassNames = new List<string>();

            if (roleID != 0)
            {
                Role role = GetRole(roleID);

                Folder.Role folderRole = GetOrCreateFolderRole(role.Name);

                using (Folder.FolderDataContext context = new Folder.FolderDataContext())
                {
                    var query = context.RoleMembers.Where(t => t.ParentID == folderRole.ID && t.Role.Type == 4).Select(t => t.Role.Name.Substring(0, t.Role.Name.LastIndexOf('.')));
                    currentRoleClassNames = query.ToList();
                }
            }

            List<FolderMenu> result = new List<FolderMenu>();

            List<List<Folder.Schema.MenuMenuItem>> folderConsolePluginMenuSelect = Folder.Console.PluginMenu.Select(t => t.Value.MenuItems).ToList();

            foreach (List<Folder.Schema.MenuMenuItem> menuItems in folderConsolePluginMenuSelect)
            {
                foreach (Folder.Schema.MenuMenuItem item in menuItems)
                {
                    result.Add(new FolderMenu
                    {
                        Name = string.Format("{0} > {1} ", string.IsNullOrWhiteSpace(item.Tab) ? "سیستم" : item.Tab.Trim(), item.Header.Trim()),
                        ClassName = item.Class.Trim(),
                        IsChecked = currentRoleClassNames.Contains(item.Class)
                    });
                }
            }

            return result.OrderBy(t => t.Name).ToList();
        }


        public static List<FolderMenuInfo> GetFolderMenuInfoesCheckableForWeb(int roleID)
        {
            List<string> currentRoleClassNames = new List<string>();

            if (roleID != 0)
            {
                Role role = GetRole(roleID);

                Folder.Role folderRole = GetOrCreateFolderRole(role.Name);

                using (Folder.FolderDataContext context = new Folder.FolderDataContext())
                {
                    var query = context.RoleMembers.Where(t => t.ParentID == folderRole.ID && t.Role.Type == 4).Select(t => t.Role.Name.Substring(0, t.Role.Name.LastIndexOf('.')));
                    currentRoleClassNames = query.ToList();
                }
            }

            List<FolderMenuInfo> result = new List<FolderMenuInfo>();

            //FillFolderConsolePluginMenu();
            /**/
            Folder.Console.PluginMenu.Clear();
            Folder.Schema.Menu mainMenu;
            // Folder.Schema.Menu mainMenu = LoadEmbededMenu(Assembly.GetExecutingAssembly(), "Menu.xml");

            //System.Reflection.Assembly.GetCallingAssembly();
            // Stream stream = GetType().Assembly.GetManifestResourceStream("CRM.Website.Menu.xml");
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            var stream = System.Reflection.Assembly.GetCallingAssembly().GetManifestResourceStream("CRM.Website.Menu.xml");
            if (stream == null)
                return null;

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            if (buffer == null || buffer.Length == 0)
                return null;

            int rootStartOffset = 0;
            for (; rootStartOffset < buffer.Length; rootStartOffset++)
                if (buffer[rootStartOffset] == 60)
                    break;

            string xml = System.Text.Encoding.UTF8.GetString(buffer, rootStartOffset, buffer.Length - rootStartOffset);
            mainMenu = Folder.Schema.MenuUtility.Deserialize<Folder.Schema.Menu>(xml);

            if (mainMenu != null)
                Folder.Console.PluginMenu.Add(Assembly.GetExecutingAssembly().GetName().Name, mainMenu);

            /**/

            List<List<Folder.Schema.MenuMenuItem>> folderConsolePluginMenuSelect = Folder.Console.PluginMenu.Select(t => t.Value.MenuItems).ToList();

            foreach (List<Folder.Schema.MenuMenuItem> menuItems in folderConsolePluginMenuSelect)
            {
                foreach (Folder.Schema.MenuMenuItem item in menuItems)
                {
                    result.Add(new FolderMenuInfo
                    {
                        //Name = string.Format("{0} > {1} ", string.IsNullOrWhiteSpace(item.Tab) ? "سیستم" : item.Tab.Trim(), item.Header.Trim()),
                        Name = string.IsNullOrWhiteSpace(item.Tab) ? "سیستم" : item.Tab.Trim(),
                        ClassName = item.Class.Trim(),
                        GroupName = item.Group.Trim(),
                        Header = item.Header,
                        Icon = item.Icon,
                        IsChecked = currentRoleClassNames.Contains(item.Class)
                    });
                }
            }

            return result.OrderBy(t => t.Name).ToList();
        }
        #endregion

        //TODO:rad 13950206
        public static UserInfo GetUserInfoByFolderUsernameAndPassword(string userName, string password, string folderConnectionString)
        {
            string encryptedPassword = Folder.Cryptography.Encrypt(password.Trim());
            bool isAuthenticatedUser = false;
            UserInfo result = null;

            using (Folder.FolderDataContext folderContext = new Folder.FolderDataContext())
            {
                //folderContext.Connection.ConnectionString = Data.Properties.Settings.Default.FolderConnectionString;
                folderContext.Connection.ConnectionString = folderConnectionString;
                //Only following line is used for username  pendar .
                // isAuthenticatedUser = folderContext.Users.Any(t => t.Username == userName && t.Password == password);
                isAuthenticatedUser = folderContext.Users.Any(t => t.Username == userName && t.Password == encryptedPassword);
            }

            if (isAuthenticatedUser)
            {
                Logger.WriteInfo("Username : {0} - Password : {1} is authenticated for web service", userName, password);

                result = new UserInfo();
                using (MainDataContext context = new MainDataContext())
                {
                    var query = context.Users
                                       .Where(u => u.UserName == userName.ToLower())
                                       .Select(u => new UserInfo
                                                    {
                                                        ID = u.ID,
                                                        UserName = u.UserName,
                                                        FullName = u.FirstName + " " + u.LastName,
                                                        RoleID = u.RoleID
                                                    }
                                              )
                                       .AsQueryable();
                    result = query.SingleOrDefault();
                }
            }

            return result;
        }

        //TODO:rad 13950206
        public static List<string> GetWebServiceMethodsNameByRoleId(int roleId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<string> result = new List<string>();

                result = context.RoleWebServices
                                .Where(rs => rs.RoleID == roleId)
                                .Select(rs => rs.WebService.Name)
                                .ToList();

                return result;
            }
        }
    }
}
