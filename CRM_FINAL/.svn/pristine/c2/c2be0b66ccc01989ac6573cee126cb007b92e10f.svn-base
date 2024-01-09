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
    public partial class ADSLIPGroupList : Local.TabWindow
    {
        #region Propperties

        #endregion

        #region Constructors

        public ADSLIPGroupList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLIPGroupType));
            TypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLIPGroupType));
        }

        private void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = Data.ADSLIPDB.SearchADSLIPGroup(TypeComboBox.SelectedIDs, TitleTextBox.Text, IsActiveCheckBox.IsChecked);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            TitleTextBox.Text = string.Empty;
            TypeComboBox.Reset();
            IsActiveCheckBox.IsChecked = null;

            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLIPGroupForm Window = new ADSLIPGroupForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }
        
        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLIPType item = ItemsDataGrid.SelectedItem as ADSLIPType;

                if (item == null)
                    return;

                ADSLIPGroupForm Window = new ADSLIPGroupForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") 
                return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ADSLIPType item = ItemsDataGrid.SelectedItem as ADSLIPType;
                    DB.Delete<ADSLIPType>(item.ID);

                    ShowSuccessMessage("گروه IP مورد نظر حذف شد");
                    
                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف نوع IP", ex);
            }
        }

        #endregion
    }
}
