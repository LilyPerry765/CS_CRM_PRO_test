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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class RoundForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;

        #endregion

        #region Constructors

        public RoundForm()
        {
            InitializeComponent();

        }

        public RoundForm(long id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            RoundSaleInfo item = new RoundSaleInfo();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.RoundListDB.GetRoundById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoundSaleInfo item = this.DataContext as RoundSaleInfo;

                item.Detach();
                DB.Save(item);

                ShowSuccessMessage("تلفن رند ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره تلفن رند", ex);
            }
        }

        #endregion
    }
}