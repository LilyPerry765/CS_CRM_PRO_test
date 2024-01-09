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
    public partial class JobGroupForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public JobGroupForm()
        {
            InitializeComponent();
            Initialize();
        }

        public JobGroupForm(int id)
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
            JobGroup jobGroup = new JobGroup();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                jobGroup = Data.JobGroupDB.GetJobGroupByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = jobGroup;
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
                JobGroup jobGroup = this.DataContext as JobGroup;

                jobGroup.Detach();
                Save(jobGroup);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره گروه شغلی", ex);
            }
        }

        #endregion
    }
}
