using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
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
using CRM.Application.Views;

namespace CRM.Application.UserControls
{
    public partial class ChangeName : Local.UserControlBase
    {
        #region Properties

        private long _ReqID = 0;
        private long _CustomerID = 0;
        public Customer Customer { get; set; }
        public long TelephoneNo { get; set; }
        private Data.ChangeName _ChangeName { get; set; }
        private Request _Request { get; set; }
        private Inquiry _Inquiry { get; set; }
        public Customer NewCustomer { get; set; }
        

        #endregion

        #region Costructors

        public ChangeName()
        {
            InitializeComponent();
            Initialize();
        }

        public ChangeName(long requestID, long customerID, long telephoneNo)
            : this()
        {
            _ReqID = requestID;
            _CustomerID = customerID;
            TelephoneNo = telephoneNo;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            NewCustomer = new Customer();
            LastCycleNoComnboBox.ItemsSource = Data.CycleDB.GetCycleByCheckable();
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            if (_IsLoaded)
                return;
            else
                _IsLoaded = true;

            if (_ReqID == 0)
            {
                _ChangeName = new CRM.Data.ChangeName();
                _Request = new Request();
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_ReqID);
                _ChangeName = Data.ChangeNameDB.GetChangeNameByID(_ReqID);

                _ChangeName = Data.ChangeNameDB.GetChangeNameByID(_ReqID);

                if (_ChangeName.HasCourtLetter)
                {
                    CourtCheckBox.IsChecked = true;
                    ChangeNameInfoGrid.DataContext = _ChangeName;
                }

                NewCustomer = Data.CustomerDB.GetCustomerByID(_ChangeName.NewCustomerID);
                NewCustomerInfo.DataContext = NewCustomer;
                CustomerNameTextBox.Text = NewCustomer.FirstNameOrTitle + " " + NewCustomer.LastName;
                LastCycleNoComnboBox.SelectedValue = _ChangeName.LastCyleID;
                LastBillDate.SelectedDate = _ChangeName.LastBillDate;

                if (_Request.PreviousAction == (byte)DB.Action.Reject)
                {
                    Inquiry inquiry = Data.InquiryDB.GetInquiryByRequestID( _ReqID);
                    InquiryGroupBox.Visibility = Visibility.Visible;
                    CounterNoTextBox.Text = inquiry.CounterNo;
                    DebtTextBox.Text = inquiry.Debt;
                    CommentTextBox.Text = inquiry.Commnet;
                }
            }

            if (TelephoneNo != 0)
            {
                List<CRM.Data.BillingServiceReference.DebtInfo> debtInfo = Data.BillingServiceDB.GetDebtInfo(new List<string> { TelephoneNo.ToString() });
                if (debtInfo.Count >= 1)
                {
                    LastBillDate.SelectedDate = debtInfo.Take(1).SingleOrDefault().LastPaidBillDate;
                    if (debtInfo.Take(1).SingleOrDefault().LastPaidBillDate != null)
                    {
                        Cycle cycle = Data.CycleDB.GetCycleByDate((DateTime)debtInfo.Take(1).SingleOrDefault().LastPaidBillDate);
                        if (cycle != null)
                        {
                            LastCycleNoComnboBox.SelectedValue = cycle.ID;
                        }
                    }

                }

            }





            //Cycle cycle = new Cycle();
            //// for not Hamper error in get current cycle in load form, The Try was considered to be
            //try
            //{
            //    cycle = Data.CycleDB.GetDateCurrentCycle();
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Contains("Sequence contains more than one element"))
            //        Folder.MessageBox.ShowInfo("با تاریخ فعلی چند دوره یافت شد. سوابق تلفن در دوره جاری قابل در یافت نیست لطفا اطلاعات دوره ها را اصلاح کنید.");
            //}

            //if (cycle != null)
            //{
            //    LastCycleNoComnboBox.SelectedValue = cycle.ID;
            //}
        }

        //private void searchButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(NationalCodetextBox.Text.Trim()))
        //    {
              
        //        NewCustomer = null;

        //        if (Data.CustomerDB.GetCustomerByNationalCodeCount(NationalCodetextBox.Text.Trim()) > 0)
        //            NewCustomer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodetextBox.Text.Trim());

        //        CustomerForm customerForm;
        //        if (NewCustomer != null)
        //            customerForm = new CustomerForm(NewCustomer.ID);
        //        else
        //            customerForm = new CustomerForm();

        //        customerForm.ShowDialog();
        //        if (customerForm.DialogResult ?? false)
        //        {
        //            NewCustomer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
        //            CustomerNameTextBox.Text = string.Empty;
        //            CustomerNameTextBox.Text = NewCustomer.FirstNameOrTitle + ' ' + NewCustomer.LastName;
        //        }
        //    }
        //    else
        //    {
        //        CustomerForm customerForm = new CustomerForm();
        //        customerForm.ShowDialog();
        //        if (customerForm.DialogResult ?? false)
        //        {
        //            NewCustomer = DB.GetEntitybyID<Customer>(customerForm.ID);
        //            CustomerNameTextBox.Text = string.Empty;
        //            CustomerNameTextBox.Text = NewCustomer.FirstNameOrTitle + ' ' + NewCustomer.LastName;
        //        }
        //    }
        //}

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;

                //if (Data.CustomerDB.GetCustomerByNationalCodeCount(NationalCodeTextBox.Text.Trim()) > 1)
                //{ MessageBox.Show("چند مشترک با این کد ملی یافت شد. ابتدا اطلاعات مشترک را اصلاح کنید"); return; }

                
                if (Data.BlackListDB.ExistNationalCodeInBlackList(NationalCodeTextBox.Text.Trim()))
                {
                    Folder.MessageBox.ShowError("کد ملی در لیست سیاه قرار دارد");
                }
                else
                {

                NewCustomer = Data.CustomerDB.GetCustomerByNationalCode(NationalCodeTextBox.Text.Trim());
                if (NewCustomer != null)
                {
                    _Request.CustomerID = NewCustomer.ID;
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = NewCustomer.FirstNameOrTitle + ' ' + NewCustomer.LastName;
                }

                else
                {
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.ShowDialog();
                    if (customerForm.DialogResult ?? false)
                    {
                        NewCustomer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                        _Request.CustomerID = NewCustomer.ID;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = NewCustomer.FirstNameOrTitle + ' ' + NewCustomer.LastName;
                    }
                }
              }
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.ShowDialog();
                if (customerForm.DialogResult ?? false)
                {
                    Customer = Data.CustomerDB.GetCustomerByID(customerForm.ID);
                    _Request.CustomerID = Customer.ID;
                    CustomerNameTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = Customer.FirstNameOrTitle + ' ' + Customer.LastName;
                }
            }


            if (Customer != null)
            {
                (Window.GetWindow(this) as CRM.Application.Views.RequestForm).RequesterNametextBox.Text = Customer.FirstNameOrTitle + " " + Customer.LastName;
            }
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            Views.ReportChangeName reportChangeName = new ReportChangeName(_ReqID);
            reportChangeName.Show();
        }

        #endregion

        private void EditSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (Customer != null)
            {
                CustomerForm window = new CustomerForm(Customer.ID);
                window.ShowDialog();
            }
        }
    }
}