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
    public partial class WorkFlowRuleForm : Local.PopupWindow
    {
        private int _ID = 0;

        public WorkFlowRuleForm()
        {
            InitializeComponent();
            Initialize();
        }

        public WorkFlowRuleForm(int id)
            : this()
        {
            _ID = id;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
            CurrentStatusComboBox.ItemsSource = Data.StatusDB.GetStatusCheckable();
            NextStatusComboBox.ItemsSource = Data.StatusDB.GetStatusCheckable();
            WorkFlowVersionComboBox.ItemsSource = Data.WorkFlowVersionDB.GetVersionsCheckable();
            RequestTypeComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
            SenderComboBox.ItemsSource = Data.WorkUnitDB.GetWorkUnitCheckable();
            RecieverComboBox.ItemsSource = Data.WorkUnitDB.GetWorkUnitCheckable();
            WorkFlowRuleComboBox.ItemsSource = Data.WorkFlowRuleDB.GetWorkFlowRuleCheckable();

        }

        private void LoadData()
        {
            WorkFlowRule item = new WorkFlowRule();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.WorkFlowRuleDB.GetWorkFlowRuleByID(_ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                WorkFlowRule item = this.DataContext as WorkFlowRule;

                item.Detach();
                Save(item);

                ShowSuccessMessage("WorkFlowRule ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره WorkFlowRule", ex);
            }
        }
    }
}
