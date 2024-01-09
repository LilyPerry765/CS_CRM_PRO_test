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
using CRM.Data.Services;
using CookComputing.XmlRpc;

namespace CRM.Application.Views
{
    public partial class ADSlTariffForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private string _ServiceTitle = "";
        private ADSLService service { get; set; }

        #endregion

        #region Constructors

        public ADSlTariffForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSlTariffForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceType));
            CustomerGroupComboBox.ItemsSource = Data.ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            GroupComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceGroupCheckable();
            NetworkComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceNetworkCheckable();
            BandWidthComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceBandWidthCheckable();
            TrafficComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceTrafficCheckable();
            DurationComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceDurationCheckable();
            GiftProfileComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceGiftProfileCheckable();
            SellChanellComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSellChanell));

            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            IBSngGroupName.ItemsSource = ibsngService.AfeGetAllGroups(userAuthentication);

            TitleTextBox.Focus();
        }

        private void LoadData()
        {
            service = new ADSLService();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                service = Data.ADSLServiceDB.GetADSLServiceById(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = service;

            List<CheckableItem> checkableList1 = new List<CheckableItem>();
            List<CheckableItem> chanellItems = Helper.GetEnumCheckable(typeof(DB.ADSLSellChanell));
            string[] chanellList = new string[5];

            if (!string.IsNullOrEmpty(service.SellChanell))
                chanellList = service.SellChanell.Split(',');

            foreach (CheckableItem item in chanellItems)
            {
                if (chanellList.Contains(item.ID.ToString()))
                {
                    item.IsChecked = true;
                }
                checkableList1.Add(item);
            }

            SellChanellComboBox.ItemsSource = checkableList1;

            _ServiceTitle = service.Title;

            if (service.GroupID != null && service.GroupID != 0)
                CustomerGroupComboBox.SelectedValue = ADSLServiceGroupDB.GetADSLServiceGroupById(service.GroupID).CustomerGroupID;

            ResizeWindow();
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                ADSLService service = this.DataContext as ADSLService;

                if (CustomerGroupComboBox.SelectedValue == null)
                    throw new Exception("لطفا گروه مشتری را تعیین نمایید");

                if (GroupComboBox.SelectedValue == null)
                    throw new Exception("لطفا گروه سرویس را تعیین نمایید");

                if (TypeComboBox.SelectedValue == null)
                    throw new Exception("لطفا نوع خرید را تعیین نمایید");

                if (BandWidthComboBox.SelectedValue == null)
                    throw new Exception("لطفا پهنای باند را تعیین نمایید");

                if (TrafficComboBox.SelectedValue == null)
                    throw new Exception("لطفا ترافیک را تعیین نمایید");

                if (DurationComboBox.SelectedValue == null)
                    throw new Exception("لطفا مدت زمان استفاده را تعیین نمایید");

                if (!string.IsNullOrEmpty(ServiceCodeTextBox.Text))
                {
                    long? code = 0;
                    if (!Helper.CheckDigitDataEntry(ServiceCodeTextBox, out code))
                    {
                        ServiceCodeTextBox.Focus();
                        throw new Exception(".برای تعیین کد سرویس فقط از اعداد استفاده نمائید");
                    }
                }

                service.SellChanell = "";
                foreach (int index in SellChanellComboBox.SelectedIDs)
                {
                    service.SellChanell = service.SellChanell + index.ToString() + ",";
                }

                if (FreeCheckBox.IsChecked != null)
                {
                    if ((bool)FreeCheckBox.IsChecked)
                    {
                        service.IsFree = true;
                        service.PaymentTypeID = (byte)DB.ADSLPaymentType.Free;
                        service.Price = 0;
                        service.Tax = 0;
                        service.Abonman = 0;
                        service.PriceSum = 0;
                        service.IsInstalment = false;
                        service.Instalment = 0;
                        service.MAXInstallmentCount = 0;
                    }
                    else
                    {
                        service.IsFree = false;
                        service.PaymentTypeID = Convert.ToInt32(PaymentTypeListBox.SelectedValue);

                        int duration = Convert.ToInt32(DurationComboBox.SelectedValue);

                        if (service.Tax != null)
                        {
                            if (service.Abonman != null)
                                service.PriceSum = Convert.ToInt64(Convert.ToDecimal(service.Price) + Convert.ToDecimal(service.Abonman * duration) + Convert.ToDecimal((int)service.Tax * 0.01 * (service.Price + (service.Abonman * duration))));
                            else
                                service.PriceSum = Convert.ToInt64(Convert.ToDecimal(service.Price) + Convert.ToDecimal((int)service.Tax * 0.01 * service.Price));
                        }
                        else
                        {
                            if (service.Abonman != null)
                                service.PriceSum = service.Price + (service.Abonman * duration);
                            else
                                service.PriceSum = service.Price;
                        }
                    }
                }

                service.Detach();
                Save(service);

                ShowSuccessMessage("تعرفه ذخیره شد");

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره تعرفه ،" + ex.Message + " !", ex);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        private void CustomerGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerGroupComboBox.SelectedValue != null)
                GroupComboBox.ItemsSource = Data.ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupID((int)CustomerGroupComboBox.SelectedValue);
        }

        private void FreeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PaymentTypeListBox.IsEnabled = false;
            InstalmentCheckBox.IsEnabled = false;
            MAXInstallmentCountTextBox.IsEnabled = false;
            PriceTextBox.IsEnabled = false;
            TaxTextBox.IsEnabled = false;
            AbonmanTextBox.IsEnabled = false;

            PaymentLabel.IsEnabled = false;
            InstalmentLabel.IsEnabled = false;
            MAXInstallmentCountLabel.IsEnabled = false;
            PriceLabel.IsEnabled = false;
            TaxLabel.IsEnabled = false;
            AbonmanLabel.IsEnabled = false;

            service.PaymentTypeID = 0;
            service.IsInstalment = false;
            service.MAXInstallmentCount = 0;
            service.Price = 0;
            service.Tax = 0;
            service.Abonman = 0;
        }

        private void FreeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PaymentTypeListBox.IsEnabled = true;
            InstalmentCheckBox.IsEnabled = true;
            MAXInstallmentCountTextBox.IsEnabled = true;
            PriceTextBox.IsEnabled = true;
            TaxTextBox.IsEnabled = true;
            AbonmanTextBox.IsEnabled = true;

            PaymentLabel.IsEnabled = true;
            InstalmentLabel.IsEnabled = true;
            MAXInstallmentCountLabel.IsEnabled = true;
            PriceLabel.IsEnabled = true;
            TaxLabel.IsEnabled = true;
            AbonmanLabel.IsEnabled = true;
        }

        private void InstalmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MAXInstallmentCountTextBox.IsEnabled = true;
        }

        private void InstalmentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MAXInstallmentCountTextBox.IsEnabled = false;
            service.MAXInstallmentCount = 0;
        }

        #endregion
    }
}
