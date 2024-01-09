using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class Service
    {

        private byte _CallParammeter = 0;
        public byte CallParammeter
        {
            get { return _CallParammeter; }
            set { _CallParammeter = value; }
        }

        private long _telephone = 0;
        public long Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }


        private string _NationalCode = string.Empty;
        public string NationalCode
        {
            get { return _NationalCode; }
            set { _NationalCode = value; }
        }


        public  System.Data.DataTable GetSMSService()
        {
            if (DB.City == "semnan")
            {

                // when i can not connet to server use this code for test
                // return DB.FillSenmanDataTable();
                ///////////
                string userName = "Admin";
                string passWord = "alibaba123";

                Service1 service = new Service1();
                System.Data.DataTable data = new System.Data.DataTable();

                switch(_CallParammeter)
                {
                    case (byte)DB.ServicCallParameter.ByTelephone:
                        {
                            data = service.GetInformationForPhone(userName, passWord, _telephone.ToString());
                        }
                        break;
                    case (byte)DB.ServicCallParameter.ByNationalCode:
                        {
                            data = service.GetInformationByNationalCode(userName, passWord, _NationalCode.ToString());
                        }
                        break;
                }
                return data;
            }
            else
            {
                return null;
            }

        }
    }
}
