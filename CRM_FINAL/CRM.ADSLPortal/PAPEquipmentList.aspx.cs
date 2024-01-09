using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;

namespace CRM.ADSLPortal
{
    public partial class PAPEquipmentList : System.Web.UI.Page
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
        }

        private void Search()
        {
            List<int> centerIDs = Data.CenterDB.GetUserCenters(DB.SearchByPropertyName<User>("UserName", Context.User.Identity.Name).SingleOrDefault().ID);

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            long portNo = -1;
            if (!string.IsNullOrWhiteSpace(PortNoTextBox.Text))
                portNo = Convert.ToInt64(PortNoTextBox.Text);

            User user = DB.SearchByPropertyName<User>("UserName", Context.User.Identity.Name).SingleOrDefault();
            int papId = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault().PAPInfoID;

            EquipmentListView.DataSource = ADSLPAPPortDB.SearchADSLPAPPortForWeb(papId, centerIDs, Convert.ToInt32(CenterList.SelectedValue), telephoneNo, portNo, Convert.ToInt32(StatusDropDown.SelectedValue));
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
            PortNoTextBox.Text = string.Empty;
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