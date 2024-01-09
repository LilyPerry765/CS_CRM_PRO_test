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
    public partial class VacateSpecialWireUserControl : Local.UserControlBase
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

        public VacateSpecialWireUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

           
        }

        public VacateSpecialWireUserControl(long requestID)
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
                if(Request.MainRequestID != null)
                {
                    List<VacateSpecialWirePoint> vacateSpecialWirePoint = Data.VacateSpecialWirePointsDB.GetVacateSpecialWirePointsByRequestID((long)Request.MainRequestID);

                    if(_specialWirePoints.Count > 0)
                    vacateSpecialWirePoint.ForEach
                        (item => 
                            {
                                if(_specialWirePoints.Any(t => t.BuchtID == item.BuchtID))
                                _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().IsSelect = true;
                            }
                        );
                    this.IsEnabled = false;
                }
                else
                {
                       List<VacateSpecialWirePoint> vacateSpecialWirePoint = Data.VacateSpecialWirePointsDB.GetVacateSpecialWirePointsByRequestID((long)Request.ID);

                       if (_specialWirePoints.Count > 0)
                           vacateSpecialWirePoint.ForEach(item =>
                               {
                                   if (_specialWirePoints.Any(t => t.BuchtID == item.BuchtID))
                                   _specialWirePoints.Where(t => t.BuchtID == item.BuchtID).SingleOrDefault().IsSelect = true;
                               }
                               );

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
                    string requestName = Data.RequestDB.GetOpenRequestNameTelephone(new List<long>{ telephone.TelephoneNo}, out inWaitingList);
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
    }
}
