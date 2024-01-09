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
    /// <summary>
    /// Interaction logic for ADSLAssignmentBuchtToPort.xaml
    /// </summary>
    public partial class ADSLAssignmentBuchtToPort : Local.PopupWindow
    {

        #region Properties

        private long PortID;
        ADSLPort aDSLPort = new ADSLPort();
        Bucht OldbuchtInput = new Bucht();
        Bucht OldbuchtOutput = new Bucht();

        #endregion

        #region constructor

        public ADSLAssignmentBuchtToPort()
        {
            InitializeComponent();
        }

        public ADSLAssignmentBuchtToPort(long portID)
            : this()
        {
            this.PortID = portID;
        }

        #endregion

        #region Methods

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            aDSLPort = DB.SearchByPropertyName<ADSLPort>("ID", this.PortID).SingleOrDefault();
            InputColumnRowConnection.BuchtType = (int)DB.BuchtType.ADSL;
            OutColumnRowConnection.BuchtType = (int)DB.BuchtType.ADSL;
            
            if (aDSLPort != null)
                PortGrid.DataContext = Data.ADSLPortDB.GetADSlPortFullInfo(aDSLPort.ID);

            if (aDSLPort != null && aDSLPort.InputBucht != null)
            {
                OldbuchtInput = Data.BuchtDB.GetBuchtByID((long)aDSLPort.InputBucht);
                if (OldbuchtInput != null)
                {
                    InputColumnRowConnection.BuchtID = OldbuchtInput.ID;
                }
            }

            if (aDSLPort != null && aDSLPort.OutBucht != null)
            {
                OldbuchtOutput = Data.BuchtDB.GetBuchtByID((long)aDSLPort.OutBucht);
                if (OldbuchtOutput != null)
                {
                    OutColumnRowConnection.BuchtID = OldbuchtOutput.ID;
                }
            }
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                Bucht buchtInput = Data.BuchtDB.GetBuchtByID((long)InputColumnRowConnection.BuchtID);
                Bucht buchtOutput = Data.BuchtDB.GetBuchtByID((long)OutColumnRowConnection.BuchtID);
                if (buchtInput.ID == buchtOutput.ID) { MessageBox.Show("بوخت ورودی و بوخت خروجی برابر می باشند !", "", MessageBoxButton.OK, MessageBoxImage.Error); return; }

                using (TransactionScope ts = new TransactionScope())
                {
                    if (OldbuchtInput != null)
                    {
                        // بوخت ورودی جدید را مقدار دهی میکند
                        buchtInput.SwitchPortID = OldbuchtInput.SwitchPortID;
                        buchtInput.ADSLStatus = OldbuchtInput.ADSLStatus;
                        buchtInput.Status = OldbuchtInput.Status;
                     //   buchtInput.ADSLPortID = OldbuchtInput.ADSLPortID;
                      //  buchtInput.ADSLType = OldbuchtInput.ADSLType;
                        buchtInput.Detach();
                        DB.Save(buchtInput);

                        // بوخت ورودی قبلی را پاک میکند
                        OldbuchtInput.SwitchPortID = null;
                        OldbuchtInput.ADSLStatus = false;
                        OldbuchtInput.Status = (byte)DB.BuchtStatus.ADSLFree;
                    //  OldbuchtInput.ADSLPortID = null;
                    //  OldbuchtInput.ADSLType = null;
                        OldbuchtInput.Detach();
                        DB.Save(OldbuchtInput);
                    }

                    else
                    {
                     //   buchtInput.ADSLPortID = aDSLPort.ID;
                      //  buchtInput.ADSLType = (byte)DB.ADSLPortType.Input;
                        buchtInput.Status = (byte)DB.BuchtStatus.ConnectedToSpliter;
                        buchtInput.Detach();
                        DB.Save(buchtInput);
                    }

                    if (OldbuchtOutput != null)
                    {
                        //بوخت خروجی جدید را مقدار دهی میکند
                        buchtOutput.SwitchPortID = OldbuchtOutput.SwitchPortID;
                        buchtOutput.BuchtIDConnectedOtherBucht = OldbuchtOutput.BuchtIDConnectedOtherBucht;
                        buchtOutput.Status = OldbuchtOutput.Status;
                       // buchtOutput.ADSLPortID = OldbuchtOutput.ADSLPortID;
                       // buchtOutput.ADSLType = OldbuchtOutput.ADSLType;
                        buchtOutput.Detach();
                        DB.Save(buchtOutput);

                        // اگر پورت متصل به مشترک باشد اطلالاعات بوخت مشترک را با تغییر بوخت عوض میکند
                        if (buchtOutput.BuchtIDConnectedOtherBucht != null)
                        {
                            Bucht buchtCustomer = Data.BuchtDB.GetBuchtByID((long)buchtOutput.BuchtIDConnectedOtherBucht);
                            buchtCustomer.BuchtIDConnectedOtherBucht = buchtOutput.ID;
                            buchtCustomer.Detach();
                            DB.Save(buchtCustomer);
                        }

                        //بوخت خروجی قبلی را پاک میکند
                        OldbuchtOutput.BuchtIDConnectedOtherBucht = null;
                        OldbuchtOutput.ADSLStatus = false;
                        OldbuchtOutput.Status = (byte)DB.BuchtStatus.ADSLFree;
                      //  OldbuchtOutput.ADSLPortID = null;
                     //   OldbuchtOutput.ADSLType = null;
                        OldbuchtOutput.Detach();
                        DB.Save(OldbuchtOutput);
                    }

                    else
                    {
                      //  buchtOutput.ADSLPortID = aDSLPort.ID;
                      //  buchtOutput.ADSLType = (byte)DB.ADSLPortType.OutPut;
                        buchtOutput.Status = (byte)DB.BuchtStatus.ConnectedToSpliter;
                        buchtOutput.Detach();
                        DB.Save(buchtOutput);
                    }

                    // مشخصات پورت را تنظیم میکند
                    aDSLPort.PortNo = (PortGrid.DataContext as ADSLPortsInfo).PortNo;
                    aDSLPort.Address = (PortGrid.DataContext as ADSLPortsInfo).Address;

                    aDSLPort.InputBucht = buchtInput.ID;
                    aDSLPort.OutBucht = buchtOutput.ID;

                    aDSLPort.Detach();
                    DB.Save(aDSLPort);

                    ts.Complete();
                }

                ShowSuccessMessage("انتساب انجام شد");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در انتساب", ex);
            }
        }
        #endregion
    }
}
