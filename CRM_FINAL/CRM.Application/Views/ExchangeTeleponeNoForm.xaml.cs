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
    /// <summary>
    /// Interaction logic for ExchangeSwitchForm.xaml
    /// </summary>
    public partial class ExchangeSwitchForm : Local.PopupWindow
    {

        public ExchangeSwitchForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ExchangeSwitchForm(long request)
        {
            ExchangeRequestInfoUserControl.ID = request;
            Initialize();
        }

        private void Initialize()
        {
            FromSwitchColumn.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
            TOSwitchColumn.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
            FromSwitchPrecodeColumn.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckable();
            TOSwitchPrecodeColumn.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckable();
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            if (ExchangeRequestInfoUserControl.ID == 0)
            {
                ExchangSwitch.Visibility = Visibility.Collapsed;
            }
            else
            {
                ExchangSwitch.Visibility = Visibility.Visible;
                ExchangePostDetailDataGrid.ItemsSource = DB.SearchByPropertyName<ExchangeTelephoneNo>("RequestID", ExchangeRequestInfoUserControl.ID);
            }
        }
        private void NewItem(object sender, RoutedEventArgs e)
        {
            if (ExchangeRequestInfoUserControl.ID == 0)
            {
                ShowErrorMessage("ابتدا در خواست را ذخیره کنید", new Exception());
            }
            else
            {

                ExchangeSwitchDetails window = new ExchangeSwitchDetails(ExchangeRequestInfoUserControl.ID);
                window.ShowDialog();
                Load();
            }
        }
        void ExchangeRequestInfo_DoSaved(bool flag)
        {
            if (flag == true)
            {
                ShowSuccessMessage("در خواست ذخیره شد");
                ExchangSwitch.Visibility = Visibility.Visible;
            }
            else
            {
                ShowErrorMessage("خطا در ذخیره درخواست", new Exception());
            }
        }


    }
}
