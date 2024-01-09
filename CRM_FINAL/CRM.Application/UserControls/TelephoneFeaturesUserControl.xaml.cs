using CRM.Data;
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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for TelephoneFeaturesUserControl.xaml
    /// </summary>
    public partial class TelephoneFeaturesUserControl : Local.UserControlBase
    {

        #region Constructor && Initialize

        long _telephoneNo = 0;
        Telephone _telephone { get; set; }
        List<CheckableItem> specialServiceTypeListAll { get; set; }
        public bool isExpanded { get; set; }

        public TelephoneFeaturesUserControl()
        {
            InitializeComponent();
        }
        public TelephoneFeaturesUserControl(long telephoneNo)
            : this()
        {
            this._telephoneNo = telephoneNo;
            Initialize();


        }

        private void Initialize()
        {
            TelephoneStatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneStatus));

        }
        #endregion

        #region Load
        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (_IsLoaded) return;
            else _IsLoaded = true;
            _telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_telephoneNo);
            specialServiceTypeListAll = Data.TelephoneSpecialServiceTypeDB.GetSpecialServicesOfTelephone(_telephoneNo);
            LimitationsGroupBox.DataContext = _telephone;
            SpecialServiceListView.ItemsSource = specialServiceTypeListAll;



        }
        #endregion

        #region Method

        #endregion
    }
}
