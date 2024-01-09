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
    public partial class Failure117UBDeleteForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public Failure117UBDeleteForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
        }

        private void LoadData()
        {

        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CenterComboBox.SelectedValue == null)
                    throw new Exception("لطفا مرکز مورد نظر را انتخاب نمایید");

                if (UBDate.SelectedDate == null)
                    throw new Exception("لطفا تاریخ مورد نظر را تعیین نمایید");

                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    List<Failure117UB> ubList = Failure117DB.GetFailureUBbyDateandCenter((int)CenterComboBox.SelectedValue, (DateTime)UBDate.SelectedDate);

                    if (ubList != null && ubList.Count != 0)
                    {
                        foreach (Failure117UB item in ubList)
                        {
                            DB.Delete<Failure117UB>(item.ID);
                        }

                        this.DialogResult = true;
                    }
                    else
                        throw new Exception("آیتمی برای حذف موجود نیست");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف، " + ex.Message + " !", ex);
            }
        }

        #endregion
    }
}
