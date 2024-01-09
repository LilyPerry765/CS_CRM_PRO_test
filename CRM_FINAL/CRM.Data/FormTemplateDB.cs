using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Data.Linq.Mapping;
namespace CRM.Data
{
    public static class FormTemplateDB
    {
        public static List<FormTemplate> GetFormTemplateByRequestID(long RequestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FormTemplates.Where(t => t.RequestTypeID == RequestID).ToList();
            }
        }

        public static List<Forms> GetFormTemplateByRequestTypeID(long RequestTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FormTemplates.Where(t => t.RequestTypeID == RequestTypeID).Select(t => new Forms
                    {
                        RequestTypeID=t.RequestTypeID,
                        ID=t.ID,
                        Template=t.Template,
                        Title=t.Title,
                        TimeSpam=t.TimeStamp,
                        
                    }).ToList();
            }
        }
    }
}
