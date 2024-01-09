using CRM.Application.Local;
using CRM.Data;
using CRM.Data.ServiceHost;
using Enterprise;
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

namespace CRM.Application.TemporaryListForPayments
{
    /// <summary>
    /// Interaction logic for TelephoneConnectionCashList.xaml
    /// </summary>
    public partial class TelephoneConnectionCashList : TabWindow
    {

        #region Constructor

        public TelephoneConnectionCashList()
        {
            InitializeComponent();
            Initiaize();
        } 

        #endregion

        #region Methods

        private void Initiaize()
        {
            CitiesCheckableComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        #endregion

        #region EventHandlers

        private void CitiesCheckableComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CentersCheckableComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CitiesCheckableComboBox.SelectedIDs);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemsDataGrid.ItemsSource = ServiceHostDB.GetCashPaymentInfo(FromDatePicker.SelectedDate.Value, ToDatePicker.SelectedDate.Value, CentersCheckableComboBox.SelectedIDs);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            UIElement container = SearchExpander as UIElement;
            Helper.ResetSearch(container);
        } 

        #endregion

    }
}
