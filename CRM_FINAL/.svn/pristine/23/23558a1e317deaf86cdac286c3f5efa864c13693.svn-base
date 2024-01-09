using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECP.PersianMessageBox;
using CRM.Data;

namespace CRM.Website.Viewes
{
    public partial class CustomerSearchForm : System.Web.UI.Page
    {
        #region Peoperties & Fields
        private long _id = 0;
        private Customer _customer;
        #endregion

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request.QueryString["CustomerID"], out _id);
            if (_id == 0)
                _customer = new Customer();

            else
            {
                _customer = Data.CustomerDB.GetCustomerByID(_id);

                if (_customer.PersonType == (int)DB.PersonType.Person)
                    PersonRadioButton.Checked = true;
                else if (_customer.PersonType == (int)DB.PersonType.Company)
                    CompanyRadioButton.Checked = true;

                FirstNameTextBox.Text = _customer.FirstNameOrTitle;
                LastNameTextBox.Text = _customer.LastName;
                NationalCodeTextBox.Text = _customer.NationalCodeOrRecordNo;
                FatherNameTextBox.Text = _customer.FatherName;
                BirthCertificateIDTextBox.Text = _customer.BirthCertificateID;
                IssuePlaceTextBox.Text = _customer.IssuePlace;

                TitleTextBox.Text = _customer.FirstNameOrTitle;
                RecordNoTextBox.Text = _customer.NationalCodeOrRecordNo;

                FemaleRadioButton.Checked = _customer.Gender == (int)DB.Gender.Female;
                MaleRadioButton.Checked = _customer.Gender == (int)DB.Gender.Male;
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PersonRadioButton.Checked)
                {
                    if (FirstNameTextBox.Text == string.Empty)
                        throw new Exception("لطفا نام را وارد نمایید");
                    else if (LastNameTextBox.Text == string.Empty)
                        throw new Exception("لطفا نام خانوادگی را نیز وارد نمایید");

                    PersonSearchResultGridView.DataBind();
                }
                else if (CompanyRadioButton.Checked)
                {
                    if (TitleTextBox.Text == string.Empty)
                        throw new Exception("لطفا عنوان را وارد نمایید");

                    CompanySearchResultGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                PersianMessageBox.Show("خطا در جستجو!" + Environment.NewLine + ex.Message, "خطا");
            }
        }

        protected void PersonRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PersonRadioButton.Checked)
            {
                PersonTypeDiv.Visible = true;
                PersonSearchResultGridView.Visible = true;
                CompanyTypeDiv.Visible = false;
                CompanySearchResultGridView.Visible = false;
            }
            else
            {
                PersonTypeDiv.Visible = false;
                PersonSearchResultGridView.Visible = false;
                CompanyTypeDiv.Visible = true;
                CompanySearchResultGridView.Visible = true;
            }
        }

        protected void CompanyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CompanyRadioButton.Checked)
            {
                PersonTypeDiv.Visible = false;
                PersonSearchResultGridView.Visible = false;
                CompanyTypeDiv.Visible = true;
                CompanySearchResultGridView.Visible = true;
            }
            else
            {
                PersonTypeDiv.Visible = true;
                PersonSearchResultGridView.Visible = true;
                CompanyTypeDiv.Visible = false;
                CompanySearchResultGridView.Visible = false;
            }
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (PersonSearchResultGridView.SelectedValue as Customer != null)
                HttpContext.Current.Session["CustomerID"] = (PersonSearchResultGridView.SelectedValue as Customer).ID;



        }

        protected void AddCustomerButton_Click(object sender, EventArgs e)
        {
            //CustomerForm customerForm = new CustomerForm();
            //customerForm.ShowDialog();

            //if (customerForm.DialogResult ?? false)
            //    ID = customerForm.ID;

            //this.DialogResult = true;
        }

        protected void RequestInboxDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["personType"] = PersonRadioButton.Checked ? new List<int>() { (int)DB.PersonType.Person } : new List<int>() { (int)DB.PersonType.Company };
            e.InputParameters["nationalCodeOrRecordNo"] = PersonRadioButton.Checked ? NationalCodeTextBox.Text : RecordNoTextBox.Text;
            e.InputParameters["firstNameOrTitle"] = PersonRadioButton.Checked ? FirstNameTextBox.Text : TitleTextBox.Text;
            e.InputParameters["lastName"] = PersonRadioButton.Checked ? LastNameTextBox.Text : string.Empty;
            e.InputParameters["fatherName"] = PersonRadioButton.Checked ? FatherNameTextBox.Text : string.Empty;
            e.InputParameters["gender"] = PersonRadioButton.Checked ? (FemaleRadioButton.Checked ? new List<int>() { (int)DB.Gender.Female } : new List<int>() { (int)DB.Gender.Male }) : new List<int>();
            e.InputParameters["birthCertificateID"] = PersonRadioButton.Checked ? BirthCertificateIDTextBox.Text : string.Empty;
            e.InputParameters["birthDateOrRecord"] = null;
            e.InputParameters["issuePlace"] = PersonRadioButton.Checked ? IssuePlaceTextBox.Text : string.Empty;
            e.InputParameters["urgentTelNo"] = string.Empty;
            e.InputParameters["mobileNo"] = string.Empty;
            e.InputParameters["email"] = string.Empty;
            e.InputParameters["startRowIndex"] = 0;
            e.InputParameters["pageSize"] = 10;
        }

        protected void PersonSearchResultGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text != "&nbsp;")
                {
                    e.Row.Cells[5].Text = (e.Row.Cells[5].Text.Trim() == DB.Gender.Female.ToString()) ? "زن" : "مرد";
                }
            }
           
        }
        #endregion Event Handlers
    }
}