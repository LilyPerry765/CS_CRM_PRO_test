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
    public partial class BankBranchForm : Local.PopupWindow
    {
        private int _ID = 0;

        public BankBranchForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            BankComboBox.ItemsSource = BankDB.GetBanksCheckable();
        }

        public BankBranchForm(int id)
            : this()
        {
            _ID = id;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Data.BankBranch bankbranch = new Data.BankBranch();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                bankbranch = BankBranchDB.GetBankBranchByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }
            this.DataContext = bankbranch;
        }

        private void SaveBankBrnachForm(object sender, RoutedEventArgs e)
        {
            try
            {
                BankBranch bankbranch = this.DataContext as BankBranch;

                bankbranch.Detach();
                Save(bankbranch);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره شعبه بانک", ex);
            }
        }





    }
}
