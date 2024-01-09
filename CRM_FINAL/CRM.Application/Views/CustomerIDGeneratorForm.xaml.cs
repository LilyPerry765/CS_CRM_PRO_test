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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for CustomerIDGeneratorForm.xaml
    /// </summary>
    public partial class CustomerIDGeneratorForm :  Local.PopupWindow
    {
        public CustomerIDGeneratorForm()
        {
            InitializeComponent();
            Initialize();
        }

        public int CenterID
        {
            get;
            set;
        }
        public int CustomerTypeID
        {
            get;
            set;
        }

        private void Initialize()
        {
           CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
           CustomerType.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
        }

        private void CityComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if(CityComboBox.SelectedValue != null)
               CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId((int)CityComboBox.SelectedValue);
        }

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                CenterID = (int)CenterComboBox.SelectedValue;
            }
        }

        private void CustomerType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerType.SelectedValue != null)
            {
                CustomerTypeID = (int)CustomerType.SelectedValue;
            }
        }
    }
}
