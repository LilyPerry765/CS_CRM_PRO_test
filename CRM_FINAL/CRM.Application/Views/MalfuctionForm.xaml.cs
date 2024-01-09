using CRM.Data;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CabinetInputMalfuctionForm.xaml
    /// </summary>
    public partial class MalfuctionForm : Local.PopupWindow
    {
        #region Properties and Fields

        private long _ID = 0;
        Malfuction Malfuction = new Malfuction();

        CabinetInput _cabinetInput;
        PostContact _postContact;
        PCMPort _pCMPort;
        PCM _PCM;
        private Data.Schema.PortMalfaction actionLogPortMalfaction = new Data.Schema.PortMalfaction();
        private byte _malfactionType = 0;

        #endregion

        #region Constructors

        public MalfuctionForm()
        {
            InitializeComponent();
        }

        public MalfuctionForm(long ID, byte malfactionType)
            : this()
        {
            _malfactionType = malfactionType;
            _ID = ID;
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.MalfuctionStatus));
        }

        #endregion

        #region EventHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime CurrentDate = DB.GetServerDate();
            switch (_malfactionType)
            {
                case (byte)DB.MalfuctionType.CabinetInput:
                    {
                        _cabinetInput = new CabinetInput();
                        _cabinetInput = Data.CabinetInputDB.GetCabinetInputByID(_ID);
                        MalfunctionTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.CabinetInputMalfuctionType));
                        break;
                    }

                case (byte)DB.MalfuctionType.PostConntact:
                    { 
                        _postContact = new PostContact(); 
                        _postContact = Data.PostContactDB.GetPostContactByID(_ID); 
                        MalfunctionTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PostContactMalfuctionType)); 
                        break; 
                    }
                case (byte)DB.MalfuctionType.PCMPort:
                    {
                        _pCMPort = new PCMPort();
                        _pCMPort = Data.PCMPortDB.GetPCMPortByID(_ID);
                        MalfunctionTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PCMMalfuctionType));

                        DistanceFromMDFLabel.Visibility = Visibility.Collapsed;
                        DistanceFromMDFTextBox.Visibility = Visibility.Collapsed;
                        DistanceFromCabinetLabel.Visibility = Visibility.Collapsed;
                        DistanceFromCabinetTextBox.Visibility = Visibility.Collapsed;
                        break;
                    }
                case (byte)DB.MalfuctionType.PCM:
                    {
                        _PCM = new PCM();
                        _PCM = Data.PCMDB.GetPCMByID((int)_ID);
                        MalfunctionTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PCMCardMalfuctionType));
                        break;
                    }
                default:
                    break;
            }

            Malfuction.DateMalfunction = CurrentDate;
            Malfuction.TimeMalfunction = CurrentDate.ToShortTimeString();
            Malfuction.MalfuctionType = _malfactionType;
            this.DataContext = Malfuction;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                if ((int)TypeComboBox.SelectedValue == (int)DB.MalfuctionStatus.healthy)
                {
                    # region Healthy


                    Malfuction = this.DataContext as Malfuction;
                    Malfuction.TypeMalfunction = 0;
                    switch (_malfactionType)
                    {

                        case (byte)DB.MalfuctionType.CabinetInput:
                            {
                                if (_cabinetInput.Status == (byte)DB.CabinetInputStatus.healthy) { MessageBox.Show("مرکزی هم اکنون در وضعیت سالم قرار دارد"); return; }
                                _cabinetInput.Status = (byte)DB.CabinetInputStatus.healthy;
                                Malfuction.CabinetInputID = _cabinetInput.ID;

                                ActionLog actionLog = new ActionLog();
                                actionLog.ActionID = (byte)DB.ActionLog.CablePaired;
                                actionLog.Date = DB.GetServerDate();
                                actionLog.UserName = Folder.User.Current.Username;

                                CRM.Data.Schema.CablePairedMalFuction cablePairedMalFuction = new Data.Schema.CablePairedMalFuction();

                                Center center = Data.CenterDB.GetCenterByCabinetInputID(_cabinetInput.ID);
                                cablePairedMalFuction.CenterID = center.ID;
                                cablePairedMalFuction.CenterName = center.CenterName;
                                cablePairedMalFuction.Date = DB.GetServerDate();
                                cablePairedMalFuction.Description = Malfuction.Description;
                                cablePairedMalFuction.Status = (byte)DB.CabinetInputStatus.healthy;
                                cablePairedMalFuction.CabinetInputID = _cabinetInput.ID;
                                actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.CablePairedMalFuction>(cablePairedMalFuction, true));
                                actionLog.Detach();
                                DB.Save(actionLog, true);
                                break;
                            }
                        case (byte)DB.MalfuctionType.PCMPort:
                            {
                                if (_pCMPort.Status != (byte)DB.PCMPortStatus.Malfaction) { MessageBox.Show("پورت هم اکنون در وضعیت سالم قرار دارد"); return; }
                                byte? pCMPortStatus = Data.PCMPortDB.GetStatusByCheckConnectedBucht(_pCMPort.ID);
                                if (pCMPortStatus == null)
                                {
                                    MessageBox.Show("وضعیت پورت یافت نشد");
                                    return;
                                }
                                else
                                {
                                    _pCMPort.Status = (byte)pCMPortStatus;
                                    Malfuction.PCMPortID = _pCMPort.ID;
                                }
                                break;
                            }
                        case (byte)DB.MalfuctionType.PCM:
                            {
                                if ((_PCM.Status != (byte)DB.PCMStatus.Destruction)) { MessageBox.Show("پی سی ام هم اکنون در وضعیت خراب قرار ندارد"); return; }

                                byte pcmLastStatus = Data.PCMDB.GetLastSatausOfMalfaction(_PCM.ID);
                                _PCM.Status = (byte)pcmLastStatus;
                                Malfuction.PCMID = _PCM.ID;

                                break;
                            }
                        case (byte)DB.MalfuctionType.PostConntact:
                            {
                                if (!(_postContact.Status == (byte)DB.PostContactStatus.PermanentBroken)) { MessageBox.Show("اتصالی پست هم اکنون در وضعیت سالم قرار دارد"); return; }
                                byte? postContactStatus = Data.PostContactDB.GetStatusByCheckConnectingToBucht(_postContact.ID);
                                if (postContactStatus == null)
                                {
                                    MessageBox.Show("وضعیت اتصالی یافت نشد");
                                    return;
                                }
                                else
                                {
                                    _postContact.Status = (byte)postContactStatus;
                                }
                                Malfuction.PostContactID = _postContact.ID;

                                ActionLog actionLog = new ActionLog();
                                actionLog.ActionID = (byte)DB.ActionLog.PostContact;
                                actionLog.Date = DB.GetServerDate();
                                actionLog.UserName = Folder.User.Current.Username;

                                CRM.Data.Schema.PostContactMalfuction postContactMalFuction = new Data.Schema.PostContactMalfuction();
                                Center center = Data.CenterDB.GetCenterByPostID(_postContact.PostID);
                                postContactMalFuction.CenterID = center.ID;
                                postContactMalFuction.CenterName = center.CenterName;
                                postContactMalFuction.PostContactID = _postContact.ID;
                                postContactMalFuction.Date = DB.GetServerDate();
                                postContactMalFuction.Description = Malfuction.Description;
                                postContactMalFuction.Status = (byte)postContactStatus;

                                actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.PostContactMalfuction>(postContactMalFuction, true));
                                actionLog.Detach();
                                DB.Save(actionLog, true);
                                break;
                            }

                        default:
                            break;
                    }


                    Malfuction.MalfuctionOrhealthy = (byte)DB.MalfuctionStatus.healthy;
                    Malfuction.LicenseNumber = LicenseUserControl.LisenseNumber;
                    Malfuction.LicenseFile = (System.Data.Linq.Binary)LicenseUserControl.lisenseFile;

                    using (TransactionScope scope = new TransactionScope())
                    {

                        switch (_malfactionType)
                        {

                            case (byte)DB.MalfuctionType.CabinetInput:
                                {
                                    _cabinetInput.Detach();
                                    DB.Save(_cabinetInput, false);
                                    break;
                                }
                            case (byte)DB.MalfuctionType.PCMPort:
                                {
                                    _pCMPort.Detach();
                                    DB.Save(_pCMPort, false);
                                    break;
                                }
                            case (byte)DB.MalfuctionType.PostConntact:
                                {
                                    _postContact.Detach();
                                    DB.Save(_postContact, false);
                                    break;
                                }
                            case (byte)DB.MalfuctionType.PCM:
                                {
                                    _PCM.Detach();
                                    DB.Save(_PCM, false);
                                    break;
                                }

                            default:
                                throw new Exception("خطا در ذخیره اطلاعات");

                        }
                        Malfuction.Detach();
                        DB.Save(Malfuction, true);

                        scope.Complete();
                    }
                    # endregion Healty
                }
                else if ((int)TypeComboBox.SelectedValue == (int)DB.MalfuctionStatus.Malfuction)
                {
                    # region Malfuction
                    if (MalfunctionTypeComboBox.SelectedValue == null)
                        throw new Exception("لطفا علت خرابی را انتخاب کنید");

                    Malfuction = this.DataContext as Malfuction;

                    switch (_malfactionType)
                    {

                        case (byte)DB.MalfuctionType.CabinetInput:
                            {
                                if (_cabinetInput.Status == (byte)DB.CabinetInputStatus.Malfuction) { MessageBox.Show("مرکزی هم اکنون در وضعیت خراب قرار دارد"); return; }

                                _cabinetInput.Status = (byte)DB.CabinetInputStatus.Malfuction;
                                Malfuction.CabinetInputID = _cabinetInput.ID;

                                ActionLog actionLog = new ActionLog();
                                actionLog.ActionID = (byte)DB.ActionLog.CablePaired;
                                actionLog.Date = DB.GetServerDate();
                                actionLog.UserName = Folder.User.Current.Username;

                                CRM.Data.Schema.CablePairedMalFuction cablePairedMalFuction = new Data.Schema.CablePairedMalFuction();

                                Center center = Data.CenterDB.GetCenterByCabinetInputID(_cabinetInput.ID);
                                cablePairedMalFuction.CenterID = center.ID;
                                cablePairedMalFuction.CenterName = center.CenterName;
                                cablePairedMalFuction.Date = DB.GetServerDate();
                                cablePairedMalFuction.Description = Malfuction.Description;
                                cablePairedMalFuction.Status = (byte)DB.CabinetInputStatus.Malfuction;
                                cablePairedMalFuction.CabinetInputID = _cabinetInput.ID;
                                actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.CablePairedMalFuction>(cablePairedMalFuction, true));
                                actionLog.Detach();
                                DB.Save(actionLog, true);
                                break;
                            }
                        case (byte)DB.MalfuctionType.PCM:
                            {
                                if (!(_PCM.Status == (byte)DB.PCMStatus.Connection || _PCM.Status == (byte)DB.PCMStatus.Install)) { MessageBox.Show("پی سی ام هم اکنون در وضعیت دایر یا نصب قرار ندارد"); return; }
                                _PCM.Status = (byte)DB.PCMStatus.Destruction;
                                Malfuction.PCMID = _PCM.ID;
                                break;
                            }
                        case (byte)DB.MalfuctionType.PCMPort:
                            {
                                if (_pCMPort.Status == (byte)DB.PCMPortStatus.Malfaction) { MessageBox.Show("پورت هم اکنون در وضعیت خراب قرار دارد"); return; }
                                _pCMPort.Status = (byte)DB.PCMPortStatus.Malfaction;
                                Malfuction.PCMPortID = _pCMPort.ID;
                                break;
                            }
                        case (byte)DB.MalfuctionType.PostConntact:
                            {
                                if (_postContact.Status == (byte)DB.PostContactStatus.PermanentBroken) { MessageBox.Show("اتصالی پست هم اکنون در وضعیت خراب قرار دارد"); return; }
                                if (_postContact.Status == (byte)DB.PostContactStatus.CableConnection || _postContact.Status == (byte)DB.PostContactStatus.Free)
                                {
                                    _postContact.Status = (byte)DB.PostContactStatus.PermanentBroken;
                                    Malfuction.PostContactID = _postContact.ID;

                                    ActionLog actionLog = new ActionLog();
                                    actionLog.ActionID = (byte)DB.ActionLog.PostContact;
                                    actionLog.Date = DB.GetServerDate();
                                    actionLog.UserName = Folder.User.Current.Username;


                                    CRM.Data.Schema.PostContactMalfuction postContactMalFuction = new Data.Schema.PostContactMalfuction();
                                    Center center = Data.CenterDB.GetCenterByPostID(_postContact.PostID);
                                    postContactMalFuction.CenterID = center.ID;
                                    postContactMalFuction.CenterName = center.CenterName;
                                    postContactMalFuction.PostContactID = _postContact.ID;
                                    postContactMalFuction.Date = DB.GetServerDate();
                                    postContactMalFuction.Description = Malfuction.Description;
                                    postContactMalFuction.Status = (byte)DB.PostContactStatus.PermanentBroken;

                                    actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.PostContactMalfuction>(postContactMalFuction, true));
                                    actionLog.Detach();
                                    DB.Save(actionLog, true);
                                }
                                else
                                {
                                    MessageBox.Show("اتصالی پست هم اکنون در وضعیت متصل یا آزاد قرار ندارد");
                                    return;
                                }

                                break;
                            }

                        default:
                            break;
                    }

                    Malfuction.MalfuctionOrhealthy = (byte)DB.MalfuctionStatus.Malfuction;
                    Malfuction.LicenseNumber = LicenseUserControl.LisenseNumber;
                    Malfuction.LicenseFile = (System.Data.Linq.Binary)LicenseUserControl.lisenseFile;
                    using (TransactionScope scope = new TransactionScope())
                    {

                        switch (_malfactionType)
                        {

                            case (byte)DB.MalfuctionType.CabinetInput:
                                {
                                    _cabinetInput.Detach();
                                    DB.Save(_cabinetInput, false);
                                    break;
                                }
                            case (byte)DB.MalfuctionType.PCMPort:
                                {
                                    _pCMPort.Detach();
                                    DB.Save(_pCMPort, false);

                                    //PCMPortInfo portInfo = Data.PCMPortDB.GetPCMPortInfoByID(_pCMPort.ID);

                                    //actionLogPortMalfaction.PortID = portInfo.ID;
                                    //actionLogPortMalfaction.Port = portInfo.PortNumber.ToString();
                                    //actionLogPortMalfaction.Card = portInfo.PCM.ToString();
                                    //actionLogPortMalfaction.Shelf = portInfo.Shelf.ToString();
                                    //actionLogPortMalfaction.Rock = portInfo.Rock.ToString();
                                    //actionLogPortMalfaction.CenterID = portInfo.CenterID;
                                    //actionLogPortMalfaction.Center = portInfo.Center;
                                    //actionLogPortMalfaction.PCMBrand = portInfo.PCMBrand;
                                    //actionLogPortMalfaction.PCMType = portInfo.PCMType;
                                    //actionLogPortMalfaction.TypeMalfaction = portInfo.PCMType;
                                    //actionLogPortMalfaction.Description = portInfo.PCMType;
                                    //actionLogPortMalfaction.Date = DB.GetServerDate();

                                    break;
                                }
                            case (byte)DB.MalfuctionType.PostConntact:
                                {
                                    _postContact.Detach();
                                    DB.Save(_postContact, false);
                                    break;
                                }
                            case (byte)DB.MalfuctionType.PCM:
                                {
                                    _PCM.Detach();
                                    DB.Save(_PCM, false);
                                    break;
                                }

                            default:
                                throw new Exception("خطا در ذخیره اطلاعات");


                        }

                        Malfuction.Detach();
                        DB.Save(Malfuction, true);

                        scope.Complete();

                    }

                    # endregion Malfuction
                }

                ShowSuccessMessage("ذخیره وضعیت انجام شد");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("ذخیره وضعیت با خطا مواجه شد", ex);
            }
        }

        #endregion
    }
}
