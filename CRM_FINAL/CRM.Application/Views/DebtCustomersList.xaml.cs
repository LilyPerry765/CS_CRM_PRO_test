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

namespace CRM.Application
{
    /// <summary>
    /// Interaction logic for DebtCustomersList.xaml
    /// </summary>
    public partial class DebtCustomersList : Local.TabWindow
    {
        public DebtCustomersList()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
           CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            if (CenterComboBox.SelectedIDs.Count != 0)
            {

                //int startRowIndex = Pager.StartRowIndex;
                //int pageSize = Pager.PageSize;

                //int lastNoHorizontalFrames = -1;
                //if (!string.IsNullOrWhiteSpace(LastNoHorizontalFramesTextBox.Text))
                //    lastNoHorizontalFrames = Convert.ToInt32(LastNoHorizontalFramesTextBox.Text);

                //int LastNoVerticalFrames = -1;
                //if (!string.IsNullOrWhiteSpace(LastNoVerticalFramesTextBox.Text))
                //    LastNoVerticalFrames = Convert.ToInt32(LastNoVerticalFramesTextBox.Text);

                //Pager.TotalRecords = Data.MDFDB.SearchMDFCount(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, lastNoHorizontalFrames, LastNoVerticalFrames, TypeComboBox.SelectedIDs);
                //ItemsDataGrid.ItemsSource = Data.MDFDB.SearchMDF(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, lastNoHorizontalFrames, LastNoVerticalFrames, TypeComboBox.SelectedIDs, startRowIndex, pageSize);


            }
            else
            {
                Folder.MessageBox.ShowInfo("لطفا حداقل یک مرکز انتخاب کنید");
            }

        }
        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {

        }
    }
}
