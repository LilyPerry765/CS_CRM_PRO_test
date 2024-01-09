using CRM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for PCMInstalationForm.xaml
    /// </summary>
    public partial class PCMInstalationForm : Local.PopupWindow
    {
        private List<Bucht> NewInputBuchtList = new List<Bucht>();
        private List<Bucht> OldInputBuchtList = new List<Bucht>();
        private List<PCMPort> OldPCMPortList = new List<PCMPort>();
        private Bucht NewOutBucht = new Bucht();

        private Bucht _buchtConnectedToInput = new Bucht();

        private PCMType _PCMType = new PCMType();
        public ObservableCollection<CheckableItem> _MDFLists { get; set; }
        public ObservableCollection<CheckableItem> _columns { get; set; }
        public ObservableCollection<CheckableItem> _rows { get; set; }
        public ObservableCollection<CheckableItem> _buchtNos { get; set; }

        public ObservableCollection<ConnectionForPCM> _connectionForPCMs { get; set; }

        List<Telephone> telephones { get; set; }

        int centerID = 0;
        public PCMInstalationForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PCMBrandComboBox.ItemsSource = Data.PCMBrandDB.GetPCMBrandCheckable();
            PCMTypeComboBox.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();


        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            CityComboBox.SelectedIndex = 0;
            PCMBrandComboBox.SelectedIndex = 0;
            PCMTypeComboBox.SelectedIndex = 3;

            List<ConnectionForPCM> firstItem = new List<ConnectionForPCM>();
            firstItem.Add(new ConnectionForPCM());
            _connectionForPCMs = new ObservableCollection<ConnectionForPCM>(firstItem);
            InputConnectionDataGrid.ItemsSource = _connectionForPCMs;
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedValue != null)
            {
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId((int)CityComboBox.SelectedValue);
                CenterComboBox.SelectedIndex = 1;
                CenterComboBox_SelectionChanged(null, null);
            }
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                centerID = (int)CenterComboBox.SelectedValue;
                OutputConnectionUserControl.CenterID = centerID;

                CabinetComboBox.ItemsSource = CabinetDB.GetCabinetCheckableByCenterID(centerID);
                _MDFLists = new ObservableCollection<CheckableItem>(Data.MDFDB.GetMDFCheckableByCenterID(centerID));

            }
        }

        private void InputConnectionDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (InputConnectionDataGrid.SelectedItem != null)
            {
                ConnectionForPCM obj = InputConnectionDataGrid.SelectedItem as ConnectionForPCM;
                if (obj != null && obj.MDFID != null)
                {
                    _columns = new ObservableCollection<CheckableItem>(DB.GetConnectionColumnInfo((int)obj.MDFID));
                    if (obj.VerticalCloumnID != null)
                    {
                        _rows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo((int)obj.VerticalCloumnID));
                        if (obj.VerticalRowID != null)
                        {
                            _buchtNos = new ObservableCollection<CheckableItem>(DB.GetConnectionBuchtInfo((int)obj.VerticalRowID, true));
                        }
                        else
                        {
                            obj.VerticalRowNo = null;
                        }
                    }
                    else
                    {
                        obj.VerticalRowID = null;
                        obj.VerticalRowNo = null;
                        obj.VerticalCloumnNo = null;

                    }


                }
                else
                {
                    obj.MDF = null;
                    obj.VerticalCloumnID = null;
                    obj.VerticalRowID = null;
                    obj.VerticalCloumnNo = null;
                    obj.VerticalRowNo = null;


                }
            }
        }

        private void PCMTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PCMTypeComboBox.SelectedValue != null)
            {
                _PCMType = Data.PCMTypeDB.GetPCMTypeByID((int)PCMTypeComboBox.SelectedValue);
            }
        }



        private void InputConnectionDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }


        private void MDFComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedItem != null)
            {
                if (InputConnectionDataGrid.SelectedItem != null)
                {
                    (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).MDF = (MDFComboBox.SelectedItem as CheckableItem).Name;

                }

                _columns = new ObservableCollection<CheckableItem>(DB.GetConnectionColumnInfo((int)(MDFComboBox.SelectedValue ?? 0)));

                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).VerticalCloumnID = null;
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).VerticalCloumnNo = null;
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).VerticalRowID = null;
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).VerticalRowNo = null;
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).BuchtID = null;
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).BuchtNo = null;

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

                _rows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo((int)ColumnComboBox.SelectedValue));
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).VerticalRowID = null;
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).VerticalRowNo = null;
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).BuchtID = null;
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).BuchtNo = null;
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

                _buchtNos = new ObservableCollection<CheckableItem>(DB.GetConnectionBuchtInfo((int)RowComboBox.SelectedValue, true));
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).BuchtID = null;
                (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).BuchtNo = null;
            }
        }

        private void ConnectionComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectionComboBox.SelectedValue != null)
            {
                if (InputConnectionDataGrid.SelectedItem != null)
                {
                    (InputConnectionDataGrid.SelectedItem as ConnectionForPCM).BuchtNo = (ConnectionComboBox.SelectedItem as CheckableItem).Name;

                }
            }
        }



        private void CabinetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetComboBox.SelectedValue != null)
            {
                CabinetInputComboBox.ItemsSource = CabinetDB.GetFreeCabinetInputByCabinetIDWithTelephon((int)CabinetComboBox.SelectedValue);
                PostComboBox.ItemsSource = PostDB.GetPostCheckableByCabinetID((int)CabinetComboBox.SelectedValue);
            }
        }

        private void PostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostComboBox.SelectedValue != null)
            {
                PostContactComboBox.ItemsSource = PostContactDB.GetFreePostContactByPostIDWithOutPCM((int)PostComboBox.SelectedValue);
            }
        }

        private void OutputConnectionUserControl_DoConnectionChange()
        {
            if (_PCMType != null && OutputConnectionUserControl.BuchtID != 0)
            {
                _connectionForPCMs.Clear();
                _connectionForPCMs = new ObservableCollection<ConnectionForPCM>(Data.BuchtDB.GetBuchtTheNumberByOutBucht(OutputConnectionUserControl.BuchtID, _PCMType.OutLine));
                InputConnectionDataGrid.ItemsSource = _connectionForPCMs;
            }
        }

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

        private void SaveForm(object sender, RoutedEventArgs e)
        {
          this.Cursor = Cursors.Wait;
          DateTime currentDateTime = DB.GetServerDate();
          try
          {
            string errorMesage =  DataVerification();
            if (!string.IsNullOrEmpty(errorMesage))
                throw new Exception(errorMesage);


            using (TransactionScope ts = new TransactionScope())
            {
                PCMRock pcmRock = PCMRockDB.GetPCMRockByNumber(centerID, Convert.ToInt32(RockTextBox.Text.Trim()));
                PostContact postContact = Data.PostContactDB.GetPostContactByID((long)PostContactComboBox.SelectedValue);
                Post post = Data.PostDB.GetPostByID((int)PostComboBox.SelectedValue);
                Cabinet cabinet = Data.CabinetDB.GetCabinetByID((int)CabinetComboBox.SelectedValue);
                CabinetInput cabinetInput = Data.CabinetInputDB.GetCabinetInputByID((long)CabinetInputComboBox.SelectedValue);

                if (pcmRock == null)
                {
                    pcmRock = new PCMRock();
                    pcmRock.CenterID = centerID;
                    pcmRock.Number = Convert.ToInt32(RockTextBox.Text.Trim());
                    pcmRock.Detach();
                    DB.Save(pcmRock);
                }

                PCMShelf pCMShelf = PCMShelfDB.GetPCMShelfByNumber(pcmRock.ID, Convert.ToInt32(ShelfTextBox.Text.Trim()));

                if (pCMShelf == null)
                {
                    pCMShelf = new PCMShelf();
                    pCMShelf.PCMRockID = pcmRock.ID;
                    pCMShelf.Number = Convert.ToInt32(ShelfTextBox.Text.Trim());
                    pCMShelf.Detach();
                    DB.Save(pCMShelf);
                }



                PCM _PCM = PCMDB.GetPCMByNumber(pCMShelf.ID, Convert.ToInt32(CardTextBox.Text.Trim()));

                if (_PCM != null)
                {
                    throw new Exception("کارت پی سی ام موجود می باشد");
                }
                else
                {
                    _PCM = new PCM();
                }

                 _PCM.ShelfID = pCMShelf.ID;
                 _PCM.Card = Convert.ToInt32(CardTextBox.Text.Trim());
                 _PCM.PCMBrandID = (int)PCMBrandComboBox.SelectedValue;
                 _PCM.PCMTypeID = _PCMType.ID;
                 _PCM.Status = (byte)DB.PCMStatus.Connection;
                 _PCM.InsertDate = currentDateTime;
                 _PCM.Detach();
                 Save(_PCM);

                 List<PCMPort> PCMPortList = new List<PCMPort>();
                 PCMPort pCMPort = new PCMPort();
                 pCMPort.PCMID = _PCM.ID;
                 pCMPort.PortNumber = 1;
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
                     pCMPort.PortNumber = i;
                     pCMPort.Status = (byte)DB.PCMPortStatus.Empty;
                     pCMPort.PortType = (byte)DB.BuchtType.InLine;
                     pCMPort.Detach();
                     PCMPortList.Add(pCMPort);
                 }
                 DB.SaveAll(PCMPortList);


                 List<PostContact> postContactList = new List<PostContact>();
                 for (int i = 0; i < PCMPortList.Count; i++)
                 {
                     PostContact item = new PostContact();
                     item.Status = (byte)DB.PostContactStatus.Free;
                     item.ConnectionType = (byte)DB.PostContactConnectionType.PCMNormal;
                     item.ConnectionNo = postContact.ConnectionNo;
                     item.PostID = postContact.PostID;
                     postContactList.Add(item);
                 }

                 DB.SaveAll(postContactList);

                 List<RequestLog> requestLogs = new List<RequestLog>();
                 requestLogs.Clear();
                 for (int i = 0; i < NewInputBuchtList.Count(); i++)
                 {
                     NewInputBuchtList[i].Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                     NewInputBuchtList[i].PCMPortID = PCMPortList[i].ID;
                     if(_connectionForPCMs.Any(t => t.BuchtID == NewInputBuchtList[i].ID && t.TelehoneNo != null))
                     {
                             NewInputBuchtList[i].SwitchPortID = telephones.Where(t2 => t2.TelephoneNo == _connectionForPCMs.Where(t => t.BuchtID == NewInputBuchtList[i].ID).Select(t => (long)t.TelehoneNo).SingleOrDefault()).SingleOrDefault().SwitchPortID;
                             NewInputBuchtList[i].Status = (byte)DB.BuchtStatus.Connection;
                             Telephone teleItem = telephones.Where(t2 => t2.TelephoneNo == _connectionForPCMs.Where(t => t.BuchtID == NewInputBuchtList[i].ID).Select(t => (long)t.TelehoneNo).SingleOrDefault()).SingleOrDefault();
                             if (teleItem != null)
                             {
                                 if (teleItem.Status == (int)DB.TelephoneStatus.Free || teleItem.Status == (int)DB.TelephoneStatus.Discharge)
                                     telephones.Where(t2 => t2.TelephoneNo == _connectionForPCMs.Where(t => t.BuchtID == NewInputBuchtList[i].ID).Select(t => (long)t.TelehoneNo).SingleOrDefault()).SingleOrDefault().Status = (int)DB.TelephoneStatus.Connecting;

                                 RequestLog requestLog = new RequestLog();
                                 requestLog.IsReject = false;
                                 requestLog.TelephoneNo = teleItem.TelephoneNo;
                                 requestLog.UserID = DB.currentUser.ID;
                                 requestLog.Date = currentDateTime;
                                 requestLog.RequestTypeID = (int)DB.RequestType.PCMInstallation;
                                 Data.Schema.PCMInstalationTelephone pcmInstalationTelephone = new Data.Schema.PCMInstalationTelephone();

                                 pcmInstalationTelephone.Rock = Convert.ToInt32(RockTextBox.Text.Trim());
                                 pcmInstalationTelephone.Shelf = Convert.ToInt32(ShelfTextBox.Text.Trim());
                                 pcmInstalationTelephone.Card = Convert.ToInt32(CardTextBox.Text.Trim());

                                 pcmInstalationTelephone.Cabinet = cabinet.CabinetNumber;
                                 pcmInstalationTelephone.CabinetInput = cabinetInput.InputNumber;
                                 pcmInstalationTelephone.Post = post.Number;
                                 pcmInstalationTelephone.PostContact = postContact.ConnectionNo;

                                 pcmInstalationTelephone.Bucht = BuchtDB.GetConnectionByBuchtID(NewInputBuchtList[i].ID);

                                 requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.PCMInstalationTelephone>(pcmInstalationTelephone, true));

                                 requestLogs.Add(requestLog);

                                 postContactList[i].Status = (int)DB.PostContactStatus.CableConnection;

                             }
                             else
                             {
                                 throw new Exception(string.Format("تلفن {0} صحیح نمی باشد", _connectionForPCMs.Where(t => t.BuchtID == NewInputBuchtList[i].ID).Select(t => (long)t.TelehoneNo).SingleOrDefault()));
                             }

                        
                     }
                     NewInputBuchtList[i].BuchtTypeID = (byte)DB.BuchtType.InLine;
                     NewInputBuchtList[i].PortNo = (byte)PCMPortList[i].PortNumber;
                     NewInputBuchtList[i].ConnectionID = postContactList[i].ID;
                     NewInputBuchtList[i].CabinetInputID = cabinetInput.ID;
                     NewInputBuchtList[i].Detach();
                 }



              
                 postContact.Status = (byte)DB.PostContactStatus.NoCableConnection;
                 postContact.ConnectionType = (byte)DB.PostContactConnectionType.PCMRemote;
                 postContact.Detach();
                 DB.Save(postContact);

                 NewOutBucht.Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                 NewOutBucht.ConnectionID = postContact.ID;
                 NewOutBucht.CabinetInputID = (long)CabinetInputComboBox.SelectedValue;
                 NewOutBucht.BuchtTypeID = (byte)DB.BuchtType.OutLine;
                 NewOutBucht.BuchtIDConnectedOtherBucht = _buchtConnectedToInput.ID;
                 NewOutBucht.Detach();
                 Save(NewOutBucht);


                 // بوخت ورودی انتخاب شده را به بوخت خروجی پی سی ام نسبت میدهد
                 _buchtConnectedToInput.BuchtIDConnectedOtherBucht = NewOutBucht.ID;
                 _buchtConnectedToInput.ConnectionID = postContact.ID;
                 _buchtConnectedToInput.Status = (byte)DB.BuchtStatus.AllocatedToInlinePCM;
                 _buchtConnectedToInput.Detach();
                 DB.Save(_buchtConnectedToInput);

                 postContactList.ForEach(t => t.Detach());
                 DB.UpdateAll(postContactList);

                 NewInputBuchtList.ForEach(t => t.Detach());
                 DB.UpdateAll(NewInputBuchtList);

                 telephones.ForEach(t => t.Detach());
                 DB.UpdateAll(telephones);

                 DB.SaveAll(requestLogs);
                ts.Complete();
            }

            _connectionForPCMs.Clear();
            PostContactComboBox.SelectedIndex = -1;
            PostComboBox.SelectedIndex = -1;
            CabinetComboBox.SelectedIndex = -1;
            CabinetInputComboBox.SelectedIndex = -1;
            OutputConnectionUserControl.Reset();
            CardTextBox.Text = string.Empty;

            this.Cursor = Cursors.Arrow;
            ShowSuccessMessage("ذخیره پی سی ام انجام شد");

          }
          catch(Exception ex)
          {
              this.Cursor = Cursors.Arrow;
              ShowErrorMessage("خطا در ذخیره اطلاعات" , ex);
          }
        }

        private string DataVerification()
        {
            string errorMesage = string.Empty;

           if(PostContactComboBox.SelectedValue == null)
               errorMesage += string.Format("\nلطفا اتصالی پست را وارد کنید");

           if (PostComboBox.SelectedValue == null)
               errorMesage += string.Format("\nلطفا پست را وارد کنید");

           if (CabinetComboBox.SelectedValue == null)
               errorMesage += string.Format("لطفا کافو را وارد کنید\n");

           if (CabinetInputComboBox.SelectedValue == null)
               errorMesage += string.Format("لطفا مرکزی را وارد کنید\n");


           if (string.IsNullOrEmpty(RockTextBox.Text))
               errorMesage += string.Format("\nلطفا رک را وارد کنید");

           if (string.IsNullOrEmpty(ShelfTextBox.Text))
               errorMesage += string.Format("\nلطفا شلف را وارد کنید");

           if (string.IsNullOrEmpty(CardTextBox.Text))
               errorMesage += string.Format("\nلطفا کارت را وارد کنید");


            if (_connectionForPCMs.Where(t => t.TelehoneNo != null).GroupBy(t => t.TelehoneNo).Any(g => g.Count() > 1))
                errorMesage += string.Format("\nتلفن تکراری نمی توان وارد کرد");

            if (_PCMType.OutLine != _connectionForPCMs.Count())
                errorMesage += string.Format("تعداد بوخت های ورودی باید {0} عدد باشد\n", _PCMType.OutLine);

            NewInputBuchtList = Data.BuchtDB.GetBuchetByListBuchtIDs(_connectionForPCMs.Select(t => t.BuchtID ?? 0).ToList());

            // بررسی تکراری بودن
            if (NewInputBuchtList.GroupBy(g => g.ID).SelectMany(t => t.Skip(1)).Count() > 0)
                errorMesage += string.Format("نمی توان اتصالی تکراری انتخاب کرد\n");


            NewOutBucht = Data.BuchtDB.GetBuchtByID((long)OutputConnectionUserControl.BuchtID);
            // بررسی موجود نبودن ورودی در خروجی ها
            if (NewInputBuchtList.Any(t => t.ID == NewOutBucht.ID) == true)
                errorMesage += string.Format("پورت خروجی انتخاب شده در پورت های ورودی موجود است\n");


            _buchtConnectedToInput = Data.BuchtDB.GetBuchtByCabinetInputID((long?)CabinetInputComboBox.SelectedValue ?? 0);
            if (_buchtConnectedToInput == null)
                errorMesage += string.Format("بوخت متصل به مرکزی یافت نشد\n");

            var x = _connectionForPCMs.Where(t => t.TelehoneNo != null).Select(t => (long)t.TelehoneNo).ToList();

             telephones = TelephoneDB.GetTelephoneByTelephoneNos(_connectionForPCMs.Where(t => t.TelehoneNo != null).Select(t => (long)t.TelehoneNo).ToList());


             if (telephones.Any(t => t.CenterID != centerID))
                 errorMesage += string.Format("تلفن {0} متعلق به مرکز انتخاب شده نمی باشد", telephones.Where(t => t.CenterID != centerID).Select(t => t.TelephoneNo.ToString()).Aggregate((i, j) => i + ',' + j));
                 
              if( _connectionForPCMs.Where(t => t.TelehoneNo != null).Count() != telephones.Count())
                  errorMesage += string.Format("\nتلفن {0} یافت نشد", _connectionForPCMs.Where(t => t.TelehoneNo != null && !telephones.Select(t2 => t2.TelephoneNo).Contains((long)t.TelehoneNo)).Select(t => t.TelehoneNo.ToString()).Aggregate((i, j) => i + ',' + j));

              List<Bucht> assignmentInfo = BuchtDB.GetBuchtBySwitchPortIDs(telephones.Select(t => (long)t.SwitchPortID).ToList());

              if (assignmentInfo.Count() != 0)
                  errorMesage += string.Format("تلفن {0} دارای تجهیزات فنی می باشد", telephones.Where(t => assignmentInfo.Select(t2=>t2.SwitchPortID).Contains(t.SwitchPortID)).Select(t => t.TelephoneNo.ToString()).Aggregate((i, j) => i + ',' + j));




            return errorMesage;







        }

        private void DeletePCM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                MessageBoxResult result = MessageBox.Show("استفاده از این فرم فقط برای اصلاح اطلاعات پی سی ام می باشد با حذف پی سی ام تلفن های آن تخلیه نمی گردد\n آیا پی سی ام حذف شود؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    DateTime currentDataTime = DB.GetServerDate();
                    string errorMesage = string.Empty;
                    PCM _PCM = new PCM();
                    PCMRock pcmRock = PCMRockDB.GetPCMRockByNumber(centerID, Convert.ToInt32(RockTextBox.Text.Trim()));
                    PCMShelf pCMShelf = new PCMShelf();

                    if (pcmRock != null)
                    {
                        pCMShelf = PCMShelfDB.GetPCMShelfByNumber(pcmRock.ID, Convert.ToInt32(ShelfTextBox.Text.Trim()));
                        if (pCMShelf != null)
                        {
                            _PCM = PCMDB.GetPCMByNumber(pCMShelf.ID, Convert.ToInt32(CardTextBox.Text.Trim()));
                            if(_PCM == null)
                            {
                                errorMesage += string.Format("کارت یافت نشد\n");
                            }
                        }
                        else
                        {
                            errorMesage += string.Format("شلف یافت نشد\n");
                        }
                    }
                    else
                    {
                        errorMesage += string.Format("\nرک یافت نشد ");
                    }


                    if (!string.IsNullOrEmpty(errorMesage))
                       throw new Exception(errorMesage);
              
                    using (TransactionScope ts = new TransactionScope())
                    {
                        List<PCMPort> Ports = Data.PCMPortDB.GetAllPCMPortByPCMID(_PCM.ID);

                        List<Bucht> oldbucht = new List<Bucht>();
                        List<Bucht> deleteBucht = Data.BuchtDB.getBuchtByPCMPortID(Ports.Select(t => t.ID).ToList());
                        if (deleteBucht.Count() > 1)
                        {
                            Bucht CabinetInputBucht = BuchtDB.GetPCMCabinetInputBucht(deleteBucht.Take(1).Select(t => (long)t.CabinetInputID).SingleOrDefault());
                            if (CabinetInputBucht != null)
                            {
                                CabinetInputBucht.Status = (int)DB.BuchtStatus.Free;
                                CabinetInputBucht.BuchtIDConnectedOtherBucht = null;
                                CabinetInputBucht.SwitchPortID = null;
                                CabinetInputBucht.ConnectionID = null;
                                CabinetInputBucht.Detach();
                                DB.Save(CabinetInputBucht);
                            }
                        }


                        List<PostContact> postContacts = PostContactDB.GetPostContactByIDs(deleteBucht.Where(t => t.ConnectionID.HasValue).Select(t => t.ConnectionID.Value).ToList());
                        foreach (PostContact obj in postContacts)
                        {
                            if (obj.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal)
                            {
                                obj.Status = (int)DB.PostContactStatus.Deleted;
                            }
                            else if (obj.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                            {
                                obj.ConnectionType = (int)DB.PostContactConnectionType.Noraml;
                                obj.Status = (int)DB.PostContactStatus.Free;
                            }
                            obj.Detach();
                        }

                        DB.UpdateAll(postContacts);

                        List<RequestLog> requestLogs = new List<RequestLog>();

                        List<Telephone> telelist = TelephoneDB.GetTelephoneBySwitchIDs(deleteBucht.Where(t=>t.SwitchPortID != null).Select(t=>(int)t.SwitchPortID).ToList());
                        foreach (Bucht obj in deleteBucht)
                        {

                            Telephone teleItem = telelist.Where(t => t.SwitchPortID == obj.SwitchPortID).SingleOrDefault();
                            if (teleItem != null)
                            {
                                RequestLog requestLog = new RequestLog();
                                requestLog.IsReject = false;
                                requestLog.TelephoneNo = teleItem.TelephoneNo;
                                requestLog.UserID = DB.currentUser.ID;
                                requestLog.Date = currentDataTime;
                                requestLog.RequestTypeID = (int)DB.RequestType.DeletePCMInstallation;
                                Data.Schema.DeletePCMInstalation deletePcmInstalation = new Data.Schema.DeletePCMInstalation();


                                deletePcmInstalation.Rock = pcmRock.Number;
                                deletePcmInstalation.Shelf = pCMShelf.Number;
                                deletePcmInstalation.Card = _PCM.Card;

                                deletePcmInstalation.Bucht = BuchtDB.GetConnectionByBuchtID(obj.ID);

                                requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.DeletePCMInstalation>(deletePcmInstalation, true));

                                requestLogs.Add(requestLog);
                            }
                            else if (obj.SwitchPortID != null)
                            {
                                throw new Exception("نقص اطلاعات در تلفن");
                            }
                           


                            //TODO:
                            obj.BuchtTypeID = (byte)DB.BuchtType.CustomerSide;
                            obj.BuchtIDConnectedOtherBucht = null;
                            obj.SwitchPortID = null;
                            obj.PCMPortID = null;
                            obj.CabinetInputID = null;
                            obj.PortNo = null;
                            obj.ConnectionID = null;
                            obj.Status = (byte)DB.BuchtStatus.Free;
                            obj.Detach();
                            oldbucht.Add(obj);
                        }



                        DB.UpdateAll(oldbucht);

                        DB.SaveAll(requestLogs);

                        DB.Delete<Data.PCM>(_PCM.ID);

                        ts.Complete();
                    }

                }
                this.Cursor = Cursors.Arrow;
                ShowSuccessMessage("پی سی ام حذف شد");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Arrow;
                ShowErrorMessage("خطا در حذف پی سی ام", ex);
            }
        }
    }
}
