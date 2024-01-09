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

namespace CRM.Application.Views
{
    public partial class ADSLIPStaticForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;

        #endregion

        #region Constructors

        public ADSLIPStaticForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLIPStaticForm(long id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            IPGroupComboBox.ItemsSource = DB.GetAllEntity<ADSLIPType>();
            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLIPStatus));
        }

        private void LoadData()
        {
            ADSLIP aDSLIP = new ADSLIP();

            if (_ID == 0)
            {
                PortNoLabel.Visibility = Visibility.Collapsed;
                IPTextBox.Visibility = Visibility.Collapsed;
                IPRow.Height = GridLength.Auto;

                SaveButton.Content = "ذخیره";
            }
            else
            {
                FromPortNoLabel.Visibility = Visibility.Collapsed;
                FromIPTextBox.Visibility = Visibility.Collapsed;
                ToPortNoLabel.Visibility = Visibility.Collapsed;
                ToIPTextBox.Visibility = Visibility.Collapsed;
                FromIPRow.Height = GridLength.Auto;
                ToIPRow.Height = GridLength.Auto;

                aDSLIP = Data.ADSLIPDB.GetADSLIPById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = aDSLIP;
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
                ADSLIP adslIP = this.DataContext as ADSLIP;
                long fromIP = 0;
                long toIP = 0;

                if (_ID == 0)
                {
                    if (!string.IsNullOrEmpty(FromIPTextBox.Text))
                        fromIP = Convert.ToInt32(FromIPTextBox.Text.Split('.')[3]);
                    else
                        throw new Exception("لطفا شماره IP را وارد نمایید !");

                    if (!string.IsNullOrEmpty(ToIPTextBox.Text))
                        toIP = Convert.ToInt32(ToIPTextBox.Text.Split('.')[3]);

                    if (toIP == 0)
                    {
                        adslIP.ID = 0;
                        adslIP.IP = FromIPTextBox.Text;

                        if (!Data.ADSLIPDB.HasIP(adslIP.TypeID, adslIP.IP))
                        {
                            adslIP.Detach();
                            Save(adslIP);
                        }
                        else
                            throw new Exception("IP تکراری می باشد.");
                    }
                    else
                    {                        
                        for (long i = fromIP; i <= toIP; i++)
                        {
                            adslIP.ID = 0;
                            adslIP.IP = FromIPTextBox.Text.Split('.')[0] + "." + FromIPTextBox.Text.Split('.')[1] + "." + FromIPTextBox.Text.Split('.')[2] + "." + i.ToString();

                            if (!Data.ADSLIPDB.HasIP(adslIP.TypeID, adslIP.IP))
                            {
                                adslIP.Detach();
                                Save(adslIP);
                            }
                            else
                                throw new Exception("IP تکراری می باشد.");
                        }
                    }
                }
                else
                {
                    adslIP.Detach();
                    Save(adslIP);
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پورت ، " + ex.Message, ex);
            }
        }

        private void PortNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c) || char.Equals(c, '.'))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }


        #endregion
    }
}
