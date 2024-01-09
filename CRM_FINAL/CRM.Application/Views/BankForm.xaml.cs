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
using System.ComponentModel;

namespace CRM.Application.Views
{
    public partial class BankForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public BankForm()
        {
            InitializeComponent();
        }

        public BankForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            Bank bank = new Bank();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                bank = Data.BankDB.GetBankById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = bank;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                Bank bank = this.DataContext as Bank;

                bank.Detach();
                Save(bank);
                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره بانک", ex);
            }
        }

        #endregion
    }
}
