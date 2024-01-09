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
using System.Data;
using System.Data.OleDb;
using System.IO;


namespace CRM.Application.Views
{
    public partial class ADSLModemPropertyForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors
        public ADSLModemPropertyForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLModemPropertyForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
            ModemModelComboBox.ItemsSource = ADSLModemDB.GetModemMOdelsCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLModemStatus));
        }

        private void LoadData()
        {
            ADSLModemProperty _ADSLModemProperty = new ADSLModemProperty();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                _ADSLModemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById(_ID);

                if (_ADSLModemProperty.Status == (byte)DB.ADSLModemStatus.Sold)
                {
                    SerailNoTextBox.IsReadOnly = true;
                    CenterComboBox.IsEnabled = false;
                }

                ModemModelComboBox.IsEnabled = false;
                SaveButton.Content = "بروزرسانی";
            }

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

                if(aDSLModemProperty.CenterID==null)
                    throw new Exception("لطفا مرکز را وارد نمایید");

                if (aDSLModemProperty.ADSLModemID == null)
                    throw new Exception("لطفا مدل مودم را وارد نمایید");

                if (string.IsNullOrWhiteSpace(aDSLModemProperty.SerialNo))
                    throw new Exception("لطفا شماره سریال مودم را وارد نمایید");
                
                //if (ADSLModemPropertyDB.GetADSLModemPropertybySerialNo((int)aDSLModemProperty.ADSLModemID, aDSLModemProperty.SerialNo))
                //    throw new Exception("شماره سریال وارد شده تکراری می باشد");

                if (aDSLModemProperty.ID == 0)
                {
                    if (_ID == 0 && aDSLModemProperty.Status == null)
                        aDSLModemProperty.Status = 2;

                    aDSLModemProperty.Detach();
                    Save(aDSLModemProperty);
                }

                if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                {
                    long telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

                    if (ADSLModemPropertyDB.HasADSLModembyTelephoneNo(telephoneNo))
                        throw new Exception("برای این شماره تلفن مودم ذخیر شده است !");

                    aDSLModemProperty.TelephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

                    ADSL aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(TelephoneNoTextBox.Text.Trim()));

                    if (aDSL != null)
                    {
                        aDSL.ModemID = aDSLModemProperty.ID;

                        aDSL.Detach();
                        Save(aDSL);
                    }
                    else
                        throw new Exception("شماره تلفن در سیستم موجود نمی باشد!");
                }
                else
                {
                    aDSLModemProperty.TelephoneNo = null;

                    List<ADSL> aDSLList = ADSLDB.GetADSLbyModemID(aDSLModemProperty.ID);
                    if (aDSLList != null)
                    {
                        foreach (ADSL item in aDSLList)
                        {
                            item.ModemID = null;

                            item.Detach();
                            Save(item);
                        }
                    }
                }
                
                aDSLModemProperty.Detach();
                Save(aDSLModemProperty);

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

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
