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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class CycleForm : Local.PopupWindow
    {
        #region Peroperties

        private int _ID = 0;

        #endregion

        #region Constructors

        public CycleForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CycleForm(int id)
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
            Cycle cycle = new Cycle();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                cycle = Data.CycleDB.GetCycleById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = cycle;
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
                Cycle cycle = this.DataContext as Cycle;



                if (CRM.Data.CycleDB.CheckOverlappingDateRanges(cycle))
                    throw new Exception("خطای بر هم افتادگی محدوده تاریخ");


                    cycle.Detach();
                    DB.Save(cycle);

                    ShowSuccessMessage("دوره ذخیره شد");
                    this.DialogResult = true;
                
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                ShowErrorMessage("خطا در ذخیره بانک", ex);
            }
        }


        #endregion
    }
}
