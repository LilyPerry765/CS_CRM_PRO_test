using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using CRM.ADSLPortal.WebReference;

namespace CRM.ADSLPortal
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
            User user = DB.SearchByPropertyName<User>("UserName", Context.User.Identity.Name).SingleOrDefault();
            PAPInfoUser papInfoUser = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault();
            ADSLPAPFeasibility aDSLPAPAFeasibility = new ADSLPAPFeasibility();

            aDSLPAPAFeasibility.PAPUserID = papInfoUser.ID;
            aDSLPAPAFeasibility.PAPInfoID = papInfoUser.PAPInfoID;
            if (status != (byte)DB.FeasibilityStatus.PhoneNotExist)
            {
                int centerID = 0;
                System.Data.DataTable telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", TelephoneNoTextBox.Text);
                if (telephoneInfo.Rows.Count != 0)
                    centerID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"]));

                if (centerID != 0)
                    aDSLPAPAFeasibility.CityID = CenterDB.GetCityIDByCenterByID(centerID);
                else
                    aDSLPAPAFeasibility.CityID = -1;
            }
            else
                aDSLPAPAFeasibility.CityID = -1;
            aDSLPAPAFeasibility.TelephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);
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

                    User user = DB.SearchByPropertyName<User>("UserName", Context.User.Identity.Name).SingleOrDefault();
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


                    if (!aDSLService.Is_Phone_Exist(TelephoneNoTextBox.Text))
                    {
                        SaveFeasibility((byte)DB.FeasibilityStatus.PhoneNotExist);
                        throw new Exception("* شماره وارد شده موجود نمی باشد !");
                    }
                    else
                    {
                        User user = DB.SearchByPropertyName<User>("UserName", Context.User.Identity.Name).SingleOrDefault();
                        PAPInfoUser papInfoUser = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault();
                        PAPInfo papInfo = DB.SearchByPropertyName<PAPInfo>("ID", papInfoUser.PAPInfoID).SingleOrDefault();

                        System.Data.DataTable telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", TelephoneNoTextBox.Text);
                        int centerID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"]));
                        int cityID = CenterDB.GetCityIDByCenterByID(centerID);
                        string cityName = DB.SearchByPropertyName<City>("ID", cityID).SingleOrDefault().Name;

                        if (papInfoUser.FeasibilityNo != null)
                        {
                            int requestNo = ADSLPAPFeasibilityDB.GetFeasibilitytNo(papInfoUser.ID);
                            if (requestNo >= papInfoUser.FeasibilityNo)
                                throw new Exception("* تعداد استعلام های ثبت شده امروز شما برای شهر " + cityName + " کامل شده است!");
                        }

                        if (ADSLTelephoneAccuracyDB.CheckTelephoneAccuracy(Convert.ToInt64(TelephoneNoTextBox.Text.Trim()), centerID))
                            throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");

                        string cabinet = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                        if (!cabinet.Contains("نا"))
                        {
                            int cabinetNo = Convert.ToInt32(cabinet);
                            if (CheckCabinetAccuracy(cabinetNo, Convert.ToInt32(centerID)))
                            {
                                SaveFeasibility((byte)DB.FeasibilityStatus.CabintTechnicalProblem);
                                throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");
                            }
                        }

                        if (ADSLDB.HasADSLInstalbyTelephoneNo(Convert.ToInt64(TelephoneNoTextBox.Text)) || ADSLPAPPortDB.HasADSLbyTelephoneNo(Convert.ToInt64(TelephoneNoTextBox.Text)))
                        {
                            SaveFeasibility((byte)DB.FeasibilityStatus.HaveADSL);
                            throw new Exception("* شماره وارد شده دارای ADSL می باشد !");
                        }
                        else
                        {
                            if (aDSLService.Phone_Is_PCM(TelephoneNoTextBox.Text))
                            {
                                SaveFeasibility((byte)DB.FeasibilityStatus.OnPCM);
                                throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");
                            }
                            else
                            {
                                //if (aDSLService.Phone_Exist_In_Post_PCM(TelephoneNoTextBox.Text))
                                //{
                                //    SaveFeasibility((byte)DB.FeasibilityStatus.PostPCM);
                                //    throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");
                                //}
                                //else
                                //{
                                if (aDSLService.TelDissectionStatus(TelephoneNoTextBox.Text))
                                {
                                    SaveFeasibility((byte)DB.FeasibilityStatus.Disconnected);
                                    throw new Exception("* شماره وارد شده قطع می باشد !");
                                }
                                else
                                {
                                    //WebReference.PhoneStatusService deptorService = new WebReference.PhoneStatusService();

                                    //bool result1 = true;
                                    //long debtAmount = 0;

                                    //deptorService.GetDebtStatus(TelephoneNoTextBox.Text, out debtAmount, out result1);
                                    //if (debtAmount > 50000)
                                    //{
                                    //    SaveFeasibility((byte)DB.FeasibilityStatus.BillingProblem);
                                    //    throw new Exception("امکان تخصیص ADSL برای شماره تلفن مورد نظر وجود ندارد، برای بررسی بیشتر به مخابرات مراجعه شود !");
                                    //}
                                    //else
                                    //{
                                    SaveFeasibility((byte)DB.FeasibilityStatus.NoProblem);
                                    ErrorMessageLabel.Visible = false;
                                    SuccessMessageLabel.Text = "درخواست ADSL برای این شماره تلفن بلا مانع می باشد.";
                                    SuccessMessageLabel.Visible = true;
                                    SendButton.Enabled = false;
                                    TelephoneNoTextBox.Enabled = false;
                                }
                                //}
                                //}
                            }
                        }
                    }
                }
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