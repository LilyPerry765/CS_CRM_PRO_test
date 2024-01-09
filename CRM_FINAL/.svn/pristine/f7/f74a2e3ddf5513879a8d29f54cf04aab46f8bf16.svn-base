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
    public partial class TelephoneTempForm : Local.PopupWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public TelephoneTempForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
        }

        private void LoadData()
        {
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
                long fromTelephoneNo = 0;
                long toTelephoneNo = 0;

                if (!string.IsNullOrWhiteSpace(FromTelephoneNoTextBox.Text))
                    fromTelephoneNo = Convert.ToInt64(FromTelephoneNoTextBox.Text);
                else
                    throw new Exception("لطفا شماره تلفن شروع رنج را وارد نمایید!");

                if (!string.IsNullOrWhiteSpace(ToTelephoneNoTextBox.Text))
                    toTelephoneNo = Convert.ToInt64(ToTelephoneNoTextBox.Text);
                else
                    throw new Exception("لطفا شماره تلفن پایان رنج را وارد نمایید!");

                if (fromTelephoneNo > toTelephoneNo)
                    throw new Exception("شروع رنج از پایان رنج بزرگتر است!");

                if (CenterComboBox.SelectedValue == null)
                    throw new Exception("لطفا مرکز را انتخاب نمایید!");

                int centerID = (int)CenterComboBox.SelectedValue;

                TelephoneTemp temp = null;
                while (fromTelephoneNo <= toTelephoneNo)
                {
                    temp = new TelephoneTemp();

                    temp.TelephoneNo = fromTelephoneNo;
                    temp.CenterID = centerID;

                    if (TelephoneDB.HasTelephoneTemp(fromTelephoneNo))
                    {
                        temp = TelephoneDB.GetTelephoneTemp(fromTelephoneNo);
                        temp.CenterID = centerID;

                        temp.Detach();
                        DB.Save(temp);
                    }
                    else
                    {
                        temp.Detach();
                        DB.Save(temp, true);
                    }

                    fromTelephoneNo = fromTelephoneNo + 1;
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        #endregion
    }
}
