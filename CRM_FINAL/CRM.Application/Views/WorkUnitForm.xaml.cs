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
    public partial class WorkUnitForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public WorkUnitForm()
        {
            InitializeComponent();
        }

        public WorkUnitForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            WorkUnit workunit = new WorkUnit();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                workunit = WorkUnitDB.GetWorkunitById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = workunit;
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
                WorkUnit workunit = this.DataContext as WorkUnit;
                workunit.Detach();
                Save(workunit);

                ShowSuccessMessage("واحد مسئول ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره واحد مسئول", ex);
            }
        }

        #endregion
    }
}
