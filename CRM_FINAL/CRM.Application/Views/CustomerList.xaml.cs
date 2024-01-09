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
using System.Data;
using CRM.Application.Codes;
using Enterprise;
using CRM.WebAPI.Models.Shahkar.CustomClasses;
using System.Net.NetworkInformation;
using CRM.Data.ShahkarBussines.Methods;
using CRM.WebAPI.Models.Shahkar.Methods;
using CRM.Data.Schema;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;

namespace CRM.Application.Views
{
    public partial class CustomerList : Local.ExtendedTabWindowBase
    {
        #region Properties
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        #endregion

        #region Constructors

        public CustomerList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PersonTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));
            GenderComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Gender));
            CitiesComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            ShahkarAuthenticationStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ShahkarAuthenticationStatus));
        }

        public void LoadData()
        {
            // Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = default(int);

            long telephoneNo = (!string.IsNullOrEmpty(TelephoneTextBox.Text.Trim())) ? Convert.ToInt64(TelephoneTextBox.Text.Trim()) : -1;
            List<int> citiesId = CitiesComboBox.SelectedIDs;
            List<int> centersId = CentersComboBox.SelectedIDs;
            List<int> personTypes = PersonTypeComboBox.SelectedIDs;
            List<int> genders = GenderComboBox.SelectedIDs;
            string nationalCodeOrRecordNo = NationalCodeOrRecordNoTextBox.Text.Trim();
            string firstNameOrTitle = FirstNameOrTitleTextBox.Text.Trim();
            string lastName = LastNameTextBox.Text.Trim();
            string fatherName = FatherNameTextBox.Text.Trim();
            string birthCertificateID = BirthCertificateIDTextBox.Text.Trim();
            DateTime? birthDateOrRecordDate = BirthDateOrRecordDate.SelectedDate;
            string issuePlace = IssuePlaceTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string postalCode = postalCodeTextBox.Text.Trim();
            string urgentTelNo = UrgentTelNoTextBox.Text.Trim();
            string mobileNo = MobileNoTextBox.Text.Trim();
            int authenticationStatusValue = Convert.ToInt32(ShahkarAuthenticationStatusComboBox.SelectedValue);
            DB.ShahkarAuthenticationStatus authenticationStatus = (DB.ShahkarAuthenticationStatus)authenticationStatusValue;

            Action mainAction = new Action(() =>
                                                {
                                                    List<CustomerFormInfo> result = Data.CustomerDB.SearchCustomer(
                                                                                                                    citiesId, centersId,
                                                                                                                    personTypes, nationalCodeOrRecordNo,
                                                                                                                    firstNameOrTitle, lastName,
                                                                                                                    fatherName, genders,
                                                                                                                    birthCertificateID, //CustomerIdTextBox.Text.Trim(),
                                                                                                                    birthDateOrRecordDate, issuePlace,
                                                                                                                    urgentTelNo, mobileNo,
                                                                                                                    email, telephoneNo, postalCode, authenticationStatus,
                                                                                                                    startRowIndex, pageSize, out count
                                                                                                                   );
                                                    Dispatcher.BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                                                                                                                        {
                                                                                                                            ItemsDataGrid.ItemsSource = result;
                                                                                                                            Pager.TotalRecords = count;
                                                                                                                        }
                                                                                                                  )
                                                                           );
                                                }
                                          );

            //مقداردهی عملیات اطلاع رسانی از وضعیت اجرای عملیات اصلی
            Action duringOperationAction = new Action(() =>
            {
                MainExtendedStatusBar.ShowProgressBar = true;
                MainExtendedStatusBar.MessageLabel.FontSize = 13;
                MainExtendedStatusBar.MessageLabel.FontWeight = FontWeights.Bold;
                MainExtendedStatusBar.MessageLabel.Text = "درحال بارگذاری...";
                Pager.IsEnabled = false;
                SearchExpander.IsEnabled = false;
                ItemsDataGrid.IsEnabled = false;
                this.Cursor = Cursors.Wait;
            });


            //مقداردهی عملیاتی که باید بعد از اتمام عملیات اصلی اجرا شود 
            Action afterOperationAction = new Action(() =>
            {
                MainExtendedStatusBar.ShowProgressBar = false;
                MainExtendedStatusBar.MessageLabel.FontSize = 8;
                MainExtendedStatusBar.MessageLabel.FontWeight = FontWeights.Normal;
                MainExtendedStatusBar.MessageLabel.Text = string.Empty;
                Pager.IsEnabled = true;
                SearchExpander.IsEnabled = true;
                ItemsDataGrid.IsEnabled = true;
                this.Cursor = Cursors.Arrow;
            });

            CRM.Application.Local.TimeConsumingOperation timeConsumingOperation = new Local.TimeConsumingOperation
            {
                MainOperationAction = mainAction,
                DuringOperationAction = duringOperationAction,
                AfterOperationAction = afterOperationAction
            };

            //اجرای عملیات
            this.RunTimeConsumingOperation(timeConsumingOperation);
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.CustomerFormInfo item = ItemsDataGrid.SelectedItem as CRM.Data.CustomerFormInfo;

                    //چنانچه مشترک دارای تلفن باشد نباید کاربر بتواند دآن را حذف کند
                    List<Telephone> telephones = TelephoneDB.GetTelephoneByCustomerID(item.ID);
                    if (telephones.Count != 0)
                    {
                        Folder.MessageBox.ShowWarning("این مشترک دارای تلفن میباشد،لذا امکان حذف وجود ندارد.");
                    }
                    else
                    {
                        DB.Delete<Data.Customer>(item.ID);
                        ShowSuccessMessage("مشترک مورد نظر حذف شد");
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف مشترک", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CRM.Data.CustomerFormInfo item = ItemsDataGrid.SelectedItem as CRM.Data.CustomerFormInfo;
                if (item == null) return;

                CustomerForm window = new CustomerForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                {
                    Customer editedCustomer = CustomerDB.GetCustomerByID(item.ID);
                    editedCustomer.IsAuthenticated = null;
                    editedCustomer.Detach();
                    DB.Save(editedCustomer);
                    Search(null, null);
                }
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            CustomerForm window = new CustomerForm();
            window.ShowDialog();
        }

        private void CitiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CitiesComboBox.SelectedIDs);
        }

        #endregion

        #region Print EventHandlers

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int startRowIndex = 0;
                int pageSize = Pager.TotalRecords;
                long telephoneNo = -1;
                if (!string.IsNullOrWhiteSpace(TelephoneTextBox.Text)) telephoneNo = Convert.ToInt64(TelephoneTextBox.Text);
                int authenticationStatusValue = Convert.ToInt32(ShahkarAuthenticationStatusComboBox.SelectedValue);
                DB.ShahkarAuthenticationStatus authenticationStatus = (DB.ShahkarAuthenticationStatus)authenticationStatusValue;

                int count = default(int);
                DataSet data = Data.CustomerDB.SearchCustomer(
                                                              CitiesComboBox.SelectedIDs, CentersComboBox.SelectedIDs,
                                                              PersonTypeComboBox.SelectedIDs, NationalCodeOrRecordNoTextBox.Text.Trim(),
                                                              FirstNameOrTitleTextBox.Text.Trim(), LastNameTextBox.Text.Trim(),
                                                              FatherNameTextBox.Text.Trim(), GenderComboBox.SelectedIDs,
                                                              BirthCertificateIDTextBox.Text.Trim(),// CustomerIdTextBox.Text.Trim(), 
                                                              BirthDateOrRecordDate.SelectedDate,
                                                              IssuePlaceTextBox.Text.Trim(), UrgentTelNoTextBox.Text.Trim(),
                                                              MobileNoTextBox.Text.Trim(), EmailTextBox.Text.Trim(), telephoneNo, postalCodeTextBox.Text.Trim(), authenticationStatus,
                                                              startRowIndex, pageSize, out count
                                                             ).ToDataSet("Result", ItemsDataGrid);

                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در چاپ لیست مشترکین");
                MessageBox.Show("خطا در چاپ", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm._sumCheckedList = _sumColumn;
            reportSettingForm.ShowDialog();
            _sumColumn = reportSettingForm._sumCheckedList;
            _groupingColumn = reportSettingForm._checkedList;
            _title = reportSettingForm._title;
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns);
        }

        #endregion print

        private void AddAgentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CRM.Data.CustomerFormInfo item = ItemsDataGrid.SelectedItem as CRM.Data.CustomerFormInfo;
                if (item == null) return;

                //تعریف نماینده فقط برای مشترکین حقوقی امکان پذیر میباشد
                if (item.PersonTypeByte != (int)DB.PersonType.Company)
                {
                    MessageBox.Show(".مشترک انتخاب شده حقوقی نیست", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                AgentForm agentForm = new AgentForm(item);
                if (agentForm.ShowDialog() == false)
                {
                    return;
                }

                Customer currentCustomer = CustomerDB.GetCustomerByID(item.ID);
                currentCustomer.AgentID = agentForm.ID;
                currentCustomer.Detach();
                DB.Save(currentCustomer);

                ShowSuccessMessage("نماینده با موفقیت ایجاد شد");
            }
        }

        private void ReceivePersonAuthenticationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PendarWebApiResult pendarWebApiResult = new PendarWebApiResult();
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                try
                {
                    //فراخوانی مقادیر تنظیمات سامانه خودمان
                    //string pendarPajouhWebApiIP = "78.39.252.109";
                    string pendarPajouhWebApiIP = SettingDB.GetSettingByKey("PendarPajouhWebApiIP").Value;
                    string pendarPajouhWebApiPort = SettingDB.GetSettingByKey("PendarPajouhWebApiPort").Value;
                    string pendarPajouhApiUserName = SettingDB.GetSettingByKey("PendarPajouhApiUserName").Value;
                    string pendarPajouhApiPassword = SettingDB.GetSettingByKey("PendarPajouhApiPassword").Value;

                    CRM.Data.CustomerFormInfo item = ItemsDataGrid.SelectedItem as CRM.Data.CustomerFormInfo;
                    if (item == null) return;

                    if (item.PersonTypeByte != (int)DB.PersonType.Person)
                    {
                        MessageBox.Show(".مشترک انتخاب شده حقیقی نیست", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    DateTime sendDateTime = DB.GetServerDate();

                    //اطلاعات خام مشترک انتخاب شده توسط کاربر را از دیتابیس بازیابی میکنیم 
                    Customer foundCustomer = CustomerDB.GetCustomerByID(item.ID);

                    //با استفاده از اطلاعات خام مشترک در دیتابیس خودمان موجودیتی متناسب با ساختار سامانه شاهکار آماده می کنیم
                    IranianAuthentication iranianAuthentication = ShahkarEntityCreators.CreateIranianAuthenticationFromPersonCustomer(foundCustomer);

                    if (iranianAuthentication == null)
                    {
                        MessageBox.Show(".در ارسال درخواست خطا رخ داده است", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    //آدرس ای پی مربوط به احراز هویت سامانه شاهکار
                    string pendarWebApiAddress = string.Format("http://{0}:{1}/api/PendarPajouhCRM/IranianPersonCustomerAuthentication", pendarPajouhWebApiIP, pendarPajouhWebApiPort);

                    //بر اساس تصمیم گیری جدید ، قبل از هر گونه اقدامی باید مشترک از طرف شاهکار احراز هویت گردد
                    string responseFromPendarWebApi = new Send<IranianAuthentication>().SendHttpWebRequest(iranianAuthentication, pendarWebApiAddress, "POST", pendarPajouhApiUserName, pendarPajouhApiPassword);

                    if (responseFromPendarWebApi == null)
                    {
                        MessageBox.Show(".در ارسال درخواست خطا رخ داده است", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    pendarWebApiResult = serializer.Deserialize<PendarWebApiResult>(responseFromPendarWebApi);

                    ShahkarRawResult shahkarResultFromPendarWebApi = new ShahkarRawResult();
                    shahkarResultFromPendarWebApi = serializer.Deserialize<ShahkarRawResult>(pendarWebApiResult.RawResultFromShahkar);

                    if (shahkarResultFromPendarWebApi.response == 200) //مشترک احراز هویت شده است
                    {
                        foundCustomer.IsAuthenticated = true;
                        foundCustomer.Detach();
                        DB.Save(foundCustomer);
                        MessageBox.Show(".مشترک احراز هویت شد", "نتیجه نهایی", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else //مشترک به هر دلیلی احراز هویت نشده است و باید پاسخ مناسب برای نمایش به کابر آماده شود
                    {
                        ShahkarMeaningfulResponse shahkarMeaningfulResponse = new ShahkarMeaningfulResponse();

                        //Check of response
                        //یعنی یک جایی از دیتای ارسالی با قواعد تعریف شده توسط شاهکار سازگار نبوده است
                        //بنابراین باید پاسخ شاهکار را بررسی کرده و یک نتجه قابل فهم را برای کاربر مهیا کنیم
                        if (shahkarResultFromPendarWebApi.response == 311)
                        {
                            shahkarMeaningfulResponse = ShahkarMeaningfulResponse.ProvideShahkarMeaningfulResponseByRawResultFromShahkar(shahkarResultFromPendarWebApi);

                            if (!string.IsNullOrEmpty(shahkarMeaningfulResponse.Descriptions))
                            {
                                MessageBox.Show(shahkarMeaningfulResponse.Descriptions, "پاسخ شاهکار", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        //بلاک زیر هر خطای دیگری غیر از 311 را شامل میشود
                        if (shahkarResultFromPendarWebApi.response != 311)
                        {
                            MessageBox.Show(shahkarResultFromPendarWebApi.comment, "پاسخ شاهکار", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        foundCustomer.IsAuthenticated = false;
                        foundCustomer.Detach();
                        DB.Save(foundCustomer);

                        MessageBox.Show(".مشترک احراز هویت نشد", "نتیجه نهایی", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    //TODO: باید ورژن جدید ای پی را پابلیش کنم
                    //ایجاد رکورد لاگ برای درخواست فوق
                    //****************************************************************************************************
                    ShahkarWebApiLog log = new ShahkarWebApiLog();

                    log.ActionType = (int)DB.ShahkarActionType.IranianPersonCustomerAuthentication;
                    log.ActionRelativeURL = "api/PendarPajouhCRM/IranianPersonCustomerAuthentication";
                    log.CustomerID = item.ID;
                    log.SendDate = sendDateTime;
                    log.UserID = DB.CurrentUser.ID;

                    CRM.Data.Schema.ShahkarResult shahkarResult = new Data.Schema.ShahkarResult();
                    shahkarResult.Comment = shahkarResultFromPendarWebApi.comment;
                    shahkarResult.RequestId = shahkarResultFromPendarWebApi.requestId;
                    shahkarResult.Response = shahkarResultFromPendarWebApi.response;
                    shahkarResult.Result = shahkarResultFromPendarWebApi.result;
                    shahkarResult.ID = string.Empty;//این شناسه همان کلاسه ای میباشد که شاهکار در زمان تخصیص یک تلفن جدید به مشترک برای ما ارسال میکند
                    shahkarResult.FollowNo = shahkarResultFromPendarWebApi.followNo;

                    ShahkarAuthenticationInfo shahkarAuthenticationInfo = new ShahkarAuthenticationInfo();
                    shahkarAuthenticationInfo.ResultDetail = shahkarResult;
                    shahkarAuthenticationInfo.IsAuthenticated = foundCustomer.IsAuthenticated;

                    log.ActionDetails = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<ShahkarAuthenticationInfo>(shahkarAuthenticationInfo, true));
                    DB.Save(log, true);
                    //****************************************************************************************************

                    Search(null, null);
                }
                catch (WebException we)
                {
                    if (we.Response != null)
                    {
                        var responseStream = we.Response.GetResponseStream();
                        string primaryErrorResponse = string.Empty;
                        pendarWebApiResult = null;
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            primaryErrorResponse = reader.ReadToEnd();
                            pendarWebApiResult = serializer.Deserialize<PendarWebApiResult>(primaryErrorResponse);
                        }
                        responseStream.Close();
                        if (pendarWebApiResult != null && pendarWebApiResult.SystemHasError)
                        {
                            MessageBox.Show(pendarWebApiResult.SystemError, "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            Logger.WriteError("{0} - خطای سیستمی", primaryErrorResponse);
                            MessageBox.Show("خطای سیستمی", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("خطا در دریافت احراز هویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    Logger.WriteError("Following exception caught in {0}: ", System.Reflection.MethodBase.GetCurrentMethod().Name);
                    Logger.WriteException(we.Message);
                }
                catch (Exception ex)
                {
                    Logger.WriteError("Following exception caught in {0}: ", System.Reflection.MethodBase.GetCurrentMethod().Name);
                    Logger.WriteException(ex.Message);
                    MessageBox.Show("خطا در دریافت احراز هویت", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
