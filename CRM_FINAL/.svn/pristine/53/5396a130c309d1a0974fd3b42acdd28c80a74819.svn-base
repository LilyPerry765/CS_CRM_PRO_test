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
    public partial class PAPDischargeRequestSearchList : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        private void Initialize()
        {
            List<int> centerIDs = Data.CenterDB.GetUserCenters(UserDB.GetUserbyUserName(Context.User.Identity.Name).ID);
            CenterList.Items.Add(new ListItem("-- انتخاب نمایید --", "0"));
            foreach (int centerID in centerIDs)
            {
                Center center = Data.CenterDB.GetCenterById(centerID);
                Region region = Data.RegionDB.GetRegionById(center.RegionID);
                City city = DB.SearchByPropertyName<City>("ID", region.CityID).SingleOrDefault();
                CenterList.Items.Add(new ListItem(city.Name + " : " + center.CenterName, center.ID.ToString()));
            }
        }

        private void Search()
        {
            List<int> centerIDs = Data.CenterDB.GetUserCenters(UserDB.GetUserbyUserName(Context.User.Identity.Name).ID);

            int papId = PAPInfoDB.GetPAPInfoIDbyUserName(Context.User.Identity.Name);

            List<Data.ADSLPAPRequestInfo> PendingRequestInfo = ADSLPAPRequestDB.SearchADSLPAPRequests(papId, TelephoneNoTextBox.Text.Trim(), Convert.ToInt32(CenterList.SelectedValue), centerIDs, (byte)DB.RequestType.ADSLDischargePAPCompany, CustomerNameTextBox.Text, (byte)Convert.ToInt16(CustomerStatusDropDown.SelectedValue), Helper.PersianToGregorian(FromInsertDateTextBox.Text), Helper.PersianToGregorian(ToInsertDateTextBox.Text), 0 /*(byte)Convert.ToInt16(InstalTimeOutList.SelectedValue)*/, Helper.PersianToGregorian(FromEndDateTextBox.Text), Helper.PersianToGregorian(ToEndDateTextBox.Text), (byte)DB.ADSLPAPRequestStatus.Pending);
            List<Data.ADSLPAPRequestInfo> RejectedRequestInfo = ADSLPAPRequestDB.SearchADSLPAPRequests(papId, TelephoneNoTextBox.Text.Trim(), Convert.ToInt32(CenterList.SelectedValue), centerIDs, (byte)DB.RequestType.ADSLDischargePAPCompany, CustomerNameTextBox.Text, (byte)Convert.ToInt16(CustomerStatusDropDown.SelectedValue), Helper.PersianToGregorian(FromInsertDateTextBox.Text), Helper.PersianToGregorian(ToInsertDateTextBox.Text), 0 /*(byte)Convert.ToInt16(InstalTimeOutList.SelectedValue)*/, Helper.PersianToGregorian(FromEndDateTextBox.Text), Helper.PersianToGregorian(ToEndDateTextBox.Text), (byte)DB.ADSLPAPRequestStatus.Reject);
            List<Data.ADSLPAPRequestInfo> CompletedRequestInfo = ADSLPAPRequestDB.SearchADSLPAPRequests(papId, TelephoneNoTextBox.Text.Trim(), Convert.ToInt32(CenterList.SelectedValue), centerIDs, (byte)DB.RequestType.ADSLDischargePAPCompany, CustomerNameTextBox.Text, (byte)Convert.ToInt16(CustomerStatusDropDown.SelectedValue), Helper.PersianToGregorian(FromInsertDateTextBox.Text), Helper.PersianToGregorian(ToInsertDateTextBox.Text), 0 /* (byte)Convert.ToInt16(InstalTimeOutList.SelectedValue)*/, Helper.PersianToGregorian(FromEndDateTextBox.Text), Helper.PersianToGregorian(ToEndDateTextBox.Text), (byte)DB.ADSLPAPRequestStatus.Completed);

            PendingRequestListView.DataSource = PendingRequestInfo;
            PendingRequestListView.DataBind();

            RejectRequestListView.DataSource = RejectedRequestInfo;
            RejectRequestListView.DataBind();

            CompletedRequestListView.DataSource = CompletedRequestInfo;
            CompletedRequestListView.DataBind();
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Initialize();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            TelephoneNoTextBox.Text = string.Empty;
            CenterList.SelectedIndex = 0;
            CustomerNameTextBox.Text = string.Empty;
            CustomerStatusDropDown.SelectedIndex = 0;
            FromInsertDateTextBox.Text = string.Empty;
            ToInsertDateTextBox.Text = string.Empty;
            //InstalTimeOutList.SelectedIndex = 0;
            FromEndDateTextBox.Text = string.Empty;
            ToEndDateTextBox.Text = string.Empty;

            Search();
        }
        
        protected void PersianDateTextBox4_TextChanged(object sender, EventArgs e)
        {
            //Label1.Text = PersianDateTextBox4.DateValue.ToString();
        }

        #endregion
    }
}