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
using CRM.Application.Local;

namespace CRM.Application.UserControls
{
    public partial class CustomerInfoSummary : UserControlBase
    {
        public  Customer _customer { get; set; }

        public static string CustomerFullName { get; set; }

        public byte? Gender { get; set; }

        public string NationalCodeOrRecordNo { get; set; }

        public bool PersonType { get; set; }

        public string TelNo { get; set; }
        

        bool mode = false;

        bool isExpanded = false;

        public bool Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        public bool IsExpandedProperty
        {
            get
            {
                return isExpanded;
            }
            set
            {
                isExpanded = value;
            }
        }

        public CustomerInfoSummary()
        {
            InitializeComponent();
           
        }
       
        public CustomerInfoSummary(long? id) : this()  
        {
            _customer = Data.CustomerDB.GetCustomerByID(id ?? 0 );

            if (_customer != null)
                CustomerFullName = _customer.FirstNameOrTitle + ' ' + _customer.LastName ?? string.Empty;
            else
                CustomerFullName = string.Empty;

            LoadData1();
        }
      
        private void LoadData(object sender, RoutedEventArgs e)
        {
            if (_IsLoaded) return;
            else _IsLoaded = true;
           CustomerInfoExpander.IsExpanded = IsExpandedProperty;
            this.DataContext = new { Customer = _customer, CustomerName = CustomerFullName };
           //if (mode == false)
           //{
               
           //     PersonTypeLable.Visibility = Visibility.Collapsed;
           //     PersonTypeListBox.Visibility = Visibility.Collapsed;
           //     NationalCodeLabel.Visibility = Visibility.Collapsed;
           //     NationalCodetextBox.Visibility = Visibility.Collapsed;
           //     GenderLabel.Visibility = Visibility.Collapsed;
           //     GenderListBox.Visibility = Visibility.Collapsed;
           //     TeleLabel.Visibility = Visibility.Collapsed;
           //     TeletextBox.Visibility = Visibility.Collapsed;

           //}
          
        }
        private void LoadData1()
        {
            CustomerInfoExpander.IsExpanded = IsExpandedProperty;
            this.DataContext = new { Customer = _customer, CustomerName = CustomerFullName };
        }
    
    }
}
