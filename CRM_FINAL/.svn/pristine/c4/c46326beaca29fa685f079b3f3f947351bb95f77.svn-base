using CRM.Application.UserControls;
using CRM.Data;
using Enterprise;
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
    public partial class TransferDepartmentForm : Local.RequestFormBase
    {

        #region Properties and Fields
        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }
        private UserControls.V2SpaceAndPowerInfoSummary _V2SpaceAndPowerInfoSummary { get; set; }

        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }


        //private Guid _CommandCircuitFileID { get; set; }
        //public byte[] CommandCircuitFileBytes { get; set; }
        //public string CommandCircuitExtension { get; set; }


        long _requestID = 0;
        Request _request { get; set; }
        CRM.Data.E1 _e1 { get; set; }
        CRM.Data.SpaceAndPower _spaceAndPower { get; set; }

        List<TransferDepartmentOffice> _transferDepartmentOffice = new List<TransferDepartmentOffice>();
        private long? _subID;

        #endregion

        #region Constructor

        public TransferDepartmentForm()
        {
            InitializeComponent();
            Initialize();
        }

        public TransferDepartmentForm(long requstID, long? subID)
            : this()
        {
            _subID = subID;
            base.RequestID = this._requestID = requstID;

        }

        #endregion

        #region Methods

        private void Initialize()
        {

        }

        public void LoadData()
        {
            try
            {
                _request = Data.RequestDB.GetRequestByID(_requestID);
                _transferDepartmentOffice = TransferDepartmentDB.GetTransferDepartmentOfficeByRequestID(_requestID);

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

                ItemsDataGrid.ItemsSource = _transferDepartmentOffice;

                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.E1:
                        {
                            //کنترل های مربوط به اطلاعات غیر از ای وان نباید نمایش داده شوند
                            V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Collapsed;

                            //بازیابی رکورد درخواست ای وان
                            _e1 = Data.E1DB.GetE1ByRequestID(_requestID);

                            //مقداردهی کنترل های مربوط به اطلاعات رکورد ای وان
                            _E1InfoSummary = new E1InfoSummary(_requestID, _subID);
                            _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
                            E1InfoSummaryUC.Content = _E1InfoSummary;
                            E1InfoSummaryUC.DataContext = _E1InfoSummary;
                            E1InfoSummaryUC.Visibility = Visibility.Visible;
                            TransferDepartmentOfficeDescriptionTextBox.Text = _e1.TransferDepartmentDescription;
                            this.DataContext = _e1;

                            //مقداردهی عملیات های مربوطه - با توجه به وضعیت های چرخه کاری
                            if (_subID != null)
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };
                            else
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                        }
                        break;
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            //کنترل های مربوط به اطلاعات غیر از فضا و پاور نباید نمایش داده شوند
                            E1InfoSummaryUC.Visibility = Visibility.Collapsed;

                            //بازیابی رکورد درخواست فضا و پاور
                            _spaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_request.ID);

                            //مقداردهی کنترل های مربوط به اطلاعات رکورد فضا و پاور
                            _V2SpaceAndPowerInfoSummary = new V2SpaceAndPowerInfoSummary(_requestID);
                            _V2SpaceAndPowerInfoSummary.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;
                            V2SpaceAndPowerInfoSummaryUC.Content = _V2SpaceAndPowerInfoSummary;
                            V2SpaceAndPowerInfoSummaryUC.DataContext = _V2SpaceAndPowerInfoSummary;
                            V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Visible;
                            TransferDepartmentDateLabel.Visibility = Visibility.Visible;
                            TransferDepartmentDatePicker.Visibility = Visibility.Visible;

                            TransferDepartmentOfficeDescriptionTextBox.Text = _spaceAndPower.EnteghalComment;

                            TransferDepartmentDatePicker.SelectedDate = _spaceAndPower.EnteghalDate;

                            this.DataContext = _spaceAndPower;

                            //مقداردهی عملیات های مربوطه - با توجه به وضعیت های چرخه کاری
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "TransferDepartmentForm.LoadData()");
            }
        }

        private void RefreshItemsDataGrid()
        {
            _transferDepartmentOffice = TransferDepartmentDB.GetTransferDepartmentOfficeByRequestID(_requestID);
            ItemsDataGrid.ItemsSource = null;
            ItemsDataGrid.ItemsSource = _transferDepartmentOffice;
        }

        #endregion

        #region EventHandlers
        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region Actions

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
                    switch (_request.RequestTypeID)
                    {
                        case (int)DB.RequestType.E1:
                            {
                                _e1 = this.DataContext as CRM.Data.E1;
                                _e1.TransferDepartmentDescription = TransferDepartmentOfficeDescriptionTextBox.Text.Trim();
                                _e1.Detach();
                                DB.Save(_e1, false);
                            }
                            break;
                        case (int)DB.RequestType.SpaceandPower:
                            {
                                _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                                _spaceAndPower.EnteghalComment = TransferDepartmentOfficeDescriptionTextBox.Text.Trim();
                                _spaceAndPower.EnteghalDate = (TransferDepartmentDatePicker.SelectedDate.HasValue) ? TransferDepartmentDatePicker.SelectedDate : DB.GetServerDate();
                                _spaceAndPower.Detach();
                                DB.Save(_spaceAndPower, false);
                            }
                            break;
                    }

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
                ShowErrorMessage("خطا در ارجاع", ex);
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

                //_e1 = this.DataContext as CRM.Data.E1;
                //_e1.TransferDepartmentFileID = null;
                //_e1.TransferDepartmentDescription = null;
                //_e1.CommandCircuitFileID = null;
                //_e1.Detach();
                //DB.Save(_e1, false);


                //DocumentsFileDB.DeleteDocumentsFileTable(_CommandCircuitFileID);
                //DocumentsFileDB.DeleteDocumentsFileTable(_FileID);
                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.E1:
                        {
                            _e1 = this.DataContext as CRM.Data.E1;
                            _e1.TransferDepartmentDescription = string.Empty;
                            _e1.Detach();
                            DB.Save(_e1, false);
                        }
                        break;
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                            _spaceAndPower.EnteghalComment = string.Empty;
                            _spaceAndPower.EnteghalDate = null;
                            _spaceAndPower.Detach();
                            DB.Save(_spaceAndPower, false);

                            //بعد از رد درخواست در این مرحله چنانچه فایلی برای آن ذخیره شده باشد باید حذف گردد
                            //if (_transferDepartmentOffice != null && _transferDepartmentOffice.Count != 0)
                            //{
                            //    List<long> transferDepartmentOfficeIds = new List<long>();
                            //    foreach (var item in _transferDepartmentOffice)
                            //    {
                            //        transferDepartmentOfficeIds.Add(item.ID);
                            //    }
                            //    if (transferDepartmentOfficeIds.Count != 0)
                            //    {
                            //        DB.DeleteAll<TransferDepartmentOffice>(transferDepartmentOfficeIds);
                            //    }
                            //}
                        }
                        break;
                }
                Subts.Complete();
                IsRejectSuccess = true;
            }

            return IsRejectSuccess;
        }

        #endregion

        #region File

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TransferDepartmentOffice transferDepartmentOffice = ItemsDataGrid.SelectedItem as TransferDepartmentOffice;
                if (transferDepartmentOffice != null)
                {

                    _FileID = (Guid)transferDepartmentOffice.TransferDepartmentFileID;
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

        private void DeleteImage_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    TransferDepartmentOffice transferDepartmentOffice = ItemsDataGrid.SelectedItem as TransferDepartmentOffice;
                    if (transferDepartmentOffice != null)
                    {

                        Guid tempFileID = transferDepartmentOffice.TransferDepartmentFileID;
                        DB.Delete<TransferDepartmentOffice>(transferDepartmentOffice.ID);

                        DocumentsFileDB.DeleteDocumentsFileTable(tempFileID);

                        RefreshItemsDataGrid();

                        switch (_request.RequestTypeID)
                        {
                            case (int)DB.RequestType.E1:
                                {
                                    //باید دیتا گرید موجود در اطلاعات ایوان از نو پر شود
                                    _E1InfoSummary.RefreshFiles(_request.ID, false);
                                }
                                break;
                            case (int)DB.RequestType.SpaceandPower:
                                {
                                    //باید دیتا گرید موجود در اطلاعات فضا و پاور از نو پر شود
                                    _V2SpaceAndPowerInfoSummary.RefreshFiles(_request.ID, false);
                                }
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف", ex);
            }


        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileBytes != null && Extension != string.Empty)
            {
                DateTime currentDateTime = DB.GetServerDate();
                _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                TransferDepartmentOffice transferDepartmentOffice = new TransferDepartmentOffice();
                transferDepartmentOffice.RequestID = _request.ID;
                transferDepartmentOffice.TransferDepartmentFileID = _FileID;
                transferDepartmentOffice.InsertDate = currentDateTime;
                transferDepartmentOffice.Detach();
                DB.Save(transferDepartmentOffice);

                RefreshItemsDataGrid();

                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.E1:
                        {
                            //باید دیتا گرید موجود در اطلاعات ایوان از نو پر شود
                            _E1InfoSummary.RefreshFiles(_request.ID, false);
                        }
                        break;
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            //باید دیتا گرید موجود در اطلاعات فضا و پاور از نو پر شود
                            _V2SpaceAndPowerInfoSummary.RefreshFiles(_request.ID, false);
                        }
                        break;
                }

                //بعد از عملیات ذخیره فایل تعیین شده ، باید محتوای دو فیلد زیر خالی شود
                this.Extension = string.Empty;
                this.FileBytes = null;

                FromFileRadioButton.IsChecked = false;
                FromScanerRadioButton.IsChecked = false;
            }
            else
            {
                Folder.MessageBox.ShowInfo("فایل انتخاب نشده است");
            }
        }

        private void ChangeFileSelected(object sender, RoutedEventArgs e)
        {
            if (FromFileRadioButton.IsChecked == true)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "All files (*.*)|*.*";

                if (dlg.ShowDialog() == true)
                {
                    FileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
                    Extension = System.IO.Path.GetExtension(dlg.FileName);
                }
                else
                {
                    FromFileRadioButton.IsChecked = false;
                }
            }
            else if (FromScanerRadioButton.IsChecked == true)
            {
                Scanner oScanner = new Scanner();
                string extension;

                FileBytes = oScanner.ScannWithExtension(out extension);
                Extension = extension;

                if (string.IsNullOrEmpty(Extension))
                {
                    FromScanerRadioButton.IsChecked = false;
                }
            }
        }

        #endregion File

        //private void TransferDepartmentFileLisBoxItem_Selected(object sender, RoutedEventArgs e)
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

        //private void TransferDepartmentScannerLisBox_Selected(object sender, RoutedEventArgs e)
        //{
        //    Scanner oScanner = new Scanner();
        //    string extension;

        //    FileBytes = oScanner.ScannWithExtension(out extension);
        //    Extension = extension;

        //    if (FileBytes != null && Extension != string.Empty)
        //    {
        //        _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
        //    }
        //}

        //private void TransferDepartmentImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        if (_e1.TransferDepartmentFileID != null)
        //        {
        //            _FileID = (Guid)_e1.TransferDepartmentFileID;
        //            CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_FileID);
        //            FileBytes = fileInfo.Content;
        //            Extension = "." + fileInfo.FileType;
        //        }
        //         if (FileBytes != null && Extension != string.Empty)
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

        //                int result = DocumentsFileDB.UpdateFileInDocumentsFile(_FileID, File.ReadAllBytes(path));
        //                if (result <= 0)
        //                {
        //                    Folder.MessageBox.ShowError("فایل بروز رسانی نشد");
        //                }
        //                else
        //                {
        //                    File.Delete(path);
        //                }
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

        //private void CommandCircuitFileLisBoxItem_Selected(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        //    dlg.Filter = "All files (*.*)|*.*";

        //    if (dlg.ShowDialog() == true)
        //    {
        //        CommandCircuitFileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
        //        CommandCircuitExtension = System.IO.Path.GetExtension(dlg.FileName);
        //    }

        //    if (CommandCircuitFileBytes != null && CommandCircuitExtension != string.Empty)
        //    {
        //        _CommandCircuitFileID = DocumentsFileDB.SaveFileInDocumentsFile(CommandCircuitFileBytes, CommandCircuitExtension);
        //    }
        //}

        //private void CommandCircuitScannerLisBox_Selected(object sender, RoutedEventArgs e)
        //{
        //    Scanner oScanner = new Scanner();
        //    string extension;

        //    CommandCircuitFileBytes = oScanner.ScannWithExtension(out extension);
        //    CommandCircuitExtension = extension;
        //}

        //private void CommandCircuitImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {

        //        if (_e1.CommandCircuitFileID != null)
        //        {
        //            _CommandCircuitFileID = (Guid)_e1.CommandCircuitFileID;
        //            CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_CommandCircuitFileID);
        //            CommandCircuitFileBytes = fileInfo.Content;
        //            CommandCircuitExtension = "." + fileInfo.FileType;
        //        }
        //        if (CommandCircuitFileBytes != null && CommandCircuitExtension != string.Empty)
        //        {
        //            string path = System.IO.Path.GetTempFileName() + CommandCircuitExtension;
        //            try
        //            {
        //                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        //                {
        //                    writer.Write(CommandCircuitFileBytes);
        //                }

        //                Process p = System.Diagnostics.Process.Start(path);
        //                p.WaitForExit();
        //            }
        //            finally
        //            {

        //                int result = DocumentsFileDB.UpdateFileInDocumentsFile(_CommandCircuitFileID, File.ReadAllBytes(path));
        //                if (result <= 0)
        //                {
        //                    Folder.MessageBox.ShowError("فایل بروز رسانی نشد");
        //                }
        //                else
        //                {
        //                    File.Delete(path);
        //                }
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

    }
}
