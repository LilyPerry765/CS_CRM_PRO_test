using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CRM.CodeGenerator.Data
{
    public class ConnectionManager
    {

        public static bool TestConnection(string connectionString)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
