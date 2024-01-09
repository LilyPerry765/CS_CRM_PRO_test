using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using CRM.ADSLPortalKermanshah.Code;


namespace CRM.ADSLPortalKermanshah
{
    public partial class FeasibilityForm : System.Web.UI.Page
    {
        #region Properties

        Service1 aDSLService = new Service1();
        private string city = string.Empty;

        #endregion

        #region Methods

        private void Initialize()
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
        }

        private void ResetControls()
        {
            TelephoneNoTextBox.Text = string.Empty;
            ErrorMessageLabel.Visible = false;
            SuccessMessageLabel.Visible = false;
            SendButton.Enabled = true;
            TelephoneNoTextBox.Enabled = true;
        }

        private void SaveFeasibility(byte status)
        {
            User user = SecurityDB.GetUserbyUserName(Context.User.Identity.Name);
            PAPInfoUser papInfoUser = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault();
            ADSLPAPFeasibility aDSLPAPAFeasibility = new ADSLPAPFeasibility();
            long telephoneNo = 8300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

            aDSLPAPAFeasibility.PAPUserID = papInfoUser.ID;
            aDSLPAPAFeasibility.PAPInfoID = papInfoUser.PAPInfoID;
            if (status != (byte)DB.FeasibilityStatus.PhoneNotExist)
            {
                int centerID = 0;
                if (TelephoneDB.HasTelephoneNo(telephoneNo))
                    centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);
                else
                    centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                if (centerID != 0)
                    aDSLPAPAFeasibility.CityID = CenterDB.GetCityIDByCenterByID(centerID);
                else
                    aDSLPAPAFeasibility.CityID = -1;
            }
            else
                aDSLPAPAFeasibility.CityID = -1;

            aDSLPAPAFeasibility.TelephoneNo = telephoneNo;
            aDSLPAPAFeasibility.Date = DB.GetServerDate();
            aDSLPAPAFeasibility.Status = status;

            Helper.Save(aDSLPAPAFeasibility, true);
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

                    User user = UserDB.GetUserbyUserName(Context.User.Identity.Name);
                    int papID = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault().PAPInfoID;
                    PAPInfo papInfo = DB.SearchByPropertyName<PAPInfo>("ID", papID).SingleOrDefault();

                    if (papInfo.OperatingStatusID != 1)
                    {
                        MessageLabel.Visible = true;
                        MessageLabel.Text = DB.SearchByPropertyName<Data.PAPInfoOperatingStatus>("ID", papInfo.OperatingStatusID).SingleOrDefault().Title;
                        SendButton.Enabled = false;
                        ResetButton.Enabled = false;
                    }
                }
            }
        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Context != null) &&
                (Context.User != null) &&
                (Context.User.Identity != null) &&
                (Context.User.Identity.IsAuthenticated))
                {
                    if (string.IsNullOrEmpty(TelephoneNoTextBox.Text)) return;

                    User user = UserDB.GetUserbyUserName(Context.User.Identity.Name);
                    PAPInfoUser papInfoUser = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault();
                    PAPInfo papInfo = DB.SearchByPropertyName<PAPInfo>("ID", papInfoUser.PAPInfoID).SingleOrDefault();
                    long telephoneNo = 8300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

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
                        {
                            SaveFeasibility((byte)DB.FeasibilityStatus.BillingProblem);
                            throw new Exception("به دلیل بدهی ثبت درخواست امکان پذیر نمی باشد !");
                        }
                    }
                    else
                        throw new Exception("این شماره در سامانه آبونمان موجود نمی باشد !");

                    List<Request> aDSLPAPInstallRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.ADSLInstalPAPCompany);
                    if (aDSLPAPInstallRequests.Count != 0)
                        throw new Exception("برای این شماره در حال حاضر درخواست دایری موجود می باشد !");

                    int centerID = 0;
                    if (TelephoneDB.HasTelephoneNo(telephoneNo))
                        centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);
                    else
                        centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                    if (centerID == 0)
                        throw new Exception("این شماره در سامانه امور مشترکین موجود نمی باشد !");

                    List<int> centerIDs = SecurityDB.GetUserCenterIDs(user.ID);

                    if (!centerIDs.Contains(centerID))
                        throw new Exception("* دسترسی این مرکز برای شما تعریف نشده است !");

                    int cityID = CenterDB.GetCityIDByCenterByID(centerID);
                    string cityName = DB.SearchByPropertyName<City>("ID", cityID).SingleOrDefault().Name;

                    if (papInfoUser.FeasibilityNo != null)
                    {
                        int requestNo = ADSLPAPFeasibilityDB.GetFeasibilitytNo(papInfoUser.ID);
                        if (requestNo >= papInfoUser.FeasibilityNo)
                            throw new Exception("* تعداد استعلام های ثبت شده امروز شما برای شهر " + cityName + " کامل شده است!");
                    }

                    if (ADSLTelephoneAccuracyDB.CheckTelephoneAccuracy(telephoneNo, centerID))
                    {
                        SaveFeasibility((byte)DB.FeasibilityStatus.TelephoneTechnicalProblem);
                        throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");
                    }

                    if (ADSLPAPPortDB.HasADSLbyTelephoneNo(telephoneNo))
                    {
                        SaveFeasibility((byte)DB.FeasibilityStatus.HaveADSL);
                        throw new Exception("* شماره وارد شده دارای ADSL می باشد !");
                    }

                    SaveFeasibility((byte)DB.FeasibilityStatus.NoProblem);
                    ErrorMessageLabel.Visible = false;
                    SuccessMessageLabel.Text = "درخواست ADSL برای این شماره تلفن بلا مانع می باشد.";
                    SuccessMessageLabel.Visible = true;
                    SendButton.Enabled = false;
                    TelephoneNoTextBox.Enabled = false;
                }
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در ارسال اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید. ");
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در ارسال اطلاعات، لطفا با پشتیبانی تماس حاصل فرمایید. ");
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

        private void ShowErrorMessage(string message)
        {
            ErrorMessageLabel.Text = message;
            ErrorMessageLabel.Visible = true;

            SuccessMessageLabel.Visible = false;
            SendButton.Enabled = false;
            TelephoneNoTextBox.Enabled = false;
        }

        #endregion
    }
}