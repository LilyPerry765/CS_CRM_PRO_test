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
    /// Interaction logic for ADSLChangePort.xaml
    /// </summary>
    public partial class ADSLChangePort : UserControl
    {
        #region Properties

        private long _RequsetID = 0;

        private CRM.Data.ADSLChangePort1 _ADSLChangePort { get; set; }
        private Request _Request { get; set; }
        private Data.ADSL _ADSL { get; set; }

        public Data.ADSLPort port { get; set; }

        public long TelephoneNo { get; set; }

        #endregion

        #region Constructors

        public ADSLChangePort()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public ADSLChangePort(long requestID, long telephoneNo)
            : this()
        {
            _RequsetID = requestID;
            TelephoneNo = telephoneNo;

            LoadData(null, null);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLChangePortStatus));
            ADSLChangePortReasonComboBox.ItemsSource = ADSLChangePortDB.GetADSLChangePortReasonCheckable();
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            _ADSL = ADSLDB.GetADSLByTelephoneNo(TelephoneNo);

            if (_ADSL != null)
                if (_ADSL.ADSLPortID != null)
                {
                    ADSLPortInfo portInfo = ADSLPortDB.GetADSlPortInfoByID((long)_ADSL.ADSLPortID);
                    PortNoTextBox.Text = portInfo.Port;
                    MDFDescriptionTextBox.Text = portInfo.MDFTitle;
                }

            if (_RequsetID == 0)
            {

            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_RequsetID);
                _ADSLChangePort = ADSLChangePortDB.GetADSLChangePortByID(_RequsetID);

                StatusComboBox.SelectedValue = _ADSLChangePort.OldPortStatusID;
                ADSLChangePortReasonComboBox.SelectedValue = _ADSLChangePort.ReasonID;
                CommentTextBox.Text = _ADSLChangePort.Comment;
            }
        }

        #endregion
    }
}
