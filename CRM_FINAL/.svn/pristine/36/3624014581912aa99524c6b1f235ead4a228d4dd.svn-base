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
    public partial class CableColorForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public CableColorForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CableColorForm(int id)
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
            CableColor cableColor = new CableColor();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                cableColor = Data.CableColorDB.GetCableColorByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = cableColor;
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
                CableColor cableColor = this.DataContext as CableColor;

                cableColor.Detach();
                Save(cableColor);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {

                ShowErrorMessage("خطا در ذخیره رنگ کابل", ex);
            }
        }

        #endregion
    }
}
