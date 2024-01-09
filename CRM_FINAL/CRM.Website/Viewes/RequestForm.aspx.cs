using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using CRM.Application;

using System.Data.Linq;
using CRM.Website.UserControl;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using CookComputing.XmlRpc;
using CRM.Data.Services;
using System.Collections;
using System.Xml.Linq;

namespace CRM.Website.Viewes
{
    public partial class RequestForm : System.Web.UI.Page
    {
        #region Property & Field

        private Data.Schema.ActionLogRequest _ActionLogRequest = new Data.Schema.ActionLogRequest();
        private long _RelatedRequestID;
        private RequestType _RequestType;
        private long _RequestID;
        private int _CityID = 0;
        private long _CustomerID;

        private byte _Mode;
        private long _TelephoneNo;
        private byte _RequestTypeID;

        private Telephone _Telephone;
        private Customer _Customer;
        private Request _Request;
        private int _RequestCenterID = 0;
        private byte[] _FileBytes { get; set; }
        private List<UsedDocs> _RefDocs;
        private bool _IsOtherCost { get; set; }
        private long _PaymentID = 0;
        private int _OtherCostID = 0;
        private OtherCost _OtherCost { get; set; }

        private UserControl.ADSL _ADSL;
        private UserControl.ADSLChangeService _ADSLChangeService;
        private bool _IsSalable = true;
        private bool _IsForward = false;
        private List<byte> _ActionIDs { get; set; }

        public bool IsSaveSuccess = false;
        public bool IsForwardSuccess = false;
        public bool IsRefundSuccess = false;
        public bool IsCancelSuccess = false;
        public bool IsPrintSuccess = false;
        public bool IsConfirmSuccess = false;
        public bool IsRejectSuccess = false;
        public bool IsSaveWatingListSuccess = false;
        public long RequestID
        {
            get { return _RequestID; }
            set { _RequestID = value; }
        }
        public Data.InstallRequest InstallReqeust { get; set; }
        public List<VisitAddress> VisitInfoList { get; set; }


        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request.QueryString["RequestID"], out _RequestID);

            byte.TryParse(Request.QueryString["RequestTypeID"], out _RequestTypeID);
            long.TryParse(Request.QueryString["TelephoneNo"], out _TelephoneNo);
            byte.TryParse(Request.QueryString["Mode"], out _Mode);

            long.TryParse(Request.QueryString["RelatedRequestID"], out _RelatedRequestID);
            long.TryParse(Request.QueryString["CustomerID"], out _CustomerID);

            if (Request.QueryString["RequestTypeID"] != null && Request.QueryString["TelephoneNo"] != null && Request.QueryString["Mode"] != null)
            {
                _RequestType = Data.RequestTypeDB.GetRequestTypeByRequestType(_RequestTypeID);

                if (_TelephoneNo != 0)
                {
                    TelephoneInformationControl.TelephoneNo = _TelephoneNo;
                    TelephoneInformationControl.RequestTypeID = _RequestTypeID;
                    TelephoneInformationControl.LoadData();

                    switch (_RequestTypeID)
                    {
                        case (byte)DB.RequestType.ADSL:
                            _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);

                            if (_Telephone != null)
                                _Customer = Data.CustomerDB.GetCustomerByID(_Telephone.CustomerID ?? 0);

                            if (_Customer == null)
                            {
                                Service1 service = new Service1();
                                System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

                                //Customer = CustomerDB.GetCustomerbyElkaID(Convert.ToInt64(telephoneInfo.Rows[0]["FI_CODE"].ToString()));
                                if (telephoneInfo.Rows.Count > 0)
                                {
                                    string FI_CODE = string.Empty;

                                    try
                                    {
                                        FI_CODE = ToStringSpecial(telephoneInfo.Rows[0]["FI_CODE"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        FI_CODE = string.Empty;
                                    }

                                    if (!string.IsNullOrWhiteSpace(FI_CODE))
                                        _Customer = CustomerDB.GetCustomerbyElkaID(Convert.ToInt64(telephoneInfo.Rows[0]["FI_CODE"].ToString()));

                                    if (_Customer == null)
                                    {
                                        _Customer = new Customer();

                                        _Customer.NationalCodeOrRecordNo = telephoneInfo.Rows[0]["MELLICODE"].ToString();
                                        _Customer.FirstNameOrTitle = telephoneInfo.Rows[0]["FirstName"].ToString();
                                        _Customer.LastName = telephoneInfo.Rows[0]["Lastname"].ToString();
                                        _Customer.FatherName = telephoneInfo.Rows[0]["FATHERNAME"].ToString();
                                        _Customer.BirthCertificateID = telephoneInfo.Rows[0]["SHENASNAME"].ToString();
                                        _Customer.MobileNo = telephoneInfo.Rows[0]["MOBILE"].ToString();
                                        _Customer.Email = telephoneInfo.Rows[0]["EMAIL"].ToString();
                                        _Customer.PersonType = (telephoneInfo.Rows[0]["CustumerType"].ToString() == "1") ? (byte)Convert.ToInt16(0) : (byte)Convert.ToInt16(1);
                                        // Customer.ElkaID = Convert.ToInt64(telephoneInfo.Rows[0]["FI_CODE"].ToString());

                                        int centerID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString()));
                                        _Customer.CustomerID = DB.GetCustomerID(centerID, (byte)DB.PersonType.Person);

                                        _Customer.Detach();
                                        DB.Save(_Customer, true);
                                    }

                                }
                            }

                            break;

                        default:
                            _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                            _Customer = Data.CustomerDB.GetCustomerByID(_Telephone.CustomerID ?? 0);
                            break;
                    }
                }

                _Request = new Request();
            }

            else if (Request.QueryString["RelatedRequestID"] != null && Request.QueryString["CustomerID"] != null)
            {
                _Request = new Request();
                _Customer = Data.CustomerDB.GetCustomerByID(_CustomerID);
                _Request.CustomerID = _Customer.ID;
                _RequestType = Data.RequestTypeDB.GetRequestTypeByRequestType((int)DB.RequestType.Dayri);
                _Request.RelatedRequestID = _RelatedRequestID;
            }

            else if (Request.QueryString["RequestID"] != null)
            {
                _Request = Data.RequestDB.GetRequestByID(_RequestID);
                if (_Request.CustomerID != null)
                {
                    _Customer = Data.CustomerDB.GetCustomerByID(_Request.CustomerID ?? 0);
                    _CustomerID = _Customer.ID;
                }

                _RefDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == _Customer.ID).ToList();
                if (_Request != null && _Request.TelephoneNo != null && _Request.TelephoneNo != 0)
                {
                    if (_Request.RequestTypeID != (int)DB.RequestType.ADSL)
                    {
                        _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_Request.TelephoneNo ?? 0);
                        if (_Customer == null)
                            _Customer = Data.CustomerDB.GetCustomerByID(_Telephone.CustomerID ?? 0);
                    }
                    else
                        _Customer = Data.CustomerDB.GetCustomerByID(_Request.CustomerID ?? 0);

                    TelephoneInformationControl.TelephoneNo = _Request.TelephoneNo ?? 0;
                    TelephoneInformationControl.RequestTypeID = _Request.RequestTypeID;
                    TelephoneInformationControl.LoadData();
                }
            }


            CityDropDownList.DataSource = Data.CityDB.GetAvailableCityCheckable();
            CityDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            CityDropDownList.DataBind();

            // OtherCostTitleDropDownList = Data.OtherCostDB.GetOtherCostCheckable();

            //PaymentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            //CostTitleColumn.ItemsSource = Data.BaseCostDB.GetBaseCostCheckable();
            //OtherCostTitleColumn.ItemsSource = Data.OtherCostDB.GetOtherCostCheckable();

            _ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
            ActionUserControl.ActionIDs = _ActionIDs;
            ActionUserControl.LoadData();

            _ActionLogRequest.FormType = this.GetType().FullName;
            _ActionLogRequest.FormName = this.Title;
            ActionLogDB.AddActionLog((byte)DB.ActionLog.View, DB.CurrentUser.UserName, _ActionLogRequest);

            LoadData();

        }

        protected void CityDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_CityID == 0 && !string.IsNullOrEmpty(CityDropDownList.SelectedValue))
            {
                City city = Data.CityDB.GetCityById(int.Parse(CityDropDownList.SelectedValue));
                CenterNameDropDownList.DataSource = Data.CenterDB.GetCenterByCityId(city.ID);
                CenterNameDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                CenterNameDropDownList.DataBind();

                _Request.CenterID = int.Parse(CenterNameDropDownList.Items[0].Value);
                CenterNameDropDownList.ClearSelection();
            }
            else
            {
                if (string.IsNullOrEmpty(CityDropDownList.SelectedValue))
                {
                    City city = Data.CityDB.GetCityById(_CityID);
                    CenterNameDropDownList.DataSource = Data.CenterDB.GetCenterByCityId(city.ID);
                    CenterNameDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    CenterNameDropDownList.DataBind();
                }
                else
                {
                    City city = Data.CityDB.GetCityById(int.Parse(CityDropDownList.SelectedValue));
                    CenterNameDropDownList.DataSource = Data.CenterDB.GetCenterByCityId(city.ID);
                    CenterNameDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    CenterNameDropDownList.DataBind();
                }
            }

            try
            {
                CenterNameDropDownList.SelectedValue = _Request.CenterID.ToString();
            }
            catch { }
        }

        protected void RequestDocGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (index < 0)
                return;

            switch (e.CommandName)
            {
                case "InsertDocument":
                    {
                        DocumentsByCustomerForWeb docInfo = (RequestDocGridView.DataSource as List<DocumentsByCustomerForWeb>)[index];
                        UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == docInfo.DocumentsByCustomer.DocumentRequestTypeID && t.CustomerID == _CustomerID && t.TypeID == docInfo.DocumentsByCustomer.TypeID && t.AnnounceID == docInfo.DocumentsByCustomer.AnnounceID && t.DocumentTypeID == docInfo.DocumentsByCustomer.DocumentTypeID).Take(1).SingleOrDefault();

                        if (doclist != null)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('فایل پیوست شده موجود می باشد!');", true);
                            return;
                        }

                        if (docInfo == null)
                            return;

                        //if (doclist != null && doclist.RequestDocumentID != 0)
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenRequestDocumentForm", string.Format("ModalDialog('/Viewes/RequestDocumentForm.aspx?RequestDocumentID={0}&DocType={1}',null,270,450); __doPostBack('ctl00$ContentsPlaceHolder$DocumentDummyLink','');", doclist.RequestDocumentID, 1), true);
                        //else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenRequestDocumentForm", string.Format("ModalDialog('/Viewes/RequestDocumentForm.aspx?CustomerID={0}&DocumentRequestTypeID={1}&DocType={2}&RequestID={3}',null,310,460); __doPostBack('ctl00$ContentsPlaceHolder$DocumentDummyLink','');", _Request.CustomerID, docInfo.DocumentsByCustomer.DocumentRequestTypeID, docInfo.DocumentsByCustomer.TypeID, _RequestID), true);
                    }
                    break;
                case "DeleteDocument":
                    {
                        List<DocumentsByCustomerForWeb> documents = RequestDocGridView.DataSource as List<DocumentsByCustomerForWeb>;
                        if (documents == null || documents.Count() == 0)
                            return;
                        DocumentsByCustomerForWeb documetInfo = documents[index];

                        UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == documetInfo.DocumentsByCustomer.DocumentRequestTypeID && t.CustomerID == _Customer.ID && t.TypeID == documetInfo.DocumentsByCustomer.TypeID && t.AnnounceID == documetInfo.DocumentsByCustomer.AnnounceID && t.DocumentTypeID == documetInfo.DocumentsByCustomer.DocumentTypeID).Take(1).SingleOrDefault();
                        RequestDocument rd = DB.GetEntitybyID<RequestDocument>(doclist.RequestDocumentID);
                        RequestDocumnetDB.DeleteRequestDocument(rd, _Request.ID);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('مدرک با موفقیت حذف شد.');", true);
                        DocumentDummyLink_Click(null, null);
                    }
                    break;

                case "ModifyDocument":
                    {
                        DocumentsByCustomerForWeb docInfo = (RequestDocGridView.DataSource as List<DocumentsByCustomerForWeb>)[index];
                        UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.DocumentRequestTypeID == docInfo.DocumentsByCustomer.DocumentRequestTypeID && t.CustomerID == _CustomerID && t.TypeID == docInfo.DocumentsByCustomer.TypeID && t.AnnounceID == docInfo.DocumentsByCustomer.AnnounceID && t.DocumentTypeID == docInfo.DocumentsByCustomer.DocumentTypeID).Take(1).SingleOrDefault();

                        if (doclist != null)
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenRequestDocumentForm", string.Format("ModalDialog('/Viewes/RequestDocumentForm.aspx?RequestDocumentID={0}&DocType={1}',null,310,460); __doPostBack('ctl00$ContentsPlaceHolder$DocumentDummyLink','');", doclist.RequestDocumentID, 1), true);
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('مدرکی یافت نشد!');", true);
                            return;
                        }
                    }
                    break;

                case "ViewDocument":
                    {
                        List<DocumentsByCustomerForWeb> documents = RequestDocGridView.DataSource as List<DocumentsByCustomerForWeb>;
                        if (documents == null || documents.Count() == 0)
                            return;
                        DocumentsByCustomerForWeb documetInfo = documents[index];
                        RequestDocument requestDocument = Data.RequestDocumnetDB.GetRequestDocument(_Request.ID, documetInfo.DocumentsByCustomer.DocumentRequestTypeID);
                        if (requestDocument != null)
                        {
                            _FileBytes = DocumentsFileDB.GetDocumentsFileTable((Guid)requestDocument.DocumentsFileID).Content;
                            string fileType = DocumentsFileDB.GetDocumentsFileTable((Guid)requestDocument.DocumentsFileID).FileType;
                            Session["FileBytes"] = _FileBytes;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenDocumentFile", string.Format("ModalDialog('/Viewes/DocumentViewForm.aspx?RequestID={0}&CustomerID={1}&DocumentRequestTypeID={2}&TypeID={3}&AnnounceID ={4}&DocumentTypeID ={5}&FileType={6}',null,750,500);", _Request.ID, _CustomerID, documetInfo.DocumentsByCustomer.DocumentRequestTypeID, documetInfo.DocumentsByCustomer.TypeID, documetInfo.DocumentsByCustomer.AnnounceID, documetInfo.DocumentsByCustomer.DocumentTypeID, fileType), true);
                        }
                        else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('فایل موجود نمی باشد.');", true);
                    }
                    break;

                default:
                    break;
            }
        }

        //protected void RequestPermissionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int index;
        //    switch (e.CommandName)
        //    {
        //        case "Delete":
        //            index = Convert.ToInt32(e.CommandArgument.ToString());
        //            if (RequestPermissionGridView.SelectedIndex >= 0)
        //            {
        //                List<DocumentsByCustomerForWeb> documents = RequestDocGridView.DataSource as List<DocumentsByCustomerForWeb>;
        //                if (documents == null || documents.Count() == 0)
        //                    return;
        //                DocumentsByCustomerForWeb docInfo = documents[index];

        //                if (docInfo == null) return;

        //                UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.DocumentRequestTypeID == docInfo.DocumentsByCustomer.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == docInfo.DocumentsByCustomer.TypeID && t.AnnounceID == docInfo.DocumentsByCustomer.AnnounceID && t.DocumentTypeID == docInfo.DocumentsByCustomer.DocumentTypeID).Take(1).SingleOrDefault();
        //                RequestDocument rd = DB.GetEntitybyID<RequestDocument>(doclist.RequestDocumentID);
        //                RequestDocumnetDB.DeleteRequestDocument(rd, _request.ID);
        //                var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_request.RequestTypeID, _request.RequestDate, _customer.PersonType);
        //                _refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.CustomerID == _customer.ID).ToList();

        //                RequestPermissionGridView.DataSource = NeededDocs.Where(t => t.TypeID == 2).ToList();
        //                RequestPermissionGridView.DataBind();
        //            }
        //            break;

        //        case "Edit":
        //            index = Convert.ToInt32(e.CommandArgument.ToString());
        //            if (RequestPermissionGridView.SelectedIndex >= 0)
        //            {
        //                List<DocumentsByCustomerForWeb> documents = RequestDocGridView.DataSource as List<DocumentsByCustomerForWeb>;
        //                if (documents == null || documents.Count() == 0)
        //                    return;
        //                DocumentsByCustomerForWeb docInfo = documents[index];

        //                if (docInfo == null) return;
        //                UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.DocumentRequestTypeID == docInfo.DocumentsByCustomer.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == docInfo.DocumentsByCustomer.TypeID && t.AnnounceID == docInfo.DocumentsByCustomer.AnnounceID && t.DocumentTypeID == docInfo.DocumentsByCustomer.DocumentTypeID).Take(1).SingleOrDefault();
        //                string url = string.Format("/Viewes/RequestDocumentForm.aspx ? RequestDocumentID={0} & DocType={1} ", doclist.RequestDocumentID, 2);
        //                Page.ClientScript.RegisterStartupScript(GetType(), "OpenRequestDocumentForm", string.Format("ModalDialog({0},null,800,500);", url), false);

        //                var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_request.RequestTypeID, _request.RequestDate, _customer.PersonType);
        //                _refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.CustomerID == _customer.ID).ToList();

        //                RequestPermissionGridView.DataSource = NeededDocs.Where(t => t.TypeID == 2).ToList();
        //                RequestPermissionGridView.DataBind();
        //            }
        //            break;

        //        default:
        //            break;
        //    }

        //}

        //protected void RequestContractGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int index;
        //    switch (e.CommandName)
        //    {
        //        case "Delete":
        //            index = Convert.ToInt32(e.CommandArgument.ToString());
        //            //    if (RequestContractGridView.SelectedIndex >= 0)
        //            //    {
        //            //        System.Windows.Forms.DialogResult result =  PersianMessageBox.Show("آیا از حذف مطمئن هستید؟","تأیید",PersianMessageBox.Buttons.YesNo,PersianMessageBox.Icon.Question)
        //            //        if (result == System.Windows.Forms.DialogResult.Yes)
        //            //        {
        //            //            DocumentsByCustomer contractInfo = RequestContractGridView.SelectedItem as DocumentsByCustomer;
        //            //            if (contractInfo == null) return;
        //            //            UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.DocumentRequestTypeID == contractInfo.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == contractInfo.TypeID && t.AnnounceID == contractInfo.AnnounceID && t.DocumentTypeID == contractInfo.DocumentTypeID).Take(1).SingleOrDefault();
        //            //            Contract contract = DB.SearchByPropertyName<Contract>("RequestID", _request.ID).Where(t => t.RequestDocumentID == doclist.RequestDocumentID).SingleOrDefault();
        //            //            ContractDB.DeleteRequestContract(contract);
        //            //            var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_request.RequestTypeID, _request.RequestDate, _customer.PersonType);
        //            //            _refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.CustomerID == _customer.ID).ToList();

        //            //            RequestContractGridView.ItemsSource = NeededDocs.Where(t => t.TypeID == 3).ToList();
        //            //            this.RequestContractGridView.Items.Refresh();
        //            //        }
        //            //    }
        //            break;

        //        case "Edit":
        //            index = Convert.ToInt32(e.CommandArgument.ToString());
        //            //    if (RequestContractGridView.SelectedIndex >= 0)
        //            //    {
        //            //        DocumentsByCustomer contractInfo = RequestContractGrid.SelectedItem as DocumentsByCustomer;
        //            //        ContractForm windowContract;
        //            //        TelRoundSaleForm windowSale;

        //            //        var x = DocumentRequestTypeDB.GetUsedDocs();
        //            //        UsedDocs doclist = x.Where(t => t.RequestID == _request.ID
        //            //                                          && t.DocumentRequestTypeID == contractInfo.DocumentRequestTypeID
        //            //                                          && t.CustomerID == _customerID
        //            //                                          && t.TypeID == (byte)contractInfo.TypeID
        //            //                                          && t.AnnounceID == contractInfo.AnnounceID
        //            //                                          && t.DocumentTypeID == contractInfo.DocumentTypeID
        //            //                                    )
        //            //                              .Take(1).SingleOrDefault();

        //            //        Contract contract = new Contract();
        //            //        if (doclist != null)
        //            //            contract = DB.SearchByPropertyName<Contract>("RequestID", _request.ID).Where(t => t.RequestDocumentID == doclist.RequestDocumentID).SingleOrDefault();
        //            //        if (contract.ID != 0 && contractInfo.IsRelatedToRoundContract == true)
        //            //        {
        //            //            windowSale = new TelRoundSaleForm(contract.ID);
        //            //            windowSale.ShowDialog();
        //            //        }
        //            //        else if (contract.ID == 0 && contractInfo.IsRelatedToRoundContract == true)
        //            //        {
        //            //            windowSale = new TelRoundSaleForm(_request);
        //            //            windowSale.ShowDialog();
        //            //        }
        //            //        else if (contract.ID != 0 && contractInfo.IsRelatedToRoundContract == false)
        //            //        {
        //            //            windowContract = new ContractForm(contract.ID);
        //            //            windowContract.ShowDialog();
        //            //        }
        //            //        else if (contract.ID == 0 && contractInfo.IsRelatedToRoundContract == false)
        //            //        {
        //            //            windowContract = new ContractForm(_request);
        //            //            windowContract.ShowDialog();
        //            //        }

        //            //        var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_request.RequestTypeID, _request.RequestDate, _customer.PersonType);
        //            //        _refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.CustomerID == _customer.ID).ToList();

        //            //        RequestContractGridView.ItemsSource = NeededDocs.Where(t => t.TypeID == 3).ToList();
        //            //        this.RequestContractGridView.Items.Refresh();
        //            //    }
        //            break;

        //        default:
        //            break;
        //    }

        //}

        protected void RequestPaymentGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (index < 0)
                return;
            //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //TextBox textBox = (TextBox)row.FindControl("TextBoxAddPost");

            RequestPaymentInfo requestPaymentInfo = (RequestPaymentGridView.DataSource as List<RequestPaymentInfo>)[index];
            if (requestPaymentInfo == null)
                return;
            RequestPayment item = requestPaymentInfo.RequestPayment;
            if (item == null)
                return;

            switch (e.CommandName)
            {
                case "RequestPaymentEdit":
                    {
                        if (item.PaymentType != (int)DB.PaymentType.Cash)
                        {
                            // Folder.MessageBox.ShowError("فقط هزینه های نقدی قابل پرداخت هستند!");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('فقط هزینه های نقدی قابل پرداخت هستند !');", true);
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "RequestPaymentEdit", string.Format("ModalDialog('/Viewes/RequestPaymentForm.aspx?RequestPaymentID={0}&RequestID={1}&IsOtherCost={2}',null,370,400); __doPostBack('ctl00$ContentsPlaceHolder$PaymentDummyLink','')", item.ID, _RequestID, false), true);
                    }
                    break;

                case "InstallmentRequestPayment":
                    {
                        if (item.BaseCostID == BaseCostDB.GetServiceCostForADSL().ID)
                        {
                            ADSLService service = ADSLServiceDB.GetADSLServiceById(int.Parse(_ADSL.ServiceValue));
                            if (service.IsInstalment == null || service.IsInstalment == false)
                            {
                                //Folder.MessageBox.ShowError("نحوه پرداخت این سرویس به صورت نقدی می باشد !");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('نحوه پرداخت این سرویس به صورت نقدی می باشد !');", true);
                                return;
                            }
                        }
                        else
                        {
                            if (item.BaseCostID == BaseCostDB.GetModemCostForADSL().ID)
                            {
                                ADSLService service = ADSLServiceDB.GetADSLServiceById(int.Parse(_ADSL.ServiceValue));
                                if (!service.IsModemInstallment)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('نحوه پرداخت این مودم برای سرویس مورد نظر به صورت نقدی می باشد !');", true);
                                    return;
                                }
                            }
                            else
                            {
                                if (item.PaymentType != (int)DB.PaymentType.Instalment)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('فقط هزینه های قسطی قابل قسط بندی هستند!');", true);
                                    return;
                                }
                            }
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "RequestPaymentEdit", string.Format("ModalDialog('/Viewes/InstallmentRequestPaymentForm.aspx?RequestPaymentID={0}',null,700,725); __doPostBack('ctl00$ContentsPlaceHolder$PaymentDummyLink','')", item.ID), true);
                    }
                    break;

                case "requestPaymentKickBack":
                    try
                    {
                        if (item.IsPaid == null || item.IsPaid == false)
                            throw new Exception("این فیش پرداخت نشده است");

                        item.IsKickedBack = true;

                        item.Detach();
                        DB.Save(item);

                        RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(_RequestID);
                        RequestPaymentGridView.DataBind();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('بازپرداخت با موفقیت انجام شد.');", true);
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message.Replace("\'", "");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا در بازپرداخت دریافت/پرداخت : " + message + "');", true);
                    }
                    break;

                case "ChangeCashInstalment":
                    //try
                    //{
                    //    if (item.BaseCostID == BaseCostDB.GetServiceCostForADSL().ID)
                    //    {
                    //        ADSLService service = ADSLServiceDB.GetADSLServiceById(int.Parse(_ADSL.ServiceValue));
                    //        if (service.IsInstalment == null || service.IsInstalment == false)
                    //        {
                    //            //Folder.MessageBox.ShowError("نحوه پرداخت این سرویس به صورت نقدی می باشد !");
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('نحوه پرداخت این سرویس به صورت نقدی می باشد !');", true);
                    //            return;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (item.BaseCostID == BaseCostDB.GetModemCostForADSL().ID)
                    //        {
                    //            ADSLService service = ADSLServiceDB.GetADSLServiceById(int.Parse(_ADSL.ServiceValue));
                    //            if (!service.IsModemInstallment)
                    //            {
                    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('نحوه پرداخت این مودم برای سرویس مورد نظر به صورت نقدی می باشد !');", true);
                    //                return;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (item.PaymentType != (int)DB.PaymentType.Instalment)
                    //            {
                    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('فقط هزینه های قسطی قابل قسط بندی هستند!');", true);
                    //                return;
                    //            }
                    //        }
                    //    }

                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "RequestPaymentEdit", string.Format("ModalDialog('/Viewes/InstallmentRequestPaymentForm.aspx?RequestPaymentID={0}',null,700,725); __doPostBack('ctl00$ContentsPlaceHolder$PaymentDummyLink','')", item.ID), true);

                    //}
                    //catch (Exception ex)
                    //{
                    //    string message = ex.Message.Replace("\'", "");
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا در بازپرداخت دریافت/پرداخت : " + message + "');", true);
                    //}




                    try
                    {
                        if (item.IsPaid == true)
                            throw new Exception("پرداخت انجام شده است، امکان تغییر نحوه پرداخت وجود ندارد !");

                        if (item.BaseCostID == BaseCostDB.GetServiceCostForADSL().ID)
                        {
                            if (item.PaymentType == (byte)DB.PaymentType.Instalment)
                            {
                                List<InstallmentRequestPayment> instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(item.ID);

                                DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                item.PaymentType = (byte)DB.PaymentType.Cash;

                                item.Detach();
                                DB.Save(item);

                                RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(RequestID);
                                RequestPaymentGridView.DataBind();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('لطفا فاکتور را مجددا صادر نمایید !');", true);
                                return;

                            }

                            if (item.PaymentType == (byte)DB.PaymentType.Cash)
                            {
                                ADSLService service = ADSLServiceDB.GetADSLServiceById(Convert.ToInt32(_ADSL.ServiceValue));
                                if (service.IsInstalment != null)
                                {
                                    if ((bool)service.IsInstalment)
                                    {
                                        int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                                        List<InstallmentRequestPayment> instalList = GenerateInstalments(true, item.ID, duration, (byte)DB.RequestType.ADSL, (long)service.PriceSum, false);

                                        DB.SaveAll(instalList);

                                        item.PaymentType = (byte)DB.PaymentType.Instalment;
                                        item.BillID = null;
                                        item.PaymentID = null;

                                        item.Detach();
                                        DB.Save(item);

                                        RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(RequestID);
                                        RequestPaymentGridView.DataBind();
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('لطفا فاکتور را مجددا صادر نمایید !');", true);
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (item.BaseCostID == BaseCostDB.GetModemCostForADSL().ID)
                            {
                                if (item.PaymentType == (byte)DB.PaymentType.Instalment)
                                {
                                    List<InstallmentRequestPayment> instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(item.ID);

                                    DB.DeleteAll<InstallmentRequestPayment>(instalmentList.Select(t => t.ID).ToList());

                                    item.PaymentType = (byte)DB.PaymentType.Cash;

                                    item.Detach();
                                    DB.Save(item);

                                    RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(RequestID);
                                    RequestPaymentGridView.DataBind();
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('لطفا فاکتور را مجددا صادر نمایید !');", true);
                                    return;
                                }

                                if (item.PaymentType == (byte)DB.PaymentType.Cash)
                                {
                                    ADSLService service = ADSLServiceDB.GetADSLServiceById(Convert.ToInt32(_ADSL.ServiceValue));
                                    if (service.IsModemInstallment != null)
                                    {
                                        if ((bool)service.IsModemInstallment)
                                        {
                                            int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                                            List<InstallmentRequestPayment> instalList = GenerateInstalments(true, item.ID, duration, (byte)DB.RequestType.ADSL, (long)item.AmountSum, false);

                                            DB.SaveAll(instalList);

                                            item.PaymentType = (byte)DB.PaymentType.Instalment;
                                            item.BillID = null;
                                            item.PaymentID = null;

                                            item.Detach();
                                            DB.Save(item);

                                            RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(RequestID);
                                            RequestPaymentGridView.DataBind();
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('لطفا فاکتور را مجددا صادر نمایید !');", true);
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //  Folder.MessageBox.ShowError("عملیات تبدیل نحوه پرداخت برای پرداخت مورد نظر امکان پذیر نمی باشد !");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('عملیات تبدیل نحوه پرداخت برای پرداخت مورد نظر امکان پذیر نمی باشد !');", true);
                                return;
                            }
                        }

                        RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(RequestID);
                        RequestPaymentGridView.DataBind();
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message.Replace("\'", "");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا : " + message + "');", true);
                    }

                    break;

                case "DeleteRelatedRequestItem":

                    try
                    {
                        if (!string.IsNullOrEmpty(QuestionResultHiddenField.Value) && QuestionResultHiddenField.Value.ToLower() == "true")
                        {
                            List<InstallmentRequestPayment> installmentRequestPayments = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(item.ID);
                            if (item.BaseCostID == (int)DB.SpecialCostID.PrePaymentTypeCostID)
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('این هزینه پیش پرداخت اقساط می باشد حذف آن از طریق حذف اقساط انجام می شود.');", true);
                            else if (installmentRequestPayments.Count != 0)
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('این هزینه شامل اقساط می باشد لطفا ابتدا اقساط را حذف کنید.');", true);
                            else if (item.IsPaid != null && (bool)item.IsPaid)
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('این فیش پرداخت شده است، امکان حذف آن وجود ندارد');", true);
                            else
                                DB.Delete<RequestPayment>(item.ID);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('حذف با موفقیت انجام شد.');", true);

                            RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(_RequestID);
                            RequestPaymentGridView.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message.Replace("\'", "");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا در حذف دریافت/پرداخت : " + message + "');", true);
                    }

                    break;

                default:
                    break;
            }
        }

        protected void RelatedRequestGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            switch (e.CommandName)
            {
                case "Edit":
                    //index = Convert.ToInt32(e.CommandArgument.ToString());

                    break;
            }
        }

        //protected void InsertRequestDocumentButton_Click(object sender, EventArgs e)
        //{

        //}

        //protected void InsertRequestPermissioButton_Click(object sender, EventArgs e)
        //{
        //    if (RequestPermissionGridView.SelectedIndex >= 0)
        //    {
        //        DocumentsByCustomerForWeb docInfo = (RequestPermissionGridView.DataSource as List<DocumentsByCustomerForWeb>)[RequestPermissionGridView.SelectedIndex];

        //        UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.DocumentRequestTypeID == docInfo.DocumentsByCustomer.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == docInfo.DocumentsByCustomer.TypeID && t.AnnounceID == docInfo.DocumentsByCustomer.AnnounceID && t.DocumentTypeID == docInfo.DocumentsByCustomer.DocumentTypeID).Take(1).SingleOrDefault();
        //        if (docInfo == null) return;

        //        if (doclist != null && doclist.RequestDocumentID != null)
        //        {
        //            string url = string.Format("/Viewes/RequestDocumentForm.aspx ? RequestDocumentID={0} & DocType={1} ", doclist.RequestDocumentID, 2);
        //            Page.ClientScript.RegisterStartupScript(GetType(), "OpenRequestDocumentForm", string.Format("ModalDialog({0},null,800,500);", url), false);
        //        }
        //        else
        //        {
        //            string url = string.Format("/Viewes/RequestDocumentForm.aspx ? CustomerID={0} & DocumentRequestTypeID={1} & DocType={2} & RequestID={3} ", _request.CustomerID, docInfo.DocumentsByCustomer.DocumentRequestTypeID, docInfo.DocumentsByCustomer.TypeID, _requestID);
        //            Page.ClientScript.RegisterStartupScript(GetType(), "OpenRequestDocumentForm", string.Format("ModalDialog({0},null,800,500);", url), false);
        //        }

        //        var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_request.RequestTypeID, _request.RequestDate, _customer.PersonType);
        //        _refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.CustomerID == _customer.ID).ToList();

        //        RequestPermissionGridView.DataSource = NeededDocs.Where(t => t.TypeID == 2).ToList();
        //        RequestPermissionGridView.DataBind();
        //    }
        //}

        protected void InsertRequestContractButton_Click(object sender, EventArgs e)
        {
            //if (RequestContractGridView.SelectedIndex >= 0)
            //{
            //    ContractForm windowContract;
            //    TelRoundSaleForm windowSale;

            //    //DocumentsByCustomer contractInfo = RequestContractGridView.SelectedItem as DocumentsByCustomer;
            //    DocumentsByCustomerForWeb contractInfo = (RequestContractGridView.DataSource as List<DocumentsByCustomerForWeb>)[RequestContractGridView.SelectedIndex];

            //    UsedDocs doclist = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.DocumentRequestTypeID == contractInfo.DocumentsByCustomer.DocumentRequestTypeID && t.CustomerID == _customerID && t.TypeID == (byte)contractInfo.DocumentsByCustomer.TypeID && t.AnnounceID == contractInfo.DocumentsByCustomer.AnnounceID && t.DocumentTypeID == contractInfo.DocumentsByCustomer.DocumentTypeID).Take(1).SingleOrDefault();
            //    Contract contract = new Contract();

            //    if (doclist != null)
            //        contract = Data.ContractDB.GetContractsByRequestID(_request.ID).Where(t => t.RequestDocumentID == doclist.RequestDocumentID).SingleOrDefault();
            //    if (contract.ID != 0 && contractInfo.DocumentsByCustomer.IsRelatedToRoundContract == true)
            //    {
            //        windowSale = new TelRoundSaleForm(contract.ID);
            //        windowSale.ShowDialog();
            //    }
            //    else if (contract.ID == 0 && contractInfo.DocumentsByCustomer.IsRelatedToRoundContract == true)
            //    {
            //        windowSale = new TelRoundSaleForm(_request);
            //        windowSale.ShowDialog();
            //    }
            //    else if (contract.ID != 0 && contractInfo.DocumentsByCustomer.IsRelatedToRoundContract == false)
            //    {
            //        windowContract = new ContractForm(contract.ID);
            //        windowContract.ShowDialog();
            //    }
            //    else if (contract.ID == 0 && contractInfo.DocumentsByCustomer.IsRelatedToRoundContract == false)
            //    {
            //        windowContract = new ContractForm(_request);
            //        windowContract.ShowDialog();
            //    }

            //    var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_request.RequestTypeID, _request.RequestDate, _customer.PersonType);
            //    _refDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _request.ID && t.CustomerID == _customer.ID).ToList();

            //    RequestContractGridView.DataSource = NeededDocs.Where(t => t.TypeID == 3).ToList();
            //    RequestContractGridView.DataBind();
            //}
        }

        protected void RequestPaymentInsertButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenRequestPaymentForm", string.Format("ModalDialog('/Viewes/RequestPaymentForm.aspx?RequestPaymentID={0}&RequestID={1}&IsOtherCost={2}',null,370,400); __doPostBack('ctl00$ContentsPlaceHolder$OtherCostDummyLink','')", 0, _RequestID, true), true);
        }

        protected void PrintFactorButton_Click(object sender, EventArgs e)
        {
            if (_RequestID != 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenSaleFactorForm", string.Format("ModalDialog('/Viewes/SaleFactorForm.aspx?RequestID={0}',null,510,820); __doPostBack('ctl00$ContentsPlaceHolder$PaymentDummyLink','')", _RequestID), true);
        }

        protected void PaidFactorButton_Click(object sender, EventArgs e)
        {
            if (_RequestID != 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenPaidFactorForm", string.Format("ModalDialog('/Viewes/PaidFactorForm.aspx?RequestID={0}',null,320,380); __doPostBack('ctl00$ContentsPlaceHolder$PaymentDummyLink','')", _RequestID), true);
        }

        protected void InsertRelatedRequestButton_Click(object sender, EventArgs e)
        {
            //RequestForm window = new RequestForm(_Request.ID, Customer.ID);
            //window.ShowDialog();
            //RelatedRequestsGrid.ItemsSource = DB.GetAllEntity<Request>().Where(t => t.RelatedRequestID == _Request.ID).ToList();
            //this.RelatedRequestsGrid.Items.Refresh();
        }

        protected void PhoneRecordsRequestTypeDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["enumType"] = typeof(DB.RequestType);
        }

        protected void PaymentTypeDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["enumType"] = typeof(DB.PaymentType);
        }

        protected void AddRequestPaymentTypeDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["enumType"] = typeof(DB.PaymentType);
        }

        protected void BaseCostDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["requestTypeID"] = _Request.RequestTypeID;
        }

        protected void PaymentFicheDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["requestID"] = _RequestID;
        }

        protected void PaymentWayDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["enumType"] = typeof(DB.PaymentWay);
        }

        protected void PaymentsPaymentTypeDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["enumType"] = typeof(DB.PaymentType);
        }

        protected void DocumentDummyLink_Click(object sender, EventArgs e)
        {
            var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, _Customer.PersonType);
            _RefDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == _Customer.ID).ToList();

            List<DocumentsByCustomerForWeb> result = new List<DocumentsByCustomerForWeb>();
            foreach (DocumentsByCustomer item in NeededDocs.Where(t => t.TypeID == 1).ToList())
            {
                DocumentsByCustomerForWeb documentsByCustomerForWeb = new DocumentsByCustomerForWeb();
                documentsByCustomerForWeb.DocumentsByCustomer = item;
                List<UsedDocs> refer = _RefDocs;
                long cnt = 0;
                if (refer.Count != 0)
                    cnt = refer.Where(t => t.RequestID == (long)_RequestID && t.DocumentTypeID == int.Parse(item.DocumentTypeID.ToString())).Select(t => t).ToList().Count();
                if (cnt != 0)
                    documentsByCustomerForWeb.IsAvailable = true;
                else
                    documentsByCustomerForWeb.IsAvailable = false;

                result.Add(documentsByCustomerForWeb);
            }

            RequestDocGridView.DataSource = result;
            this.RequestDocGridView.DataBind();
        }

        protected void OtherCostDummyLink_Click(object sender, EventArgs e)
        {
            EnableRequestDetails();
            RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(_RequestID);
            RequestPaymentGridView.DataBind();
        }

        protected void PaymentDummyLink_Click(object sender, EventArgs e)
        {
            RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(_RequestID);
            RequestPaymentGridView.DataBind();
        }

        #endregion Event

        #region Method

        public void LoadData()
        {
            if (_RequestID == 0)
            {
                _Request = new Request();
                if (_RequestType == null)
                    return;

                switch (_RequestType.ID)
                {
                    case (int)DB.RequestType.ADSL:
                        RequestDetailLabel.Text = string.Format("جزئیات درخواست {0}", _RequestType.Title);
                        long customerID = 0;
                        if (_Customer != null)
                        {
                            customerID = _Customer.ID;
                            _Request.RequesterName = _Customer.FirstNameOrTitle + " " + _Customer.LastName;
                        }

                        UserControl.ADSL aDSLUserControl;
                        if (RequestDetailPanel.FindControl("ADSLUserControl") == null)
                        {
                            aDSLUserControl = (UserControl.ADSL)LoadControl("~/UserControl/ADSL.ascx");
                            aDSLUserControl.ID = "ADSLUserControl";
                            RequestDetailPanel.Controls.Add(aDSLUserControl);
                        }
                        else
                            aDSLUserControl = RequestDetailPanel.FindControl("ADSLUserControl") as UserControl.ADSL;
                        aDSLUserControl.RequestID = _RequestID;
                        //mas  aDSLUserControl.CustomerID = customerID;
                        aDSLUserControl.ADSLCustomer = CustomerDB.GetCustomerByID(customerID);
                        aDSLUserControl.TelephoneNo = _TelephoneNo;
                        aDSLUserControl.LoadData();

                        _ADSL = aDSLUserControl;

                        Title = "ثبت درخواست ADSL ";

                        if (aDSLUserControl.IsWaitingList)
                        {
                            _ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.SaveWaitingList, (byte)DB.NewAction.Exit };
                            ActionUserControl.ActionIDs = _ActionIDs;
                            ActionUserControl.LoadData();
                        }
                        break;

                    case (int)DB.RequestType.ADSLChangeService:
                        UserControl.ADSLChangeService aDSLChangeServiceUserControl;
                        RequestDetailLabel.Text = string.Format("جزئیات درخواست {0}", _RequestType.Title);

                        if (RequestDetailPanel.FindControl("ADSLChangeServiceUserControl") == null)
                        {
                            aDSLChangeServiceUserControl = (UserControl.ADSLChangeService)LoadControl("~/UserControl/ADSLChangeService.ascx");
                            aDSLChangeServiceUserControl.ID = "ADSLChangeServiceUserControl";
                            RequestDetailPanel.Controls.Add(aDSLChangeServiceUserControl);
                        }
                        else
                            aDSLChangeServiceUserControl = RequestDetailPanel.FindControl("ADSLChangeServiceUserControl") as UserControl.ADSLChangeService;


                        if (_Telephone != null)
                            aDSLChangeServiceUserControl.TelephoneNo = _Telephone.TelephoneNo;
                        else
                            aDSLChangeServiceUserControl.TelephoneNo = _TelephoneNo;

                        aDSLChangeServiceUserControl.RequestID = _RequestID;
                        //aDSLChangeServiceUserControl.CustomerID = _customer.ID;
                        aDSLChangeServiceUserControl.LoadData();

                        _ADSLChangeService = aDSLChangeServiceUserControl;
                        Title = "ثبت درخواست شارژ مجدد ADSL ";

                        _ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                        break;

                    default:
                        RequestDetailPanel.Controls.Clear();
                        RequestDetailPanel.Visible = false;
                        Title = "ثبت درخواست ";
                        break;
                }

                _Request.RequestDate = DB.GetServerDate();
                if (!IsPostBack)
                {
                    RequestorNameTextBox.Text = _Request.RequesterName;
                    RequestLetterNoTextBox.Text = _Request.RequestLetterNo;
                    RequestDateTextBox.Text = Date.GetPersianDate(_Request.RequestDate, Date.DateStringType.Short);
                    RequestLetterDateTextBox.Text = Date.GetPersianDate(_Request.RequestLetterDate, Date.DateStringType.Short);

                    TelephoneInformationControl.TelephoneNo = _TelephoneNo;
                    TelephoneInformationControl.RequestTypeID = _RequestTypeID;
                    TelephoneInformationControl.LoadData();
                }

                ActionUserControl.ActionIDs = _ActionIDs;
                ActionUserControl.LoadData();
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_RequestID);
                _RequestCenterID = _Request.CenterID;
                Status Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);

                Title = "بروز رسانی درخواست ";
                if (!IsPostBack)
                {
                    CenterNameDropDownList.SelectedValue = _Request.CenterID.ToString();
                    RequestorNameTextBox.Text = _Request.RequesterName;
                    RequestLetterNoTextBox.Text = _Request.RequestLetterNo;
                    RequestDateTextBox.Text = Date.GetPersianDate(_Request.RequestDate, Date.DateStringType.Short);
                    RequestLetterDateTextBox.Text = Date.GetPersianDate(_Request.RequestLetterDate, Date.DateStringType.Short);
                    //TelephoneInfo.DataContext = _TelephoneInformation;
                    TelephoneInformationControl.TelephoneNo = (long)_Request.TelephoneNo;
                    TelephoneInformationControl.RequestTypeID = _Request.RequestTypeID;
                    TelephoneInformationControl.LoadData();
                }

                _RequestType = Data.RequestTypeDB.GetRequestTypeByRequestType((int)_Request.RequestTypeID);
                EnableRequestDetails();
                _RefDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestID == _Request.ID && t.CustomerID == _Customer.ID).ToList();
                _CityID = Data.RequestDB.GetCity(_Request.ID);

                switch (_Request.RequestTypeID)
                {
                    case (int)DB.RequestType.ADSL:
                        RequestDetailLabel.Text = string.Format("جزئیات درخواست {0}", _RequestType.Title);
                        TelephoneInformationControl.TelephoneNo = (long)_Request.TelephoneNo;
                        TelephoneInformationControl.RequestTypeID = _Request.RequestTypeID;
                        TelephoneInformationControl.LoadData();

                        UserControl.ADSL aDSLUserControl;
                        if (RequestDetailPanel.FindControl("ADSLUserControl") == null)
                        {
                            aDSLUserControl = (UserControl.ADSL)LoadControl("~/UserControl/ADSL.ascx");
                            aDSLUserControl.ID = "ADSLUserControl";
                            RequestDetailPanel.Controls.Add(aDSLUserControl);
                        }
                        else
                            aDSLUserControl = RequestDetailPanel.FindControl("ADSLUserControl") as UserControl.ADSL;
                        aDSLUserControl.RequestID = _RequestID;
                        //mas aDSLUserControl.CustomerID = _Customer.ID;
                        aDSLUserControl.ADSLCustomer = _Customer;
                        aDSLUserControl.TelephoneNo = (long)_Request.TelephoneNo;
                        aDSLUserControl.LoadData();
                        _ADSL = aDSLUserControl;

                        //TelephoneInfo.Content = _TelephoneInformation;
                        //TelephoneInfo.DataContext = _TelephoneInformation;

                        RequestDetailPanel.Visible = true;

                        if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                        {
                            Title = "ثبت درخواست ADSL ";

                            if (aDSLUserControl.IsWaitingList)
                            {
                                _ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.SaveWaitingList, (byte)DB.NewAction.Exit };
                                ActionUserControl.ActionIDs = _ActionIDs;
                                ActionUserControl.LoadData();
                            }
                        }

                        if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                        {
                            Title = "فروش سرویس ADSL ";
                            _ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                            ActionUserControl.ActionIDs = _ActionIDs;
                            ActionUserControl.LoadData();
                            DisableRequestInfo();

                            //TelephoneInfo.IsExpanded = false;
                            //RequestInfo.IsExpanded = false;

                            if (aDSLUserControl.IsWaitingList)
                            {
                                _ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.SaveWaitingList, (byte)DB.NewAction.Exit };
                                ActionUserControl.ActionIDs = _ActionIDs;
                                ActionUserControl.LoadData();
                            }
                        }

                        if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Feedback).ID)
                        {
                            Title = "واگذاری خطوط ADSL ";
                            _ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                            ActionUserControl.ActionIDs = _ActionIDs;
                            ActionUserControl.LoadData();
                            DisableRequestInfo();
                        }

                        if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Observation).ID)
                        {
                            Title = "رئیس مرکز ADSL ";
                            _ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                            ActionUserControl.ActionIDs = _ActionIDs;
                            ActionUserControl.LoadData();
                            DisableRequestInfo();
                        }


                        break;

                    case (int)DB.RequestType.ADSLChangeService:
                        RequestDetailLabel.Text = string.Format("جزئیات درخواست {0}", _RequestType.Title);

                        TelephoneInformationControl.TelephoneNo = (long)_Request.TelephoneNo;
                        TelephoneInformationControl.RequestTypeID = _Request.RequestTypeID;
                        TelephoneInformationControl.LoadData();

                        UserControl.ADSLChangeService aDSLChangeServiceUserControl;
                        if (RequestDetailPanel.FindControl("ADSLChangeServiceUserControl") == null)
                        {
                            aDSLChangeServiceUserControl = (UserControl.ADSLChangeService)LoadControl("~/UserControl/ADSLChangeService.ascx");
                            aDSLChangeServiceUserControl.ID = "ADSLChangeServiceUserControl";
                            RequestDetailPanel.Controls.Add(aDSLChangeServiceUserControl);
                        }
                        else
                            aDSLChangeServiceUserControl = RequestDetailPanel.FindControl("ADSLChangeServiceUserControl") as UserControl.ADSLChangeService;
                        aDSLChangeServiceUserControl.RequestID = _RequestID;
                        aDSLChangeServiceUserControl.CustomerID = _Customer.ID;
                        aDSLChangeServiceUserControl.TelephoneNo = (long)_Request.TelephoneNo;
                        aDSLChangeServiceUserControl.LoadData();
                        _ADSLChangeService = aDSLChangeServiceUserControl;

                        //TelephoneInfo.Content = _TelephoneInformation;
                        //TelephoneInfo.DataContext = _TelephoneInformation;
                        //RequestDetail.Content = _ADSLChangeService;
                        //RequestDetail.DataContext = _ADSLChangeService;
                        //RequestDetail.Visibility = Visibility.Visible;

                        RequestDetailPanel.Visible = true;

                        if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
                        {
                            Title = "فروش شارژ مجدد ADSL ";
                            _ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Cancelation, (byte)DB.NewAction.Exit };
                            ActionUserControl.ActionIDs = _ActionIDs;
                            ActionUserControl.LoadData();
                        }

                        if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                        {
                            Title = "اعلام نظر امور مشترکین برای تغییر تعرفه ADSL";
                            _ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                            ActionUserControl.ActionIDs = _ActionIDs;
                            ActionUserControl.LoadData();
                            DisableRequestInfo();
                        }

                        if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
                        {
                            Title = "اعمال تغییر تعرفه ADSL";
                            _ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
                            ActionUserControl.ActionIDs = _ActionIDs;
                            ActionUserControl.LoadData();
                            DisableRequestInfo();
                        }

                        if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Pending).ID)
                        {
                            Title = " تغییر تعرفه ADSL";
                            _ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };
                            ActionUserControl.ActionIDs = _ActionIDs;
                            ActionUserControl.LoadData();
                            DisableRequestInfo();
                        }

                        if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                        {
                            Title = " تغییر تعرفه ADSL";
                            _ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                            ActionUserControl.ActionIDs = _ActionIDs;
                            ActionUserControl.LoadData();
                            DisableRequestInfo();
                        }

                        break;

                    default:
                        break;
                }
                InsertRequestPayment();
                FillVisitInfo();
                //this.RequestContractGrid.Items.Refresh();
            }
            if (_Telephone != null)
            {
                if (_Request != null && _Request.RequestTypeID != (int)DB.RequestType.ChangeLocationSpecialWire)
                {
                    _CityID = Data.CityDB.GetCityByCenterID(_Telephone.CenterID).ID;
                    //(this.RequestInfo.DataContext as Request).CenterID = Telephone.CenterID;
                    _Request.CenterID = _Telephone.CenterID;
                    CenterNameDropDownList.Enabled = false;
                    CityDropDownList.Enabled = false;
                }

                Cycle cycle = new Cycle();
                try
                {
                    cycle = Data.CycleDB.GetDateCurrentCycle();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Sequence contains more than one element"))
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('با تاریخ فعلی چند دوره یافت شد. سوابق تلفن در دوره جاری قابل در یافت نیست لطفا اطلاعات دوره ها را اصلاح کنید');", true);
                }

                if (cycle != null)
                {

                    List<RequestLog> reqeustLogs = new List<RequestLog>();
                    //ReqeustDataGridComboBoxColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RequestType));

                    try
                    {
                        reqeustLogs = Data.RequestLogDB.GetReqeustLogByTelephone(_Telephone.TelephoneNo, (DateTime)cycle.FromDate, (DateTime)cycle.ToDate);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + "در دریافت سوابق تلفن خطا رخ داده است." + " !" + "');", true);
                    }
                    //PhoneRecordsInfoGridView.DataSource = reqeustLogs;
                    //PhoneRecordsInfoGridView.DataBind();
                    RelatedDocumentRadTabStrip.Tabs.FindTabByText("سوابق تلفن").Visible = true;
                }
            }

            if (_TelephoneNo != 0)
            {
                _Telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);

                _CityID = Data.CityDB.GetCityByCenterID(_Telephone.CenterID).ID;
                //(this.RequestInfo.DataContext as Request).CenterID = _Telephone.CenterID;
                CenterNameDropDownList.Enabled = false;
                CityDropDownList.Enabled = false;
            }

            if (_CityID == 0)
                CityDropDownList.ClearSelection();
            else
                try
                {
                    CityDropDownList.ClearSelection();
                    CityDropDownList.Items.FindByValue(_CityID.ToString()).Selected = true;
                }
                catch { }

            CityDropDownList_SelectedIndexChanged(null, null);
        }

        private void DisableRequestInfo()
        {
            RequestorNameTextBox.ReadOnly = true;
            CityDropDownList.Enabled = false;
            CenterNameDropDownList.Enabled = false;
            RequestDateTextBox.Enabled = false;
            RequestLetterNoTextBox.ReadOnly = true;
            RequestLetterDateTextBox.Enabled = false;
        }

        private void EnableRequestDetails()
        {
            RequestDocGridView.Visible = true;
            //RequestPermissionGridView.Visible = true;
            //RequestContractGridView.Visible = true;
            //RelatedRequestGridView.Visible = true;
            RequestPaymentGridView.Visible = true;
            if (_Customer != null)
            {
                var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_Request.RequestTypeID, _Request.RequestDate, _Customer.PersonType);

                List<DocumentsByCustomerForWeb> documents = new List<DocumentsByCustomerForWeb>();
                foreach (DocumentsByCustomer documentItem in NeededDocs.Where(t => t.TypeID == 1).ToList())
                {
                    DocumentsByCustomerForWeb documentsByCustomerForWeb = new DocumentsByCustomerForWeb();
                    documentsByCustomerForWeb.DocumentsByCustomer = documentItem;
                    List<UsedDocs> refer = _RefDocs;
                    long cnt = 0;
                    if (refer.Count != 0)
                        cnt = refer.Where(t => t.RequestID == (long)_RequestID && t.DocumentTypeID == int.Parse(documentItem.DocumentTypeID.ToString())).Select(t => t).ToList().Count();
                    if (cnt != 0)
                        documentsByCustomerForWeb.IsAvailable = true;
                    else
                        documentsByCustomerForWeb.IsAvailable = false;

                    documentsByCustomerForWeb.DocumentName = documentItem.DocumentName;
                    documentsByCustomerForWeb.IsForcible = documentItem.IsForcible;
                    documents.Add(documentsByCustomerForWeb);
                }
                RequestDocGridView.DataSource = documents;
                RequestDocGridView.DataBind();

                List<DocumentsByCustomerForWeb> permissions = new List<DocumentsByCustomerForWeb>();
                foreach (DocumentsByCustomer permissionItem in NeededDocs.Where(t => t.TypeID == 2).ToList())
                {
                    DocumentsByCustomerForWeb documentsByCustomerForWeb = new DocumentsByCustomerForWeb();
                    documentsByCustomerForWeb.DocumentsByCustomer = permissionItem;
                    List<UsedDocs> refer = _RefDocs;
                    long cnt = 0;
                    if (refer.Count != 0)
                        cnt = refer.Where(t => t.RequestID == (long)_RequestID && t.DocumentTypeID == int.Parse(permissionItem.DocumentTypeID.ToString())).Select(t => t).ToList().Count();
                    if (cnt != 0)
                        documentsByCustomerForWeb.IsAvailable = true;
                    else
                        documentsByCustomerForWeb.IsAvailable = false;

                    permissions.Add(documentsByCustomerForWeb);
                }
                //RequestPermissionGridView.DataSource = permissions;
                //RequestPermissionGridView.DataBind();

                List<DocumentsByCustomerForWeb> contracts = new List<DocumentsByCustomerForWeb>();
                foreach (DocumentsByCustomer contractItem in NeededDocs.Where(t => t.TypeID == 3).ToList())
                {
                    DocumentsByCustomerForWeb documentsByCustomerForWeb = new DocumentsByCustomerForWeb();
                    documentsByCustomerForWeb.DocumentsByCustomer = contractItem;
                    List<UsedDocs> refer = _RefDocs;
                    long cnt = 0;
                    if (refer.Count != 0)
                        cnt = refer.Where(t => t.RequestID == (long)_RequestID && t.DocumentTypeID == int.Parse(contractItem.DocumentTypeID.ToString())).Select(t => t).ToList().Count();
                    if (cnt != 0)
                        documentsByCustomerForWeb.IsAvailable = true;
                    else
                        documentsByCustomerForWeb.IsAvailable = false;

                    contracts.Add(documentsByCustomerForWeb);
                }
                //RequestContractGridView.DataSource = contracts;
                //RequestContractGridView.DataBind();
            }

            //RelatedRequestGridView.DataSource = Data.RequestDB.GetRelatedRequestByID(_request.ID);
            //RelatedRequestGridView.DataBind();

            RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(_RequestID);
            RequestPaymentGridView.DataBind();
        }

        public bool Save()
        {
            _Request.CenterID = Convert.ToInt32(CenterNameDropDownList.SelectedValue);
            _Request.RequesterName = RequestorNameTextBox.Text;
            _Request.RequestDate = (DateTime)Helper.PersianToGregorian(RequestDateTextBox.Text);
            _Request.RequestLetterNo = RequestLetterNoTextBox.Text;
            _Request.RequestLetterDate = string.IsNullOrEmpty(RequestLetterDateTextBox.Text) ? (DateTime?)null : Helper.PersianToGregorian(RequestLetterDateTextBox.Text);

            try
            {
                _Request.IsViewed = true;
                _Request.WaitForToBeCalculate = null;

                if (_RequestType == null || _RequestType.ID <= 0)
                    _RequestType = RequestTypeDB.getRequestTypeByID(_Request.RequestTypeID);
                if (_RequestType == null)
                    return false;

                Status Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);
                if (Status != null && (Status.StatusType == (byte)DB.RequestStatusType.Observation || Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter || Status.StatusType == (byte)DB.RequestStatusType.GetCosts))
                {
                    IsSaveSuccess = true;
                    return true;
                }

                //switch (_request.RequestType.ID)
                switch (_RequestType.ID)
                {
                    case (int)DB.RequestType.ADSL:
                        Save_ADSLRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangeService:
                        Save_ADSLChangeServiceRequest();
                        break;

                    default:
                        break;
                }

                _RequestID = _Request.ID;
                LoadData();
                IsSaveSuccess = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('با موفقیت ذخیره شد');", true);
                CloseRequestForm();
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;

                string message = ex.Message.Replace("\'", "");
                Response.Write(string.Format("<script>alert('{0}');</script>", " خطا در ذخیره اطلاعات : " + message));
                CloseRequestForm();
            }

            return IsSaveSuccess;
        }

        private void Save_ADSLRequest()
        {
            if (_ADSL.HasCreditAgent == false)
                throw new Exception("اعتبار نمایندگی شما کافی نمی باشد");

            if (_ADSL.HasCreditUser == false)
                throw new Exception("اعتبار کاربری شما کافی نمی باشد");

            CRM.Data.ADSLRequest ADSLRequest = new CRM.Data.ADSLRequest();

            if (_RequestID != 0)
                ADSLRequest = ADSLRequestDB.GetADSLRequestByID(_RequestID);

            ADSLRequest.CustomerOwnerID = _ADSL.ADSLCustomer.ID;
            ADSLRequest.CustomerOwnerElkaID = _ADSL.ADSLCustomer.ElkaID;

            //Customer aDSLCustomer = CustomerDB.GetCustomerByID(_ADSL.ADSLCustomer.ID);
            //mas  Customer aDSLCustomer = CustomerDB.GetCustomerByID(_ADSL.CustomerID);
            Customer aDSLCustomer = CustomerDB.GetCustomerByID(_ADSL.ADSLCustomer.ID);
            if (!string.IsNullOrWhiteSpace(_ADSL.MobileNo))
            {
                if (_ADSL.MobileNo.Trim().Count() != 11 || !_ADSL.MobileNo.Trim().StartsWith("09"))
                    throw new Exception("لطفا شماره تلفن همراه صحیح را وارد نمایید");

                aDSLCustomer.MobileNo = _ADSL.MobileNo.Trim();
            }
            aDSLCustomer.Detach();
            DB.Save(aDSLCustomer);

            //if (_ADSL.ADSLCustomer.ID == 0)
            //mas if (_ADSL.CustomerID == 0 || _ADSL.ADSLCustomer.ID == 0)
            if (_ADSL.ADSLCustomer == null || _ADSL.ADSLCustomer.ID == 0)
                _ADSL.ADSLCustomer = DB.SearchByPropertyName<Customer>("NationalCodeOrRecordNo", _ADSL.NationalCode.Trim())[0];

            if (_TelephoneNo == 0)
            {
                if (_Telephone != null)
                    ADSLRequest.TelephoneNo = _Telephone.TelephoneNo;
                else
                    ADSLRequest.TelephoneNo = (long)_Request.TelephoneNo;
            }
            else
                ADSLRequest.TelephoneNo = _TelephoneNo;

            ADSLRequest.CustomerOwnerStatus = string.IsNullOrEmpty(_ADSL.ADSLOwnerStatusValue) ? (byte?)null : Convert.ToByte(_ADSL.ADSLOwnerStatusValue);

            if (!string.IsNullOrWhiteSpace(_ADSL.CommentCustomers))
                ADSLRequest.CommentCustomers = _ADSL.CommentCustomers;

            Service1 webService = new Service1();

            //if (ADSLRequest.CustomerOwnerStatus == (byte)DB.ADSLOwnerStatus.Owner)
            //    webService.Update_MobileNumber_By_FI_CODE("Admin", "alibaba123", aDSLCustomer.ElkaID.ToString(), (aDSLCustomer.PersonType == 0) ? "1" : "2", aDSLCustomer.MobileNo.ToString());

            if (!string.IsNullOrEmpty(_ADSL.CustomerGroupValue))
                ADSLRequest.CustomerGroupID = Convert.ToInt32(_ADSL.CustomerGroupValue);
            else
                throw new Exception("لطفا گروه مشتری را تعیین نمایید");

            if (!string.IsNullOrEmpty(_ADSL.JobGroupValue))
                ADSLRequest.JobGroupID = Convert.ToInt32(_ADSL.JobGroupValue);
            else
                ADSLRequest.JobGroupID = null;

            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
            if (user != null)
                ADSLRequest.ADSLSellerAgentID = user.SellerAgentID;
            else
                ADSLRequest.ADSLSellerAgentID = null;
            ADSLRequest.ReagentTelephoneNo = _ADSL.ReagentTelephoneNo;
            ADSLRequest.Status = false;

            if (_RelatedRequestID != 0)
                _Request.RelatedRequestID = _RelatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (_RequestID == 0)
            {
                if (_TelephoneNo == 0)
                    _Request.TelephoneNo = _Telephone.TelephoneNo;
                else
                    _Request.TelephoneNo = _TelephoneNo;
            }
            _Request.RequestTypeID = _RequestType.ID;

            if (!string.IsNullOrEmpty(CenterNameDropDownList.SelectedValue))
                _Request.CenterID = Convert.ToInt32(CenterNameDropDownList.SelectedValue);
            _Request.CustomerID = _ADSL.ADSLCustomer.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
            _Request.IsVisible = true;

            if (_RequestID == 0 || _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
            {
                CRM.Data.Status status = DB.GetStatus(_RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
                _IsSalable = false;
            }

            if (_RequestID != 0 && _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                if (!string.IsNullOrEmpty(_ADSL.ServiceValue))
                    ADSLRequest.ServiceID = int.Parse(_ADSL.ServiceValue);
                else
                    throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

                ADSLRequest.LicenseLetterNo = _ADSL.LicenceLetterNo;

                if (!string.IsNullOrEmpty(_ADSL.AdditionalServiceValue))
                    ADSLRequest.AdditionalServiceID = int.Parse(_ADSL.AdditionalServiceValue);
                else
                    ADSLRequest.AdditionalServiceID = null;

                //if (_ADSL.CustomerPriorityComboBox.SelectedValue != null)
                //    ADSLRequest.CustomerPriority = (byte)_ADSL.CustomerPriorityComboBox.SelectedValue;
                //else
                ADSLRequest.CustomerPriority = (byte)DB.ADSLCustomerPriority.Normal;
                ADSLRequest.RequiredInstalation = _ADSL.RequiredInstalationIsChecked;
                ADSLRequest.HasIP = _ADSL.HasIPStatic;
                if (_ADSL.HasIPStatic.HasValue)
                {
                    if ((bool)_ADSL.HasIPStatic)
                        if (string.IsNullOrEmpty(_ADSL.IPTypeValue))
                            throw new Exception("لطفا نوع IP مورد نظر را تعیین نمایید");
                        else
                        {
                            if (_ADSL.IPStatic == null && _ADSL.GroupIPStatic == null)
                                throw new Exception("لطفا IP مورد نظر را انتخاب نمایید، یا گزینه انتخاب IP را بردارید.");

                            if ((byte)Convert.ToInt16(_ADSL.IPTypeValue) == (byte)DB.ADSLIPType.Single)
                            {
                                if (ADSLRequest.IPStaticID != null && ADSLRequest.IPStaticID != _ADSL.IPStatic.ID)
                                {
                                    ADSLIP oldIp = ADSLIPDB.GetADSLIPById((long)ADSLRequest.IPStaticID);
                                    oldIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldIp.TelephoneNo = null;

                                    oldIp.Detach();
                                    DB.Save(oldIp);
                                }

                                if (ADSLRequest.GroupIPStaticID != null)
                                {
                                    ADSLGroupIP oldGroupIp = ADSLIPDB.GetADSLGroupIPById((long)ADSLRequest.GroupIPStaticID);
                                    oldGroupIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldGroupIp.TelephoneNo = null;

                                    oldGroupIp.Detach();
                                    DB.Save(oldGroupIp);
                                }

                                ADSLRequest.IPStaticID = _ADSL.IPStatic.ID;
                                ADSLRequest.IPDuration = int.Parse(_ADSL.IPTimeValue);
                                ADSLRequest.GroupIPStaticID = null;

                                _ADSL.IPStatic.TelephoneNo = _Request.TelephoneNo;
                                _ADSL.IPStatic.Status = (byte)DB.ADSLIPStatus.Reserve;
                            }

                            if ((byte)Convert.ToInt16(_ADSL.IPTypeValue) == (byte)DB.ADSLIPType.Group)
                            {
                                if (ADSLRequest.GroupIPStaticID != null && ADSLRequest.GroupIPStaticID != _ADSL.GroupIPStatic.ID)
                                {
                                    ADSLGroupIP oldGroupIp = ADSLIPDB.GetADSLGroupIPById((long)ADSLRequest.GroupIPStaticID);
                                    oldGroupIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldGroupIp.TelephoneNo = null;

                                    oldGroupIp.Detach();
                                    DB.Save(oldGroupIp);
                                }

                                if (ADSLRequest.IPStaticID != null)
                                {
                                    ADSLIP oldIp = ADSLIPDB.GetADSLIPById((long)ADSLRequest.IPStaticID);
                                    oldIp.Status = (byte)DB.ADSLIPStatus.Free;
                                    oldIp.TelephoneNo = null;

                                    oldIp.Detach();
                                    DB.Save(oldIp);
                                }

                                ADSLRequest.GroupIPStaticID = _ADSL.GroupIPStatic.ID;
                                ADSLRequest.IPDuration = int.Parse(_ADSL.IPTimeValue);
                                ADSLRequest.IPStaticID = null;

                                _ADSL.GroupIPStatic.TelephoneNo = _Request.TelephoneNo;
                                _ADSL.GroupIPStatic.Status = (byte)DB.ADSLIPStatus.Reserve;
                            }
                        }
                }

                ADSLRequest.NeedModem = _ADSL.HasNeedModem;
                if (_ADSL.HasNeedModem != null)
                {
                    if ((bool)_ADSL.HasNeedModem)
                        if (string.IsNullOrEmpty(_ADSL.ModemTypeValue))
                            throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                        else
                        {
                            if (string.IsNullOrEmpty(_ADSL.ModemSerialNo))
                                throw new Exception("لطفا شماره سریال مودم را انتخاب نمایید، یا گزینه درخواست مودم را بردارید. ");

                            ADSLRequest.ModemID = string.IsNullOrEmpty(_ADSL.ModemTypeValue) ? (int?)null : int.Parse(_ADSL.ModemTypeValue);
                            ADSLRequest.ModemSerialNoID = int.Parse(_ADSL.ModemSerialNo);
                            ADSLRequest.ModemMACAddress = _ADSL.ModemMACAddress;

                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSLRequest.ModemSerialNoID);
                            modem.TelephoneNo = _Request.TelephoneNo;
                            modem.Status = (byte)DB.ADSLModemStatus.Reserve;

                            modem.Detach();
                            DB.Save(modem);
                        }
                    else
                    {
                        if (ADSLRequest != null && ADSLRequest.ModemSerialNoID != null)
                        {
                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSLRequest.ModemSerialNoID);
                            modem.TelephoneNo = null;
                            modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                            modem.Detach();
                            DB.Save(modem);

                            ADSLRequest.ModemID = null;
                            ADSLRequest.ModemSerialNoID = null;
                            ADSLRequest.ModemMACAddress = "";
                        }
                    }
                }
            }

            if (_IsSalable)
            {
                RequestPayment requestPayment = new RequestPayment();

                BaseCost baseCostInstall = BaseCostDB.GetInstallCostForADSL();

                if (_Request.ID != 0)
                    requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCostInstall.ID);

                if (requestPayment == null)
                    requestPayment = new RequestPayment();

                requestPayment.BaseCostID = baseCostInstall.ID;
                requestPayment.RequestID = _Request.ID;
                requestPayment.Cost = baseCostInstall.Cost;
                requestPayment.Tax = baseCostInstall.Tax;
                if (baseCostInstall.Tax != null && baseCostInstall.Tax != 0)
                    requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCostInstall.Tax * 0.01) * requestPayment.Cost));
                else
                    requestPayment.AmountSum = requestPayment.Cost;
                requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                requestPayment.IsKickedBack = false;
                requestPayment.IsAccepted = false;

                requestPayment.Detach();
                DB.Save(requestPayment);

                if (ADSLRequest.ModemID != null)
                {
                    ADSLModem modem = ADSLModemDB.GetADSLModemById((int)ADSLRequest.ModemID);
                    BaseCost baseCost = BaseCostDB.GetModemCostForADSL();
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLRequest.ServiceID);
                    int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                    List<InstallmentRequestPayment> instalmentList = null;

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                    {
                        requestPayment = new RequestPayment();

                        if (service.IsModemInstallment != null)
                        {
                            if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                            else
                                requestPayment.Cost = modem.Price;

                            if (baseCost.Tax != null)
                                requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                            else
                                requestPayment.AmountSum = baseCost.Cost;

                            if ((bool)service.IsModemInstallment)
                            {
                                instalmentList = GenerateInstalments(true, requestPayment.ID, duration, (byte)DB.RequestType.ADSL, (long)requestPayment.AmountSum, false);
                                requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                            }
                            else
                                requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        }
                        else
                            requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                    }

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    if (service.ModemDiscount != null && service.ModemDiscount != 0)
                        requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                    else
                        requestPayment.Cost = modem.Price;

                    if (baseCost.Tax != null)
                        requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                    else
                        requestPayment.AmountSum = baseCost.Cost;

                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);

                    if (instalmentList != null)
                    {
                        foreach (InstallmentRequestPayment currentInstalment in instalmentList)
                        {
                            currentInstalment.RequestPaymentID = requestPayment.ID;
                            currentInstalment.Detach();
                            DB.Save(currentInstalment);
                        }
                    }
                }
                else
                {
                    BaseCost baseCost = BaseCostDB.GetModemCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment != null)
                    {
                        if (requestPayment.IsPaid.HasValue && requestPayment.IsPaid.Value)
                            throw new Exception("فیش مودم پرداخت شده است، امکان حذف آن وجود ندارد");

                        DB.Delete<RequestPayment>(requestPayment.ID);
                    }
                }

                if (ADSLRequest.RequiredInstalation.HasValue)
                {
                    if ((bool)ADSLRequest.RequiredInstalation.Value)
                    {
                        Service1 service = new Service1();
                        System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());

                        ADSLInstalCostCenter installCost = ADSLInstallCostCenterDB.GetADSLInstallCostByCenterID(CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())));
                        BaseCost baseCost = BaseCostDB.GetInstalCostForADSL();

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment == null)
                            requestPayment = new RequestPayment();

                        requestPayment.BaseCostID = baseCost.ID;
                        requestPayment.RequestID = _Request.ID;
                        requestPayment.Cost = installCost.InstallADSLCost;
                        requestPayment.Tax = baseCost.Tax;
                        if (baseCost.Tax != null)
                            requestPayment.AmountSum = installCost.InstallADSLCost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * installCost.InstallADSLCost);
                        else
                            requestPayment.AmountSum = installCost.InstallADSLCost;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;

                        requestPayment.Detach();
                        DB.Save(requestPayment);
                    }
                    else
                    {
                        BaseCost baseCost = BaseCostDB.GetInstalCostForADSL();

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment != null)
                        {
                            if (requestPayment.IsPaid.HasValue && requestPayment.IsPaid.Value)
                                throw new Exception("فیش نصب حضوری پرداخت شده است، امکان حذف آن وجود ندارد");

                            DB.Delete<RequestPayment>(requestPayment.ID);
                        }
                    }
                }

                if (ADSLRequest.ServiceID != null)
                {
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLRequest.ServiceID);
                    BaseCost baseCost = BaseCostDB.GetServiceCostForADSL();

                    List<InstallmentRequestPayment> instalmentList = null;

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                    {
                        requestPayment = new RequestPayment();
                        requestPayment.PaymentType = (byte)service.PaymentTypeID;

                        if (service.IsInstalment != null)
                            if ((bool)service.IsInstalment)
                                requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                    }
                    else
                    {
                        if (requestPayment.IsPaid != null)
                            if (requestPayment.IsPaid.HasValue && requestPayment.IsPaid.Value)
                                if (requestPayment.AmountSum != service.PriceSum)
                                    throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                        if (requestPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                            if (requestPayment.AmountSum != service.PriceSum)
                            {
                                List<InstallmentRequestPayment> instalments = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);
                                if (instalments.Count > 0)
                                    throw new Exception("هزینه سرویس قبلی قسط بندی شده است، لطفا ابتدا اقساط آن را حذف نمایید");
                            }
                    }

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    requestPayment.Cost = service.Price;
                    requestPayment.Abonman = (service.Abonman != null) ? service.Abonman * (int)service.DurationID : 0;
                    requestPayment.Tax = service.Tax;
                    requestPayment.AmountSum = service.PriceSum;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);

                    if (service.IsInstalment != null)
                        if ((bool)service.IsInstalment)
                        {
                            instalmentList = GenerateInstalments(true, requestPayment.ID, (int)service.DurationID, (byte)DB.RequestType.ADSL, (long)service.PriceSum, false);
                            DB.SaveAll(instalmentList);
                        }
                }

                if (ADSLRequest.AdditionalServiceID != null)
                {
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLRequest.AdditionalServiceID);
                    BaseCost baseCost = BaseCostDB.GetAdditionalServiceCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                    if (requestPayment == null)
                        requestPayment = new RequestPayment();
                    else
                    {
                        if (requestPayment.IsPaid.HasValue)
                            if (requestPayment.IsPaid.Value)
                                if (requestPayment.AmountSum != service.PriceSum)
                                    throw new Exception("هزینه ترافیک اضافی پرداخت شده است، امکان تغییر آن وجود ندارد");
                    }

                    requestPayment.BaseCostID = baseCost.ID;
                    requestPayment.RequestID = _Request.ID;
                    requestPayment.Cost = service.Price;
                    requestPayment.Tax = service.Tax;
                    requestPayment.AmountSum = service.PriceSum;
                    requestPayment.PaymentType = (byte)service.PaymentTypeID;
                    requestPayment.IsKickedBack = false;
                    requestPayment.IsAccepted = false;

                    requestPayment.Detach();
                    DB.Save(requestPayment);
                }
                else
                {
                    BaseCost baseCost = BaseCostDB.GetAdditionalServiceCostForADSL();

                    if (_Request.ID != 0)
                        requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);
                    if (requestPayment != null)
                    {
                        if (requestPayment.IsPaid.HasValue && requestPayment.IsPaid.Value)
                            throw new Exception("هزینه مربوط به ترافیک اضافی پرداخت شده است");
                        DB.Delete<Data.RequestPayment>(requestPayment.ID);
                    }
                }

                if (ADSLRequest.HasIP == true)
                {
                    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLRequest.ServiceID);

                    if (ADSLRequest.IPStaticID != null)
                    {
                        BaseCost baseCost = BaseCostDB.GetIPCostForADSL();
                        int serviceDuration = (int)ADSLRequest.IPDuration;

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment == null)
                            requestPayment = new RequestPayment();

                        requestPayment.BaseCostID = baseCost.ID;
                        requestPayment.RequestID = _Request.ID;

                        if (service.IPDiscount != null && service.IPDiscount != 0)
                            requestPayment.Cost = Convert.ToInt64((baseCost.Cost * serviceDuration) - (baseCost.Cost * serviceDuration * (service.IPDiscount * 0.01)));
                        else
                            requestPayment.Cost = baseCost.Cost * serviceDuration;

                        requestPayment.Tax = baseCost.Tax;
                        if (baseCost.Tax != null && baseCost.Tax != 0)
                            requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost));
                        else
                            requestPayment.AmountSum = requestPayment.Cost;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;

                        requestPayment.Detach();
                        DB.Save(requestPayment);
                    }

                    if (ADSLRequest.GroupIPStaticID != null)
                    {
                        BaseCost baseCost = BaseCostDB.GetIPCostForADSL();
                        int serviceduration = (int)ADSLRequest.IPDuration;

                        if (_Request.ID != 0)
                            requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                        if (requestPayment == null)
                            requestPayment = new RequestPayment();

                        requestPayment.BaseCostID = baseCost.ID;
                        requestPayment.RequestID = _Request.ID;
                        ADSLGroupIP groupIP = ADSLIPDB.GetADSLGroupIPById((long)ADSLRequest.GroupIPStaticID);

                        if (service.IPDiscount != null && service.IPDiscount != 0)
                            requestPayment.Cost = Convert.ToInt64((groupIP.BlockCount * baseCost.Cost * serviceduration) - (groupIP.BlockCount * baseCost.Cost * serviceduration * (service.IPDiscount * 0.01)));
                        else
                            requestPayment.Cost = groupIP.BlockCount * baseCost.Cost * serviceduration;

                        requestPayment.Tax = baseCost.Tax;
                        if (baseCost.Tax != null && baseCost.Tax != 0)
                            requestPayment.AmountSum = Convert.ToInt64(Convert.ToDouble(requestPayment.Cost) + Convert.ToDouble((baseCost.Tax * 0.01) * requestPayment.Cost));
                        else
                            requestPayment.AmountSum = requestPayment.Cost;
                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                        requestPayment.IsKickedBack = false;
                        requestPayment.IsAccepted = false;

                        requestPayment.Detach();
                        DB.Save(requestPayment);
                    }
                }
            }

            if (_RequestID == 0)
                RequestForADSL.SaveADSLRequestForWeb(_Request, ADSLRequest, _ADSL.IPStatic, _ADSL.GroupIPStatic, null, null, true);
            else
                RequestForADSL.SaveADSLRequestForWeb(_Request, ADSLRequest, _ADSL.IPStatic, _ADSL.GroupIPStatic, null, null, false);

            _RequestID = _Request.ID;
        }

        private void Save_ADSLChangeServiceRequest()
        {
            if (_ADSLChangeService._HasCreditAgent == false)
                throw new Exception("اعتبار نمایندگی شما کافی نمی باشد");

            if (_ADSLChangeService._HasCreditUser == false)
                throw new Exception("اعتبار کاربری شما کافی نمی باشد");

            Data.ADSLChangeService ADSLChangeService = new CRM.Data.ADSLChangeService();
            Data.ADSL ADSL = Data.ADSLDB.GetADSLByTelephoneNo(_ADSLChangeService.TeleInfo.TelephoneNo);

            ADSLChangeService.OldServiceID = (byte)ADSL.TariffID;
            if (!string.IsNullOrEmpty(_ADSLChangeService.ServiceValue))
                ADSLChangeService.NewServiceID = int.Parse(_ADSLChangeService.ServiceValue);
            else
                throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

            ADSLChangeService.LicenseLetterNo = _ADSLChangeService.NewLicenceLetterNoValue;
            ADSLChangeService.CommentCustomers = "";
            ADSLChangeService.ChangeServiceType = (byte)DB.ADSLChangeServiceType.Presence;
            ADSLChangeService.ChangeServiceActionType = (byte)Convert.ToInt32(_ADSLChangeService.ActionTypeValue);

            if (_RelatedRequestID != 0)
                _Request.RelatedRequestID = _RelatedRequestID;
            else
                _Request.RelatedRequestID = null;

            if (RequestID == 0)
            {
                if (_TelephoneNo == 0)
                    _Request.TelephoneNo = _Telephone.TelephoneNo;
                else
                    _Request.TelephoneNo = _TelephoneNo;
            }

            _Request.CenterID = int.Parse(CenterNameDropDownList.SelectedValue);
            _Request.CustomerID = _Customer.ID;
            _Request.IsVisible = true;

            if (RequestID == 0)
            {
                CRM.Data.Status status = DB.GetStatus(_RequestType.ID, (int)DB.RequestStatusType.Start);
                _Request.StatusID = status.ID;
            }

            _Request.CreatorUserID = DB.CurrentUser.ID;
            _Request.RequestTypeID = _RequestType.ID;
            _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;

            ADSLChangeService.NeedModem = _ADSLChangeService.HasNeedModem;

            if (_ADSLChangeService.HasNeedModem.HasValue)
            {
                if ((bool)_ADSLChangeService.HasNeedModem)
                {
                    if (string.IsNullOrEmpty(_ADSLChangeService.ModemTypeValue))
                        throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                    else
                    {
                        if (string.IsNullOrEmpty(_ADSLChangeService.ModemSerialNo))
                            throw new Exception("لطفا شماره سریال مودم را انتخاب نمایید، یا گزینه درخواست مودم را بردارید. ");

                        ADSLChangeService.ModemID = int.Parse(_ADSLChangeService.ModemTypeValue);
                        ADSLChangeService.ModemSerialNoID = int.Parse(_ADSLChangeService.ModemSerialNo);
                        ADSLChangeService.ModemMACAddress = _ADSLChangeService.ModemMACAddress;

                        ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSLChangeService.ModemSerialNoID);
                        modem.TelephoneNo = _Request.TelephoneNo;
                        modem.Status = (byte)DB.ADSLModemStatus.Reserve;

                        modem.Detach();
                        DB.Save(modem);
                    }
                }
                else
                {
                    if (ADSLChangeService.ModemSerialNoID != null)
                    {
                        ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)ADSLChangeService.ModemSerialNoID);
                        modem.TelephoneNo = null;
                        modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                        modem.Detach();
                        DB.Save(modem);

                        ADSLChangeService.ModemID = null;
                        ADSLChangeService.ModemSerialNoID = null;
                        ADSLChangeService.ModemMACAddress = "";
                    }
                }
            }

            if (RequestID == 0)
                RequestForADSL.SaveADSLChangeServiceRequest(_Request, ADSLChangeService, null, true);
            else
                RequestForADSL.SaveADSLChangeServiceRequest(_Request, ADSLChangeService, null, false);

            RequestPayment requestPayment = new RequestPayment();

            if (ADSLChangeService.NewServiceID != null)
            {
                BaseCost baseCost = BaseCostDB.GetServiceCostForADSLChangeTariff();
                ADSLService service = ADSLServiceDB.GetADSLServiceById((int)ADSLChangeService.NewServiceID);
                ADSLService oldService = ADSLServiceDB.GetADSLServiceById((int)ADSL.TariffID);
                List<InstallmentRequestPayment> instalmentList = null;
                int duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));

                switch ((byte)Convert.ToInt16(_ADSLChangeService.ActionTypeValue))
                {
                    case (byte)DB.ADSLChangeServiceActionType.ExtensionService:
                        if (service.PaymentTypeID != (byte)DB.PaymentType.NoPayment)
                        {
                            instalmentList = new List<InstallmentRequestPayment>();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment == null)
                            {
                                requestPayment = new RequestPayment();
                                requestPayment.PaymentType = (byte)service.PaymentTypeID;

                                if (service.IsInstalment != null)
                                    if ((bool)service.IsInstalment)
                                    {
                                        instalmentList = GenerateInstalments(true, requestPayment.ID, duration, (byte)DB.RequestType.ADSL, (long)service.PriceSum, false);
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                    }
                            }
                            else
                            {
                                if (requestPayment.IsPaid != null)
                                    if ((bool)requestPayment.IsPaid)
                                        if (requestPayment.AmountSum != service.PriceSum)
                                            throw new Exception("هزینه سرویس پرداخت شده است، امکان تغییر آن وجود ندارد");

                                if (requestPayment.PaymentType == (byte)DB.PaymentType.Instalment)
                                    if (requestPayment.AmountSum != service.PriceSum)
                                    {
                                        List<InstallmentRequestPayment> instalments = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByRequestPaymentID(requestPayment.ID);
                                        if (instalments.Count > 0)
                                            throw new Exception("هزینه سرویس قبلی قسط بندی شده است، لطفا ابتدا اقساط آن را حذف نمایید");
                                    }
                            }

                            requestPayment.BaseCostID = baseCost.ID;
                            requestPayment.RequestID = _Request.ID;
                            requestPayment.Cost = service.Price;
                            requestPayment.Abonman = (service.Abonman != null) ? service.Abonman * (int)duration : 0;
                            requestPayment.Tax = service.Tax;
                            requestPayment.AmountSum = service.PriceSum;
                            requestPayment.IsKickedBack = false;
                            requestPayment.IsAccepted = false;

                            requestPayment.Detach();
                            DB.Save(requestPayment);

                            if (instalmentList != null && instalmentList.Count != 0)
                            {
                                foreach (InstallmentRequestPayment currentInstalment in instalmentList)
                                {
                                    currentInstalment.RequestPaymentID = requestPayment.ID;
                                }

                                DB.SaveAll(instalmentList);
                            }
                        }

                        break;

                    case (byte)DB.ADSLChangeServiceActionType.ChangeService:

                        if (service.PaymentTypeID != (byte)DB.PaymentType.NoPayment)
                        {
                            double dayCount = 0;
                            double useDayCount = 0;
                            DateTime? now = DB.GetServerDate();
                            long refundAmount = 0;
                            long refundCustomer = 0;
                            dayCount = (int)oldService.DurationID * 30;
                            useDayCount = now.Value.Date.Subtract((DateTime)ADSL.InstallDate).TotalDays;
                            refundAmount = Convert.ToInt64(_ADSLChangeService.ReturnedCost);// oldCost - Convert.ToInt64((useDayCount * dayCost) + useCreditCost);


                            if (oldService.ModemDiscount != null && oldService.ModemDiscount != 0)
                            {
                                if (ADSL.ModemID != null)
                                {
                                    ADSLModem modem = ADSLModemDB.GetADSLModemById((int)ADSL.ModemID);
                                    double discountCost = (long)modem.Price * (long)oldService.ModemDiscount * 0.01;
                                    refundCustomer = Convert.ToInt64(Convert.ToDouble(discountCost / dayCount) * Convert.ToDouble(dayCount - useDayCount));
                                }
                            }

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment == null)
                            {
                                requestPayment = new RequestPayment();
                                requestPayment.PaymentType = (byte)service.PaymentTypeID;
                            }

                            requestPayment.BaseCostID = baseCost.ID;
                            requestPayment.RequestID = _Request.ID;
                            requestPayment.Cost = service.Price - refundAmount + refundCustomer;
                            requestPayment.Tax = baseCost.Tax;
                            if (service.Tax != null && service.Tax != 0)
                                requestPayment.AmountSum = (service.Price + (Convert.ToInt64(service.Tax * 0.01) * service.Price)) - refundAmount + refundCustomer;
                            else
                                requestPayment.AmountSum = service.PriceSum - refundAmount + refundCustomer;
                            requestPayment.PaymentType = (byte)service.PaymentTypeID;
                            requestPayment.IsKickedBack = false;
                            requestPayment.IsAccepted = false;

                            requestPayment.Detach();
                            DB.Save(requestPayment);

                            if (service.IsInstalment != null)
                                if ((bool)service.IsInstalment)
                                {
                                    instalmentList = GenerateInstalments(true, requestPayment.ID, duration, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.AmountSum, false);
                                    requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                }
                        }
                        if (ADSLChangeService.ModemID != null)
                        {
                            ADSLModem modem = ADSLModemDB.GetADSLModemById((int)ADSLChangeService.ModemID);
                            baseCost = BaseCostDB.GetModemCostForADSL();
                            service = ADSLServiceDB.GetADSLServiceById((int)ADSLChangeService.NewServiceID);
                            duration = Convert.ToInt32(ADSLServiceDB.GetADSLServiceDurationTitle((int)service.ID));
                            instalmentList = new List<InstallmentRequestPayment>();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment == null)
                            {
                                requestPayment = new RequestPayment();

                                if (service.IsModemInstallment != null)
                                {
                                    if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                        requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                                    else
                                        requestPayment.Cost = modem.Price;

                                    if (baseCost.Tax != null)
                                        requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                                    else
                                        requestPayment.AmountSum = baseCost.Cost;

                                    if ((bool)service.IsModemInstallment)
                                    {
                                        instalmentList = GenerateInstalments(true, requestPayment.ID, duration, (byte)DB.RequestType.ADSLChangeService, (long)requestPayment.AmountSum, false);
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Instalment;
                                    }
                                    else
                                        requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                                }
                                else
                                    requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
                            }

                            requestPayment.BaseCostID = baseCost.ID;
                            requestPayment.RequestID = _Request.ID;
                            if (service.ModemDiscount != null && service.ModemDiscount != 0)
                                requestPayment.Cost = Convert.ToInt64(modem.Price - (modem.Price * service.ModemDiscount * 0.01));
                            else
                                requestPayment.Cost = modem.Price;

                            if (baseCost.Tax != null)
                                requestPayment.AmountSum = requestPayment.Cost + (Convert.ToInt64((int)baseCost.Tax * 0.01) * requestPayment.Cost);
                            else
                                requestPayment.AmountSum = baseCost.Cost;

                            requestPayment.IsKickedBack = false;
                            requestPayment.IsAccepted = false;

                            requestPayment.Detach();
                            DB.Save(requestPayment);

                            if (instalmentList != null && instalmentList.Count != 0)
                            {
                                foreach (InstallmentRequestPayment currentInstalment in instalmentList)
                                {
                                    currentInstalment.RequestPaymentID = requestPayment.ID;
                                    currentInstalment.Detach();
                                    DB.Save(currentInstalment);
                                }
                            }
                        }
                        else
                        {
                            baseCost = BaseCostDB.GetModemCostForADSL();

                            if (_Request.ID != 0)
                                requestPayment = Data.RequestPaymentDB.GetRequestPaymentByRequsetIDandCostID(RequestID, baseCost.ID);

                            if (requestPayment != null)
                            {
                                if ((bool)requestPayment.IsPaid)
                                    throw new Exception("فیش مودم پرداخت شده است، امکان حذف آن وجود ندارد");
                                DB.Delete<RequestPayment>(requestPayment.ID);
                            }
                        }

                        break;

                    default:
                        break;
                }
            }

            RequestID = _Request.ID;
        }

        public bool Forward()
        {
            try
            {
                _IsForward = true;
                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(_RequestID) == false)
                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                switch (_RequestType.ID)
                {
                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                    case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                    case (byte)DB.RequestType.ChangeAddress:
                    case (byte)DB.RequestType.ChangeName:
                    case (byte)DB.RequestType.ChangeNo:
                    case (byte)DB.RequestType.CutAndEstablish:
                    case (byte)DB.RequestType.Dayri:
                    case (byte)DB.RequestType.Dischargin:
                    case (byte)DB.RequestType.E1:
                    case (byte)DB.RequestType.E1Fiber:
                    case (byte)DB.RequestType.OpenAndCloseZero:
                    case (byte)DB.RequestType.RefundDeposit:
                    case (byte)DB.RequestType.Reinstall:
                    case (byte)DB.RequestType.SpecialService:
                    case (byte)DB.RequestType.TitleIn118:
                        {
                            if (PreForward())
                            {
                                IsSaveSuccess = true;
                                IsForwardSuccess = true;
                            }
                            else
                            {
                                Save();

                                if (IsSaveSuccess == true)
                                    IsForwardSuccess = true;
                                else
                                    IsForwardSuccess = false;
                            }

                            if (RequestPaymentDB.PaidAllPaymentsbyRequestID(_RequestID) == false)
                                throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");

                            CheckRequestDocument();
                        }
                        break;

                    case (byte)DB.RequestType.ADSL:
                        CheckRequestDocument();
                        Forward_ADSL();
                        break;

                    default:
                        Save();
                        CheckRequestDocument();
                        IsForwardSuccess = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                string message = ex.Message.Replace("\'", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا در ارجاع : " + message + "');", true);
            }

            return IsForwardSuccess;
        }

        public bool PreForward()
        {
            Status Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);
            if (Status != null && (Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter))
                return true;

            //if (Data.StatusDB.IsFinalStep(this.currentStat))
            //    return true;

            return false;
        }

        public void CheckRequestDocument()
        {
            if (_Customer != null)
            {
                List<DocumentsByCustomer> NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_RequestType.ID, DB.GetServerDate(), _Customer.PersonType).Where(t => t.IsForcible == true).ToList();
                NeededDocs.ForEach(item =>
                {
                    if (!Data.RequestDB.CheckIsDocument(_Request.ID, item.DocumentTypeID))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + "دریافت مدرک " + item.DocumentName + " اجباری است." + "');", true);
                        throw new Exception("خطا در دریافت مدارک");
                    }
                });
            }
        }

        public void Forward_ADSL()
        {
            Save_ADSLRequest();

            ADSLRequest aDSLRequest = ADSLRequestDB.GetADSLRequestByID(RequestID);
            Data.Services.CRMWebService sms = new Data.Services.CRMWebService();

            if (_RequestID == 0 || _Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID)
            {
                string mobileNos = "";
                string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ADSLRegister));
                string customerFullName = CustomerDB.GetFullNameByCustomerID(aDSLRequest.CustomerOwnerID);
                Data.ADSL aDSL = ADSLDB.GetADSLByTelephoneNo(_Request.TelephoneNo);
                bool isNew = false;
                if (aDSL == null)
                {
                    aDSL = new Data.ADSL();
                    isNew = true;
                }

                aDSL.TelephoneNo = (long)_Request.TelephoneNo;
                aDSL.CustomerOwnerID = (long)aDSLRequest.CustomerOwnerID;
                aDSL.CustomerOwnerStatus = aDSLRequest.CustomerOwnerStatus;
                aDSL.UserName = _Request.TelephoneNo.ToString();
                aDSL.OrginalPassword = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 9);
                aDSL.HashPassword = GenerateMD5HashPassword(aDSL.OrginalPassword);
                aDSL.InstallDate = null;
                aDSL.ExpDate = null;
                aDSL.Status = (byte)DB.ADSLStatus.Pending;

                aDSL.Detach();
                DB.Save(aDSL, isNew);

                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID(aDSLRequest.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID(aDSLRequest.CustomerOwnerID);

                if (!string.IsNullOrWhiteSpace(mobileNos))
                {
                    SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ADSLRegister));
                    if (SmsService.IsActive == true)
                    {
                        message = message.Replace("CustomerName", customerFullName).Replace("TeleohoneNo", _Request.TelephoneNo.ToString()).Replace("UserName", aDSL.UserName).Replace("Password", aDSL.OrginalPassword).Replace("Enter", Environment.NewLine);
                        long result = sms.SendMessage("9126311256", "8007", mobileNos, "500042020", message);
                    }
                }

                IsForwardSuccess = true;
            }

            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                if (!string.IsNullOrEmpty(_ADSL.ServiceValue))
                    aDSLRequest.ServiceID = int.Parse(_ADSL.ServiceValue);
                else
                    throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

                if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                    if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                    {
                        RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                        RequestPaymentGridView.DataBind();
                        throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                    }
                List<int> statuses = WorkFlowDB.GetListNextStatesID(DB.Action.Confirm, _Request.StatusID);
                if (statuses.Count == 1)
                {
                    int nextStatusID = StatusDB.GetStatueByStatusID(statuses[0]).ID;
                    switch (nextStatusID)
                    {
                        case 198:
                            int currentMDFID = ADSLMDFRangeDB.GetMDFinRangebyTelephoneNo((long)_Request.TelephoneNo, _Request.CenterID);
                            List<Data.ADSLPort> portFreeList = ADSLMDFDB.GetFreeADSLPortByCenterID(_Request.CenterID, currentMDFID);
                            if (portFreeList.Count != 0)
                            {
                                Data.ADSLPort port = portFreeList[0];

                                port.Status = (byte)DB.ADSLPortStatus.reserve;
                                port.TelephoneNo = _Request.TelephoneNo;
                                port.Detach();
                                DB.Save(port);

                                aDSLRequest.ADSLPortID = port.ID;
                                aDSLRequest.Detach();
                                DB.Save(aDSLRequest);
                            }
                            else
                                throw new Exception("در این مرکز تجهیزات فنی وجود ندارد");
                            break;

                        case 196:
                            break;

                        default:
                            break;
                    }
                }

                aDSLRequest.LicenseLetterNo = _ADSL.LicenceLetterNo;
                aDSLRequest.CustomerPriority = (byte)DB.ADSLCustomerPriority.Normal;
                aDSLRequest.RequiredInstalation = _ADSL.RequiredInstalationIsChecked;
                aDSLRequest.NeedModem = _ADSL.HasNeedModem;
                aDSLRequest.Status = false;

                if (_ADSL.HasNeedModem != null)
                {
                    if ((bool)_ADSL.HasNeedModem)
                        if (string.IsNullOrEmpty(_ADSL.ModemTypeValue))
                            throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                        else
                        {
                            aDSLRequest.ModemID = int.Parse(_ADSL.ModemTypeValue);
                            if (aDSLRequest.RequiredInstalation == null || (bool)aDSLRequest.RequiredInstalation == false)
                            {
                                if (string.IsNullOrEmpty(_ADSL.ModemSerialNo))
                                    throw new Exception("لطفا شماره سریال مودم را وارد نمایید");
                                if (string.IsNullOrEmpty(_ADSL.ModemMACAddress))
                                    throw new Exception("لطفا آدرس فیزیکی مودم را وارد نمایید");
                            }
                        }

                    if (aDSLRequest.ModemSerialNoID != null)
                    {
                        ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)aDSLRequest.ModemSerialNoID);
                        modem.TelephoneNo = _Request.TelephoneNo;
                        modem.Status = (byte)DB.ADSLModemStatus.Sold;

                        modem.Detach();
                        DB.Save(modem);
                    }
                }

                //ADSLSellerAgentUser user = ADSLSellerGroupDB.GetADSLSellerAgentUserByID((int)_Request.CreatorUserID);
                //if (user != null)
                //{
                //    ADSLSellerAgent sellerAgent = ADSLSellerGroupDB.GetADSLSellerAgentByID(user.SellerAgentID);
                //    ADSLService service = ADSLServiceDB.GetADSLServiceById((int)aDSLRequest.ServiceID);

                //    if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                //    {
                //        long paymentAmount = RequestPaymentDB.GetAmountSumforPaidPayment(_Request.ID, (byte)DB.PaymentType.Cash);
                //        sellerAgent.CreditCashUse = sellerAgent.CreditCashUse + paymentAmount;// (_ADSL._SumPriceIP + _ADSL._SumPriceService + _ADSL._SumPriceModem + ranjeCost + installCost);
                //        sellerAgent.CreditCashRemain = sellerAgent.CreditCashRemain - paymentAmount;// (_ADSL._SumPriceIP + _ADSL._SumPriceService + _ADSL._SumPriceModem + ranjeCost + installCost);

                //        user.CreditCashUse = user.CreditCashUse + paymentAmount;//(_ADSL._SumPriceIP + _ADSL._SumPriceService + _ADSL._SumPriceModem + ranjeCost + installCost);
                //        user.CreditCashRemain = user.CreditCashRemain - paymentAmount;//(_ADSL._SumPriceIP + _ADSL._SumPriceService + _ADSL._SumPriceModem + ranjeCost + installCost);

                //        sellerAgent.Detach();
                //        DB.Save(sellerAgent);

                //        user.Detach();
                //        DB.Save(user);
                //    }
                //}

                string mobileNos = "";
                string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.SaleADSL));
                string customerFullName = CustomerDB.GetFullNameByCustomerID(aDSLRequest.CustomerOwnerID);
                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID(aDSLRequest.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID(aDSLRequest.CustomerOwnerID);

                if (!string.IsNullOrWhiteSpace(mobileNos))
                {
                    SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.SaleADSL));
                    if (SmsService.IsActive == true)
                    {
                        message = message.Replace("CustomerName", customerFullName).Replace("TelephoneNo", _Request.TelephoneNo.ToString()).Replace("Enter", Environment.NewLine);
                        long result = sms.SendMessage("9126311256", "8007", mobileNos, "500042020", message);
                    }
                }
                IsForwardSuccess = true;
            }
        }

        public bool Refund()
        {
            try
            {
                switch (_RequestType.ID)
                {
                    case (byte)DB.RequestType.ADSL:
                        Refund_ADSL();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                IsRefundSuccess = false;
                string message = ex.Message.Replace("\'", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا در استرداد ودیعه : " + message + "');", true);
            }

            return IsRefundSuccess;
        }

        public void Refund_ADSL()
        {
            IsRefundSuccess = true;
        }

        public string GenerateMD5HashPassword(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public bool Cancel()
        {
            try
            {
                _RequestID = _Request.ID;
                _Request.IsCancelation = true;

                Data.CancelationRequestList cancelationRequest = new CancelationRequestList();
                cancelationRequest.ID = _RequestID;
                cancelationRequest.EntryDate = DB.GetServerDate();
                cancelationRequest.UserID = Folder.User.Current.ID;

                cancelationRequest.Detach();
                DB.Save(cancelationRequest, true);

                _Request.Detach();
                DB.Save(_Request);

                IsCancelSuccess = true;
            }
            catch
            {
                IsCancelSuccess = false;
            }
            return IsCancelSuccess;
        }

        public void CloseRequestForm()
        {
            Response.Write("<script> window.close(); </script>");
        }

        public bool Deny()
        {
            try
            {
                switch (_RequestType.ID)
                {
                    case (int)DB.RequestType.ADSL:
                        Deny_ADSLRequest();
                        break;
                }

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا در رد درخواست، " + message + "');", true);
            }

            return IsRejectSuccess;
        }

        private void Deny_ADSLRequest()
        {
            if (RequestPaymentDB.HasPaidRequestPaymentByRequestID(_RequestID))
                throw new Exception("هزینه این درخواست پرداخت شده است، امکان رد آن وجود ندارد");
            else
            {
                List<InstallmentRequestPayment> instalments = InstallmentRequestPaymentDB.GetInstalmentbyRequestID(_RequestID);
                if (instalments != null)
                    if (instalments.Count != 0)
                        DB.DeleteAll<InstallmentRequestPayment>(instalments.Select(t => t.ID).ToList());

                List<RequestPayment> noPaidPayments = RequestPaymentDB.GetRequestPaymentByRequestID(_RequestID);
                DB.DeleteAll<RequestPayment>(noPaidPayments.Select(t => t.ID).ToList());

                CRM.Data.ADSLRequest ADSLRequest = DB.SearchByPropertyName<Data.ADSLRequest>("ID", _RequestID).SingleOrDefault();

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                    ADSLRequest.CommentCustomers = _ADSL.CommentCustomers;

                ADSLRequest.Detach();
                DB.Save(ADSLRequest);
            }
        }

        public bool Confirm()
        {
            try
            {
                switch (_RequestType.ID)
                {
                    case (int)DB.RequestType.ADSL:
                        Confirm_ADSLRequest();
                        break;

                    case (int)DB.RequestType.ADSLChangeService:
                        Confirm_ADSLChangeServiceRequest();
                        break;
                    default:
                        break;
                }

                IsConfirmSuccess = true;
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا در  تایید درخواست :" + message + "');", true);
            }

            return IsConfirmSuccess;
        }

        private void Confirm_ADSLRequest()
        {
            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                CRM.Data.ADSLRequest ADSLRequest = DB.SearchByPropertyName<Data.ADSLRequest>("ID", RequestID).SingleOrDefault();
                ADSLRequest.CommentCustomers = _ADSL.CommentCustomers;

                ADSLRequest.Detach();
                DB.Save(ADSLRequest);
            }
        }

        private void Confirm_ADSLChangeServiceRequest()
        {
            Save_ADSLChangeServiceRequest();
            LoadData();

            CRM.Data.ADSLChangeService aDSLChangeService = ADSLChangeTariffDB.GetADSLChangeServicebyID(RequestID);
            Data.ADSL aDSL = ADSLDB.GetADSLByUserName(_Request.TelephoneNo.ToString());

            if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                if (RequestPaymentDB.PaidAllPaymentsbyRequestID(RequestID) == false)
                {
                    RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                    RequestPaymentGridView.DataBind();
                    throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                }

            if (_ADSLChangeService.HasNeedModem.HasValue)
            {
                if ((bool)_ADSLChangeService.HasNeedModem.Value)
                    if (string.IsNullOrEmpty(_ADSLChangeService.ModemTypeValue))
                        throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                    else
                    {
                        aDSLChangeService.ModemID = int.Parse(_ADSLChangeService.ModemTypeValue);

                        if (string.IsNullOrEmpty(_ADSLChangeService.ModemSerialNo))
                            throw new Exception("لطفا شماره سریال مودم را وارد نمایید");
                        else
                        {
                            ADSLModemProperty modem = ADSLModemPropertyDB.GetADSLModemPropertiesById(int.Parse(_ADSLChangeService.ModemSerialNo));
                            modem.TelephoneNo = _Request.TelephoneNo;
                            modem.Status = (byte)DB.ADSLModemStatus.Sold;

                            modem.Detach();
                            DB.Save(modem);
                        }
                    }
            }

            ADSLService service = ADSLServiceDB.GetADSLServiceById((int)aDSLChangeService.NewServiceID);
            ADSLServiceTraffic traffic = ADSLServiceDB.GetADSLServiceTraffiBycID((int)service.TrafficID);

            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            switch ((byte)Convert.ToInt16(_ADSLChangeService.ActionTypeValue))
            {
                case (byte)DB.ADSLChangeServiceActionType.ExtensionService:
                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userAuthentication.Add("deposit", traffic.Credit.ToString());
                    userAuthentication.Add("is_absolute_change", false);
                    userAuthentication.Add("deposit_type", "renew");
                    userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Extension Service Request (Renew)");

                    try
                    {
                        ibsngService.changeDeposit(userAuthentication);
                    }
                    catch (Exception)
                    {
                        throw new Exception("تغییر اعتبار با موفقیت انجام نشد");
                    }

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userInfo.Add("renew_next_group", service.IBSngGroupName);
                    userInfo.Add("renew_remove_user_exp_dates", "1");
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    try
                    {
                        ibsngService.UpdateUserAttrs(userAuthentication);
                        aDSLChangeService.IsIBSngUpdated = true;
                    }
                    catch (Exception)
                    {
                        throw new Exception("تغییر گروه با موفقیت انجام نشد");
                    }

                    break;

                case (byte)DB.ADSLChangeServiceActionType.ChangeService:

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userAuthentication.Add("deposit", traffic.Credit.ToString());
                    userAuthentication.Add("is_absolute_change", true);
                    userAuthentication.Add("deposit_type", "renew");
                    userAuthentication.Add("deposit_comment", "Change by Pendar_CRM, Change Service Request (Renew)");

                    try
                    {
                        ibsngService.changeDeposit(userAuthentication);
                    }
                    catch (Exception)
                    {
                        throw new Exception("تغییر اعتبار با موفقیت انجام نشد");
                    }

                    userAuthentication.Clear();

                    userAuthentication.Add("auth_name", "pendar");
                    userAuthentication.Add("auth_pass", "Pendar#!$^");
                    userAuthentication.Add("auth_type", "ADMIN");

                    userAuthentication.Add("user_id", aDSL.UserID);

                    userInfo.Add("group_name", service.IBSngGroupName);
                    userAuthentication.Add("attrs", userInfo);
                    userAuthentication.Add("to_del_attrs", "");

                    try
                    {
                        ibsngService.UpdateUserAttrs(userAuthentication);
                        aDSLChangeService.IsIBSngUpdated = true;
                    }
                    catch (Exception)
                    {
                        throw new Exception("تغییر گروه با موفقیت انجام نشد");
                    }

                    break;

                default:
                    break;
            }
            aDSL.TariffID = aDSLChangeService.NewServiceID;
            aDSL.LicenseLetterNo = aDSLChangeService.LicenseLetterNo;
            if (aDSLChangeService.NeedModem == true)
                aDSL.ModemID = aDSLChangeService.ModemID;

            aDSL.InstallDate = DB.GetServerDate();
            aDSL.ExpDate = DB.GetServerDate().AddMonths((int)service.DurationID);

            aDSL.Detach();
            DB.Save(aDSL);

            aDSLChangeService.FinalUserID = DB.CurrentUser.ID;
            aDSLChangeService.FinalDate = DB.GetServerDate();
            //aDSLChangeTariff.FinalComment = _ADSLChangeTariff.FinalCommentTextBox.Text;
            aDSLChangeService.Status = true;

            aDSLChangeService.Detach();
            DB.Save(aDSLChangeService);

            ADSLHistory aDSLHistory = new ADSLHistory();
            aDSLHistory.TelephoneNo = _Request.TelephoneNo.ToString();
            aDSLHistory.ServiceID = aDSL.TariffID;
            aDSLHistory.InsertDate = DB.GetServerDate();

            aDSLHistory.Detach();
            DB.Save(aDSLHistory, true);

            CRM.Data.Schema.ADSLChangeTariff ADSLChangeTariffLog = new Data.Schema.ADSLChangeTariff();
            ADSLChangeTariffLog.OldTariffID = (int)aDSLChangeService.OldServiceID;
            ADSLChangeTariffLog.NewTariffID = (int)aDSLChangeService.NewServiceID;

            RequestLog requestLog = new RequestLog();
            requestLog = new RequestLog();
            requestLog.RequestID = _Request.ID;
            requestLog.RequestTypeID = _Request.RequestTypeID;
            requestLog.TelephoneNo = _ADSLChangeService.TeleInfo.TelephoneNo;

            requestLog.Description = XElement.Parse(LogSchemaUtility.Serialize<CRM.Data.Schema.ADSLChangeTariff>(ADSLChangeTariffLog, true));

            requestLog.Detach();
            DB.Save(requestLog);

            //ADSLSellerAgentUser user = ADSLSellerGroupDB.GetADSLSellerAgentUserByID((int)_Request.CreatorUserID);
            //if (user != null)
            //{
            //    ADSLSellerAgent sellerAgent = ADSLSellerGroupDB.GetADSLSellerAgentByID(user.SellerAgentID);
            //    if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash && service.IsInstalment == false)
            //    {
            //        sellerAgent.CreditCashUse = sellerAgent.CreditCashUse + _ADSLChangeService.SumPriceService;
            //        sellerAgent.CreditCashRemain = sellerAgent.CreditCashRemain - _ADSLChangeService.SumPriceService;

            //        user.CreditCashUse = user.CreditCashUse + _ADSLChangeService.SumPriceService;
            //        user.CreditCashRemain = user.CreditCashRemain - _ADSLChangeService.SumPriceService;

            //        sellerAgent.Detach();
            //        DB.Save(sellerAgent);

            //        user.Detach();
            //        DB.Save(user);
            //    }
            //}

            CRM.Application.CRMWebService.CRMWebService sms = new CRM.Application.CRMWebService.CRMWebService();
            string mobileNos = "";
            string customerFullName = "";

            if (aDSL.CustomerOwnerID != null)
            {
                if (!string.IsNullOrWhiteSpace(CustomerDB.GetCustomerMobileByID((long)aDSL.CustomerOwnerID)))
                    mobileNos = CustomerDB.GetCustomerMobileByID((long)aDSL.CustomerOwnerID);

                customerFullName = CustomerDB.GetFullNameByCustomerID((long)aDSL.CustomerOwnerID);
            }

            string message = SMSServiceDB.GetSMSMessage(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ChangeService));

            SMSService SmsService = SMSServiceDB.GetSMSService(Helper.GetEnumDescriptionByValue(typeof(DB.SMSServiceTitle), (byte)DB.SMSServiceTitle.ChangeService));
            if (SmsService.IsActive == true)
            {
                message = message.Replace("CustomerName", customerFullName).Replace("TelephoneNo", _Request.TelephoneNo.ToString()).Replace("Enter", Environment.NewLine);

                //long result = sms.SendMessage("9126311256", "8007", mobileNos, "500042020", message);
                // sms.SendMessage(mobileNos, message);
            }
        }

        public bool SaveWaitingList()
        {
            try
            {
                switch (_RequestType.ID)
                {
                    case (int)DB.RequestType.ADSL:
                        SaveWaitingList_ADSLRequest();
                        break;
                }

                IsSaveWatingListSuccess = true;
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('خطا در ذخیره درخواست در لیست انتظار : " + message + "');", true);
            }

            return IsSaveWatingListSuccess;
        }

        private void SaveWaitingList_ADSLRequest()
        {
            Save();

            if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
            {
                CRM.Data.ADSLRequest ADSLRequest = new CRM.Data.ADSLRequest();

                if (!string.IsNullOrEmpty(_ADSL.ServiceValue))
                    ADSLRequest.ServiceID = int.Parse(_ADSL.ServiceValue);
                else
                    throw new Exception("لطفا سرویس مورد نظر را تعیین نمایید");

                ADSLRequest.LicenseLetterNo = _ADSL.LicenceLetterNo;
                ADSLRequest.CustomerPriority = (byte)DB.ADSLCustomerPriority.Normal;
                ADSLRequest.RequiredInstalation = _ADSL.RequiredInstalationIsChecked;
                ADSLRequest.NeedModem = _ADSL.HasNeedModem;

                if (_ADSL.HasNeedModem.HasValue)
                {
                    if ((bool)_ADSL.HasNeedModem)
                        if (string.IsNullOrEmpty(_ADSL.ModemTypeValue))
                            throw new Exception("لطفا مودم مورد نظر را تعیین نمایید");
                        else
                            ADSLRequest.ModemID = int.Parse(_ADSL.ModemTypeValue);
                }

                if (_Request.RequestPaymentTypeID == (byte)DB.PaymentType.Cash)
                {
                    if (RequestPaymentDB.PaidAllPaymentsbyRequestID(_RequestID) == false)
                        throw new Exception("لطفا پرداخت های نقدی مربوطه را بپردازید");
                }
            }
            WaitingList waitingList = new WaitingList();
            waitingList.ReasonID = (byte)DB.WaitingListReason.PortLess;
            waitingList.Status = false;

            ADSLRequest aDSLRequest = ADSLRequestDB.GetADSLRequestByID(_RequestID);
            RequestForADSL.SaveWaitingList(_RequestID, _Request, waitingList);

            _RequestID = _Request.ID;
        }

        private void FillVisitInfo()
        {
            long addressID = -1;
            addressID = GetAddressOfRequest();
            if (!(addressID == -1 || addressID == null))
            {

                VisitInfoList = Data.VisitAddressDB.GetVisitAddressByRequestID(_Request.ID, (long)addressID).OrderByDescending(t => t.ID).ToList();
                if (VisitInfoList.Count != 0)
                {
                    RelatedDocumentRadTabStrip.Tabs.FindTabByText("بازدید از محل").Visible = true;
                    //VisitInfo.Visibility = Visibility.Visible;
                    //CrossPostComboBoxColumn.ItemsSource = Data.PostDB.GetPostCheckable();
                    //VisitInfo.DataContext = _VisitInfoList; 
                    //VisitInfoGridView.DataSource = VisitInfoList;
                    //VisitInfoGridView.DataBind();
                }
            }
        }

        public List<InstallmentRequestPayment> GenerateInstalments(bool daily, long requestPaymentID, int instalmentCount, byte requestTypeID, long amount, bool isCheque)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int _floorValue = 1000;
                List<InstallmentRequestPayment> installmentRequestPayments = new List<InstallmentRequestPayment>();
                int PaymentAmountEachPart = 0;

                if (requestTypeID == (byte)DB.RequestType.ADSL || requestTypeID == (byte)DB.RequestType.ADSLChangeService)
                {
                    string startDate = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    string endateCount = Helper.AddMonthToPersianDate(startDate, instalmentCount);
                    string endDate = Helper.AddMonthToPersianDate(startDate, instalmentCount);
                    string startDateEachPart = startDate;
                    if (daily == true)
                    {
                        double days = Helper.PersianToGregorian(endDate).Value.Date.Subtract(Helper.PersianToGregorian(startDate).Value.Date).TotalDays;
                        PaymentAmountEachPart = (int)(amount / (decimal)days);
                    }
                    else
                        PaymentAmountEachPart = (int)(amount / (decimal)instalmentCount);

                    for (int i = 1; i <= instalmentCount; i++)
                    {
                        InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                        int dateEachPart = 1;
                        installmentRequestPayment.RequestPaymentID = requestPaymentID;
                        installmentRequestPayment.TelephoneNo = _Request.TelephoneNo;
                        installmentRequestPayment.IsCheque = isCheque;

                        installmentRequestPayment.StartDate = Helper.PersianToGregorian(startDateEachPart).Value.Date;
                        string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

                        installmentRequestPayment.EndDate = Helper.PersianToGregorian(endDateEachPart).Value.Date;

                        if (daily == true)
                            dateEachPart = (int)installmentRequestPayment.EndDate.Date.Subtract(installmentRequestPayment.StartDate.Date).TotalDays + 1;

                        if (instalmentCount == i)
                            installmentRequestPayment.Cost = (long)(amount - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                        else
                            installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                        startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);

                        installmentRequestPayments.Add(installmentRequestPayment);
                    }
                }
                else
                {
                    string startDate = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    string endDate = Helper.AddMonthToPersianDate(startDate, instalmentCount);
                    string startDateEachPart = startDate;

                    if (daily == true)
                    {
                        double days = Helper.PersianToGregorian(endDate).Value.Date.Subtract(Helper.PersianToGregorian(startDate).Value.Date).TotalDays;
                        PaymentAmountEachPart = (int)(amount / (decimal)days);
                    }
                    else
                        PaymentAmountEachPart = (int)(amount / (decimal)instalmentCount);

                    for (int i = 1; i <= instalmentCount; i++)
                    {
                        InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                        int dateEachPart = 1;
                        installmentRequestPayment.RequestPaymentID = requestPaymentID;
                        installmentRequestPayment.IsCheque = isCheque;

                        installmentRequestPayment.StartDate = Helper.PersianToGregorian(startDateEachPart).Value.Date;
                        string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

                        installmentRequestPayment.EndDate = Helper.PersianToGregorian(endDateEachPart).Value.Date;
                        if (daily == true)
                            dateEachPart = (int)installmentRequestPayment.EndDate.Date.Subtract(installmentRequestPayment.StartDate.Date).TotalDays + 1;

                        if (instalmentCount == i)
                            installmentRequestPayment.Cost = (long)(amount - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                        else
                            installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                        startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);
                        installmentRequestPayments.Add(installmentRequestPayment);
                    }
                }

                return installmentRequestPayments;
            }
        }
        private void InsertRequestPayment()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                if (_IsSalable)
                {
                    List<RequestPayment> requestPaymentList = new List<RequestPayment>();
                    DateTime dateTime = DB.GetServerDate();
                    switch (_Request.RequestTypeID)
                    {
                        case (byte)DB.RequestType.ADSL:
                        case (byte)DB.RequestType.ADSLChangeService:
                            break;

                        default:

                            List<BaseCost> currentRequestTypeCost = Data.BaseCostDB.GetBaseCostByRequestTypeID(_Request.RequestTypeID);
                            if (_Request.RequestTypeID == (byte)DB.RequestType.Dayri || _Request.RequestTypeID == (byte)DB.RequestType.Reinstall)
                            {
                                if (InstallReqeust.RegisterAt118 == true)
                                    currentRequestTypeCost = currentRequestTypeCost.Union(Data.BaseCostDB.GetBaseCostByRequestTypeID((byte)DB.RequestType.TitleIn118)).ToList();

                                if (InstallReqeust.ClassTelephone != (int)DB.ClassTelephone.LimitLess)
                                    currentRequestTypeCost = currentRequestTypeCost.Union(Data.BaseCostDB.GetBaseCostByRequestTypeID((byte)DB.RequestType.OpenAndCloseZero)).ToList();
                            }
                            List<RequestPayment> requestPayments = Data.RequestPaymentDB.GetRequestPaymentByRequestID(RequestID);
                            List<BaseCost> NewRequestTypeCost = currentRequestTypeCost.Where(t => !requestPayments.Select(rp => rp.BaseCostID).Contains(t.ID)).ToList();

                            long addressID = -1;
                            addressID = GetAddressOfRequest();
                            int? outBoundMeter = Data.VisitAddressDB.GetOutBoundMeterByRequestID(_Request.ID, addressID);
                            long? cableMeter = Data.VisitAddressDB.GetCableMeterByRequestID(_Request.ID, addressID);

                            foreach (BaseCost cost in NewRequestTypeCost)
                            {
                                if (cost.Cost != 0 && cost.UseOutBound == false)
                                {
                                    RequestPayment requestPayment = new RequestPayment();
                                    requestPayment.InsertDate = dateTime;
                                    requestPayment.BaseCostID = cost.ID;
                                    requestPayment.RequestID = RequestID;
                                    requestPayment.PaymentType = (byte)cost.PaymentType;
                                    requestPayment.IsKickedBack = false;
                                    requestPayment.IsAccepted = false;
                                    requestPayment.IsPaid = false;
                                    requestPayment.Cost = cost.Cost;
                                    requestPayment.Tax = cost.Tax;

                                    if (cost.Tax != null)
                                        requestPayment.AmountSum = cost.Cost + Convert.ToInt64(cost.Tax * 0.01 * cost.Cost);
                                    else
                                        requestPayment.AmountSum = cost.Cost;

                                    requestPaymentList.Add(requestPayment);

                                    requestPayment.Detach();
                                    DB.Save(requestPayment);
                                }
                                else if (cost.Cost == 0 && cost.IsFormula == true)
                                {
                                    string formula = cost.Formula;
                                    if (cost.UseOutBound == true)
                                    {
                                        if (outBoundMeter.HasValue == true)
                                            formula = cost.Formula.Replace("outBoundMeter", outBoundMeter.ToString());
                                        else
                                            continue;
                                    }

                                    if (cost.UseCableMeter == true)
                                    {
                                        if (cableMeter.HasValue == true)
                                            formula = cost.Formula.Replace("CableMeter", cableMeter.ToString());
                                        else
                                            continue;
                                    }

                                    if (cost.UseZeroBlock == true)
                                    {
                                        byte? zeroBlockValue = Data.ZeroStatusDB.GetZeroBlockByRequestID(_Request);
                                        if (zeroBlockValue != null)
                                            formula = cost.Formula.Replace("ZeroBlock", Convert.ToString(zeroBlockValue));
                                        else
                                        {
                                            Folder.MessageBox.ShowInfo("برای این روال بستن صفر در نظر گرفته نشده است لطفا با مدیر سیستم تماس بگیرید");
                                            continue;
                                        }
                                    }

                                    double costFormula = Calculate.Execute(formula);
                                    if (costFormula == 0) continue;

                                    RequestPayment requestPayment = new RequestPayment();
                                    requestPayment.InsertDate = dateTime;
                                    requestPayment.BaseCostID = cost.ID;
                                    requestPayment.RequestID = RequestID;
                                    requestPayment.PaymentType = (byte)cost.PaymentType;
                                    requestPayment.IsKickedBack = false;
                                    requestPayment.IsAccepted = false;
                                    requestPayment.IsPaid = false;

                                    requestPayment.Cost = 0;
                                    requestPayment.Tax = cost.Tax;
                                    if (cost.Tax != null)
                                        requestPayment.AmountSum = Convert.ToInt64(costFormula + cost.Tax * 0.01 * costFormula);
                                    else
                                        requestPayment.AmountSum = Convert.ToInt64(costFormula);

                                    requestPaymentList.Add(requestPayment);

                                    requestPayment.Detach();
                                    DB.Save(requestPayment);
                                }
                            }
                            break;
                    }
                }
                scope.Complete();
            }

            RequestPaymentGridView.DataSource = Data.RequestPaymentDB.GetRequestPaymentInfoByRequestID(RequestID);
            RequestPaymentGridView.DataBind();
        }

        private long GetAddressOfRequest()
        {
            long? addressID = -1;
            //switch (_request.RequestTypeID)
            //{
            //    case (int)DB.RequestType.Dayri:m
            //    case (int)DB.RequestType.Reinstall:
            //        addressID = _installReqeust.InstallAddressID;
            //        break;
            //    case (int)DB.RequestType.ChangeLocationCenterToCenter:
            //    case (int)DB.RequestType.ChangeLocationCenterInside:
            //        addressID = _changeLocation.NewInstallAddressID;
            //        break;
            //    case (int)DB.RequestType.E1:
            //        addressID = _e1.InstallAddressID;
            //        break;
            //    case (int)DB.RequestType.SpecialWire:
            //        addressID = _specialWire.InstallAddressID;
            //        break;
            //}
            return addressID ?? -1;
        }

        private string ToStringSpecial(object value)
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "Null")
                    return "";
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        #endregion Methods
    }
}