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
    public partial class ADSLServiceGiftProfileForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLServiceGiftProfileForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLServiceGiftProfileForm(int id)
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
            ADSLServiceGiftProfile aDSLServiceGift = new ADSLServiceGiftProfile();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                aDSLServiceGift = Data.ADSLServiceDB.GetADSLServiceGiftProfileByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = aDSLServiceGift;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                ADSLServiceGiftProfile aDSLServiceGift = this.DataContext as ADSLServiceGiftProfile;

                aDSLServiceGift.Detach();
                Save(aDSLServiceGift);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پروفایل هدیه", ex);
            }
        }

        #endregion
    }
}
