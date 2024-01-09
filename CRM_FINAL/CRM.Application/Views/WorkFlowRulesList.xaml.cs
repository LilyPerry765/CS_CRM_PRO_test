using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;

namespace CRM.Application.Views
{
    public partial class WorkFlowRulesList : Local.TabWindow
    {

        #region Constructor & Fields

        public WorkFlowRulesList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods

        private void Initialize()
        {
            RequestTypesDataGrid.ItemsSource = Data.TypesDB.GetRequestTypes();
            WorkFlowVersionColumn.ItemsSource = Data.WorkFlowDB.GetVersionsCheckable();
            List<CheckableItem> WorkUnits = Data.WorkUnitDB.GetWorkUnitsCheckable(true);
            SenderColumn.ItemsSource = WorkUnits;
            RecieverColumn.ItemsSource = WorkUnits;
            FormColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Form)).OrderBy(cl => cl.Name);
            ActionColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Action));
            SpecialConditionsColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SpecialConditions));
        }

        public void LoadData()
        {
            if (RequestTypesDataGrid.SelectedIndex < 0) return;

            int requestTypeID = (RequestTypesDataGrid.SelectedItem as RequestType).ID;

            List<CheckableItem> statusReasons = Data.StatusDB.GetStatesNameValue(requestTypeID);
            CurrentStatusColumn.ItemsSource = statusReasons;
            NextStatusColumn.ItemsSource = statusReasons;

            RulesDataGrid.ItemsSource = WorkFlowDB.GetWorkFlowRules(requestTypeID);
        }

        #endregion Load Methods

        #region Event Handlers

        private void RequestTypeTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RequestTypesDataGrid.Items.Filter = new Predicate<object>(x => (x as RequestType).Title.Contains(RequestTypeTitleTextBox.Text.Trim().ToLower()));

            if (string.IsNullOrEmpty(RequestTypeTitleTextBox.Text.Trim()) && RequestTypesDataGrid.SelectedItem != null)
            {
                RequestTypesDataGrid.ScrollIntoView(RequestTypesDataGrid.SelectedItem);
            }
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void RequestTypesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RequestTypesDataGrid.SelectedIndex >= 0 && (RequestTypesDataGrid.SelectedItem as Data.RequestType) != null)
            {
                Data.RequestType requestType = (RequestTypesDataGrid.SelectedItem as Data.RequestType);
                LoadData();
            }
            else
            {
                RulesDataGrid.ItemsSource = null;
            }
        }


        private void DeleteRule(object sender, RoutedEventArgs e)
        {
            if (RulesDataGrid.SelectedIndex < 0 || RulesDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Data.WorkFlowDB.DeleteWorkFlowRule((RulesDataGrid.SelectedItem as Data.WorkFlowRule));
                    //ShowSuccessMessage("چرخه کاری مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                //ShowErrorMessage("خطا در حذف چرخه کاری", ex);
            }

        }

        private void RulesDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                Data.WorkFlowRule workFlowRule = e.Row.Item as Data.WorkFlowRule;

                workFlowRule.RequestTypeID = (RequestTypesDataGrid.SelectedItem as Data.RequestType).ID;

                if (workFlowRule.SenderID == -1) workFlowRule.SenderID = null;
                if (workFlowRule.RecieverID == -1) workFlowRule.RecieverID = null;
                if (workFlowRule.SpecialConditionsID == -1) workFlowRule.SpecialConditionsID = null;


                workFlowRule.Detach();
                DB.Save(workFlowRule);
                RequestTypesDataGrid_SelectionChanged(sender, null);
                ShowSuccessMessage("چرخه کاری ذخیره شده");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره چرخه کاری", ex);
            }
        }

        #endregion Event Handlers

    }
}
