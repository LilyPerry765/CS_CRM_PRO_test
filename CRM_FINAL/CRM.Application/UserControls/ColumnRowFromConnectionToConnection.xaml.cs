using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ColumnRowFromConnectionToConnection.xaml
    /// </summary>
    public partial class ColumnRowFromConnectionToConnection : Local.UserControlBase
    {
        public ColumnRowFromConnectionToConnection()
        {
            InitializeComponent();
            this.PropertyChanged +=new PropertyChangedEventHandler(CenterIDChanged);
        }

        #region Fildes && Propertise
        static int _centerID = 0;
        static long _fromBuchtID = 0;
        static long _toBuchtID = 0;

        public int CenterID
        {
            get { return _centerID; }
            set { _centerID = value; this.OnPropertyChanged(value); }
        }

    
        public long FromBuchtID
        {
            get { return _fromBuchtID; }
        }

        public long ToBuchtID
        {
            get { return _toBuchtID; }
        }
        #endregion

        #region Events


        #endregion

        #region CenterProperties
        private event PropertyChangedEventHandler _propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add{this._propertyChanged += value;}
            remove { this._propertyChanged -= value; }
        }

        protected virtual void OnPropertyChanged(int propertyValue)
        {
            if (this._propertyChanged != null)
                this._propertyChanged(this, new PropertyChangedEventArgs(propertyValue.ToString()));
    
        }

        private void CenterIDChanged(object sender, PropertyChangedEventArgs e)
        {
            MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID(Convert.ToInt32(e.PropertyName));
        }
        #endregion

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
                ToConnectionBuchtComboBox.ItemsSource = FromConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfoByUsesType((int)ConnectionRowComboBox.SelectedValue, (byte)DB.RequiredConnection.UnConnectToCable);
            }
        }

        private void FromConnectionBuchtComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromConnectionBuchtComboBox.SelectedValue != null)
            {
                _fromBuchtID = (long)FromConnectionBuchtComboBox.SelectedValue;
            }

        }

        private void ToConnectionBuchtComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToConnectionBuchtComboBox.SelectedValue != null)
            {
                _toBuchtID = (long)ToConnectionBuchtComboBox.SelectedValue;
            }
        }

    }
}
