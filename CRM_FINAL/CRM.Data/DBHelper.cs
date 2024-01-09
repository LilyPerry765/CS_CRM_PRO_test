﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace CRM.Data
{
    public class DBHelper
    {
        private string conString;
        private SqlConnection sqlCon;
        private string strQry;

        public DBHelper(string server, string username, string password, string dbName)
        {
            if (String.IsNullOrEmpty(username))
                conString = "Server=" + server + ";Database=" + dbName + ";Trusted_Connection=True;";
            else
                conString = "Server=" + server + ";Database= " + dbName + ";User Id=" + username + ";Password=" + password + ";";

            sqlCon = new SqlConnection(conString);
        }

        public DBHelper(string connectionstring = null)
        {
            if (string.IsNullOrEmpty(connectionstring))
            {
                using(MainDataContext context = new MainDataContext())
                {
                    sqlCon = new SqlConnection(context.Connection.ConnectionString);
                }
                
            }
            else
                sqlCon = new SqlConnection(connectionstring);
        }

        public bool Connected()
        {
            try
            {
                sqlCon.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable ExecuteDT(string qry)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(qry, sqlCon);
                cmd.CommandTimeout = 0;
                if (sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dtGet = new DataTable();
                da.Fill(dtGet);
                return dtGet;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public DataTable ExecuteDT(SqlCommand cmd)
        {
            try
            {
                cmd.CommandTimeout = 0;
                if (sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dtGet = new DataTable();
                da.Fill(dtGet);
                return dtGet;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public DataTable ExecuteQuery(string qry)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(qry, sqlCon);
                cmd.CommandTimeout = 0;
                if (sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dtGet = new DataTable();
                da.Fill(dtGet);
                return dtGet;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public object ExecuteNonQuery(string qry)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(qry, sqlCon);
                cmd.CommandTimeout = 0;
                if (sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();
                cmd.CommandText = qry;
                var rslt = cmd.ExecuteNonQuery();
                return rslt;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public object ExecuteScalar(string qry)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(qry, sqlCon);
                if (sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();
                cmd.CommandText = qry;
                var rslt = cmd.ExecuteScalar();
                return rslt;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public int ExecuteParam(string commandText, SqlParameter[] prm)
        {
            try
            {
                SqlCommand command = new SqlCommand(commandText, sqlCon);
                for (int i = 0; i < prm.Length; i++)
                {
                    if (prm[i].Value == null || DBNull.Value.Equals(prm[i].Value))
                        command.Parameters.AddWithValue(prm[i].ParameterName, DBNull.Value);
                    else
                        command.Parameters.AddWithValue(prm[i].ParameterName, prm[i].Value);
                }
                if (sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();

                
                Int32 rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception e)
            {
                return -1;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public DataTable ExecuteParamDT(string commandText, SqlParameter[] prm)
        {
            try
            {
                SqlCommand command = new SqlCommand(commandText, sqlCon);
                for (int i = 0; i < prm.Length; i++)
                {
                    if (prm[i].Value == null || DBNull.Value.Equals(prm[i].Value))
                        command.Parameters.AddWithValue(prm[i].ParameterName, DBNull.Value);
                    else
                        command.Parameters.AddWithValue(prm[i].ParameterName, prm[i].Value);
                }
                if (sqlCon.State != ConnectionState.Open)
                    sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                DataTable dtGet = new DataTable();
                da.Fill(dtGet);
                return dtGet;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public DataTable getDBList()
        {
            return ExecuteDT("SELECT name FROM master..sysdatabases");
        }

        public DataTable getIndexList(string dbName)
        {
            return ExecuteDT("SELECT s.name + '.' + O.Name ObjectName, si.name, dt.avg_fragmentation_in_percent, dt.avg_page_space_used_in_percent " +
                             "FROM (SELECT object_id, index_id, avg_fragmentation_in_percent, avg_page_space_used_in_percent " +
                             "FROM sys.dm_db_index_physical_stats (DB_ID('" + dbName + "'), NULL, NULL, NULL, 'DETAILED') " +
                             "WHERE index_id <> 0) as dt " +
                             "INNER JOIN sys.indexes si ON si.object_id = dt.object_id AND si.index_id = dt.index_id " +
                             "INNER JOIN sys.objects O ON O.object_id = dt.object_id " +
                             "INNER JOIN sys.schemas S ON S.schema_id = O.schema_id");
        }

        public bool RebuildIndex(string TableName, string IndexName)
        {
            strQry = "ALTER INDEX " + IndexName + " ON " + TableName + " REBUILD";
            var rslt = ExecuteNonQuery(strQry);
            if (rslt != null)
                return true;
            else
                return false;
        }

        public bool ShrinkDB(string dbName)
        {
            strQry = "IF (DATABASEPROPERTYEX('" + dbName + "', 'IsAutoShrink')= 0) " +
                     "	DBCC SHRINKDATABASE ( " + dbName + ", 0)";
            var rslt = ExecuteNonQuery(strQry);
            if (rslt != null)
                return true;
            else
                return false;
        }

        public bool CheckDB(string dbName)
        {
            strQry = "ALTER DATABASE [" + dbName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE " +
                     "DBCC CHECKDB ( '" + dbName + "', REPAIR_REBUILD) " +
                     "ALTER DATABASE [" + dbName + "] SET  MULTI_USER";
            var rslt = ExecuteNonQuery(strQry);
            if (rslt != null)
                return true;
            else
                return false;
        }

        public DataTable getTablesContainCHAR()
        {
            strQry = "SELECT C.TABLE_SCHEMA, C.TABLE_NAME, C.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS C " +
                     "INNER JOIN INFORMATION_SCHEMA.TABLES T " +
                     "ON C.TABLE_SCHEMA = T.TABLE_SCHEMA AND C.TABLE_NAME = T.TABLE_NAME " +
                     "WHERE T.TABLE_TYPE = 'BASE TABLE' AND C.DATA_TYPE LIKE '%CHAR'";
            return ExecuteDT(strQry);
        }

        public string MakeReplaceCharStr(string Schema, string Table, string Column, string FindWhat, string ReplaceWith)
        {
            return "UPDATE ['" + Schema + "'].['" + Table + "'] SET ['" + Column + "'] = REPLACE(['" + Column + "'], '" + FindWhat + "', '" + ReplaceWith + "')'";
        }

        private List<string> getSqlServerInstanceList()
        {
            List<string> list = new List<string>();
            DataTable dataSources = SqlDataSourceEnumerator.Instance.GetDataSources();
            foreach (DataRow row in dataSources.Rows)
            {
                string item = row["ServerName"].ToString();
                if (((row["InstanceName"] != DBNull.Value) && (row["InstanceName"] != null)) && (row["InstanceName"].ToString().Trim() != string.Empty))
                {
                    item = item + @"\" + row["InstanceName"].ToString();
                }
                list.Add(item);
            }
            list.Sort();
            return list;
        }
    }

    public class SqlExceptionHelper
    {
        //TODO:rad روشی برای به دست آوردن نوع خطا
        /// <summary>
        /// .این متد خطا را گرفته و پیغام مناسب را برای کاربر میفرستد
        /// </summary>
        /// <param name="sqlException"></param>
        /// <returns></returns>
        public static string ErrorMessage(SqlException sqlException)
        {
            int sqlExceptionNumber = 0;
            if (sqlException.Errors.Count != 0)
            {
                sqlExceptionNumber = sqlException.Errors[0].Number;
            }
            string result = string.Empty;
            switch (sqlExceptionNumber)
            {
                case 2:
                    {
                        //An error has occurred while establishing a connection to the server. When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections.
                        result = "خطا در دسترسی به پایگاه داده";
                    }break;
                case 515:
                    {
                        //Cannot insert the value NULL into column '', table ''; column does not allow nulls.
                        result = "مقادیر ستون های اجباری باید تعیین گردند.";
                    }
                    break;
                case 2601:
                    {
                        //Cannot insert duplicate key row in object '%.*ls' with unique index '%.*ls'.
                        result = "مقادیر وارد شده ، در پایگاه داده وجود دارند.";
                    }
                    break;
                case 2627:
                    {
                        //Violation of %ls constraint '%.*ls'. Cannot insert duplicate key in object '%.*ls'.
                        result = "مقادیر وارد شده ، در پایگاه داده وجود دارند.";
                    }
                    break;
                case 547:
                    {
                        //DELETE statement conflicted with COLUMN REFERENCE constraint 'Constraint Name'.
                        if (sqlException.Errors[0].Message.Contains("DELETE statement conflicted with"))
                        {
                            result = "این رکورد با رکودهایی از جدوال دیگر در پایگاه داده ، در ارتباط میباشد و قابل حذف نیست.";
                        }
                    }
                    break;
                case 8152:
                    {
                        //String or binary data would be truncated
                        result = "تعداد کاراکترهای وارد شده، از حد مجاز بیشتر میباشد.";
                    }
                    break;
                case 0:
                default:
                    {
                        //do nothing
                        break;
                    }
            }
            return result;
        }
    }
}