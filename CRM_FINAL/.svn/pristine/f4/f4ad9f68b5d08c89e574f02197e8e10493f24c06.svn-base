using CRM.Application.Views;
using CRM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for VacateSpecialWireUserControl.xaml
    /// </summary>
    public partial class ChangeLocationSpecialWireUserControl : Local.UserControlBase
    {
        private long _requestID;
        public Request Request { get; set; }
        Bucht sourceBucht = new Bucht();
        Center sourceCenter = new Center();
        public Customer customer { get; set; }
        public ObservableCollection<SpecialWirePoints> _specialWirePoints;
        public List<SpecialWirePoints> SpecialWirePoints
        {
            get 
            {
                return new List<SpecialWirePoints>(_specialWirePoints);
            }
        }

        public ChangeLocationSpecialWireUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

           
        }

        public ChangeLocationSpecialWireUserControl(long requestID)
            : this()
        {
            this._requestID = requestID;
        }
     

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (_IsLoaded)
                return;
            else
                _IsLoaded = false;

            if (this._requestID == 0)
            {

            }
            else
            {
                Request = Data.RequestDB.GetRequestByID(this._requestID);
                TelephoneNoTextBox.Text = Request.TelephoneNo.ToString();
                TelephoneNoButton_Click(null,null);
                if (!Data.StatusDB.IsFinalStep(Request.StatusID))
                {
                    if (Request.MainRequestID != null)
                    {
                        List<ChangeLocationSpecialWirePoint> changeLocationSpecialWirePoint = Data.ChangeLocationSpecialWirePointsDB.GetChangeLocationSpecialWirePointsByRequestID((long)Request.MainRequestID);

                        if (_specialWirePoints.Count > 0)
                        {
                            changeLocationSpecialWirePoint.ForEach(item =>
                            {

                                _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().IsSelect = true;
                                _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NearestTelephoneNo = item.NearestTelephoneNo;
                                _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NewInstallAddressID = item.NewInstallAddressID;
                                _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NewAddressContent = item.NewAddressContent;
                                _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NewPostCode = item.NewPostalCode;

                            }
                        );

                        }
                    }
                    else
                    {
                        List<ChangeLocationSpecialWirePoint> changeLocationSpecialWirePoint = Data.ChangeLocationSpecialWirePointsDB.GetChangeLocationSpecialWirePointsByRequestID((long)Request.ID);

                        if (_specialWirePoints.Count > 0)
                        {
                            changeLocationSpecialWirePoint.ForEach(item =>
                             {

                                 _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().IsSelect = true;
                                 _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NearestTelephoneNo = item.NearestTelephoneNo;
                                 _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NewInstallAddressID = item.NewInstallAddressID;
                                 _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NewAddressContent = item.NewAddressContent;
                                 _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().NewPostCode = item.NewPostalCode;
                             }

                            );

                        }
                    }

                }
                  
                


              
            }

        }

        private void TelephoneNoButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(Convert.ToInt64(TelephoneNoTextBox.Text.Trim()));

            if (BlackListDB.ExistTelephoneNoInBlackList(telephone.TelephoneNo))
            {
                Folder.MessageBox.ShowError("تلفن در لیست سیاه قرار دارد");
            }
            else
            {
                if (telephone == null)
                {
                    isValid = false;
                    Folder.MessageBox.ShowInfo("سیم خصوصی بااین تلفن یافت نشد");
                }
                if (_requestID == 0)
                {
                    // check to exist telephone on other request
                    bool inWaitingList = false;
                    string requestName = Data.RequestDB.GetOpenRequestNameTelephone(new List<long>{ telephone.TelephoneNo} , out inWaitingList);
                    if (!string.IsNullOrWhiteSpace(requestName))
                    {
                        isValid = false;
                        Folder.MessageBox.ShowError("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
                    }
                    else if (telephone.Status != (int)DB.TelephoneStatus.Connecting)
                    {
                        isValid = false;
                        Folder.MessageBox.ShowError("تلفن در وضعیت دایر قرار ندارد");
                    }
                    // 
                }

                if (isValid == true)
                {
                    City city = Data.CityDB.GetCityByCenterID(telephone.CenterID);
                    CentersComboBoxColumn.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                    customer = Data.CustomerDB.GetCustomerByID(telephone.CustomerID ?? Request.CustomerID ?? 0);
                    _specialWirePoints = new ObservableCollection<SpecialWirePoints>(Data.SpecialWirePointsDB.GetSpecialWirePointsByTelephone(telephone.TelephoneNo));
                    sourceCenter = Data.SpecialWireDB.GetSourceCenterSpecialWireByTelephoneNo(telephone.TelephoneNo, out sourceBucht);
                }

                PointsInfoDataGrid.ItemsSource = _specialWirePoints;
            }
        }

        private void PointsInfoDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (sourceBucht != null && sourceBucht.ID != 0 && (e.Row.Item as SpecialWirePoints) != null && (e.Row.Item as SpecialWirePoints).BuchtID == sourceBucht.ID)
            {
                e.Row.Background = new SolidColorBrush(Colors.LightSteelBlue);
            }
        }
        
        private void PointsInfoDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if ((PointsInfoDataGrid.SelectedItem as SpecialWirePoints) != null && !string.IsNullOrEmpty((PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewPostCode))
            {
                string postalCode = (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewPostCode;
                int countAddress = Data.AddressDB.GetAddressByPostalCode(postalCode).Count;
                if (countAddress == 0)
                {
                    CustomerAddressForm customerAddressForm = new CustomerAddressForm();
                    customerAddressForm.PostallCode = postalCode;
                    customerAddressForm.ShowDialog();
                    if (customerAddressForm.DialogResult ?? false)
                    {
                        if (customerAddressForm.ID != 0)
                        {
                            Address address = Data.AddressDB.GetAddressByID(customerAddressForm.ID);
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewAddressContent = string.Empty;
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewAddressContent = address.AddressContent;
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewInstallAddressID = address.ID;
                        }
                        else
                        {
                            Folder.MessageBox.ShowInfo("کد پستی یافت نشد");
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewAddressContent = string.Empty;
                            (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewInstallAddressID = null;
                        }
                    }
                    else
                    {
                        Folder.MessageBox.ShowInfo("کد پستی یافت نشد");
                        (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewAddressContent = string.Empty;
                        (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewInstallAddressID = null;
                    }
                }

                else if (countAddress == 1)
                {
                    Address address = Data.AddressDB.GetAddressByPostalCode(postalCode).Take(1).SingleOrDefault();
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewAddressContent = string.Empty;
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewAddressContent = address.AddressContent;
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewInstallAddressID = address.ID;
                }
                else if (countAddress >= 2)
                {
                    Folder.MessageBox.ShowInfo("با کد پستی چند آدرس یافت شد لطفا اطلاعات مشترک را اصلاح کنید");
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewAddressContent = string.Empty;
                    (PointsInfoDataGrid.SelectedItem as SpecialWirePoints).NewInstallAddressID = null;
                }

            }
        }
    }
}
