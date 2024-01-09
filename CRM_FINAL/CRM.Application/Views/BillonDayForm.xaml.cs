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
    public partial class BillonDayForm : Local.PopupWindow
    {

        #region Properties

        #endregion

        #region Constructors
        
        public BillonDayForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadData()
        {

        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            long telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

            List<InstallmentRequestPayment> instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentRemainByTelephoneNo(telephoneNo);

            long sumInstalment = 0;

            foreach (InstallmentRequestPayment currentInstalment in instalmentList)
            {
                if ((Convert.ToInt32((DateTime.Now - (DateTime)currentInstalment.StartDate).TotalDays)) > 0)                
                {
                    sumInstalment = sumInstalment + currentInstalment.Cost;
                }
            }

            InstalmentTextBox.Text = sumInstalment.ToString() + " ریال";
        }
        
        private void TelephoneNoTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);

            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion
    }
}
