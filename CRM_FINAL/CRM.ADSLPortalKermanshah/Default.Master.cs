using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.ADSLPortalKermanshah
{
    public partial class Default : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            ShowLoginLink();
        }

        private void ShowLoginLink()
        {
            if ((Context != null) &&
            (Context.User != null) &&
            (Context.User.Identity != null) &&
            (Context.User.Identity.IsAuthenticated))
            {
                WelComeLabel.Text = "کاربر گرامی خوش آمدید" + " | ";
                LoginLink.Text = "خروج سیستم";
                LoginLink.PostBackUrl = "Logout.aspx";
                ADSLMenu.Visible = true;
                //AddrequestLink.Visible = true;
                //AddrequestLink.PostBackUrl = "PAPRequestForm.aspx";
                //RequestListLink.Visible = true;
                //RequestListLink.PostBackUrl = "PAPRequestList.aspx";
            }
            //else
            //{
            //    LoginLink.Text = "ورود کاربران";
            //    LoginLink.PostBackUrl = "Login.aspx";
            //}
        }
    }
}