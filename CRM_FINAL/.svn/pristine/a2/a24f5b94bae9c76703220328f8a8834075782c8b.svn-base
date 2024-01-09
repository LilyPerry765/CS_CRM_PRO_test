using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
    /// Interaction logic for CustomerTelephoneInfoForm.xaml
    /// </summary>
    public partial class CustomerTelephoneInfoForm : Local.PopupWindow
    {
        #region Properties And Fields

        private long _customerID = 0;
        List<TeleInfo> teleInfoList = new List<TeleInfo>();

        #endregion

        #region Constructor
        public CustomerTelephoneInfoForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CustomerTelephoneInfoForm(long customerID)
            : this()
        {
            this._customerID = customerID;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TelephoneNoStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.TelephoneStatus));
        }

        #endregion

        #region EventhHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_customerID != 0)
            {
                WaittingImage.Visibility = Visibility.Visible;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += DoWork;
                worker.RunWorkerCompleted += WorkerCompleted;
                worker.RunWorkerAsync();

            }
            else
            {
                MessageBox.Show("مشترک مشخص نشده است .", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TelephoneDataGrid.DataContext = teleInfoList;

            WaittingImage.Visibility = Visibility.Collapsed;
            TelephonInfo.Visibility = Visibility.Visible;

        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {

            List<Telephone> teleList = Data.TelephoneDB.GetTelephoneByCustomerID(_customerID);
            teleInfoList = Data.TelephoneDB.GetTelephoneInfoByTelePhonNo(teleList).ToList();
            List<CRM.Data.BillingServiceReference.DebtInfo> DebtList = CRM.Application.Codes.ServiceReference.GetDebtInfo(teleList.Select(t => t.TelephoneNo.ToString()).ToList());
            DebtList.ForEach(Item =>
            {
                teleInfoList.Where(t => t.TelephoneNo == Convert.ToInt64(Item.PhoneNo)).SingleOrDefault().Dept = Item.DebtAmount;
                teleInfoList.Where(t => t.TelephoneNo == Convert.ToInt64(Item.PhoneNo)).SingleOrDefault().LastPaidBillDate = Item.LastPaidBillDate;
            }
                            );
        }

        #endregion
    }
}
