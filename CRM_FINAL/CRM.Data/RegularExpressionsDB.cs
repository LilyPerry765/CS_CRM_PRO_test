using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class RegularExpressionsDB
    {
        public static List<RegularExpression> SearchRegularExpressions(string name, string regularExpressinon, string errorMessage)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RegularExpressions
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(name) || t.Name.Contains(name)) &&
                            (string.IsNullOrWhiteSpace(regularExpressinon) || t.RegularExpressinon.Contains(regularExpressinon)) &&
                            (string.IsNullOrWhiteSpace(errorMessage) || t.ErrorMessage.Contains(errorMessage))
                          )
                    //.OrderBy(t => t.Name)
                    .ToList();
            }
        }

        public static RegularExpression GetRegularExpressionsByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RegularExpressions
                    .Where(t => t.Id == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetRegularExpressionsCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.RegularExpressions
                    .Select(t => new CheckableItem
                    {
                        ID = t.Id,
                        Name = t.Name,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }
    }
}