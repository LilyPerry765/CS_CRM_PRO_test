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
    public partial class ADSLInstallCostSingleCenterForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLInstallCostSingleCenterForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLInstallCostSingleCenterForm(int id)
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
            ADSLInstallCostCenterInfo installCost = new ADSLInstallCostCenterInfo();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                installCost = Data.ADSLInstallCostCenterDB.GetADSLInstallCostInfoById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = installCost;
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
                ADSLInstallCostCenterInfo installCostInfo = this.DataContext as ADSLInstallCostCenterInfo;

                ADSLInstalCostCenter installCost = ADSLInstallCostCenterDB.GetADSLInstallCostById(installCostInfo.ID);

                if (!string.IsNullOrWhiteSpace(CostTextbox.Text))
                    installCost.InstallADSLCost = Convert.ToInt64(CostTextbox.Text);

                installCost.Detach();
                Save(installCost);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره هزینه نصب", ex);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
