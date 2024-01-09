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
using System.Windows.Shapes;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class ADSLIPHistoryForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;
        private int _Type = 0;

        #endregion

        #region Constructors

        public ADSLIPHistoryForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLIPHistoryForm(long id, int type)
            : this()
        {
            _ID = id;
            _Type = type;
        }

        #endregion

        #region Methods

        private void Initialize()
        {            
        }

        private void LoadData()
        {
            if (_Type == (byte)DB.ADSLIPType.Group)
            {
                BlockCountLabel.Visibility = Visibility.Visible;
                BlockCountTextBox.Visibility = Visibility.Visible;
                VirtulaRangeLabel.Visibility = Visibility.Visible;
                VirtulaRangeTextBox.Visibility = Visibility.Visible;                
            }

            ADSLIPHistoryInfo aDSLIP = new ADSLIPHistoryInfo();

            if (_ID != 0)
            {
                aDSLIP = Data.ADSLIPDB.GetADSLIPforHistoryById(_ID, _Type);
            }

            this.DataContext = aDSLIP;
            ItemsDataGrid.ItemsSource = ADSLIPDB.GetADSLIPHistoryListByID(_ID, _Type);
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion
    }
}
