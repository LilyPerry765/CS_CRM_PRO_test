using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace Test.CRM.ServiceHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ////DateTime fromDateTime = Convert.ToDateTime("9-12-2015 00:00:00");
                ////DateTime toDateTime = Convert.ToDateTime("9-13-2015 00:00:00");

                //string errorMessages = string.Empty;
                //bool error = false;

                ////BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                ////EndpointAddress endpointAddress = new EndpointAddress("http://78.39.252.109:83/CRM.ServiceHost.CRMService.svc");
                ////basicHttpBinding.MaxBufferSize = 2147483647;
                ////basicHttpBinding.MaxReceivedMessageSize = 2147483647;

                ////using (ServiceReference1.CRMServiceClient CRMServiceClient = new ServiceReference1.CRMServiceClient(basicHttpBinding, endpointAddress))
                ////{
                ////    var result = CRMServiceClient.ChangeTelephoneInfo(out error, out errorMessages, "PendarPajouh", "Kermanshah@srv", fromDateTime, toDateTime, new List<int> { }, new List<int> { });

                ////   // ServiceReference1.ServiceHostCustomClassTelephoneInfo result2 = CRMServiceClient.TelephoneInfo(out error, out errorMessages, "Billing", "sNQC4SsKjDXBTdwtGyCENkeMXNYrI5nimE2Podf/dvS9KrO0eat7/6P/tYqUEeBXJupMnDv1Tr8YekC86yonRg==", 8338265485);

                ////    // result = CRMServiceClient.GetChangeTelephone(out error, out errorMessages, "Pendar", "Pajouh", fromDateTime, toDateTime, centerCode, RequestTypeComboBox.SelectedIDs);
                ////    CRMServiceClient.Close();
                ////}
                ////}

                //ServiceReference1.CRMServiceClient serviceProxy = new ServiceReference1.CRMServiceClient();
                ////serviceProxy.ClientCredentials.UserName.UserName = "billing";
                ////serviceProxy.ClientCredentials.UserName.Password = "kermanshah@billing";
                ////List<ServiceReference1.ServiceHostCustomClassTelephoneGroupType> telephoneInfo =  
                //datagrid.ItemsSource = serviceProxy.ChangeTelephoneInfo(out error, out errorMessages, Convert.ToDateTime("11/5/2015"), Convert.ToDateTime("12/5/2015"), new List<int> { }, new List<int> { }).OrderBy(t => t.NewTelephoneNo).ToList();
                ////datagrid.ItemsSource = serviceProxy.RequestTypeInfo(out error, out errorMessages);
                ////Console.WriteLine(telephoneInfo.TechType);
                ////Console.ReadLine();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    ServiceReference2.CRMServiceClient client = new ServiceReference2.CRMServiceClient();
            //    List<int> centersCode = new List<int> { 5 };
            //    List<int> requestTypes = new List<int> { 99 };
            //    bool hasError = false;
            //    string errorMessage = string.Empty;
            //    List<ServiceReference2.ServiceHostCustomClassChangeTelephone> result = client.ChangeTelephoneInfo(out hasError, out errorMessage, FromDatePicker.SelectedDate.Value, ToDatePicker.SelectedDate.Value, centersCode, requestTypes);
            //    ItemsDataGrid.ItemsSource = result;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
