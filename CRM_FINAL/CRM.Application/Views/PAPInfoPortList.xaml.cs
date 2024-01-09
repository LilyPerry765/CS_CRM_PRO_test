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
    public partial class PAPInfoPortList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public PAPInfoPortList()
        {
            //ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            //popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            //base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PAPInfoComboBox.ItemsSource = PAPInfoDB.GetPAPInfoCheckable();
            CentersComboBox.ItemsSource = CenterDB.GetCenterCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPAPPortStatus));
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            PAPInfoComboBox.Reset();
            CentersComboBox.Reset();
            TelephoneNoTextBox.Text = string.Empty;
            InputPortTextBox.Text = string.Empty;
            OutputPortTextBox.Text = string.Empty;
            FromDateDate.SelectedDate = null;
            ToDateDate.SelectedDate = null;
            StatusComboBox.Reset();

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

           // Pager.TotalRecords = Data.PAPInfoPortDB.SearchPAPPortsCount(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs,InputPortTextBox.Text,OutputPortTextBox.Text, telephoneNo,FromDateDate.SelectedDate, ToDateDate.SelectedDate,StatusComboBox.SelectedIDs);
           // ItemsDataGrid.ItemsSource = Data.PAPInfoPortDB.SearchPAPPorts(PAPInfoComboBox.SelectedIDs, CentersComboBox.SelectedIDs, InputPortTextBox.Text, OutputPortTextBox.Text, telephoneNo, FromDateDate.SelectedDate, ToDateDate.SelectedDate, StatusComboBox.SelectedIDs, startRowIndex, pageSize);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
