using CRM.Application.Local;
using CRM.Application.Views;
using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.ExtendedSpaceAndPower
{
    /// <summary>
    /// Interaction logic for V3SpaceAndPowerUserControl.xaml
    /// </summary>
    public partial class V3SpaceAndPowerUserControl : UserControlBase
    {
        #region Properties and Fields

        public Customer _Customer { get; set; }

        public Request _Request { get; set; }

        public int CenterID { set; get; }

        #endregion

        public V3SpaceAndPowerUserControl()
        {
            InitializeComponent();
        }

        private void SearchCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            ExtendCheckBox.IsEnabled = false;
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                string nationalCode = NationalCodeTextBox.Text.Trim();
                CustomerNameTextBox.Clear();
                bool isNationalCodeInBlackList = BlackListDB.ExistNationalCodeInBlackList(nationalCode);
                if (isNationalCodeInBlackList)
                {
                    MessageBox.Show(".کد ملی در لیست سیاه قرار دارد", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _Customer = CustomerDB.GetCustomerByNationalCode(nationalCode); 
                    if (_Customer != null)
                    {
                        _Request.CustomerID = _Customer.ID;
                        CustomerNameTextBox.Text = string.Format("{0} {1}", _Customer.FirstNameOrTitle, _Customer.LastName);
                        ExtendCheckBox.IsEnabled = true;
                        EditCustomerButton.IsEnabled = true;
                    }
                    else
                    {
                        CustomerForm customerForm = new CustomerForm();
                        customerForm.CenterID = CenterID;
                        customerForm.ShowDialog();
                        if (customerForm.DialogResult ?? false)
                        {
                            _Customer = CustomerDB.GetCustomerByID(customerForm.ID);
                            _Request.CustomerID = _Customer.ID;
                            CustomerNameTextBox.Text = string.Format("{0} {1}", _Customer.FirstNameOrTitle, _Customer.LastName);
                            NationalCodeTextBox.Text = _Customer.NationalCodeOrRecordNo;
                            ExtendCheckBox.IsEnabled = true;
                            EditCustomerButton.IsEnabled = true;
                        }
                    }
                }
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.CenterID = CenterID;
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    _Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                    _Request.CustomerID = _Customer.ID;
                    CustomerNameTextBox.Clear();
                    CustomerNameTextBox.Text = string.Format("{0} {1}", _Customer.FirstNameOrTitle, _Customer.LastName);
                    NationalCodeTextBox.Text = _Customer.NationalCodeOrRecordNo;
                    ExtendCheckBox.IsEnabled = true;
                    EditCustomerButton.IsEnabled = true;
                }
            }
            if (_Customer != null)
            {
                (Window.GetWindow(this) as CRM.Application.Views.RequestForm).RequesterNametextBox.Text = string.Format("{0} {1}", _Customer.FirstNameOrTitle, _Customer.LastName);
            }
        }

        private void EditCustomerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteSpaceAndPowerMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditSpaceAndPowerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (SpaceAndPowersDataGrid.SelectedIndex >= 0)
            {
                //SpaceAndPowerInfo item = SpaceAndPowersDataGrid.SelectedItem as SpaceAndPowerInfo;
                //if (item == null)
                //{
                //    return;
                //}
                //
                //V3SpaceAndPowerForm window = new V3SpaceAndPowerForm(item.ID);
                //window.ShowDialog();
                //
                //if (window.DialogResult.HasValue && window.DialogResult.Value)
                //{
                //    //TODO: Referesh SpaceAndPowersDataGrid
                //}
            }
        }

        private void AddSpaceAndPowerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            V3SpaceAndPowerForm window = new V3SpaceAndPowerForm();
            window.ShowDialog();

            if (window.DialogResult.HasValue && window.DialogResult.Value)
            {
                //TODO: Referesh SpaceAndPowersDataGrid
            }
        }

        private void SearchAddressButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditAddressButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExtendCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ExtendCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void PreviousSpaceAndPowerRequestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PreviousSpaceAndPowerRequestsDataGrid_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {

        }
    }
}
