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
    public partial class PAPInfoCostForm : Local.PopupWindow
    {
        #region Properties

        private int _CostID = 0;

        #endregion

        #region Constructors

        public PAPInfoCostForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PAPInfoCostForm(int id)
            : this()
        {
            _CostID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        private void LoadData()
        {
            PAPCostInfo pAPInfoCost = new PAPCostInfo();

            if (_CostID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                pAPInfoCost = Data.PAPInfoCostDB.GetPAPInfoCostById(_CostID);

                ItemsDataGrid.ItemsSource = PAPInfoCostDB.SearchPAPCostHistories(_CostID).OrderByDescending(t => t.ID);

                TitleTextBox.IsReadOnly = true;
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = pAPInfoCost;
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
                if (StartDate.SelectedDate == null)
                    throw new Exception("لطفا تاریخ شروع را تعیین نمایید");
                else
                {
                    if (_CostID == 0)
                    {
                        PAPInfoCost cost = new PAPInfoCost();
                        cost.Title = TitleTextBox.Text;

                        cost.Detach();
                        Save(cost, true);

                        _CostID = cost.ID;
                    }

                    PAPInfoCostHistory currnetCostHistory = new PAPInfoCostHistory();
                    PAPInfoCostHistory oldCostHistory = PAPInfoCostDB.LastPAPCostHistory(_CostID);
                    if (oldCostHistory != null)
                    {
                        DateTime startDate = (DateTime)StartDate.SelectedDate;
                        oldCostHistory.EndDate = startDate.AddDays(-1);

                        oldCostHistory.Detach();
                        Save(oldCostHistory);
                    }

                    currnetCostHistory.CostID = _CostID;
                    currnetCostHistory.Value = ValueTextBox.Text;
                    currnetCostHistory.StartDate = (DateTime)StartDate.SelectedDate;

                    currnetCostHistory.Detach();
                    Save(currnetCostHistory);

                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره هزینه های شرکت ،  PAP" + ex.Message + " !", ex);
            }
        }

        private void ValueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
