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
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class InstallmentRequestPaymentCorrectionForm : Local.RequestFormBase
    {
        #region Properties

        private long _TelephoneNo = 0;
        private long _SumOldInstalment = 0;
        private List<InstallmentRequestPayment> oldInstallment;
        private List<InstallmentRequestPayment> newInstallment;

        #endregion

        #region Constructors

        public InstallmentRequestPaymentCorrectionForm()
        {
            InitializeComponent();
            Initialize();
        }

        public InstallmentRequestPaymentCorrectionForm(long telephoneNo)
            : this()
        {
            _TelephoneNo = telephoneNo;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny };
        }

        private void LoadData()
        {
            _SumOldInstalment = Convert.ToInt64(InstallmentRequestPaymentDB.GetSumRemainInstallmenttByTelephoneNo(_TelephoneNo));

            OldSumInstalmentTextBox.Text = _SumOldInstalment + " ریال";
            oldInstallment = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentRemainByTelephoneNo(_TelephoneNo);

            ItemsDataGrid.ItemsSource = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentInfoRemainByTelephoneNo(_TelephoneNo);
        }

        public List<InstallmentRequestPayment> GenerateInstalments(int instalmentCount, long amount)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int _floorValue = 1000;
                List<InstallmentRequestPayment> installmentRequestPayments = new List<InstallmentRequestPayment>();
                int PaymentAmountEachPart = 0;

                string startDate = "";
                if (oldInstallment != null)
                    startDate = oldInstallment.FirstOrDefault().StartDate.ToPersian(Date.DateStringType.Short);
                else
                    startDate = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                string endateCount = Helper.AddMonthToPersianDate(startDate, instalmentCount);
                string endDate = Helper.AddMonthToPersianDate(startDate, instalmentCount);
                string startDateEachPart = startDate;

                PaymentAmountEachPart = (int)(amount / (decimal)instalmentCount);

                for (int i = 1; i <= instalmentCount; i++)
                {
                    InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                    int dateEachPart = 1;
                    installmentRequestPayment.RequestPaymentID = 0;
                    installmentRequestPayment.TelephoneNo = _TelephoneNo;
                    installmentRequestPayment.IsCheque = false;
                    installmentRequestPayment.IsPaid = false;
                    installmentRequestPayment.IsDeleted = false;

                    installmentRequestPayment.StartDate = Helper.PersianToGregorian(startDateEachPart).Value.Date;
                    string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

                    installmentRequestPayment.EndDate = Helper.PersianToGregorian(endDateEachPart).Value.Date;

                    if (instalmentCount == i)
                        installmentRequestPayment.Cost = (long)(amount - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                    else
                        installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                    startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);

                    installmentRequestPayments.Add(installmentRequestPayment);
                }

                return installmentRequestPayments;
            }
        }

        public override bool Confirm()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewSumInstalmentTextBox.Text))
                    throw new Exception("لطفا جمع اقساط را وارد نمایید !");

                if (string.IsNullOrWhiteSpace(NewMonthTextBox.Text))
                    throw new Exception("لطفا تعداد اقساط را وارد نمایید !");

                if (newInstallment == null)
                    throw new Exception("اقساط جدید ایجاد نشده است !");

                InstallmentRequestPaymentCorrection correction = new InstallmentRequestPaymentCorrection();

                correction.TelephoneNo = _TelephoneNo;
                correction.OldSumInstalmentCost = _SumOldInstalment;
                correction.NewSumInstalmentCost = Convert.ToInt64(NewSumInstalmentTextBox.Text);
                if (oldInstallment != null && oldInstallment.Count != 0)
                    correction.OldMonth = oldInstallment.Count;
                else
                    correction.OldMonth = 0;
                correction.NewMonth = newInstallment.Count;
                correction.UserID = DB.CurrentUser.ID;

                correction.Detach();
                DB.Save(correction);

                if (oldInstallment != null)
                {
                    foreach (InstallmentRequestPayment currentOldInstalment in oldInstallment)
                    {
                        currentOldInstalment.IsDeleted = true;

                        currentOldInstalment.Detach();
                        Save(currentOldInstalment);
                    }
                }

                if (newInstallment != null)
                {
                    foreach (InstallmentRequestPayment currentNewInstalment in newInstallment)
                    {
                        currentNewInstalment.RequestPaymentID = null;

                        currentNewInstalment.Detach();
                        Save(currentNewInstalment);
                    }
                }

                IsConfirmSuccess = false;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            this.Close();
            return IsConfirmSuccess;
        }

        public override bool Deny()
        {
            this.Close();

            return IsRejectSuccess;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void CorrectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewSumInstalmentTextBox.Text))
                    throw new Exception("لطفا جمع اقساط را وارد نمایید !");

                if (string.IsNullOrWhiteSpace(NewMonthTextBox.Text))
                    throw new Exception("لطفا تعداد اقساط را وارد نمایید !");

                long newSumInstalment = Convert.ToInt64(NewSumInstalmentTextBox.Text);
                newInstallment = GenerateInstalments(Convert.ToInt32(NewMonthTextBox.Text), newSumInstalment);

                List<InstalmentRequestPaymentInfo> instalmentInfo = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentInfo(newInstallment);

                ItemsDataGrid.ItemsSource = null;
                ItemsDataGrid.ItemsSource = instalmentInfo;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion
    }
}
