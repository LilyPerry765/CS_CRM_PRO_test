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
    public partial class PCMPortList : Local.TabWindow
    {
        #region Constructor & Fields

        public PCMPortList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            PortTypeCheckableComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.BuchtType)).Where(t=>t.ID == (int)DB.BuchtType.InLine || t.ID == (int)DB.BuchtType.OutLine);
            StatusCheckableComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PCMStatus));

            PortTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.BuchtType)).Where(t => t.ID == (int)DB.BuchtType.InLine || t.ID == (int)DB.BuchtType.OutLine);
            StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PCMStatus));
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
        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int portNumber = -1;
            if (!string.IsNullOrWhiteSpace(PortNumberTextBox.Text)) portNumber = Convert.ToInt32(PortNumberTextBox.Text);

            Pager.TotalRecords = Data.PCMPortDB.SearchPCMPortCount(CenterUserControl.CityComboBox.SelectedIDs, CenterUserControl.CenterComboBox.SelectedIDs, RockCheckableComboBox.SelectedIDs, ShelfCheckableComboBox.SelectedIDs, CardCheckableComboBox.SelectedIDs, PortTypeCheckableComboBox.SelectedIDs, StatusCheckableComboBox.SelectedIDs, portNumber);
            ItemsDataGrid.ItemsSource = Data.PCMPortDB.SearchPCMPort(
                CenterUserControl.CityComboBox.SelectedIDs ,
                CenterUserControl.CenterComboBox.SelectedIDs ,
                RockCheckableComboBox.SelectedIDs ,
                ShelfCheckableComboBox.SelectedIDs ,
                CardCheckableComboBox.SelectedIDs ,
                PortTypeCheckableComboBox.SelectedIDs,
                StatusCheckableComboBox.SelectedIDs,
                portNumber
                , startRowIndex
                , pageSize
                                      );
        }

        private void CenterUserControl_DoCenterComboBoxLostFocus_1()
        {
            RockCheckableComboBox.ItemsSource = Data.PCMRockDB.GetPCMRockCheckableByCenterIDs(CenterUserControl.CenterComboBox.SelectedIDs);
        }
        private void RockCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            ShelfCheckableComboBox.ItemsSource = Data.PCMShelfDB.GetCheckableItemPCMShelfByRockIDs(RockCheckableComboBox.SelectedIDs);
        }

        private void ShelfCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CardCheckableComboBox.ItemsSource = Data.PCMDB.GetCheckableItemPCMCardInfoByShelfID(ShelfCheckableComboBox.SelectedIDs);
        }
        #endregion Event Handlers

        private void MalfactionClick(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                MalfuctionForm MalfuctionFormWindow = new MalfuctionForm((ItemsDataGrid.SelectedItem as PCMPortInfo).ID, (Byte)DB.MalfuctionType.PCMPort);
                MalfuctionFormWindow.ShowDialog();

                TabWindow_Loaded(null, null);
            }
        }

      

    }
}
