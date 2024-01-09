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
using CRM.Application.UserControls;
using System.Data;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    public partial class Failure117UBMainList : Local.TabWindow
    {
        #region Properties

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        #endregion

        #region Constructor

        public Failure117UBMainList()
        {
            //ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            //popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            //base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ReciveData(object sender, RoutedEventArgs e)
        {
            Failure117UBForm window = new Failure117UBForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            TelephoneNoTextBox.Text = string.Empty;

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            Pager.TotalRecords = Data.Failure117DB.GetFailure117UBInfoListCount(telephoneNo, CenterComboBox.SelectedIDs, UBDate.SelectedDate);
            ItemsBuchtDataGrid.ItemsSource = Data.Failure117DB.GetFailure117UBInfoList(telephoneNo, CenterComboBox.SelectedIDs, UBDate.SelectedDate, startRowIndex, pageSize);
        }

        private void DeleteinDate(object sender, RoutedEventArgs e)
        {
            Failure117UBDeleteForm window = new Failure117UBDeleteForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void DeleteGroupItem(object sender, RoutedEventArgs e)
        {
            ADSLPAPPortDeleteForm window = new ADSLPAPPortDeleteForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                int startRowIndex = 0;
                int pageSize = Pager.TotalRecords;

                long telephoneNo = -1;
                if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                    telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

                DataSet data = Data.Failure117DB.GetFailure117UBInfoList(telephoneNo, CenterComboBox.SelectedIDs, UBDate.SelectedDate, startRowIndex, pageSize).OrderByDescending(t => t.TelephoneNo).ToList().ToDataSet("Result", ItemsBuchtDataGrid);
                Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);

            }
            catch (Exception ex)
            {
                Enterprise.Logger.Write(ex, "خطا در جستجوی ");
                MessageBox.Show("خطا در جستجو", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsBuchtDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm._sumCheckedList = _sumColumn;
            reportSettingForm.ShowDialog();
            _sumColumn = reportSettingForm._sumCheckedList;
            _groupingColumn = reportSettingForm._checkedList;
            _title = reportSettingForm._title;

        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsBuchtDataGrid.Name, ItemsBuchtDataGrid.Columns);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsBuchtDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsBuchtDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        #endregion
    }
}
