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
    public partial class VerticalMDFColumnForm : Local.PopupWindow
    {
        private int _verticalMDFColumnID = 0;
        private VerticalMDFColumn verticalMDFColumn;
        private static bool _multiInsert = false;

        private int _rowInsert = 0;

        public VerticalMDFColumnForm()
        {
            InitializeComponent();

        }

        public VerticalMDFColumnForm(int id, bool multiInsert)
            : this()
        {
            _multiInsert = multiInsert;
            _verticalMDFColumnID = id;
            Initialize();
        }

        public VerticalMDFColumnForm(int id, int rowInsert)
            : this()
        {
            _rowInsert = rowInsert;
            _verticalMDFColumnID = id;
            Initialize();
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {

            verticalMDFColumn = Data.VerticalMDFColumnDB.GetVerticalMDFColumnByID(_verticalMDFColumnID);
        }

        private void LoadData()
        {
            if (_rowInsert == 0)
            {
                if (_multiInsert)
                {
                    VerticalCloumnLabel.Visibility = Visibility.Collapsed;
                    VerticalCloumnTextBox.Visibility = Visibility.Collapsed;


                    FromVerticalCloumnNoLabel.Visibility = Visibility.Visible;
                    FromVerticalCloumnNo.Visibility = Visibility.Visible;
                    ToVerticalCloumnNoLabel.Visibility = Visibility.Visible;
                    ToVerticalCloumnNo.Visibility = Visibility.Visible;
                    NumberVerticalCloumnLabel.Visibility = Visibility.Visible;
                    NumberVerticalCloumnTextBox.Visibility = Visibility.Visible;
                }
                else
                {
                    VerticalCloumnLabel.Visibility = Visibility.Visible;
                    VerticalCloumnTextBox.Visibility = Visibility.Visible;

                    FromVerticalCloumnNoLabel.Visibility = Visibility.Collapsed;
                    FromVerticalCloumnNo.Visibility = Visibility.Collapsed;
                    ToVerticalCloumnNoLabel.Visibility = Visibility.Collapsed;
                    ToVerticalCloumnNo.Visibility = Visibility.Collapsed;
                    NumberVerticalCloumnLabel.Visibility = Visibility.Collapsed;
                    NumberVerticalCloumnTextBox.Visibility = Visibility.Collapsed;

                    this.DataContext = verticalMDFColumn;
                }
            }
            else
            {
                VerticalCloumnLabel.Visibility = Visibility.Collapsed;
                VerticalCloumnTextBox.Visibility = Visibility.Collapsed;

                FromVerticalCloumnNoLabel.Visibility = Visibility.Collapsed;
                FromVerticalCloumnNo.Visibility = Visibility.Collapsed;

                ToVerticalCloumnNoLabel.Visibility = Visibility.Collapsed;
                ToVerticalCloumnNo.Visibility = Visibility.Collapsed;

                NumberVerticalCloumnLabel.Content = "شماره طبقه";
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
                if (_rowInsert == 0)
                {
                    if (_multiInsert)
                    {
                        int fromVerticalColumnNo = Convert.ToInt32(FromVerticalCloumnNo.Text.Trim());
                        int toVerticalColumnNo = Convert.ToInt32(ToVerticalCloumnNo.Text.Trim());
                        int numberVerticalCloumn = Convert.ToInt32(NumberVerticalCloumnTextBox.Text.Trim());

                        List<VerticalMDFRow> VerticalMDFRows = new List<VerticalMDFRow>();
                        for (int i = fromVerticalColumnNo; i <= toVerticalColumnNo; i++)
                        {
                            VerticalMDFColumn verticalMDFColumnTemp = new VerticalMDFColumn();
                            verticalMDFColumnTemp.MDFFrameID = verticalMDFColumn.MDFFrameID;
                            verticalMDFColumnTemp.VerticalCloumnNo = i;
                            DB.Save(verticalMDFColumnTemp);

                            for (int j = 1; j <= numberVerticalCloumn; j++)
                            {
                                VerticalMDFRow verticalMDFRowTemp = new VerticalMDFRow();
                                verticalMDFRowTemp.VerticalMDFColumnID = verticalMDFColumnTemp.ID;
                                verticalMDFRowTemp.VerticalRowNo = j;
                                VerticalMDFRows.Add(verticalMDFRowTemp);

                            }
                            DB.SaveAll(VerticalMDFRows);
                        }
                    }
                    //milad doran
                    //else
                    //{

                    //    Data.VerticalMDFColumn newVerticalMDFColumn = this.DataContext as Data.VerticalMDFColumn;
                    //    newVerticalMDFColumn.Detach();
                    //    DB.Save(newVerticalMDFColumn);
                    //}
                    //TODO:rad 13950331
                    else
                    {
                        try
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                Data.VerticalMDFColumn verticalMDFColumn = this.DataContext as Data.VerticalMDFColumn;

                                //چنانچه طبقه مربوط به ردیف انتخاب شده دارای بوخت باشند باید ستون شماره ردیف در جدول بوخت که برای افزایش سرعت کوئری اضافه شده است ، بروزرسانی شود
                                //لذا کلیه ی بوخت های ردیف انتخاب شده را در صورت وجود باید فراخوانی کنیم

                                List<Bucht> buchtsOfVerticalMdfRowsOfSelectedVerticalMDFColumn = VerticalMDFColumnDB.GetAllBuchtByColumnID(verticalMDFColumn.ID);
                                if (buchtsOfVerticalMdfRowsOfSelectedVerticalMDFColumn.Count > 0)
                                {
                                    buchtsOfVerticalMdfRowsOfSelectedVerticalMDFColumn.ForEach((bu) =>
                                    {
                                        bu.ColumnNo = verticalMDFColumn.VerticalCloumnNo;
                                        bu.Detach();
                                    }
                                                                                   );
                                    DB.UpdateAll<Bucht>(buchtsOfVerticalMdfRowsOfSelectedVerticalMDFColumn);
                                }

                                verticalMDFColumn.Detach();
                                DB.Save(verticalMDFColumn);
                                scope.Complete();
                            }
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    VerticalMDFRow verticalMDFRowTemp = new VerticalMDFRow();
                    verticalMDFRowTemp.VerticalMDFColumnID = _verticalMDFColumnID;
                    verticalMDFRowTemp.VerticalRowNo = Convert.ToInt32(NumberVerticalCloumnTextBox.Text.Trim());
                    DB.Save(verticalMDFRowTemp, true);
                }
                ShowSuccessMessage("ذخیره انجام شد");
                this.DialogResult = true;

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key in object"))
                {
                    ShowErrorMessage("نمی توان دو ردیف هم شماره وارد کرد .خطا در ذخیره بوخت", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در ذخیره بوخت", ex);
                }
            }
        }
    }
}
