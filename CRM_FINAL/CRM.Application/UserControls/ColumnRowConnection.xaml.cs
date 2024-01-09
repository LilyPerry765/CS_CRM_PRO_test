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
using System.Collections.ObjectModel;
using Enterprise;
using System.ComponentModel;

namespace CRM.Application.UserControls
{
    public partial class ColumnRowConnection : Local.UserControlBase
    {
        #region Properties

        List<Bucht> OldOutBuchtList = new List<Bucht>();
        List<PCMPort> OldPCMPortList = new List<PCMPort>();

        private long _buchtID = 0;
        static int _centerID = 0;
        private int? _buchtType = null;

        // ام دی اف، ای دی انتخاب شده را بر میگرداند
        public int? MDFID
        {
            get { return (int?)MDFComboBox.SelectedValue; }
            set { MDFComboBox.SelectedValue = value; }
        }

        public Bucht Bucht
        {
            get { return (Bucht)GetValue(BuchtProperty); }
            set { SetValue(BuchtProperty, value); }
        }

        public static readonly DependencyProperty BuchtProperty = DependencyProperty.Register("Bucht", typeof(Bucht), typeof(ColumnRowConnection));

        public int CenterID
        {
            get { return _centerID; }
            set { _centerID = value; this.OnPropertyChanged(value); }
        }

        public long BuchtID
        {
            get { return (long)GetValue(BuchtIDProperty); }
            set { SetValue(BuchtIDProperty, value); }
        }

        public static readonly DependencyProperty BuchtIDProperty = DependencyProperty.Register("BuchtID", typeof(long), typeof(ColumnRowConnection), new PropertyMetadata(SetBuchtID));

        public int? BuchtType   
        {
            get { return _buchtType; }
            set { _buchtType = value; }
        }

        /// <summary>
        /// pcm only be install to the buchts without connecting cabinet input
        /// true :  get buchts without connecting cabinet input
        /// false : get buchts free and ADSLFree
        /// </summary>
        public bool BuchtsNoCabinetInput
        {
            get { return (bool)GetValue(BuchtsNoCabinetInputProperty); }
            set { SetValue(BuchtsNoCabinetInputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BuchtsNoCabinetInput.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BuchtsNoCabinetInputProperty = DependencyProperty.Register("BuchtsNoCabinetInput", typeof(bool), typeof(ColumnRowConnection));

        #endregion

        #region Constructors

        public ColumnRowConnection()
        {
            InitializeComponent();

            this.PropertyChanged += new PropertyChangedEventHandler(CenterIDChanged);
        }

        #endregion

        #region Center Properties

        private event PropertyChangedEventHandler _propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { this._propertyChanged += value; }
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

        #region Methods

        public delegate void ConnectionChange();

        public event ConnectionChange DoConnectionChange;

        private void OnDoConnectionChange()
        {
            if (DoConnectionChange != null)
                DoConnectionChange();
        }

        #endregion

        #region Event Handlers

        public void LoadData(object sender, RoutedEventArgs e)
        {
            //if (_IsLoaded) return;
            //else _IsLoaded = true;

            if (_buchtID != 0)
            {
                Bucht = Data.BuchtDB.GetBuchetByID(_buchtID);

                if (Bucht != null)
                {
                    ConnectionInfo connectionInfo = DB.GetConnectionInfoByBuchtID(Bucht.ID);

                    MDFComboBox.SelectedValue = connectionInfo.MDFID;
                    MDFComboBox.Text = connectionInfo.MDF;
                    MDFComboBox_SelectionChanged(null, null);

                    ConnectionColumnComboBox.SelectedValue = connectionInfo.VerticalColumnID;
                    ConnectionColumnComboBox_SelectionChanged(null, null);

                    ConnectionRowComboBox.SelectedValue = connectionInfo.VerticalRowID;
                    ConnectionRowComboBox_SelectionChanged(null, null);

                    ConnectionBuchtComboBox.SelectedValue = connectionInfo.BuchtID;
                   ConnectionBuchtComboBox_SelectionChanged(null, null);
                }
            }
        }

        private void MDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedValue != null)
            {
                ConnectionColumnComboBox.ItemsSource = DB.GetConnectionColumnInfo((int)MDFComboBox.SelectedValue);
                ConnectionColumnComboBox.SelectedIndex = -1;
                ConnectionRowComboBox.SelectedIndex = -1;
                ConnectionBuchtComboBox.SelectedIndex = -1;
            }
        }

        private void ConnectionColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionColumnComboBox.SelectedValue != null)
            {
                ConnectionRowComboBox.ItemsSource = DB.GetConnectionRowInfo((int)ConnectionColumnComboBox.SelectedValue);
                ConnectionRowComboBox.SelectedIndex = -1;
                ConnectionBuchtComboBox.SelectedIndex = -1;
            }
        }

        private void ConnectionRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionRowComboBox.SelectedValue != null)
            {
                if (Bucht != null)
                {
                    ConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)ConnectionRowComboBox.SelectedValue, BuchtsNoCabinetInput, _buchtType)
                                                            .Union(new List<CheckableItem> 
                                                                        { 
                                                                            new CheckableItem 
                                                                            { 
                                                                                LongID = Bucht.ID, 
                                                                                Name = Bucht.BuchtNo.ToString(), 
                                                                                IsChecked = false 
                                                                            } 
                                                                        }
                                                                   );
                }
                else
                {
                    ConnectionBuchtComboBox.ItemsSource = DB.GetConnectionBuchtInfo((int)ConnectionRowComboBox.SelectedValue, BuchtsNoCabinetInput, _buchtType);
                    ConnectionBuchtComboBox.SelectedIndex = -1;
                }
            }
        }

        private void ConnectionBuchtComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionBuchtComboBox.SelectedValue != null)
            {
                BuchtID = (long)ConnectionBuchtComboBox.SelectedValue;
                OnDoConnectionChange();

            }
        }

        static void SetBuchtID(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj != null && obj is ColumnRowConnection)
                (obj as ColumnRowConnection).SetNewValue(e);
        }

        private void SetNewValue(DependencyPropertyChangedEventArgs e)
        {
            _buchtID = (long)e.NewValue;
          //  LoadData(null, null);
        }

        #endregion

        public void Reset()
        {
           MDFComboBox.SelectedIndex = -1;
           ConnectionColumnComboBox.SelectedIndex = -1;
           ConnectionRowComboBox.SelectedIndex = -1;
           ConnectionBuchtComboBox.SelectedIndex = -1;
        }
    }
}
