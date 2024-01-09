using CRM.Application.Local;
using CRM.Data;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for VisitPlaceByTelephoneForm.xaml
    /// </summary>
    public partial class VisitPlaceByTelephoneForm : PopupWindow
    {

        #region Properties and Feilds

        private long _telephoneNo;

        private Address _address = new Address();

        private Telephone _telephone = new Telephone();

        #endregion

        #region Constructor

        public VisitPlaceByTelephoneForm()
        {
            InitializeComponent();
        }

        public VisitPlaceByTelephoneForm(long telephoneNo)
            : base()
        {
            this._telephoneNo = telephoneNo;
        }

        #endregion

        #region EventHandlers

        private void OutBoundCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            OutBoundCheckBoxVisitInfo();
        }

        private void OutBoundCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            OutBoundCheckBoxVisitInfo();
        }

        private void CrossCabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CrossCabinetComboBox.SelectedValue != null)
            {
                CrossPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID((int)CrossCabinetComboBox.SelectedValue);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                return;
            }
            try
            {
                VisitAddress visitAddress = VisitPlacesDetails.DataContext as VisitAddress;
                if (visitAddress != null)
                {
                    if (OutBoundCheckBox.IsChecked == true)
                    {
                        if (string.IsNullOrEmpty(OutBoundMetertextBox.Text.Trim()) || OutBoundMetertextBox.Text.Trim().Equals("0"))
                        {
                            MessageBox.Show(".لطفاً متراژ خارج از مرز مشخص نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                    else
                    {
                        //چنانچه کاربر تلفن را از حالت "خارج از مرز بودن" دربیاورد فیلدهای زیر باید خالی شوند
                        visitAddress.IsOutBound = false;
                        visitAddress.OutBoundEstablishDate = null;
                        visitAddress.OutBoundMeter = null;
                        visitAddress.SixMeterMasts = null;
                        visitAddress.EightMeterMasts = null;
                        visitAddress.ThroughWidth = null;
                        visitAddress.CrossPostID = null;
                    }
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (visitAddress.RelatedVisitID == -1)
                        {
                            visitAddress.RelatedVisitID = null;
                        }
                        DateTime currentDate = DB.GetServerDate();
                        visitAddress.VisitDate = currentDate;
                        visitAddress.VisitHour = currentDate.Hour.ToString() + ":" + currentDate.Minute.ToString();
                        visitAddress.Detach();
                        DB.Save(visitAddress, false);

                        //ثبت لاگ مربوط به بازدید از محل
                        CRM.Data.Schema.TelephoneVisitAddress telephoneVisitAddress = new Data.Schema.TelephoneVisitAddress();
                        telephoneVisitAddress.TelephoneNo = this._telephone.TelephoneNo;
                        telephoneVisitAddress.AddressID = this._address.ID;
                        telephoneVisitAddress.AddressContent = this._address.AddressContent;
                        telephoneVisitAddress.PostalCode = this._address.PostalCode;
                        telephoneVisitAddress.AirCableMeter = visitAddress.AirCableMeter;
                        telephoneVisitAddress.CableMeter = visitAddress.CableMeter;
                        telephoneVisitAddress.CrossPostID = visitAddress.CrossPostID;
                        telephoneVisitAddress.EightMeterMasts = visitAddress.EightMeterMasts;
                        telephoneVisitAddress.IsOutBound = visitAddress.IsOutBound;
                        telephoneVisitAddress.OutBoundEstablishDate = visitAddress.OutBoundEstablishDate;
                        telephoneVisitAddress.OutBoundMeter = visitAddress.OutBoundMeter;
                        telephoneVisitAddress.SixMeterMasts = visitAddress.SixMeterMasts;
                        telephoneVisitAddress.ThroughWidth = visitAddress.ThroughWidth;
                        telephoneVisitAddress.VisitDate = visitAddress.VisitDate;
                        telephoneVisitAddress.VisitHour = visitAddress.VisitHour;
                        RequestLog requestLog = new RequestLog();
                        requestLog.RequestTypeID = (int)DB.RequestType.TelephoneVisitAddress;
                        requestLog.TelephoneNo = telephoneVisitAddress.TelephoneNo;
                        requestLog.UserID = DB.CurrentUser.ID;
                        requestLog.Date = DB.GetServerDate();
                        requestLog.Description = System.Xml.Linq.XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.TelephoneVisitAddress>(telephoneVisitAddress, true));
                        requestLog.Detach();
                        DB.Save(requestLog);
                        //پایان بخش ثبت لاگ

                        scope.Complete();
                        ShowSuccessMessage(".ذخیره با موفقیت انجام شد");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
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

        private void SearchTelephone(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()))
            {
                long telephoneNo = long.Parse(TelephoneNoTextBox.Text.Trim());
                this._telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);
                CrossCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(_telephone.CenterID);

                //مقداردهی کنترل های مربوط به آدرس تلفن
                if (this._telephone.InstallAddressID.HasValue)
                {
                    _address = AddressDB.GetAddressByID((long)_telephone.InstallAddressID);
                    PostCodeTextbox.Text = _address.PostalCode;
                    AddressTextbox.Text = _address.AddressContent;
                }
                else
                {
                    MessageBox.Show(".برای تلفن وارد شده ، آدرسی ثبت نشده است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                //**********************************************

                //مقداردهی کنترل های مربوط به بازدید از محل
                VisitAddress visitAddress = VisitAddressDB.GetLastVisitAddressByTelephoneNo(telephoneNo);
                if (visitAddress != null)
                {
                    VisitDateComboBox.ItemsSource = Data.VisitAddressDB.GetVisitAddressCheckable(visitAddress.AddressID)
                                                                   .Union(new List<CheckableItem> { new CheckableItem { ID = -1, Name = string.Empty, IsChecked = false } });
                    if (visitAddress.CrossPostID != null)
                    {
                        Cabinet cabinet = Data.CabinetDB.GetCabinetByPostID((int)visitAddress.CrossPostID);
                        CrossCabinetComboBox.SelectedValue = cabinet.ID;
                        CrossCabinetComboBox_SelectionChanged(null, null);
                        CrossPostComboBox.SelectedValue = visitAddress.CrossPostID;
                    }
                    VisitPlacesDetails.DataContext = visitAddress;
                }
                else
                {
                    MessageBox.Show(".این تلفن بازدید نداشته است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //**********************************************
            }
            else
            {
                MessageBox.Show(".شماره تلفن را وارد نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            VisitPlacesDetails.DataContext = new VisitAddress();
        }

        #endregion

    }
}
