using CRM.Application.Local;
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
    /// Interaction logic for InstallmentRegistrationForm.xaml
    /// </summary>
    public partial class InstallmentRegistrationForm : PopupWindow
    {

        #region Properties and Fields

        public long RequestId { get; set; }
        public long RequestPaymentId { get; set; }
        public RequestPayment PrepaymentRequestPayment { get; set; }
        public TelephoneConnectionInstallment CurrentConnectionInstallment { get; set; }
        public long CurrentConnectionInstallmentID { get; set; }

        #endregion

        #region Constructors

        public InstallmentRegistrationForm()
        {
            InitializeComponent();
        }

        public InstallmentRegistrationForm(long requestPaymentId, long requestId, RequestPayment prepaymentRequestPayment, long currentConnectionInstallmentID)
            : this()
        {
            this.RequestId = requestId;
            this.RequestPaymentId = requestPaymentId;
            this.PrepaymentRequestPayment = prepaymentRequestPayment;
            this.CurrentConnectionInstallmentID = currentConnectionInstallmentID;
        }

        #endregion

        #region EventHandlers

        private void RequestDetailsLoadButton_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(RequestIdNumericTextBox.Text.Trim())) //حتماً باید شناسه درخواست در ابتدا مشخص گردد
            //{
            //    MessageBox.Show(".شناسه درخواست دایری مربوطه را وارد نمائید", "", MessageBoxButton.OK, MessageBoxImage.Information);
            //    RequestIdNumericTextBox.Focus();
            //    return;
            //}
            //
        }

        private void SaveInstallmentsButton_Click(object sender, RoutedEventArgs e)
        {
            InstallRequestShortInfo installRequestInformation = InstallRequestInformationGroupBox.DataContext as InstallRequestShortInfo;
            if (installRequestInformation.MethodOfPaymentForTelephoneConnection.HasValue && installRequestInformation.MethodOfPaymentForTelephoneConnection.Value != (byte)DB.MethodOfPaymentForTelephoneConnection.Installment)
            {
                MessageBox.Show(".امکان ایجاد قسط برای این درخواست وجود ندارد", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(InstallmentsCountNumericTextBox.Text.Trim()))
            {
                MessageBox.Show(".تعیین 'تعداد اقساط' الزامی است", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                InstallmentsCountNumericTextBox.Focus();
                return;
            }
            else if (Convert.ToInt32(InstallmentsCountNumericTextBox.Text.Trim()) > 36)
            {
                MessageBox.Show(".بازه معتبر برای تعداد اقساط' 1 الی 36 ' میباشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                InstallmentsCountNumericTextBox.Focus();
                return;
            }

            if (CurrentConnectionInstallmentID == 0)
            {
                TelephoneConnectionInstallment telephoneConnectionInstallment = new TelephoneConnectionInstallment();
                telephoneConnectionInstallment.InstallmentsCount = Convert.ToInt32(InstallmentsCountNumericTextBox.Text.Trim());
                telephoneConnectionInstallment.RequestPaymentID = this.RequestPaymentId;
                telephoneConnectionInstallment.UserID = DB.CurrentUser.ID;
                telephoneConnectionInstallment.InsertDate = DB.GetServerDate();
                telephoneConnectionInstallment.Detach();
                DB.Save(telephoneConnectionInstallment, true);
                this.DialogResult = true;
            }
            else
            {
                this.CurrentConnectionInstallment.InstallmentsCount = Convert.ToInt32(InstallmentsCountNumericTextBox.Text.Trim());
                this.CurrentConnectionInstallment.RequestPaymentID = this.RequestPaymentId;
                this.CurrentConnectionInstallment.UserID = DB.CurrentUser.ID;
                this.CurrentConnectionInstallment.InsertDate = DB.GetServerDate();
                this.CurrentConnectionInstallment.Detach();
                DB.Save(this.CurrentConnectionInstallment, false);
                this.DialogResult = true;
            }
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadData();
        }

        #endregion


        #region Methods

        private void LoadData()
        {
            if (RequestInformationGroupBox.DataContext != null) //اگر دیتایی از قبل وجود داشته باشد ، باید کلیر شود
            {
                RequestInformationGroupBox.DataContext = new RequestShortInfo();
                CustomerInformationGroupBox.DataContext = new CustomerShortInfo();
                InstallRequestInformationGroupBox.DataContext = new InstallRequestShortInfo();
            }

            //بارگذاری درخواست مربوطه
            RequestShortInfo requestInformation = RequestDB.GetRequestShortInfoById(this.RequestId);

            if (requestInformation == null)
            {
                MessageBox.Show(".رکوردی یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (requestInformation != null && requestInformation.RequestTypeId != (int)DB.RequestType.Dayri) //برای تقسیط حتماً باید درخواست دایری باشد
            {
                MessageBox.Show(".فقط شناسه درخواست های دایری را وارد نمائید", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            RequestInformationGroupBox.DataContext = requestInformation;

            //بارگذاری اطلاعات مشترک درخواست کننده
            CustomerShortInfo customerInformation = CustomerDB.GetCustomerShortInfoById(requestInformation.CustomerID.Value);
            CustomerInformationGroupBox.DataContext = customerInformation;

            //بارگذاری اطلاعات دایری مربوطه
            InstallRequestShortInfo installRequestInformation = InstallRequestDB.GetInstallShortInfoByRequestId(requestInformation.ID.Value);
            InstallRequestInformationGroupBox.DataContext = installRequestInformation;

            PrePaymentAmountTextBox.Text = string.Format("{0} ریال", this.PrepaymentRequestPayment.AmountSum.ToString());

            if (this.CurrentConnectionInstallmentID != 0)
            {
                this.CurrentConnectionInstallment = TelephoneConnectionInstallmentDB.GetTelephoneConnectionInstallmentById(this.CurrentConnectionInstallmentID);
                if (this.CurrentConnectionInstallment != null)
                {
                    InstallmentsCountNumericTextBox.Text = this.CurrentConnectionInstallment.InstallmentsCount.ToString();
                    SaveInstallmentsButton.Content = "بروزرسانی";
                }
            }
        }

        #endregion

    }
}
