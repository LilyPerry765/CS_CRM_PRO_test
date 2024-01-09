using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace CRMConvertApplication
{
    public static class DB
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
                MetaDataMember creatorUesrID = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "CreatorUserID").SingleOrDefault();
                MetaDataMember modifyUserID = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "ModifyUserID").SingleOrDefault();


                object obj = instance;

                //if (modifyDate != null)
                //    modifyDate.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());

                //if (modifyUserID != null)
                //    modifyUserID.MemberAccessor.SetBoxedValue(ref obj, DB.CurrentUser.ID);

                if (isNew || (primaryKey.MemberAccessor.GetBoxedValue(instance) == null || (IsValidType(primaryKey.MemberAccessor.GetBoxedValue(instance), typeof(Int64))) && Convert.ToInt64(primaryKey.MemberAccessor.GetBoxedValue(instance)) == 0))
                {
                    //if (creatorUesrID != null)
                    //    creatorUesrID.MemberAccessor.SetBoxedValue(ref obj, DB.CurrentUser.ID);

                    //if (insertDateField != null)
                    //    insertDateField.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());

                    context.GetTable(instance.GetType()).InsertOnSubmit(instance);
                }
                else
                {
                    context.GetTable(instance.GetType()).Attach(instance);
                    context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, instance);
                }
                context.SubmitChanges();
                try
                {
                    //Logger.WriteInfo(context.Mapping.GetTable(instance.GetType()).TableName + " ID = " + context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "ID").SingleOrDefault().MemberAccessor.GetBoxedValue(instance));
                }
                catch
                {
                }
            }
        }

        public static void SaveAll<T>(List<T> instance) where T : class
        {
            //TODO : Add Update
            using (MainDataContext context = new MainDataContext())
            {
                context.GetTable(typeof(T)).InsertAllOnSubmit(instance);
                context.SubmitChanges();
            }
        }

        public static long GenerateRequestID()
        {
            DateTime serverDate = DB.GetServerDate();

            long requestID = GetMaxRequestIDByDate(serverDate) + 1;

            if (requestID == 1)
                requestID = long.Parse(Date.GetPersianDate(serverDate, Date.DateStringType.NoSlash) + "000" + requestID.ToString());

            return requestID;
        }

        public static DateTime GetServerDate()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExecuteQuery<DateTime>("SELECT GETDATE()").SingleOrDefault();
            }
        }

        public static long GetMaxRequestIDByDate(DateTime date)
        {
            using (MainDataContext context = new MainDataContext())
            {
                long requestId = 0;
                if (context.Requests.Where(t => t.InsertDate.Date == date.Date).ToList().Count != 0)
                    requestId = context.Requests
                            .Where(t => t.InsertDate.Date == date.Date).Max(t => t.ID);

                return requestId;
            }
        }

        public static Status GetStatus(int requestType, int statusType)
        {
            return GetStatusbyStatusType(requestType, statusType);
        }

        public static Status GetStatusbyStatusType(int requestType, int statusType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Status.Where(t => context.RequestSteps.Where(rs => requestType == rs.RequestTypeID).Select(rs => rs.ID).Contains(t.RequestStepID)).Where(t => t.StatusType == statusType).SingleOrDefault();
            }
        }
    }
}
