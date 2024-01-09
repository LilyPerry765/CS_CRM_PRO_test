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
using CRM.Data;
using Enterprise;
using System.Xml.Linq;
using CRM.Data.Schema;

namespace CRM.Application.Views
{
    public partial class RoleForm : Local.PopupWindow
    {
        #region Properties

        private int _RoleID = 0;

        private bool _isServiceRole;
        public bool IsServiceRole
        {
            get { return _isServiceRole; }
            set { _isServiceRole = value; }
        }

        private List<ResourceInfo> CurrentResourceInfo
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public RoleForm()
        {
            InitializeComponent();
            Initialize();
        }

        public RoleForm(int id)
            : this()
        {
            _RoleID = id;
        }

        public RoleForm(int id, bool isServiceRole)
            : this()
        {
            _RoleID = id;
            this._isServiceRole = isServiceRole;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            this.CurrentResourceInfo = new List<ResourceInfo>();
        }

        private void LoadData()
        {
            Role role = new Role();

            if (this.IsServiceRole) //بر اساس تصمیم - چنانچه نقش برای وب سرویس ایجاد شده باشد نباید بتواند سایر دسترسی ها را ببیند
            {
                MenusExpander.Visibility = Visibility.Collapsed;
                RequestStepsExpander.Visibility = Visibility.Collapsed;
                ResourcesAndActionsExpander.Visibility = Visibility.Collapsed;
                ReportsExpander.Visibility = Visibility.Collapsed;
                WebServiceListView.ItemsSource = RoleWebServiceDB.GetWebServiceCheckable(_RoleID);
            }
            else
            {
                ReportTemplateBox.ItemsSource = SecurityDB.GetReportTemplatesCheckable(_RoleID);
                RequestStepBox.ItemsSource = SecurityDB.GetRequestStepCheckable(_RoleID);
                ResourceTreeView.ItemsSource = SecurityDB.GetHierarchyResources(_RoleID);
                MenuListBox.ItemsSource = SecurityDB.GetMenuCheckable(_RoleID);

                WebServicesExpander.Visibility = Visibility.Collapsed;
            }

            if (_RoleID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                role = Data.SecurityDB.GetRole(_RoleID);
                this.CurrentResourceInfo = RoleDB.GetResourceInfosByRoleID(role.ID);
                SaveButton.Content = "بروزرسانی";
                if (this.IsServiceRole)
                {
                    Logger.WriteInfo("Role : {0} has service role.", role.Name);
                }
            }
            this.DataContext = role;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                Role role = new Role();
                List<ResourceCheckable> selectedResourcesIds = ResourceTreeView.Items.Cast<ResourceCheckable>().Where(t => t.IsChecked == true).ToList();
                List<int> selectedReportTemplateIds = ReportTemplateBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();
                List<int> selectedRequestStepIds = RequestStepBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();
                List<string> selectedMenuClassNames = MenuListBox.Items.Cast<FolderMenu>().ToList().Where(t => t.IsChecked == true).Select(t => t.ClassName).ToList();

                role = this.DataContext as Role;

                if (_RoleID == 0 && _isServiceRole)
                {
                    role.IsServiceRole = true;
                }
                else if (_RoleID == 0 && !_isServiceRole)
                {
                    role.IsServiceRole = false;
                }

                if (_isServiceRole)
                {
                    List<int> selectedWebServicesId = WebServiceListView.Items
                                                                        .Cast<CheckableItem>()
                                                                        .Where(t => t.IsChecked == true)
                                                                        .Select(t => t.ID)
                                                                        .ToList();
                    SecurityDB.SaveRole(role, selectedWebServicesId);
                }
                else
                {
                    SecurityDB.SaveRole(role, selectedResourcesIds, selectedReportTemplateIds, selectedRequestStepIds);
                    SecurityDB.SaveFolderMenuRoles(role, selectedMenuClassNames);

                    //save log details
                    RoleResourceLogInfo logInfo = new RoleResourceLogInfo();
                    logInfo.RoleId = role.ID;
                    logInfo.RoleName = role.Name;
                    logInfo.UserID = DB.CurrentUser.ID;

                    if (this.CurrentResourceInfo.Count == 0) //آیا نقش جاری دارای دسترسی هایی بوده است یا خیر
                    {
                        logInfo.OldResourceLogInfo = new List<ResourceLogInfo>(); //چنانچه نقش جاری دارای دسترسی نبوده باشد آنگاه باید در لاگ ویرایش نقش مقدار خالی برای مقادیر دسترسی های قدیم ذخیره گردد 
                    }
                    else
                    {
                        //چنانچه نقش جاری دارای دسترسی هایی بوده باشد ، به ازای هر رکورد دسترسی ، رکورد متناظر آن برای لاگ ایجاد و در لیست دسترسی های قدیم ذخیره میگردد
                        foreach (ResourceInfo item in this.CurrentResourceInfo)
                        {
                            logInfo.OldResourceLogInfo.Add(new ResourceLogInfo
                                                            {
                                                                ID = item.ID,
                                                                Name = item.Name,
                                                                Description = item.Description
                                                            }
                                                           );
                        }
                    }

                    //بعد از عملیات ذخیره سازی رکورد نقش ، دسترسی های آن را بازیابی میکنیم
                    List<ResourceInfo> newResourceInfo = RoleDB.GetResourceInfosByRoleID(role.ID);
                    if (newResourceInfo.Count == 0)
                    {
                        logInfo.NewResourceLogInfo = new List<ResourceLogInfo>();
                    }
                    else
                    {
                        foreach (ResourceInfo item in newResourceInfo)
                        {
                            logInfo.NewResourceLogInfo.Add(new ResourceLogInfo
                                                           {
                                                               ID = item.ID,
                                                               Name = item.Name,
                                                               Description = item.Description
                                                           }
                                                          );
                        }
                    }

                    ActionLog actionLog = new ActionLog();
                    actionLog.ActionID = (int)DB.ActionLog.RoleEdit;
                    actionLog.UserName = DB.CurrentUser.UserName;
                    actionLog.Date = DB.GetServerDate();
                    actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<RoleResourceLogInfo>(logInfo, true));
                    DB.Save(actionLog);
                }

                ShowSuccessMessage("اطلاعات ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "Save Role Error");
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion
    }
}
