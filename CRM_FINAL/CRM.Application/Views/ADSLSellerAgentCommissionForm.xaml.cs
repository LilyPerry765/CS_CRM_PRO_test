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
    public partial class ADSLSellerAgentCommissionForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLSellerAgentCommissionForm()
        {
            InitializeComponent();
        }

        public ADSLSellerAgentCommissionForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        

        private void LoadData()
        {
            ADSLSellerAgent aDSLSellerAgent = new ADSLSellerAgent();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                aDSLSellerAgent = Data.ADSLSellerGroupDB.GetADSLSellerAgentByID(_ID);

                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = aDSLSellerAgent;
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
                ADSLSellerAgent aDSLSellerAgent = this.DataContext as ADSLSellerAgent;

                aDSLSellerAgent.Detach();
                Save(aDSLSellerAgent);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پورسانت فروش", ex);
            }
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
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
