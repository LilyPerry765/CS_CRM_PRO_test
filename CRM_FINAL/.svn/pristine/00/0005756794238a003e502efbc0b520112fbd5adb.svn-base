using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlClient;
using System.Threading;
using System.Data.OleDb;

namespace FoxProToDataSet
{
    public class Convert
    {
        
        DataSet resultDataSet = new DataSet();
        DataTable dataTable;
        public System.Data.DataTable Convert_Tb;
        public System.Data.DataTable Convert_Tb_Grid;
        public Thread TRD;
        public double PB;
        public string strFilename;
        public string city;
        public string main_table = "";
        public string temp_table;
        public string query;
        public string id = "0";
        public string tel_no = "";
        public string name = "";
        public string family = "";
        public string address = "";
        public string ConnectionString = "";
        public string Is_Authentication = "";
        public string start = "";
        public string _TableName = "";
        public string _Filename = "";
        public System.Data.DataTable Structure_Tb;
        public System.DateTime Dnow;

        public delegate void RecordInsert(long current, long total);
        public event RecordInsert RecoredInserted;

        public DataSet StartConvert(string filename, string tableName)
        {
            _Filename = filename;
            _TableName = tableName;
            Convert_Fox_DB(_Filename, _TableName);
            return resultDataSet;
        }
        public void Convert_Fox_DB(string FileName, string TableName)
        {
            try
            {
                System.Data.DataTable Convert_Tb_Temp = new DataTable();
                Convert_Tb_Grid = new DataTable();
                Convert_Tb = new DataTable();
                Structure_Tb = new DataTable(); //فایل هدر فاکس پرو خوانده می شود و ستونها و انواع آنها را مشخص می کند.
                System.Data.DataColumn Data_Column = new DataColumn();
                System.Data.DataRow Dr;
                Structure_Tb.Columns.Add("Field_Name", System.Type.GetType("System.String"));
                Structure_Tb.Columns.Add("Field_Type", System.Type.GetType("System.String"));
                Structure_Tb.Columns.Add("LOF", System.Type.GetType("System.Int64"));
                Structure_Tb.Columns.Add("DOF", System.Type.GetType("System.Int64"));
                Structure_Tb.Columns.Add("NDP", System.Type.GetType("System.Int64"));

                int Record_Lenght = 0;

                ArrayList[] Field_Attribute;
                byte[] Header = new byte[32]; // هدر فاکس پرو 32 بایتی است
                byte[] Field = new byte[32];
                long Recourd_Count;
                long Data_Record_First;
                long Field_Count;

                FileStream fs = File.OpenRead(FileName);
                BinaryReader br = new BinaryReader(fs);

                br.Read(Header, 0, 32);

                Recourd_Count = (long)Header[4] + (long)Header[5] * 256 + (long)Header[6] * 256 * 256 + (long)Header[7] * 256 * 256 * 256; // تعداد رکوردها
                Data_Record_First = (long)Header[8] + (long)Header[9] * 256; // موقعیت اولین رکورد در فاکس
                Field_Count = (Data_Record_First - 33) / 32;// تعداد فیلدها


                Field_Attribute = new ArrayList[Field_Count];
                for (int i = 0; i < Field_Count; i++)
                {

                    br.Read(Field, 0, 32);
                    string Field_Name = System.Text.Encoding.ASCII.GetString(Field, 0 , 10);
                    string Field_Type = System.Text.Encoding.ASCII.GetString(Field, 11 , 1);
                    long Displacement_of_field = (long)Field[12] + (long)Field[13] * 256 + (long)Field[14] * 256 * 256 + (long)Field[15] * 256 * 256 * 256;
                    int Length_of_field = (int)Field[16];
                    int Number_of_decimal_places = (int)Field[17]; // فاصله خالی
                    Field_Name = Field_Name.Replace("\0", ""); // آخر رشته
                    Dr = Structure_Tb.NewRow();

                   
                    if (Field_Name.ToLower() == "check")
                        Field_Name = "check1";
                    if (Field_Name.ToLower() == "in")
                        Field_Name = "in1";
                    if (Field_Name.ToLower() == "group")
                        Field_Name = "group1";
                    if (Field_Name.ToLower() == "start")
                        Field_Name = "start1";
                    if (Field_Name.ToLower() == "end")
                        Field_Name = "end1";
                    if (Field_Name.ToLower() == "counter")
                        Field_Name = "counter1";
                    if (Field_Name.ToLower() == "date")
                        Field_Name = "date12";
                    if (Field_Name.ToLower() == "desc")
                        Field_Name = "Fdesc";

                    if (Field_Name.Equals("TSL"))
                    {
                        Dr["Field_Name"] = Field_Name;
                        Dr["Field_Type"] = 'C';
                    }
                    else
                    {
                        Dr["Field_Name"] = Field_Name;
                        Dr["Field_Type"] = Field_Type;
                    }
                    Dr["LOF"] = Length_of_field;
                    Dr["DOF"] = Displacement_of_field;
                    Dr["NDP"] = Number_of_decimal_places;
                    Structure_Tb.Rows.Add(Dr);
                    Record_Lenght = Length_of_field + Record_Lenght;
                }

                for (int i = 0; i < Field_Count; i++)
                    Convert_Tb_Temp.Columns.Add(Structure_Tb.Rows[i]["Field_Name"].ToString(), System.Type.GetType("System.String"));
                Createtable(_TableName);

                Codeconvert Dos_To_Win = new Codeconvert();
                Dos_To_Win.init();
                int Field_Len;
                byte[] Record;
                br.BaseStream.Seek(System.Convert.ToInt64(Structure_Tb.Rows[0]["DOF"]), SeekOrigin.Current);  // شروع دیتا DOF


                for (int i = 0; i < Recourd_Count; i++)
                {

                    br.ReadByte();
                    Dr = Convert_Tb_Temp.NewRow();
                    // بین هر رکورد یک خط خالی یه چیزی تو این مایه ها
                    for (int j = 0; j < Field_Count; j++)
                    {

                        Field_Len = System.Convert.ToInt32(Structure_Tb.Rows[j]["LOF"]);
                        Record = null;
                        Record = new byte[Field_Len];
                        br.Read(Record, 0, System.Convert.ToInt32(Structure_Tb.Rows[j]["LOF"]));

                        Dr[Structure_Tb.Rows[j]["Field_Name"].ToString()] = Dos_To_Win.Dos_Convert_ToWin(Record);

                    }
                    InsertRecord(_TableName, Dr);

                    if (RecoredInserted != null)
                        RecoredInserted(i, Recourd_Count);
                    
                }
                br.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                
            }
        }
        public void Createtable(string TableName)
        {

            dataTable = new DataTable(TableName);
            try
            {
                for (int i = 0; i < Structure_Tb.Rows.Count; i++)
                {
                    
                        dataTable.Columns.Add(Structure_Tb.Rows[i]["Field_Name"].ToString().Replace("?", ""), Type.GetType("System.String"));
                }
                /* Implementation for auto increment field  */
                //DataColumn dataColumn = dataTable.Columns.Add(Structure_Tb.Rows[Structure_Tb.Rows.Count - 1]["Field_Name"].ToString().Replace("?", ""), typeof(string));
                //dataColumn.AutoIncrement = true;

                resultDataSet.Tables.Add(dataTable);
            }
            catch (Exception ex)
            {
            }
        }


        public void InsertRecord(string TableName, System.Data.DataRow DR)
        {
            DataRow dataRow = resultDataSet.Tables[TableName].NewRow();
            try
            {
                for (int i = 0; i < Structure_Tb.Rows.Count ; i++)
                {
                    dataRow[Structure_Tb.Rows[i]["Field_Name"].ToString()] = DR[Structure_Tb.Rows[i]["Field_Name"].ToString()].ToString().Trim();
                }

                dataTable.Rows.Add(dataRow);
            }
            catch (Exception ex)
            {
            }

        }
    }
}

