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
    public partial class ADSLModemPropertyGroupForm : Local.PopupWindow
    {
        #region Properties
        
        #endregion

        #region Constructors

        public ADSLModemPropertyGroupForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
            ModemModelComboBox.ItemsSource = ADSLModemDB.GetModemMOdelsCheckable();
        }

        private void LoadData()
        {
            ADSLModemProperty _ADSLModemProperty = new ADSLModemProperty();

            this.DataContext = _ADSLModemProperty;
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
                ADSLModemProperty aDSLModemProperty = this.DataContext as ADSLModemProperty;

                if (aDSLModemProperty.CenterID == null)
                    throw new Exception("لطفا مرکز را وارد نمایید");

                if (aDSLModemProperty.ADSLModemID == null)
                    throw new Exception("لطفا مدل مودم را وارد نمایید");

                if (string.IsNullOrWhiteSpace(FixSerialNoTextBox.Text))
                    throw new Exception("لطفا بخش ثابت شماره سریال مودم را وارد نمایید");

                //if (ADSLModemPropertyDB.GetADSLModemPropertybySerialNo((int)aDSLModemProperty.ADSLModemID, aDSLModemProperty.SerialNo))
                //    throw new Exception("شماره سریال وارد شده تکراری می باشد");

                long fromNo = 0;
                long toNo = 0;

                if (!string.IsNullOrEmpty(FromSerailNoTextBox.Text))
                    fromNo = Convert.ToInt32(FromSerailNoTextBox.Text);
                else
                    throw new Exception("لطفا ابتدای شماره سریال را وارد نمایید !");

                if (!string.IsNullOrEmpty(ToSerailNoTextBox.Text))
                    toNo = Convert.ToInt32(ToSerailNoTextBox.Text);
                else
                    throw new Exception("لطفا انتهای شماره سریال را وارد نمایید !");


                for (long i = fromNo; i <= toNo; i++)
                {
                    aDSLModemProperty.ID = 0;                    
                    aDSLModemProperty.SerialNo = FixSerialNoTextBox.Text + i.ToString();
                    aDSLModemProperty.MACAddress = null;
                    aDSLModemProperty.Status = 2;

                    if (!ADSLModemPropertyDB.GetADSLModemPropertybySerialNo((int)aDSLModemProperty.ADSLModemID, aDSLModemProperty.SerialNo))
                    {
                        aDSLModemProperty.Detach();
                        Save(aDSLModemProperty);
                    }
                    else
                        throw new Exception("شماره سریال " + aDSLModemProperty.SerialNo + "  تکراری می باشد");
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره  ویژگی های مودم ADSL، " + ex.Message + " !", ex);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);

            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion
    }
}
