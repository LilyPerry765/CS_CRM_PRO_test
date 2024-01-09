using CRM.Data;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for CenterToCenterTranslationList.xaml
    /// </summary>
    public partial class CenterToCenterTranslationList : Local.TabWindow
    {
        public CenterToCenterTranslationList()
        {
            InitializeComponent();
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
      
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            Pager.TotalRecords = Data.CenterToCenterTranslationDB.SearchCenterToCenterTranslationCount(
                CityComboBox.SelectedIDs,
                CenterComboBox.SelectedIDs,
                OldCabinetTextBox.Text.Trim(),
                NewCabinetTextBox.Text.Trim(),
                AccomplishmentDateDate.SelectedDate,
                RequestLetterNoTextBox.Text,
                AccomplishmentTimeTextBox.Text
            );
            ItemsDataGrid.ItemsSource = Data.CenterToCenterTranslationDB.SearchCenterToCenterTranslation(
                 CityComboBox.SelectedIDs,
                 CenterComboBox.SelectedIDs,
                 OldCabinetTextBox.Text.Trim(),
                 NewCabinetTextBox.Text.Trim(),
                 AccomplishmentDateDate.SelectedDate,
                 RequestLetterNoTextBox.Text,
                 AccomplishmentTimeTextBox.Text,
                 startRowIndex,
                 pageSize
             );
        }
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            CenterToCenterTranslationForm window = new CenterToCenterTranslationForm((int)DB.RequestType.CenterToCenterTranslation);
            window.ShowDialog();


            if (window.DialogResult == true)
                Search(null, null);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Search(null, null);
        }
    }
}
