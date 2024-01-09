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
    public partial class ADSLMDFRangeForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int _MDFID = 0;

        #endregion

        #region Constructors

        public ADSLMDFRangeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLMDFRangeForm(int mDFID)
            : this()
        {
            _MDFID = mDFID;
        }

        #endregion

        #region Methods

        private void Initialize()
        {

        }

        private void LoadData()
        {
            if (_MDFID != 0)
            {
                CenterTextBox.Text = ADSLMDFRangeDB.GetADSLMDFInfobyMDFID(_MDFID).Center;
                MDFTextBox.Text = ADSLMDFRangeDB.GetADSLMDFInfobyMDFID(_MDFID).MDFTitle;

                ItemsDataGrid.ItemsSource = Data.ADSLMDFRangeDB.GetADSLMDFRangebyMDFID(_MDFID);
            }
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
                if (StartRangeTextBox.Text.Trim().Count() != 10)
                    throw new Exception("لطفا برای شروع رنج شماره 10 رقمی صحیح را وارد نمایید");
                if (EndRangeTextBox.Text.Trim().Count() != 10)
                    throw new Exception("لطفا برای پایان رنج شماره 10 رقمی صحیح را وارد نمایید");

                long startRange =Convert.ToInt64(StartRangeTextBox.Text.Trim());
                long endRange =Convert.ToInt64(EndRangeTextBox.Text.Trim());

                ADSLMDFRange mDFRange = new ADSLMDFRange();

                if (_ID != 0)
                    mDFRange.ID = _ID;

                List<ADSLMDFRange> mDFRangeList = ADSLMDFRangeDB.GetADSLMDFRangebyMDFID(_MDFID);

                foreach (ADSLMDFRange item in mDFRangeList)
                {
                    if (startRange <= item.StartTelephoneNo && endRange >= item.EndTelephoneNo)
                        throw new Exception("رنج تکراری می باشد");

                    if (startRange >= item.StartTelephoneNo && startRange <= item.EndTelephoneNo)
                        throw new Exception("رنج تکراری می باشد");

                    if (endRange >= item.StartTelephoneNo && endRange <= item.EndTelephoneNo)
                        throw new Exception("رنج تکراری می باشد");

                    if (startRange >= item.StartTelephoneNo && endRange <= item.EndTelephoneNo)
                        throw new Exception("رنج تکراری می باشد");
                }

                mDFRange.MDFID = _MDFID;
                mDFRange.StartTelephoneNo = startRange;
                mDFRange.EndTelephoneNo = endRange;

                mDFRange.Detach();
                Save(mDFRange);

                ShowSuccessMessage("رنج ذخیره شد");

                LoadData();

                _ID = 0;
                StartRangeTextBox.Text = string.Empty;
                EndRangeTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره رنج،"+ex.Message+" !", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLMDFRange item = ItemsDataGrid.SelectedItem as ADSLMDFRange;

                if (item == null)
                    return;

                _ID = item.ID;
                StartRangeTextBox.Text = item.StartTelephoneNo.ToString();
                EndRangeTextBox.Text = item.EndTelephoneNo.ToString();
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}")
                return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ADSLMDFRange item = ItemsDataGrid.SelectedItem as ADSLMDFRange;
                    DB.Delete<ADSLMDFRange>(item.ID);

                    ShowSuccessMessage("رنج مورد نظر حذف شد");

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف شهر", ex);
            }
        }

        private void RangePreviewTextInput(object sender, TextCompositionEventArgs e)
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
