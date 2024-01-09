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
    public partial class ADSLMDFList : Local.TabWindow
    {
        #region Properties
 
        #endregion

        #region Constructor

        public ADSLMDFList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            //MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckable();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }        

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Search(null, null);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            CenterComboBox.Reset();
            CityComboBox.Reset();
        }
        
        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            Pager.TotalRecords = Data.ADSLMDFDB.SearchADSLMDFCount(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs);
            ItemsDataGrid.ItemsSource = Data.ADSLMDFDB.SearchADSLMDF(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs,startRowIndex, pageSize);
        }

        private void AddRange(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLMDFInfo item = ItemsDataGrid.SelectedItem as ADSLMDFInfo;

                if (item == null)
                    return;

                ADSLMDFRangeForm Window = new ADSLMDFRangeForm((int)item.ID);
                Window.ShowDialog();                
            }
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion

        
    }
}
