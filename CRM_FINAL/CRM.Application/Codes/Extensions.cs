using CRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CRM.Application.Codes
{
    public static class Extensions
    {
        /// <summary>
        /// .این متد چک میکند که یک شی از یک نوع داده خاص مقداری دارد یا خیر
        /// </summary>
        /// <param name="obj">شیء مورد نظر</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object obj)
        {
            bool result = false;
            if (
                obj == null //for reference types such as classes
                ||
                string.IsNullOrEmpty(obj.ToString()) //for string type
                ||
                obj.ToString().Equals("0") //for integer types
                ||
                obj.ToString().Equals("0.0") //for float type
               )
            {
                result = true;
            }
            return result;
        }


        public static System.Data.DataSet ToDataSet<T>(this IList<T> list, string dataTableName, System.Windows.Controls.DataGrid dataGrid)
        {
            Type elementType = typeof(T);
            PropertyInfo[] propertyInfos = elementType.GetProperties().Where(t2 => t2.PropertyType.Namespace == "System").ToArray();

            //elementType.GetProperties().Where(t2 => t2.PropertyType.Namespace == "CRM.Data").ToList()
            //    .ForEach(t3=>
            //    {
            //        Type temp = Assembly.Load("CRM.Data").GetTypes().First(t4 => t4.Name == t3.Name);
            //        if (temp != null)
            //        {

            //           var x = list.Select(l => l.GetType().GetNestedTypes().Take(1).SingleOrDefault()).ToList();
            //            //var x = list.SelectMany(l =>  typeof(temp)).ToList();
            //            propertyInfos = propertyInfos.Union(temp.GetProperties().Where(t2 => t2.PropertyType.Namespace == "System").ToArray()).ToArray();
            //        }
            //    });

            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable t = new System.Data.DataTable(dataTableName);
            ds.Tables.Add(t);


            //add a column to table for each public property on T
            foreach (PropertyInfo propInfo in propertyInfos)
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            
            foreach (T item in list)
            {
               
                System.Data.DataRow row = t.NewRow();

                foreach (PropertyInfo propInfo in propertyInfos)
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }
            int columnsCount = dataGrid.Columns.Count();
            dataGrid.Columns.ToList().ForEach(
            t3 =>
            {
                if (t3.ClipboardContentBinding != null)
                {
                     string columnName = ((System.Windows.Data.Binding)(t3.ClipboardContentBinding)).Path.Path.ToString();
                     if (ds.Tables[dataTableName].Columns[columnName] != null)
                    {
                       // ds.Tables[dataTableName].Columns[columnName].SetOrdinal(/*columnsCount -*/ t3.DisplayIndex);
                        ds.Tables[dataTableName].Columns[columnName].Caption = (string)t3.Header;
              
                    }
                }
            });

            //List<string> deletaName = new List<string>();
            //foreach (DataColumn column in ds.Tables[dataTableName].Columns)
            //{
            //    if (!dataGridSelectedIndexs.Any(t2 => t2.bindingPath == column.ColumnName))
            //    {
            //        deletaName.Add(column.ColumnName);
            //    }

            //}

            //deletaName.ForEach(t2 => { ds.Tables[dataTableName].Columns.Remove(t2); });

            return ds;
        }

        /// <summary>
        /// این متد چک میکند که آیا یک شی از یک اینترفیس جنریک ارث بری کرده است یا خیر
        /// </summary>
        /// <param name="type">شی مورد نظر</param>
        /// <param name="genericInterface">نوع اینترفیس جنریک</param>
        /// <returns></returns>
        public static bool InheritedFrom(this Type type, Type genericInterface)
        {
            bool result = false;
            result = type.GetInterfaces()
                         .Any(ty =>
                                     (ty.IsGenericType) &&
                                     (ty.GetGenericTypeDefinition() == genericInterface)
                             );
            return result;
        }
    }
}
