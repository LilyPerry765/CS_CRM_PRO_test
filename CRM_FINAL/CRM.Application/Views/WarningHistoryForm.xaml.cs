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
using System.Globalization;
using Enterprise;
using System.Windows.Threading;

namespace CRM.Application.Views
{
    public partial class WarningHistoryForm : Local.PopupWindow
    {
        #region Properties and Fields
        
        private int _ID = 0;

        #endregion

        #region Constructor

        public WarningHistoryForm()
        {
            InitializeComponent();
            Initialize();
        }

        public WarningHistoryForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region EventHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
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
                WarningHistory item = this.DataContext as WarningHistory;

                if (_ID == 0)
                {
                    long telephonNo = -1;
                    if (!long.TryParse(TelephoneTextBox.Text.Trim(), out telephonNo))
                        throw new Exception("تلفن صحیح نیست");

                    Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(telephonNo);

                    if (telephone == null)
                        throw new Exception("تلفن موجود نیست");

                    if (telephone.Status == (byte)DB.TelephoneStatus.Free)
                        throw new Exception("تلفن آزاد می باشد");

                    item.InsertDate = DB.GetServerDate();
                }

                item.Detach();
                DB.Save(item);

                ShowSuccessMessage("ذخیره انجام شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(" خطا در ذخیره", ex);
            }
        }
        
        //TODO:rad
        private void TelephoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this._ID == 0 && !string.IsNullOrEmpty(TelephoneTextBox.Text.Trim())) //برای ثبت اخطار تعیین شماره تلفن الزامی است و نباید خالی باشد
            {
                //آیا یک مقدار عددی برای شماره تلفن تعیین شده است یا خیر
                long? enteredTelephoneNo = -1;
                bool enteredTelephoneNoIsValid = true;
                enteredTelephoneNoIsValid = Helper.CheckDigitDataEntry(TelephoneTextBox, out enteredTelephoneNo);

                if (enteredTelephoneNoIsValid)
                {
                    try
                    {
                        //بدست آوردن نام مشترک مربوط به شماره تلفن تعیین شده
                        Customer customer = CustomerDB.GetCustomerByTelephone((long)enteredTelephoneNo);
                        CustomerNameTextBox.Text = (customer != null) ? string.Format("{0} {1}", customer.FirstNameOrTitle, customer.LastName) : "-----";
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex, "خطا در جستجو نام مشترک - بخش مشترکین - لیست اخطارها");
                        MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(".برای تعیین شماره تلفن فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TelephoneTextBox.SelectAll();
                    TelephoneTextBox.Focus();
                }
            }
            else if (this._ID == 0 && string.IsNullOrEmpty(TelephoneTextBox.Text.Trim())) //در صورت خالی بودن مقدار شماره تلفن به کاربر پیغامی نشان داده شود
            {
                MessageBox.Show(".تعیین شماره تلفن الزامی است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                TelephoneTextBox.Focus();
            }
            else
            {
                return;
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.WarningHistory));
        }

        private void LoadData()
        {
            WarningHistory item = new WarningHistory();

            if (_ID == 0)
            {
                DateTime dateTime = DB.GetServerDate();
                item.Date = dateTime.Date;
                item.Time = dateTime.ToString("hh:mm:ss");
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.WarningHistoryDB.GetWarningHistoryByID(_ID);

                //TODO:rad edit
                Customer customer = CustomerDB.GetCustomerByTelephone(item.TelephonNo);
                if (customer != null)
                {
                    CustomerNameTextBox.Text = string.Format("{0} {1}", customer.FirstNameOrTitle, customer.LastName);
                }
                else
                {
                    CustomerNameTextBox.Text = "-----";
                }

                TelephoneTextBox.IsEnabled = false;
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        #endregion


    }
}
