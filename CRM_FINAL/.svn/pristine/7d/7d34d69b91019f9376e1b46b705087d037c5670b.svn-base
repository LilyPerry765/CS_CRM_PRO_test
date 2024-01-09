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
    public partial class SubsidiaryCodeForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public SubsidiaryCodeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SubsidiaryCodeForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SubsidiaryCodeType));
        }

        private void LoadData()
        {
            SubsidiaryCode subsidiaryCode = new SubsidiaryCode();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                subsidiaryCode = Data.SubsidiaryCodeDB.GetSubsidiaryCodeByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = subsidiaryCode;
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
                if (TypeComboBox.SelectedValue == null)
                    throw new Exception("لطفا نوع شرکت تابعه را تعیین نمایید");

                SubsidiaryCode subsidiaryCode = this.DataContext as SubsidiaryCode;

                subsidiaryCode.Detach();
                Save(subsidiaryCode);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره کد شرکت تابعه، " + ex.Message + " !", ex);
            }
        }

        #endregion
    }
}
