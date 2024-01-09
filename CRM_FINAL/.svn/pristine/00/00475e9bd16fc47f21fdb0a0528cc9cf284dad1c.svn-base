using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM.ADSLPortal
{
    public partial class Login : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string strUsername = UserNameTextBox.Text.Trim();
            string strPassword = PasswordTextBox.Text.Trim();
            bool loginResult = true;
            bool isActiveUser = true;

            bool _result = Helper.AuthenticateUser(strUsername, strPassword, out loginResult, out isActiveUser);

            if (_result)
            {
                Data.User user = Data.DB.SearchByPropertyName<Data.User>("UserName", strUsername).SingleOrDefault();
                user.LastLoginDate = Data.DB.GetServerDate();

                user.Detach();
                Helper.Save(user, false);

                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(strUsername, false);
            }
            else
            {
                ErroRow.Visible = true;

                if (!isActiveUser)
                    messageLabel.Text = "نام کاربری شما غیر فعال می باشد !";
                else
                {
                    if (loginResult)
                        messageLabel.Text = "نام کاربری و / یا رمز عبور صحیح وارد نشده است";
                    else
                        messageLabel.Text = "مجاز به ورود نمی باشید !";
                }
            }
        }

        #endregion
    }
}