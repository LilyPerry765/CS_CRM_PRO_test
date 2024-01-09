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
    /// Interaction logic for MDFUserControl.xaml
    /// </summary>
    public partial class MDFUserControl : Local.UserControlBase
    {
        private int _buchtType = 0;
        private long _buchtID = 0;
        public int CenterID { get; set; }
        public Bucht Bucht { get; set; }

        public long BuchtID { get; set; }

        public MDFUserControl()
        {
            InitializeComponent();
            Initialize();
        }
      public MDFUserControl(int buchtType)
            : this()
        {
            this._buchtType = buchtType;
        }
      public MDFUserControl(long buchtID)
            : this()
        {
            this._buchtID = buchtID;
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

            MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(CenterID);
            if (_buchtID != 0)
            {
                ConnectionInfo connectionInfo = DB.GetConnectionInfoByBuchtID(_buchtID);
                Bucht = Data.BuchtDB.GetBuchetByID(_buchtID);
                _buchtType = connectionInfo.BuchtTypeID;

                MDFComboBox.SelectedValue = connectionInfo.MDFID;
                MDFComboBox_SelectionChanged(null, null);

                ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                ConnectionColumnComboBox_SelectionChanged(null, null);

                ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                ConnectionRowComboBox_SelectionChanged(null, null);

                ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
                ConnectionBuchtComboBox_SelectionChanged(null, null);

            }
        }

        private void MDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedValue != null)
                ConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)MDFComboBox.SelectedValue);
        }

        private void ConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionColumnComboBox.SelectedValue != null)
                ConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)ConnectionColumnComboBox.SelectedValue);
        }

        private void ConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionRowComboBox.SelectedValue != null)
            {
                if (Bucht != null)
                    ConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)ConnectionRowComboBox.SelectedValue, true, _buchtType).Union(new List<CheckableItem> { new CheckableItem { LongID = Bucht.ID, Name = Bucht.BuchtNo.ToString(), IsChecked = false } });
                else
                    ConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)ConnectionRowComboBox.SelectedValue, true, _buchtType);
            }
        }

        private void ConnectionBuchtComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionBuchtComboBox.SelectedValue != null)
            {
                BuchtID = (long) ConnectionBuchtComboBox.SelectedValue;
            }
            else
            {
                BuchtID = 0;
            }
            
        }

        
    }
}
