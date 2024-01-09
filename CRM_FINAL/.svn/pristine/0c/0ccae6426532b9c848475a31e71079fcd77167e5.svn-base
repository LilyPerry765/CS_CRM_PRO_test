using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using System.Transactions;
using System.Collections;
using System.IO;
using AjaxControlToolkit;
using CRM.Application;



namespace CRM.Website.UserControl
{
    public partial class ActionUserControl : System.Web.UI.UserControl
    {

        #region Properties and Fields

        CRM.Website.Classes.StatusBarMessage _StatusBarMessage = new CRM.Website.Classes.StatusBarMessage();
        private Data.Schema.ActionLogRequest _ActionLogRequest = new Data.Schema.ActionLogRequest();
        private string _UserName;

        public List<byte> ActionIDs { get; set; }
        public List<long> ItemIDs { get; set; }
        public byte ListType { get; set; }
        #endregion

        #region Methods

        public void LoadData()
        {
            if (!IsPostBack)
            {
                ActionDropDownList.DataSource = Helper.GetEnumItem(typeof(DB.NewAction)).Where(t => ActionIDs.Contains(t.ID));
                ActionDropDownList.DataBind();
                ActionDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                ActionDropDownList.SelectedIndex = 0;
            }
        }

        public void OnActionCompeleted(bool isSuccessfull, string message, Exception ex)
        {
            if (isSuccessfull)
            {
                _StatusBarMessage.ShowInMaster(message, isSuccessfull);
            }
            else
            {
                if (ex != null)
                {
                    _StatusBarMessage.ShowInMaster(message + " " + ex.Message, isSuccessfull);
                }
                else
                {
                    _StatusBarMessage.ShowInMaster(message, isSuccessfull);
                }
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            _UserName = Folder.User.Current.Username;
        }

        protected void DoAction(object sender, EventArgs e)
        {
            if (ActionDropDownList.SelectedValue == string.Empty)
                return;
            byte actionId = byte.Parse(ActionDropDownList.SelectedValue);

            switch (actionId)
            {
                case (byte)DB.NewAction.Save:
                    ActionPanel.Style.Add("display", "none");
                    SavePanel.Style.Add("display", "block");
                    break;

                case (byte)DB.NewAction.Print:
                    ActionPanel.Style.Add("display", "none");
                    PrintPanel.Style.Add("display", "block");
                    break;

                case (byte)DB.NewAction.Forward:
                    ActionPanel.Style.Add("display", "none");
                    ForwardPanel.Style.Add("display", "block");
                    break;


                case (byte)DB.NewAction.Cancelation:
                    ActionPanel.Style.Add("display", "none");
                    CancelPanel.Style.Add("display", "block");
                    break;

                case (byte)DB.NewAction.Exit:
                    ActionPanel.Style.Add("display", "none");
                    ExitPanel.Style.Add("display", "block");
                    break;

                case (byte)DB.NewAction.SaveWaitingList:
                    ActionPanel.Style.Add("display", "none");
                    SaveWatingListPanel.Style.Add("display", "block");
                    break;

                case (byte)DB.NewAction.Deny:
                    ActionPanel.Style.Add("display", "none");
                    DenyPanel.Style.Add("display", "block");
                    break;

                case (byte)DB.NewAction.Confirm:
                    ActionPanel.Style.Add("display", "none");
                    ConfirmPanel.Style.Add("display", "block");
                    break;

                case (byte)DB.NewAction.Refund:
                    ActionPanel.Style.Add("display", "none");
                    RefundPanel.Style.Add("display", "block");
                    break;

                default:
                    break;
            }
        }

        protected void Reset(object sender, ImageClickEventArgs e)
        {
            ActionDropDownList.ClearSelection();

            ExitPanel.Style.Add("display", "none");
            CancelPanel.Style.Add("display", "none");
            ForwardPanel.Style.Add("display", "none");
            PrintPanel.Style.Add("display", "none");
            SavePanel.Style.Add("display", "none");
            RefundPanel.Style.Add("display", "none");

            ActionPanel.Style.Add("display", "inline-block");
            ActionLabel.Style.Add("display", "inline-block");
            ActionDropDownList.Style.Add("display", "inline-block");
            _StatusBarMessage.ClearInMaster();
        }

        protected void Save(object sender, ImageClickEventArgs e)
        {
            Viewes.RequestForm requestForm = (this.Parent).Parent.FindControl("ContentsPlaceHolder").Page as Viewes.RequestForm;
            if (requestForm != null)
            {
                if (requestForm.Save())
                {
                    _ActionLogRequest.FormType = requestForm.GetType().FullName;
                    _ActionLogRequest.FormName = requestForm.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.Save, _UserName, _ActionLogRequest);
                    Response.Write(string.Format("<script>alert('{0}');</script>", "درخواست با موفقیت ذخیره شد. "));
                    requestForm.CloseRequestForm();
                }
            }
            // Response.Write(string.Format("<script>alert('{0}');</script>", "خطا در ذخیره درخواست "));
            Reset(null, null);
        }

        protected void Print(object sender, ImageClickEventArgs e)
        {
            Reset(null, null);

            //IEnumerable result;
            //CRM.Application.Views.ADSLMDFPortList _ADSLMDFPortList = new ADSLMDFPortList();
            //switch (ListType)
            //{
            //    case ((byte)DB.ListType.ADSLMDFPortList):

            //        result = ReportDB.GetADSLPortsInfo(_ADSLMDFPortList.CityComboBox.SelectedIDs,
            //                                          _ADSLMDFPortList.CenterComboBox.SelectedIDs,
            //                                          _ADSLMDFPortList.StatusComboBox.SelectedIDs,
            //                                          _ADSLMDFPortList.MDFComboBox.SelectedIDs,
            //                                          _ADSLMDFPortList.RowComboBox.SelectedIDs,
            //                                          _ADSLMDFPortList.ColumnComboBox.SelectedIDs,
            //                                          _ADSLMDFPortList.PortComboBox.SelectedIDs,
            //                                          _ADSLMDFPortList.TelephoneNoTextBox.Text.Trim());

            //        SendToPrint(result, (int)DB.UserControlNames.ADSLPortsReport);
            //        break;
            //}
        }

        protected void Forward(object sender, ImageClickEventArgs e)
        {
            try
            {
                Viewes.RequestForm requestForm = (this.Parent).Parent.FindControl("ContentsPlaceHolder").Page as Viewes.RequestForm;
                if (requestForm != null)
                {
                    if (requestForm.Forward())
                    {
                        long requestID = requestForm.RequestID;
                        Request request = Data.RequestDB.GetRequestByID(requestID);

                        Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);

                        _ActionLogRequest.FormType = requestForm.GetType().FullName;
                        _ActionLogRequest.FormName = requestForm.Title;
                        ActionLogDB.AddActionLog((byte)DB.ActionLog.Forward, _UserName, _ActionLogRequest);

                        Response.Write(string.Format("<script>alert('{0}');</script>", "درخواست با موفقیت ذخیره شد. "));
                        requestForm.CloseRequestForm();
                    }
                    else
                        Reset(null, null);
                    //  requestForm.CloseRequestForm();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                Response.Write(string.Format("<script>alert('{0}');</script>", message));
            }
        }

        protected void Cancel(object sender, ImageClickEventArgs e)
        {
            Viewes.RequestForm requestForm = (this.Parent).Parent.FindControl("ContentsPlaceHolder").Page as Viewes.RequestForm;
            if (requestForm != null)
            {
                if (requestForm.Cancel())
                {
                    _ActionLogRequest.FormType = requestForm.GetType().FullName;
                    _ActionLogRequest.FormName = requestForm.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.Cancelation, _UserName, _ActionLogRequest);

                    //requestForm.DialogResult = true;
                    //requestForm.Close();
                    requestForm.CloseRequestForm();
                }
                else
                    Reset(null, null);
            }
        }

        protected void Exit(object sender, ImageClickEventArgs e)
        {
            Viewes.RequestForm requestForm = (this.Parent).Parent.FindControl("ContentsPlaceHolder").Page as Viewes.RequestForm;
            if (requestForm != null)
                requestForm.CloseRequestForm();
        }

        protected void SaveWaitingList(object sender, ImageClickEventArgs e)
        {
            Viewes.RequestForm requestForm = (this.Parent).Parent.FindControl("ContentsPlaceHolder").Page as Viewes.RequestForm;
            if (requestForm != null)
            {
                if (requestForm.SaveWaitingList())
                {
                    long requestID = requestForm.RequestID;
                    Request request = Data.RequestDB.GetRequestByID(requestID);
                   // Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);

                    _ActionLogRequest.FormType = requestForm.GetType().FullName;
                    _ActionLogRequest.FormName = requestForm.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.SaveWaitingList, _UserName, _ActionLogRequest);

                    requestForm.CloseRequestForm();
                }
                else
                    Reset(null, null);
            }
        }

        protected void Confirm(object sender, ImageClickEventArgs e)
        {
            Viewes.RequestForm requestForm = (this.Parent).Parent.FindControl("ContentsPlaceHolder").Page as Viewes.RequestForm;
            if (requestForm != null)
            {
                if (requestForm.Confirm())
                {
                    long requestID = requestForm.RequestID;
                    Request request = Data.RequestDB.GetRequestByID(requestID);
                    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);

                    _ActionLogRequest.FormType = requestForm.GetType().FullName;
                    _ActionLogRequest.FormName = requestForm.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.Confirm, _UserName, _ActionLogRequest);

                    //_CurrentRequestWindow.DialogResult = true;
                    //_CurrentRequestWindow.Close();
                    requestForm.CloseRequestForm();
                }
            }
            Reset(null, null);

        }

        protected void Deny(object sender, ImageClickEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                Viewes.RequestForm requestForm = (this.Parent).Parent.FindControl("ContentsPlaceHolder").Page as Viewes.RequestForm;
                if (requestForm != null)
                {
                    if (requestForm.Deny())
                    {
                        long requestID = requestForm.RequestID;
                        Request request = Data.RequestDB.GetRequestByID(requestID);
                        Data.WorkFlowDB.SetNextState(DB.Action.Reject, request.StatusID, request.ID, null, null, null);

                        _ActionLogRequest.FormType = requestForm.GetType().FullName;
                        _ActionLogRequest.FormName = requestForm.Title;
                        ActionLogDB.AddActionLog((byte)DB.ActionLog.Reject, _UserName, _ActionLogRequest);
                        requestForm.CloseRequestForm();
                    }
                }
                scope.Complete();
            }

            Reset(null, null);
        }

        protected void Refund(object sender, ImageClickEventArgs e)
        {
            try
            {
                Viewes.RequestForm requestForm = (this.Parent).Parent.FindControl("ContentsPlaceHolder").Page as Viewes.RequestForm;
                if (requestForm != null)
                {
                    if (requestForm.Refund())
                    {
                        long requestID = requestForm.RequestID;
                        Request request = Data.RequestDB.GetRequestByID(requestID);

                        Data.WorkFlowDB.SetNextState(DB.Action.AutomaticForward, request.StatusID, request.ID);

                        _ActionLogRequest.FormType = requestForm.GetType().FullName;
                        _ActionLogRequest.FormName = requestForm.Title;
                        ActionLogDB.AddActionLog((byte)DB.ActionLog.Refund, _UserName, _ActionLogRequest);
                        requestForm.CloseRequestForm();
                    }
                    else
                        Reset(null, null);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                Response.Write(string.Format("<script>alert('{0}');</script>", message));
            }
        }

        #endregion
    }
}