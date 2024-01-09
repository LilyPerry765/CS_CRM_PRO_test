using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CRM.Website.Viewes
{
    public partial class CustomerForm : System.Web.UI.Page
    {
        #region Properties

        private long _id;
        private int _centerID = 0;
        private int _customerType = 0;
        private Customer _customer { get; set; }

        #endregion

        #region Methods

        private void LoadData()
        {
            if (_id == 0)
            {
                _customer = new Customer();
                Title = "ثبت مشخصات مشترکین";
                if (_centerID != 0 && _customerType != 0)
                {
                    _customer.CustomerID = DB.GetCustomerID(_centerID, _customerType);
                    PersonCreateCustomerIDTextBox.Text = _customer.CustomerID;
                    CompanyCreateCustomerIDTextBox.Text = _customer.CustomerID;
                }
            }
            else
            {
                Title = "بروز رسانی مشخصات مشترکین";
                _customer = Data.CustomerDB.GetCustomerByID(_id);
                if (_customer.PersonType == (int)DB.PersonType.Company)
                {
                    CompanyRadioButton.Checked = true;
                    TitleTextBox.Text = _customer.FirstNameOrTitle;
                    RecordNoTextBox.Text = _customer.NationalCodeOrRecordNo;
                    PhoneNoTextBox.Text = _customer.UrgentTelNo;
                    CompanyEmailTextBox.Text = _customer.Email;
                    AgencyNoTextBox.Text = _customer.AgencyNumber;
                    NationIDTextBox.Text = _customer.NationalID;
                    RecordDateTextBox.Text = _customer.BirthDateOrRecordDate.ToString();
                    CompanyMobileNoTextBox.Text = _customer.MobileNo;
                    AgencyTextBox.Text = _customer.Agency;
                    CompanyCreateCustomerIDTextBox.Text = _customer.CustomerID;
                }
                else if (_customer.PersonType == (int)DB.PersonType.Person)
                {
                    PersonRadioButton.Checked = true;
                    if (_customer.Gender == (int)DB.Gender.Female)
                        FemaleRadioButton.Checked = true;
                    else if (_customer.Gender == (int)DB.Gender.Male)
                        MaleRadioButton.Checked = true;

                    NationalCodeTextBox.Text = _customer.NationalCodeOrRecordNo;
                    FirstNameTextBox.Text = _customer.FirstNameOrTitle;
                    FatherNameTextBox.Text = _customer.FatherName;
                    IssuePlaceTextBox.Text = _customer.IssuePlace;
                    UrgentTelNoTextBox.Text = _customer.UrgentTelNo;
                    PersonCreateCustomerIDTextBox.Text = _customer.CustomerID;
                    LastNameTextBox.Text = _customer.LastName;
                    BirthCertificateIDTextBox.Text = _customer.BirthCertificateID;
                    BirthDateTextBox.Text = _customer.BirthDateOrRecordDate.ToString();
                    MobileNoTextBox.Text = _customer.MobileNo;
                    EmailTextBox.Text = _customer.Email;
                }
            }
        }
        #endregion

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session["AddedCustomerID"] = string.Empty;

            int.TryParse(Request.QueryString["CenterID"], out _centerID);
            int.TryParse(Request.QueryString["CustomerType"], out _customerType);
            long.TryParse(Request.QueryString["CustomerID"], out _id);

            if (_centerID != 0 && _customerType != 0)
            {
                _customer.CustomerID = DB.GetCustomerID(_centerID, _customerType);
                PersonCreateCustomerIDTextBox.Text = _customer.CustomerID;
                CompanyCreateCustomerIDTextBox.Text = _customer.CustomerID;
            }

            LoadData();
        }

        protected void PersonCreateCustomerIDButton_Click(object sender, EventArgs e)
        {
            var city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
            if (city == "semnan")
            {
                _customer.CustomerID = DB.GetCustomerID(_centerID, _customerType);
                if (PersonRadioButton.Checked)
                    PersonCreateCustomerIDTextBox.Text = _customer.CustomerID;
                else if (CompanyRadioButton.Checked)
                    CompanyCreateCustomerIDTextBox.Text = _customer.CustomerID;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //if (CRM.Application.Codes.Validation.WindowIsValid.IsValid(this) == false)
            //    return;

            try
            {
                if ((CompanyRadioButton.Checked && !string.IsNullOrEmpty(RecordNoTextBox.Text)) || (PersonRadioButton.Checked && !string.IsNullOrEmpty(NationalCodeTextBox.Text)))
                {
                    if (PersonRadioButton.Checked)
                    {
                        if (string.IsNullOrEmpty(PersonCreateCustomerIDTextBox.Text))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "شماره پرونده معتبر نمی باشد" + "');", true);
                            return;
                        }
                        else if (CustomerDB.GetCustomerByCustomerID(PersonCreateCustomerIDTextBox.Text) != null)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "شماره پرونده وارد شده تکراری است" + "');", true);
                            return;
                        }
                        Customer currentCustomer = CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text);
                        if (currentCustomer != null)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "کد ملی وارد شده تکراری است" + "');", true);
                            return;
                        }
                    }
                    else if (CompanyRadioButton.Checked)
                    {
                        if (string.IsNullOrEmpty(CompanyCreateCustomerIDTextBox.Text))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "شماره پرونده معتبر نمی باشد" + "');", true);
                            return;
                        }
                        else if (CustomerDB.GetCustomerByCustomerID(CompanyCreateCustomerIDTextBox.Text) != null)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "شماره پرونده وارد شده تکراری است" + "');", true);
                            return;
                        }
                        Customer currentCustomer = CustomerDB.GetCustomerByNationalCode(RecordNoTextBox.Text);
                        if (currentCustomer != null)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "شماره ثبت وارد شده تکراری است" + "');", true);
                            return;
                        }
                    }



                    DateTime result;

                    if (PersonRadioButton.Checked)
                    {
                        _customer.PersonType = (byte)DB.PersonType.Person;
                        _customer.Gender = FemaleRadioButton.Checked ? (byte)DB.Gender.Female : (byte)DB.Gender.Male;

                        _customer.NationalCodeOrRecordNo = NationalCodeTextBox.Text;
                        _customer.FirstNameOrTitle = FirstNameTextBox.Text;
                        _customer.FatherName = FatherNameTextBox.Text;
                        _customer.IssuePlace = IssuePlaceTextBox.Text;
                        _customer.UrgentTelNo = UrgentTelNoTextBox.Text;
                        _customer.CustomerID = PersonCreateCustomerIDTextBox.Text;
                        _customer.LastName = LastNameTextBox.Text;
                        _customer.BirthCertificateID = BirthCertificateIDTextBox.Text;
                        _customer.BirthDateOrRecordDate = DateTime.TryParse(BirthDateTextBox.Text, out result) ? (DateTime?)DateTime.Parse(BirthDateTextBox.Text) : null;
                        _customer.MobileNo = MobileNoTextBox.Text;
                        _customer.Email = EmailTextBox.Text;
                    }
                    else if (CompanyRadioButton.Checked)
                    {
                        _customer.PersonType = (byte)DB.PersonType.Company;

                        _customer.FirstNameOrTitle = TitleTextBox.Text;
                        _customer.NationalCodeOrRecordNo = RecordNoTextBox.Text;
                        _customer.UrgentTelNo = PhoneNoTextBox.Text;
                        _customer.Email = CompanyEmailTextBox.Text;
                        _customer.AgencyNumber = AgencyNoTextBox.Text;
                        _customer.NationalID = NationIDTextBox.Text;
                        _customer.BirthDateOrRecordDate = DateTime.TryParse(RecordDateTextBox.Text, out result) ? (DateTime?)DateTime.Parse(RecordDateTextBox.Text) : null;
                        _customer.MobileNo = CompanyMobileNoTextBox.Text;
                        _customer.Agency = AgencyTextBox.Text;
                        _customer.CustomerID = CompanyCreateCustomerIDTextBox.Text;
                    }

                    _customer.Detach();
                    DB.Save(_customer);

                    _id = _customer.ID;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "EndAddingCustomerScript", string.Format("window.returnValue = {0}; window.close();", _id), true);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "مقادیر وارد شده در پایگاه داده وجود دارد" + "');", true);
                else
                {
                    string message = ex.Message.Replace("\'", "");
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "خطا در ذخیره اطلاعات : " + message + "');", true);
                }
            }
        }

        protected void PersonRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PersonRadioButton.Checked)
            {
                PersonTypeDiv.Visible = true;
                CompanyTypeDiv.Visible = false;
            }
            else if (!PersonRadioButton.Checked)
            {
                PersonTypeDiv.Visible = false;
                CompanyTypeDiv.Visible = true;
            }
        }

        protected void CompanyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CompanyRadioButton.Checked)
            {
                PersonTypeDiv.Visible = false;
                CompanyTypeDiv.Visible = true;
            }
            else if (!CompanyRadioButton.Checked)
            {
                PersonTypeDiv.Visible = true;
                CompanyTypeDiv.Visible = false;
            }
        }
        #endregion

    }
}