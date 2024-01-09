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
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class VerticalMDFRowForm : Local.PopupWindow
    {
        private int _ID = 0;
        private bool _MultiInsert = false;
        MDF mDF = new MDF();

        VerticalMDFRow _verticalMDFRow = new VerticalMDFRow();
        public VerticalMDFRowForm()
        {
            InitializeComponent();

        }

        public VerticalMDFRowForm(int id, bool multiInsert)
            : this()
        {
            _MultiInsert = multiInsert;
            _ID = id;
            Initialize();
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {

        }

        private void LoadData()
        {
            _verticalMDFRow = Data.VerticalMDFRowDB.GetVerticalMDFRowByID(_ID);

            if (_MultiInsert == false)
            {
                FromBuchtLabel.Visibility = Visibility.Collapsed;
                FromBuchtTextBox.Visibility = Visibility.Collapsed;

                ToBuchtLabel.Visibility = Visibility.Collapsed;
                ToBuchtTextBox.Visibility = Visibility.Collapsed;

                BuchtTypeComboBoxLabel.Visibility = Visibility.Collapsed;
                BuchtTypeComboBox.Visibility = Visibility.Collapsed;
                SaveButton.Content = "بروزرسانی";
            }
            else if (_MultiInsert == true)
            {
                FromVerticalRowNoTextBox.IsEnabled = false;
                BuchtTypeComboBox.ItemsSource = Data.BuchtTypeDB.GetBuchtTypeCheckable();
                SaveButton.Content = "ذخیره";
            }
            this.DataContext = _verticalMDFRow;

        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            //milad doran
            //if (_MultiInsert == false)
            //{
            //    try
            //    {
            //        VerticalMDFRow item = this.DataContext as VerticalMDFRow;

            //        item.Detach();
            //        Save(item);

            //        ShowSuccessMessage("طبقه ام دی ام ذخیره شد");
            //        this.DialogResult = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        if (ex.Message.Contains("Cannot insert duplicate key in object"))
            //        {
            //            ShowErrorMessage("نمی توان دو طبقه هم شماره وارد کرد .خطا در ذخیره ", ex);
            //        }
            //        else
            //        {
            //            ShowErrorMessage("خطا در ذخیره طبقه ام دی ام", ex);
            //        }
            //    }

            //}
            //TODO:rad 13950331 
            if (_MultiInsert == false)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        VerticalMDFRow item = this.DataContext as VerticalMDFRow;

                        //چنانچه طبقه انتخاب شده دارای بوخت باشد باید ستون شماره طبقه در جدول بوخت که برای افزایش سرعت کوئری اضافه شده است ، بروزرسانی شود
                        //لذا کلیه ی بوخت های طبقه انتخاب شده را در صورت وجود باید فراخوانی کنیم
                        List<Bucht> buchtsOfSelectedVerticalMdfRow = VerticalMDFRowDB.GetAllBuchtByRowID(item.ID);

                        if (buchtsOfSelectedVerticalMdfRow.Count > 0)
                        {
                            buchtsOfSelectedVerticalMdfRow.ForEach((bu) =>
                            {
                                bu.RowNo = item.VerticalRowNo;
                                bu.Detach();
                            }
                                                                   );
                            DB.UpdateAll<Bucht>(buchtsOfSelectedVerticalMdfRow);
                        }
                        item.Detach();
                        Save(item);

                        scope.Complete();
                    }

                    ShowSuccessMessage("طبقه ام دی ام ذخیره شد");
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Cannot insert duplicate key in object"))
                    {
                        ShowErrorMessage("نمی توان دو طبقه هم شماره وارد کرد .خطا در ذخیره ", ex);
                    }
                    else
                    {
                        ShowErrorMessage("خطا در ذخیره طبقه ام دی ام", ex);
                    }
                }

            }
            else if (_MultiInsert == true)
            {

                long startBucht = Convert.ToInt32(FromBuchtTextBox.Text.Trim());
                long NumberBucht = Convert.ToInt32(ToBuchtTextBox.Text.Trim());

                VerticalMDFColumn verticalMDFColumn = VerticalMDFColumnDB.GetVerticalMDFColumnByID(_verticalMDFRow.VerticalMDFColumnID);

                MDFFrame frame = Data.MDFFrameDB.GetMDFFrameByID(verticalMDFColumn.MDFFrameID);
                MDF mDF = Data.MDFDB.GetMDFByID(frame.MDFID);

                Center center = CenterDB.GetCenterByCenterID(mDF.CenterID);
                City city = CityDB.GetCityByCenterID(center.ID);

                try
                {
                    List<Bucht> buchts = new List<Bucht>();
                    buchts.Clear();

                    for (long Capacity = startBucht; Capacity <= NumberBucht; Capacity++)
                    {
                        Bucht bucht = new Bucht();
                        bucht.BuchtNo = Capacity;

                        //ستون های زیر به منظور افزایش سرعت کوئری ها اضافه شده است
                        bucht.CityID = city.ID;
                        bucht.CenterID = center.ID;
                        bucht.Center = center.CenterName;
                        bucht.MDFID = mDF.ID;
                        bucht.MDF = mDF.Number.ToString() + '(' + mDF.Description + ')';
                        bucht.FrameID = frame.ID;
                        bucht.Frame = frame.FrameNo;
                        bucht.ColumnNo = verticalMDFColumn.VerticalCloumnNo;
                        bucht.RowNo = _verticalMDFRow.VerticalRowNo;
                        bucht.BuchtNo = Capacity;
                        //end

                        bucht.MDFRowID = _verticalMDFRow.ID;
                        bucht.BuchtTypeID = (int)BuchtTypeComboBox.SelectedValue;
                        bucht.Status = (int)DB.BuchtStatus.Free;
                        buchts.Add(bucht);

                    }
                    DB.SaveAll(buchts);

                    if ((int)BuchtTypeComboBox.SelectedValue == (byte)DB.BuchtType.ADSL)
                    {
                        List<ADSLPort> ports = new List<ADSLPort>();

                        foreach (Bucht bucht in buchts)
                        {
                            ADSLPort port = new ADSLPort();

                            port.InputBucht = bucht.ID;
                            port.Status = (byte)DB.ADSLPortStatus.Free;

                            ports.Add(port);
                        }

                        DB.SaveAll(ports);
                    }

                    VerticalMDFRow item = this.DataContext as VerticalMDFRow;
                    item.RowCapacity = Convert.ToInt32(Data.BuchtDB.GetLastBuchtNoByVerticalRowID(_verticalMDFRow.ID) ?? 0);
                    item.Detach();
                    Save(item);
                    ShowSuccessMessage("طبقه ام دی اف ذخیره شد");
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Cannot insert duplicate key in object"))
                    {
                        ShowErrorMessage("نمی توان دو طبقه هم شماره وارد کرد .خطا در ذخیره ", ex);
                    }
                    else
                    {
                        ShowErrorMessage("خطا در ذخیره ستون ام دی اف", ex);
                    }
                }
            }
        }
    }
}
