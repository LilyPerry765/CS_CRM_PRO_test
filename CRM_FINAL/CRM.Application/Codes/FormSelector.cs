using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Data;

namespace CRM.Application.Local
{
    public static class FormSelector
    {
        public static PopupWindow Select(List<long> requestIDs, int? statusID = null, long? subID = null)
        {
            long requestID = requestIDs.Take(1).SingleOrDefault();
            int requestCurrentStateID = (statusID.HasValue) ? statusID.Value : RequestDB.GetCurrentState(requestID);
            int requestCurrentStepID = RequestDB.GetCurrentStep(requestID);
            int? formID = Data.WorkFlowDB.GetProperForm(requestID, requestCurrentStateID);

            PopupWindow window;

            switch (formID)
            {
                case (int)DB.Form.Install:
                    window = new Views.RequestForm(requestID);
                    break;
                case (int)DB.Form.Investigation:
                    window = new Views.InvestigatePossibilityForm(requestID, subID);
                    break;
                case (int)DB.Form.WiringMDFGroupedForm:
                    window = new Views.WiringMDFGroupedForm(requestIDs, subID);
                    break;
                case (int)DB.Form.WiringNetworkGroupedForm:
                    window = new Views.WiringNetworkGroupedForm(requestIDs, subID);
                    break;
                case (int)DB.Form.ChooseNumber:
                    window = new Views.ChooseNoForm(requestID);
                    break;
                case (int)DB.Form.IssueWiring:
                    window = new Views.IssueWiringForm(requestID);
                    break;
                case (int)DB.Form.MDFWiring:
                    window = new Views.MDFWiringForm(requestID, subID);
                    break;
                case (int)DB.Form.Wiring:
                    window = new Views.WiringForm(requestID, subID);
                    break;
                case (int)DB.Form.AssingmentLines:
                    window = new Views.AssignmentLinesForm(requestID);
                    break;
                case (int)DB.Form.Dayeri:
                    window = new Views.DayeriForm(requestID);
                    break;

                case (int)DB.Form.ExchangPost:
                    {
                        long exchangeID = Data.ExchangePostDB.GetExchangePostByRequestID(requestID);
                        window = new Views.ExchangePostForm(exchangeID);
                        break;
                    }
                case (int)DB.Form.ExchangeCentralCableMDF:
                    {
                        window = new Views.ExchangeCentralCableMDFForm(requestID);
                        break;
                    }
                case (int)DB.Form.ExchangeCenralCableCabinet:
                    {

                        window = new Views.ExchangeCabinetForm(requestID);
                        break;
                    }
                case (int)DB.Form.LinsemanForm:
                    window = new Views.LinsemanForm(requestID);
                    break;

                //case (int)DB.Form.Assignment:
                //    window = new Views.AssignmentForm(requestID);
                //    break;

                case (int)DB.Form.countor:
                    window = new Views.ChanngeLocationCountorForm(requestID);
                    break;

                case (int)DB.Form.MDF:
                    window = new Views.ChangeLocationMDFForm(requestID);
                    break;

                case (int)DB.Form.ThingsOfCustomer:
                    window = new Views.ChangeLocationThingeOfCustonerForm(requestID);
                    break;

                //case (int)DB.Form.DeterminedPostForm:
                //    window = new Views.DeterminedPostForm(requestID);
                //    break;

                case (int)DB.Form.ThreeWay:
                    window = null;
                    break;

                case (int)DB.Form.ChangeName:
                    window = new Views.ChangeNameForm(requestID);
                    break;

                case (int)DB.Form.Inquiry:
                    window = new Views.InquiryForm(requestID);
                    break;

                case (int)DB.Form.CutAndEstablish:
                    window = new Views.CutAndEstablishForm(requestID);
                    break;

                case (int)DB.Form.ConfirmChangeLocationCenterToCenter:
                    window = new Views.ConfirmChangeLocationCenterToCenterForm(requestID);
                    break;

                case (int)DB.Form.SpecialService:
                    window = new Views.SpecialServiceForm(requestID);
                    break;

                case (int)DB.Form.ADSLAssignmentLines:
                    window = new Views.ADSLAssignmentLines(requestID);
                    break;

                case (int)DB.Form.ADSLMDF:
                    window = new Views.ADSLMDF(requestID);
                    break;

                case (int)DB.Form.ADSLOMC:
                    window = new Views.ADSLOMC(requestID);
                    break;

                case (int)DB.Form.ADSLSetup:
                    window = new Views.ADSLSetup(requestID);
                    break;

                case (int)DB.Form.ADSLPAPCompany:
                    window = new Views.ADSLPAPForm(requestID);
                    break;

                case (int)DB.Form.TaskOfCustomer:
                    window = new Views.TaskOfCustomerForm(requestID);
                    break;

                case (int)DB.Form.ChangePreCode:
                    window = new Views.ChangePreCodeForm(requestID);
                    break;

                case (int)DB.Form.SpaceAndPower:
                    window = new Views.SpaceAndPowerForm(requestID);
                    break;

                case (int)DB.Form.ConstructionOfficeForm:
                    window = new Views.ConstructionOfficeForm(requestID);
                    break;

                case (int)DB.Form.NetworkAssistantForm:
                    window = new Views.NetworkAssistantForm(requestID);
                    break;

                case (int)DB.Form.AgreementContractForm:
                    window = new Views.AgreementContractForm(requestID);
                    break;

                case (int)DB.Form.DeviceHallForm:
                    window = new Views.DeviceHallForm(requestID);
                    break;

                case (int)DB.Form.AdministrationOfTheTelecommunicationEquipmentForm:
                    window = new Views.AdministrationOfTheTelecommunicationEquipmentForm(requestID);
                    break;

                case (int)DB.Form.InvoiceIssuanceForm:
                    window = new Views.InvoiceIssuanceForm(requestID);
                    break;

                case (int)DB.Form.Failure117:
                    window = new Views.Failure117Form(requestID);
                    break;

                case (int)DB.Form.Failure117Network:
                    window = new Views.Failure117NetworkForm(requestID);
                    break;

                case (int)DB.Form.VisitPlaces:
                    window = new Views.VisitPlacesForm(requestID);
                    break;

                case (int)DB.Form.ChooseNumberE1:
                    window = new Views.E1ChooseNumber(requestID, subID);
                    break;

                case (int)DB.Form.TechnicalSupportDepartment:
                    window = new Views.E1TechnicalSupportDepartmentForm(requestID, subID);
                    break;

                case (int)DB.Form.TechnicalSupport:
                    window = new Views.E1TechnicalSupportForm(requestID, subID);
                    break;

                case (int)DB.Form.SwitchE1:
                    window = new Views.E1SwitchForm(requestID, subID);
                    break;
                case (int)DB.Form.CustomerToApproveDebtorForm:
                    window = new Views.CustomerToApproveDebtorForm(requestID);
                    break;
                case (int)DB.Form.E1MicrowavesForm:
                    window = new Views.E1MicrowavesForm(requestID);
                    break;
                case (int)DB.Form.ADSLSupport:
                    window = new Views.ADSLSupportForm(requestID);
                    break;
                case (int)DB.Form.CenterToCenterRequestFormTranslation:
                    window = new Views.CenterToCenterTranslationForm(requestID);
                    break;
                case (int)DB.Form.CenterToCenterTranslationChooseNumberForm:
                    window = new Views.CenterToCenterTranslationChooseNumberForm(requestID);
                    break;
                case (int)DB.Form.CenterToCenterTranslationSwitchForm:
                    window = new Views.CenterToCenterTranslationSwitchForm(requestID);
                    break;
                case (int)DB.Form.CenterToCenterTranslationMDFForm:
                    window = new Views.CenterToCenterTranslationMDFForm(requestID);
                    break;
                case (int)DB.Form.CenterToCenterTranslatioNetworkForm:
                    window = new Views.CenterToCenterTranslatioNetworkForm(requestID);
                    break;
                case (int)DB.Form.TranslationPostForm:
                    window = new Views.TranslationPostForm(requestID);
                    break;
                case (int)DB.Form.TranslationPostNetworkForm:
                    window = new Views.TranslationPostNetworkForm(requestID);
                    break;
                case (int)DB.Form.TranslationPostInvestigatePossibilityForm:
                    window = new Views.TranslationPostInvertigateForm(requestID);
                    break;
                case (int)DB.Form.TranslationCabinetMDFFrom:
                    window = new Views.ExchangeCabinetInputMDFForm(requestID);
                    break;
                case (int)DB.Form.TranslationCabinetNetworkFrom:
                    window = new Views.ExchangeCabinetInputNetworkForm(requestID);
                    break;
                case (int)DB.Form.TranslationCabinetInvestigateFrom:
                    window = new Views.TranslationCabinetInvestigateFrom(requestID);
                    break;
                case (int)DB.Form.TranslationCentralCableMDFFrom:
                    window = new Views.TranslationCentralCableMDFForm(requestID);
                    break;
                case (int)DB.Form.TranslationCentralCableMDFForMDFFrom:
                    window = new Views.TranslationCentralCableMDFForMDFForm(requestID);
                    break;
                case (int)DB.Form.TranslationCentralCableMDFNetworkFrom:
                    window = new Views.TranslationCentralCableMDFNetworkForm(requestID);
                    break;
                case (int)DB.Form.TranslationCentralCableMDFInvestigateFrom:
                    window = new Views.TranslationCentralCableMDFInvestigateForm(requestID);
                    break;
                case (int)DB.Form.TranslationPostInputFom:
                    window = new Views.TranslationPostInputFom(requestID);
                    break;
                case (int)DB.Form.TranslationPostInputNetworkForm:
                    window = new Views.TranslationPostInputNetworkForm(requestID);
                    break;
                case (int)DB.Form.TranslationPostInputMDFForm:
                    window = new Views.TranslationPostInputMDFForm(requestID);
                    break;
                case (int)DB.Form.TranslationPostInputInvestigateForm:
                    window = new Views.TranslationPostInputInvestigateForm(requestID);
                    break;
                case (int)DB.Form.BuchtSwitchingForm:
                    window = new Views.BuchtSwitchingForm(requestID);
                    break;
                case (int)DB.Form.BuchtSwitchingMDFForm:
                    window = new Views.BuchtSwitchingMDFForm(requestID);
                    break;
                case (int)DB.Form.BuchtSwitchingNetworkFrom:
                    window = new Views.BuchtSwitchingNetworkFrom(requestID);
                    break;
                case (int)DB.Form.TranslationOpticalToNormalForm:
                    window = new Views.TranslationOpticalCabinetToNormalForm(requestID);
                    break;
                case (int)DB.Form.TranslationOpticalToNormalChooseNumberForm:
                    window = new Views.TranslationOpticalToNormalChooseNumberForm(requestID);
                    break;
                case (int)DB.Form.TranslationOpticalToNormalSwitchForm:
                    window = new Views.TranslationOpticalToNormalSwitchForm(requestID);
                    break;
                case (int)DB.Form.TranslationOpticalToNormalMDFForm:
                    window = new Views.TranslationOpticalToNormalMDFForm(requestID);
                    break;
                case (int)DB.Form.TranslationOpticalToNormalNetworkForm:
                    window = new Views.TranslationOpticalToNormalNetworkForm(requestID);
                    break;
                case (int)DB.Form.TranslationPCMToNormalForm:
                    window = new Views.TranslationPCMToNormalForm(requestID);
                    break;
                case (int)DB.Form.TranslationPCMToNormalMDFForm:
                    window = new Views.TranslationPCMToNormalMDFForm(requestID);
                    break;
                case (int)DB.Form.TranslationPCMToNormalNetworkForm:
                    window = new Views.TranslationPCMToNormalNetworkForm(requestID);
                    break;
                case (int)DB.Form.RequestRefundForm:
                    window = new Views.RequestRefundForm(requestID);
                    break;

                case (int)DB.Form.SwapTelephoneMDFForm:
                    window = new Views.SwapTelephoneMDFForm(requestID);
                    break;
                case (int)DB.Form.SwapTelephoneForm:
                    window = new Views.SwapTelephoneForm(requestID);
                    break;
                case (int)DB.Form.SwapPCMForm:
                    window = new Views.SwapPCMFrom(requestID);
                    break;
                case (int)DB.Form.SwapPCMMDFForm:
                    window = new Views.SwapPCMMDFFrom(requestID);
                    break;
                case (int)DB.Form.SwapPCMNetworkForm:
                    window = new Views.SwapPCMNetworkFrom(requestID);
                    break;
                case (int)DB.Form.DesignDirectorForm:
                    window = new Views.DesignDirectorForm(requestID, subID);
                    break;
                case (int)DB.Form.TransferDepartmentForm:
                    window = new Views.TransferDepartmentForm(requestID, subID);
                    break;
                case (int)DB.Form.PowerDepartmentForm:
                    window = new Views.PowerDepartmentForm(requestID, subID);
                    break;
                case (int)DB.Form.InstallingDepartmentForm:
                    window = new Views.InstallingDepartmentForm(requestID, subID);
                    break;
                case (int)DB.Form.MonitoringDepartmentForm:
                    window = new Views.MonitoringDepartmentForm(requestID, subID);
                    break;

                case (int)DB.Form.ExchangePCMCardFrom:
                    window = new Views.ExchangePCMCardFrom(requestID);
                    break;

                case (int)DB.Form.ExchangePCMCardMDFFrom:
                    window = new Views.ExchangePCMCardMDFForm(requestID);
                    break;




                case (int)DB.Form.ExchangeGSMForm:
                    window = new Views.ExchangeGSMForm(requestID);
                    break;

                case (int)DB.Form.ExchangeGSMChooseNumberFrom:
                    window = new Views.ExchangeGSMChooseNumberFrom(requestID);
                    break;

                case (int)DB.Form.ExchangeGSMCounterForm:
                    window = new Views.ExchangeGSMCounterForm(requestID);
                    break;

                case (int)DB.Form.ExchangeGSMMDFForm:
                    window = new Views.ExchangeGSMMDFForm(requestID);
                    break;

                case (int)DB.Form.ExchangeGSMNetworkForm:
                    window = new Views.ExchangeGSMNetworkForm(requestID);
                    break;
                case (int)DB.Form.SwitchDesigningOfficeForm:
                    window = new Views.SwitchDesigningOfficeForm(requestID);
                    break;
                default:
                    throw new ApplicationException("فرم مرتبط در چرخه کاری تعریف نشده است");
            }

            window.currentStat = requestCurrentStateID;
            window.currentStep = requestCurrentStepID;

            return window;
        }
    }
}