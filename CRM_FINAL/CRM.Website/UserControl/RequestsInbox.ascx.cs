using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Application;
using System.Globalization;
using CRM.Data;


namespace CRM.Website.UserControl
{
    public partial class RequestsInbox : System.Web.UI.UserControl
    {
        #region Properties and Fields

        Classes.StatusBarMessage _StatusBarMessageClass = new Classes.StatusBarMessage();

        public static bool _IsExpanded = false;

        ListItem _FirstItem = new ListItem(string.Empty, string.Empty, true);

        public int SelectedStepID
        {
            set
            {
                _selectedStepID = value;
                if (_selectedStepID > 0)
                    _requestTypeID = Data.RequestStepDB.GetRequestStepByID(_selectedStepID).RequestTypeID;
            }
        }
        private int _requestTypeID = 0;
        private int _selectedStepID = 0;
        #endregion

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (!IsPostBack)
            //{
            LoadData();
            ExpandImageButton.ImageUrl = "~/Images/collapse.png";
            FullSearchPanel.Visible = true;
            _IsExpanded = true;
            // }


            //if (_selectedStepID > 0)
            //{
            //    StepDropDownList.Items.FindByValue(_selectedStepID.ToString()).Selected = true;
            //    StepDropDownList.Enabled = false;
            //}

            //if (_requestTypeID > 0)
            //{
            //    RequestTypeDropDownList.Items.FindByValue(_requestTypeID.ToString()).Selected = true;
            //    RequestTypeDropDownList.Enabled = false;
            //}
        }
        protected void ExpandImageButton_Click(object sender, ImageClickEventArgs e)
        {
            _IsExpanded = !_IsExpanded;
            if (_IsExpanded)
            {
                ExpandImageButton.ImageUrl = "~/Images/collapse.png";
                FullSearchPanel.Visible = true;
                try
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "key", "createCalendar();", true);
                }
                catch { }
            }
            else
            {
                ExpandImageButton.ImageUrl = "~/Images/expand.png";
                FullSearchPanel.Visible = false;
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            _StatusBarMessageClass.ClearInMaster();
            RequestTypeDropDownList.SelectedIndex = RequestTypeDropDownList.Items.Count - 1;
            CentersDropDownList.SelectedIndex = CentersDropDownList.Items.Count - 1;
            StepDropDownList.SelectedIndex = StepDropDownList.Items.Count - 1;
            PaymentTypeDropDownList.SelectedIndex = PaymentTypeDropDownList.Items.Count - 1;

            IDTextBox.Text = string.Empty;
            RequestStartDateTextBox.Text = string.Empty;
            RequestEndDateTextBox.Text = string.Empty;
            ModifyStartDateTextBox.Text = string.Empty;
            ModifyEndDateTextBox.Text = string.Empty;
            TelephoneNoTextBox.Text = string.Empty;
            RequesterNameTextBox.Text = string.Empty;
            RequestLetterNoTextBox.Text = string.Empty;
            CustomerNameTextBox.Text = string.Empty;
            LetterDateTextBox.Text = string.Empty;

            InquiryModeCheckBoxCheckBox.Checked = false;

            try
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "key", "createCalendar();", true);
            }
            catch { }
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            //SearchResultGridView.PageSize = 20;
            // SearchResultGridView.DataSource = RequestDB.SearchRequests(IDTextBox.Text.Trim(), TelephoneNoTextBox.Text.Trim(), Helper.PersianToGregorian(RequestStartDateTextBox.Text), Helper.PersianToGregorian(RequestEndDateTextBox.Text), Helper.PersianToGregorian(ModifyStartDateTextBox.Text), Helper.PersianToGregorian(ModifyEndDateTextBox.Text), requestTypesIDs, centerIDs, CustomerNameTextBox.Text, RequesterNameTextBox.Text, paymentTypesIDs, stepIDs, RequestLetterNoTextBox.Text, Helper.PersianToGregorian(LetterDateTextBox.Text), isInquiryMode, isArchived);
            SearchResultGridView.DataBind();
        }
        protected void RequestInboxDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

            _IsExpanded = true;

            List<int> requestTypesIDs = new List<int>();
            List<int> centerIDs = new List<int>();
            List<int> paymentTypesIDs = new List<int>();
            List<int> stepIDs = new List<int>();

            if (RequestTypeDropDownList.SelectedItem != null & RequestTypeDropDownList.SelectedItem.Text.Trim() != string.Empty)
            {
                requestTypesIDs.Add(int.Parse(RequestTypeDropDownList.SelectedItem.Value));
            }
            if (CentersDropDownList.SelectedItem != null & CentersDropDownList.SelectedItem.Text.Trim() != string.Empty)
            {
                centerIDs.Add(int.Parse(CentersDropDownList.SelectedItem.Value));
            }
            //if (PaymentTypeDropDownList.SelectedItem != null & PaymentTypeDropDownList.SelectedItem.Text.Trim() != string.Empty)
            //{
            //    paymentTypesIDs.Add(int.Parse(PaymentTypeDropDownList.SelectedItem.Value));
            //}
            if (StepDropDownList.SelectedItem != null & StepDropDownList.SelectedItem.Text.Trim() != string.Empty)
            {
                stepIDs.Add(int.Parse(StepDropDownList.SelectedItem.Value));
            }

            e.InputParameters["id"] = IDTextBox.Text.Trim();
            e.InputParameters["telephoneNo"] = TelephoneNoTextBox.Text.Trim();
            e.InputParameters["requestStartDate"] = Helper.PersianToGregorian(RequestStartDateTextBox.Text);
            e.InputParameters["requestEndDate"] = Helper.PersianToGregorian(RequestEndDateTextBox.Text);
            e.InputParameters["modifyStartDate"] = Helper.PersianToGregorian(ModifyStartDateTextBox.Text);
            e.InputParameters["modifyEndDate"] = Helper.PersianToGregorian(ModifyEndDateTextBox.Text);
            e.InputParameters["requestTypesIDs"] = _requestTypeID > 0 ? new List<int> { _requestTypeID } : requestTypesIDs;
            e.InputParameters["centerIDs"] = centerIDs;
            e.InputParameters["customerName"] = CustomerNameTextBox.Text;
            e.InputParameters["requesterName"] = RequesterNameTextBox.Text;
            e.InputParameters["paymentTypesIDs"] = paymentTypesIDs;
            e.InputParameters["stepIDs"] = _selectedStepID > 0 ? new List<int> { _selectedStepID } : stepIDs;
            e.InputParameters["requestLetterNo"] = RequestLetterNoTextBox.Text;
            e.InputParameters["letterDate"] = Helper.PersianToGregorian(LetterDateTextBox.Text);
            e.InputParameters["isInquiryMode"] = InquiryModeCheckBoxCheckBox.Checked;
            e.InputParameters["isArchived"] = false;
        }

        protected void SearchResultGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text.Trim() != string.Empty)
                {
                    Session["SelectedStepID"] = _selectedStepID;
                    e.Row.Attributes["ondblclick"] = string.Format("ModalDialog('/Viewes/RequestForm.aspx?RequestID={0}',null,750,785); __doPostBack('ctl00$RequestsInboxPageView_userControl$DummyLink','');", e.Row.Cells[0].Text.Trim());
                }
            }
        }

        protected void DummyLink_Click(object sender, EventArgs e)
        {
            if (Session["SelectedStepID"] != null)
                int.TryParse(Session["SelectedStepID"].ToString(), out _selectedStepID);
            Search();
        }
        #endregion

        #region methods

        private void LoadData()
        {
            _FirstItem.Selected = true;
            _StatusBarMessageClass.ClearInMaster();

            if (PaymentTypeDropDownList.Items.Count == 0)
            {
                PaymentTypeDropDownList.DataSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
                PaymentTypeDropDownList.DataBind();
                PaymentTypeDropDownList.Items.Add(_FirstItem);
            }

            if (CentersDropDownList.Items.Count == 0)
            {
                CentersDropDownList.DataSource = CenterDB.GetCenterCheckable();
                CentersDropDownList.DataBind();
                CentersDropDownList.Items.Add(_FirstItem);
            }


            if (_selectedStepID > 0)
            {
                StepDropDownList.DataSource = Data.WorkFlowDB.GetRequestStepsCheckableByIDs(new List<int> { _requestTypeID });
                StepDropDownList.DataBind();
                StepDropDownList.Items.Add(_FirstItem);
                StepDropDownList.Enabled = false;
            }
            else
            {
                StepDropDownList.DataSource = Data.WorkFlowDB.GetRequestStepsCheckable();
                StepDropDownList.DataBind();
                StepDropDownList.Items.Add(_FirstItem);
                StepDropDownList.Enabled = true;
            }



            if (_requestTypeID > 0)
            {
                RequestTypeDropDownList.DataSource = TypesDB.GetRequestTypeCheckableByID(new List<int> { _requestTypeID });
                RequestTypeDropDownList.DataBind();
                RequestTypeDropDownList.Items.Add(_FirstItem);
                RequestTypeDropDownList.Enabled = false;
            }
            else
            {
                RequestTypeDropDownList.DataSource = TypesDB.GetRequestTypesCheckable();
                RequestTypeDropDownList.DataBind();
                RequestTypeDropDownList.Items.Add(_FirstItem);
                RequestTypeDropDownList.Enabled = true;
            }
            //ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Delete, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward };
        }

        public void Search()
        {
            SearchResultGridView.DataBind();
            if (_requestTypeID > 0)
            {
                RequestTypeDropDownList.DataSource = TypesDB.GetRequestTypeCheckableByID(new List<int> { _requestTypeID });
                RequestTypeDropDownList.DataBind();
                RequestTypeDropDownList.Items.Add(_FirstItem);
                RequestTypeDropDownList.Enabled = false;
            }

            if (_selectedStepID > 0)
            {
                StepDropDownList.DataSource = Data.WorkFlowDB.GetRequestStepsCheckableByIDs(new List<int> { _requestTypeID });
                StepDropDownList.DataBind();
                StepDropDownList.Items.Add(_FirstItem);
                StepDropDownList.Enabled = false;
            }
        }

        #endregion
    }
}