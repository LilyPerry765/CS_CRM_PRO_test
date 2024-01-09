using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using System.Transactions;

namespace CRM.Website.Viewes
{
    public partial class RequestDocumentForm : System.Web.UI.Page
    {
        #region Properties & Fields

        private RequestDocument _reqDoc;
        private long _customerID;
        private long _documentRequestTypeID;
        private long _id;
        private byte _docType;

        private long _requestID;
        private byte[] _fileBytes;
        private string _extension;

        private long _requestDocumentID;
        private RequestDocument _lastReqDoc;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request.QueryString["RequestDocumentID"], out _requestDocumentID);
            byte.TryParse(Request.QueryString["DocType"], out _docType);

            if (_requestDocumentID > 0)
            {
                _reqDoc = DB.GetAllEntity<RequestDocument>().Where(t => t.ID == _requestDocumentID).SingleOrDefault();
                if (_reqDoc != null)
                    _id = _reqDoc.ID;
            }
            else
                _reqDoc = new RequestDocument();


            if (Request.QueryString["CustomerID"] == null)
                _customerID = -1;
            else
                long.TryParse(Request.QueryString["CustomerID"], out _customerID);

            long.TryParse(Request.QueryString["RequestID"], out _requestID);
            long.TryParse(Request.QueryString["DocumentRequestTypeID"], out _documentRequestTypeID);
            byte.TryParse(Request.QueryString["DocType"], out _docType);

            LoadData();
        }

        protected void ViewButton_Click(object sender, EventArgs e)
        {
            if (_fileBytes != null)
            {
                HttpContext.Current.Session["FileBytes"] = _fileBytes;
                Page.ClientScript.RegisterStartupScript(GetType(), "OpenDocumentViewForm", "ModalDialog('/Viewes/DocumentViewForm.aspx',null,300,400);", false);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "فایل موجود نمیباشد." + " !" + "');", true);
            }
        }

        //protected void UploadFileButton_Click(object sender, EventArgs e)
        //{
        //    //Page.ClientScript.RegisterStartupScript(GetType(), "OpenDocumentInputForm", "ModalDialog('/Viewes/DocumentInputForm.aspx',null,300,400);", false);

        //    _fileBytes = HttpContext.Current.Session["FileBytes"] as byte[];
        //    _extension = HttpContext.Current.Session["Extension"].ToString();
        //    if (_fileBytes != null && _extension != string.Empty)
        //        ViewButton.CssClass = "shown";
        //}

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateTime = DB.GetServerDate();
                Guid? oldGuid = new Guid();
                //using (TransactionScope MainTransactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                //{
                oldGuid = _reqDoc.DocumentsFileID;

                if (NewRadioButton.Checked)
                {
                    if (DocumentFileUpload.HasFile)
                    {
                        if (DocumentFileUpload.FileContent.Length > 1048576)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "فایل پیوست شده بیش از یک مگابایت حجم دارد" + " !" + "');", true);
                            return;
                        }

                        _fileBytes = DocumentFileUpload.FileBytes;
                        _extension = System.IO.Path.GetExtension(DocumentFileUpload.FileName);
                    }
                    if (_fileBytes == null && _extension == string.Empty)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "فایل موجود نمیباشد." + " !" + "');", true);
                        return;
                    }
                    else
                        _reqDoc.DocumentsFileID = DocumentsFileDB.SaveFileInDocumentsFile(_fileBytes, _extension);


                    if (_id == 0)
                    {
                        _reqDoc.CustomerID = _customerID;
                        _reqDoc.DocumentRequestTypeID = _documentRequestTypeID;
                        _reqDoc.InsertDate = dateTime;

                        RequestDocumnetDB.SaveRequestDocumentForWeb(_reqDoc, _requestID, true);
                    }
                    else
                    {
                        _reqDoc.Detach();
                        DB.Save(_reqDoc);
                        if (oldGuid != null)
                            DocumentsFileDB.DeleteDocumentsFileTable((Guid)oldGuid);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('مدرک با موفقیت اضافه شد.!'); window.close();", true);
                }
                else
                {
                    if (_lastReqDoc == null || _lastReqDoc.DocumentsFileID == null)
                        throw new Exception("اطلاعات از آخرین مدرک یافت نشد.");
                    else
                        _reqDoc.DocumentsFileID = _lastReqDoc.DocumentsFileID;

                    _reqDoc = DB.GetAllEntity<RequestDocument>().Where(r =>// r.ValidToDate == _validtoDate &&
                   r.DocumentRequestTypeID == _documentRequestTypeID && r.CustomerID == _customerID).FirstOrDefault();

                    RequestDocumnetDB.SaveRequestDocument(_reqDoc, _requestID, false);
                }
                // }
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "خطا : " + message + "');", true);
            }
        }

        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyRadioButton.Checked)
            {
                _lastReqDoc = Data.RequestDocumnetDB.GetLastRequestDocument(_documentRequestTypeID, _customerID);
                if (_lastReqDoc != null && _lastReqDoc.DocumentsFileID != null)
                {
                    FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)_lastReqDoc.DocumentsFileID);
                    if (fileInfo.Content != null)
                    {
                        _fileBytes = fileInfo.Content;
                        _extension = fileInfo.FileType;
                        if (_fileBytes != null && _extension != string.Empty)
                            CopyDocSavedValueLabel.Visible = true;
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            if (_id == 0)
            {
                SaveButton.Text = "ذخیره";
            }
            else
            {
                SaveButton.Text = "بروز رسانی";
                Title = "بروز رسانی مدارک مشترک ";
            }
            if (_docType == 2)
            {
                DocumentNotextBox.ReadOnly = false;
                DocumentDateTextBox.ReadOnly = false;
                IssuingOfficetextBox.ReadOnly = false;
                IssuingRoletextBox.ReadOnly = false;

                DocumentNotextBox.Enabled = true;
                DocumentDateTextBox.Enabled = true;
                IssuingOfficetextBox.Enabled = true;
                IssuingRoletextBox.Enabled = true;
            }
            else
            {
                DocumentNotextBox.ReadOnly = true;
                DocumentDateTextBox.ReadOnly = true;
                IssuingOfficetextBox.ReadOnly = true;
                IssuingRoletextBox.ReadOnly = true;

                DocumentNotextBox.Enabled = false;
                DocumentDateTextBox.Enabled = false;
                IssuingOfficetextBox.Enabled = false;
                IssuingRoletextBox.Enabled = false;
            }
        }
        #endregion
    }
}