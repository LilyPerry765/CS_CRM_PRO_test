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
    public partial class ActionLogList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public ActionLogList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ActionNameComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ActionLog));
            ActionNameColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ActionLog));
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

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
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            Pager.TotalRecords = Data.ActionLogDB.SearcActionLogsCount(ActionNameComboBox.SelectedIDs, UserNameTextBox.Text, FromActionDate.SelectedDate, ToActionDate.SelectedDate);
            ItemsDataGrid.ItemsSource = Data.ActionLogDB.SearcActionLogs(ActionNameComboBox.SelectedIDs, UserNameTextBox.Text, FromActionDate.SelectedDate, ToActionDate.SelectedDate, startRowIndex, pageSize);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }
         
        #endregion
    }
}
