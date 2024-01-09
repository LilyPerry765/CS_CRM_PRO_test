using CRM.Data;
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
    /// <summary>
    /// Interaction logic for ReportSettingForm.xaml
    /// </summary>
    public partial class ReportSettingForm : Local.PopupWindow
    {
        private List<DataGridColumn> _dataGridColumn;
        List<DataGridSelectedIndex> _dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        public List<DataGridSelectedIndex> _checkedList = new List<DataGridSelectedIndex>();
        public List<DataGridSelectedIndex> _sumCheckedList = new List<DataGridSelectedIndex>();
        public string _title = string.Empty;
        public ReportSettingForm()
        {
            InitializeComponent();
        }
        public ReportSettingForm(List<DataGridColumn> dataGridColumn):this()
        {
            this._dataGridColumn = dataGridColumn;
        }
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {


            _dataGridColumn.ForEach(t=>
            {
                 if (t.ClipboardContentBinding != null)
                  {
                     string columnName = ((System.Windows.Data.Binding)(t.ClipboardContentBinding)).Path.Path.ToString();
                     _dataGridSelectedIndexs.Add(new DataGridSelectedIndex { bindingPath = columnName, Header = t.Header.ToString(), Index = t.DisplayIndex});
                  }
            });
            ColumnList.ItemsSource = _dataGridSelectedIndexs;
            SumList.ItemsSource    = _dataGridSelectedIndexs;

            this.TitleTextBox.Text = _title;

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox item = sender as CheckBox;
            if (!_checkedList.Any(t => t.bindingPath == item.Tag.ToString()))
            {
                _checkedList.Add(new DataGridSelectedIndex { bindingPath = item.Tag.ToString(), Header = item.Content.ToString() });
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox item = sender as CheckBox;
            if (_checkedList.Any(t => t.bindingPath == item.Tag.ToString()))
            {
                _checkedList.RemoveAll(t => t.bindingPath.ToString() == item.Tag.ToString());
            }
        }


        private void TitleTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _title = TitleTextBox.Text;
        }

        private void PopupWindow_ContentRendered(object sender, EventArgs e)
        {
            UIElement container = this.Content as UIElement;
            #region
            ListBox listBox = Helper.FindVisualChildByName<ListBox>(container, "ColumnList");
            List<CheckBox> checkBoxs = Helper.FindVisualChildren<CheckBox>(listBox).ToList();
            checkBoxs.Where(t => _checkedList.Select(t2=>t2.bindingPath).Contains(t.Tag)).ToList().ForEach(t => t.IsChecked = true);
            #endregion

            #region
            ListBox sumListBox = Helper.FindVisualChildByName<ListBox>(container, "SumList");
            List<CheckBox> sumCheckBoxs = Helper.FindVisualChildren<CheckBox>(listBox).ToList();
            var x = sumCheckBoxs.Where(t => _sumCheckedList.Select(t2 => t2.bindingPath).Contains(t.Tag)).ToList();
            sumCheckBoxs.Where(t => _sumCheckedList.Select(t2 => t2.bindingPath).Contains(t.Tag)).ToList().ForEach(t => t.IsChecked = true);
            #endregion



        }

        private void SumCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            CheckBox item = sender as CheckBox;
            if (!_sumCheckedList.Any(t => t.bindingPath == item.Tag.ToString()))
            {
                _sumCheckedList.Add(new DataGridSelectedIndex { bindingPath = item.Tag.ToString(), Header = item.Content.ToString() });
            }
            
        }

        private void SumCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox item = sender as CheckBox;
            if (_sumCheckedList.Any(t => t.bindingPath == item.Tag.ToString()))
            {
                _sumCheckedList.RemoveAll(t => t.bindingPath.ToString() == item.Tag.ToString());
            }
        }
    }
}
