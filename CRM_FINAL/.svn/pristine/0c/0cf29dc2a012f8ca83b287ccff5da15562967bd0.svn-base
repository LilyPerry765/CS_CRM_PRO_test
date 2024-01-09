using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using CRM.ADSLPortalKermanshah.Code;
using System.Transactions;

namespace CRM.ADSLPortalKermanshah
{
    public partial class PAPInstallRequestForm : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        private void Initialize()
        {
            List<int> centerIDs = Data.CenterDB.GetUserCenters(UserDB.GetUserbyUserName(Context.User.Identity.Name).ID);

            ///// Center ComboBox
            //CenterList.Items.Add(new ListItem("-- انتخاب نمایید --", "0"));
            //foreach (int item in centerIDs)
            //{
            //    Center center = CenterDB.GetCenterByCenterID(item);
            //    Region region = RegionDB.GetRegionById(center.RegionID);
            //    City city = CityDB.GetCitybyCityID(region.CityID);
            //    CenterList.Items.Add(new ListItem(city.Name + " : " + center.CenterName, center.ID.ToString()));
            //}
            ///// End : Center ComboBox

            //InstalTimeOutList.Items.Add(new ListItem("-- انتخاب نمایید --", "0"));
            //for (int index = 1; index <= 4; index++)
            //{
            //    InstalTimeOutList.Items.Add(new ListItem(DB.GetEnumDescriptionByValue(typeof(DB.ADSLPAPInstalTimeOut), index), index.ToString()));
            //}
        }

        private void ResetControls()
        {
            TelephoneNoTextBox.Text = string.Empty;
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;

            ///// Center ComboBox
            //CenterList.SelectedIndex = 0;
            ///// End : Center ComboBox

            CustomerStatusList.SelectedIndex = 0;
            //InstalTimeOutList.SelectedIndex = 0;
            RowTextBox.Text = string.Empty;
            ColumnTextBox.Text = string.Empty;
            BuchtTextBox.Text = string.Empty;
            MessageTelephoneLabel.Visible = false;
            SaveErrorMessageLabel.Text = "";
            //DebtErrorLabel.Visible = false;
        }

        private bool CheckCabinetAccuracy(int cabinetNo, int centerID)
        {
            bool result = false;

            if (ADSLPAPCabinetAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                result = true;
            else
                result = false;

            return result;
        }

        private void ShowErrorMessage(string message)
        {
            MessageTelephoneLabel.Text = message;
            MessageTelephoneLabel.Visible = true;

            SaveSuccessMessageLabel.Visible = false;
            SaveErrorMessageLabel.Text = "خطا در ارسال درخواست !";
            SaveErrorMessageLabel.Visible = true;
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if ((Context != null) &&
                (Context.User != null) &&
                (Context.User.Identity != null) &&
                (Context.User.Identity.IsAuthenticated))
                {
                    Initialize();

                    User user = SecurityDB.GetUserbyUserName(Context.User.Identity.Name);
                    int papID = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault().PAPInfoID;
                    PAPInfo papInfo = DB.SearchByPropertyName<PAPInfo>("ID", papID).SingleOrDefault();

                    if (papInfo.OperatingStatusID != 1)
                    {
                        SaveErrorMessageLabel.Visible = true;
                        SaveErrorMessageLabel.Text = DB.SearchByPropertyName<Data.PAPInfoOperatingStatus>("ID", papInfo.OperatingStatusID).SingleOrDefault().Title;
                        SaveButton.Enabled = false;
                        ResetButton.Enabled = false;
                    }
                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Context != null) &&
                (Context.User != null) &&
                (Context.User.Identity != null) &&
                (Context.User.Identity.IsAuthenticated))
                {
                    string city = SettingDB.GetSettingValueByKey("City");
                    User user = UserDB.GetUserbyUserName(Context.User.Identity.Name);
                    PAPInfoUser papInfoUser = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault();
                    PAPInfo papInfo = DB.SearchByPropertyName<PAPInfo>("ID", papInfoUser.PAPInfoID).SingleOrDefault();
                    long telephoneNo = 0;

                    if (papInfo.ID != 1)
                    {
                        if (string.IsNullOrWhiteSpace(RowTextBox.Text))
                            throw new Exception("لطفا شماره ردیف را وارد نمایید *");
                        if (string.IsNullOrWhiteSpace(ColumnTextBox.Text))
                            throw new Exception("لطفا شماره طبقه را وارد نمایید *");
                        if (string.IsNullOrWhiteSpace(BuchtTextBox.Text))
                            throw new Exception("لطفا شماره اتصالی را وارد نمایید *");
                    }

                    if (city == "Kermanshah")
                    {
                        telephoneNo = 8300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());
                        
                        BillingCRMService billingService = new BillingCRMService();
                        V_LastDebtInfo debtInfo = null;

                        try
                        {
                            debtInfo = billingService.GetV_LastDebt(telephoneNo.ToString(), "pendar", "pendar@92");
                        }
                        catch (Exception)
                        {
                            ShowErrorMessage("این شماره در سامانه آبونمان موجود نمی باشد");
                            return;
                        }

                        if (debtInfo != null)
                        {
                            double debt = debtInfo.Lastdebt;

                            if (debt > Convert.ToInt64(Data.SettingDB.GetSettingValueByKey("ADSLPAPRequestDebt")))
                                throw new Exception("به دلیل بدهی ثبت درخواست امکان پذیر نمی باشد !");
                        }
                    }
                    if (city == "gilan")
                        telephoneNo = 1300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());
                    
                    int centerID = 0;
                    if (TelephoneDB.HasTelephoneNo(telephoneNo))
                    {
                        centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);
                        Bucht buchtCRM = BuchtDB.GetBuchtbyTelephoneNo(telephoneNo);

                        if (buchtCRM != null)
                            if (buchtCRM.PCMPortID != null)
                                throw new Exception("امکان تخصیص ADSL به این شماره وجود ندارد !");
                    }
                    else
                        centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                    List<int> centerIDs = SecurityDB.GetUserCenterIDs(user.ID);

                    if (centerID == 0)
                        throw new Exception("این شماره در سامانه امور مشترکین موجود نمی باشد !");

                    if (!centerIDs.Contains(centerID))
                        throw new Exception("* دسترسی این مرکز برای شما تعریف نشده است !");

                    int cityID = CenterDB.GetCityIDByCenterByID(centerID);
                    string cityName = DB.SearchByPropertyName<City>("ID", cityID).SingleOrDefault().Name;

                    if (papInfoUser.InstallRequestNo != null)
                    {
                        int requestNo = ADSLPAPRequestDB.GetPAPRequestNo(user.ID, (byte)DB.RequestType.ADSLInstalPAPCompany);
                        if (requestNo >= papInfoUser.InstallRequestNo)
                            throw new Exception("* تعداد درخواست های دایری ثبت شده امروز شما برای شهر " + cityName + " کامل شده است!");
                    }

                    List<Request> aDSLPAPInstallRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.ADSLInstalPAPCompany);
                    if (aDSLPAPInstallRequests.Count != 0)
                        throw new Exception("برای این شماره در حال حاضر درخواست دایری موجود می باشد !");

                    if (ADSLTelephoneAccuracyDB.CheckTelephoneAccuracy(telephoneNo, centerID))
                        throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");

                    if (ADSLPAPPortDB.HasADSLbyTelephoneNo(telephoneNo))
                        throw new Exception("* این شماره تلفن در حال حاضر دارای ADSL می باشد !");

                    ADSLPAPRequest papRequestK = new ADSLPAPRequest();
                    Request requestK = new Request();
                    ADSLPAPPort bucht = null;

                    if (!string.IsNullOrWhiteSpace(RowTextBox.Text) && !string.IsNullOrWhiteSpace(ColumnTextBox.Text) && !string.IsNullOrWhiteSpace(BuchtTextBox.Text))
                    {
                        if (!Data.ADSLPAPPortDB.HasPAPBucht(papInfo.ID, Convert.ToInt32(RowTextBox.Text), Convert.ToInt32(ColumnTextBox.Text), Convert.ToInt64(BuchtTextBox.Text), centerID))
                            throw new Exception("* بوخت مورد نظر موجود نمی باشد !");
                        if (!Data.ADSLPAPPortDB.GetBuchtStatus(papInfo.ID, Convert.ToInt32(RowTextBox.Text), Convert.ToInt32(ColumnTextBox.Text), Convert.ToInt64(BuchtTextBox.Text), centerID))
                            throw new Exception("* بوخت مورد نظر خالی نمی باشد !");

                        bucht = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(papInfo.ID, Convert.ToInt32(RowTextBox.Text), Convert.ToInt32(ColumnTextBox.Text), Convert.ToInt32(BuchtTextBox.Text), centerID);
                    }

                    requestK.TelephoneNo = telephoneNo;
                    requestK.RequestTypeID = (byte)DB.RequestType.ADSLInstalPAPCompany;
                    requestK.CenterID = centerID;
                    requestK.RequestDate = DB.GetServerDate();
                    requestK.RequesterName = papInfo.Title;
                    requestK.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                    requestK.StatusID = Data.WorkFlowDB.GetNextStatesID(DB.Action.Confirm, DB.GetStatus((byte)DB.RequestType.ADSLInstalPAPCompany, (byte)DB.RequestStatusType.Start).ID);
                    requestK.InsertDate = DB.GetServerDate();
                    requestK.ModifyDate = DB.GetServerDate();
                    requestK.CreatorUserID = user.ID;
                    requestK.ModifyUserID = user.ID;
                    requestK.PreviousAction = 1;
                    requestK.IsViewed = false;

                    papRequestK.RequestTypeID = (byte)DB.RequestType.ADSLInstalPAPCompany;
                    papRequestK.PAPInfoID = papInfo.ID;
                    papRequestK.TelephoneNo = telephoneNo;
                    papRequestK.Customer = FirstNameTextBox.Text + " " + LastNameTextBox.Text;
                    papRequestK.CustomerStatus = (byte)Convert.ToUInt16(CustomerStatusList.SelectedItem.Value);
                    if (bucht != null)
                    {
                        papRequestK.ADSLPAPPortID = bucht.ID;
                        papRequestK.SplitorBucht = RowTextBox.Text + "," + ColumnTextBox.Text + "," + BuchtTextBox.Text;
                    }
                    else
                        papRequestK.SplitorBucht = "";

                    papRequestK.LineBucht = "";
                    papRequestK.NewPort = "";
                    papRequestK.InstalTimeOut = 1;// (byte)Convert.ToInt16(InstalTimeOutList.SelectedValue);
                    papRequestK.Status = (byte)DB.ADSLPAPRequestStatus.Pending;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (requestK != null)
                        {
                            requestK.ID = Helper.GenerateRequestID();
                            requestK.Detach();
                            Helper.Save(requestK, true);
                        }

                        if (papRequestK != null)
                        {
                            if (requestK != null)
                                papRequestK.ID = requestK.ID;

                            papRequestK.Detach();
                            Helper.Save(papRequestK, true);
                        }

                        if (bucht != null)
                        {
                            bucht.TelephoneNo = telephoneNo;
                            bucht.Status = (byte)DB.ADSLPAPPortStatus.Reserve;

                            bucht.Detach();
                            Helper.Save(bucht);
                        }

                        scope.Complete();
                    }
                }

                SaveErrorMessageLabel.Visible = false;
                SaveSuccessMessageLabel.Text = "عملیات ارسال درخواست با موفقیت انجام شد.";
                SaveSuccessMessageLabel.Visible = true;

                ResetControls();
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در ارسال اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید. ");
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در ارسال اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        protected void Click(object sender, EventArgs e)
        {
            try
            {
                MessageTelephoneLabel.Text = string.Empty;
                string city = SettingDB.GetSettingValueByKey("City");

                if (string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text.Trim()))
                    throw new Exception("لطفا شماره تلفن را وارد نمایید.");
                if (TelephoneNoTextBox.Text.Trim().Length != 8)
                    throw new Exception("لطفا شماره تلفن صحیح 8 رقمی وارد نمایید.");

                long telephoneNo = 0;

                if (city == "Kermanshah")
                {                    
                    telephoneNo = 8300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

                    BillingCRMService billingService = new BillingCRMService();
                    V_LastDebtInfo debtInfo = null;

                    try
                    {
                        debtInfo = billingService.GetV_LastDebt(telephoneNo.ToString(), "pendar", "pendar@92");
                    }
                    catch (Exception)
                    {
                        MessageTelephoneLabel.Text = "این شماره در سامانه آبونمان موجود نمی باشد !";
                        MessageTelephoneLabel.Visible = true;
                        SaveSuccessMessageLabel.Visible = false;
                        return;
                    }

                    if (debtInfo != null)
                    {
                        double debt = debtInfo.Lastdebt;

                        if (debt > Convert.ToInt64(Data.SettingDB.GetSettingValueByKey("ADSLPAPRequestDebt")))
                            throw new Exception("به دلیل بدهی ثبت درخواست امکان پذیر نمی باشد !");
                    }
                }
                if (city == "gilan")
                    telephoneNo = 1300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

                List<Request> aDSLPAPInstallRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.ADSLInstalPAPCompany);
                if (aDSLPAPInstallRequests.Count != 0)
                    throw new Exception("برای این شماره در حال حاضر درخواست دایری موجود می باشد !");

                int centerID = 0;
                if (TelephoneDB.HasTelephoneNo(telephoneNo))
                {
                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);

                    if (telephone.Status != (byte)DB.TelephoneStatus.Connecting)
                        throw new Exception("تلفن مورد نظر دایر نمی باشد" + telephone.Status.ToString());

                    centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);
                    Bucht buchtCRM = BuchtDB.GetBuchtbyTelephoneNo(telephoneNo);

                    if (buchtCRM != null)
                        if (buchtCRM.PCMPortID != null)
                            throw new Exception("امکان تخصیص ADSL به این شماره وجود ندارد !");
                }
                else
                    centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                if (centerID == 0)
                    throw new Exception("این شماره در سامانه امور مشترکین موجود نمی باشد !");

                if (ADSLTelephoneAccuracyDB.CheckTelephoneAccuracy(telephoneNo, centerID))
                    throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");

                if (ADSLPAPPortDB.HasADSLbyTelephoneNo(telephoneNo))
                    throw new Exception("* این شماره تلفن در حال حاضر دارای ADSL می باشد !");
            }
            catch (FormatException ex)
            {
                MessageTelephoneLabel.Text = "لطفا شماره تلفن صحیح 8 رقمی وارد نمایید.";
                MessageTelephoneLabel.Visible = true;
                SaveSuccessMessageLabel.Visible = false;
            }
            catch (NullReferenceException ex)
            {
                MessageTelephoneLabel.Text = "لطفا شماره تلفن صحیح 8 رقمی وارد نمایید.";
                MessageTelephoneLabel.Visible = true;
                SaveSuccessMessageLabel.Visible = false;
            }
            catch (InvalidOperationException ex)
            {
                MessageTelephoneLabel.Text = "لطفا شماره تلفن صحیح 8 رقمی وارد نمایید.";
            }
            catch (Exception ex)
            {
                MessageTelephoneLabel.Text = ex.Message;
                MessageTelephoneLabel.Visible = true;
                SaveSuccessMessageLabel.Visible = false;
            }
        }

        #endregion
    }
}