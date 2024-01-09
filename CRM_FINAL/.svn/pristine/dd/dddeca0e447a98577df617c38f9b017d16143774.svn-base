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
using System.Data.OleDb;
using System.Data;

namespace CRM.Application.Views
{
    public partial class ADSLModemPropertyFileForm : Local.PopupWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public ADSLModemPropertyFileForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
            ModemModelComboBox.ItemsSource = ADSLModemDB.GetModemMOdelsCheckable();
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void DownloadFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Microsoft.Win32.SaveFileDialog sa = new Microsoft.Win32.SaveFileDialog();
                sa.Filter = "xls|Excel";
                if (sa.ShowDialog() == true)
                {
                    string connectionString =
                    string.Format(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=Excel 8.0;", sa.FileName);

                    using (OleDbConnection Connection =
                        new OleDbConnection(connectionString))
                    {
                        Connection.Open();

                        using (OleDbCommand command =
                        new OleDbCommand())
                        {
                            command.Connection = Connection;

                            command.CommandText = @"CREATE TABLE 
                                    [Modem](SerialNo text(200))";

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void importFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CenterComboBox.SelectedValue == null)
                    throw new Exception("لطفا مرکز مورد نظر را انتخاب نمایید");

                if (ModemModelComboBox.SelectedValue == null)
                    throw new Exception("لطفا مدل مودم مورد نظر را انتخاب نمایید");

                Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
                if (op.ShowDialog() != true)
                    return;

                string connectionString = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=Excel 8.0;", op.FileName);

                using (OleDbConnection Connection =
                    new OleDbConnection(connectionString))
                {
                    Connection.Open();

                    using (OleDbCommand command =
                    new OleDbCommand())
                    {
                        command.Connection = Connection;

                        DataTable dt = null;

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                        {
                            command.CommandText = "SELECT * FROM [Modem$]";
                            dt = new DataTable();
                            adapter.SelectCommand = command;
                            adapter.Fill(dt);

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                try
                                {
                                    string serial = dt.Rows[i]["SerialNo"].ToString();

                                    ADSLModemProperty modem = new ADSLModemProperty();
                                    modem.SerialNo = serial;
                                    modem.CenterID = (int)CenterComboBox.SelectedValue;
                                    modem.ADSLModemID = (int)ModemModelComboBox.SelectedValue;
                                    modem.TelephoneNo = null;
                                    modem.MACAddress = null;
                                    modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                                    modem.Detach();
                                    DB.Save(modem, true);
                                }
                                catch
                                {
                                    continue;
                                }
                            }

                            System.Windows.MessageBox.Show(string.Format("{0} Records Inserted Successfully", dt.Rows.Count));
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        #endregion
    }
}
