using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using CRM.Data;

namespace CRM.ADSLPortal
{
    public partial class PAPDischargeRequestForm : System.Web.UI.Page
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
                SaveErrorMessageLabel.Text = "";

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

                    if (papInfoUser.DischargeRequestNo != null)
                    {
                        int requestNo = ADSLPAPRequestDB.GetPAPRequestNo(user.ID, (byte)DB.RequestType.ADSLDischargePAPCompany);
                        if (requestNo >= papInfoUser.DischargeRequestNo)
                            throw new Exception("* تعداد درخواست های تخلیه ثبت شده امروز شما برای شهر " + cityName + " کامل شده است!");
                    }

                    List<Request> aDSLPAPDischargeRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.ADSLDischargePAPCompany);
                    if (aDSLPAPDischargeRequests.Count != 0)
                        throw new Exception("برای این شماره در حال حاضر درخواست تخلیه موجود می باشد !");

                    Service1 aDSLService = new Service1();
                    if (!ADSLPAPPortDB.HasPAPTelephone(telephoneNo, papInfo.ID, Convert.ToInt32(CenterList.SelectedValue)))
                        throw new Exception("* شماره وارد شده موجود نمی باشد !");
                    else
                    {
                        ADSLPAPRequest papRequest = new ADSLPAPRequest();
                        Request request = new Request();
                        //Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo).SingleOrDefault();

                        if (!Data.ADSLPAPPortDB.HasPAPPort(papInfo.ID, Convert.ToInt64(PortColumnTextBox.Text), Convert.ToInt32(CenterList.SelectedValue)))
                            throw new Exception("* پورت مورد نظر موجود نمی باشد !");
                        else
                        {
                            if (!Data.ADSLPAPPortDB.HasPAPTelephone(telephoneNo, papInfo.ID, Convert.ToInt32(CenterList.SelectedValue)))
                                throw new Exception("* سرویس ADSlشماره تلفن مورد نظر مربوط به این شرکت نمی باشد !");
                            else
                            {
                                if (Data.ADSLPAPPortDB.GetTelephonePortNo(telephoneNo, papInfo.ID, Convert.ToInt32(CenterList.SelectedValue)) != Convert.ToInt64(PortColumnTextBox.Text))
                                    throw new Exception("* شماره پورت وارد شده مربوط به این تلفن نمی باشد !");
                                else
                                {
                                    request.TelephoneNo = telephoneNo;
                                    request.RequestTypeID = (byte)DB.RequestType.ADSLDischargePAPCompany;
                                    request.CenterID = Convert.ToInt32(CenterList.SelectedValue);
                                    //if (telephone != null)
                                    //    request.CenterID = telephone.CenterID;
                                    //else
                                    //    request.CenterID = -1;                                    
                                    request.RequestDate = DB.GetServerDate();
                                    request.RequesterName = papInfo.Title;
                                    request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                                    request.StatusID = Data.WorkFlowDB.GetNextStatesID(DB.Action.Confirm, DB.GetStatus((byte)DB.RequestType.ADSLDischargePAPCompany, (byte)DB.RequestStatusType.Start).ID);
                                    request.InsertDate = DB.GetServerDate();
                                    request.ModifyDate = DB.GetServerDate();
                                    request.CreatorUserID = user.ID;
                                    request.ModifyUserID = user.ID;
                                    request.PreviousAction = 1;
                                    request.IsViewed = false;

                                    papRequest.RequestTypeID = (byte)DB.RequestType.ADSLDischargePAPCompany;
                                    papRequest.PAPInfoID = papInfo.ID;
                                    papRequest.TelephoneNo = telephoneNo;
                                    papRequest.Customer = FirstNameTextBox.Text + " " + LastNameTextBox.Text;
                                    papRequest.CustomerStatus = (byte)Convert.ToUInt16(CustomerStatusList.SelectedItem.Value);
                                    papRequest.SplitorBucht = PortColumnTextBox.Text; // +" - " + SpliterRowTextBox.Text + " - " + SpliterNoTextBox.Text;
                                    papRequest.LineBucht = ""; //LineColumnTextBox.Text + " - " + LineRowTextBox.Text + " - " + LineNoTextBox.Text;
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
                        }
                    }
                }

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