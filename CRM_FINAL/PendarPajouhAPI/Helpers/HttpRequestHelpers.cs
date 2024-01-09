using CRM.Data;
using Enterprise;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Script.Serialization;

namespace PendarPajouhAPI.Helpers
{
    public static class HttpRequestHelpers
    {
        public static string GenerateRequestId()
        {
            DateTime serverDate = DB.GetServerCompleteFormatDate();

            string currentDateString = "";
            string requestId = "";
            string provinceCode = "0301";

            currentDateString = string.Format("{0}{1}", serverDate.ToString("yyyyMMddHHmmss"), (serverDate.Ticks / 10).ToString().Substring(11, 6));

            requestId = string.Format("{0}{1}", provinceCode, currentDateString);

            return requestId;
        }

        public static string GenerateBase64Authorization(string username, string password)
        {
            string concatedCredential = string.Format("{0}:{1}", username, password);

            string authHeader = string.Format("Basic {0}", StringHelpers.ToBase64(concatedCredential));

            return authHeader;
        }

        public static string SendHttpWebRequest(object entity, string webApiAddress, string authorizationHeader)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string responseDetails = string.Empty;
            string jsonOfEntity = serializer.Serialize(entity);
            byte[] entityArray = System.Text.Encoding.UTF8.GetBytes(jsonOfEntity);

            Uri apiAddress = new Uri(webApiAddress, UriKind.Absolute);

            HttpWebRequest request = WebRequest.Create(apiAddress) as HttpWebRequest;
            request.ContentLength = entityArray.Length;
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
            var requestStream = request.GetRequestStream();
            requestStream.Write(entityArray, 0, entityArray.Length);
            requestStream.Close();

            WebResponse response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(responseStream))
            {
                responseDetails = reader.ReadToEnd();
            }
            responseStream.Close();
            response.Close();

            return responseDetails;
        }

        public static string SendHttpWebRequestWithoutData(string webApiAddress, string authorizationHeader)
        {
            string responseDetails = string.Empty;

            Uri apiAddress = new Uri(webApiAddress, UriKind.Absolute);

            HttpWebRequest request = WebRequest.Create(apiAddress) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers[HttpRequestHeader.Authorization] = authorizationHeader;

            WebResponse response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(responseStream))
            {
                responseDetails = reader.ReadToEnd();
            }
            responseStream.Close();
            response.Close();

            return responseDetails;
        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                if (reply.Status == IPStatus.Success)
                {
                    pingable = true;
                }
            }
            catch (PingException pe)
            {
                Logger.WriteError("PendarPajouhAPI => Following exception caught in {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.WriteException("PendarPajouhAPI => {0}", pe.Message);
            }
            return pingable;
        }

    }
}