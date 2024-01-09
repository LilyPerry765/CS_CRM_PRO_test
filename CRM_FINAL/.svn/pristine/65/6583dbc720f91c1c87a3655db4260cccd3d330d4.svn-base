using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class SpecialConditionFactory
    {
       
        SpecialCondition _specialCondition = null;

        public SpecialCondition SpecialCondition 
        { 
            get{ return _specialCondition; }
        }


        bool _IsNew = false;

        public bool IsNew
        {
            get { return _IsNew; }
        }
        public SpecialConditionFactory(long requestID, DB.SpecialConditions specialConditionsType , bool value)
        {

            _specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(requestID);
            if (_specialCondition == null)
            {
                _IsNew = true;
                _specialCondition = new SpecialCondition();
                _specialCondition.RequestID = requestID;
            }
            else
            {
                _IsNew = false;
            }

            switch(specialConditionsType)
            {
                case DB.SpecialConditions.BeFreeOldPhone:
                    _specialCondition.BeFreeOldPhone = value;
                    break;
                case DB.SpecialConditions.ChangeLocationInsider:
                    _specialCondition.ChangeLocationInsider = value;
                    break;
                case DB.SpecialConditions.EqualityOfBuchtTypeCusromerSide:
                    _specialCondition.EqualityOfBuchtTypeCusromerSide = value;
                    break;
                case DB.SpecialConditions.EqualityOfBuchtTypeOptical:
                    _specialCondition.EqualityOfBuchtTypeOptical = value;
                    break;
                case DB.SpecialConditions.IsDebt:
                    _specialCondition.IsDebt = value;
                    break;
                case DB.SpecialConditions.IsE1PTP:
                    _specialCondition.IsE1PTP = value;
                    break;
                case DB.SpecialConditions.IsGSM:
                    _specialCondition.IsGSM = value;
                    break;
                case DB.SpecialConditions.IsOpticalCabinet:
                    _specialCondition.IsOpticalCabinet = value;
                    break;
                case DB.SpecialConditions.IsOpticalE1:
                    _specialCondition.IsOpticalE1 = value;
                    break;
                case DB.SpecialConditions.MiddlePointSpecialWire:
                    _specialCondition.MiddlePointSpecialWire = value;
                    break;
                case DB.SpecialConditions.NotEqualityOfBuchtType:
                    _specialCondition.NotEqualityOfBuchtType = value;
                    break;
                case DB.SpecialConditions.NotEqualityOfBuchtTypeOptical:
                    _specialCondition.NotEqualityOfBuchtTypeOptical = value;
                    break;
                case DB.SpecialConditions.ReturnedFromWiring:
                    _specialCondition.ReturnedFromWiring = value;
                    break;

            }


        }
    }
}
