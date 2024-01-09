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
    public partial class MDFWorkingHoursForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public MDFWorkingHoursForm()
        {
            InitializeComponent();
            Initialize();
        }

        public MDFWorkingHoursForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        private void LoadData()
        {
            MDFWorkingHour workingHours = new MDFWorkingHour();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                workingHours = MDFWorkingHoursDB.GetMDFWorkingHourByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = workingHours;
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
                MDFWorkingHour workingHours = this.DataContext as MDFWorkingHour;

                workingHours.Detach();
                Save(workingHours);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره ساعات کاری MDF", ex);
            }
        }

        #endregion
    }
}
