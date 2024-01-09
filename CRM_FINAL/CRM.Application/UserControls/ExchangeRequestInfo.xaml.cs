using System;
using System.Collections.Generic;
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
using CRM.Data;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for ExchangeRequestInfo.xaml
    /// </summary>
    public partial class ExchangeRequestInfo : UserControl
    {
        #region Properties and Fields

        List<int> RequestTypeIDs = new List<int>();
        private int CityID = 0;

        private long _ID = 0;
        public long ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        private int status = -1;
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        Request request;
        public Request Request
        {
            get
            {
                return request;
            }
            set
            {
                request = value;
            }
        }

        int requestType;
        public int RequestType
        {
            get
            {
                return requestType;
            }
            set
            {
                requestType = value;
            }
        }

        private List<ExchangePost> exchangePost = new List<ExchangePost>();
        public List<ExchangePost> ExchangePostExchangeRequestInfo
        {
            get
            {
                return exchangePost;
            }
            set
            {
                exchangePost = value;
            }
        }

        #endregion

        #region Constructor

        public ExchangeRequestInfo()
        {
            InitializeComponent();
            Initialize();
        }

        public ExchangeRequestInfo(long id)
            : this()
        {
            _ID = id;

        }

        #endregion

        #region Methods

        public void Initialize()
        {
            RequestTypeComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        public void Load()
        {
            if (_ID == 0)
            {
                request = new Data.Request();
                request.RequestTypeID = RequestType;
                request.RequestDate = DB.GetServerDate();
            }
            else
            {
                request = Data.RequestDB.GetRequestByID(_ID);
                CityID = Data.CityDB.GetCityByCenterID(request.CenterID).ID;

                RequestTypeComboBox.SelectedValue = request.RequestTypeID;
                RequestTypeComboBox_SelectionChanged(null, null);

                CenterComboBox.SelectedValue = request.CenterID;
                CenterComboBox_SelectionChanged_1(null, null);
            }


            this.DataContext = request;

            if (CityID == 0)
                CityComboBox.SelectedIndex = 0;
            else
                CityComboBox.SelectedValue = CityID;

            RequestTypeComboBox.IsEnabled = false;
        }

        #endregion

        #region EventHandlers

        private void RequestTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RequestTypeComboBox.SelectedValue != null)
            {
                RequestType = (int)RequestTypeComboBox.SelectedValue;
                if (DoRequestTypeChange != null)
                    DoRequestTypeChange((int)RequestTypeComboBox.SelectedValue);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);

                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);

                (this.RequestInfo.DataContext as Request).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
                CenterComboBox.SelectedItem = (CenterComboBox.Items[0] as CenterInfo);
                CenterComboBox_SelectionChanged_1(null, null);
            }
            else
            {
                if (CityComboBox.SelectedValue == null)
                {
                    City city = Data.CityDB.GetCityById(CityID);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
                else
                {
                    City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
            }
        }

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
                OnDoCenterChange((int)CenterComboBox.SelectedValue);
        }

        #endregion

        #region Custom Events

        /// <summary>
        /// for when change RequestType 
        /// </summary>
        /// <param name="RequestTypeID"> return RequestTypeID</param>
        public delegate void RequestTypeChange(int RequestTypeID);
        public event RequestTypeChange DoRequestTypeChange;
        public void OnDoRequestTypeChange(int RequestTypeID)
        {
            if (DoRequestTypeChange != null)
                DoRequestTypeChange(RequestTypeID);
        }

        /// <summary>
        /// For when change Center
        /// </summary>
        /// <param name="centerID">Return CenterID</param>
        public delegate void CenterChange(int centerID);
        public event CenterChange DoCenterChange;
        public void OnDoCenterChange(int centerID)
        {
            if (DoCenterChange != null)
                DoCenterChange(centerID);
        }

        #endregion

    }
}
