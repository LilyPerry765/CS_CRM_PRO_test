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
    /// Interaction logic for GroupingCablePairList.xaml
    /// </summary>
    public partial class GroupingCablePairList : Local.TabWindow
    {
        public GroupingCablePairList()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
          //  Search(null, null);
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CableNumberComboBox.ItemsSource = Data.CableDB.GetCableCheckableByCenterID(CenterComboBox.SelectedIDs);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);

        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = Data.CablePairDB.GetGroupingCablePair(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, CableNumberComboBox.SelectedIDs_l);
        }

        private void AssignBucht(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                GroupingCablePair item = ItemsDataGrid.SelectedItem as Data.GroupingCablePair;
                if (item == null) return;

                AssignCablePairToBuchtForm window = new AssignCablePairToBuchtForm(item.CableID ?? 0, (byte)DB.TypeCablePairToBucht.Assign);
                window.ShowDialog();

                Search(null, null);
            }
        }
        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                GroupingCablePair item = ItemsDataGrid.SelectedItem as Data.GroupingCablePair;
                if (item == null) return;

                AssignCablePairToBuchtForm window = new AssignCablePairToBuchtForm(item.CableID ?? 0, (byte)DB.TypeCablePairToBucht.Leave);
                window.ShowDialog();

                Search(null, null);
            }
        }
    }
}
