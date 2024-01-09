using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PCMRemoveList.xaml
    /// </summary>
    public partial class PCMRemoveList : Local.TabWindow
    {
        #region Constructor
        private BackgroundWorker backgroundWorker;
        List<PCMCardInfo> PCMCardInfos;
        private Data.Schema.PCMDrop actionLogPCMRemove = new Data.Schema.PCMDrop();
        #endregion

        public PCMRemoveList()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();

            //PCMBrandColumn.ItemsSource = Data.PCMBrandDB.GetPCMBrandCheckable();
            //PCMTypeColumn.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();
            StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.PCMStatus));
            //CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();
        }

          #region Worker

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            RockCheckableComboBox.ItemsSource = Data.PCMRockDB.GetPCMRockCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void RockCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            ShelfCheckableComboBox.ItemsSource = Data.PCMShelfDB.GetCheckableItemPCMShelfByRockIDs(RockCheckableComboBox.SelectedIDs);
        }

        private void ShelfCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CardCheckableComboBox.ItemsSource = Data.PCMDB.GetCheckableItemPCMCardInfoByShelfID(ShelfCheckableComboBox.SelectedIDs);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);

        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;


            Pager.TotalRecords = Data.PCMRemoveFormDB.SearchCount(
                    CenterComboBox.SelectedIDs,
                    CityComboBox.SelectedIDs,
                    RockCheckableComboBox.SelectedIDs,
                    ShelfCheckableComboBox.SelectedIDs,
                    CardCheckableComboBox.SelectedIDs);

            PCMCardInfos = Data.PCMRemoveFormDB.Search(
                                CenterComboBox.SelectedIDs,
                                CityComboBox.SelectedIDs,
                                RockCheckableComboBox.SelectedIDs,
                                ShelfCheckableComboBox.SelectedIDs,
                                CardCheckableComboBox.SelectedIDs   ,
                                startRowIndex,
                                pageSize);


            ItemsDataGrid.ItemsSource = PCMCardInfos;

        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {

            // first, Transfer Customer to Post connection 
            // then, buchts and ports are released.
            // then, status Card(PCM) is installed
            // type PostContact is AppointedByTheCentral and More PostContact will be delete


            try
            {

                    List<PCMCardInfo> PCMCardInfos = ItemsDataGrid.ItemsSource as List<PCMCardInfo>;

                    if (PCMCardInfos.Where(t => t.IsChecked == true).ToList().Any(t => t.CustomerOfPCM.Where(t2 => t2.Telephone != null).Count() > 1))
                        throw new Exception("پی سی ام با یک مشترک متصل قابلیت جمع آوری را دارد");

                    if (PCMCardInfos.All(t => t.IsChecked == false))
                        throw new Exception("لطفا پی سی ام را انتخاب کنید");

                        PCMCardInfos.Where(t => t.IsChecked == true).ToList().ForEach((PCMCardInfo item) =>
                        {
                            List<PostContactBuchtPortInfo> PostContactBuchtPortInfo = Data.PCMDB.GetAllPostContactByPCMID(item.ID).ToList();
                            List<PostContact> PostContacts = PostContactBuchtPortInfo.Select(t => t.PostContact).ToList();

                            List<Bucht> Buchts = PostContactBuchtPortInfo.Select(t => t.Bucht).ToList();
                            List<PCMPort> PCMPorts = PostContactBuchtPortInfo.Select(t => t.PCMPort).ToList();
                            PCM PCM = PostContactBuchtPortInfo.Take(1).Select(t => t.PCM).SingleOrDefault();
                            if (PostContacts.Where(t => t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal && t.Status != (byte)DB.PostContactStatus.Free && t.Status != (byte)DB.PostContactStatus.PermanentBroken).Count() > 1)
                            {
                                MessageBox.Show("کارت " + item.Card + " بیش از یک پورت پر دارد،امکان جمع آوری ندارد.", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {


                                using (TransactionScope ts = new TransactionScope())
                                {

                                    PostContact PostContactContect = PostContacts.Where(t => t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal && t.Status == (byte)DB.PostContactStatus.CableConnection).SingleOrDefault();

                                    Bucht buchtConectedToInput = Data.BuchtDB.GetBuchtByID((long)Buchts.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().BuchtIDConnectedOtherBucht);
                                    buchtConectedToInput.SwitchPortID = Buchts.Where(t => t.ConnectionID == PostContactContect.ID).SingleOrDefault().SwitchPortID;
                                    buchtConectedToInput.Status = (byte)DB.BuchtStatus.Connection;
                                    buchtConectedToInput.PortNo = null;
                                    buchtConectedToInput.PCMPortID = null;
                                    buchtConectedToInput.BuchtIDConnectedOtherBucht = null;
                                    buchtConectedToInput.ConnectionID = PostContactContect.ID;
                                    buchtConectedToInput.Detach();

                                    Buchts.ForEach((Bucht buchtItem) =>
                                    {
                                        buchtItem.ConnectionID = null;
                                        buchtItem.CabinetInputID = null;
                                        buchtItem.BuchtIDConnectedOtherBucht = null;
                                        buchtItem.Status = (byte)DB.BuchtStatus.ConnectedToPCM;
                                        buchtItem.SwitchPortID = null;
                                        buchtItem.Detach();
                                    });

                                    DB.UpdateAll(Buchts);
                                    DB.Save(buchtConectedToInput, false);

                                    PCMPorts = PCMPorts.Where(t => t.Status == (Byte)DB.PCMPortStatus.Connection).ToList();
                                    PCMPorts.ForEach((PCMPort portItem) => { portItem.Status = (Byte)DB.PCMPortStatus.Empty; portItem.Detach(); });
                                    DB.UpdateAll(PCMPorts);

                                    PCM.Status = (byte)DB.PCMStatus.Install;
                                    PCM.InstallAddress = null;
                                    PCM.InstallPostCode = null;
                                    PCM.Detach();
                                    DB.Save(PCM);

                                    List<PostContact> PostContactsOther = PostContacts.Where(t => t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote || t.Status == (byte)DB.PostContactStatus.Free).ToList();

                                    PostContactContect.ConnectionType = (byte)DB.PostContactConnectionType.Noraml;
                                    PostContactContect.Detach();
                                    DB.Save(PostContactContect);

                                    PostContactsOther.ForEach(t => { t.Status = (int)DB.PostContactStatus.Deleted; t.Detach(); });
                                    DB.UpdateAll(PostContactsOther);

                                  //  DB.DeleteAll<PostContact>(PostContactsOther.Select(t => t.ID).ToList());


                                    actionLogPCMRemove.CenterID = item.CenterID;
                                    actionLogPCMRemove.Center = item.CenterName;
                                    actionLogPCMRemove.Rock = item.RockNumber.ToString();
                                    actionLogPCMRemove.Shelf = item.ShelfNumber.ToString();
                                    actionLogPCMRemove.Card = item.Card.ToString();
                                    actionLogPCMRemove.Type = item.PCMBrandName + " " + item.PCMTypeName;
                                    actionLogPCMRemove.PostContact = item.ConnectionNo.ToString();

                                    ActionLog actionLog = new ActionLog();
                                    actionLog.ActionID = (byte)DB.ActionLog.PCMDrop;
                                    actionLog.UserName = Folder.User.Current.Username;
                                    actionLogPCMRemove.Date = actionLog.Date = DB.GetServerDate();
                                    actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.PCMDrop>(actionLogPCMRemove, true));
                                    actionLog.Detach();
                                    DB.Save(actionLog);
                                    ts.Complete();
                                }
                            }


                        });

                    Search(null, null);


                    MessageBox.Show("جمع آوری انجام شد");


            }
            catch (Exception ex)
            {

                ShowErrorMessage("خطا در انجام عملیات", ex);

            }

        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //  Search(null, null);
        }
        #endregion

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }




    }
}
