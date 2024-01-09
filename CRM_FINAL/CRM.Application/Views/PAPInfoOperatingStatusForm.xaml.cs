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
    public partial class PAPInfoOperatingStatusForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public PAPInfoOperatingStatusForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PAPInfoOperatingStatusForm(int id)
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
            PAPInfoOperatingStatus pAPInfoOperatingStatus = new PAPInfoOperatingStatus();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                pAPInfoOperatingStatus = Data.PAPInfoOperatingStatusDB.GetOperatingStatusByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = pAPInfoOperatingStatus;
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
                PAPInfoOperatingStatus pAPInfoOperatingStatus = this.DataContext as PAPInfoOperatingStatus;

                pAPInfoOperatingStatus.Detach();
                Save(pAPInfoOperatingStatus);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره دلایل مجوز شرکت های PAP", ex);
            }
        }

        #endregion
    }
}
