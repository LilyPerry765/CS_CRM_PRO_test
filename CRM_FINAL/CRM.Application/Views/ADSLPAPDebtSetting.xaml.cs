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

namespace CRM.Application.Views
{
    public partial class ADSLPAPDebtSetting : Local.PopupWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public ADSLPAPDebtSetting()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            DebtTextBox.Text = Data.SettingDB.GetSettingValueByKey("ADSLPAPRequestDebt");
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            Data.Setting debtSetting = Data.SettingDB.GetSettingByKey("ADSLPAPRequestDebt");

            debtSetting.Value = DebtTextBox.Text.Trim();

            debtSetting.Detach();
            Data.DB.Save(debtSetting);

            this.Close();
        }

        private void DebtTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
