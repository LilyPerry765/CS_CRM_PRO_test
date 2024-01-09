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
    /// <summary>
    /// Interaction logic for RequestedChangeLocation.xaml
    /// </summary>
    public partial class RequestedChangeLocation : Local.PopupWindow
    {



        public Customer customer { get; set; }
        public Request request { get; set; }

        public RequestedChangeLocation()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            IEnumerable<Center> centers = DB.GetAllEntity<Center>().Where(c => DB.CurrentUser.CenterIDs.Contains(c.ID)).ToList();
            CentercomboBox.ItemsSource = centers;
            RequestTypecomboBox.ItemsSource = DB.GetAllEntity<RequestType>();
            UserInfo.DataContext = DB.CurrentUser;
            customer = new Customer();
            request = new Request();

        }

        private void RequestTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodetextBox.Text.Trim()))
            {
                if (DB.SearchByPropertyName<Customer>("NationalCodeOrRecordNo", NationalCodetextBox.Text.Trim()).Count > 0)
                    customer = DB.SearchByPropertyName<Customer>("NationalCodeOrRecordNo", NationalCodetextBox.Text.Trim())[0];
                if (customer != null)
                {
                    request.CustomerID = customer.ID;
                    CustomerNametextBox.Text = string.Empty;
                    CustomerNametextBox.Text = customer.FirstNameOrTitle + ' ' + customer.LastName;
                    List<Telephone> teleList = DB.SearchByPropertyName<Telephone>("CustomerID", customer.ID).ToList();
                    List<TeleInfo> teleInfo = Data.TelephoneDB.GetTelephoneInfoByTelePhonNo(teleList);
                    TelephoneDataGrid.DataContext = teleInfo;

                }

                else
                {
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.ShowDialog();
                    if (customerForm.DialogResult ?? false)
                    {

                        customer = Data.CustomerDB.GetCustomerByID( customerForm.ID);
                        request.CustomerID = customer.ID;
                        CustomerNametextBox.Text = string.Empty;
                        CustomerNametextBox.Text = customer.FirstNameOrTitle + ' ' + customer.LastName;
                        List<Telephone> teleList = DB.SearchByPropertyName<Telephone>("CustomerID", customer.ID).ToList();
                        List<TeleInfo> teleInfo = Data.TelephoneDB.GetTelephoneInfoByTelePhonNo(teleList);
                        TelephoneDataGrid.DataContext = teleInfo;

                    }

                }
            }
            else
            {

                CustomerForm customerForm = new CustomerForm();
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {


                    customer = DB.GetEntitybyID<Customer>(customerForm.ID);
                    request.CustomerID = customer.ID;
                    CustomerNametextBox.Text = string.Empty;
                    CustomerNametextBox.Text = customer.FirstNameOrTitle + ' ' + customer.LastName;
                    List<Telephone> teleList = DB.SearchByPropertyName<Telephone>("CustomerID", customer.ID).ToList();
                    List<TeleInfo> teleInfo = Data.TelephoneDB.GetTelephoneInfoByTelePhonNo(teleList);
                    TelephoneDataGrid.DataContext = teleInfo;


                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
