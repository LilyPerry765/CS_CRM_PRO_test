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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class RequestPaymentForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;
        private long _RequestID = 0;
        private Request _request { get; set; }
        private bool IsOtherCost { get; set; }
        #endregion

        #region Constructor

        public RequestPaymentForm()
        {
            InitializeComponent();

        }

        public RequestPaymentForm(long id, long requestID, bool isOtherCost)
            : this()
        {
            IsOtherCost = isOtherCost;
            _RequestID = requestID;
            _ID = id;
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _request = Data.RequestDB.GetRequestByID(_RequestID);
            BaseCostComboBox.ItemsSource = Data.BaseCostDB.GetBaseCostCheckableByRequestTypeID(_request.RequestTypeID);
            OtherCostComboBox.ItemsSource = Data.OtherCostDB.GetOtherCostCheckable();
            PaymentWayComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PaymentWay));
            BankComboBox.ItemsSource = Data.BankDB.GetBanksCheckable();
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PaymentType));
            if (IsOtherCost == false)
            {
                PaymentTypeComboBox.IsEnabled = false;
                PaymentTypeListBox.IsEnabled = false;
                BaseCostComboBox.IsEnabled = false;
                AmountSum.IsEnabled = false;
            }
            else
            {
                PaymentTypeListBox.SelectedValue = 2;
                PaymentTypeListBox.IsEnabled = false;
            }
        }

        private void LoadData()
        {
            RequestPayment item = new RequestPayment();
            // item.PaymentType = (byte)_request.RequestPaymentTypeID;
            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                item = Data.RequestPaymentDB.GetRequestPaymentByID(_ID);

                if (item.OtherCostID != null)
                    OtherCostButtom.IsSelected = true;

                if (item.BaseCostID != null)
                    BaseCostButton.IsSelected = true;

                if (item.PaymentFicheID != null)
                    PaymentFicheButtom.IsSelected = true;

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
            ListBox_SelectionChanged(null, null);
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PaymentFicheComboBox.ItemsSource = Data.PaymentFicheDB.GetPaymentFicheCheckable(_RequestID);
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                RequestPayment item = this.DataContext as RequestPayment;
                if (Convert.ToInt32(PaymentTypeListBox.SelectedValue) == 2)
                {
                    item.OtherCostID = Convert.ToInt32(OtherCostComboBox.SelectedValue);
                    item.InsertDate = DB.GetServerDate();
                    item.RequestID = _RequestID;
                    item.PaymentWay = (byte)PaymentWayComboBox.SelectedValue;
                    item.PaymentType = (byte)PaymentTypeComboBox.SelectedValue;
                    item.IsKickedBack = false;
                    item.Cost = Convert.ToInt64(AmountSum.Text);
                    item.Tax = 0;
                }
                item.AmountSum = Convert.ToInt64(AmountSum.Text);

                if (_ID == 0)
                    item.RequestID = _RequestID;

                if (item.PaymentWay == null)
                    throw new Exception("لطفا نحوه پرداخت را تعیین نمایید");
                if (item.BankID == null)
                    throw new Exception("لطفا نام بانک را انتخاب نمایید");
                if (item.FicheNunmber == null)
                    throw new Exception("لطفا شماره فیش را وارد نمایید");
                if (item.FicheDate == null)
                    throw new Exception("لطفا تاریخ فیش را وارد نمایید");
                if (item.PaymentDate == null)
                    throw new Exception("لطفا تاریخ پرداخت را وارد نمایید");

                item.Detach();
                Save(item);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره دریافت / پرداخت ، " + ex.Message + " !", ex);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OtherCostForm window = new OtherCostForm();
            window.ShowDialog();
            Initialize();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BaseCostDataGrid == null) return;
            BaseCostDataGrid.Visibility = Visibility.Collapsed;
            PaymentFicheGrid.Visibility = Visibility.Collapsed;
            OtherCostGrid.Visibility = Visibility.Collapsed;
            switch (Convert.ToInt16(PaymentTypeListBox.SelectedValue))
            {
                case 1:
                    BaseCostDataGrid.Visibility = Visibility.Visible;
                    OtherCostComboBox.SelectedIndex = -1;
                    PaymentFicheComboBox.SelectedIndex = -1;
                    break;
                case 2:
                    OtherCostGrid.Visibility = Visibility.Visible;
                    BaseCostComboBox.SelectedIndex = -1;
                    PaymentFicheComboBox.SelectedIndex = -1;
                    break;
                case 3:
                    PaymentFicheGrid.Visibility = Visibility.Visible;
                    BaseCostComboBox.SelectedIndex = -1;
                    OtherCostComboBox.SelectedIndex = -1;
                    break;
                default:
                    break;
            }


        }

        private void BaseCostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BaseCostComboBox.SelectedValue != null)
            {
                Data.BaseCost baseCost = Data.BaseCostDB.GetBaseCostByID((int)BaseCostComboBox.SelectedValue);
                AmountSum.Text = baseCost.Cost.ToString();
            }
        }

        private void Amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        private void OtherCostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OtherCostComboBox.SelectedValue != null)
            {
                OtherCost otherCost = Data.OtherCostDB.GetOtherCostByID(Convert.ToInt32(OtherCostComboBox.SelectedValue));
                AmountSum.Text = otherCost.BasePrice.ToString();
            }
        }

        #endregion
    }
}
