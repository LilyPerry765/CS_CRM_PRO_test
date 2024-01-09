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
    public partial class SwitchTypeForm : Local.PopupWindow
    {
        #region Properties

        int _ID;

        #endregion

        #region Constructors

        public SwitchTypeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SwitchTypeForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            SwitchTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchTypeCode));
            TrafficTypeCodeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.TrafficTypeCode));
        }

        private void LoadData()
        {
            SwitchType switchType = new SwitchType();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                switchType = Data.SwitchTypeDB.GetSwitchTypeByID(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = switchType;
        }

        #endregion

        #region Event Handlers
        
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                SwitchType switchType = this.DataContext as SwitchType;

                switchType.Detach();
                Save(switchType);
                
                ShowSuccessMessage("ذخیره سوئیچ انجام شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("ذخیره سوئیچ انجام نشد", ex);
            }
        }

        #endregion
    }
}
