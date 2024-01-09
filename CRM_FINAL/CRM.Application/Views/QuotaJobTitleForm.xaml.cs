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
    public partial class QuotaJobTitleForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public QuotaJobTitleForm()
        {
            InitializeComponent();
            Initialize();
        }

        public QuotaJobTitleForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        private void LoadData()
        {
            QuotaJobTitle item = new QuotaJobTitle();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                item = Data.QuotaJobTitleDB.GetQuotaJobTitleByID(_ID);
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

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                QuotaJobTitle item = this.DataContext as QuotaJobTitle;

                item.Detach();
                Save(item);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره سهمیه شغلی", ex);
            }
        }

        #endregion
    }
}
