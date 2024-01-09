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
    public partial class ADSLMDFPortList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public ADSLMDFPortList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {            
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            StatusComboBox.ItemsSource=Helper.GetEnumCheckable(typeof(DB.ADSLPortStatus));
            
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ActionUserControl._ListType = (byte) DB.ListType.ADSLMDFPortList;
            ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Print };
            Search(null, null);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            CenterComboBox.Reset();
            CityComboBox.Reset();
            MDFComboBox.Reset();
            RowComboBox.Reset();
            ColumnComboBox.Reset();            
            PortComboBox.Reset();
            TelephoneNoTextBox.Text = string.Empty;
            StatusComboBox.Reset();

            Search(null,null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            Pager.TotalRecords = Data.ADSLMDFDB.SearchADSLMDFPortCount(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, StatusComboBox.SelectedIDs, RowComboBox.SelectedIDs, ColumnComboBox.SelectedIDs, PortComboBox.SelectedIDs, TelephoneNoTextBox.Text.Trim());
            ItemsDataGrid.ItemsSource = Data.ADSLMDFDB.SearchADSLMDFPort(CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs, MDFComboBox.SelectedIDs, StatusComboBox.SelectedIDs, RowComboBox.SelectedIDs, ColumnComboBox.SelectedIDs, PortComboBox.SelectedIDs, TelephoneNoTextBox.Text.Trim(), startRowIndex, pageSize);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                ADSLPortInfo portInfo = ItemsDataGrid.SelectedItem as ADSLPortInfo;
                ADSLPort port = ADSLPortDB.GetADSlPortByID(portInfo.ID);

                ADSLPortsForm window = new ADSLPortsForm(port.ID);
                window.ShowDialog();

                Search(null, null);
            }
        }        

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (CityComboBox.SelectedIDs.Count != 0)
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            if (CenterComboBox.SelectedIDs.Count != 0)
                MDFComboBox.ItemsSource = ADSLMDFDB.GetMDFCheckablebyCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void MDFComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MDFComboBox.SelectedIDs.Count != 0)
                RowComboBox.ItemsSource = VerticalMDFColumnDB.GetVerticalMDFColumnCheckablebyMDFIDs(MDFComboBox.SelectedIDs);
        }

        private void RowComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RowComboBox.SelectedIDs.Count != 0)
                ColumnComboBox.ItemsSource = VerticalMDFRowDB.GetVerticalMDFRowCheckablebyRowIDs(RowComboBox.SelectedIDs);
        }

        private void ColumnComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ColumnComboBox.SelectedIDs.Count != 0)
                PortComboBox.ItemsSource = BuchtDB.GetBuchtChechablebyColumnIDs(ColumnComboBox.SelectedIDs);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
