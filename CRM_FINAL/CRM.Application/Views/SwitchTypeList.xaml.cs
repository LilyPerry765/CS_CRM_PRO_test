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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;
using System.Collections.ObjectModel;

namespace CRM.Application.Views
{
    public partial class SwitchTypeList : Local.TabWindow
    {
        #region Constructors

        public SwitchTypeList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TrafficTypeCodeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TrafficTypeCode));
            TrafficTypeCode.ItemsSource = Helper.GetEnumItem(typeof(DB.TrafficTypeCode));

            SwitchTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SwitchTypeCode));
            SwitchTypeGrid.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchTypeCode));
        }

        private void LoadData()
        {
            Search(null, null);
        }        
        
        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {

            int capacity = -1;
            int operationalCapacity = -1;
            int installCapacity = -1;
            int specialServiceCapacity = -1;
            int counterDigitCount = -1;
            int publicCapacity = -1;

            if (!string.IsNullOrWhiteSpace(PublicCapacityTextBox.Text))
                capacity = Convert.ToInt32(CapacityTextBox.Text);

            if (!string.IsNullOrWhiteSpace(PublicCapacityTextBox.Text))
                operationalCapacity = Convert.ToInt32(OperationalCapacityTextBox.Text);

            if (!string.IsNullOrWhiteSpace(PublicCapacityTextBox.Text))
                installCapacity = Convert.ToInt32(InstallCapacityTextBox.Text);

            if (!string.IsNullOrWhiteSpace(PublicCapacityTextBox.Text))
                specialServiceCapacity = Convert.ToInt32(SpecialServiceCapacityTextBox.Text);

            if (!string.IsNullOrWhiteSpace(PublicCapacityTextBox.Text))
                counterDigitCount = Convert.ToInt32(CounterDigitCountTextBox.Text);

            if (!string.IsNullOrWhiteSpace(PublicCapacityTextBox.Text))
                publicCapacity = Convert.ToInt32(PublicCapacityTextBox.Text);

            ItemDataGrid.ItemsSource = Data.SwitchTypeDB.SearchSwitchType(CommercialNameTextBox.Text, SwitchTypeComboBox.SelectedIDs, IsDigitalCheckBox.IsChecked, capacity, operationalCapacity, installCapacity, specialServiceCapacity, counterDigitCount, SupportPublicNoCheckBox.IsChecked, publicCapacity, TrafficTypeCodeComboBox.SelectedIDs);

        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {

        }

        private void SearchExpander_Expanded(object sender, RoutedEventArgs e)
        {
        }        

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {

                SwitchType switchType = e.Row.Item as SwitchType;
                switchType.Detach();
                DB.Save(switchType);
                ShowSuccessMessage("ویرایش سوئیچ انجام شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("ویرایش انجام نشد", ex);
            }

        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemDataGrid.SelectedIndex >= 0)
            {
                SwitchType switchType = ItemDataGrid.SelectedItem as SwitchType;
                if (switchType == null) return;
                SwitchTypeForm window = new SwitchTypeForm(switchType.ID);
                window.ShowDialog();
                if (window.DialogResult == true)
                    Search(null, null);
            }

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemDataGrid.SelectedIndex < 0 || ItemDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("آیا از حذف کردن مطمئن هستید", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        SwitchType item = ItemDataGrid.SelectedItem as SwitchType;
                        DB.Delete<Data.SwitchType>(item.ID);
                        ShowSuccessMessage("حذف سوئیچ انجام شد");
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در حذف سوئیچ", ex);
                }
            }

        }

        private void StatusBar_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {

            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void InsertItem(object sender, RoutedEventArgs e)
        {
            SwitchTypeForm window = new SwitchTypeForm(0);
            window.ShowDialog();
            if (window.DialogResult == true)
                Search(null, null);
        }

        #endregion
    }
}
