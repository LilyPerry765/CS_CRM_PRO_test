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
    /// <summary>
    /// Interaction logic for ADSLTrafficCostForm.xaml
    /// </summary>
    public partial class ADSLTrafficCostForm : Local.PopupWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public ADSLTrafficCostForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            Less5TextBox.Text = ADSLTrafficBaseCostDB.GetTrafficCostbyID(1).ToString();
            Between5and10TextBox.Text = ADSLTrafficBaseCostDB.GetTrafficCostbyID(2).ToString();
            More10TextBox.Text = ADSLTrafficBaseCostDB.GetTrafficCostbyID(3).ToString();
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
                ADSLTrafficBaseCost cost1 = ADSLTrafficBaseCostDB.GetTrafficCost(1);
                cost1.Cost = Convert.ToInt64(Less5TextBox.Text);
                cost1.Detach();
                DB.Save(cost1);

                ADSLTrafficBaseCost cost2 = ADSLTrafficBaseCostDB.GetTrafficCost(2);
                cost2.Cost = Convert.ToInt64(Between5and10TextBox.Text);
                cost2.Detach();
                DB.Save(cost2);

                ADSLTrafficBaseCost cost3 = ADSLTrafficBaseCostDB.GetTrafficCost(3);
                cost3.Cost = Convert.ToInt64(More10TextBox.Text);
                cost3.Detach();
                DB.Save(cost3);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره", ex);
            }
        }

        #endregion

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
