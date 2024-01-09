using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using CRM.Data;
using System.Transactions;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for RequestDocumentForm.xaml
    /// </summary>
    public partial class RequestDocumentForm : Local.PopupWindow
    {
        public RequestDocument reqDoc { get; set; }
        public Data.RequestForm reqForm { get; set; }
        public RequestDocument LastreqDoc { get; set; }
        private long _customerID { get; set; }
        private long _documentRequestTypeID { get; set; }
        private long _id { get; set; }
        private byte _docType { get; set; }
        public long _requestID { get; set; }
        //  public DateTime? _validtoDate { get; set; }

        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        public RequestDocumentForm()
        {
            InitializeComponent();
            reqDoc = new RequestDocument();
            DataContext = this;
        }

        public RequestDocumentForm(long requestDocumentID, byte docType)
            : this()
        {
            reqDoc = Data.RequestDocumnetDB.GetRequestDocumentByID(requestDocumentID);
            _id = reqDoc.ID;
            _docType = docType;
            DataContext = this;
        }

        public RequestDocumentForm(int FormID, byte docType)
            : this()
        {
            reqForm = Data.DocumentRequestTypeDB.GetFormByID(FormID);
            _id = reqForm.ID;
            _docType = docType;
            DataContext = this;
        }

        public RequestDocumentForm(long? customerID, long documentRequestTypeID, byte? docType, long requestID)
            : this()
        {
            _customerID = customerID ?? -1;
            _requestID = requestID;
            _documentRequestTypeID = documentRequestTypeID;
            _docType = docType ?? 0;
            Initialize();

        }

        private void Initialize()
        {
            Title = "ثبت مدارک مشترک ";
        }

        private void Selectfile_Click(object sender, RoutedEventArgs e)
        {

            DocumentInputForm window = new DocumentInputForm();
            window.ShowDialog();

            FileBytes = window.FileBytes;
            Extension = window.Extension;
            if (FileBytes != null && Extension != string.Empty)
                SavedValueLabel.Visibility = Visibility.Visible;
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateTime = DB.GetServerDate();
                Guid? oldGuid = new Guid();
                using (TransactionScope MainTransactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    oldGuid = reqDoc.DocumentsFileID;

                    if (NewradioButton.IsSelected == true)
                    {
                        if (FileBytes == null || Extension == string.Empty)
                        {
                            throw new Exception("فایل یافت نشد.");
                        }
                        else
                        {
                            reqDoc.DocumentsFileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                        }
                    }
                    else
                    {
                        if (LastreqDoc == null || LastreqDoc.DocumentsFileID == null)
                        {
                            throw new Exception("اطلاعات از آخرین مدرک یافت نشد.");
                        }
                        else
                        {
                            reqDoc.DocumentsFileID = LastreqDoc.DocumentsFileID;
                        }
                    }

                    if (_id == 0)
                    {
                        reqDoc.CustomerID = _customerID;
                        reqDoc.DocumentRequestTypeID = _documentRequestTypeID;
                        reqDoc.InsertDate = dateTime;

                        RequestDocumnetDB.SaveRequestDocument(reqDoc, _requestID, true);
                    }
                    else
                    {
                        reqDoc.Detach();
                        Save(reqDoc);
                        if (oldGuid != null)
                            DocumentsFileDB.DeleteDocumentsFileTable((Guid)oldGuid);

                    }
                    MainTransactionScope.Complete();

                }
                //ShowSuccessMessage("فایل دخیره شد");
                MessageBox.Show(".فایل ذخیره شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره فایل" , ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            if (_id == 0)
            {
                Savebtn.Content = "ذخیره";
            }
            else
            {
                Savebtn.Content = "بروز رسانی";
                Title = "بروز رسانی مدارک مشترک ";
            }
            if (_docType == 2)
                mojavezGrid.Visibility = Visibility.Visible;
            else
                mojavezGrid.Visibility = Visibility.Collapsed;

        }

        private void SavedValueLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

                if (FileBytes != null)
                {
                    CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
                    window.FileBytes = FileBytes;
                    window.FileType = Extension;
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("فایل موجود نمیباشد.");
                }

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CopyradioButton != null && CopyradioButton.IsSelected == true)
            {
                LastreqDoc = Data.RequestDocumnetDB.GetLastRequestDocument(_documentRequestTypeID, _customerID);
                if (LastreqDoc != null && LastreqDoc.DocumentsFileID != null)
                {
                    FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)LastreqDoc.DocumentsFileID);
                    if (fileInfo.Content != null)
                    {
                        FileBytes = fileInfo.Content;
                        Extension = fileInfo.FileType;
                        if (FileBytes != null && Extension != string.Empty)
                            CopyDocSavedValueLabel.Visibility = Visibility.Visible;
                    }
                }
           
            }
        }
    }
}
