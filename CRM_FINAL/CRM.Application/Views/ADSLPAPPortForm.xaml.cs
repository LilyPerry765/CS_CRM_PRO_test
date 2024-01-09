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
    public partial class ADSLPAPPortForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;
        private int CityID = 0;

        #endregion

        #region Constructors

        public ADSLPAPPortForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLPAPPortForm(long id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            if (DB.City == "semnan")
            {
                PortGrid.Visibility = Visibility.Visible;
                BuchtGrid.Visibility = Visibility.Collapsed;
            }

            if (DB.City == "kermanshah" || DB.City == "gilan")
            {
                PortGrid.Visibility = Visibility.Collapsed;
                BuchtGrid.Visibility = Visibility.Visible;
            }

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PAPInfoComboBox.ItemsSource = DB.GetAllEntity<PAPInfo>(); // Data.PAPInfoDB.GetPapInfo();
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLPAPPortStatus));
        }

        private void LoadData()
        {
            ADSLPAPPort port = new ADSLPAPPort();

            if (DB.City == "semnan")
            {
                if (_ID == 0)
                {
                    PortNoLabel.Visibility = Visibility.Collapsed;
                    PortNoTextBox.Visibility = Visibility.Collapsed;
                    PortNoRow.Height = GridLength.Auto;

                    SaveButton.Content = "ذخیره";
                }
                else
                {
                    FromPortNoLabel.Visibility = Visibility.Collapsed;
                    FromPortNoTextBox.Visibility = Visibility.Collapsed;
                    ToPortNoLabel.Visibility = Visibility.Collapsed;
                    ToPortNoTextBox.Visibility = Visibility.Collapsed;
                    FromPortNoRow.Height = GridLength.Auto;
                    ToPortNoRow.Height = GridLength.Auto;

                    port = Data.ADSLPAPPortDB.GetADSLPAPPortById(_ID);
                    SaveButton.Content = "بروزرسانی";

                    CityID = Data.ADSLPAPPortDB.GetCity(port.ID);
                }
            }

            if (DB.City == "kermanshah" || DB.City == "gilan")
            {
                if (_ID == 0)
                {
                    RowNoTextBox.IsReadOnly = false;
                    ColumnNoTextBox.IsReadOnly = false;
                    BuchtNoLabel.Visibility = Visibility.Collapsed;
                    BuchtNoTextBox.Visibility = Visibility.Collapsed;
                    BuchtNoRow.Height = GridLength.Auto;

                    SaveButton.Content = "ذخیره";
                }
                else
                {
                    RowNoTextBox.IsReadOnly = true;
                    ColumnNoTextBox.IsReadOnly = true;
                    FromBuchtNoLabel.Visibility = Visibility.Collapsed;
                    FromBuchtNoTextBox.Visibility = Visibility.Collapsed;
                    ToBuchtNoLabel.Visibility = Visibility.Collapsed;
                    ToBuchtNoTextBox.Visibility = Visibility.Collapsed;
                    FromBuchtNoRow.Height = GridLength.Auto;
                    ToBuchtNoRow.Height = GridLength.Auto;

                    port = Data.ADSLPAPPortDB.GetADSLPAPPortById(_ID);
                    SaveButton.Content = "بروزرسانی";

                    CityID = Data.ADSLPAPPortDB.GetCity(port.ID);
                }
            }

            this.DataContext = port;

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

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                ADSLPAPPort port = this.DataContext as ADSLPAPPort;

                if (DB.City == "semnan")
                {
                    long fromPort = 0;
                    long toPort = 0;

                    if (_ID == 0)
                    {
                        if (!string.IsNullOrEmpty(FromPortNoTextBox.Text))
                            fromPort = Convert.ToInt32(FromPortNoTextBox.Text);
                        else
                            throw new Exception("لطفا شماره پورت را وارد نمایید !");

                        if (!string.IsNullOrEmpty(ToPortNoTextBox.Text))
                            toPort = Convert.ToInt32(ToPortNoTextBox.Text);

                        if (toPort == 0)
                        {
                            port.ID = 0;
                            port.PortNo = fromPort;

                            if (!Data.ADSLPAPPortDB.HasPAPPort(port.PAPInfoID, (long)port.PortNo, port.CenterID))
                            {
                                port.Detach();
                                Save(port);
                            }
                            else
                                throw new Exception("پورت تکراری می باشد.");
                        }
                        else
                        {
                            for (long i = fromPort; i <= toPort; i++)
                            {
                                port.ID = 0;
                                port.PortNo = i;

                                if (!Data.ADSLPAPPortDB.HasPAPPort(port.PAPInfoID, (long)port.PortNo, port.CenterID))
                                {
                                    port.Detach();
                                    Save(port);
                                }
                                else
                                    throw new Exception("پورت تکراری می باشد.");
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                            port.TelephoneNo = null;

                        port.Detach();
                        Save(port);
                    }
                }

                if (DB.City == "kermanshah" || DB.City == "gilan")
                {
                    long fromBucht = 0;
                    long toBucht = 0;

                    if (_ID == 0)
                    {
                        if (!string.IsNullOrEmpty(FromBuchtNoTextBox.Text))
                            fromBucht = Convert.ToInt32(FromBuchtNoTextBox.Text);
                        else
                            throw new Exception("لطفا شماره اتصالی را وارد نمایید !");

                        if (!string.IsNullOrEmpty(ToBuchtNoTextBox.Text))
                            toBucht = Convert.ToInt32(ToBuchtNoTextBox.Text);

                        if (toBucht == 0)
                        {
                            port.ID = 0;
                            port.BuchtNo = fromBucht;

                            if (!Data.ADSLPAPPortDB.HasPAPBucht(port.PAPInfoID, (int)port.RowNo, (int)port.ColumnNo, (long)port.BuchtNo, port.CenterID))
                            {
                                port.Detach();
                                Save(port);
                            }
                            else
                                throw new Exception("بوخت تکراری می باشد.");
                        }
                        else
                        {
                            for (long i = fromBucht; i <= toBucht; i++)
                            {
                                port.ID = 0;
                                port.BuchtNo = i;

                                if (!Data.ADSLPAPPortDB.HasPAPBucht(port.PAPInfoID, (int)port.RowNo, (int)port.ColumnNo, (long)port.BuchtNo, port.CenterID))
                                {
                                    port.Detach();
                                    Save(port);
                                }
                                else
                                    throw new Exception("بوخت تکراری می باشد.");
                            }
                        }
                    }
                    else
                    {
                        if (port.Status != (byte)DB.ADSLPAPPortStatus.Instal && port.TelephoneNo != null)
                            throw new Exception("پورت مورد نظر دایر می باشد، امکان تغییر وضعیت آن وجود ندارد.");

                        port.Detach();
                        Save(port);
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
