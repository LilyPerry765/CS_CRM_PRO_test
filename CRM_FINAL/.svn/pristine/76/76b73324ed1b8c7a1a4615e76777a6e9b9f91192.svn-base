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

namespace CRM.Application.Views
{    
    public partial class BuchtForm : Local.PopupWindow
    {
        int _frameID = 0;
        
        public BuchtForm()
        {
            InitializeComponent();
        }
        
        public BuchtForm(int frameID):this()
        {
            _frameID = frameID;
        }
        
        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                int verticalFirstColumn = Convert.ToInt32(VerticalFirstColumn.Text.Trim());
                int verticalLastColumn = Convert.ToInt32(VerticalLastColumn.Text.Trim());
                int verticalRowsCount = Convert.ToInt32(VerticalRowsCount.Text.Trim());
                int verticalRowCapacity = Convert.ToInt32(VerticalRowCapacity.Text.Trim());
                Save(verticalFirstColumn, verticalLastColumn, verticalRowsCount, verticalRowCapacity);
                ShowSuccessMessage("ایجاد بوخت انجام شد");
                
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره بوخت", ex);
            }

            
               
          
          
        }

        private void Save(int verticalFirstColumn, int verticalLastColumn, int verticalRowsCount , int verticalRowCapacity)
        {
            try
            {
                int buchtType;
                MDF mDF = Data.MDFDB.GetMDFByFrameID(_frameID);
                MDFFrame frame = Data.MDFFrameDB.GetMDFFrameByID(_frameID);
                Center center = CenterDB.GetCenterByCenterID(mDF.CenterID);
                City city = CityDB.GetCityByCenterID(center.ID);
                
                if (BuchtTypeComboBox.SelectedValue != null)
                {
                    buchtType = (int)BuchtTypeComboBox.SelectedValue;
                }
                else
                {
                    throw new Exception("نوع بوخت انتخاب نشده است");
                }

                for (int column = verticalFirstColumn; column <= verticalLastColumn; column++)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        VerticalMDFColumn verticalMDFColumn = new Data.VerticalMDFColumn();
                        verticalMDFColumn.VerticalCloumnNo = column;
                        verticalMDFColumn.MDFFrameID = _frameID;
                        verticalMDFColumn.Detach();
                        DB.Save(verticalMDFColumn);

                        for (int row = 1; row <= verticalRowsCount; row++)
                        {
                            VerticalMDFRow verticalMDFRow = new VerticalMDFRow();
                            verticalMDFRow.VerticalMDFColumnID = verticalMDFColumn.ID;
                            verticalMDFRow.VerticalRowNo = row;
                            verticalMDFRow.RowCapacity = verticalRowCapacity;
                            verticalMDFRow.Detach();
                            DB.Save(verticalMDFRow);

                            List<Bucht> buchts = new List<Bucht>();
                            buchts.Clear();

                            for (int Capacity = 1; Capacity <= verticalRowCapacity; Capacity++)
                            {
                                Bucht bucht = new Bucht();
                                bucht.BuchtTypeID = buchtType;
                                bucht.BuchtNo = Capacity;
                                bucht.MDFRowID = verticalMDFRow.ID;

                                //ستون های زیر به منظور افزایش سرعت کوئری ها اضافه شده است
                                bucht.CityID = city.ID;
                                bucht.CenterID = center.ID;
                                bucht.Center = center.CenterName;
                                bucht.MDFID = mDF.ID;
                                bucht.MDF = mDF.Number.ToString() + '(' + mDF.Description + ')';
                                bucht.FrameID = frame.ID;
                                bucht.Frame = frame.FrameNo;
                                bucht.ColumnNo = verticalMDFColumn.VerticalCloumnNo;
                                bucht.RowNo = verticalMDFRow.VerticalRowNo;
                                //end

                                bucht.Status = (int)DB.BuchtStatus.Free;
                                buchts.Add(bucht);                                
                            }

                            DB.SaveAll(buchts);

                            if (buchtType == (byte)DB.BuchtType.ADSL)
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
                        }

                        scope.Complete();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void PopupWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            int? centerID = Data.MDFFrameDB.GetMDFByFramID(_frameID).CenterID;
            if (centerID != null)
            BuchtTypeComboBox.ItemsSource = Data.BuchtTypeDB.GetBuchtTypeCheckable(centerID ?? 0);
        }
    }
}
