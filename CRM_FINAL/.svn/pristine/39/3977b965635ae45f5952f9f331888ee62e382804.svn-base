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
    /// Interaction logic for E1ChooseNumber.xaml
    /// </summary>
    public partial class E1ChooseNumber : Local.RequestFormBase
    {

        #region Properties and Fields

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }

        private UserControls.V2SpaceAndPowerInfoSummary _V2SpaceAndPowerInfoSummary { get; set; }

        private UserControls.DDFUserControl _DDFUserControl { get; set; }

        private UserControls.DDFUserControl _DDFInterfaceUserControl { get; set; }

        long _requestID = 0;
        long _oldTelephone = 0;
        Request _request { get; set; }
        CRM.Data.E1 _e1 { get; set; }

        SpaceAndPower _spaceAndPower { get; set; }

        private long? _subID;

        E1Link _e1Link { get; set; }

        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        /// <summary>
        /// .مقدار این پراپرتی تعیین میکند که آیا کاربر فایلی را انتخاب کرده است یا خیر و پیرو آن دکمه ذخیره فایل فعال یا غیر فعال خواهد شد
        /// </summary>
        public bool FileIsSelected { get; set; }

        List<SwitchOffice> _switchOffices = new List<SwitchOffice>();

        #endregion

        #region Constructor

        public E1ChooseNumber()
        {
            InitializeComponent();
            Initialize();
        }
        public E1ChooseNumber(long requstID, long? subID)
            : this()
        {
            this._subID = subID;
            base.RequestID = this._requestID = requstID;
        }

        #endregion

        #region Methods

        private void RefreshItemsDataGrid()
        {
            _switchOffices = SwitchOfficeDB.GetSwitchOfficeByRequestID(_requestID);
            ItemsDataGrid.ItemsSource = null;
            ItemsDataGrid.ItemsSource = _switchOffices;
        }

        private void Initialize()
        {

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }

        public void LoadData()
        {
            _request = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;

            SwitchComboBox.ItemsSource = Data.SwitchDB.GetSwitchCheckableByCenterID(_request.CenterID);

            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.E1:
                case (byte)DB.RequestType.E1Fiber:
                    {
                        _e1 = Data.E1DB.GetE1ByRequestID(_requestID);
                        V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Collapsed;

                        //TODO:rad 13950427 
                        //در فکس جدید آقای کاووسی خواسته شده بود که تصاویر درخواست متقاضی در مرحله اداره طراحی سوئیچ دیده شود
                        //هم روال ایوان و هم فضاپاور
                        //البته برای فضاپاور یک فرم جداگانه تعریف کردم
                        //Fetch current Request
                        _request = RequestDB.GetRequestByID(this._requestID);
                        _requestInfoSummary = new UserControls.RequestInfoSummary(this._requestID);
                        RequestInfoSummaryUC.Content = _requestInfoSummary;
                        RequestInfoSummaryUC.DataContext = _requestInfoSummary;

                        _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
                        _customerInfoSummary.IsExpandedProperty = true;
                        _customerInfoSummary.Mode = true;
                        CustomerInfoUC.Content = _customerInfoSummary;
                        CustomerInfoUC.DataContext = _customerInfoSummary;

                        _E1InfoSummary = new E1InfoSummary(_requestID, null);
                        _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
                        E1InfoSummaryUC.Content = _E1InfoSummary;
                        E1InfoSummaryUC.DataContext = _E1InfoSummary;

                        _oldTelephone = _e1.TelephoneNo ?? 0;
                        if (_oldTelephone != 0)
                        {
                            E1VirtualNumberTextBox.Text = Data.TelephoneDB.GetTelephoneByTelephoneNo(_oldTelephone).VirtualTelephoneNo.ToString();
                        }

                        if (_e1 != null)
                        {
                            SwitchComboBox.SelectedValue = _e1.SwitchID;
                            SwitchComboBox_SelectionChanged(null, null);
                            SwitchPrecodeIDComboBox.SelectedValue = _e1.SwitchPrecodeID;
                            SwitchPrecodeIDComboBox_SelectionChanged(null, null);
                            E1NumberComboBox.SelectedValue = _e1.TelephoneNo;

                            this.DataContext = _e1;
                        }
                        else
                        {
                            Folder.MessageBox.ShowInfo("اطلاعات درخواست E1 یافت نشد.");
                        }

                        RefreshItemsDataGrid();
                    }
                    break;
                case (byte)DB.RequestType.VacateE1:
                    {
                        if (_subID != null)
                        {
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };

                            CustomerInfoUC.Visibility = Visibility.Collapsed;
                            E1InfoSummaryUC.Visibility = Visibility.Collapsed;
                            V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Collapsed;
                            ChooseNumberDetail.Visibility = Visibility.Collapsed;

                            _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);

                            _DDFUserControl = new DDFUserControl((int)_e1Link.SwitchE1NumberID);
                            _DDFUserControl.CenterID = _request.CenterID;
                            DDFGroupBox.DataContext = _DDFUserControl;
                            DDFGroupBox.Content = _DDFUserControl;
                            DDFGroupBox.Visibility = Visibility.Visible;
                            DDFGroupBox.IsEnabled = false;

                            _DDFInterfaceUserControl = new DDFUserControl((int)_e1Link.SwitchInterfaceE1NumberID);
                            _DDFInterfaceUserControl.CenterID = _request.CenterID;
                            InterfaceDDFGroupBox.DataContext = _DDFInterfaceUserControl;
                            InterfaceDDFGroupBox.Content = _DDFInterfaceUserControl;
                            InterfaceDDFGroupBox.Visibility = Visibility.Visible;
                            InterfaceDDFGroupBox.IsEnabled = false;

                        }
                        else
                        {
                            CustomerInfoUC.Visibility = Visibility.Collapsed;
                            E1InfoSummaryUC.Visibility = Visibility.Collapsed;

                            _e1 = Data.E1DB.GetLastInstallE1ByTelephone((long)_request.TelephoneNo);

                            Telephone telepone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_request.TelephoneNo);
                            E1VirtualNumberTextBox.Text = telepone.VirtualTelephoneNo.ToString();
                            TelInfo telInfo = Data.SwitchDB.GetSwitchInfoByTelNo(telepone.TelephoneNo);
                            SwitchComboBox.SelectedValue = telInfo.switchID;
                            SwitchComboBox_SelectionChanged(null, null);
                            SwitchPrecodeIDComboBox.SelectedValue = telepone.SwitchPrecodeID;
                            SwitchPrecodeIDComboBox_SelectionChanged(null, null);
                            E1NumberComboBox.SelectedValue = telepone.TelephoneNo;

                            SwitchComboBox.IsEnabled = false;
                            SwitchPrecodeIDComboBox.IsEnabled = false;
                            E1NumberComboBox.IsEnabled = false;
                            E1VirtualNumberTextBox.IsEnabled = false;
                        }
                        this.DataContext = _e1;
                    }
                    break;
                case (byte)DB.RequestType.SpaceandPower:
                    {
                        //کنترل های مربوط به اطلاعات غیر از فضا و پاور نباید نمایش داده شوند
                        E1InfoSummaryUC.Visibility = Visibility.Collapsed;
                        ChooseNumberDetail.Visibility = Visibility.Collapsed;

                        //کنترل زیر در روال فضاپاور مقداردهی نمیشود . چون برای مرحله اداره طراحی سوئیچ روال فضاپاور یک فرم جداگانه تعریف کردم
                        //RequestInfoSummaryUC.Content = _requestInfoSummary;
                        //SwitchDesigningOfficeForm new form for current step

                        _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
                        _customerInfoSummary.IsExpandedProperty = true;
                        _customerInfoSummary.Mode = true;
                        CustomerInfoUC.Content = _customerInfoSummary;
                        CustomerInfoUC.DataContext = _customerInfoSummary;

                        //بازیابی رکورد درخواست فضا و پاور
                        _spaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_request.ID);

                        //مقداردهی کنترل های مربوط به اطلاعات رکورد فضا و پاور
                        _V2SpaceAndPowerInfoSummary = new V2SpaceAndPowerInfoSummary(_requestID);
                        _V2SpaceAndPowerInfoSummary.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;
                        V2SpaceAndPowerInfoSummaryUC.Content = _V2SpaceAndPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.DataContext = _V2SpaceAndPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Visible;
                        SwitchOfficeDescriptionLabel.Visibility = Visibility.Visible;
                        SwitchOfficeDescriptionTextBox.Visibility = Visibility.Visible;
                        SwitchOfficeDateLabel.Visibility = Visibility.Visible;
                        SwitchOfficeDatePicker.Visibility = Visibility.Visible;
                        SwitchOfficeDescriptionTextBox.Text = _spaceAndPower.SwitchDesigningOfficeComment;
                        SwitchOfficeDatePicker.SelectedDate = _spaceAndPower.SwitchDesigningOfficeDate;
                        this.DataContext = _spaceAndPower;

                        //مقداردهی عملیات های مربوطه - با توجه به وضعیت های چرخه کاری
                        ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

                        RefreshItemsDataGrid();
                    }
                    break;
            }


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
                        case (byte)DB.RequestType.E1:
                        case (byte)DB.RequestType.E1Fiber:
                            {

                                Telephone telephone = new Telephone();
                                if (_oldTelephone == 0)
                                {

                                    telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)E1NumberComboBox.SelectedValue);
                                    if (telephone.Status == (byte)DB.TelephoneStatus.Reserv)
                                        throw new Exception("تلفن رزرو می باشد");
                                    telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                                    if (E1VirtualNumberTextBox.Text.Trim() != string.Empty)
                                    {
                                        long virtualTelephoneNo = 0;
                                        long.TryParse(E1VirtualNumberTextBox.Text.Trim(), out virtualTelephoneNo);
                                        if (virtualTelephoneNo != 0)
                                        {
                                            telephone.VirtualTelephoneNo = virtualTelephoneNo;
                                        }
                                        else
                                        {
                                            Folder.MessageBox.ShowInfo("شماره مجازی وارد شده صحیح نمیباشد");
                                            return false;
                                        }
                                    }
                                    telephone.CustomerID = _request.CustomerID;
                                    telephone.InstallAddressID = _e1.InstallAddressID;
                                    telephone.CorrespondenceAddressID = _e1.CorrespondenceAddressID;
                                    telephone.Detach();
                                    DB.Save(telephone);
                                }
                                else if (_e1.TelephoneNo != _oldTelephone)
                                {
                                    Telephone Oldtelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_oldTelephone);

                                    Oldtelephone.Status = DB.GetTelephoneLastStatus(_e1.TelephoneNo ?? 0);
                                    Oldtelephone.VirtualTelephoneNo = null;
                                    Oldtelephone.CustomerID = null;
                                    Oldtelephone.InstallAddressID = null;
                                    Oldtelephone.CorrespondenceAddressID = null;
                                    Oldtelephone.Detach();
                                    DB.Save(Oldtelephone);


                                    telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)E1NumberComboBox.SelectedValue);

                                    telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                                    if (E1VirtualNumberTextBox.Text.Trim() != string.Empty)
                                    {
                                        long virtualTelephoneNo = 0;
                                        long.TryParse(E1VirtualNumberTextBox.Text.Trim(), out virtualTelephoneNo);
                                        if (virtualTelephoneNo != 0)
                                        {
                                            telephone.VirtualTelephoneNo = virtualTelephoneNo;
                                        }
                                        else
                                        {
                                            Folder.MessageBox.ShowInfo("شماره مجازی وارد شده صحیح نمیباشد");
                                            return false;
                                        }
                                    }
                                    telephone.CustomerID = _request.CustomerID;
                                    telephone.InstallAddressID = _e1.InstallAddressID;
                                    telephone.CorrespondenceAddressID = _e1.CorrespondenceAddressID;
                                    telephone.Detach();
                                    DB.Save(telephone);
                                }

                                _e1 = this.DataContext as CRM.Data.E1;
                                _e1.Detach();
                                DB.Save(_e1, false);

                                _request.StatusID = (int)StatusComboBox.SelectedValue;
                                _request.TelephoneNo = (long)E1NumberComboBox.SelectedValue;
                                _request.Detach();
                                DB.Save(_request);
                            }
                            break;
                        case (byte)DB.RequestType.VacateE1:
                            {
                                if (_subID != null)
                                {
                                    _e1Link.SwitchDate = DB.GetServerDate();
                                    _e1Link.Detach();
                                    DB.Save(_e1Link);
                                }
                                else
                                {
                                    if (!Data.E1LinkDB.CheckALLSwitchDate(_request.ID))
                                    {
                                        throw new Exception("اطلاعات مربوط به همه رکورد ها ذخیره نشده است");
                                    }
                                    else
                                    {
                                        _request.StatusID = (int)StatusComboBox.SelectedValue;
                                        _request.Detach();
                                        DB.Save(_request);
                                    }
                                }
                            }
                            break;
                        case (byte)DB.RequestType.SpaceandPower:
                            {
                                _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                                _spaceAndPower.SwitchDesigningOfficeComment = SwitchOfficeDescriptionTextBox.Text;
                                _spaceAndPower.SwitchDesigningOfficeDate = (SwitchOfficeDatePicker.SelectedDate.HasValue) ? SwitchOfficeDatePicker.SelectedDate : DB.GetServerDate();
                                _spaceAndPower.Detach();
                                DB.Save(_spaceAndPower, false);

                                _request.StatusID = (int)StatusComboBox.SelectedValue;
                                _request.Detach();
                                DB.Save(_request, false);
                            }
                            break;
                    }

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
            Save();
            if (IsSaveSuccess == true)
                IsForwardSuccess = true;
            return IsForwardSuccess;
        }

        public override bool Deny()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _request.StatusID = (int)StatusComboBox.SelectedValue;

                if (_request.RequestTypeID == (int)DB.RequestType.SpaceandPower)
                {
                    _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                    _spaceAndPower.SwitchDesigningOfficeComment = string.Empty;
                    _spaceAndPower.SwitchDesigningOfficeDate = null;
                    _spaceAndPower.Detach();
                    DB.Save(_spaceAndPower, false);

                    //بعد از رد درخواست در این مرحله چنانچه فایلی برای آن ذخیره شده باشد باید حذف گردد
                    //if (_switchOffices != null && _switchOffices.Count != 0)
                    //{
                    //    List<long> switchOfficeIds = new List<long>();
                    //    foreach (var item in _switchOffices)
                    //    {
                    //        switchOfficeIds.Add(item.ID);
                    //    }
                    //    if (switchOfficeIds.Count != 0)
                    //    {
                    //        DB.DeleteAll<SwitchOffice>(switchOfficeIds);
                    //    }
                    //}
                    IsRejectSuccess = true;
                }
                else
                {
                    if (_e1 != null && _e1.TelephoneNo != 0)
                    {
                        Telephone Oldtelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_e1.TelephoneNo);

                        Oldtelephone.Status = DB.GetTelephoneLastStatus(_e1.TelephoneNo ?? 0);
                        Oldtelephone.VirtualTelephoneNo = null;
                        Oldtelephone.CustomerID = null;
                        Oldtelephone.InstallAddressID = null;
                        Oldtelephone.CorrespondenceAddressID = null;
                        Oldtelephone.Detach();
                        DB.Save(Oldtelephone);

                        _e1.TelephoneNo = null;
                        _e1.SwitchPrecodeID = null;
                        _e1.SwitchID = null;
                        _e1.Detach();
                        DB.Save(_e1);
                        _request.TelephoneNo = null;
                        IsRejectSuccess = true;
                    }
                }
                _request.Detach();
                DB.Save(_request);

                scope.Complete();
            }
            return IsRejectSuccess;
        }

        #endregion

        #region EventHandlers

        private void SwitchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwitchComboBox.SelectedValue != null)
            {
                SwitchPrecodeIDComboBox.ItemsSource = Data.SwitchPrecodeDB.GetE1SwitchPrecodeCheckableItemBySwitchID((int)SwitchComboBox.SelectedValue);

            }
        }

        private void SwitchPrecodeIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwitchPrecodeIDComboBox.SelectedValue != null)
            {

                if (_e1.TelephoneNo == null)
                {
                    E1NumberComboBox.ItemsSource = Data.TelephoneDB.GetE1CheckableItemTelephoneBySwitchPreCodeID((int)SwitchPrecodeIDComboBox.SelectedValue);
                }
                else
                {
                    E1NumberComboBox.ItemsSource = Data.TelephoneDB.GetE1CheckableItemTelephoneBySwitchPreCodeID((int)SwitchPrecodeIDComboBox.SelectedValue).Union(new List<CheckableItem> { Data.TelephoneDB.GetCheckableItemTelephoneByTelephone(_e1.TelephoneNo ?? 0) });
                }

            }
        }

        //private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    //دکمه ذخیره فایل فعال میشود اگر و فقط اگر که یک فایل توسط کاربر انتخاب شده باشد
        //    e.CanExecute = this.FileIsSelected;
        //}

        //private void SaveCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        //{
        //    if (FileBytes != null && Extension != string.Empty)
        //    {
        //        DateTime currentDateTime = DB.GetServerDate();
        //        _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
        //        SwitchOffice switchOffice = new SwitchOffice();
        //        switchOffice.RequestID = _request.ID;
        //        switchOffice.SwitchFileID = _FileID;
        //        switchOffice.InsertDate = currentDateTime;
        //        switchOffice.Detach();
        //        DB.Save(switchOffice);

        //        RefreshItemsDataGrid();

        //        //بعد از عملیات ذخیره فایل تعیین شده ، باید محتوای دو فیلد زیر خالی شود
        //        this.Extension = string.Empty;
        //        this.FileBytes = null;

        //        FromFileRadioButton.IsChecked = false;
        //        FromScanerRadioButton.IsChecked = false;
        //        this.FileIsSelected = false;
        //    }
        //    else
        //    {
        //        Folder.MessageBox.ShowError("فایل انتخاب نشده است");
        //    }
        //}

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
                ShowErrorMessage("خطا در حذف فایل", ex);
            }


        }

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
            //try
            //{
            //    if (FromFileRadioButton.IsChecked == true)
            //    {
            //        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //        dlg.Filter = "All files (*.*)|*.*";
            //
            //        if (dlg.ShowDialog() == true)
            //        {
            //            FileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
            //            Extension = System.IO.Path.GetExtension(dlg.FileName);
            //        }
            //    }
            //    else if (FromScanerRadioButton.IsChecked == true)
            //    {
            //        Scanner oScanner = new Scanner();
            //        string extension;
            //
            //        FileBytes = oScanner.ScannWithExtension(out extension);
            //        Extension = extension;
            //    }
            //
            //    //اگر فایلی انتخاب شده باشد ، باید پراپرتی زیر مقدار درست بگیرد در غیر این صورت مقدار نادرست
            //    //this.FileIsSelected = (FileBytes != null && FileBytes.Length > 0 && !string.IsNullOrEmpty(Extension)) ? true : false;
            //
            //    //اگر فایلی انتخاب نشده باشد باید کنترلهای فایل و اسکنر به حالت اولیه خود برگردانده شوند اصطلاحاً ریست شوند
            //    if (this.FileIsSelected == false)
            //    {
            //        FromFileRadioButton.IsChecked = false;
            //        FromScanerRadioButton.IsChecked = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //this.FileIsSelected = false;
            //    Enterprise.Logger.Write(ex, "خطا در انتخاب فایل اداره طراحی سوئیچ");
            //    Folder.MessageBox.ShowError("خطا در انتخاب فایل");
            //}
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

    }
}
