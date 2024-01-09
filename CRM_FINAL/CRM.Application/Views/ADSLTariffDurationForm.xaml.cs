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
    public partial class ADSLTariffDurationForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLTariffDurationForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLTariffDurationForm(int id)
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
            ADSLServiceDuration duration = new ADSLServiceDuration();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                duration = Data.ADSLServiceDB.GetADSLServiceDurationByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = duration;
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
                ADSLServiceDuration duration = this.DataContext as ADSLServiceDuration;

                duration.Detach();
                Save(duration);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره مدت زمان استفاده ADSL", ex);
            }
        }

        private void Textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
