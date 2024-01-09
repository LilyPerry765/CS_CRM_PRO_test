using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.ADSLPortalKermanshah
{
    public partial class Logout : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();

            Response.Redirect("~/Login.aspx");
        }

        #endregion
    }
}