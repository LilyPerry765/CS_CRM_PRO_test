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
using System.IO;
using Microsoft.Win32;
using System.Data;
using System.Data.OleDb;

namespace CRM.Application.Views
{
    public partial class Failure117UBForm : Local.PopupWindow
    {
        #region Properties

        private DataSet _TelephoneList { get; set; }

        #endregion

        #region Constructors

        public Failure117UBForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        private void LoadData()
        {
        }

        public DataSet ExcelFile_To_Dataset(string FileName, bool hasHeaders, DataSet ds = null)
        {
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

            DataSet output = new DataSet();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                int counter = 0;
                foreach (DataRow schemaRow in schemaTable.Rows)
                {
                    string sheet = schemaRow["TABLE_NAME"].ToString();
                    if (!sheet.EndsWith("_"))
                    {
                        try
                        {
                            DataTable outputTable = new DataTable();
                            if (ds != null)
                            {
                                if (ds.Tables.Count > counter)
                                    outputTable = ds.Tables[counter].Copy();
                                counter++;
                            }
                            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                            cmd.CommandType = CommandType.Text;
                            outputTable.TableName = sheet;
                            output.Tables.Add(outputTable);
                            new OleDbDataAdapter(cmd).Fill(outputTable);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, FileName), ex);
                        }
                    }
                }
            }
            return output;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InsertDate.SelectedDate == null)
                    throw new Exception("لطفا تاریخ مورد نظر را انتخاب نمایید");

                DataTable telephoneTable = _TelephoneList.Tables[0];
                int countRow = telephoneTable.Rows.Count;
                Failure117UB ub = new Failure117UB();

                for (int i = 0; i < countRow; i++)
                {
                    ub = new Failure117UB();
                    ub.TelephoneNo = Convert.ToInt64(telephoneTable.Rows[i].ItemArray[0].ToString());

                    int centerID = 0;
                    if (TelephoneDB.HasTelephoneNo(ub.TelephoneNo))
                        centerID = CenterDB.GetCenterIDbyTelephoneNo(ub.TelephoneNo);
                    else
                    {
                        if (!TelephoneDB.HasTelephoneTemp(ub.TelephoneNo))
                            centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(ub.TelephoneNo);
                        else
                            throw new Exception("شماره تلفن " + ub.TelephoneNo + " در سیتم اطلاعات جامع موجود نمی باشد");
                    }

                    ub.CenterID = centerID;
                    ub.UBDate = (DateTime)InsertDate.SelectedDate;
                    ub.UserID = DB.CurrentUser.ID;
                    ub.InsertDate = DB.GetServerDate();

                    ub.Detach();
                    DB.Save(ub);
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خواندن اطلاعات از فایل با خطا مواجه شد" + ex.Message + " !", ex);
            }
        }

        private void EnterDataClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                _TelephoneList = ExcelFile_To_Dataset(dlg.FileName, true/*, ds*/);
            }
        }

        #endregion
    }
}
