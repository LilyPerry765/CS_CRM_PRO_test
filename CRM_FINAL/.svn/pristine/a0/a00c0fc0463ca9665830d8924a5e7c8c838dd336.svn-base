using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using System.Transactions;
using CRM.ADSLPortal.WebReference;


namespace CRM.ADSLPortal
{
    public partial class PAPInstallRequestForm : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        private void Initialize()
        {
            List<int> centerIDs = Data.CenterDB.GetUserCenters(DB.SearchByPropertyName<User>("UserName", Context.User.Identity.Name).SingleOrDefault().ID);
            CenterList.Items.Add(new ListItem("-- انتخاب نمایید --", "0"));
            foreach (int item in centerIDs)
            {
                Center center = DB.SearchByPropertyName<Center>("ID", item).SingleOrDefault();
                Region region = DB.SearchByPropertyName<Region>("ID", center.RegionID).SingleOrDefault();
                City city = DB.SearchByPropertyName<City>("ID", region.CityID).SingleOrDefault();
                CenterList.Items.Add(new ListItem(city.Name + " : " + center.CenterName, center.ID.ToString()));
            }

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
            CenterList.SelectedIndex = 0;
            CustomerStatusList.SelectedIndex = 0;
            //InstalTimeOutList.SelectedIndex = 0;
            PortColumnTextBox.Text = string.Empty;
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

                    User user = DB.SearchByPropertyName<User>("UserName", Context.User.Identity.Name).SingleOrDefault();
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
                    User user = DB.SearchByPropertyName<User>("UserName", Context.User.Identity.Name).SingleOrDefault();
                    PAPInfoUser papInfoUser = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault();
                    PAPInfo papInfo = DB.SearchByPropertyName<PAPInfo>("ID", papInfoUser.PAPInfoID).SingleOrDefault();
                    int cityID = CenterDB.GetCityIDByCenterByID(Convert.ToInt32(CenterList.SelectedValue));
                    string cityName = DB.SearchByPropertyName<City>("ID", cityID).SingleOrDefault().Name;
                    long telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

                    if (papInfoUser.InstallRequestNo != null)
                    {
                        int requestNo = ADSLPAPRequestDB.GetPAPRequestNo(user.ID, (byte)DB.RequestType.ADSLInstalPAPCompany);
                        if (requestNo >= papInfoUser.InstallRequestNo)
                            throw new Exception("* تعداد درخواست های دایری ثبت شده امروز شما برای شهر " + cityName + " کامل شده است!");
                    }

                    List<Request> aDSLPAPInstallRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.ADSLInstalPAPCompany);
                    if (aDSLPAPInstallRequests.Count != 0)
                        throw new Exception("برای این شماره در حال حاضر درخواست دایری موجود می باشد !");

                    List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.ADSL);
                    if (aDSLRequests.Count != 0)
                        throw new Exception("برای این شماره در حال حاضر درخواست دایری در مخابرات موجود می باشد !");

                    if (ADSLTelephoneAccuracyDB.CheckTelephoneAccuracy(telephoneNo, Convert.ToInt32(CenterList.SelectedValue)))
                        throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");
                    
                    Service1 aDSLService = new Service1();
                    System.Data.DataTable telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", TelephoneNoTextBox.Text);

                    //if (Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"]) == 57)
                    //    throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");

                    if (!telephoneInfo.Rows[0]["KAFU_NUM"].ToString().Contains("نا"))
                    {
                        int cabinetNo = Convert.ToInt32(telephoneInfo.Rows[0]["KAFU_NUM"].ToString());
                        if (CheckCabinetAccuracy(cabinetNo, Convert.ToInt32(CenterList.SelectedValue)))
                            throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");
                    }
                    if (!aDSLService.Is_Phone_Exist(TelephoneNoTextBox.Text))
                        throw new Exception("* شماره وارد شده موجود نمی باشد !");
                    else
                    {
                        if (ADSLDB.HasADSLInstalbyTelephoneNo(telephoneNo) || ADSLPAPPortDB.HasADSLbyTelephoneNo(telephoneNo))
                            throw new Exception("* شماره وارد شده دارای ADSL می باشد !");
                        else
                        {
                            if (aDSLService.Phone_Is_PCM(TelephoneNoTextBox.Text))
                                throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");
                            else
                            {
                                //if (aDSLService.Phone_Exist_In_Post_PCM(TelephoneNoTextBox.Text))
                                //    throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");
                                //else
                                //{
                                if (aDSLService.TelDissectionStatus(TelephoneNoTextBox.Text))
                                    throw new Exception("* شماره وارد شده قطع می باشد !");
                                else
                                {
                                    //WebReference.PhoneStatusService deptorService = new WebReference.PhoneStatusService();

                                    //bool result1 = true;
                                    //long debtAmount = 0;

                                    //deptorService.GetDebtStatus(TelephoneNoTextBox.Text, out debtAmount, out result1);
                                    //if (debtAmount > 50000)
                                    //    throw new Exception("امکان تخصیص ADSL برای شماره تلفن مورد نظر وجود ندارد، برای بررسی بیشتر به مخابرات مراجعه شود !");
                                    //else
                                    //{
                                    ADSLPAPRequest papRequest = new ADSLPAPRequest();
                                    Request request = new Request();
                                    //PAPInfoUser papInfoUser = DB.SearchByPropertyName<PAPInfoUser>("Username", Context.User.Identity.Name).SingleOrDefault();
                                    //PAPInfo papInfo = DB.SearchByPropertyName<PAPInfo>("ID", papInfoUser.PAPInfoID).SingleOrDefault();
                                    //Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo).SingleOrDefault();

                                    if (!Data.ADSLPAPPortDB.HasPAPPort(papInfo.ID, Convert.ToInt64(PortColumnTextBox.Text), Convert.ToInt32(CenterList.SelectedValue)))
                                        throw new Exception("* پورت مورد نظر موجود نمی باشد !");
                                    else
                                    {
                                        if (!Data.ADSLPAPPortDB.GetPortStatus(papInfo.ID, Convert.ToInt64(PortColumnTextBox.Text), Convert.ToInt32(CenterList.SelectedValue)))
                                            throw new Exception("* پورت مورد نظر خالی نمی باشد !");
                                        else
                                        {
                                            request.TelephoneNo = telephoneNo;
                                            request.RequestTypeID = (byte)DB.RequestType.ADSLInstalPAPCompany;
                                            request.CenterID = Convert.ToInt32(CenterList.SelectedValue);
                                            //if (telephone != null)
                                            //    request.CenterID = telephone.CenterID;
                                            //else
                                            //    request.CenterID = -1;                                            
                                            request.RequestDate = DB.GetServerDate();
                                            request.RequesterName = papInfo.Title;
                                            request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                                            request.StatusID = Data.WorkFlowDB.GetNextStatesID(DB.Action.Confirm, DB.GetStatus((byte)DB.RequestType.ADSLInstalPAPCompany, (byte)DB.RequestStatusType.Start).ID);
                                            request.InsertDate = DB.GetServerDate();
                                            request.ModifyDate = DB.GetServerDate();
                                            request.CreatorUserID = user.ID;
                                            request.ModifyUserID = user.ID;
                                            request.PreviousAction = 1;
                                            request.IsViewed = false;

                                            papRequest.RequestTypeID = (byte)DB.RequestType.ADSLInstalPAPCompany;
                                            papRequest.PAPInfoID = papInfo.ID;
                                            papRequest.TelephoneNo = telephoneNo;
                                            papRequest.Customer = FirstNameTextBox.Text + " " + LastNameTextBox.Text;
                                            papRequest.CustomerStatus = (byte)Convert.ToUInt16(CustomerStatusList.SelectedItem.Value);
                                            papRequest.SplitorBucht = PortColumnTextBox.Text; // +" - " + SpliterRowTextBox.Text + " - " + SpliterNoTextBox.Text;
                                            papRequest.LineBucht = "";// LineColumnTextBox.Text + " - " + LineRowTextBox.Text + " - " + LineNoTextBox.Text;
                                            papRequest.NewPort = "";
                                            papRequest.InstalTimeOut = 1;// (byte)Convert.ToInt16(InstalTimeOutList.SelectedValue);
                                            papRequest.Status = (byte)DB.ADSLPAPRequestStatus.Pending;

                                            using (TransactionScope scope = new TransactionScope())
                                            {
                                                if (request != null)
                                                {
                                                    request.ID = Helper.GenerateRequestID();
                                                    request.Detach();
                                                    Helper.Save(request, true);
                                                }

                                                if (papRequest != null)
                                                {
                                                    if (request != null)
                                                        papRequest.ID = request.ID;

                                                    papRequest.Detach();
                                                    Helper.Save(papRequest, true);
                                                }

                                                scope.Complete();
                                            }
                                        }
                                    }
                                    //}
                                }
                                //}
                            }
                        }
                    }
                }

                SaveErrorMessageLabel.Visible = false;
                SaveSuccessMessageLabel.Text = "عملیات ارسال درخواست با موفقیت انجام شد.";
                SaveSuccessMessageLabel.Visible = true;

                ResetControls();
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

        #endregion
    }
}