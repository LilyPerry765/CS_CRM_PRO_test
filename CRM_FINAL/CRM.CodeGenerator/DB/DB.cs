using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CRM.CodeGenerator.Data
{
    public static class DataBase
    {

        public static string ConnectionString
        {
            get;
            set;
        }

        public static List<Table> Tables
        {
            get
            {
                List<Table> tables = new List<Table>();
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM sys.tables WHERE type_desc = 'USER_TABLE' ORDER BY Name", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tables.Add(new Table { ID = (int)reader["object_id"], Name = reader["Name"].ToString() });
                            }
                        }

                        command.CommandText = @"SELECT  SysColumns.column_id    ID
                                                       ,SysColumns.name         Name
                                                       ,SysTypes.name           Type
                                                       ,( SELECT value FROM ::FN_LISTEXTENDEDPROPERTY('MS_Description', 'schema', 'dbo', 'table', @TableName, 'column', SysColumns.name) ) Description
                                                        ,SysColumns.max_length  MaxLength
                                                FROM    sys.columns SysColumns INNER JOIN sys.types SysTypes ON SysColumns.system_type_id = SysTypes.system_type_id
                                                WHERE   SysTypes.NAME <> 'sysname' AND SysColumns.object_ID = @TableID";

                        command.Parameters.Add("@TableID", SqlDbType.Int);
                        command.Parameters.Add("@TableName", SqlDbType.VarChar);

                        foreach (Table table in tables)
                        {
                            command.Parameters["@tableID"].Value = table.ID;
                            command.Parameters["@tableName"].Value = table.Name;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    table.Columns.Add(
                                        new Column { 
                                                         ID = (int)reader["ID"],
                                                         Table = table, 
                                                         Name = reader["Name"].ToString(), 
                                                         Type = reader["Type"].ToString(), 
                                                         Description = reader["Description"].ToString(),
                                                         MaxLength = Convert.ToInt32(reader["MaxLength"])
                                                       }
                                                    );
                                }
                            }
                        }
                        command.Parameters.Clear();

                        command.CommandText = "SELECT  referenced_object_id [ReferencedTableID], parent_object_id [TableID], parent_column_id [ColumnID] FROM sys.foreign_key_columns";
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tables.Where(t => t.ID == (int)reader["TableID"]).FirstOrDefault().Columns.Where(c => c.ID == (int)reader["ColumnID"]).FirstOrDefault().ReferencedTable = tables.Where(t => t.ID == (int)reader["ReferencedTableID"]).FirstOrDefault();
                            }
                        }
                    }
                }

                return tables;
            }
        }
    }
}

