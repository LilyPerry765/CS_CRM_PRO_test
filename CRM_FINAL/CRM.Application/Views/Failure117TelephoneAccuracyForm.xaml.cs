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
using System.Data;
using System.Data.OleDb;
using Microsoft.Win32;

namespace CRM.Application.Views
{
    public partial class Failure117TelephoneAccuracyForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int CityID = 0;
        private DataSet _TelephoneList { get; set; }

        #endregion

        #region Constructors

        public Failure117TelephoneAccuracyForm()
        {
            InitializeComponent();
            Initialize();
        }

        public Failure117TelephoneAccuracyForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        private void LoadData()
        {
            Failure117TelephoneAccuracy telephoneAccuracy = new Failure117TelephoneAccuracy();

            if (_ID == 0)
            {
                TelephoneNoLabel.Visibility = Visibility.Collapsed;
                TelephoneNoTextBox.Visibility = Visibility.Collapsed;
                TelephoneNoRow.Height = GridLength.Auto;

                SaveButton.Content = "ذخیره";
            }
            else
            {
                FromTelephoneNoLabel.Visibility = Visibility.Collapsed;
                FromTelephoneNoTextBox.Visibility = Visibility.Collapsed;
                ToTelephoneNoLabel.Visibility = Visibility.Collapsed;
                ToTelephoneNoTextBox.Visibility = Visibility.Collapsed;
                FromTelephoneNoRow.Height = GridLength.Auto;
                ToTelephoneNoRow.Height = GridLength.Auto;

                telephoneAccuracy = Data.Failure117CabenitAccuracyDB.GetTelephoneAccuracyById(_ID);
                CityID = Data.Failure117CabenitAccuracyDB.GetCityForTelephone(telephoneAccuracy.ID);

                if (telephoneAccuracy.CorrectDate != null)
                {
                    CityComboBox.IsEnabled = false;
                    CenterComboBox.IsEnabled = false;
                    TelephoneNoTextBox.IsReadOnly = true;
                    SaveButton.IsEnabled = false;
                }

                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = telephoneAccuracy;

            if (CityID == 0)
                CityComboBox.SelectedIndex = 0;
            else
                CityComboBox.SelectedValue = CityID;
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
                Failure117TelephoneAccuracy telephoneAccuracy = this.DataContext as Failure117TelephoneAccuracy;

                long fromTelephoneNo = 0;
                long toTelephoneNo = 0;

                if (_TelephoneList != null)
                {
                    DataTable telephoneTable = _TelephoneList.Tables[0];
                    int countRow = telephoneTable.Rows.Count;
                    Failure117TelephoneAccuracy accuracy = new Failure117TelephoneAccuracy();

                    for (int i = 0; i < countRow; i++)
                    {
                        accuracy = new Failure117TelephoneAccuracy();
                        accuracy.ID = 0;
                        accuracy.TelephoneNo = Convert.ToInt64(telephoneTable.Rows[i].ItemArray[0].ToString());

                        int centerID = 0;
                        if (TelephoneDB.HasTelephoneNo(accuracy.TelephoneNo))
                            centerID = CenterDB.GetCenterIDbyTelephoneNo(accuracy.TelephoneNo);
                        else
                        {
                            if (!TelephoneDB.HasTelephoneTemp(accuracy.TelephoneNo))
                                centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(accuracy.TelephoneNo);
                            else
                                throw new Exception("شماره تلفن " + accuracy.TelephoneNo + " در سیتم اطلاعات جامع موجود نمی باشد");
                        }

                        accuracy.CenterID = centerID;

                        accuracy.Detach();
                        DB.Save(accuracy);
                    }
                }
                else
                {
                    if (_ID == 0)
                    {
                        if (!string.IsNullOrEmpty(FromTelephoneNoTextBox.Text))
                            fromTelephoneNo = Convert.ToInt64(FromTelephoneNoTextBox.Text.Trim());
                        else
                            throw new Exception("لطفا شماره تلفن را وارد نمایید !");

                        if (!string.IsNullOrEmpty(ToTelephoneNoTextBox.Text))
                            toTelephoneNo = Convert.ToInt64(ToTelephoneNoTextBox.Text.Trim());
                        if (toTelephoneNo == 0)
                        {
                            telephoneAccuracy.ID = 0;
                            telephoneAccuracy.TelephoneNo = fromTelephoneNo;

                            if (!Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(telephoneAccuracy.TelephoneNo, telephoneAccuracy.CenterID))
                            {
                                telephoneAccuracy.Detach();
                                Save(telephoneAccuracy);
                            }
                            else
                                throw new Exception("پیش از این خرابی تلفن مورد نظر در این مرکز اعلام شده است");
                        }
                        else
                        {
                            for (long i = fromTelephoneNo; i <= toTelephoneNo; i++)
                            {
                                telephoneAccuracy.ID = 0;
                                telephoneAccuracy.TelephoneNo = i;

                                if (!Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(telephoneAccuracy.TelephoneNo, telephoneAccuracy.CenterID))
                                {
                                    telephoneAccuracy.Detach();
                                    Save(telephoneAccuracy);
                                }
                                else
                                    throw new Exception("پیش از این خرابی تلفن " + telephoneAccuracy.TelephoneNo.ToString() + " در این مرکز اعلام شده است");
                            }
                        }
                    }
                    else
                    {
                        if (!Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(telephoneAccuracy.TelephoneNo, telephoneAccuracy.CenterID))
                        {
                            telephoneAccuracy.Detach();
                            Save(telephoneAccuracy);
                        }
                        else
                            throw new Exception("پیش از این خرابی تلفن مورد نظر در این مرکز اعلام شده است");
                    }
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره تلفن خراب" + " ، " + ex.Message + "!", ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as Failure117TelephoneAccuracy).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
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

        private void TelephoneNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }
        
        private void GetFile(object sender, RoutedEventArgs e)
        {
            TelephoneNoTextBox.Text = string.Empty;
            TelephoneNoTextBox.IsReadOnly = true;
            FromTelephoneNoTextBox.Text = string.Empty;
            FromTelephoneNoTextBox.IsReadOnly = true;
            ToTelephoneNoTextBox.Text = string.Empty;
            ToTelephoneNoTextBox.IsReadOnly = true;
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
