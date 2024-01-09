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
    public partial class ADSLDischargeUserControl : UserControl
    {
        #region Properties

        private long _ReqID = 0;
        private long _TelephoneNo { get; set; }

        private Data.ADSLDischarge _ADSLDischarge { get; set; }
        private Request _Request { get; set; }
        public Data.ADSL _ADSL { get; set; }
        public TeleInfo TeleInfo { get; set; }
        private int customerGroupID { get; set; }

        private int sellerAgentID { get; set; }

        #endregion

        #region Costructors

        public ADSLDischargeUserControl()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public ADSLDischargeUserControl(long requestID, long telephoneNo)
            : this()
        {
            _ReqID = requestID;
            _TelephoneNo = telephoneNo;
            LoadData(null, null);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ADSLDischargeReasonComboBox.ItemsSource = ADSLDischargeDB.GetADSLDischargeReasonCheckable();
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            _ADSL = Data.ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (_ADSL.WasPCM == true)
                PCMLabel.Visibility = Visibility.Visible;

            if (_ReqID == 0)
            {
                ServiceInfo.DataContext = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSL.TariffID);

            }
            else
            {
                ADSLDischarge aDSLDischarge = Data.ADSLDischargeDB.GetADSLDischargeByID(_ReqID);
                CommentTextBox.Text = aDSLDischarge.Comment ?? string.Empty;
                _Request = Data.RequestDB.GetRequestByID(_ReqID);
                _ADSLDischarge = DB.SearchByPropertyName<Data.ADSLDischarge>("ID", _ReqID).Take(1).SingleOrDefault();
                _ADSL = Data.ADSLDB.GetADSLByTelephoneNo((long)_Request.TelephoneNo);
                ServiceInfo.DataContext = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSL.TariffID);
                ADSLDischargeReasonComboBox.SelectedValue = _ADSLDischarge.ReasonID;
            }
        }

        #endregion
    }
}
