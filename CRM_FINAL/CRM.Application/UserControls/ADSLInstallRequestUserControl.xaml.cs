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
    /// Interaction logic for ADSLInstallRequest.xaml
    /// </summary>
    public partial class ADSLInstallRequestUserControl : UserControl
    {
        #region Properties

        private long _RequsetID = 0;

        private CRM.Data.ADSLInstallRequest _ADSLInstalRequest { get; set; }
        private Request _Request { get; set; }
        public long TelephoneNo { get; set; }

        #endregion

        public ADSLInstallRequestUserControl()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public ADSLInstallRequestUserControl(long requestID, long telephoneNo)
            : this()
        {
            _RequsetID = requestID;
            TelephoneNo = telephoneNo;
        }

        private void Initialize()
        {
            //throw new NotImplementedException();
        }

        public ADSLInstallRequestUserControl(long requestID, long customerID, long telephoneNo)
            : this()
        {
            _RequsetID = requestID;
            TelephoneNo = telephoneNo;

            //LoadData(null, null);
        }
    }
}
