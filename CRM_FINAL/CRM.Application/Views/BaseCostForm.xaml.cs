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
    public partial class BaseCostForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        
        BaseCost _baseCost { get; set; }

        #endregion

        #region Constructors

        public BaseCostForm()
        {
            InitializeComponent();
            Initialize();
        }

        public BaseCostForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestTypeComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
            QuotaDiscountComboBox.ItemsSource = Data.QuotaDiscountDB.GetQuotaDiscountCheckable();
            ChargingTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ChargingGroup));
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PaymentType));
            //WorkUnitComboBox.ItemsSource = Data.WorkUnitDB.GetWorkUnitCheckable();
        }

        private void LoadData()
        {

            _baseCost = new BaseCost();
            _baseCost.UseOutBound = false;
            _baseCost.UseZeroBlock = false;
            _baseCost.UseCableMeter = false;


            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                _baseCost = Data.BaseCostDB.GetBaseCostByID(_ID);

                if (_baseCost.ID == (int)DB.SpecialCostID.PrePaymentTypeCostID)
                {
                    this.IsEnabled = false;
                }
                else if (_baseCost.ID == (int)DB.SpecialCostID.SpecialServiceTypeCostID)
                {
                    foreach (UIElement control in MainGrid.Children)
                    {
                        control.IsEnabled = false;
                    }
                    PaymentTypeComboBox.IsEnabled = true;
                    TaxTextBox.IsEnabled = true;
                    IsActiveComboBox.IsEnabled = true;
                }
                else if (_baseCost.ID == (int)DB.SpecialCostID.BetweenCenterSpecialWireCostID)
                {
                    foreach (UIElement control in MainGrid.Children)
                    {
                        control.IsEnabled = false;
                    }
                    PaymentTypeComboBox.IsEnabled = true;
                    TaxTextBox.IsEnabled = true;
                    IsActiveComboBox.IsEnabled = true;
                }
                else if (_baseCost.ID == (int)DB.SpecialCostID.OpenSecondZero || _baseCost.ID == (int)DB.SpecialCostID.BlockSecondZero)
                {
                    foreach (UIElement control in MainGrid.Children)
                    {
                        control.IsEnabled = false;
                    }

                    CostTextBox.IsEnabled = true;
                    PaymentTypeComboBox.IsEnabled = true;
                    TaxTextBox.IsEnabled = true;
                    IsActiveComboBox.IsEnabled = true;
                }

                if (_baseCost.PaymentType == (int)DB.PaymentType.Instalment)
                {
                    ChequeLabel.Visibility = Visibility.Visible;
                    ChequeCheckBox.Visibility = Visibility.Visible;
                }
                else if (Convert.ToInt32(PaymentTypeComboBox.SelectedValue) == (int)DB.PaymentType.Cash)
                {
                    AccountNumberLabel.Visibility = Visibility.Visible;
                    AccountNumberTextBox.Visibility = Visibility.Visible;
                }
                else
                {
                    AccountNumberLabel.Visibility = Visibility.Collapsed;
                    AccountNumberTextBox.Visibility = Visibility.Collapsed;
                    ChequeLabel.Visibility = Visibility.Collapsed;
                    ChequeCheckBox.Visibility = Visibility.Collapsed;
                }

                // ----- مربوط به هزینه کارشناس در روال ADSL -----
                if (_baseCost.ID == 37)
                    TitleTextBox.IsReadOnly = true;
                // ----- End.

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = _baseCost;
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
                BaseCost item = this.DataContext as BaseCost;
                if (item.IsFormula == true && (item.Formula == null || item.Formula == string.Empty))
                {
                    throw new Exception("لطفا فرمول را وارد کنید.");
                }
                item.Detach();
                Save(item);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                   ShowErrorMessage("خطا در ذخیره هزینه ! ", ex);
            }
        }

        private void PaymentTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PaymentTypeComboBox.SelectedValue != null)
            {
                if (Convert.ToInt32(PaymentTypeComboBox.SelectedValue) == (int)DB.PaymentType.Instalment)
                {
                    ChequeLabel.Visibility = Visibility.Visible;
                    ChequeCheckBox.Visibility = Visibility.Visible;
                }
                else if(Convert.ToInt32(PaymentTypeComboBox.SelectedValue) == (int)DB.PaymentType.Cash)
                {
                    AccountNumberLabel.Visibility = Visibility.Visible;
                    AccountNumberTextBox.Visibility = Visibility.Visible;
                }
                else
                {
                    ChequeCheckBox.IsChecked = false;
                    AccountNumberTextBox.Text = string.Empty;
                    AccountNumberLabel.Visibility = Visibility.Collapsed;
                    AccountNumberTextBox.Visibility = Visibility.Collapsed;
                    ChequeLabel.Visibility = Visibility.Collapsed;
                    ChequeCheckBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void FormulaEditorButton_Click(object sender, RoutedEventArgs e)
        {

            FormulaEditorForm formulaEditorForm = new FormulaEditorForm(_baseCost.Formula);
            formulaEditorForm.ShowDialog();
            if (formulaEditorForm.Formula != null)
            {
                (this.DataContext as BaseCost).Formula = (string)formulaEditorForm.Formula.Trim();
                (this.DataContext as BaseCost).UseOutBound = formulaEditorForm.UseOutBound;
                (this.DataContext as BaseCost).UseCableMeter = formulaEditorForm.UseCableMeter;
                (this.DataContext as BaseCost).UseZeroBlock = formulaEditorForm.UseZeroBlock;
            }



        }

        #endregion
    }
}
