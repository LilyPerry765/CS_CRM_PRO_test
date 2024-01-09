using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;

namespace CRM.Application.Views
{
    public partial class CauseOfTakePossessionList : Local.TabWindow
    {
        #region Constructor & Fields

        public CauseOfTakePossessionList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {

        }

        public void LoadData()
        {
            Search(null, null);
        }
        #endregion Load Methods

        #region Event Handlers
        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }


        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {

            ItemsDataGrid.ItemsSource = Data.CauseOfTakePossessionDB.Search(NameTextBox.Text.Trim());
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            HideMessage();
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.CauseOfTakePossession item = ItemsDataGrid.SelectedItem as CRM.Data.CauseOfTakePossession;

                    //چون جدول موجودیت این لیست دارای ستون 
                    //IsReadOnly
                    //میباشد ، برخی رکورد ها را نه میتوان حذف کرد و نه ویرایش
                    if (item.ID == (int)DB.CauseOfTakePossession.CollectingEquipment)
                    {
                        throw new Exception(".این رکورد قابل حذف نمیباشد");
                    }
                    DB.Delete<Data.CauseOfTakePossession>(item.ID);
                    ShowSuccessMessage("مودم مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف علت", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            HideMessage();
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                try
                {
                    CauseOfTakePossession item = ItemsDataGrid.SelectedItem as Data.CauseOfTakePossession;
                    if (item == null) return;

                    //چون جدول موجودیت این لیست دارای ستون 
                    //IsReadOnly
                    //میباشد ، برخی رکورد ها را نه میتوان حذف کرد و نه ویرایش
                    if (item.ID == (int)DB.CauseOfTakePossession.CollectingEquipment)
                    {
                        throw new Exception(".این رکورد قابل ویرایش نمیباشد");
                    }

                    CauseOfTakePossessionForm window = new CauseOfTakePossessionForm(item.ID);
                    window.ShowDialog();

                    if (window.DialogResult == true)
                        LoadData();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در ویرایش علت", ex);
                }
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.CauseOfTakePossession item = e.Row.Item as Data.CauseOfTakePossession;

                //چون جدول موجودیت این لیست دارای ستون 
                //IsReadOnly
                //میباشد ، برخی رکورد ها را نه میتوان حذف کرد و نه ویرایش
                if (item.ID == (int)DB.CauseOfTakePossession.CollectingEquipment)
                {
                    throw new Exception(".این رکورد قابل ویرایش نمیباشد");
                }

                item.Detach();
                DB.Save(item);

                Search(null, null);

                ShowSuccessMessage("علت مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره علت", ex);
            }
        }
        #endregion Event Handlers

        private void AddItem(object sender, RoutedEventArgs e)
        {
            HideMessage();
            CauseOfTakePossessionForm window = new CauseOfTakePossessionForm();
            window.ShowDialog();
        }
    }
}
