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
    public partial class ADSLIPGroupForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLIPGroupForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLIPGroupForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLIPGroupType));
        }
        
        private void LoadData()
        {
            ADSLIPType aDSLIPType = new ADSLIPType();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                aDSLIPType = Data.ADSLIPDB.GetADSLIPTypeByID(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = aDSLIPType;
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
                ADSLIPType aDSLIPType = this.DataContext as ADSLIPType;
                aDSLIPType.Detach();
                Save(aDSLIPType);

                ShowSuccessMessage("گروه IP ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره گروه IP", ex);
            }
        }

        #endregion
    }
}
