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
    public partial class ADSLPAPPortDeleteForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;
        private int CityID = 0;

        #endregion

        #region Constructors

        public ADSLPAPPortDeleteForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLPAPPortDeleteForm(long id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PAPInfoComboBox.ItemsSource = DB.GetAllEntity<PAPInfo>(); // Data.PAPInfoDB.GetPapInfo();            
        }

        private void LoadData()
        {
            if (CityID == 0)
                CityComboBox.SelectedIndex = 0;
            else
                CityComboBox.SelectedValue = CityID;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void DeleteBucht(object sender, RoutedEventArgs e)
        {
            try
            {
                ADSLPAPPort port = new ADSLPAPPort();

                long fromBucht = 0;
                long toBucht = 0;
                int papInfoID = 0;
                int centerID = 0;
                int rowNo = 0;
                int columnNo = 0;

                if (PAPInfoComboBox.SelectedValue != null)
                    papInfoID = Convert.ToInt32(PAPInfoComboBox.SelectedValue);
                else
                    throw new Exception("لطفا شرکت PAP مورد نظر را انتخاب نمایید");
                if (CenterComboBox.SelectedValue != null)
                    centerID = Convert.ToInt32(CenterComboBox.SelectedValue);
                else
                    throw new Exception("لطفا مرکز مورد نظر را انتخاب نمایید");
                if (!string.IsNullOrWhiteSpace(RowNoTextBox.Text))
                    rowNo = Convert.ToInt32(RowNoTextBox.Text.Trim());
                else
                    throw new Exception("لطفا شماره ردیف مورد نظر را انتخاب نمایید");
                if (!string.IsNullOrWhiteSpace(ColumnNoTextBox.Text))
                    columnNo = Convert.ToInt32(ColumnNoTextBox.Text.Trim());
                else
                    throw new Exception("لطفا شماره ستون مورد نظر را انتخاب نمایید");
                if (!string.IsNullOrEmpty(FromBuchtNoTextBox.Text))
                    fromBucht = Convert.ToInt32(FromBuchtNoTextBox.Text);
                else
                    throw new Exception("لطفا شماره اتصالی را وارد نمایید !");

                if (!string.IsNullOrEmpty(ToBuchtNoTextBox.Text))
                    toBucht = Convert.ToInt32(ToBuchtNoTextBox.Text);

                if (toBucht == 0)
                {
                    port = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(papInfoID, rowNo, columnNo, fromBucht, centerID);

                    if (port != null)
                    {
                        if (port.Status == (byte)DB.ADSLPAPPortStatus.Instal || port.Status == (byte)DB.ADSLPAPPortStatus.Reserve || port.TelephoneNo != null)
                            throw new Exception("پورت مورد نظر دایر می باشد، امکان حذف آن وجود ندارد.");
                        else
                            DB.Delete<ADSLPAPPort>(port.ID);
                    }
                    else
                        throw new Exception("بوخت مورد نظر موجود نمی باشد!");
                }
                else
                {
                    for (long i = fromBucht; i <= toBucht; i++)
                    {
                        port = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(papInfoID, rowNo, columnNo, i, centerID);

                        if (port != null)
                        {
                            if (port.Status == (byte)DB.ADSLPAPPortStatus.Instal || port.TelephoneNo != null)
                                throw new Exception("پورت مورد نظر دایر می باشد، امکان حذف آن وجود ندارد.");
                            if (port.Status == (byte)DB.ADSLPAPPortStatus.Reserve || port.Status == (byte)DB.ADSLPAPPortStatus.Reserve || port.TelephoneNo != null)
                                throw new Exception("پورت مورد نظر رزرو می باشد، امکان حذف آن وجود ندارد.");

                            DB.Delete<ADSLPAPPort>(port.ID);
                        }
                        else
                            throw new Exception("بوخت مورد نظر موجود نمی باشد!");
                    }
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پورت ، " + ex.Message, ex);
            }
        }

        private void PortNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as ADSLPAPPort).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
            }
            else
            {
                if (CityComboBox.SelectedValue == null)
                {
                    City city = Data.CityDB.GetCityById(CityID);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
                else
                {
                    City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
            }
        }

        #endregion
    }
}
