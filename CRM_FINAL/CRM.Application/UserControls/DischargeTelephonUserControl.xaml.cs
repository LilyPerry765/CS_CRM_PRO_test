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
    /// Interaction logic for DischargeTelephonUserControl.xaml
    /// </summary>
    public partial class DischargeTelephonUserControl : Local.UserControlBase
    {

        #region properties

        long _ReqID = 0;
        long _CustomerID = 0;
        long _TelephoneNo = 0;

        public CRM.Data.TakePossession takePossession = new Data.TakePossession();
        TeleInfo teleInfo = new TeleInfo();
        Request _Request { get; set;}

        public TeleInfo TeleInfo
        {
            get
            {
                return teleInfo;
            }
            set
            {
                teleInfo = value;
            }
        }
        #endregion

        #region Costructors

        public DischargeTelephonUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        public DischargeTelephonUserControl(long requestID, long customerID, long telephoneNo)
            : this()
        {
            _ReqID = requestID;
            _CustomerID = customerID;
            _TelephoneNo = telephoneNo;
        }

        #endregion

        #region Method

        private void Initialize()
        {
            ReasonDisChargeComboBox.ItemsSource = Data.CauseOfTakePossessionDB.GetCauseOfTakePossessionCheckableItem();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_CustomerID != 0)
            {
                // TODO : در جدول Telephone فیلد های CustomerID و CustomerAddressID اضافه شده که در پایان دایری باید پر شوند

                List<Telephone> teleList = Data.TelephoneDB.GetTelephoneByCustomerID(_CustomerID);
                List<TeleInfo> teleInfoList = Data.TelephoneDB.GetTelephoneInfoByTelePhonNo(teleList).ToList();
                TelephoneDataGrid.DataContext = teleInfoList;
                
                if (teleInfoList != null && teleInfoList.Count > 0)
                    TelephoneDataGrid.SelectedValue = _TelephoneNo;
            }

            if (_ReqID != 0)
            {
                takePossession = Data.TakePossessionDB.GetTakePossessionByID(_ReqID);
                _Request = Data.RequestDB.GetRequestByID(_ReqID);
                ///////// تنظیم مقدار DataGrid
                List<Telephone> telLisst = new List<Telephone>();
                Telephone tel = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_Request.TelephoneNo);
                telLisst.Add(tel);
                teleInfo = Data.TelephoneDB.GetTelephoneInfoByTelePhonNo(telLisst).SingleOrDefault();
                if (teleInfo != null && TelephoneDataGrid.Items.Count > 0)
                    TelephoneDataGrid.SelectedValue = teleInfo.TelephoneNo;


                ////////

            }

            this.DataContext = takePossession;
        }

        #endregion

        #region Event Handler

        private void TelephoneDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            teleInfo = TelephoneDataGrid.SelectedItem as TeleInfo;
        }

        #endregion
    }
}
