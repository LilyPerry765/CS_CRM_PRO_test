using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class DataGridColumnConfigDB
    {

        public static DataGridColumnConfig GetDataGridColumnConfig(string formName, string dataGridName)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.DataGridColumnConfigs.Where(t => t.UserID == DB.currentUser.ID && t.FormName == formName && t.DataGridName == dataGridName).SingleOrDefault();
            }
        }

        public static DataGridColumnConfig GetDataGridColumnConfig(string formName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.DataGridColumnConfigs.Where(t => t.UserID == DB.currentUser.ID && t.FormName == formName).SingleOrDefault();
            }
        }

        public static List<DataGridColumnConfig> GetDataGridColumnConfig(int userID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.DataGridColumnConfigs.Where(t => t.UserID == userID).ToList();
            }
        }
    }
}
