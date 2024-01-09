using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;

namespace CRM.Website.Viewes
{
    public partial class SearchCustomer : System.Web.UI.Page
    {
        #region Properties & Fields

        private long _id = 0;
        private int _customerType = 0;
        private Customer _customer;
        private int _centerID = 0;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session["SearchedCustomerID"] = string.Empty;
        }

        protected void SearchPersonRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SearchPersonRadioButton.Checked)
            {
                SearchPersonTypeDiv.Visible = true;
                PersonSearchResultGridView.Visible = true;
                SearchCompanyTypeDiv.Visible = false;
                CompanySearchResultGridView.Visible = false;
            }
            else
            {
                SearchPersonTypeDiv.Visible = false;
                PersonSearchResultGridView.Visible = false;
                SearchCompanyTypeDiv.Visible = true;
                CompanySearchResultGridView.Visible = true;
            }
        }

        protected void SearchCompanyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SearchCompanyRadioButton.Checked)
            {
                SearchPersonTypeDiv.Visible = false;
                PersonSearchResultGridView.Visible = false;
                SearchCompanyTypeDiv.Visible = true;
                CompanySearchResultGridView.Visible = true;
            }
            else
            {
                SearchPersonTypeDiv.Visible = true;
                PersonSearchResultGridView.Visible = true;
                SearchCompanyTypeDiv.Visible = false;
                CompanySearchResultGridView.Visible = false;
            }
        }

        protected void PersonSearchResultGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text != "&nbsp;")
                    e.Row.Cells[6].Text = (e.Row.Cells[6].Text.Trim() == DB.Gender.Female.ToString()) ? "زن" : "مرد";

                if (e.Row.Cells[0].Text.Trim() != string.Empty)
                    e.Row.Attributes["ondblclick"] = string.Format("window.returnValue = {0}; window.close();", e.Row.Cells[0].Text.Trim());
            }
        }

        protected void CompanySearchResultGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text != "&nbsp;")
                {
                    try
                    {
                        DateTime date = DateTime.Parse(e.Row.Cells[4].Text.Substring(0, 19));
                        e.Row.Cells[4].Text = Date.GetPersianDate(date, Date.DateStringType.Short);
                    }
                    catch { }
                }

                if (e.Row.Cells[0].Text.Trim() != string.Empty)
                    e.Row.Attributes["ondblclick"] = string.Format("window.returnValue = {0}; window.close();", e.Row.Cells[0].Text.Trim());
            }
        }

        protected void CustomerInfoDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["personType"] = SearchPersonRadioButton.Checked ? (int)DB.PersonType.Person : (int)DB.PersonType.Company;
            e.InputParameters["nationalCodeOrRecordNo"] = SearchPersonRadioButton.Checked ? NationalCodeTextBox.Text : RecordNoTextBox.Text;
            e.InputParameters["firstNameOrTitle"] = SearchPersonRadioButton.Checked ? FirstNameTextBox.Text : TitleTextBox.Text;
            e.InputParameters["lastName"] = SearchPersonRadioButton.Checked ? LastNameTextBox.Text : string.Empty;
            e.InputParameters["fatherName"] = SearchPersonRadioButton.Checked ? FatherNameTextBox.Text : string.Empty;
            e.InputParameters["BirthCertificateID"] = SearchPersonRadioButton.Checked ? BirthCertificateIDTextBox.Text : string.Empty;
            e.InputParameters["IssuePlace"] = SearchPersonRadioButton.Checked ? IssuePlaceTextBox.Text : string.Empty;
            e.InputParameters["gender"] = SearchPersonRadioButton.Checked ? (FemaleRadioButton.Checked ? (int)DB.Gender.Female : (int)DB.Gender.Male) : -1;
        }
        
        protected void AddCustomerButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenCustomerForm", "OpenCustomerForm();", true);
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SearchPersonRadioButton.Checked)
                {
                    //if (FirstNameTextBox.Text == string.Empty)
                    //    throw new Exception("لطفا نام را وارد نمایید");
                    //else if (LastNameTextBox.Text == string.Empty)
                    //    throw new Exception("لطفا نام خانوادگی را نیز وارد نمایید");

                    PersonSearchResultGridView.DataBind();
                    PersonSearchResultGridView.Visible = true;
                    CompanySearchResultGridView.Visible = false;
                }
                else if (SearchCompanyRadioButton.Checked)
                {
                    //if (TitleTextBox.Text == string.Empty)
                    //    throw new Exception("لطفا عنوان را وارد نمایید");
                    CompanySearchResultGridView.DataBind();
                    CompanySearchResultGridView.Visible = true;
                    PersonSearchResultGridView.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "خطا در جستجو!" + message + " !" + "');", true);
            }
        }

        protected void CustomerCreated(object sender, EventArgs e)
        {
            long customerID = 0;
            long.TryParse(this.DummyHidden.Value, out customerID);
            if (customerID > 0)
            {
                _customer = CustomerDB.GetCustomerByID(customerID);
                NationalCodeTextBox.Text = _customer.NationalCodeOrRecordNo;

                if (_customer.PersonType == (byte)DB.PersonType.Person)
                {
                    SearchPersonRadioButton.Checked = true;
                    SearchCompanyRadioButton.Checked = false;
                    if (_customer.Gender == (byte)DB.Gender.Female)
                        FemaleRadioButton.Checked = true;
                    else
                        MaleRadioButton.Checked = true;

                    SearchPersonTypeDiv.Visible = true;
                    SearchCompanyTypeDiv.Visible = false;

                    FirstNameTextBox.Text = _customer.FirstNameOrTitle;
                    NationalCodeTextBox.Text = _customer.NationalCodeOrRecordNo;
                    FatherNameTextBox.Text = _customer.FatherName;
                    LastNameTextBox.Text = _customer.LastName;
                    BirthCertificateIDTextBox.Text = _customer.BirthCertificateID;
                    IssuePlaceTextBox.Text = _customer.IssuePlace;

                }
                else
                {
                    SearchCompanyRadioButton.Checked = true;
                    SearchPersonRadioButton.Checked = false;

                    SearchPersonTypeDiv.Visible = false;
                    SearchCompanyTypeDiv.Visible = true;

                    TitleTextBox.Text = _customer.FirstNameOrTitle;
                    RecordNoTextBox.Text = _customer.NationalCodeOrRecordNo;
                }

                SearchButton_Click(null, null);
            }
        }

        #endregion
    }
}