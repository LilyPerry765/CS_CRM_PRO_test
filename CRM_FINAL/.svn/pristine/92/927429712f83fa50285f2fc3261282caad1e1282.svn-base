using CRM.Application.UserControls;
using CRM.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for DesignDirectorForm.xaml
    /// </summary>
    /// 
    
    public partial class MonitoringDepartmentForm : Local.RequestFormBase
    {

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }

        //private Guid _FileID { get; set; }
        //public byte[] FileBytes { get; set; }
        //public string Extension { get; set; }

        long _requestID = 0;
        Request _request { get; set; }
        CRM.Data.E1 _e1 { get; set; }
        private long? _subID;


        public MonitoringDepartmentForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

        }
        public MonitoringDepartmentForm(long requstID, long? subID)
            : this()
        {
            _subID = subID;
            base.RequestID = this._requestID = requstID;

        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {



            _e1 = Data.E1DB.GetE1ByRequestID(_requestID);
            _request = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;

            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.IsExpandedProperty = true;
            _customerInfoSummary.Mode = true;
            CustomerInfoUC.Content = _customerInfoSummary;
            CustomerInfoUC.DataContext = _customerInfoSummary;


            _requestInfoSummary = new RequestInfoSummary(_request.ID);
            _requestInfoSummary.RequestInfoExpander.IsExpanded = true;
            RequestInfoUC.Content = _requestInfoSummary;
            RequestInfoUC.DataContext = _requestInfoSummary;

            _E1InfoSummary = new E1InfoSummary(_requestID, _subID);
            _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
            E1InfoSummaryUC.Content = _E1InfoSummary;
            E1InfoSummaryUC.DataContext = _E1InfoSummary;


            if (_subID != null)
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };
            else
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

            this.DataContext = _e1;
        }

        //private void FileLisBoxItem_Selected(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        //    dlg.Filter = "All files (*.*)|*.*";

        //    if (dlg.ShowDialog() == true)
        //    {
        //        FileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
        //        Extension = System.IO.Path.GetExtension(dlg.FileName);
        //    }

        //    if (FileBytes != null && Extension != string.Empty)
        //    {
        //        _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
        //    }
        //}

        //private void ScannerLisBox_Selected(object sender, RoutedEventArgs e)
        //{
        //    Scanner oScanner = new Scanner();
        //    string extension;

        //    FileBytes = oScanner.ScannWithExtension(out extension);
        //    Extension = extension;
        //}

        //private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {


        //        if (_e1.DesignDirectorFileID != null)
        //        {
        //            _FileID = (Guid)_e1.DesignDirectorFileID;
        //            CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_FileID);
        //            FileBytes = fileInfo.Content;
        //            Extension = "." + fileInfo.FileType;
        //        }

        //        if (FileBytes != null && (Extension == ".png" || Extension == ".jpg"  || Extension == ".bmp" ))
        //        {
        //            FileBytes = DocumentsFileDB.GetDocumentsFileTable(_FileID).Content;
        //            CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
        //            window.FileBytes = FileBytes;
        //            window.ShowDialog();
        //        }
        //        else if (FileBytes != null && Extension != string.Empty)
        //        {
        //            string path = System.IO.Path.GetTempFileName() + Extension;
        //            try
        //            {
        //                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        //                {
        //                    writer.Write(FileBytes);
        //                }

        //                Process p = System.Diagnostics.Process.Start(path);
        //                p.WaitForExit();
        //            }
        //            finally
        //            {
        //                File.Delete(path);
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("فایل موجود نمی باشد !");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage(ex.Message, ex);
        //    }
        //}


        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                        _e1 = this.DataContext as CRM.Data.E1;
                        _e1.Detach();
                        DB.Save(_e1, false);

                        _request.StatusID = (int)StatusComboBox.SelectedValue;
                        _request.Detach();
                        DB.Save(_request);

                        ts.Complete();
                        IsSaveSuccess = true;

                }
                ShowSuccessMessage("دخیره اطلاعات انجام شد.");
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
            return IsSaveSuccess;
        }
        public override bool Forward()
        {
            try
            {
                Save();
                if (IsSaveSuccess == true)
                    IsForwardSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ارجاع" , ex);
                IsForwardSuccess = false;
            }

            return IsForwardSuccess;
        }

        public override bool Deny()
        {
            using (TransactionScope Subts = new TransactionScope(TransactionScopeOption.Required))
            {
                _request.StatusID = (int)StatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                _e1 = this.DataContext as CRM.Data.E1;
                _e1.ExploitationDate = null;
                _e1.Detach();
                DB.Save(_e1, false);


               // DocumentsFileDB.DeleteDocumentsFileTable(_FileID);

                Subts.Complete();
                IsRejectSuccess = true;
            }

            return IsRejectSuccess;
        }


    }
}

