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
using System.Xml.Linq;


namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for PCMAssignment.xaml
    /// </summary>
    public partial class PCMAssignment : Local.PopupWindow
    {

        #region Properties and Fields

        private CRM.Data.AssignmentInfo _assignmentInfo { get; set; }
        Bucht buchtConnectedToInput;
        private int _centerID { get; set; }
        private Data.Schema.PCMInstall actionLogPCMInstall = new Data.Schema.PCMInstall();
        PCM pCM = new PCM();

        #endregion

        #region Constructors

        public PCMAssignment()
        {
            InitializeComponent();

        }

        public PCMAssignment(Data.AssignmentInfo assignmentInfo, int centerID)
            : this()
        {
            this._centerID = centerID;
            this._assignmentInfo = assignmentInfo;
            Initialize();
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
                using (TransactionScope ts = new TransactionScope())
                {

                    // Get PCM ports list
                    List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID((int)MUIDComboBox.SelectedValue).ToList();

                    //Get all bucht of PCM
                    List<Bucht> buchtInlineList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();

                    // Separating outline bucht and inline bucht
                    Bucht buchtPCMOutline = buchtInlineList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault();
                    buchtInlineList = buchtInlineList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.InLine).ToList();
                    //


                    // Separating input PCM port 
                    pCMPortList = pCMPortList.Where(t => t.PortType == (byte)DB.BuchtType.InLine).ToList();

                    // Get Remote PostContact
                    //اتصالی پستی که قرار است پی سی ام روی آن قرار گیرد بدست میاوریم
                    PostContact postContact = Data.PostContactDB.GetPostContactByID((long)_assignmentInfo.PostContactID);
                    //
                    
                    if (_assignmentInfo.PostContactStatus == (byte)DB.PostContactStatus.Free) //انتساب پی سی ام در حالتی که اتصالی آزاداست - راد
                    {
                        if (MUIDComboBox.SelectedValue != null && InputComboBox.SelectedValue != null)
                        {
                            // ابتدا اتصالی ریموت را با رکورد انتخاب شده کاربر مقدار میده سپس به تعداد باقی اتصالی های رکورد اضافه میکند
                            // اتصالی ریموت پی سی ام را مقدار دهی میکند
                            postContact.Status = (byte)DB.PostContactStatus.NoCableConnection;
                            postContact.ConnectionType = (byte)DB.PostContactConnectionType.PCMRemote;
                            postContact.Detach();
                            DB.Save(postContact);

                            //شناسه اتصالی پستی که توسط کاربر برای انتساب پی سی ام در لیست اتصالی های پست انتخاب شده است ، به بوخت خروجی پی سی ام و بوخت طرف مشترک منتسب میشود - راد
                            buchtPCMOutline.ConnectionID = postContact.ID;
                            buchtPCMOutline.CabinetInputID = (long)InputComboBox.SelectedValue;
                            buchtPCMOutline.Detach();
                            // بوخت ورودی پی سی ام را به ورودی انتخاب شده انتساب می دهد
                            buchtPCMOutline.BuchtIDConnectedOtherBucht = buchtConnectedToInput.ID;
                            DB.Save(buchtPCMOutline);

                            // بوخت ورودی انتخاب شده را به بوخت خروجی پی سی ام نسبت میدهد
                            buchtConnectedToInput.BuchtIDConnectedOtherBucht = buchtPCMOutline.ID;
                            buchtConnectedToInput.ConnectionID = postContact.ID;
                            buchtConnectedToInput.Status = (byte)DB.BuchtStatus.AllocatedToInlinePCM;
                            buchtConnectedToInput.Detach();
                            DB.Save(buchtConnectedToInput);

                            List<PostContact> postContactList = new List<PostContact>();
                            for (int i = 0; i < pCMPortList.Count; i++)
                            {
                                //مشترک بر روی اتصالی ها داخل این بلاک میتواند قرار بگیرد - راد
                                PostContact item = new PostContact();
                                item.Status = (byte)DB.PostContactStatus.Free;
                                item.ConnectionType = (byte)DB.PostContactConnectionType.PCMNormal;
                                item.ConnectionNo = postContact.ConnectionNo;
                                item.PostID = postContact.PostID;
                                postContactList.Add(item);
                            }

                            DB.SaveAll(postContactList);

                            for (int i = 0; i < postContactList.Count; i++)
                            {
                                //کلیه رکورد های داخل لیست زیر نوع بوختشان ورودی است
                                //buchtInlineList[i].BuchtTypeID = 8 
                                buchtInlineList[i].ConnectionID = postContactList[i].ID;
                                buchtInlineList[i].CabinetInputID = (long)InputComboBox.SelectedValue;
                                buchtInlineList[i].Detach();
                            }
                            DB.UpdateAll(buchtInlineList);

                            pCM.Status = (byte)DB.PCMStatus.Connection;
                            pCM.InstallAddress = AddressTextBox.Text;
                            pCM.InstallPostCode = PostCodeTextBox.Text;
                            pCM.Detach();
                            DB.Save(pCM);
                        }
                        else
                        {
                            throw new Exception("لطفا مشخصه پی سی ام را انتخاب کنید");
                        }

                    }
                    else if (_assignmentInfo.PostContactStatus == (byte)DB.PostContactStatus.CableConnection) //انتساب پی سی ام در حالتی که اتصالی به یک تلفن وصل است - راد
                    {
                        PCMPort FirstPCMPort = pCMPortList.OrderBy(t => t.PortNumber).Take(1).SingleOrDefault();
                        Bucht FirstBuchtConnectToPCM = Data.BuchtDB.getBuchtByPCMPortID(new List<int> { FirstPCMPort.ID }).SingleOrDefault();


                        int switchportID = (int)buchtConnectedToInput.SwitchPortID;

                        // اتصال به پورت سوئیچ بوخت متصل فعلی را قطع میکند
                        buchtConnectedToInput.SwitchPortID = null;
                        buchtConnectedToInput.Status = (byte)DB.BuchtStatus.AllocatedToInlinePCM;
                        buchtConnectedToInput.BuchtIDConnectedOtherBucht = buchtPCMOutline.ID;
                        buchtConnectedToInput.Detach();
                        DB.Save(buchtConnectedToInput);
                        //

                        // بوخت متصل فعلی را به اولین بوخت پس سی ام انتساب میدهد
                        FirstBuchtConnectToPCM.SwitchPortID = switchportID;
                        FirstBuchtConnectToPCM.Status = (byte)DB.BuchtStatus.Connection;
                        FirstBuchtConnectToPCM.Detach();
                        DB.Save(FirstBuchtConnectToPCM);
                        //


                        //
                        buchtInlineList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();
                        buchtInlineList = buchtInlineList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.InLine).ToList();
                        //



                        // بوخت ورودی پی سی ام را تنظیم میکند
                        buchtPCMOutline.ConnectionID = postContact.ID;
                        buchtPCMOutline.CabinetInputID = buchtConnectedToInput.CabinetInputID;
                        buchtPCMOutline.BuchtIDConnectedOtherBucht = buchtConnectedToInput.ID;
                        buchtPCMOutline.Detach();
                        DB.Save(buchtPCMOutline);
                        //

                        // تنظیم اتصالی های پست
                        postContact.ConnectionType = (byte)DB.PostContactConnectionType.PCMRemote;
                        postContact.Status = (byte)DB.PostContactStatus.NoCableConnection;
                        postContact.Detach();
                        DB.Save(postContact);

                        //


                        // ایجاد اتصالی های پست جدید و مقدار دهی به بوخت ها

                        List<PostContact> postContactList = new List<PostContact>();
                        for (int i = 0; i < pCMPortList.Count; i++)
                        {
                            PostContact item = new PostContact();
                            item.Status = (byte)DB.PostContactStatus.Free;
                            item.ConnectionType = (byte)DB.PostContactConnectionType.PCMNormal;
                            item.ConnectionNo = postContact.ConnectionNo;
                            item.PostID = postContact.PostID;
                            postContactList.Add(item);
                        }

                        DB.SaveAll(postContactList);

                        for (int i = 0; i < postContactList.Count; i++)
                        {
                            buchtInlineList[i].ConnectionID = postContactList[i].ID;
                            buchtInlineList[i].CabinetInputID = buchtConnectedToInput.CabinetInputID;
                            buchtInlineList[i].Detach();
                        }
                        DB.UpdateAll(buchtInlineList);

                        PostContact postContactConnectForChangeStatus = Data.PostContactDB.GetPostContactByID((long)buchtInlineList.Where(t => t.ID == FirstBuchtConnectToPCM.ID).Select(t => t.ConnectionID).SingleOrDefault());
                        postContactConnectForChangeStatus.Status = (byte)DB.PostContactStatus.CableConnection;
                        postContactConnectForChangeStatus.Detach();
                        DB.Save(postContactConnectForChangeStatus);

                        pCM.Status = (byte)DB.PCMStatus.Connection;
                        pCM.InstallAddress = AddressTextBox.Text;
                        pCM.InstallPostCode = PostCodeTextBox.Text;
                        pCM.Detach();
                        DB.Save(pCM);

                        //

                    }
                    actionLogPCMInstall.CenterID = Data.CenterDB.GetCenterById(_centerID).ID;
                    actionLogPCMInstall.Center = Data.CenterDB.GetCenterById(_centerID).CenterName;
                    var shelf = Data.PCMShelfDB.GetPCMShelfByID(pCM.ShelfID);
                    actionLogPCMInstall.Rock = Data.PCMRockDB.GetPCMRockByID(shelf.PCMRockID).Number.ToString();
                    actionLogPCMInstall.Shelf = shelf.Number.ToString();
                    actionLogPCMInstall.Card = pCM.Card.ToString();
                    actionLogPCMInstall.Type = Data.PCMBrandDB.GetPCMPrandByPCMID(pCM.ID).Name + " " + (pcmTypeComboBox.SelectedItem as CheckableItem).Name;


                    var post = Data.PostDB.GetPostByID(postContact.PostID);
                    var cabinet = Data.CabinetDB.GetCabinetByID(post.CabinetID);
                    actionLogPCMInstall.Post = post.Number.ToString();
                    actionLogPCMInstall.Cabinet = cabinet.CabinetNumber.ToString() + DB.GetDescription(cabinet.CabinetCode);
                    actionLogPCMInstall.PostContact = postContact.ConnectionNo.ToString();

                    ActionLog actionLog = new ActionLog();
                    actionLog.ActionID = (byte)DB.ActionLog.PCMInstall;
                    actionLog.UserName = Folder.User.Current.Username;
                    actionLogPCMInstall.Date = actionLog.Date = DB.GetServerDate();
                    actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.PCMInstall>(actionLogPCMInstall, true));
                    actionLog.Detach();
                    DB.Save(actionLog);
                    ts.Complete();
                    ShowSuccessMessage("انتساب پی سی ام انجام شد");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ثبت اطلاعات", ex);
            }
        }

        private void MUIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MUIDComboBox.SelectedValue != null)
            {
                pCM = DB.SearchByPropertyName<PCM>("ID", (int)MUIDComboBox.SelectedValue).SingleOrDefault();
                pcmTypeComboBox.SelectedValue = pCM.PCMTypeID;
            }
        }

        private void InputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InputComboBox.SelectedValue != null)
            {
                BuchtNoTextBox.Text = "";

                /// 
                List<int> cabinetIDList = new List<int>();
                cabinetIDList.Add((int)_assignmentInfo.CabinetID);

                buchtConnectedToInput = Data.BuchtDB.GetBuchtByCabinetIDs(cabinetIDList)
                                                    .Where(t => t.CabinetInputID == (long)InputComboBox.SelectedValue)
                                                    .SingleOrDefault();

                if (buchtConnectedToInput != null)
                {
                    BuchtNoTextBox.Text = DB.GetConnectionByBuchtID(buchtConnectedToInput.ID);
                }
                ///
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            MUIDComboBox.ItemsSource = Data.PCMDB.GetPCMCheckable(_centerID, (byte)DB.PCMStatus.Install);

            pcmTypeComboBox.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();


            // ورودی های خالی را بدست می اورد
            List<int> cabinetIDList = new List<int>();
            cabinetIDList.Add((int)_assignmentInfo.CabinetID);
            List<long?> cabinetIDListConnect = Data.BuchtDB.GetBuchtByCabinetIDs(cabinetIDList)
                                                           .Where(t => t.CabinetInputID != null && t.ConnectionID != null)
                                                           .Select(t => t.CabinetInputID)
                                                           .ToList();

            //**************************************************************************************************************************************************************************
            if (_assignmentInfo.PostContactStatus == (byte)DB.PostContactStatus.CableConnection)
            {
                List<CheckableItem> obj = new List<CheckableItem>();
                obj.Add(new CheckableItem
                {
                    LongID = (long)_assignmentInfo.InputNumberID,
                    Name = _assignmentInfo.InputNumber.ToString(),
                    IsChecked = false
                }
                       );

                InputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID(_assignmentInfo.CabinetID ?? -1)
                                                               .Where(t => !cabinetIDListConnect.Contains(t.LongID))
                                                               .Union(obj);

                InputComboBox.SelectedValue = (long)_assignmentInfo.InputNumberID;
                InputComboBox_SelectionChanged(null, null);
                InputComboBox.IsEnabled = false;
            }
            else if (_assignmentInfo.PostContactStatus == (byte)DB.PostContactStatus.Free)
            {
                InputComboBox.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID(_assignmentInfo.CabinetID ?? -1)
                                                               .Where(t => !cabinetIDListConnect.Contains(t.LongID))
                                                               .ToList();
            }
            //**************************************************************************************************************************************************************************
        }

        #endregion
    }
}
