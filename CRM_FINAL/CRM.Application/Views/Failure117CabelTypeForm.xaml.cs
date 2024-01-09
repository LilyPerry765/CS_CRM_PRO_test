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
    public partial class Failure117CabelTypeForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public Failure117CabelTypeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public Failure117CabelTypeForm(int id)
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
            Failure117CableType cableType = new Failure117CableType();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                cableType = Data.Failue117CableTypeDB.GetCableTypeByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = cableType;
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
                Failure117CableType cableType = this.DataContext as Failure117CableType;

                cableType.Detach();
                Save(cableType);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره نوع کابل", ex);
            }
        }

        #endregion
    }
}
