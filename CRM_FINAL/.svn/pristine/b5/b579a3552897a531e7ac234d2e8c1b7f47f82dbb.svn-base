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
    public partial class ADSLSellerAgentUserCreditList : Local.PopupWindow
    {
        #region Properties

        private int _UserID = 0;

        #endregion

        #region Constructors

        public ADSLSellerAgentUserCreditList()
        {
            InitializeComponent();
        }

        public ADSLSellerAgentUserCreditList(int userID)
            : this()
        {
            _UserID = userID;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            FromPaymentDate.SelectedDate = DB.GetServerDate().AddDays(-10);
            ToPaymentDate.SelectedDate = DB.GetServerDate();

            SearchButton_Click(null, null);            

            ResizeWindow();
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = ADSLSellerGroupDB.SearchADSLSellerAgentUserCredit(_UserID,RequestIDTextBox.Text.Trim(), TelephonenNoTextBox.Text.Trim(), FromPaymentDate.SelectedDate, ToPaymentDate.SelectedDate);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion        
    }
}
