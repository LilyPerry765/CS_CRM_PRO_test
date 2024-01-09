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
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class ADSLEquimentForm : Local.PopupWindow
    {
        #region Properties

        private int _ID;
        private ADSLEquipment aDSLEquipmet;

        private int FromPort = 0;
        private int ToPort = 0;

        private List<Bucht> NewInputBuchtList = new List<Bucht>();
        private List<Bucht> NewOutputBuchtList = new List<Bucht>();
        private List<Bucht> OldInputBuchtList = new List<Bucht>();
        private List<Bucht> OldOutputBuchtList = new List<Bucht>();
        private List<ADSLPort> OldADSLPortList = new List<ADSLPort>();

        private int _CityID = 0;
        private int _RockID = 0;

        #endregion

        #region Constructors

        public ADSLEquimentForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLEquimentForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            RockComboBox.ItemsSource = DB.GetAllEntity<Rock>();
            ADSLAAATypeCombobox.ItemsSource = Data.ADSLAAATypeDB.GetADSLAAATypeCheckable();
            ADSLPortTypeCombobox.ItemsSource = Data.ADSLPortTypeDB.GetADSLPortTypeCheckable();
            FromOutputConnection.BuchtType = FromInputConnection.BuchtType = (int)DB.BuchtType.ADSL; 
            TypeOfConnectedComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.TypeOfConnectedBuchtADSL));
            LocationInstallComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLEquimentLocationInstall));
            ProductComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLEquimentProduct));
            TypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLEquimentType));
        }

        private void LoadData()
        {
            aDSLEquipmet = new ADSLEquipment();



            if (_ID != 0)
            {
                aDSLEquipmet = Data.ADSLEquipmentDB.GetADSLEquipmentByID(_ID);

                _CityID = Data.ADSLEquipmentDB.GetCity(aDSLEquipmet.ID);
                if (_CityID == 0)
                    CityComboBox.SelectedIndex = 0;

                else
                    CityComboBox.SelectedValue = _CityID;

                FromInputConnection.CenterID = aDSLEquipmet.CenterID;

                OldADSLPortList = Data.ADSLPortDB.GetADSLPortsByEquipmentID(aDSLEquipmet.ID).OrderBy(t => t.ID).ToList();
               // OldInputBuchtList = Data.BuchtDB.GetBuchtByADSLPortID(OldADSLPortList.Select(p => p.ID) , (byte)DB.ADSLPortType.Input).ToList();
              //  OldOutputBuchtList = Data.BuchtDB.GetBuchtByADSLPortID(OldADSLPortList.Select(p => p.ID) , (byte)DB.ADSLPortType.OutPut).ToList();

                FromPortTextBox.Text = OldADSLPortList[0].PortNo;
                ToPortTextBox.Text = OldADSLPortList[OldADSLPortList.Count() - 1].PortNo;

                if (OldInputBuchtList.Count != 0)
                {
                    FromInputConnection.BuchtID = OldInputBuchtList[0].ID;
                    ToInputConnection.BuchtID = OldInputBuchtList[OldInputBuchtList.Count() - 1].ID;
                }

                if (OldOutputBuchtList.Count != 0)
                {
                    FromOutputConnection.BuchtID = OldOutputBuchtList[0].ID;
                    ToOutputConnection.BuchtID = OldOutputBuchtList[OldOutputBuchtList.Count() - 1].ID;
                }

                InputConnectionGroupBox.IsEnabled = false;
                OutputConnectionGroupBox.IsEnabled = false;
                CityComboBox.IsEnabled = false;
                CenterComboBox.IsEnabled = false;
                PortInfoGroupBox.IsEnabled = false;
                SaveButton.Content = "بروز رسانی";


                _RockID = Data.ADSLEquipmentDB.GetRockID(aDSLEquipmet.ID);

                this.DataContext = aDSLEquipmet;
            }
            else
            {
                this.DataContext = aDSLEquipmet;

                if (_CityID == 0)
                    CityComboBox.SelectedIndex = 0;

                else
                    CityComboBox.SelectedValue = _CityID;

               

            }

            



            if (_RockID == 0)
                RockComboBox.SelectedIndex = 0;
            else
                RockComboBox.SelectedValue = _RockID;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void DoConnectionChange()
        {
            try
            {
                if (_ID == 0)
                {
                    if (!int.TryParse(FromPortTextBox.Text, out FromPort))
                        throw new Exception("از پورت نمی تواند خالی یا مقدار غیرعددی باشد");

                    if (!int.TryParse(ToPortTextBox.Text, out ToPort))
                        throw new Exception("تا پورت نمی تواند خالی یا مقدار غیر عددی باشد");

                    NewInputBuchtList = Data.BuchtDB.GetBuchtByMDFID(FromInputConnection.MDFID ?? -1).Where(t => t.ID >= (long)FromInputConnection.ConnectionBuchtComboBox.SelectedValue).ToList();

                    //اگر تعداد پورت های وارد شده فرد باشد تعداد ورودی و خروجی برابر نمی باشد
                    if ((ToPort - FromPort + 1) % 2 != 0) throw new Exception("تعداد پورتهای وارد شده صحیح نیست");

                    if (FromPort < ToPort)
                    {
                        // دو برابر تعداد پورت ها از بوختها ،  برای انتساب به بوخت ها انتخاب میکند هر پورت یک بوخت ورودی داری و یک بوخت خروجی
                        NewInputBuchtList = NewInputBuchtList.Take((ToPort - FromPort + 1) * 2).ToList();

                        if ((ToPort - FromPort + 1) > NewInputBuchtList.Count) throw new Exception("تعداد بوخت ها کمتر از تعداد پورت ها میباشد");
                        //تعیین پوخت های خروجی
                        NewOutputBuchtList = NewInputBuchtList.Skip((ToPort - FromPort + 1)).ToList();
                        // تعیین بوخت های ورودی
                        NewInputBuchtList = NewInputBuchtList.Take((ToPort - FromPort + 1)).ToList();


                        ToInputConnection.BuchtID = NewInputBuchtList[NewInputBuchtList.Count() - 1].ID;
                        ToInputConnection.LoadData(null, null);


                        //FromOutputConnection.MDFID = (int)FromInputConnection.MDFComboBox.SelectedValue;
                        //FromOutputConnection.BuchtID = NewOutputBuchtList[0].ID;
                        //FromOutputConnection.LoadData(null, null);

                        //ToOutputConnection.BuchtID = NewOutputBuchtList[NewOutputBuchtList.Count() - 1].ID;
                        //ToOutputConnection.LoadData(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در اطلاعات وارد شده", ex);
            }
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                aDSLEquipmet = this.DataContext as ADSLEquipment;
                if (_ID == 0)
                {
                    // بررسی وجود بوخت تکراری
                    if (NewInputBuchtList.Any(t => NewOutputBuchtList.Select(bo => bo.ID).Contains(t.ID)))
                        throw new Exception("بوخت تکراری نمی توان وارد کرد");

                    using (TransactionScope ts = new TransactionScope())
                    {
                        aDSLEquipmet.Detach();
                        DB.Save(aDSLEquipmet);

                        if ((byte)TypeOfConnectedComboBox.SelectedValue == (byte)DB.TypeOfConnectedBuchtADSL.regular)
                        {
                            // تولید پورت ها
                            int CountOfPort = ToPort - FromPort + 1;
                            List<ADSLPort> ADSLPortList = new List<ADSLPort>();
                            for (int i = FromPort; i <= ToPort; i++)
                            {
                                ADSLPort aDSLPort = new ADSLPort();
                                aDSLPort.ADSLEquipmentID = aDSLEquipmet.ID;
                                aDSLPort.PortNo = i.ToString();
                                aDSLPort.Status = (byte)DB.ADSLPortStatus.Free;
                                ADSLPortList.Add(aDSLPort);
                            }

                            DB.SaveAll(ADSLPortList);

                            // پورت ها بوخت های ورودی و خروجی را مشخص میکند
                            for (int j = 0; j <= ADSLPortList.Count() - 1; j++)
                            {
                                // ورودی ها
                               // NewInputBuchtList[j].ADSLPortID = ADSLPortList[j].ID;
                                NewInputBuchtList[j].Status = (byte)DB.BuchtStatus.ConnectedToSpliter;
                              //  NewInputBuchtList[j].ADSLType = (byte)DB.ADSLPortType.Input;
                                NewInputBuchtList[j].Detach();
                                ADSLPortList[j].InputBucht = NewInputBuchtList[j].ID;

                                // خروجی ها
                              //  NewOutputBuchtList[j].ADSLPortID = ADSLPortList[j].ID;
                                NewOutputBuchtList[j].Status = (byte)DB.BuchtStatus.ConnectedToSpliter;
                             //   NewOutputBuchtList[j].ADSLType = (byte)DB.ADSLPortType.OutPut;
                                NewOutputBuchtList[j].Detach();
                                ADSLPortList[j].OutBucht = NewOutputBuchtList[j].ID;
                            }

                            foreach (ADSLPort item in ADSLPortList)
                            {
                                item.Detach();
                            }

                            DB.UpdateAll(ADSLPortList);
                            DB.UpdateAll(NewInputBuchtList);
                            DB.UpdateAll(NewOutputBuchtList);
                            ts.Complete();
                        }

                        else if ((byte)TypeOfConnectedComboBox.SelectedValue == (byte)DB.TypeOfConnectedBuchtADSL.Irregular)
                        {
                            if (!int.TryParse(FromPortTextBox.Text, out FromPort))
                                throw new Exception("از پورت نمی تواند خالی یا مقدار غیرعددی باشد");

                            if (!int.TryParse(ToPortTextBox.Text, out ToPort))
                                throw new Exception("تا پورت نمی تواند خالی یا مقدار غیر عددی باشد");


                            // تولید پورت ها
                            int CountOfPort = ToPort - FromPort + 1;
                            List<ADSLPort> ADSLPortList = new List<ADSLPort>();

                            for (int i = FromPort; i <= ToPort; i++)
                            {
                                ADSLPort aDSLPort = new ADSLPort();
                                aDSLPort.ADSLEquipmentID = aDSLEquipmet.ID;
                                aDSLPort.PortNo = i.ToString();
                                aDSLPort.Status = (byte)DB.ADSLPortStatus.Free;
                                ADSLPortList.Add(aDSLPort);
                            }

                            DB.SaveAll(ADSLPortList);
                            ts.Complete();
                        }

                        else if ((byte)TypeOfConnectedComboBox.SelectedValue == (byte)DB.TypeOfConnectedBuchtADSL.Intermittent)
                            MessageBox.Show("لطفا نحوه اتصال دیگری انتخاب کنید");
                        else
                            MessageBox.Show("لطفا نحوه اتصال را انتخاب کنید");

                        ShowSuccessMessage("ذخیره ADSL انجام شد"); this.DialogResult = true;
                    }
                }
                else
                {
                    aDSLEquipmet.Detach();
                    Save(aDSLEquipmet);
                    ShowSuccessMessage("ذخیره ADSL انجام شد"); this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ورود اطلاعات", ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
               (this.DataContext as ADSLEquipment).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
               CenterComboBox.SelectedValue = (CenterComboBox.Items[0] as CenterInfo).ID;
               CenterComboBox_SelectionChanged(null, null);
            }
            else
            {
                if (CityComboBox.SelectedValue == null)
                {
                    City city = Data.CityDB.GetCityById(_CityID);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
                else
                {
                    City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
            }
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                FromOutputConnection.CenterID = FromInputConnection.CenterID = (int)CenterComboBox.SelectedValue;
            }
        }
        
        private void RockComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_RockID == 0)
                ShelfComboBox.ItemsSource = DB.GetAllEntity<Shelf>().Where(t => t.RockID == (int)RockComboBox.SelectedValue).ToList();
            else
            {
                if (RockComboBox.SelectedValue == null)
                    ShelfComboBox.ItemsSource = DB.GetAllEntity<Shelf>().Where(t => t.RockID == _RockID).ToList();
                else
                    ShelfComboBox.ItemsSource = DB.GetAllEntity<Shelf>().Where(t => t.RockID == (int)RockComboBox.SelectedValue).ToList();
            }
        }

        #endregion

        private void DoOutPutConnectionChange()
        {
            if (_ID == 0)
            {
                NewOutputBuchtList = Data.BuchtDB.GetBuchtByMDFID(FromOutputConnection.MDFID ?? -1).Where(t => t.ID >= (long)FromOutputConnection.ConnectionBuchtComboBox.SelectedValue).ToList();

                NewOutputBuchtList = NewOutputBuchtList.Take(ToPort - FromPort + 1).ToList();

                ToOutputConnection.BuchtID = NewOutputBuchtList[NewOutputBuchtList.Count() - 1].ID;
                ToOutputConnection.LoadData(null, null);
            }
        }
    }
}
