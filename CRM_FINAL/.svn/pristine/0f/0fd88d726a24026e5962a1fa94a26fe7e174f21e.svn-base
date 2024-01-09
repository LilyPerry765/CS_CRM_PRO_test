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
    public partial class ADSLSellerGroupForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLSellerGroupForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLSellerGroupForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLSellerGroupType));
        }

        private void LoadData()
        {
            ADSLSellerGroup aDSLSellerGroup = new ADSLSellerGroup();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                aDSLSellerGroup = Data.ADSLSellerGroupDB.GetADSLSellerGroupByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = aDSLSellerGroup;
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
                ADSLSellerGroup aDSLSellerGroup = this.DataContext as ADSLSellerGroup;

                if (aDSLSellerGroup.Type == null)
                    throw new Exception("لطفا نوع گروه فروش را تعیین نمایید");

                aDSLSellerGroup.Detach();
                Save(aDSLSellerGroup);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره گروه فروش ADSL، " + ex.Message + " !", ex);
            }
        }

        #endregion
    }
}
