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
    public partial class MDFWorkingHoursList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public MDFWorkingHoursList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
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
            ItemsDataGrid.ItemsSource =  MDFWorkingHoursDB.SearchMDFWorkingHour(DayTextBox.Text);
        }
        
        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                MDFWorkingHour item = ItemsDataGrid.SelectedItem as MDFWorkingHour;
                if (item == null) return;

                MDFWorkingHoursForm window = new MDFWorkingHoursForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        #endregion
    }
}
