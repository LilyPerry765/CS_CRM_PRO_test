using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace CRM.ADSLPortal
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Methods

        private void ResetControls()
        {
            EmailTextBox.Text = string.Empty;
            UserNameTextBox.Text = string.Empty;
            OldPasswordTextBox.Text = string.Empty;
            NewPasswordTextBox.Text = string.Empty;
            ReNewPasswordTextBox.Text = string.Empty;
            MessageLabel.Visible = false;
            SaveErrorMessageLabel.Text = "";
        }

        private void ShowErrorMessage(string message)
        {
            SaveSuccessMessageLabel.Visible = false;

            SaveErrorMessageLabel.Text = "خطا در تغییر رمز، " + message + " !";
            SaveErrorMessageLabel.Visible = true;
        }

        private void ShowSuccessMessage()
        {
            SaveErrorMessageLabel.Visible = false;

            SaveSuccessMessageLabel.Text = "عملیات ارسال درخواست با موفقیت انجام شد.";
            SaveSuccessMessageLabel.Visible = true;
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void ChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                User user = Data.DB.SearchByPropertyName<Data.User>("UserName", UserNameTextBox.Text.Trim()).SingleOrDefault();
                if (user != null)
                {
                    PAPInfoUser papUser = DB.SearchByPropertyName<PAPInfoUser>("ID", user.ID).SingleOrDefault();

                    if (papUser != null)
                    {
                        if (string.Equals(papUser.Email, EmailTextBox.Text.Trim()))
                        {
                            if (string.Equals(papUser.Password, OldPasswordTextBox.Text.Trim()))
                            {
                                if (string.Equals(NewPasswordTextBox.Text.Trim(), ReNewPasswordTextBox.Text.Trim()))
                                {                                    
                                    string papName = DB.SearchByPropertyName<PAPInfo>("ID", papUser.PAPInfoID).SingleOrDefault().Title;
                                    papUser.Password = NewPasswordTextBox.Text.Trim();

                                    //MailMessage mail = new MailMessage(new MailAddress(EmailTextBox.Text.Trim(), "شرکت مخابرات استان سمنان"),
                                    //                                   new MailAddress(papUser.Email));
                                    //mail.Subject = "تغییر رمز عبور ";
                                    //mail.Body = "<br/><br/>" + "کاربر گرامی شرکت " + papName + "<br/>" + "با سلام،" + "<br/><br/>" + "در پورتال مخابرات رمز عبور شما به شرح زیر تغییر یافته است. " + "<br/><br/>" +
                                    //    "UserName : " + user.UserName + "<br/>" + "Password : " + NewPasswordTextBox.Text.Trim();
                                    //mail.IsBodyHtml = true;

                                    //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                                    //client.EnableSsl = true;
                                    //client.Timeout = 10000;
                                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    //client.UseDefaultCredentials = false;
                                    //client.Credentials = new NetworkCredential("semnan.telecome.adsl@gmail.com", "zxcv@1256");

                                    //client.Send(mail);

                                    papUser.Detach();
                                    Helper.Save(papUser);

                                    ShowSuccessMessage();
                                }
                                else
                                    throw new Exception("تکرار رمز عبور جدید صحیح نمی باشد");
                            }
                            else
                                throw new Exception("رمز عبور قدیم صحیح نمی باشد");
                        }
                        else
                            throw new Exception("آدرس الکترونیکی برای این کاربر صحیح نمی باشد");
                    }
                    else
                        throw new Exception("نام کاربری صحیح نمی باشد");
                }
                else
                    throw new Exception("نام کاربری صحیح نمی باشد");
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

        #endregion
    }
}