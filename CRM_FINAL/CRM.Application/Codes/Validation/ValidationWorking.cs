using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CRM.Application.Codes.Validation
{
    public class ValidationWorking
    {
        public static Codes.Validation.XmlValidation GetValidationXml()
        {
            using (Data.MainDataContext entity
                = new Data.MainDataContext())
            {
                var varSettings = entity.Settings.FirstOrDefault(q => q.Key == "XmlValidation");

                if (varSettings == null || varSettings.Content == null)
                {
                    XmlValidation xmlValidationNull = new XmlValidation();
                    SaveValidationXml(xmlValidationNull);

                    return (xmlValidationNull);
                }

                System.Xml.Serialization.XmlSerializer xmlSerialize = new System.Xml.Serialization.XmlSerializer(typeof(Codes.Validation.XmlValidation));

                Codes.Validation.XmlValidation xmlValidation = (Codes.Validation.XmlValidation)xmlSerialize.Deserialize(new MemoryStream(varSettings.Content.ToArray()));

                return (xmlValidation);
            }
        }

        public static void SaveValidationXml(Codes.Validation.XmlValidation xmlValidation)
        {
            System.IO.MemoryStream fs = new MemoryStream();

            System.Xml.Serialization.XmlSerializer xmlSerialize = new System.Xml.Serialization.XmlSerializer(typeof(Codes.Validation.XmlValidation));
            xmlSerialize.Serialize(fs, xmlValidation);

            fs.Close();

            using (Data.MainDataContext entity = new Data.MainDataContext())
            {
                var varSetting = entity.Settings.FirstOrDefault(q => q.Key == "XmlValidation");

                if (varSetting == null)
                {
                    varSetting = new Data.Setting() { Key = "XmlValidation" };
                    entity.Settings.InsertOnSubmit(varSetting);
                }

                varSetting.Content = fs.ToArray();
                entity.SubmitChanges();
            }
        }
    }
}
