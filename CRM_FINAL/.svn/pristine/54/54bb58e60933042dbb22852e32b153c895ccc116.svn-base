using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TranslationPostInputDB
    {
        public static TranslationPostInput GetTranslationPostInputByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationPostInputs.Where(t => t.RequestID == requestID).SingleOrDefault();
            }
        }

        public static List<TranslationPostInputConectionSelection> GetTranslationPostInputConectionSelectionByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationPostInputConnections.Where(t => t.RequestID == requestID)
                    .Select(t => new TranslationPostInputConectionSelection
                    {
                        ID = t.ID,
                        Connection = t.ConnectionID,
                        NewConnection = t.NewConnectionID,
                        CabinetInput = t.CabinetInputID
                    }).ToList();
            }
        }

        public static List<TranslationPostInputConnection> GetTranslationPostInputConectionByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationPostInputConnections.Where(t => t.RequestID == requestID).ToList();
            }
        }

        public static void VerifyData(TranslationPostInput translationPostInput, List<TranslationPostInputConectionSelection> translationPostInputConectionSelections)
        {
            Cabinet FromCabinet = Data.CabinetDB.GetCabinetByID(translationPostInput.FromCabinetID);
            Cabinet ToCabinet = Data.CabinetDB.GetCabinetByID(translationPostInput.ToCabinetID);

            Post fromPost = Data.PostDB.GetPostByID(translationPostInput.FromPostID);
            Post toPost = Data.PostDB.GetPostByID(translationPostInput.ToPostID);

            if ((FromCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && ToCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet)
                 || (ToCabinet.CabinetUsageType == (byte)DB.CabinetUsageType.OpticalCabinet && FromCabinet.CabinetUsageType != (byte)DB.CabinetUsageType.OpticalCabinet))
                throw new Exception("امکان برگردان از کافو نوری به کافو غیر از نوری نیست");



            if (translationPostInputConectionSelections.Any(t=> t.Connection == 0))
                throw new Exception("لطفا اتصالی را انتخاب کنید");

            if (translationPostInputConectionSelections.GroupBy(t => t.Connection).Any(t => t.Count() > 1))
                throw new Exception("استفاده از اتصالی تکراری در لیست انتخاب انصالی امکان پذیر نیست");


            if (translationPostInputConectionSelections.Any(t => t.CabinetInput == 0))
                throw new Exception("لطفا ورودی را انتخاب کنید");

            if (translationPostInputConectionSelections.GroupBy(t => t.CabinetInput).Any(t => t.Count() > 1))
                throw new Exception("استفاده از ورودی تکراری در لیست انتخاب انصالی امکان پذیر نیست");

            if (fromPost.ID != toPost.ID)
            {
                if (translationPostInputConectionSelections.Any(t=> t.NewConnection == 0))
                    throw new Exception("لطفا برای همه اتصالی های اتصالی جدید را انتخاب کنید.");

                if (translationPostInputConectionSelections.GroupBy(t => t.NewConnection).Any(t => t.Count() > 1))
                    throw new Exception("استفاده از اتصالی جدید تکراری در لیست انتخاب انصالی امکان پذیر نیست");
            }
            else
            {
                if (!(translationPostInputConectionSelections.All(t => t.NewConnection == 0) || translationPostInputConectionSelections.All(t => t.NewConnection != 0)))
                        throw new Exception("لطفا برای همه اتصالی ها اتصالی جدید را انتخاب کنید یا برای انتقال با همان انتصالی ها همه را خالی بگذارید.");

            }

         

            //if (fromPost.ID != toPost.ID)
            //{

            //    List<PostContact> fromPostContact = Data.PostContactDB.GetPostContactByListID(translationPostInputConectionSelections.Select(t => t.Connection).ToList());

            //    List<PostContact> toPostContact = Data.PostContactDB.GetPostContactByPostID(toPost.ID);

            //    if (toPostContact.Where(t => fromPostContact.Select(t2 => t2.ConnectionNo).Contains(t.ConnectionNo)).Any(t => t.Status != (byte)DB.PostContactStatus.Free))
            //        throw new Exception("اتصالی انتخاب شده در پست جدید باید آزاد باشد");

            //}



        }

        public static TranslationPostInputDetail GetTranslationPostInputDetailByID(long reqeustID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationPostInputs.Where(t => t.RequestID == reqeustID)
                    .Select(t => new TranslationPostInputDetail
                    {
                        FromCabinet = t.Cabinet.CabinetNumber,
                        ToCabinet = t.Cabinet1.CabinetNumber,
                        FromPost = t.Post.Number,
                        ToPost = t.Post1.Number,

                    }).SingleOrDefault();
            }
        }
    }
}
