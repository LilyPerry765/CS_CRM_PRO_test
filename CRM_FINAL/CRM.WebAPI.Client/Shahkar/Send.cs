using CRM.WebAPI.Models.Interfaces;
using CRM.WebAPI.Models.Shahkar.CustomClasses;
using Enterprise;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CRM.WebAPI.Client.Shahkar
{
    /// <summary>
    /// . دیتا تایپ هایی میتوانند از این کلاس استفاده کنند که از اینترفیس زیر ارث بری کرده باشند
    /// IResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Send<T> where T : IResult
    {
        /// <summary>
        /// .این متد یک درخواست را برای آدرس مورد نظر به صورت رست ارسال میکند ، همچنین دیتا را در آن می گنجاند
        /// </summary>
        /// <param name="data">دیتا برای ارسال</param>
        /// <param name="apiAddress">آدرس مورد نظر</param>
        /// <param name="httpVerb">نوع درخواست اچ تی تی پی</param>
        /// <returns></returns>
        public Result SendHttpWebRequest(T data, string apiAddress, string httpVerb, string userName, string password)
        {
            Result result = null;

            try
            {
                Logger.WriteInfo("{0} with requestId: {1}  is sending to Shahkar.", data.GetType().Name, data.requestId);

                string authorizationBase64 = Helpers.GenerateBase64Authorization(userName, password);

                string responseDetails = string.Empty;
                string jsonData = JsonConvert.SerializeObject(data);
                byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

                Uri shahkarApiAddress = new Uri(apiAddress, UriKind.Absolute);

                HttpWebRequest request = WebRequest.Create(shahkarApiAddress) as HttpWebRequest;
                request.Method = httpVerb;
                request.ContentLength = dataBytes.Length;
                request.ContentType = "application/json";
                request.Headers[HttpRequestHeader.Authorization] = authorizationBase64;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(dataBytes, 0, dataBytes.Length);
                requestStream.Close();

                WebResponse response = request.GetResponse();

                Logger.WriteInfo("{0} is sent to Shahkar.", data.requestId);

                Stream responseStream = response.GetResponseStream();

                Logger.WriteInfo("{0} receives response from  Shahkar.", data.requestId);

                using (StreamReader reader = new StreamReader(responseStream))
                {
                    responseDetails = reader.ReadToEnd();
                }
                responseStream.Close();
                response.Close();

                result = JsonConvert.DeserializeObject<Result>(responseDetails);

                Logger.WriteInfo("Result from Shahkar  : {0}", Environment.NewLine + responseDetails);
            }
            catch (Exception ex)
            {
                Logger.WriteInfo("{0} has catght following error: ", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Write(ex);
            }
            return result;
        }
    }
}
