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
    public partial class PAPExchangeRequestForm : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        private void Initialize()
        {
            List<int> centerIDs = Data.CenterDB.GetUserCenters(UserDB.GetUserbyUserName(Context.User.Identity.Name).ID);
            //CenterList.Items.Add(new ListItem("-- انتخاب نمایید --", "0"));
            //foreach (int item in centerIDs)
            //{
            //    Center center = DB.SearchByPropertyName<Center>("ID", item).SingleOrDefault();
            //    Region region = DB.SearchByPropertyName<Region>("ID", center.RegionID).SingleOrDefault();
            //    City city = DB.SearchByPropertyName<City>("ID", region.CityID).SingleOrDefault();
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
            //OldRowTextBox.Text = string.Empty;
            //OldColumnTextBox.Text = string.Empty;
            //OldBuchtTextBox.Text = string.Empty;
            NewRowTextBox.Text = string.Empty;
            NewColumnTextBox.Text = string.Empty;
            NewBuchtTextBox.Text = string.Empty;
            //SpliterRowTextBox.Text = string.Empty;
            //SpliterNoTextBox.Text = string.Empty;
            //LineColumnTextBox.Text = string.Empty;
            //LineRowTextBox.Text = string.Empty;
            //LineNoTextBox.Text = string.Empty;
            MessageTelephoneLabel.Visible = false;
            SaveErrorMessageLabel.Text = "";
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

                    if (city == "Kermanshah")
                        telephoneNo = 8300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());
                    if (city == "gilan")
                        telephoneNo = 1300000000 + Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

                    if (papInfo.ID != 1)
                    {
                        if (string.IsNullOrWhiteSpace(NewRowTextBox.Text))
                            throw new Exception("لطفا شماره ردیف را وارد نمایید *");
                        if (string.IsNullOrWhiteSpace(NewColumnTextBox.Text))
                            throw new Exception("لطفا شماره طبقه را وارد نمایید *");
                        if (string.IsNullOrWhiteSpace(NewBuchtTextBox.Text))
                            throw new Exception("لطفا شماره اتصالی را وارد نمایید *");
                    }

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

                    if (papInfoUser.InstallRequestNo != null)
                    {
                        int requestNo = ADSLPAPRequestDB.GetPAPRequestNo(user.ID, (byte)DB.RequestType.ADSLDischargePAPCompany);
                        if (requestNo >= papInfoUser.ExchangeRequestNo)
                            throw new Exception("* تعداد درخواست های تعویض پورت ثبت شده امروز شما برای شهر " + cityName + " کامل شده است!");
                    }

                    List<Request> aDSLPAPExchangeRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.ADSLExchangePAPCompany);
                    if (aDSLPAPExchangeRequests.Count != 0)
                        throw new Exception("برای این شماره در حال حاضر درخواست تعویض پورت موجود می باشد !");

                    ADSLPAPRequest papRequestK = new ADSLPAPRequest();
                    Request requestK = new Request();
                    ADSLPAPPort newBucht = null;

                    if (!string.IsNullOrWhiteSpace(NewRowTextBox.Text) && !string.IsNullOrWhiteSpace(NewColumnTextBox.Text) && !string.IsNullOrWhiteSpace(NewBuchtTextBox.Text))
                    {
                        if (!Data.ADSLPAPPortDB.HasPAPBucht(papInfo.ID, Convert.ToInt32(NewRowTextBox.Text), Convert.ToInt32(NewColumnTextBox.Text), Convert.ToInt64(NewBuchtTextBox.Text), centerID))
                            throw new Exception("* بوخت مورد نظر موجود نمی باشد !");
                        if (!Data.ADSLPAPPortDB.GetBuchtStatus(papInfo.ID, Convert.ToInt32(NewRowTextBox.Text), Convert.ToInt32(NewColumnTextBox.Text), Convert.ToInt64(NewBuchtTextBox.Text), centerID))
                            throw new Exception("* بوخت مورد نظر خالی نمی باشد !");

                        newBucht = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(papInfo.ID, Convert.ToInt32(NewRowTextBox.Text), Convert.ToInt32(NewColumnTextBox.Text), Convert.ToInt32(NewBuchtTextBox.Text), centerID);
                    }

                    if (!Data.ADSLPAPPortDB.HasPAPTelephone(telephoneNo, papInfo.ID, centerID))
                        throw new Exception("* سرویس ADSlشماره تلفن مورد نظر مربوط به این شرکت نمی باشد !");
                    else
                    {
                        requestK.TelephoneNo = telephoneNo;
                        requestK.RequestTypeID = (byte)DB.RequestType.ADSLExchangePAPCompany;
                        requestK.CenterID = centerID;
                        requestK.RequestDate = DB.GetServerDate();
                        requestK.RequesterName = papInfo.Title;
                        requestK.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                        requestK.StatusID = Data.WorkFlowDB.GetNextStatesID(DB.Action.Confirm, DB.GetStatus((byte)DB.RequestType.ADSLExchangePAPCompany, (byte)DB.RequestStatusType.Start).ID);
                        requestK.InsertDate = DB.GetServerDate();
                        requestK.ModifyDate = DB.GetServerDate();
                        requestK.CreatorUserID = user.ID;
                        requestK.ModifyUserID = user.ID;
                        requestK.PreviousAction = 1;
                        requestK.IsViewed = false;

                        papRequestK.RequestTypeID = (byte)DB.RequestType.ADSLExchangePAPCompany;
                        papRequestK.PAPInfoID = papInfo.ID;
                        papRequestK.TelephoneNo = telephoneNo;
                        papRequestK.Customer = FirstNameTextBox.Text + " " + LastNameTextBox.Text;
                        papRequestK.CustomerStatus = (byte)Convert.ToUInt16(CustomerStatusList.SelectedItem.Value);
                        if (newBucht != null)
                        {
                            papRequestK.ADSLPAPPortID = newBucht.ID;
                            papRequestK.NewPort = NewRowTextBox.Text + "," + NewColumnTextBox.Text + "," + NewBuchtTextBox.Text;
                        }
                        else
                            papRequestK.NewPort = "";

                        papRequestK.SplitorBucht = "";
                        papRequestK.LineBucht = "";
                        papRequestK.InstalTimeOut = 1;
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

                            if (newBucht != null)
                            {
                                newBucht.TelephoneNo = telephoneNo;
                                newBucht.Status = (byte)DB.ADSLPAPPortStatus.Reserve;

                                newBucht.Detach();
                                Helper.Save(newBucht);
                            }

                            scope.Complete();
                        }
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
    }
}