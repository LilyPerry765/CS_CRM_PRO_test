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
using Enterprise;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace CRM.Application.Views
{
    public partial class PCMForm : Local.PopupWindow
    {
        #region Properties

        private List<Bucht> NewInputBuchtList = new List<Bucht>();
        private List<Bucht> OldInputBuchtList = new List<Bucht>();
        private List<PCMPort> OldPCMPortList = new List<PCMPort>();
        private PCMCardInfo _PCMCardInfo;
        private PCM _PCM;
        private Bucht NewOutBucht = new Bucht();
        private PCMType _PCMType = new PCMType();
        private int _ID = 0;
        private Center center = new Center();
        private Region region = new Region();
        private City city = new City();
        private Province province = new Province();
        private int CityID = 0;

        public ObservableCollection<CheckableItem> _MDFLists { get; set; }
        public ObservableCollection<CheckableItem> _columns { get; set; }
        public ObservableCollection<CheckableItem> _rows { get; set; }
        public ObservableCollection<CheckableItem> _buchtNos { get; set; }

        public ObservableCollection<ConnectionForPCM> _connectionForPCMs { get; set; }
        public List<ConnectionForPCM> _cloneConnectionForPCMs { get; set; }

        private Data.Schema.PCMCreate actionLogPCMCreate = new Data.Schema.PCMCreate();

        #endregion

        #region Constructors

        public PCMForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PCMForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PCMBrandComboBox.ItemsSource = Data.PCMBrandDB.GetPCMBrandCheckable();
            PCMTypeComboBox.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();
        }

        public void LoadData()
        {
            _PCMCardInfo = new PCMCardInfo();
            _connectionForPCMs = new ObservableCollection<ConnectionForPCM>();

            try
            {
                if (_ID != 0)
                {
                    outBuchtEditButton.Visibility = Visibility.Visible;

                    _PCMCardInfo = Data.PCMDB.GetPCMCardInfoByPCMID(_ID);
                    //

                    CenterComboBox.SelectedValue = _PCMCardInfo.CenterID;
                    CenterComboBox_SelectionChanged(null, null);

                    RockComboBox.SelectedValue = _PCMCardInfo.PCMRockID;
                    RockComboBox_SelectionChanged_1(null, null);

                    ShelfComboBox.SelectedValue = _PCMCardInfo.PCMShelfID;
                    CityID = Data.PCMDB.GetCity(_PCMCardInfo.ID);

                    if (CityID == 0)
                        CityComboBox.SelectedIndex = 0;
                    else
                        CityComboBox.SelectedValue = CityID;
                    //

                    OldPCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(_PCMCardInfo.ID).ToList();

                    OldInputBuchtList = Data.BuchtDB.getBuchtByPCMPortID(OldPCMPortList.Select(t => t.ID).ToList()).OrderBy(t => t.BuchtNo).ToList();
                    //
                    NumberPortOut.Text = OldPCMPortList.Where(t => t.PortType == (byte)DB.BuchtType.OutLine).SingleOrDefault().PortNumber.ToString();
                    NumberFirstPortInput.Text = OldPCMPortList.Where(t => t.PortType == (byte)DB.BuchtType.InLine).Min(t => t.PortNumber).ToString();
                    //

                    //
                    _connectionForPCMs = new ObservableCollection<ConnectionForPCM>(Data.BuchtDB.GetConnectionForPCM(OldInputBuchtList.Where(t => t.BuchtTypeID == (int)DB.BuchtType.InLine).Select(t => (long)t.ID).ToList()));
                    _cloneConnectionForPCMs = _connectionForPCMs.Select(t => (ConnectionForPCM)t.Clone()).ToList();
                    InputConnectionDataGrid.ItemsSource = _connectionForPCMs;
                    //

                    //
                    OutputConnectionUserControl.CenterID = _PCMCardInfo.CenterID;
                    OutputConnectionUserControl.BuchtID = OldInputBuchtList.Where(t => t.BuchtTypeID == (int)DB.BuchtType.OutLine).SingleOrDefault().ID;
                    //


                    PCMInfoGrid.DataContext = new PCMBindProperty { PCM = _PCMCardInfo };
                }
                else
                {
                    PCMInfoGrid.DataContext = new PCMBindProperty { PCM = _PCMCardInfo };

                    List<ConnectionForPCM> firstItem = new List<ConnectionForPCM>();
                    firstItem.Add(new ConnectionForPCM());
                    _connectionForPCMs = new ObservableCollection<ConnectionForPCM>(firstItem);
                    InputConnectionDataGrid.ItemsSource = _connectionForPCMs;
                    NumberFirstPortInput.Text = "1";
                    NumberPortOut.Text = "1";
                    if (CityID == 0)
                        CityComboBox.SelectedIndex = 0;
                    else
                        CityComboBox.SelectedValue = CityID;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                Folder.MessageBox.ShowError("خطا در بارگذاری اطلاعات پی سی ام لطفا اطلاعات صحیح پی سی ام را به مدیر سیستم گزارش دهید");
            }

        }

        //public void DoConnectionChange()
        //{
        //    if (_ID == 0)
        //    {
        //      //  NewInputBuchtList = Data.BuchtDB.GetBuchtByMDFID(InputConnectionUserControl.MDFID ?? -1).Where(t => t.ID > (long)InputConnectionUserControl.ConnectionBuchtComboBox.SelectedValue && t.Status == (byte)DB.BuchtStatus.Free && t.CabinetInputID == null && t.PCMPortID == null).OrderBy(t => t.ID).ToList();
        //        if (_PCMType != null)
        //            NewInputBuchtList = NewInputBuchtList.Take(_PCMType.OutLine - 1).ToList();
        //        InputConnectionDataGrid.ItemsSource = NewInputBuchtList;
        //    }
        //}

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void PCMTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PCMTypeComboBox.SelectedValue != null)
            {
                _PCMType = Data.PCMTypeDB.GetPCMTypeByID((int)PCMTypeComboBox.SelectedValue);
                //if (NewInputBuchtList != null)
                //    NewInputBuchtList = NewInputBuchtList.Take(_PCMType.OutLine - 1).ToList();
            }
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    if (_PCMCardInfo != null && _PCMCardInfo.Status == (byte)DB.PCMStatus.Connection)
                    {
                        MessageBox.Show("این پی سی ام دایر شده است امکان ویرایش ندارد", "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    };

                    _PCMCardInfo = (PCMInfoGrid.DataContext as PCMBindProperty).PCM;

                    if (_ID == 0)
                    {
                        if (Data.PCMTypeDB.CheckBeRepeatedRockShelfCard(_PCMCardInfo))
                        {
                            MessageBox.Show("این رک شلف کارت قبلا استفاده شده است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        };
                    }

                    int PortNumber;
                    if (!Int32.TryParse(NumberFirstPortInput.Text.Trim(), out PortNumber))
                        throw new Exception("لطفا در پورت ورودی مقدار صحیح وارد کنید");

                    int OutPortNumber;
                    if (!Int32.TryParse(NumberPortOut.Text, out OutPortNumber))
                        throw new Exception("لطفا در پورت خروجی مقدار صحیح وارد کنید");


                    //  List<long> NewInBuchtListID = (InputConnectionDataGrid.ItemsSource as List<Bucht>).Select(t => t.ID).ToList();
                    NewInputBuchtList = Data.BuchtDB.GetBuchetByListBuchtIDs(_connectionForPCMs.Select(t => (long)t.BuchtID).ToList());

                    // بررسی تکراری بودن
                    if (NewInputBuchtList.GroupBy(g => g.ID).SelectMany(t => t.Skip(1)).Count() > 0)
                        throw new Exception("نمی توان اتصالی تکراری انتخاب کرد");


                    NewOutBucht = Data.BuchtDB.GetBuchtByID((long)OutputConnectionUserControl.ConnectionBuchtComboBox.SelectedValue);
                    // بررسی موجود نبودن خروجی در ورودی ها
                    if (NewInputBuchtList.Any(t => t.ID == NewOutBucht.ID) == true)
                        throw new Exception("پورت خروجی انتخاب شده در پورت های ورودی موجود است");

                    // هنگام بروز رسانی اطلاعات قبلی آزاد و اطلاعات جدید ثبت میشود
                    List<Bucht> oldbucht = new List<Bucht>();
                    foreach (Bucht obj in Data.BuchtDB.getBuchtByPCMPortID(OldPCMPortList.Select(t => t.ID).ToList()))
                    {
                        //TODO:
                        obj.BuchtTypeID = (byte)DB.BuchtType.CustomerSide;
                        obj.PCMPortID = null;
                        obj.PortNo = null;
                        obj.Status = (byte)DB.BuchtStatus.Free;
                        obj.Detach();
                        oldbucht.Add(obj);
                    }

                    DB.UpdateAll(oldbucht);

                    foreach (PCMPort obj in OldPCMPortList)
                    {
                        DB.Delete<PCMPort>(obj.ID);
                    }

                    NewOutBucht.BuchtTypeID = (byte)DB.BuchtType.OutLine;



                    _PCM = new PCM();
                    _PCM.ID = _PCMCardInfo.ID;
                    _PCM.ShelfID = _PCMCardInfo.PCMShelfID;
                    _PCM.Card = _PCMCardInfo.Card;
                    _PCM.PCMBrandID = _PCMCardInfo.PCMBrandID;
                    _PCM.PCMTypeID = _PCMCardInfo.PCMTypeID;
                    _PCM.Status = (byte)DB.PCMStatus.Install;
                    _PCM.InsertDate = DB.GetServerDate();
                    _PCM.Detach();
                    Save(_PCM);

                    List<PCMPort> PCMPortList = new List<PCMPort>();
                    // ایجاد پورت خروجی

                    PCMPort pCMPort = new PCMPort();
                    pCMPort.PCMID = _PCM.ID;
                    pCMPort.PortNumber = OutPortNumber;
                    pCMPort.Status = (byte)DB.PCMPortStatus.Empty;
                    pCMPort.PortType = (byte)DB.BuchtType.OutLine;
                    pCMPort.Detach();
                    Save(pCMPort);

                    NewOutBucht.PCMPortID = pCMPort.ID;
                    NewOutBucht.PortNo = (byte)pCMPort.PortNumber;

                    for (int i = 1; i <= NewInputBuchtList.Count(); i++)
                    {
                        pCMPort = new PCMPort();
                        pCMPort.PCMID = _PCM.ID;
                        pCMPort.PortNumber = PortNumber++;
                        pCMPort.Status = (byte)DB.PCMPortStatus.Empty;
                        pCMPort.PortType = (byte)DB.BuchtType.InLine;
                        pCMPort.Detach();
                        PCMPortList.Add(pCMPort);

                    }
                    DB.SaveAll(PCMPortList);

                    for (int i = 0; i < NewInputBuchtList.Count(); i++)
                    {
                        NewInputBuchtList[i].PCMPortID = PCMPortList[i].ID;
                        NewInputBuchtList[i].BuchtTypeID = (byte)DB.BuchtType.InLine;
                        NewInputBuchtList[i].Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                        NewInputBuchtList[i].PortNo = (byte)PCMPortList[i].PortNumber;
                        NewInputBuchtList[i].Detach();
                    }
                    DB.UpdateAll(NewInputBuchtList);
                    NewOutBucht.Status = (byte)DB.BuchtStatus.ConnectedToPCM;

                    NewOutBucht.Detach();
                    Save(NewOutBucht);

                    actionLogPCMCreate.CenterID = (CenterComboBox.SelectedItem as CenterInfo).ID;
                    actionLogPCMCreate.Center = (CenterComboBox.SelectedItem as CenterInfo).CenterName;
                    actionLogPCMCreate.Rock = (RockComboBox.SelectedItem as CheckableItem).Name;
                    actionLogPCMCreate.Shelf = (ShelfComboBox.SelectedItem as CheckableItem).Name;
                    actionLogPCMCreate.Card = _PCMCardInfo.Card.ToString();
                    actionLogPCMCreate.Type = (PCMBrandComboBox.SelectedItem as CheckableItem).Name + " " + (PCMTypeComboBox.SelectedItem as CheckableItem).Name;
                    ActionLog actionLog = new ActionLog();
                    actionLog.ActionID = (byte)DB.ActionLog.PCMCreate;
                    actionLog.UserName = Folder.User.Current.Username;
                    actionLogPCMCreate.Date = actionLog.Date = DB.GetServerDate();
                    actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.PCMCreate>(actionLogPCMCreate, true));
                    actionLog.Detach();
                    DB.Save(actionLog);

                    ts.Complete();
                }
                _ID = _PCM.ID;
                LoadData();
                ShowSuccessMessage("نصب پی سی ام انجام شد");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در نصب پی سی ام", ex);
            }
        }

        UserControls.ColumnRowConnection ColumnRowConnectionColumn = new UserControls.ColumnRowConnection();

        private void ColumnRowConnectionColumn_Loaded(object sender, RoutedEventArgs e)
        {
            ColumnRowConnectionColumn = sender as UserControls.ColumnRowConnection;

        }

        private void RockComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (RockComboBox.SelectedValue != null)
            {
                //milad doran
                //ShelfComboBox.ItemsSource = Data.PCMShelfDB.GetPCMShelfByRockID((int)RockComboBox.SelectedValue)
                //                                           .OrderBy(t => t.Number)
                //                                           .Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false })
                //                                           .ToList();
                //TODO:rad 13941215
                List<CheckableItem> shelfCheckableItems = new List<CheckableItem>();

                int pcmRockId = Convert.ToInt32(RockComboBox.SelectedValue);
                shelfCheckableItems = PCMShelfDB.GetPcmShelfCheckableItemsByPcmRockId(pcmRockId);

                ShelfComboBox.ItemsSource = shelfCheckableItems;
            }

        }
        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                List<int> centerID = new List<int> { (int)CenterComboBox.SelectedValue };
                RockComboBox.ItemsSource = Data.PCMRockDB.GetPCMRockCheckableByCenterIDs(centerID);

                // InputConnectionUserControl.CenterID = (int)CenterComboBox.SelectedValue;
                // ColumnRowConnectionColumn.CenterID = (int)CenterComboBox.SelectedValue;
                OutputConnectionUserControl.CenterID = (int)CenterComboBox.SelectedValue;

                _MDFLists = new ObservableCollection<CheckableItem>(Data.MDFDB.GetMDFCheckableByCenterID((int)CenterComboBox.SelectedValue));

            }

        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                if ((PCMInfoGrid.DataContext as PCMBindProperty) != null)
                {
                    (PCMInfoGrid.DataContext as PCMBindProperty).PCM.CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
                    CenterComboBox.SelectedValue = (CenterComboBox.Items[0] as CenterInfo).ID;
                    CenterComboBox_SelectionChanged(null, null);
                }
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

        #region SelectionChangedComboBox

        private void MDFComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedItem != null)
            {
                if (InputConnectionDataGrid.SelectedItem != null)
                {
                    (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).MDF = (MDFComboBox.SelectedItem as CheckableItem).Name;
                }

                //milad doran
                //_columns = new ObservableCollection<CheckableItem>(DB.GetConnectionColumnInfo((int)(MDFComboBox.SelectedValue ?? 0)));

                //prepare mdfId from MDFComboBox
                //TODO:rad 13941215
                int mdfId = (MDFComboBox.SelectedValue != null) ? Convert.ToInt32(MDFComboBox.SelectedValue) : 0;

                List<CheckableItem> result = new List<CheckableItem>();
                result = DB.GetConnectionColumnInfoByMdfId(mdfId);

                ObservableCollection<CheckableItem> columns = new ObservableCollection<CheckableItem>(result);

                this._columns = columns;
                //TODO:rad editing end
            }
        }

        private void ColumnComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ColumnComboBox.SelectedValue != null)
            {
                if (InputConnectionDataGrid.SelectedItem != null)
                {
                    (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).VerticalCloumnNo = _columns.Where(t => t.ID == (int)ColumnComboBox.SelectedValue).SingleOrDefault().Name;
                }

                //milad doran
                //_rows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo((int)ColumnComboBox.SelectedValue));

                //prepare verticalMdfColumnId from ColumnComboBox
                //TODO:rad 13941215
                int verticalMdfColumnId = Convert.ToInt32(ColumnComboBox.SelectedValue);

                List<CheckableItem> result = new List<CheckableItem>();
                result = DB.GetConnectionRowInfoByVerticalMdfColunmnId(verticalMdfColumnId);

                ObservableCollection<CheckableItem> rows = new ObservableCollection<CheckableItem>(result);

                this._rows = rows;
                //TODO:rad editing end
            }
        }

        private void RowComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (RowComboBox.SelectedItem != null)
            {
                if (InputConnectionDataGrid.SelectedItem != null)
                {
                    (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).VerticalRowNo = (RowComboBox.SelectedItem as CheckableItem).Name;
                }

                //milad doran
                //_buchtNos = new ObservableCollection<CheckableItem>(DB.GetConnectionBuchtInfo((int)RowComboBox.SelectedValue, true));

                //prepare verticalMdfRowId from RowComboBox
                //TODO:rad 13941215
                int verticalMdfRowId = Convert.ToInt32(RowComboBox.SelectedValue);

                List<CheckableItem> result = new List<CheckableItem>();
                result = DB.GetConnectionBuchtInfoByVerticalMdfRowId(verticalMdfRowId, false);

                ObservableCollection<CheckableItem> buchtNos = new ObservableCollection<CheckableItem>(result);
                this._buchtNos = buchtNos;
                //TODO:rad editing end
            }
        }

        private void ConnectionComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionComboBox.SelectedItem != null)
            {
                if (InputConnectionDataGrid.SelectedItem != null)
                {
                    (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).BuchtNo = (ConnectionComboBox.SelectedItem as CheckableItem).Name;
                }
            }

            if (ConnectionComboBox.SelectedValue != null)
            {
                if (_ID == 0)
                {
                    if (_PCMType != null && _connectionForPCMs.Count() == 1)
                    {
                        _connectionForPCMs = new ObservableCollection<ConnectionForPCM>(Data.BuchtDB.GetBuchtTheNumber((long)ConnectionComboBox.SelectedValue, _PCMType.OutLine));
                        InputConnectionDataGrid.ItemsSource = _connectionForPCMs;
                    }
                }
            }
        }

        #endregion SelectionChangedComboBox

        #region LoadComboBox
        ComboBox MDFComboBox = new ComboBox();
        private void MDFComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            MDFComboBox = sender as ComboBox;
        }
        ComboBox RowComboBox = new ComboBox();
        private void RowComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            RowComboBox = sender as ComboBox;
        }
        ComboBox ColumnComboBox = new ComboBox();
        private void ColumnComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            ColumnComboBox = sender as ComboBox;
        }
        ComboBox ConnectionComboBox = new ComboBox();
        private void ConnectionComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            ConnectionComboBox = sender as ComboBox;
        }

        #endregion

        private void InputConnectionDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                if (InputConnectionDataGrid.SelectedValue != null)
                {

                    int rowIndex = (int)(InputConnectionDataGrid.SelectedItem as ConnectionForPCM).rowIndex;
                    if (rowIndex != 0)
                    {

                        long? oldBuchtID = _cloneConnectionForPCMs.Where(t => t.rowIndex == rowIndex).SingleOrDefault().BuchtID;

                        Bucht buchtEditing = OldInputBuchtList.Where(t => t.ID == oldBuchtID).SingleOrDefault();

                        long? newBuchtID = (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).BuchtID;

                        if (newBuchtID == null)
                            throw new Exception("لطفا بوخت جدید را انتخاب کنید");

                        if (buchtEditing.ID != newBuchtID)
                        {
                            Bucht newBucht = Data.BuchtDB.GetBuchetByID(newBuchtID ?? 0);


                            if (newBucht.SwitchPortID != null)
                                throw new Exception("بوخت جدید انتخاب شده به مشترک متصل می باشد");

                            if (newBucht.PCMPortID != null)
                                throw new Exception("بوخت جدید انتخاب شده به پی سی ام متصل می باشد");

                            if (newBucht.ConnectionID != null)
                                throw new Exception("بوخت جدید انتخاب شده به اتصالی پست متصل می باشد");

                            if (newBucht.CabinetInputID != null)
                                throw new Exception("بوخت جدید انتخاب شده به مرکزی کافو متصل می باشد");


                            using (TransactionScope ts = new TransactionScope())
                            {
                                newBucht.SwitchPortID = buchtEditing.SwitchPortID;
                                newBucht.CabinetInputID = buchtEditing.CabinetInputID;
                                newBucht.BuchtTypeID = buchtEditing.BuchtTypeID;
                                newBucht.PCMPortID = buchtEditing.PCMPortID;
                                newBucht.PortNo = buchtEditing.PortNo;
                                newBucht.ConnectionID = buchtEditing.ConnectionID;
                                newBucht.Status = buchtEditing.Status;
                                newBucht.Detach();
                                DB.Save(newBucht);


                                buchtEditing.SwitchPortID = null;
                                buchtEditing.CabinetInputID = null;
                                buchtEditing.BuchtTypeID = (int)DB.BuchtType.CustomerSide;
                                buchtEditing.PCMPortID = null;
                                buchtEditing.PortNo = null;
                                buchtEditing.ConnectionID = null;
                                buchtEditing.Status = (int)DB.BuchtStatus.Free;
                                buchtEditing.Detach();
                                DB.Save(buchtEditing);


                                ts.Complete();
                            }

                            LoadData();



                        }
                    }


                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در دخیره اطلاعات", ex);
            }

        }

        private void outBuchtEditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OutputConnectionUserControl.BuchtID != null && OutputConnectionUserControl.BuchtID != 0)
                {
                    if (OutputConnectionUserControl.BuchtID != OldInputBuchtList.Where(t => t.BuchtTypeID == (int)DB.BuchtType.OutLine).SingleOrDefault().ID)
                    {
                        Bucht buchtEditing = OldInputBuchtList.Where(t => t.BuchtTypeID == (int)DB.BuchtType.OutLine).SingleOrDefault();

                        Bucht newBucht = Data.BuchtDB.GetBuchetByID(OutputConnectionUserControl.BuchtID);

                        if (newBucht.SwitchPortID != null)
                            throw new Exception("بوخت جدید انتخاب شده به مشترک متصل می باشد");

                        if (newBucht.PCMPortID != null)
                            throw new Exception("بوخت جدید انتخاب شده به پی سی ام متصل می باشد");

                        if (newBucht.ConnectionID != null)
                            throw new Exception("بوخت جدید انتخاب شده به اتصالی پست متصل می باشد");

                        if (newBucht.CabinetInputID != null)
                            throw new Exception("بوخت جدید انتخاب شده به مرکزی کافو متصل می باشد");


                        using (TransactionScope ts = new TransactionScope())
                        {

                            Bucht CabinetInputBucht = Data.BuchtDB.GetPCMCabinetInputBucht(buchtEditing.CabinetInputID ?? 0);
                            CabinetInputBucht.BuchtIDConnectedOtherBucht = newBucht.ID;
                            CabinetInputBucht.Detach();
                            DB.Save(CabinetInputBucht);

                            newBucht.SwitchPortID = buchtEditing.SwitchPortID;
                            newBucht.CabinetInputID = buchtEditing.CabinetInputID;
                            newBucht.BuchtTypeID = buchtEditing.BuchtTypeID;
                            newBucht.PCMPortID = buchtEditing.PCMPortID;
                            newBucht.PortNo = buchtEditing.PortNo;
                            newBucht.ConnectionID = buchtEditing.ConnectionID;
                            newBucht.Status = buchtEditing.Status;
                            newBucht.BuchtIDConnectedOtherBucht = buchtEditing.BuchtIDConnectedOtherBucht;
                            newBucht.Detach();
                            DB.Save(newBucht);

                            buchtEditing.BuchtIDConnectedOtherBucht = null;
                            buchtEditing.SwitchPortID = null;
                            buchtEditing.CabinetInputID = null;
                            buchtEditing.BuchtTypeID = (int)DB.BuchtType.CustomerSide;
                            buchtEditing.PCMPortID = null;
                            buchtEditing.PortNo = null;
                            buchtEditing.ConnectionID = null;
                            buchtEditing.Status = (int)DB.BuchtStatus.Free;
                            buchtEditing.Detach();
                            DB.Save(buchtEditing);


                            ts.Complete();
                        }

                        LoadData();


                    }


                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در دخیره اطلاعات", ex);
            }
        }

        private void InputConnectionDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (InputConnectionDataGrid.SelectedItem != null)
            {

                ConnectionForPCM obj = InputConnectionDataGrid.SelectedItem as ConnectionForPCM;

                if (obj != null && obj.VerticalCloumnID != null && obj.MDFID != null)
                {
                    _columns = new ObservableCollection<CheckableItem>(DB.GetConnectionColumnInfo(obj.MDFID ?? 0));
                }
                else
                {
                    if (_columns != null && obj.rowIndex != 0)
                    {
                        _columns.Clear();
                    }
                }

                if (obj != null && obj.VerticalCloumnID != null)
                {
                    _rows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo((int)obj.VerticalCloumnID));
                }
                else
                {
                    if (_rows != null && obj.rowIndex != 0)
                    {
                        _rows.Clear();
                    }
                }

                if (obj != null && obj.VerticalRowID != null)
                {
                    _buchtNos = new ObservableCollection<CheckableItem>(DB.GetConnectionBuchtInfo((int)obj.VerticalRowID, true));
                }
                else
                {
                    if (_buchtNos != null && obj.rowIndex != 0)
                    {
                        _buchtNos.Clear();
                    }
                }
            }

        }

        #endregion

    }
}
