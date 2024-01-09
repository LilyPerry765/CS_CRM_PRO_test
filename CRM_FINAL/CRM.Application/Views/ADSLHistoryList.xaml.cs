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
using Enterprise;
using CRM.Data;


namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ADSLHistoryList.xaml
    /// </summary>
    public partial class ADSLHistoryList : Local.TabWindow
    {
        #region Properties

        #endregion Properties
        #region constructor

        public ADSLHistoryList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Methods

        private void Initialize()
        {
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckable();
            ServiceColumn.ItemsSource = ADSLServiceDB.GetADSLServiceCheckable();
        }

        private void LoadData()
        {
            ADSLHistoryListDataGrid.ItemsSource = ADSLHistoryDB.SearchAll();
        }

        #endregion Mathods

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
           // long TelNos = !string.IsNullOrWhiteSpace(TelNoTextBox.Text) ? Convert.ToInt64(TelNoTextBox.Text) : -1;
            List<ADSLHistoryInfo> Result= Data.ADSLHistoryDB.SearchCustomersHistory(ServiceComboBox.SelectedIDs,
                                                      TelNoTextBox.Text,
                                                      StartDate.SelectedDate);
            //foreach(ADSLHistoryInfo adslhistoryinfo in Result)
            //{
            //    adslhistoryinfo.InsertPersianDate = (adslhistoryinfo.InsertDate.HasValue) ? Helper.GetPersianDate(adslhistoryinfo.InsertDate, Helper.DateStringType.Short) : "";
            //}

            ADSLHistoryListDataGrid.ItemsSource = Result;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            TelNoTextBox.Text = string.Empty;
            ServiceComboBox.SelectedIndex = -1;
            StartDate.SelectedDate = null;
            LoadData();
          
        }

        #endregion EventHandlers
    }
}
