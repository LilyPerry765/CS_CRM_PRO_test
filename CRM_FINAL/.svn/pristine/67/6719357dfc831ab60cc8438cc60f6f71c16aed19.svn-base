using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.ADSLPortalKermanshah
{
    public partial class Home : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        private void Initialize()
        {
            if ((Context != null) &&
            (Context.User != null) &&
            (Context.User.Identity != null) &&
            (Context.User.Identity.IsAuthenticated))
            {
                LoginLink.Visible = false;
            }
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

        #endregion
    }
}