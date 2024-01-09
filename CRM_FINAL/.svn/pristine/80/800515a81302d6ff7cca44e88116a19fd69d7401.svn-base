using CRM.Application.Local;
using CRM.Data;
using Enterprise;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for WarningMessageList.xaml
    /// </summary>
    public partial class WarningMessageList : TabWindow
    {
        #region Constructor
        public WarningMessageList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region EventHandlers

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = WarningMessageDB.SearchWarningMessage(WarningTypeComboBox.SelectedIDs);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            WarningTypeComboBox.Reset();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            WarningMessageForm window = new WarningMessageForm(0);
            window.Title = "فرم افزودن پیام اخطار";
            window.ShowDialog();

            if (window.DialogResult.Value)
            {
                SearchButton_Click(null, null);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                WarningMessage selectedItem = ItemsDataGrid.SelectedItem as WarningMessage;
                if (selectedItem == null) return;
                WarningMessageForm window = new WarningMessageForm(selectedItem.ID);
                window.Title = "فرم ویرایش پیام اخطار";
                window.ShowDialog();

                if (window.DialogResult.Value)
                {
                    LoadData();
                }
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    WarningMessage item = ItemsDataGrid.SelectedItem as WarningMessage;
                    DB.Delete<WarningMessage>(item.ID);
                    ShowSuccessMessage(".عملیات حذف با موفقیت انجام گرفت");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ShowErrorMessage("خطا در حذف پیام اخطار", ex);
            }
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            WarningTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.WarningHistory));
            WarningTypeDataGridComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.WarningHistory));
        }

        public void LoadData()
        {
            SearchButton_Click(null, null);
        }

        #endregion

    }
}
