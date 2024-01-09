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
using Stimulsoft.Report;
using System.Windows.Media.Effects;
using CRM.Application.Reports.ReportUserControls;
using System.Reflection;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for FormsList.xaml
    /// </summary>
    public partial class FormsList : Local.TabWindow 
    {
        #region Constructor
        
        public FormsList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Initializer

        public void Initialize()
        {
            RequestTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof( DB.RequestType));
            RequestTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RequestType));
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //StiOptions.Engine.GlobalEvents.SavingReportInDesigner -= new Stimulsoft.Report.Design.StiSavingObjectEventHandler(GlobalEvents_SavingReportInDesigner);
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }
        
        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = Data.DocumentTypeDB.SearchForm(RequestTypeComboBox.SelectedIDs, FormTitleTextBox.Text);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            FormTemplateForm window = new FormTemplateForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Forms item = ItemsDataGrid.SelectedItem as Forms;
                if (item == null) return;

                FormTemplateForm window = new FormTemplateForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Data.Forms item = ItemsDataGrid.SelectedItem as Data.Forms;
                    FormTemplate frmTemplate = new FormTemplate();
                    frmTemplate.ID = item.ID;
                    frmTemplate.RequestTypeID = item.RequestTypeID;
                    frmTemplate.Template = item.Template;
                    frmTemplate.TimeStamp = item.TimeSpam;
                    frmTemplate.Title = item.Title;

                    DB.Delete<Data.FormTemplate>(frmTemplate.ID);

                    ShowSuccessMessage("فرم مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف فرم", ex);
            }
        }


        #endregion
    }
}
