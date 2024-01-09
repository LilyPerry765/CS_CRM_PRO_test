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
    /// Interaction logic for PBXUserControl.xaml
    /// </summary>
    public partial class PBXUserControl : Local.UserControlBase
    {
        private long _reqeustID = 0;
        private long _telephonNo = 0;
        ObservableCollection<PBXTelephone> PBXs;
        public List<PBXTelephone> OldPBXs { get; set; }
        private Request request { get; set; }
        public PBXUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        public PBXUserControl(long reqeustID, long telephoneNo)
            : this()
        {
            this._reqeustID = reqeustID;
            this._telephonNo = telephoneNo;
        }
        private void Initialize()
        {

        }
        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {

            HeadTelephoneComboBox.ItemsSource = TelephoneComboBoxColumn.ItemsSource = Data.TelephonePBXDB.GetAllCustomerTelephone(_telephonNo);
            
            PBXs = new ObservableCollection<PBXTelephone>();
            PBXs = new ObservableCollection<PBXTelephone>(Data.TelephonePBXDB.GetTelephonePBX(_telephonNo));
            OldPBXs = new List<PBXTelephone>(PBXs.Select(t => (PBXTelephone)t.Clone()).ToList());
            if (_reqeustID != 0)
            {
                request = Data.RequestDB.GetRequestByID(_reqeustID);
                HeadTelephoneComboBox.SelectedValue = PBXs.Take(1).SingleOrDefault().HeadTelephoneNo;
            }
            else
            {
                HeadTelephoneComboBox.SelectedValue = _telephonNo;
            }
            TelephoneDataGrid.ItemsSource = PBXs;

        }

        private void ItemDelete(object sender, RoutedEventArgs e)
        {
            if (TelephoneDataGrid.SelectedItem != null)
                PBXs.Remove(TelephoneDataGrid.SelectedItem as PBXTelephone);
        }
    }
}
