using CRM.Data;
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
    /// Interaction logic for InstallmentRequestPaymentChequeForm.xaml
    /// </summary>
    public partial class InstallmentRequestPaymentChequeForm : Local.PopupWindow
    {

        private long _insatallmentRequestPaymentID = 0;
        private InstallmentRequestPayment _insatallmentRequestPayment { get; set; }
        public InstallmentRequestPaymentChequeForm()
        {
            InitializeComponent();
         
        }
        public InstallmentRequestPaymentChequeForm(long installmentRequestPaymentID ):this()
        {
            _insatallmentRequestPaymentID = installmentRequestPaymentID;
            Initialize();
        }
        private void Initialize()
        {
           
        }
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _insatallmentRequestPayment = Data.InstallmentRequestPaymentDB.GetInstallmentRequestPaymentByID(_insatallmentRequestPaymentID);
            this.DataContext = _insatallmentRequestPayment;
        }


        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                InstallmentRequestPayment item = this.DataContext as InstallmentRequestPayment;

                if (Data.InstallmentRequestPaymentDB.ChechExistEndDate(item))
                {
                    throw new Exception("این تاریخ قبلا ثبت شده است");
                }
                item.Detach();
                DB.Save(item, false);
                this.Close();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
        }


    }
}
