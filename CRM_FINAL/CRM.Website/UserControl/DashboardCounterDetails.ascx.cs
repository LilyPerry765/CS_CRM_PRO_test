using CRM.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.Website.UserControl
{
    public partial class DashboardCounterDetails : System.Web.UI.UserControl, IPostBackEventHandler
    {
        #region Properties

        int _stepID = 0;

        public string Header
        {
            set { HeaderLabel.Text = value; }
            get { return HeaderLabel.Text; }
        }

        public int Count
        {
            set { CountLabel.Text = value.ToString(); }
        }

        public DateTime? LastItemDate
        {
            set { DateLabel.Text = Helper.GetPersianDate((DateTime?)value, Helper.DateStringType.DateTime).ToString(); }
        }

        public int StepID
        {
            get { return _stepID; }
            set { _stepID = value; }
        }

        public int ColorNumber { get; set; }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ColorNumber == 0) ColorNumber = 1;
            ContainerPanel.Style.Add("background-image", string.Format("url('../Images/Counter_Grid_Middle_0{0}.png')", (((int)ColorNumber % 6) + 1)));
            if (StepID > 0)
                ContainerPanel.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this, "DashboardCounterDetailClick");
        }

        #region IPostBackEventHandler Members

        public void RaisePostBackEvent(string eventArgument)
        {
            if (!string.IsNullOrEmpty(eventArgument))
            {
                if (eventArgument.ToLower().Contains("dashboardcounterdetailclick"))
                {
                    if (_stepID > 0)
                    {
                        System.Web.UI.Page page = HttpContext.Current.Handler as System.Web.UI.Page;

                        HiddenField ribbonItemTextDummyHidden = page.Master.FindControl("RibbonItemTextDummyHidden") as HiddenField;
                        ribbonItemTextDummyHidden.Value = "کارتابل درخواست";

                        HiddenField ribbonItemIDDummyHidden = page.Master.FindControl("RibbonItemIDDummyHidden") as HiddenField;
                        ribbonItemIDDummyHidden.Value = "RequestsInbox";


                        HiddenField requestStepIDDummyHidden = page.Master.FindControl("RequestStepIDDummyHidden") as HiddenField;
                        requestStepIDDummyHidden.Value = _stepID.ToString();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenRequestInbox", "RibbonItemOnClick();", true);
                    }
                }
            }
        }

        #endregion IPostBackEventHandler Members
    }
}