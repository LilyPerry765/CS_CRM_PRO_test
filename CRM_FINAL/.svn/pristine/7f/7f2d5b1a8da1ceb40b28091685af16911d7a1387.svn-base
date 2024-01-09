using CRM.Application.UserControls;
using CRM.Data;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for VisitPlacesForm2.xaml
    /// </summary>
    public partial class VisitPlacesForm : Local.RequestFormBase
    {
        #region Properties and Fields

        private long _requestID;
        private Request _request;
        private Address _address;
        public bool _openedInInvestigateForm = false;
        public bool ShowOldAddressInPrint = false; //از این متغیرر جهت نمایش و یا عدم نمایش آدرس قدیم بر روی گزارشان بازدید از محل استفاده شده است
        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.InstallInfoSummary _installInfoSummary { get; set; }

        public ObservableCollection<CheckableItem> _Cabinets { get; set; }
        public ObservableCollection<CheckableItem> _Posts { get; set; }

        VisitAddress _visitAddress = new VisitAddress();

        #endregion

        #region Constructors

        public VisitPlacesForm()
        {
            InitializeComponent();
            Initialize();
        }

        public VisitPlacesForm(long requestID)
            : this()
        {
            base.RequestID = this._requestID = requestID;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        private void LoadData()
        {
            if (!_openedInInvestigateForm)
            {
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
            }
            else
            {
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Print };

                CustomerInfoUC.Visibility = Visibility.Collapsed;
                RequestInfoUC.Visibility = Visibility.Collapsed;

                StatusLabel.Visibility = Visibility.Collapsed;
                StatusComboBox.Visibility = Visibility.Collapsed;
            }

            DateTime dateTime = DB.GetServerDate();
            _visitAddress.VisitDate = dateTime;
            _visitAddress.VisitHour = dateTime.Hour.ToString() + ":" + dateTime.Minute.ToString();
            if (_requestID != 0)
            {
                _request = Data.RequestDB.GetRequestByID(_requestID);

                _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
                CustomerInfoUC.Content = _customerInfoSummary;
                CustomerInfoUC.DataContext = _customerInfoSummary;

                _requestInfoSummary = new RequestInfoSummary(_request.ID);
                RequestInfoUC.Content = _requestInfoSummary;
                RequestInfoUC.DataContext = _requestInfoSummary;

                _Cabinets = new ObservableCollection<CheckableItem>(Data.CabinetDB.GetCabinetCheckableByCenterID(_request.CenterID));
            }
            CrossCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(_request.CenterID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;

            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    {
                        DayriLoaded();
                    }
                    break;

                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                case (int)DB.RequestType.ChangeLocationCenterInside:
                    {
                        ChangeLocationLoaded();
                        ShowOldAddressInPrint = true;
                    }
                    break;

                case (int)DB.RequestType.E1:
                    {
                        E1Loaded();
                    }
                    break;

                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    {
                        SpecialWireLoaded();
                    }
                    break;

                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    {
                        ChangeLocationSpecialWireLoaded();
                        ShowOldAddressInPrint = true;
                    }
                    break;
            }

            if (_address == null)
            {
                throw new Exception("اطلاعات آدرس یافت نشد.");
            }

            PostCodeTextbox.Text = _address.PostalCode;
            AddressTextbox.Text = _address.AddressContent;

            VisitAddress visitAddress = Data.VisitAddressDB.GetVisitAddressByRequestID(_requestID, _address.ID).OrderByDescending(t => t.ID).FirstOrDefault();
            VisitDateComboBox.ItemsSource = Data.VisitAddressDB.GetVisitAddressCheckable(_address.ID).Union(new List<CheckableItem> { new CheckableItem { ID = -1, Name = string.Empty, IsChecked = false } });

            if (visitAddress != null)
            {
                VisitPlacesDetails.DataContext = _visitAddress = visitAddress;

                StatusComboBox.SelectedValue = _request.StatusID;
                StatusComboBox_SelectionChanged(null, null);

                if (visitAddress.CrossPostID != null)
                {
                    Cabinet cabinet = Data.CabinetDB.GetCabinetByPostID((int)visitAddress.CrossPostID);
                    CrossCabinetComboBox.SelectedValue = cabinet.ID;
                    CrossCabinetComboBox_SelectionChanged(null, null);
                    CrossPostComboBox.SelectedValue = visitAddress.CrossPostID;
                }
            }
            else
            {
                VisitPlacesDetails.DataContext = _visitAddress;
            }
            ItemsDataGrid.ItemsSource = Data.VisitAddressDB.GetVisitAddressCabinetAndPost(_address.ID);
        }

        private void OutBoundCheckBoxVisitInfo()
        {
            if (OutBoundCheckBox.IsChecked == true)
            {
                OutBoundMeterLabel.Visibility = Visibility.Visible;
                OutBoundMetertextBox.Visibility = Visibility.Visible;

                OutBoundEstablishDateLabel.Visibility = Visibility.Visible;
                OutBoundEstablishDatePicker.Visibility = Visibility.Visible;


                SixMeterMastsLabel.Visibility = Visibility.Visible;
                SixMeterMastsTextBox.Visibility = Visibility.Visible;

                EightMeterMastsLabel.Visibility = Visibility.Visible;
                EightMeterMastsTextBox.Visibility = Visibility.Visible;

                ThroughWidthLabel.Visibility = Visibility.Visible;
                ThroughWidthTextBox.Visibility = Visibility.Visible;

                CrossCabinetLabel.Visibility = Visibility.Visible;
                CrossCabinetComboBox.Visibility = Visibility.Visible;

                CrossPostMeterLabel.Visibility = Visibility.Visible;
                CrossPostComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                OutBoundMeterLabel.Visibility = Visibility.Collapsed;
                OutBoundMetertextBox.Visibility = Visibility.Collapsed;

                OutBoundEstablishDateLabel.Visibility = Visibility.Collapsed;
                OutBoundEstablishDatePicker.Visibility = Visibility.Collapsed;

                SixMeterMastsLabel.Visibility = Visibility.Collapsed;
                SixMeterMastsTextBox.Visibility = Visibility.Collapsed;

                EightMeterMastsLabel.Visibility = Visibility.Collapsed;
                EightMeterMastsTextBox.Visibility = Visibility.Collapsed;

                ThroughWidthLabel.Visibility = Visibility.Collapsed;
                ThroughWidthTextBox.Visibility = Visibility.Collapsed;

                CrossCabinetLabel.Visibility = Visibility.Collapsed;
                CrossCabinetComboBox.Visibility = Visibility.Collapsed;

                CrossPostMeterLabel.Visibility = Visibility.Collapsed;
                CrossPostComboBox.Visibility = Visibility.Collapsed;

                OutBoundMetertextBox.Text = string.Empty;
                OutBoundEstablishDatePicker.SelectedDate = null;
            }
        }


        #endregion

        #region Loaded

        private void ChangeLocationSpecialWireLoaded()
        {
            CRM.Data.ChangeLocationSpecialWire changeLocationSpecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(_requestID);
            _address = Data.AddressDB.GetAddressByID((long)changeLocationSpecialWire.InstallAddressID);
        }

        private void SpecialWireLoaded()
        {
            CRM.Data.SpecialWire specialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(_requestID);
            _address = Data.AddressDB.GetAddressByID((long)specialWire.InstallAddressID);
            ShowOldAddressInPrint = true;
        }

        private void E1Loaded()
        {
            CRM.Data.E1 e1 = Data.E1DB.GetE1ByRequestID(_requestID);
            _address = Data.AddressDB.GetAddressByID((long)e1.InstallAddressID);
        }

        private void DayriLoaded()
        {
            InstallRequest installRequest = Data.InstallRequestDB.GetInstallRequestByRequestID(_requestID);
            _address = Data.AddressDB.GetAddressByID((long)installRequest.InstallAddressID);
        }

        private void DischarginLoaded()
        {

        }

        private void ChangeLocationLoaded()
        {
            ChangeLocation changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(_requestID);
            _address = Data.AddressDB.GetAddressByID((long)changeLocation.NewInstallAddressID);
        }

        #endregion

        #region Combobox Loaded

        ComboBox CabinetComboBox = new ComboBox();
        private void CabinetComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            CabinetComboBox = sender as ComboBox;
        }

        ComboBox PostComboBox = new ComboBox();
        private void PostComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            PostComboBox = sender as ComboBox;
        }

        #endregion

        #region EventHandlers

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در بارگزاری اطلاعات", ex);
            }
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CrossCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CrossCabinetComboBox.SelectedValue != null)
            {
                CrossPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)CrossCabinetComboBox.SelectedValue);
            }
        }

        private void OutBoundCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            OutBoundCheckBoxVisitInfo();
        }

        private void OutBoundCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            OutBoundCheckBoxVisitInfo();
        }

        private void CabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetComboBox.SelectedValue != null)
            {
                _Posts = new ObservableCollection<CheckableItem>(Data.PostDB.GetPostCheckableByCabinetID((int)CabinetComboBox.SelectedValue));
            }

            if (CabinetComboBox.SelectedItem != null)
            {
                (ItemsDataGrid.SelectedItem as VisitPlacesCabinetAndPostClass).CabinetNumber = (CabinetComboBox.SelectedItem as CheckableItem).Name;
            }
        }

        private void PostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostComboBox.SelectedItem != null)
            {
                (ItemsDataGrid.SelectedItem as VisitPlacesCabinetAndPostClass).PostNumber = (PostComboBox.SelectedItem as CheckableItem).Name;
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                VisitPlacesCabinetAndPostClass visitPlacesCabinerAndPost = ItemsDataGrid.SelectedItem as VisitPlacesCabinetAndPostClass;
                if (_visitAddress.ID == 0)
                {
                    _visitAddress = VisitPlacesDetails.DataContext as VisitAddress;
                    _visitAddress.AddressID = _address.ID;
                    _visitAddress.RequestID = _requestID;
                    _visitAddress.Detach();
                    DB.Save(_visitAddress, true);
                }

                if (visitPlacesCabinerAndPost.ID == 0)
                {

                    VisitPlacesCabinetAndPost visitPlacesCabinetAndPost = new VisitPlacesCabinetAndPost();
                    visitPlacesCabinetAndPost.VisitAddressID = _visitAddress.ID;
                    visitPlacesCabinetAndPost.CabinetID = visitPlacesCabinerAndPost.CabinetID;
                    visitPlacesCabinetAndPost.PostID = visitPlacesCabinerAndPost.PostID;

                    visitPlacesCabinetAndPost.Detach();
                    DB.Save(visitPlacesCabinetAndPost, true);
                }
                else
                {

                    VisitPlacesCabinetAndPost item = Data.VisitPlacesCabinetAndPostDB.GetVisitPlacesCabinetAndPostByID(visitPlacesCabinerAndPost.ID);
                    item.CabinetID = visitPlacesCabinerAndPost.CabinetID;
                    item.PostID = visitPlacesCabinerAndPost.PostID;
                    item.Detach();
                    DB.Save(item, false);
                }

                ts.Complete();
            }

            RequestFormBase_Loaded(null, null);

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            VisitPlacesCabinetAndPostClass visitPlacesCabinerAndPost = ItemsDataGrid.SelectedItem as VisitPlacesCabinetAndPostClass;
            if (visitPlacesCabinerAndPost != null)
            {
                DB.Delete<VisitPlacesCabinetAndPost>(visitPlacesCabinerAndPost.ID);
            }

            RequestFormBase_Loaded(null, null);
        }

        #endregion

        #region Actions

        public override bool Print()
        {
            IEnumerable result = ReportDB.GetVisitTheSiteByRequestID(new List<long> { _requestID });

            //StiVariables
            StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
            StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
            StiVariable cityNameVariable = new StiVariable("CityName", "CityName", DB.PersianCity);
            switch (_request.RequestTypeID)
            {
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                case (int)DB.RequestType.ChangeLocationCenterInside:
                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    {
                        //این نوع از درخواستها دارای آدرس قدیم میباشند
                        CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.VisitAddressWithOldAddressReport, true, dateVariable, timeVariable, cityNameVariable);
                    }
                    break;
                default:
                    {
                        CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.VisitAddressReport, true, dateVariable, timeVariable, cityNameVariable);
                    }
                    break;
            }
            //RequestsInbox.ShowReport(result, string.Empty, (int)DB.UserControlNames.VisitAddressReport, ShowOldAddressInPrint);
            return true;
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
                Status Status = Data.StatusDB.GetStatueByStatusID((int)StatusComboBox.SelectedValue);
                if (OutBoundCheckBox.IsChecked == true && (string.IsNullOrEmpty(OutBoundMetertextBox.Text) || Convert.ToInt32(OutBoundMetertextBox.Text.Trim()) == 0))
                {
                    MessageBox.Show("لطفا متراژ خارج از مرز را وارد کنید");
                    IsSaveSuccess = false;
                    return false;
                }



                using (TransactionScope ts = new TransactionScope())
                {
                    _visitAddress = VisitPlacesDetails.DataContext as VisitAddress;
                    if (_visitAddress.RelatedVisitID == -1)
                        _visitAddress.RelatedVisitID = null;

                    if (_visitAddress.ID == 0)
                    {

                        _visitAddress.AddressID = _address.ID;
                        _visitAddress.RequestID = _requestID;
                        _visitAddress.Detach();
                        DB.Save(_visitAddress, true);
                    }
                    else
                    {
                        _visitAddress.Detach();
                        DB.Save(_visitAddress, false);
                    }

                    if (!_openedInInvestigateForm)
                    {
                        _request.StatusID = (int)StatusComboBox.SelectedValue;
                        _request.Detach();
                        DB.Save(_request, false);
                    }
                    ts.Complete();
                }
                IsSaveSuccess = true;
                ShowSuccessMessage("ذخیره انجام شد");
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            RequestFormBase_Loaded(null, null);
            return IsSaveSuccess;

        }

        public override bool Forward()
        {
            Save();
            if (IsSaveSuccess == true)
                IsForwardSuccess = true;

            return IsForwardSuccess;
        }

        #endregion

    }
}
