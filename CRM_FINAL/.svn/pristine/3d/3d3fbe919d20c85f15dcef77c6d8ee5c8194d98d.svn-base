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

    public partial class PowerDepartmentForm : Local.RequestFormBase
    {
        #region Properties And Fields

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }
        private UserControls.V2SpaceAndPowerInfoSummary _V2SpaceAndPowerInfoSummary { get; set; }

        List<PowerOffice> _powerOffice = new List<PowerOffice>();

        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        long _requestID = 0;
        Request _request { get; set; }
        CRM.Data.E1 _e1 { get; set; }
        CRM.Data.SpaceAndPower _spaceAndPower { get; set; }

        private long? _subID;

        #endregion

        #region Constructor

        public PowerDepartmentForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PowerDepartmentForm(long requstID, long? subID)
            : this()
        {
            _subID = subID;
            base.RequestID = this._requestID = requstID;
        }

        #endregion

        #region Methods

        private void RefreshItemsDataGrid()
        {
            _powerOffice = PowerOfficeDB.GetPowerOfficeByRequestID(_requestID);
            ItemsDataGrid.ItemsSource = null;
            ItemsDataGrid.ItemsSource = _powerOffice;
        }

        private void Initialize()
        {

        }

        public void LoadData()
        {
            _request = Data.RequestDB.GetRequestByID(_requestID);
            _powerOffice = PowerOfficeDB.GetPowerOfficeByRequestID(_request.ID);
            ItemsDataGrid.ItemsSource = _powerOffice;

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

            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.E1:
                    {
                        V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Collapsed;
                        E1InfoSummaryUC.Visibility = Visibility.Visible;

                        _e1 = Data.E1DB.GetE1ByRequestID(_requestID);
                        _E1InfoSummary = new E1InfoSummary(_requestID, _subID);
                        _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
                        E1InfoSummaryUC.Content = _E1InfoSummary;
                        E1InfoSummaryUC.DataContext = _E1InfoSummary;
                        PowerDescriptionTextBox.Text = _e1.PowerDescription;

                        if (_subID != null)
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };
                        else
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

                        this.DataContext = _e1;
                    }
                    break;
                case (int)DB.RequestType.SpaceandPower:
                    {
                        //کنترل های مربوط به اطلاعات غیر از فضا و پاور نباید نمایش داده شوند
                        E1InfoSummaryUC.Visibility = Visibility.Collapsed;

                        //بازیابی رکورد درخواست فضا و پاور
                        _spaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_request.ID);

                        //مقداردهی کنترل های مربوط به اطلاعات رکورد فضا و پاور
                        V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Visible;
                        PowerDateLabel.Visibility = Visibility.Visible;
                        PowerDatePicker.Visibility = Visibility.Visible;
                        _V2SpaceAndPowerInfoSummary = new V2SpaceAndPowerInfoSummary(_request.ID);
                        _V2SpaceAndPowerInfoSummary.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;
                        V2SpaceAndPowerInfoSummaryUC.Content = _V2SpaceAndPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.DataContext = _V2SpaceAndPowerInfoSummary;
                        PowerDescriptionTextBox.Text = _spaceAndPower.NirooComment;
                        PowerDatePicker.SelectedDate = _spaceAndPower.NirooDate;

                        //مقداردهی عملیات های مربوطه - با توجه به وضعیت های چرخه کاری
                        ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

                        this.DataContext = _spaceAndPower;
                    }
                    break;
            }
        }


        #endregion

        #region Actions

        public override bool Deny()
        {
            using (TransactionScope Subts = new TransactionScope(TransactionScopeOption.Required))
            {
                _request.StatusID = (int)StatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);
                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.E1:
                        {
                            _e1 = this.DataContext as CRM.Data.E1;
                            _e1.PowerDescription = null;
                            _e1.Detach();
                            DB.Save(_e1, false);
                        }
                        break;
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                            _spaceAndPower.NirooComment = string.Empty;
                            _spaceAndPower.NirooDate = null;
                            _spaceAndPower.Detach();
                            DB.Save(_spaceAndPower, false);

                            //بعد از رد درخواست در این مرحله چنانچه فایلی برای آن ذخیره شده باشد باید حذف گردد
                            //if (_powerOffice != null && _powerOffice.Count != 0)
                            //{
                            //    List<long> powerOfficeIds = new List<long>();
                            //    foreach (var item in _powerOffice)
                            //    {
                            //        powerOfficeIds.Add(item.ID);
                            //    }
                            //    if (powerOfficeIds.Count != 0)
                            //    {
                            //        DB.DeleteAll<PowerOffice>(powerOfficeIds);
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
                    _request.StatusID = (int)StatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);

                    switch (_request.RequestTypeID)
                    {
                        case (int)DB.RequestType.E1:
                            {
                                _e1 = this.DataContext as CRM.Data.E1;
                                _e1.PowerDescription = PowerDescriptionTextBox.Text;
                                _e1.Detach();
                                DB.Save(_e1, false);
                            }
                            break;
                        case (int)DB.RequestType.SpaceandPower:
                            {
                                _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                                _spaceAndPower.NirooComment = PowerDescriptionTextBox.Text;
                                _spaceAndPower.NirooDate = (PowerDatePicker.SelectedDate.HasValue) ? PowerDatePicker.SelectedDate : DB.GetServerDate();
                                _spaceAndPower.Detach();
                                DB.Save(_spaceAndPower, false);
                            }
                            break;
                    }
                    ts.Complete();
                }
                IsSaveSuccess = true;
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

        #endregion

        #region EventHandlers

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
                PowerOffice powerOfficeItem = ItemsDataGrid.SelectedItem as PowerOffice;
                if (powerOfficeItem != null)
                {
                    _FileID = (Guid)powerOfficeItem.PowerFileID;
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
                    PowerOffice powerOfficeItem = ItemsDataGrid.SelectedItem as PowerOffice;
                    if (powerOfficeItem != null)
                    {

                        Guid tempFileID = powerOfficeItem.PowerFileID;
                        DB.Delete<PowerOffice>(powerOfficeItem.ID);

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
                PowerOffice powerOffice = new PowerOffice();
                powerOffice.RequestID = _request.ID;
                powerOffice.PowerFileID = _FileID;
                powerOffice.InsertDate = currentDateTime;
                powerOffice.Detach();
                DB.Save(powerOffice);

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

        #endregion 

        //private void PowerFileLisBoxItem_Selected(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        //    dlg.Filter = "All files (*.*)|*.*";

        //    if (dlg.ShowDialog() == true)
        //    {
        //        PowerFileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
        //        PowerExtension = System.IO.Path.GetExtension(dlg.FileName);
        //    }

        //    if (PowerFileBytes != null && PowerExtension != string.Empty)
        //    {
        //        _PowerFileID = DocumentsFileDB.SaveFileInDocumentsFile(PowerFileBytes, PowerExtension);
        //    }
        //}

        //private void PowerScannerLisBox_Selected(object sender, RoutedEventArgs e)
        //{
        //    Scanner oScanner = new Scanner();
        //    string extension;

        //    PowerFileBytes = oScanner.ScannWithExtension(out extension);
        //    PowerExtension = extension;
        //}

        //private void PowerImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        if (_e1.PowerFileID != null)
        //        {
        //            _PowerFileID = (Guid)_e1.PowerFileID;
        //            CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_PowerFileID);
        //            fileInfo = DocumentsFileDB.GetDocumentsFileTable(_PowerFileID);
        //            PowerFileBytes = fileInfo.Content;
        //            PowerExtension = "." + fileInfo.FileType;
        //        }
        //        if (PowerFileBytes != null && PowerExtension != string.Empty)
        //        {
        //            string path = System.IO.Path.GetTempFileName() + PowerExtension;
        //            try
        //            {
        //                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        //                {
        //                    writer.Write(PowerFileBytes);
        //                }

        //                Process p = System.Diagnostics.Process.Start(path);
        //                p.WaitForExit();
        //            }
        //            finally
        //            {
        //              int result =  DocumentsFileDB.UpdateFileInDocumentsFile(_PowerFileID, File.ReadAllBytes(path));
        //              if (result <= 0)
        //              {
        //                  Folder.MessageBox.ShowError("فایل بروز رسانی نشد");
        //              }
        //              else
        //              {
        //                  File.Delete(path);
        //              }
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

