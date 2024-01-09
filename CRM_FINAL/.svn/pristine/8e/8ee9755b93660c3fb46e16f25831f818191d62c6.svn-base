using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using System.Transactions;
using CRM.ADSLPortalKermanshah.Code;

namespace CRM.ADSLPortalKermanshah
{
    public partial class PAPDischargeRequestForm : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        private void Initialize()
        {
            //List<int> centerIDs = Data.CenterDB.GetUserCenters(UserDB.GetUserbyUserName(Context.User.Identity.Name).ID);
            //CenterList.Items.Add(new ListItem("-- انتخاب نمایید --", "0"));
            //foreach (int item in centerIDs)
            //{
            //    Center center = CenterDB.GetCenterByCenterID(item);
            //    Region region = RegionDB.GetRegionById(center.RegionID);
            //    City city = CityDB.GetCitybyCityID(region.CityID);
            //    CenterList.Items.Add(new ListItem(city.Name + " : " + center.CenterName, center.ID.ToString()));
            //}

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
            //CenterList.SelectedIndex = 0;
            CustomerStatusList.SelectedIndex = 0;
            //InstalTimeOutList.SelectedIndex = 0;
            //RowTextBox.Text = string.Empty;
            //ColumnTextBox.Text = string.Empty;
            //BuchtTextBox.Text = string.Empty;
            MessageTelephoneLabel.Visible = false;
            SaveErrorMessageLabel.Text = "";
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
                SaveErrorMessageLabel.Text = "";

                if ((Context != null) &&
                (Context.User != null) &&
                (Context.User.Identity != null) &&
                (Context.User.Identity.IsAuthenticated))
                {
                    string city = SettingDB.GetSettingValueByKey("City");
                    User user = UserDB.GetUserbyUserName(Context.User.Identity.Name);
                    PAPInfoUser papInfoUser = PAPInfoUserDB.GetPAPUserByID(user.ID);
                    PAPInfo papInfo =PAPInfoDB.GetPAPInfoByID(papInfoUser.PAPInfoID);
                    long telephoneNo = 0;

                    if (city == "Kermanshah")
                        telephoneNo = 8300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());
                    if (city == "gilan")
                        telephoneNo = 1300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

                    int centerID = 0;
                    if (TelephoneDB.HasTelephoneNo(telephoneNo))
                        centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);
                    else
                        centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                    List<int> centerIDs = SecurityDB.GetUserCenterIDs(user.ID);

                    if (!centerIDs.Contains(centerID))
                        throw new Exception("* دسترسی این مرکز برای شما تعریف نشده است !");

                    int cityID = CenterDB.GetCityIDByCenterByID(centerID);
                    string cityName = DB.SearchByPropertyName<City>("ID", cityID).SingleOrDefault().Name;

                    if (papInfoUser.DischargeRequestNo != null)
                    {
                        int requestNo = ADSLPAPRequestDB.GetPAPRequestNo(user.ID, (byte)DB.RequestType.ADSLDischargePAPCompany);
                        if (requestNo >= papInfoUser.DischargeRequestNo)
                            throw new Exception("* تعداد درخواست های تخلیه ثبت شده امروز شما برای شهر " + cityName + " کامل شده است!");
                    }

                    List<Request> aDSLPAPDischargeRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.ADSLDischargePAPCompany);
                    if (aDSLPAPDischargeRequests.Count != 0)
                        throw new Exception("برای این شماره در حال حاضر درخواست تخلیه موجود می باشد !");

                    ADSLPAPRequest papRequestK = new ADSLPAPRequest();
                    Request requestK = new Request();

                    //if (!Data.ADSLPAPPortDB.HasPAPBucht(papInfo.ID, Convert.ToInt32(RowTextBox.Text), Convert.ToInt32(ColumnTextBox.Text), Convert.ToInt64(BuchtTextBox.Text), centerID))
                    //    throw new Exception("* بوخت مورد نظر موجود نمی باشد !");
                    //else
                    //{
                    if (!Data.ADSLPAPPortDB.HasPAPTelephone(telephoneNo, papInfo.ID, centerID))
                        throw new Exception("* سرویس ADSlشماره تلفن مورد نظر مربوط به این شرکت نمی باشد !");
                    else
                    {
                        //if (Data.ADSLPAPPortDB.HasTelephoneBucht(telephoneNo, papInfo.ID, Convert.ToInt32(RowTextBox.Text), Convert.ToInt32(ColumnTextBox.Text), Convert.ToInt64(BuchtTextBox.Text), centerID))
                        //    throw new Exception("* شماره پورت وارد شده مربوط به این تلفن نمی باشد !");
                        //else
                        //{
                        ADSLPAPPort bucht = ADSLPAPPortDB.GetADSLBuchtbyTelephoneNo(telephoneNo);

                        requestK.TelephoneNo = telephoneNo;
                        requestK.RequestTypeID = (byte)DB.RequestType.ADSLDischargePAPCompany;
                        requestK.CenterID = centerID;
                        requestK.RequestDate = DB.GetServerDate();
                        requestK.RequesterName = papInfo.Title;
                        requestK.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                        requestK.StatusID = Data.WorkFlowDB.GetNextStatesID(DB.Action.Confirm, DB.GetStatus((byte)DB.RequestType.ADSLDischargePAPCompany, (byte)DB.RequestStatusType.Start).ID);
                        requestK.InsertDate = DB.GetServerDate();
                        requestK.ModifyDate = DB.GetServerDate();
                        requestK.CreatorUserID = user.ID;
                        requestK.ModifyUserID = user.ID;
                        requestK.PreviousAction = 1;
                        requestK.IsViewed = false;

                        papRequestK.RequestTypeID = (byte)DB.RequestType.ADSLDischargePAPCompany;
                        papRequestK.PAPInfoID = papInfo.ID;
                        papRequestK.TelephoneNo = telephoneNo;
                        papRequestK.Customer = FirstNameTextBox.Text + " " + LastNameTextBox.Text;
                        papRequestK.CustomerStatus = (byte)Convert.ToUInt16(CustomerStatusList.SelectedItem.Value);
                        papRequestK.ADSLPAPPortID = bucht.ID;
                        papRequestK.SplitorBucht = "";// RowLabel.Text + "," + ColumnLabel.Text + "," + BuchtLabel.Text;
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

                            scope.Complete();
                        }
                        //}
                    }
                    //}
                }

                RowLabel.Text = string.Empty;
                ColumnLabel.Text = string.Empty;
                BuchtLabel.Text = string.Empty;

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
            MessageTelephoneLabel.Text = message;
            MessageTelephoneLabel.Visible = true;

            SaveSuccessMessageLabel.Visible = false;
            SaveErrorMessageLabel.Text = "خطا در ارسال درخواست !";
            SaveErrorMessageLabel.Visible = true;
        }

        #endregion

        protected void Click(object sender, EventArgs e)
        {
            string city = SettingDB.GetSettingValueByKey("City");

            long telephoneNo = 0;

            if (city == "Kermanshah")
                telephoneNo = 8300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());
            if (city == "gilan")
                telephoneNo = 1300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

            ADSLPAPPort bucht = ADSLPAPPortDB.GetADSLBuchtbyTelephoneNo(telephoneNo);

            if (bucht != null)
            {
                RowLabel.Text = bucht.RowNo.ToString();
                ColumnLabel.Text = bucht.ColumnNo.ToString();
                BuchtLabel.Text = bucht.BuchtNo.ToString();
            }
        }
    }
}