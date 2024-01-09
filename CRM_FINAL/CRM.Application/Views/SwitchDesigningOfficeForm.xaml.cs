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
    /// فرم اداره طراحی سوئیچ
    /// </summary>
    public partial class SwitchDesigningOfficeForm : Local.RequestFormBase
    {
        #region Properties and Fields

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.V2SpaceAndPowerInfoSummary _v2SpaceAndPowerInfoSummary { get; set; }

        long _requestID = 0;
        CRM.Data.Request _request { get; set; }
        CRM.Data.SpaceAndPower _spaceAndPower { get; set; }

        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        List<SwitchOffice> _switchOffices = new List<SwitchOffice>();

        #endregion

        #region Constructors

        public SwitchDesigningOfficeForm()
        {
            InitializeComponent();
        }

        public SwitchDesigningOfficeForm(long requestID)
            : this
                ()
        {
            base.RequestID = this._requestID = requestID;
        }

        #endregion

        #region EventHandlers

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileBytes != null && Extension != string.Empty)
            {
                DateTime currentDateTime = DB.GetServerDate();
                _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                SwitchOffice switchOffice = new SwitchOffice();
                switchOffice.RequestID = _request.ID;
                switchOffice.SwitchFileID = _FileID;
                switchOffice.InsertDate = currentDateTime;
                switchOffice.Detach();
                DB.Save(switchOffice);

                RefreshItemsDataGrid();

                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            //باید دیتا گرید موجود در اطلاعات فضا و پاور از نو پر شود
                            _v2SpaceAndPowerInfoSummary.RefreshFiles(_request.ID, false);
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

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region File

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SwitchOffice switchOfficeItem = ItemsDataGrid.SelectedItem as SwitchOffice;
                if (switchOfficeItem != null)
                {

                    _FileID = (Guid)switchOfficeItem.SwitchFileID;
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
                    SwitchOffice switchOfficeItem = ItemsDataGrid.SelectedItem as SwitchOffice;
                    if (switchOfficeItem != null)
                    {

                        Guid tempFileID = switchOfficeItem.SwitchFileID;
                        DB.Delete<SwitchOffice>(switchOfficeItem.ID);

                        DocumentsFileDB.DeleteDocumentsFileTable(tempFileID);

                        RefreshItemsDataGrid();

                        switch (_request.RequestTypeID)
                        {
                            case (int)DB.RequestType.SpaceandPower:
                                {
                                    //باید دیتا گرید موجود در اطلاعات فضا و پاور از نو پر شود
                                    _v2SpaceAndPowerInfoSummary.RefreshFiles(_request.ID, false);
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف فایل", ex);
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

        #endregion

        #region Methods

        private void LoadData()
        {
            //Fetch current Request
            _request = RequestDB.GetRequestByID(this._requestID);
            _requestInfoSummary = new UserControls.RequestInfoSummary(this._requestID);
            RequestInfoSummaryUC.Content = _requestInfoSummary;
            RequestInfoSummaryUC.DataContext = _requestInfoSummary;

            //Fetch current Request's Customer
            _customerInfoSummary = new UserControls.CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.IsExpandedProperty = true;
            _customerInfoSummary.Mode = true;
            CustomerInfoSummaryUC.Content = _customerInfoSummary;
            CustomerInfoSummaryUC.DataContext = _customerInfoSummary;

            CRM.Data.Status status = StatusDB.GetStatueByStatusID(_request.StatusID);
            StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;

            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.SpaceandPower:
                    {
                        //بازیابی رکورد فضاپاور
                        _spaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_requestID);

                        //مقداردهی عملیات های مربوطه با توجّه به چرخه کاری
                        this.ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

                        //پر کردن کنترل های مربوط به اطلاعات رکورد فضاپاور
                        _v2SpaceAndPowerInfoSummary = new UserControls.V2SpaceAndPowerInfoSummary(this._requestID);
                        _v2SpaceAndPowerInfoSummary.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;
                        V2SpaceAndPowerInfoSummaryUC.Content = _v2SpaceAndPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.DataContext = _v2SpaceAndPowerInfoSummary;

                        this.DataContext = _spaceAndPower;

                        SwitchDesigningOfficeCommentTextBox.Text = _spaceAndPower.SwitchDesigningOfficeComment;
                        SwitchDesigningOfficeDatePicker.SelectedDate = _spaceAndPower.SwitchDesigningOfficeDate;

                        this.RefreshItemsDataGrid();
                    }
                    break;
            }
        }
        private void RefreshItemsDataGrid()
        {
            _switchOffices = SwitchOfficeDB.GetSwitchOfficeByRequestID(_requestID);
            ItemsDataGrid.ItemsSource = null;
            ItemsDataGrid.ItemsSource = _switchOffices;
        }

        #endregion

        #region Actions

        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return IsSaveSuccess;
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    switch (_request.RequestTypeID)
                    {
                        case (byte)DB.RequestType.SpaceandPower:
                            {
                                _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                                _spaceAndPower.SwitchDesigningOfficeComment = SwitchDesigningOfficeCommentTextBox.Text;
                                _spaceAndPower.SwitchDesigningOfficeDate = (SwitchDesigningOfficeDatePicker.SelectedDate.HasValue) ? SwitchDesigningOfficeDatePicker.SelectedDate : DB.GetServerDate();
                                _spaceAndPower.Detach();
                                DB.Save(_spaceAndPower, false);
                            }
                            break;
                    }

                    _request.StatusID = (int)StatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request, false);

                    scope.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage(".ذخیره اطلاعات انجام شد");
                }
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
                this.Save();
                if (this.IsSaveSuccess)
                {
                    IsForwardSuccess = true;
                }
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
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                _request.StatusID = (int)StatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);
                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                            _spaceAndPower.SwitchDesigningOfficeComment = string.Empty;
                            _spaceAndPower.SwitchDesigningOfficeDate = null;
                            _spaceAndPower.Detach();
                            DB.Save(_spaceAndPower, false);
                        }
                        break;
                }
                scope.Complete();
                IsRejectSuccess = true;
            }
            return IsRejectSuccess;
        }

        #endregion
    }
}
