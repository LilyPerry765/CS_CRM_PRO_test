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
    public partial class ADSLPortsForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;

        #endregion

        #region Constructors

        public ADSLPortsForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLPortsForm(long id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetAvailableCenterCheckable();
            //EquipmentComboBox.ItemsSource = Data.ADSLEquipmentDB.GetADSLEquipmentCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLPortStatus));
        }

        private void LoadData()
        {
            ADSLPortsInfo aDSLPort = new ADSLPortsInfo();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                aDSLPort = Data.ADSLPortDB.GetADSlPortFullInfo(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = aDSLPort;
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
                ADSLPortsInfo aDSLPortsInfo = this.DataContext as ADSLPortsInfo;

                ADSLPort adslPort = ADSLPortDB.GetADSlPortByID(aDSLPortsInfo.ID);

                if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                {
                    long telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

                    if (ADSLPortDB.HasADSLPortbyTelephoneNo(telephoneNo))
                        throw new Exception("برای این شماره تلفن پورت ذخیر شده است !");

                    adslPort.TelephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text.Trim());

                    ADSL aDSL = ADSLDB.GetADSLByTelephoneNo(Convert.ToInt64(TelephoneNoTextBox.Text.Trim()));

                    if (aDSL != null)
                    {
                        aDSL.ADSLPortID = adslPort.ID;

                        aDSL.Detach();
                        Save(aDSL);
                    }
                    else
                        throw new Exception("شماره تلفن در سیستم موجود نمی باشد!");
                }
                else
                {
                    adslPort.TelephoneNo = null;

                    List<ADSL> aDSLList = ADSLDB.GetADSLbyPortID(adslPort.ID);
                    if (aDSLList != null)
                    {
                        foreach (ADSL item in aDSLList)
                        {
                            item.ADSLPortID = null;

                            item.Detach();
                            Save(item);
                        }
                    }
                }

                adslPort.Status = (byte)Convert.ToInt16(StatusComboBox.SelectedValue);

                adslPort.Detach();
                Save(adslPort);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage(ex.Message, ex);
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
