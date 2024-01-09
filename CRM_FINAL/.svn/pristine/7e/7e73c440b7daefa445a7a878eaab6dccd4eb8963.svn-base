using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class DocumentTypeDB
    {
        public static List<DocumentType> SearchDocumentType(List<int> doumentType, string documentName, bool? existOnce)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.DocumentTypes
                    .Where(t =>
                            (doumentType.Count == 0 || doumentType.Contains((int)t.TypeID)) &&    
                            (string.IsNullOrWhiteSpace(documentName) || t.DocumentName.Contains(documentName)) &&
                            (!existOnce.HasValue || existOnce == t.ExistOnce))
                    .ToList();
            }
        }

        public static DocumentType GetDocumentTypeById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.DocumentTypes
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetDocumentTypeCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.DocumentTypes.Select(t => new CheckableItem
                {
                    ID = t.ID,
                    Name = t.DocumentName,
                    IsChecked = false
                }).ToList();
            }
        }

        public static List<DocumentType> GetAllEntity()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.DocumentTypes.OrderBy(t=>t.ID).ToList();
            }
        }

        public static List<Forms> SearchForm(List<int> RequestTypes, string Title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FormTemplates.Where(t =>
                    (RequestTypes.Count == 0 || RequestTypes.Contains((int)t.RequestTypeID))
                    && (string.IsNullOrEmpty(Title) || t.Title.Contains(Title))).Select(t => new Forms
                    {
                        RequestTypeID=t.RequestTypeID,
                        Title=t.Title,
                        TimeSpam=t.TimeStamp,
                        ID=t.ID,
                        Template=t.Template
                    }).ToList();
            }
        }

        public static FormTemplate GetFormTemplateByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FormTemplates.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}
