using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Threading.Tasks;
using CRM.Application.Local;
using System.Windows.Media;
using System.Data;

namespace CRM.Application.Views
{
    public partial class InstalmentRequestPaymentList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public InstalmentRequestPaymentList()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            Search(null,null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            long TelNo = !string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text) ? Convert.ToInt64(TelephoneNoTextBox.Text) : 0;
            List<InstalmentRequestPaymentInfo> Result = InstalmentRequestPaymentDB.GetInstalmentRequestPaymentList(TelNo, StartFromDate.SelectedDate, StartToDate.SelectedDate, EndFromDate.SelectedDate, EndToDate.SelectedDate, IsChequeCheckBox.IsChecked);
            
            foreach (InstalmentRequestPaymentInfo info in Result)
            {
                if (info.IsChequeByte == true)
                    info.IsCheque = "بلی";
                else if (info.IsChequeByte == false)
                    info.IsCheque = "خیر";
            }

            ItemsDataGrid.ItemsSource = Result;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        #endregion
    }
}
