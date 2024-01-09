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

    public partial class InstallingDepartmentForm : Local.RequestFormBase
    {

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }

        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        long _requestID = 0;
        Request _request { get; set; }
        CRM.Data.E1 _e1 { get; set; }
        private long? _subID;


        List<E1Files> _E1Files = new List<E1Files>();


        public InstallingDepartmentForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

        }
        public InstallingDepartmentForm(long requstID, long? subID)
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
            LoadE1File();
        }


        private void LoadE1File()
        {
            List<PowerOffice> powerOffice = PowerOfficeDB.GetPowerOfficeByRequestID(_requestID);
            List<CableDesignOffice> cableDesignOffice = CableDesignOfficeDB.GetCableDesignOfficeByRequestID(_requestID);
            List<TransferDepartmentOffice> transferDepartmentOffice = TransferDepartmentDB.GetTransferDepartmentOfficeByRequestID(_requestID);
            List<SwitchOffice> switchOffice = SwitchOfficeDB.GetSwitchOfficeByRequestID(_requestID);


            switchOffice.ForEach(t => { _E1Files.Add(new E1Files { ID = t.ID, FileID = t.SwitchFileID, InsertDate = t.InsertDate, RequestID = t.RequestID, FileType = "سوئیچ" }); });
            cableDesignOffice.ForEach(t => { _E1Files.Add(new E1Files { ID = t.ID, FileID = t.CableDesignFileID, InsertDate = t.InsertDate, RequestID = t.RequestID, FileType = "کابل" }); });
            transferDepartmentOffice.ForEach(t => { _E1Files.Add(new E1Files { ID = t.ID, FileID = t.TransferDepartmentFileID, InsertDate = t.InsertDate, RequestID = t.RequestID, FileType = "انتقال" }); });
            powerOffice.ForEach(t => { _E1Files.Add(new E1Files { ID = t.ID, FileID = t.PowerFileID, InsertDate = t.InsertDate, RequestID = t.RequestID, FileType = "نیرو" }); });


            ItemsDataGrid.ItemsSource = null;
            ItemsDataGrid.ItemsSource = _E1Files;
        }
        //private void DesignDirectorImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    ShowFile(_e1.DesignDirectorFileID);
        //}

        //private void ShowFile(Guid? fileID)
        //{
        //    try
        //    {
        //        if (fileID != null)
        //        {
        //            CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)fileID);
        //            fileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)fileID);
        //            FileBytes = fileInfo.Content;
        //            Extension = "." + fileInfo.FileType;

        //            if (FileBytes != null && (Extension == ".png" || Extension == ".jpg" || Extension == ".bmp"))
        //            {
        //                CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
        //                window.FileBytes = FileBytes;
        //                window.ShowDialog();
        //            }
        //            else if (FileBytes != null && Extension != string.Empty)
        //            {
        //                string path = System.IO.Path.GetTempFileName() + Extension;
        //                try
        //                {
        //                    using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        //                    {
        //                        writer.Write(FileBytes);
        //                    }

        //                    Process p = System.Diagnostics.Process.Start(path);
        //                    p.WaitForExit();
        //                }
        //                finally
        //                {
        //                    File.Delete(path);
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("فایل موجود نمی باشد !");
        //            }
        //        }
        //        else
        //        {
        //            Folder.MessageBox.ShowInfo("برای این قسمت فایل اضافه نشده است");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage(ex.Message, ex);
        //    }
        //}

        //private void TransferDepartmentImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //  //  ShowFile(_e1.TransferDepartmentFileID);
        //}

        //private void CommandCircuitImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //   // ShowFile(_e1.CommandCircuitFileID);
        //}

        //private void PowerImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    ShowFile(_e1.PowerFileID);
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
                _e1.Detach();
                DB.Save(_e1, false);

                Subts.Complete();
                IsRejectSuccess = true;
            }

            return IsRejectSuccess;
        }

        #region File




        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                E1Files E1File = ItemsDataGrid.SelectedItem as E1Files;
                if (E1File != null)
                {

                    _FileID = (Guid)E1File.FileID;
                    CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_FileID);
                    FileBytes = fileInfo.Content;
                    Extension = "." + fileInfo.FileType;
                    if (FileBytes != null && Extension != string.Empty)
                    {
                        string path = System.IO.Path.GetTempFileName() + Extension;
                        try
                        {
                            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                            {
                                writer.Write(FileBytes);
                            }

                            Process p = System.Diagnostics.Process.Start(path);
                            p.WaitForExit();
                        }
                        finally
                        {

                            int result = DocumentsFileDB.UpdateFileInDocumentsFile(_FileID, File.ReadAllBytes(path));
                            if (result <= 0)
                            {
                                Folder.MessageBox.ShowError("فایل بروز رسانی نشد");
                            }
                            else
                            {
                                File.Delete(path);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("فایل موجود نمی باشد !");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

        }
        #endregion file


    }
}


