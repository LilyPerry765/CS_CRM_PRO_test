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
    public partial class ADSLTariffTrafficForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLTariffTrafficForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLTariffTrafficForm(int id)
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
            ADSLServiceTraffic traffic = new ADSLServiceTraffic();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                traffic = Data.ADSLServiceDB.GetADSLServiceTraffiBycID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = traffic;
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
                ADSLServiceTraffic traffic = this.DataContext as ADSLServiceTraffic;

                traffic.Detach();
                Save(traffic);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره ترافیک مصرفی ADSL", ex);
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
