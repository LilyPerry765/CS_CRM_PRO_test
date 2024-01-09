using CRM.WebAPI.Models.Shahkar.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.WebAPI.Models.Shahkar.CustomClasses
{
    /// <summary>
    /// چون پاسخ های سامانه شاهکار برای کاربر مبهم است ، این کلاس را برای
    /// قابل فهم کردن پاسخ های شاهکار ایجاد کردم
    /// نمونه پاسخ شاهکار  : 
    /// 
        //{
        //"response":311,
        //"requestId":"030120170404150351564277",
        //"result":"RequiredField:birthDate;",
        //"comment":"مقدار نامعتبر"
        //}
    /// </summary>
    public class ShahkarMeaningfulResponse
    {
        #region Properties and Fields

        public string Descriptions { get; set; }

        #endregion

        #region Methods


        public static ShahkarMeaningfulResponse ProvideShahkarMeaningfulResponseByRawResultFromShahkar(ShahkarRawResult shahkarRawResult)
        {
            ShahkarMeaningfulResponse shahkarMeaningfulResponse = new ShahkarMeaningfulResponse();

            string[] resultItems = shahkarRawResult.result.Split(';');
            foreach (string resultItem in resultItems)
            {
                if (string.IsNullOrEmpty(resultItem))
                {
                    continue;
                }

                //بررسی این موضوع که آیا علت عدم دریافت احراز هویت از شاهکار ، مقادیر غیرمعتبر بوده است یا خیر
                shahkarMeaningfulResponse.Descriptions += ShahkarValidator.InvalidFieldCheck(resultItem);

                //بررسی این موضوع که آیا علت عدم دریافت احراز هویت از شاهکار ، مقادیر خالی بوده است یا خیر
                shahkarMeaningfulResponse.Descriptions += ShahkarValidator.RequiredFieldCheck(resultItem);
            }

            return shahkarMeaningfulResponse;
        }

        #endregion
    }
}
