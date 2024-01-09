using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
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
    public partial class CustomerToApproveDebtorForm : Local.RequestFormBase
    {

        private long _customerID = 0;
       // WebReference.PhoneStatusService ds = new WebReference.PhoneStatusService();
        List<TeleInfo> teleInfoList = new List<TeleInfo>();

        public CustomerToApproveDebtorForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }

        public CustomerToApproveDebtorForm(long requestID):this()
        {
            InitializeComponent();
            base.RequestID = requestID;
            this._customerID = Data.RequestDB.GetRequestByID(requestID).CustomerID ?? 0;
        }

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
                Folder.MessageBox.ShowInfo("مشترک یافت نشد.");
            }
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TelephoneDataGrid.DataContext = teleInfoList;

            WaittingImage.Visibility = Visibility.Collapsed;
            TelephonInfo.Visibility = Visibility.Visible;

            this.ResizeWindow();

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
        public override bool Deny()
        {
            IsRejectSuccess = true;
            return IsRejectSuccess;
        }

        public override bool Forward()
        {
        
            IsForwardSuccess = true;
            return IsForwardSuccess;
        }
    }
}
