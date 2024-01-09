using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace CRM.Data
{
    public static class CRMWebServiceDB
    {
        private static bool IsValidType(object o, Type t)
        {
            try
            {
                System.Convert.ChangeType(o, t);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Save(object instance, bool isNew = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                MetaDataMember primaryKey = context.Mapping.GetTable(instance.GetType()).RowType.IdentityMembers[0];
                MetaDataMember insertDateField = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "InsertDate").SingleOrDefault();
                MetaDataMember modifyDate = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "ModifyDate").SingleOrDefault();

                object obj = instance;

                if (modifyDate != null)
                    modifyDate.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());

                if (isNew || (primaryKey.MemberAccessor.GetBoxedValue(instance) == null || (IsValidType(primaryKey.MemberAccessor.GetBoxedValue(instance), typeof(Int64))) && Convert.ToInt64(primaryKey.MemberAccessor.GetBoxedValue(instance)) == 0))
                {
                    if (insertDateField != null)
                        insertDateField.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());

                    context.GetTable(instance.GetType()).InsertOnSubmit(instance);
                }
                else
                {
                    context.GetTable(instance.GetType()).Attach(instance);
                    context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, instance);
                }
                context.SubmitChanges();
            }
        }

        public static void SendMessage(string telephoneNos, string message)
        {
            Services.CRMWebService service = new Services.CRMWebService();

            service.SendMessage(telephoneNos, message);
        }
    }
}
