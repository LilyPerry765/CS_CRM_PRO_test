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
    public partial class Failure117LineStatusForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public Failure117LineStatusForm()
        {
            InitializeComponent();
            Initialize();
        }

        public Failure117LineStatusForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            LineStatusTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.Failure117LineStatus));
        }

        private void LoadData()
        {
            Failure117LineStatus lineStatus = new Failure117LineStatus();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                lineStatus = Data.Failure117DB.GetLineStatusByID(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = lineStatus;

            if (lineStatus.Type != null)
            {
                LineStatusTypeComboBox.SelectedValue = lineStatus.Type;
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
                Failure117LineStatus lineStatus = this.DataContext as Failure117LineStatus;
                lineStatus.Detach();
                DB.Save(lineStatus);

                ShowSuccessMessage("وضعیت خط ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره وضعیت خط", ex);
            }
        }

        #endregion
    }
}
