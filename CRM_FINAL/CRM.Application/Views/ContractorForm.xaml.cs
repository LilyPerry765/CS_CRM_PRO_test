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
    public partial class ContractorForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ContractorForm()
        {
            InitializeComponent();
        }

        public ContractorForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            Contractor contractor = new Contractor();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                contractor = Data.ContractorDB.GetContractorById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = contractor;
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
                Contractor contractor = this.DataContext as Contractor;

                contractor.Detach();
                DB.Save(contractor);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پیمانکار", ex);
            }
        }

        #endregion
    }
}
