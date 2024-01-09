using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CaseManagement
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

        public DBHelper()
        {
            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ToString());
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
        }

        public DateTime GetServerDate()
        {
            SqlCommand cmd = new SqlCommand("SELECT GETDATE()", sqlCon);
            if (sqlCon.State != ConnectionState.Open)
                sqlCon.Open();
            cmd.CommandText = "SELECT GETDATE()";
            var rslt = cmd.ExecuteScalar();
            return (DateTime)rslt;
        }
    }
}