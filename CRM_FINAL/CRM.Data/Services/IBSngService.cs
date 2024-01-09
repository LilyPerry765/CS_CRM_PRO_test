using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using CookComputing.XmlRpc;

namespace CRM.Data.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class IBSngService
    {
        public IBSngService()
        {
            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [XmlRpcUrl("http://ibsng.tcsem.ir:1235/")]
        public interface Isend_recv : IXmlRpcProxy
        {
            [XmlRpcMethod("user.getUserInfo")]
            XmlRpcStruct GetUserInfo(XmlRpcStruct arguments);

            [XmlRpcMethod("charge.getChargeInfo")]
            XmlRpcStruct getChargeInfo(XmlRpcStruct arguments);

            [XmlRpcMethod("user.searchUser")]
            object[] SearchUser(params object[] arguments);

            [XmlRpcMethod("login.login")]
            bool login(XmlRpcStruct arguments);

            [XmlRpcMethod("user.changeDeposit")]
            void changeDeposit(XmlRpcStruct arguments);

            [XmlRpcMethod("user.changeCredit")]
            void changeCredit(XmlRpcStruct arguments);

            [XmlRpcMethod("group.getGroupUsersCount")]
            XmlRpcStruct getGroupUsersCount(XmlRpcStruct arguments);

            [XmlRpcMethod("report.getConnections")]
            XmlRpcStruct GetConnections(XmlRpcStruct arguments);

            [XmlRpcMethod("user.addNewUsers")]
            object[] AddNewUsers(XmlRpcStruct arguments);

            [XmlRpcMethod("user.updateUserAttrs")]
            void UpdateUserAttrs(XmlRpcStruct arguments);

            [XmlRpcMethod("user.delUser")]
            void DeleteUser(XmlRpcStruct arguments);
            
            [XmlRpcMethod("util.afeGetAllGroups")]
            object[] AfeGetAllGroups(XmlRpcStruct arguments);

            [XmlRpcMethod("report.getUserDepositChanges")]
            XmlRpcStruct GetUserDepositChanges(XmlRpcStruct arguments);

            [XmlRpcMethod("report.getCreditChanges")]
            XmlRpcStruct GetUserCreditChanges(XmlRpcStruct arguments);

            [XmlRpcMethod("report.getUserAuditLogs")]
            XmlRpcStruct GetUserAuditLogs(XmlRpcStruct arguments);            

            [XmlRpcMethod("user.killUser")]
            void KillUser(XmlRpcStruct arguments);
        }

        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public struct Connections
        {
            public object total_rows;
            public double total_in_bytes;
            public object total_duration;
            public double total_out_bytes;
            public object total_credit;
            public ConnectionDetail[] report;
        }

        public struct ConnectionDetail
        {
            public string called_number;
            public string logout_time_formatted;
            public string caller_id;
            public string voip_provider;
            public int user_id;
            public string login_time_formatted;
            public object[] details;
            public string ras_description;
            public string service_type;
            public string username;
            public double voip_provider_credit_used;
            public string successful;
            public string prefix_code;
            public string called_ip;
            public string disconnect_cause;
            public int voip_provider_id;
            public int connection_log_id;
            public double cpm;
            public double duration_seconds;
            public double credit_used;
            public int retry_count;
            public string prefix_name;
            public string remote_ip;
        }
    }
}
