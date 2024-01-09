using CRM.Application.Local;
using CRM.Data;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TelecomminucationServiceForm.xaml
    /// </summary>
    public partial class TelecomminucationServiceForm : PopupWindow
    {
        #region Properties and Fields

        private long _ID = 0;

        #endregion

        #region Constructor

        public TelecomminucationServiceForm()
        {
            InitializeComponent();
            this.Initialize();
        }

        public TelecomminucationServiceForm(long id)
            : this()
        {
            this._ID = id;
        }

        #endregion

        #region EventHandlers

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(TitleTextBox.Text.Trim()))
                {
                    throw new ArgumentException(".تعیین عنوان کالا/خدمات الزامی است");
                }
                if (string.IsNullOrEmpty(UnitPriceNumericTextBox.Text.Trim()))
                {
                    throw new ArgumentException(".تعیین قیمت واحد کالا/خدمات الزامی است");
                }
                if (string.IsNullOrEmpty(TaxNumericTextBox.Text.Trim()))
                {
                    throw new ArgumentException(".تعیین مالیات الزامی است");
                }
                if (RequestTypesComboBox.SelectedValue == null)
                {
                    throw new ArgumentException(".تعیین نوع درخواست الزامی است");
                }
                if (UnitMeasuresComboBox.SelectedValue == null)
                {
                    throw new ArgumentException(".تعیین واحد اندازه گیری الزامی است");
                }

                TelecomminucationService telecomminucationService = this.DataContext as TelecomminucationService;
                telecomminucationService.Detach();
                Save(telecomminucationService);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                {
                    ShowErrorMessage(".مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در ذخیره کالا/خدمات", ex);
                }
            }
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestTypesComboBox.ItemsSource = RequestTypeDB.GetAllEntity();
            UnitMeasuresComboBox.ItemsSource = UnitMeasureDB.GetUnitMeasures();
        }

        public void LoadData()
        {
            TelecomminucationService item = new TelecomminucationService();
            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = TelecomminucationServiceDB.GetTelecomminucationServiceByID(this._ID);
                SaveButton.Content = "بروزرسانی";
            }
            this.DataContext = item;
        }

        #endregion

    }
}
