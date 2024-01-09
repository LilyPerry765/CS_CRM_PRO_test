using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;

namespace CRM.ADSLPortalKermanshah
{
    public partial class PAPEquipmentList : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        private void Initialize()
        {
            List<int> centerIDs = Data.CenterDB.GetUserCenters(UserDB.GetUserbyUserName(Context.User.Identity.Name).ID);          
            CenterList.Items.Add(new ListItem("-- انتخاب نمایید --", "0"));
            foreach (int item in centerIDs)
            {
                Center center = DB.SearchByPropertyName<Center>("ID", item).SingleOrDefault();
                Region region = DB.SearchByPropertyName<Region>("ID", center.RegionID).SingleOrDefault();
                City city = DB.SearchByPropertyName<City>("ID", region.CityID).SingleOrDefault();
                CenterList.Items.Add(new ListItem(city.Name + " : " + center.CenterName, center.ID.ToString()));
            }
        }

        private void Search()
        {
            List<int> centerIDs = Data.CenterDB.GetUserCenters(UserDB.GetUserbyUserName(Context.User.Identity.Name).ID);

            long rowNo = -1;
            if (!string.IsNullOrWhiteSpace(RowNoTextBox.Text))
                rowNo = Convert.ToInt64(RowNoTextBox.Text);

            long columnNo = -1;
            if (!string.IsNullOrWhiteSpace(ColumnNoTextBox.Text))
                columnNo = Convert.ToInt64(ColumnNoTextBox.Text);

            long buchtNo = -1;
            if (!string.IsNullOrWhiteSpace(BuchtNoTextBox.Text))
                buchtNo = Convert.ToInt64(BuchtNoTextBox.Text);

            //User user = UserDB.GetUserbyUserName(Context.User.Identity.Name);
            //PAPInfoUserInfo papUser = PAPInfoUserDB.GetPAPInfoUserByID(user.ID);
            //PAPInfo pap = PAPInfoDB.GetPAPInfoByID(papUser.PAPInfoID);
            int papId = PAPInfoDB.GetPAPInfoIDbyUserName(Context.User.Identity.Name);

            EquipmentListView.DataSource = ADSLPAPPortDB.SearchADSLPAPBuchtForWeb(papId, centerIDs, Convert.ToInt32(CenterList.SelectedValue), TelephoneNoTextBox.Text.Trim(), rowNo, columnNo, buchtNo, Convert.ToInt32(StatusDropDown.SelectedValue));
            EquipmentListView.DataBind();
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
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            CenterList.SelectedIndex = 0;
            TelephoneNoTextBox.Text = string.Empty;
            RowNoTextBox.Text = string.Empty;
            ColumnNoTextBox.Text = string.Empty;
            BuchtNoTextBox.Text = string.Empty;
            StatusDropDown.SelectedIndex = 0;

            Search();
        }

        protected void RequestPager_PreRender(object sender, EventArgs e)
        {
            Search();
        }

        #endregion
    }
}