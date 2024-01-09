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
    public partial class SearchRequestList : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        private void Initialize()
        {
            //List<int> centerIDs = Data.CenterDB.GetUserCenters(UserDB.GetUserbyUserName(Context.User.Identity.Name).ID);
            //CenterList.Items.Add(new ListItem("-- انتخاب نمایید --", "0"));
            //foreach (int centerID in centerIDs)
            //{
            //    Center center = Data.CenterDB.GetCenterById(centerID);
            //    Region region = Data.RegionDB.GetRegionById(center.RegionID);
            //    City city = CityDB.GetCitybyCityID(region.CityID);
            //    CenterList.Items.Add(new ListItem(city.Name + " : " + center.CenterName, center.ID.ToString()));
            //}
        }

        private void Search()
        {
            try
            {
                ErrorLabel.Visible = false;
                PendingRequestListView.DataSource = null;
                PendingRequestListView.DataBind();

                if (string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text.Trim()))
                    throw new Exception("لطفا شماره تلفن مورد نظر را وارد نمایید !");

                List<int> centerIDs = Data.CenterDB.GetUserCenters(UserDB.GetUserbyUserName(Context.User.Identity.Name).ID);

                int papId = PAPInfoDB.GetPAPInfoIDbyUserName(Context.User.Identity.Name);

                List<Data.ADSLPAPRequestInfo> PendingRequestInfo = ADSLPAPRequestDB.SearchADSLPAPRequestbytelephoneNoforWeb(papId, TelephoneNoTextBox.Text.Trim(), 0, centerIDs, -1, null, null, null, null/* Helper.PersianToGregorian(FromInsertDateTextBox.Text), Helper.PersianToGregorian(ToInsertDateTextBox.Text), Helper.PersianToGregorian(FromEndDateTextBox.Text), Helper.PersianToGregorian(ToEndDateTextBox.Text)*/);

                if (PendingRequestInfo != null && PendingRequestInfo.Count != 0)
                {
                    PendingRequestListView.DataSource = PendingRequestInfo;
                    PendingRequestListView.DataBind();
                }
                else
                    throw new Exception("رکوردی یافت نشد !");
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = ex.Message;
                ErrorLabel.Visible = true;
            }
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Context != null) &&
                (Context.User != null) &&
                (Context.User.Identity != null) &&
                (Context.User.Identity.IsAuthenticated))
            {
                if (!Page.IsPostBack)
                {
                    Initialize();
                }

                ErrorLabel.Visible = false;
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            ErrorLabel.Visible = false;
            TelephoneNoTextBox.Text = string.Empty;
            //CenterList.SelectedIndex = 0;
            //CustomerNameTextBox.Text = string.Empty;
            //CustomerStatusDropDown.SelectedIndex = 0;
            //FromInsertDateTextBox.Text = string.Empty;
            //ToInsertDateTextBox.Text = string.Empty;
            //InstalTimeOutList.SelectedIndex = 0;
            //FromEndDateTextBox.Text = string.Empty;
            //ToEndDateTextBox.Text = string.Empty;

            //Search();
        }

        protected void PersianDateTextBox4_TextChanged(object sender, EventArgs e)
        {
            //Label1.Text = PersianDateTextBox4.DateValue.ToString();
        }

        #endregion
    }
}