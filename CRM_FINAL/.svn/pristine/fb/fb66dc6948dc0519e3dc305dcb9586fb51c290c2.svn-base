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
    public partial class MDFFrameForm : Local.PopupWindow
    {
        private int _ID = 0;
        Data.MDF mDF;
        private int CityID = 0;

        public MDFFrameForm()
        {
            InitializeComponent();
            Initialize();
        }

        public MDFFrameForm(int id)
            : this()
        {
            _ID = id;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void LoadData()
        {
            MDFFrame item = new MDFFrame();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";


                if (CityID == 0)
                    CityComboBox.SelectedIndex = 0;

                else
                    CityComboBox.SelectedValue = CityID;
            }
            else
            {
                item = Data.MDFFrameDB.GetMDFFrameByID(_ID);
                CityID = Data.MDFFrameDB.GetCityIDByFrameID(item.ID);

                if (CityID == 0)
                    CityComboBox.SelectedIndex = 0;

                else
                    CityComboBox.SelectedValue = CityID;

                MDF mdf = Data.MDFDB.GetMDFByID(item.MDFID);

                CenterComboBox.SelectedValue = mdf.CenterID;

                MDFComboBox.SelectedValue = item.MDFID;
                MDFComboBox_SelectionChanged(null, null);

                SaveButton.Content = "بروزرسانی";

                FrameNoLable.Visibility = Visibility.Collapsed;
                FrameNoTextBox.Visibility = Visibility.Collapsed;
                DataFrameRecovery.Visibility = Visibility.Collapsed;
                CountFramesLabel.Visibility = Visibility.Collapsed;
                CountFramesTextBox.Visibility = Visibility.Collapsed;
            }

            this.DataContext = item;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {

                byte buchtType = (byte)Data.DB.BuchtType.CustomerSide;
                MDFFrame mDFFrame = this.DataContext as MDFFrame;

                if (_ID == 0)
                {
                    int countFrames = Convert.ToInt32(CountFramesTextBox.Text != string.Empty ? CountFramesTextBox.Text : "1");
                    int frameNo = Convert.ToInt32(FrameNoTextBox.Text != string.Empty ? FrameNoTextBox.Text : "0");
                    List<int> FrameNoList = new List<int>();

                    for (int frame = 0; frame < countFrames; frame++)
                    {
                        FrameNoList.Add(frameNo + frame);
                    }


                    //if (MDFFrameDB.CheckRepeatingFrames(FrameNoList, mDF))
                    //    throw new Exception("فریم ها موجود هستند");



                    MDFFrameDB.SaveMDFFrame(Convert.ToInt32(VerticalLastColumn.Text),
                             Convert.ToInt32(VerticalFirstColumn.Text),
                             countFrames,
                             frameNo,
                             Convert.ToInt32(VerticalRowsCount.Text),
                             Convert.ToInt32(VerticalRowCapacity.Text),
                             mDF,
                             buchtType,
                             mDFFrame);
                }
                else
                {
                    mDFFrame.Detach();
                    DB.Save(mDFFrame);

                }


                ShowSuccessMessage("ذخیره ام دی ام انجام شد");
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("ایتم وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره فریم", ex);
            }
             
        }

        private void BuchtTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MDFComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedValue != null)
            {
                mDF = Data.MDFDB.GetMDFByID((int)MDFComboBox.SelectedValue);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MDFComboBox.SelectedValue != null)
            {
                FrameNoTextBox.Text = (MDFDB.GetLastFram((int)MDFComboBox.SelectedValue) + 1 ?? 1).ToString();
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                if (this.DataContext != null && (CenterComboBox.Items[0] as CenterInfo) != null)
                    CenterComboBox.SelectedValue = (CenterComboBox.Items[0] as CenterInfo).ID;
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

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterID((int)CenterComboBox.SelectedValue);
            }
        }
    }
}
