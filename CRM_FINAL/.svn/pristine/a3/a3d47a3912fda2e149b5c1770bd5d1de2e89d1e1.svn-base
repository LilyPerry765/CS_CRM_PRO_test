using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace CRM.Data
{

	public partial class ActionLog
	{
		public void Detach()
		{

			  
		}
	}

	public partial class AdameEmkanat
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class Address
	{
		public void Detach()
		{

			this._BlackLists = new EntitySet<BlackList>(new Action<BlackList>(this.attach_BlackLists), new Action<BlackList>(this.detach_BlackLists));
			this._ChangeAddresses = new EntitySet<ChangeAddress>(new Action<ChangeAddress>(this.attach_ChangeAddresses), new Action<ChangeAddress>(this.detach_ChangeAddresses));
			this._ChangeAddresses1 = new EntitySet<ChangeAddress>(new Action<ChangeAddress>(this.attach_ChangeAddresses1), new Action<ChangeAddress>(this.detach_ChangeAddresses1));
			this._ChangeAddresses2 = new EntitySet<ChangeAddress>(new Action<ChangeAddress>(this.attach_ChangeAddresses2), new Action<ChangeAddress>(this.detach_ChangeAddresses2));
			this._ChangeAddresses3 = new EntitySet<ChangeAddress>(new Action<ChangeAddress>(this.attach_ChangeAddresses3), new Action<ChangeAddress>(this.detach_ChangeAddresses3));
			this._ChangeLocations = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations), new Action<ChangeLocation>(this.detach_ChangeLocations));
			this._ChangeLocations1 = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations1), new Action<ChangeLocation>(this.detach_ChangeLocations1));
			this._ChangeLocations2 = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations2), new Action<ChangeLocation>(this.detach_ChangeLocations2));
			this._ChangeLocations3 = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations3), new Action<ChangeLocation>(this.detach_ChangeLocations3));
			this._ChangeLocationSpecialWires = new EntitySet<ChangeLocationSpecialWire>(new Action<ChangeLocationSpecialWire>(this.attach_ChangeLocationSpecialWires), new Action<ChangeLocationSpecialWire>(this.detach_ChangeLocationSpecialWires));
			this._ChangeLocationSpecialWires1 = new EntitySet<ChangeLocationSpecialWire>(new Action<ChangeLocationSpecialWire>(this.attach_ChangeLocationSpecialWires1), new Action<ChangeLocationSpecialWire>(this.detach_ChangeLocationSpecialWires1));
			this._ChangeNos = new EntitySet<ChangeNo>(new Action<ChangeNo>(this.attach_ChangeNos), new Action<ChangeNo>(this.detach_ChangeNos));
			this._ChangeNos1 = new EntitySet<ChangeNo>(new Action<ChangeNo>(this.attach_ChangeNos1), new Action<ChangeNo>(this.detach_ChangeNos1));
			this._E1s = new EntitySet<E1>(new Action<E1>(this.attach_E1s), new Action<E1>(this.detach_E1s));
			this._E1s1 = new EntitySet<E1>(new Action<E1>(this.attach_E1s1), new Action<E1>(this.detach_E1s1));
			this._E1s2 = new EntitySet<E1>(new Action<E1>(this.attach_E1s2), new Action<E1>(this.detach_E1s2));
			this._ExchangeCabinetInputConncetions = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions));
			this._ExchangeCabinetInputConncetions1 = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions1), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions1));
			this._InvestigatePossibilityWaitinglists = new EntitySet<InvestigatePossibilityWaitinglist>(new Action<InvestigatePossibilityWaitinglist>(this.attach_InvestigatePossibilityWaitinglists), new Action<InvestigatePossibilityWaitinglist>(this.detach_InvestigatePossibilityWaitinglists));
			this._RefundDeposits = new EntitySet<RefundDeposit>(new Action<RefundDeposit>(this.attach_RefundDeposits), new Action<RefundDeposit>(this.detach_RefundDeposits));
			this._RefundDeposits1 = new EntitySet<RefundDeposit>(new Action<RefundDeposit>(this.attach_RefundDeposits1), new Action<RefundDeposit>(this.detach_RefundDeposits1));
			this._SpaceAndPowers = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers), new Action<SpaceAndPower>(this.detach_SpaceAndPowers));
			this._SpecialWires = new EntitySet<SpecialWire>(new Action<SpecialWire>(this.attach_SpecialWires), new Action<SpecialWire>(this.detach_SpecialWires));
			this._SpecialWires1 = new EntitySet<SpecialWire>(new Action<SpecialWire>(this.attach_SpecialWires1), new Action<SpecialWire>(this.detach_SpecialWires1));
			this._SpecialWireAddresses = new EntitySet<SpecialWireAddress>(new Action<SpecialWireAddress>(this.attach_SpecialWireAddresses), new Action<SpecialWireAddress>(this.detach_SpecialWireAddresses));
			this._SpecialWireAddresses1 = new EntitySet<SpecialWireAddress>(new Action<SpecialWireAddress>(this.attach_SpecialWireAddresses1), new Action<SpecialWireAddress>(this.detach_SpecialWireAddresses1));
			this._SpecialWirePoints = new EntitySet<SpecialWirePoint>(new Action<SpecialWirePoint>(this.attach_SpecialWirePoints), new Action<SpecialWirePoint>(this.detach_SpecialWirePoints));
			this._TakePossessions = new EntitySet<TakePossession>(new Action<TakePossession>(this.attach_TakePossessions), new Action<TakePossession>(this.detach_TakePossessions));
			this._TakePossessions1 = new EntitySet<TakePossession>(new Action<TakePossession>(this.attach_TakePossessions1), new Action<TakePossession>(this.detach_TakePossessions1));
			this._Telephones = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones), new Action<Telephone>(this.detach_Telephones));
			this._Telephones1 = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones1), new Action<Telephone>(this.detach_Telephones1));
			this._TranslationOpticalCabinetToNormalConncetions = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions));
			this._TranslationOpticalCabinetToNormalConncetions1 = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions1), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions1));
			this._VacateSpecialWires = new EntitySet<VacateSpecialWire>(new Action<VacateSpecialWire>(this.attach_VacateSpecialWires), new Action<VacateSpecialWire>(this.detach_VacateSpecialWires));
			this._VacateSpecialWires1 = new EntitySet<VacateSpecialWire>(new Action<VacateSpecialWire>(this.attach_VacateSpecialWires1), new Action<VacateSpecialWire>(this.detach_VacateSpecialWires1));
			this._VisitAddresses = new EntitySet<VisitAddress>(new Action<VisitAddress>(this.attach_VisitAddresses), new Action<VisitAddress>(this.detach_VisitAddresses));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			this._InstallRequests = new EntitySet<InstallRequest>(new Action<InstallRequest>(this.attach_InstallRequests), new Action<InstallRequest>(this.detach_InstallRequests));
			this._InstallRequests1 = new EntitySet<InstallRequest>(new Action<InstallRequest>(this.attach_InstallRequests1), new Action<InstallRequest>(this.detach_InstallRequests1));
			this._Customers = new EntitySet<Customer>(new Action<Customer>(this.attach_Customers), new Action<Customer>(this.detach_Customers));
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class AdjacentPost
	{
		public void Detach()
		{

			this._Post = default(EntityRef<Post>);
			this._Post1 = default(EntityRef<Post>);
			  
		}
	}

	public partial class ADSL
	{
		public void Detach()
		{

			this._ADSLCustomerGroup = default(EntityRef<ADSLCustomerGroup>);
			this._ADSLCustomerType = default(EntityRef<ADSLCustomerType>);
			this._ADSLGroupIP = default(EntityRef<ADSLGroupIP>);
			this._ADSLIP = default(EntityRef<ADSLIP>);
			this._ADSLModemProperty = default(EntityRef<ADSLModemProperty>);
			this._ADSLPort = default(EntityRef<ADSLPort>);
			this._ADSLService = default(EntityRef<ADSLService>);
			this._PAPInfo = default(EntityRef<PAPInfo>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class ADSL1
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ADSLAAAActionLog
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ADSLAAAType
	{
		public void Detach()
		{

			this._ADSLEquipments = new EntitySet<ADSLEquipment>(new Action<ADSLEquipment>(this.attach_ADSLEquipments), new Action<ADSLEquipment>(this.detach_ADSLEquipments));
			  
		}
	}

	public partial class ADSLAgentLog
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ADSLChangeCustomerOwnerCharacteristic
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Customer = default(EntityRef<Customer>);
			this._Customer1 = default(EntityRef<Customer>);
			  
		}
	}

	public partial class ADSLChangeIPRequest
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ADSLChangePlace
	{
		public void Detach()
		{

			this._ADSLPort = default(EntityRef<ADSLPort>);
			this._ADSLPort1 = default(EntityRef<ADSLPort>);
			this._Center = default(EntityRef<Center>);
			this._Center1 = default(EntityRef<Center>);
			this._Request = default(EntityRef<Request>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class ADSLChangePort1
	{
		public void Detach()
		{

			this._ADSLChangePortReason = default(EntityRef<ADSLChangePortReason>);
			this._ADSLPort = default(EntityRef<ADSLPort>);
			this._ADSLPort1 = default(EntityRef<ADSLPort>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ADSLChangePortReason
	{
		public void Detach()
		{

			this._ADSLChangePort1s = new EntitySet<ADSLChangePort1>(new Action<ADSLChangePort1>(this.attach_ADSLChangePort1s), new Action<ADSLChangePort1>(this.detach_ADSLChangePort1s));
			  
		}
	}

	public partial class ADSLChangeService
	{
		public void Detach()
		{

			this._ADSLModem = default(EntityRef<ADSLModem>);
			this._ADSLService = default(EntityRef<ADSLService>);
			this._ADSLService1 = default(EntityRef<ADSLService>);
			this._Request = default(EntityRef<Request>);
			this._User = default(EntityRef<User>);
			this._User1 = default(EntityRef<User>);
			  
		}
	}

	public partial class ADSLCustomerGroup
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._ADSLGroupIPs = new EntitySet<ADSLGroupIP>(new Action<ADSLGroupIP>(this.attach_ADSLGroupIPs), new Action<ADSLGroupIP>(this.detach_ADSLGroupIPs));
			this._ADSLIPs = new EntitySet<ADSLIP>(new Action<ADSLIP>(this.attach_ADSLIPs), new Action<ADSLIP>(this.detach_ADSLIPs));
			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._ADSLServiceGroups = new EntitySet<ADSLServiceGroup>(new Action<ADSLServiceGroup>(this.attach_ADSLServiceGroups), new Action<ADSLServiceGroup>(this.detach_ADSLServiceGroups));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			  
		}
	}

	public partial class ADSLCustomerType
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._Wirelesses = new EntitySet<Wireless>(new Action<Wireless>(this.attach_Wirelesses), new Action<Wireless>(this.detach_Wirelesses));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			  
		}
	}

	public partial class ADSLCutTemporary
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ADSLDischarge
	{
		public void Detach()
		{

			this._ADSLDischargeReason = default(EntityRef<ADSLDischargeReason>);
			this._ADSLService = default(EntityRef<ADSLService>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ADSLDischargeReason
	{
		public void Detach()
		{

			this._ADSLDischarges = new EntitySet<ADSLDischarge>(new Action<ADSLDischarge>(this.attach_ADSLDischarges), new Action<ADSLDischarge>(this.detach_ADSLDischarges));
			  
		}
	}

	public partial class ADSLEquipment
	{
		public void Detach()
		{

			this._ADSLPorts = new EntitySet<ADSLPort>(new Action<ADSLPort>(this.attach_ADSLPorts), new Action<ADSLPort>(this.detach_ADSLPorts));
			this._ADSLAAAType = default(EntityRef<ADSLAAAType>);
			this._ADSLPortType = default(EntityRef<ADSLPortType>);
			this._Center = default(EntityRef<Center>);
			this._Shelf = default(EntityRef<Shelf>);
			  
		}
	}

	public partial class ADSLGroupIP
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._ADSLIPHistories = new EntitySet<ADSLIPHistory>(new Action<ADSLIPHistory>(this.attach_ADSLIPHistories), new Action<ADSLIPHistory>(this.detach_ADSLIPHistories));
			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._Wirelesses = new EntitySet<Wireless>(new Action<Wireless>(this.attach_Wirelesses), new Action<Wireless>(this.detach_Wirelesses));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			this._ADSLCustomerGroup = default(EntityRef<ADSLCustomerGroup>);
			this._ADSLIPType = default(EntityRef<ADSLIPType>);
			  
		}
	}

	public partial class ADSLHistory
	{
		public void Detach()
		{

			this._ADSLService = default(EntityRef<ADSLService>);
			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class ADSLInstalCostCenter
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class ADSLInstallRequest
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ADSLIntroductionWay
	{
		public void Detach()
		{

		}
	}

	public partial class ADSLIP
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._ADSLIPHistories = new EntitySet<ADSLIPHistory>(new Action<ADSLIPHistory>(this.attach_ADSLIPHistories), new Action<ADSLIPHistory>(this.detach_ADSLIPHistories));
			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._Wirelesses = new EntitySet<Wireless>(new Action<Wireless>(this.attach_Wirelesses), new Action<Wireless>(this.detach_Wirelesses));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			this._ADSLCustomerGroup = default(EntityRef<ADSLCustomerGroup>);
			this._ADSLIPType = default(EntityRef<ADSLIPType>);
			  
		}
	}

	public partial class ADSLIPHistory
	{
		public void Detach()
		{

			this._ADSLGroupIP = default(EntityRef<ADSLGroupIP>);
			this._ADSLIP = default(EntityRef<ADSLIP>);
			  
		}
	}

	public partial class ADSLIPType
	{
		public void Detach()
		{

			this._ADSLGroupIPs = new EntitySet<ADSLGroupIP>(new Action<ADSLGroupIP>(this.attach_ADSLGroupIPs), new Action<ADSLGroupIP>(this.detach_ADSLGroupIPs));
			this._ADSLIPs = new EntitySet<ADSLIP>(new Action<ADSLIP>(this.attach_ADSLIPs), new Action<ADSLIP>(this.detach_ADSLIPs));
			  
		}
	}

	public partial class ADSLIRANRequest
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ADSLMDFRange
	{
		public void Detach()
		{

			this._MDF = default(EntityRef<MDF>);
			  
		}
	}

	public partial class ADSLModem
	{
		public void Detach()
		{

			this._ADSLChangeServices = new EntitySet<ADSLChangeService>(new Action<ADSLChangeService>(this.attach_ADSLChangeServices), new Action<ADSLChangeService>(this.detach_ADSLChangeServices));
			this._ADSLModem2 = default(EntityRef<ADSLModem>);
			this._ADSLModemProperties = new EntitySet<ADSLModemProperty>(new Action<ADSLModemProperty>(this.attach_ADSLModemProperties), new Action<ADSLModemProperty>(this.detach_ADSLModemProperties));
			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			this._ADSLModem1 = default(EntityRef<ADSLModem>);
			  
		}
	}

	public partial class ADSLModemProperty
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._Wirelesses = new EntitySet<Wireless>(new Action<Wireless>(this.attach_Wirelesses), new Action<Wireless>(this.detach_Wirelesses));
			this._ADSLModem = default(EntityRef<ADSLModem>);
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class ADSLPAPCabinetAccuracy
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class ADSLPAPFeasibility
	{
		public void Detach()
		{

			this._City = default(EntityRef<City>);
			this._PAPInfo = default(EntityRef<PAPInfo>);
			  
		}
	}

	public partial class ADSLPAPPort
	{
		public void Detach()
		{

			this._ADSLPAPRequests = new EntitySet<ADSLPAPRequest>(new Action<ADSLPAPRequest>(this.attach_ADSLPAPRequests), new Action<ADSLPAPRequest>(this.detach_ADSLPAPRequests));
			this._Center = default(EntityRef<Center>);
			this._PAPInfo = default(EntityRef<PAPInfo>);
			  
		}
	}

	public partial class ADSLPAPRequest
	{
		public void Detach()
		{

			this._ADSLPAPPort = default(EntityRef<ADSLPAPPort>);
			this._PAPInfo = default(EntityRef<PAPInfo>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ADSLPort
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._ADSLChangePlaces = new EntitySet<ADSLChangePlace>(new Action<ADSLChangePlace>(this.attach_ADSLChangePlaces), new Action<ADSLChangePlace>(this.detach_ADSLChangePlaces));
			this._ADSLChangePlaces1 = new EntitySet<ADSLChangePlace>(new Action<ADSLChangePlace>(this.attach_ADSLChangePlaces1), new Action<ADSLChangePlace>(this.detach_ADSLChangePlaces1));
			this._ADSLChangePort1s = new EntitySet<ADSLChangePort1>(new Action<ADSLChangePort1>(this.attach_ADSLChangePort1s), new Action<ADSLChangePort1>(this.detach_ADSLChangePort1s));
			this._ADSLChangePort1s1 = new EntitySet<ADSLChangePort1>(new Action<ADSLChangePort1>(this.attach_ADSLChangePort1s1), new Action<ADSLChangePort1>(this.detach_ADSLChangePort1s1));
			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			this._ADSLEquipment = default(EntityRef<ADSLEquipment>);
			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			  
		}
	}

	public partial class ADSLPortType
	{
		public void Detach()
		{

			this._ADSLEquipments = new EntitySet<ADSLEquipment>(new Action<ADSLEquipment>(this.attach_ADSLEquipments), new Action<ADSLEquipment>(this.detach_ADSLEquipments));
			  
		}
	}

	public partial class ADSLRequest
	{
		public void Detach()
		{

			this._ADSLRequest2 = default(EntityRef<ADSLRequest>);
			this._ADSLCustomerGroup = default(EntityRef<ADSLCustomerGroup>);
			this._ADSLCustomerType = default(EntityRef<ADSLCustomerType>);
			this._ADSLGroupIP = default(EntityRef<ADSLGroupIP>);
			this._ADSLIP = default(EntityRef<ADSLIP>);
			this._ADSLModem = default(EntityRef<ADSLModem>);
			this._ADSLPort = default(EntityRef<ADSLPort>);
			this._ADSLRequest1 = default(EntityRef<ADSLRequest>);
			this._ADSLSellerAgent = default(EntityRef<ADSLSellerAgent>);
			this._ADSLService = default(EntityRef<ADSLService>);
			this._Contractor = default(EntityRef<Contractor>);
			this._Request = default(EntityRef<Request>);
			this._User = default(EntityRef<User>);
			this._User1 = default(EntityRef<User>);
			this._User2 = default(EntityRef<User>);
			this._User3 = default(EntityRef<User>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class ADSLSellerAgent
	{
		public void Detach()
		{

			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._ADSLSellerAgentRecharges = new EntitySet<ADSLSellerAgentRecharge>(new Action<ADSLSellerAgentRecharge>(this.attach_ADSLSellerAgentRecharges), new Action<ADSLSellerAgentRecharge>(this.detach_ADSLSellerAgentRecharges));
			this._ADSLSellerAgentUsers = new EntitySet<ADSLSellerAgentUser>(new Action<ADSLSellerAgentUser>(this.attach_ADSLSellerAgentUsers), new Action<ADSLSellerAgentUser>(this.detach_ADSLSellerAgentUsers));
			this._ADSLServiceGroupSellers = new EntitySet<ADSLServiceGroupSeller>(new Action<ADSLServiceGroupSeller>(this.attach_ADSLServiceGroupSellers), new Action<ADSLServiceGroupSeller>(this.detach_ADSLServiceGroupSellers));
			this._ADSLServiceSellers = new EntitySet<ADSLServiceSeller>(new Action<ADSLServiceSeller>(this.attach_ADSLServiceSellers), new Action<ADSLServiceSeller>(this.detach_ADSLServiceSellers));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			this._ADSLSellerGroup = default(EntityRef<ADSLSellerGroup>);
			this._City = default(EntityRef<City>);
			  
		}
	}

	public partial class ADSLSellerAgentRecharge
	{
		public void Detach()
		{

			this._ADSLSellerAgent = default(EntityRef<ADSLSellerAgent>);
			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class ADSLSellerAgentUser
	{
		public void Detach()
		{

			this._ADSLSellerAgentUserCredits = new EntitySet<ADSLSellerAgentUserCredit>(new Action<ADSLSellerAgentUserCredit>(this.attach_ADSLSellerAgentUserCredits), new Action<ADSLSellerAgentUserCredit>(this.detach_ADSLSellerAgentUserCredits));
			this._ADSLSellerAgentUserRecharges = new EntitySet<ADSLSellerAgentUserRecharge>(new Action<ADSLSellerAgentUserRecharge>(this.attach_ADSLSellerAgentUserRecharges), new Action<ADSLSellerAgentUserRecharge>(this.detach_ADSLSellerAgentUserRecharges));
			this._ADSLSellerAgent = default(EntityRef<ADSLSellerAgent>);
			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class ADSLSellerAgentUserCredit
	{
		public void Detach()
		{

			this._ADSLSellerAgentUser = default(EntityRef<ADSLSellerAgentUser>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ADSLSellerAgentUserRecharge
	{
		public void Detach()
		{

			this._ADSLSellerAgentUser = default(EntityRef<ADSLSellerAgentUser>);
			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class ADSLSellerGroup
	{
		public void Detach()
		{

			this._ADSLSellerAgents = new EntitySet<ADSLSellerAgent>(new Action<ADSLSellerAgent>(this.attach_ADSLSellerAgents), new Action<ADSLSellerAgent>(this.detach_ADSLSellerAgents));
			  
		}
	}

	public partial class ADSLSellTraffic
	{
		public void Detach()
		{

			this._ADSLService = default(EntityRef<ADSLService>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ADSLService
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._ADSLChangeServices = new EntitySet<ADSLChangeService>(new Action<ADSLChangeService>(this.attach_ADSLChangeServices), new Action<ADSLChangeService>(this.detach_ADSLChangeServices));
			this._ADSLChangeServices1 = new EntitySet<ADSLChangeService>(new Action<ADSLChangeService>(this.attach_ADSLChangeServices1), new Action<ADSLChangeService>(this.detach_ADSLChangeServices1));
			this._ADSLDischarges = new EntitySet<ADSLDischarge>(new Action<ADSLDischarge>(this.attach_ADSLDischarges), new Action<ADSLDischarge>(this.detach_ADSLDischarges));
			this._ADSLHistories = new EntitySet<ADSLHistory>(new Action<ADSLHistory>(this.attach_ADSLHistories), new Action<ADSLHistory>(this.detach_ADSLHistories));
			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._ADSLSellTraffics = new EntitySet<ADSLSellTraffic>(new Action<ADSLSellTraffic>(this.attach_ADSLSellTraffics), new Action<ADSLSellTraffic>(this.detach_ADSLSellTraffics));
			this._ADSLService2 = default(EntityRef<ADSLService>);
			this._ADSLServiceCenters = new EntitySet<ADSLServiceCenter>(new Action<ADSLServiceCenter>(this.attach_ADSLServiceCenters), new Action<ADSLServiceCenter>(this.detach_ADSLServiceCenters));
			this._ADSLServiceSellers = new EntitySet<ADSLServiceSeller>(new Action<ADSLServiceSeller>(this.attach_ADSLServiceSellers), new Action<ADSLServiceSeller>(this.detach_ADSLServiceSellers));
			this._Wirelesses = new EntitySet<Wireless>(new Action<Wireless>(this.attach_Wirelesses), new Action<Wireless>(this.detach_Wirelesses));
			this._WirelessChangeServices = new EntitySet<WirelessChangeService>(new Action<WirelessChangeService>(this.attach_WirelessChangeServices), new Action<WirelessChangeService>(this.detach_WirelessChangeServices));
			this._WirelessChangeServices1 = new EntitySet<WirelessChangeService>(new Action<WirelessChangeService>(this.attach_WirelessChangeServices1), new Action<WirelessChangeService>(this.detach_WirelessChangeServices1));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			this._WirelessSellTraffics = new EntitySet<WirelessSellTraffic>(new Action<WirelessSellTraffic>(this.attach_WirelessSellTraffics), new Action<WirelessSellTraffic>(this.detach_WirelessSellTraffics));
			this._ADSLService1 = default(EntityRef<ADSLService>);
			this._ADSLServiceBandWidth = default(EntityRef<ADSLServiceBandWidth>);
			this._ADSLServiceDuration = default(EntityRef<ADSLServiceDuration>);
			this._ADSLServiceGiftProfile = default(EntityRef<ADSLServiceGiftProfile>);
			this._ADSLServiceGroup = default(EntityRef<ADSLServiceGroup>);
			this._ADSLServiceNetwork = default(EntityRef<ADSLServiceNetwork>);
			this._ADSLServiceTraffic = default(EntityRef<ADSLServiceTraffic>);
			  
		}
	}

	public partial class ADSLService1
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ADSLServiceBandWidth
	{
		public void Detach()
		{

			this._ADSLServices = new EntitySet<ADSLService>(new Action<ADSLService>(this.attach_ADSLServices), new Action<ADSLService>(this.detach_ADSLServices));
			  
		}
	}

	public partial class ADSLServiceCenter
	{
		public void Detach()
		{

			this._ADSLService = default(EntityRef<ADSLService>);
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class ADSLServiceDuration
	{
		public void Detach()
		{

			this._ADSLServices = new EntitySet<ADSLService>(new Action<ADSLService>(this.attach_ADSLServices), new Action<ADSLService>(this.detach_ADSLServices));
			  
		}
	}

	public partial class ADSLServiceGiftProfile
	{
		public void Detach()
		{

			this._ADSLServices = new EntitySet<ADSLService>(new Action<ADSLService>(this.attach_ADSLServices), new Action<ADSLService>(this.detach_ADSLServices));
			  
		}
	}

	public partial class ADSLServiceGroup
	{
		public void Detach()
		{

			this._ADSLServices = new EntitySet<ADSLService>(new Action<ADSLService>(this.attach_ADSLServices), new Action<ADSLService>(this.detach_ADSLServices));
			this._ADSLServiceGroupCenters = new EntitySet<ADSLServiceGroupCenter>(new Action<ADSLServiceGroupCenter>(this.attach_ADSLServiceGroupCenters), new Action<ADSLServiceGroupCenter>(this.detach_ADSLServiceGroupCenters));
			this._ADSLServiceGroupSellers = new EntitySet<ADSLServiceGroupSeller>(new Action<ADSLServiceGroupSeller>(this.attach_ADSLServiceGroupSellers), new Action<ADSLServiceGroupSeller>(this.detach_ADSLServiceGroupSellers));
			this._ADSLCustomerGroup = default(EntityRef<ADSLCustomerGroup>);
			  
		}
	}

	public partial class ADSLServiceGroupCenter
	{
		public void Detach()
		{

			this._ADSLServiceGroup = default(EntityRef<ADSLServiceGroup>);
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class ADSLServiceGroupSeller
	{
		public void Detach()
		{

			this._ADSLSellerAgent = default(EntityRef<ADSLSellerAgent>);
			this._ADSLServiceGroup = default(EntityRef<ADSLServiceGroup>);
			  
		}
	}

	public partial class ADSLServiceNetwork
	{
		public void Detach()
		{

			this._ADSLServices = new EntitySet<ADSLService>(new Action<ADSLService>(this.attach_ADSLServices), new Action<ADSLService>(this.detach_ADSLServices));
			  
		}
	}

	public partial class ADSLServiceSeller
	{
		public void Detach()
		{

			this._ADSLSellerAgent = default(EntityRef<ADSLSellerAgent>);
			this._ADSLService = default(EntityRef<ADSLService>);
			  
		}
	}

	public partial class ADSLServiceTraffic
	{
		public void Detach()
		{

			this._ADSLServices = new EntitySet<ADSLService>(new Action<ADSLService>(this.attach_ADSLServices), new Action<ADSLService>(this.detach_ADSLServices));
			  
		}
	}

	public partial class ADSLSetupContactInformation
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class ADSLSupportCommnet
	{
		public void Detach()
		{

			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class ADSLSupportRequest
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ADSLTelephoneAccuracy
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class ADSLTelephoneNoHistory
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			this._PAPInfo = default(EntityRef<PAPInfo>);
			  
		}
	}

	public partial class ADSLTrafficBaseCost
	{
		public void Detach()
		{

			  
		}
	}

	public partial class Announce
	{
		public void Detach()
		{

			this._DocumentRequestTypes = new EntitySet<DocumentRequestType>(new Action<DocumentRequestType>(this.attach_DocumentRequestTypes), new Action<DocumentRequestType>(this.detach_DocumentRequestTypes));
			this._QuotaDiscounts = new EntitySet<QuotaDiscount>(new Action<QuotaDiscount>(this.attach_QuotaDiscounts), new Action<QuotaDiscount>(this.detach_QuotaDiscounts));
			  
		}
	}

	public partial class AnnounceTo118
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class AORBPostAndCabinet
	{
		public void Detach()
		{

			this._Cabinets = new EntitySet<Cabinet>(new Action<Cabinet>(this.attach_Cabinets), new Action<Cabinet>(this.detach_Cabinets));
			this._Posts = new EntitySet<Post>(new Action<Post>(this.attach_Posts), new Action<Post>(this.detach_Posts));
			  
		}
	}

	public partial class Bank
	{
		public void Detach()
		{

			this._BankBranches = new EntitySet<BankBranch>(new Action<BankBranch>(this.attach_BankBranches), new Action<BankBranch>(this.detach_BankBranches));
			this._RequestPayments = new EntitySet<RequestPayment>(new Action<RequestPayment>(this.attach_RequestPayments), new Action<RequestPayment>(this.detach_RequestPayments));
			  
		}
	}

	public partial class BankBranch
	{
		public void Detach()
		{

			this._RequestPayments = new EntitySet<RequestPayment>(new Action<RequestPayment>(this.attach_RequestPayments), new Action<RequestPayment>(this.detach_RequestPayments));
			this._Bank = default(EntityRef<Bank>);
			  
		}
	}

	public partial class BaseCost
	{
		public void Detach()
		{

			this._Installments = new EntitySet<Installment>(new Action<Installment>(this.attach_Installments), new Action<Installment>(this.detach_Installments));
			this._RequestPayments = new EntitySet<RequestPayment>(new Action<RequestPayment>(this.attach_RequestPayments), new Action<RequestPayment>(this.detach_RequestPayments));
			this._QuotaDiscount = default(EntityRef<QuotaDiscount>);
			this._RequestType = default(EntityRef<RequestType>);
			  
		}
	}

	public partial class BlackList
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._BlackListReason = default(EntityRef<BlackListReason>);
			this._Telephone = default(EntityRef<Telephone>);
			this._User = default(EntityRef<User>);
			this._User1 = default(EntityRef<User>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class BlackListReason
	{
		public void Detach()
		{

			this._BlackLists = new EntitySet<BlackList>(new Action<BlackList>(this.attach_BlackLists), new Action<BlackList>(this.detach_BlackLists));
			  
		}
	}

	public partial class Bucht
	{
		public void Detach()
		{

			this._ADSLPorts = new EntitySet<ADSLPort>(new Action<ADSLPort>(this.attach_ADSLPorts), new Action<ADSLPort>(this.detach_ADSLPorts));
			this._ADSLPorts1 = new EntitySet<ADSLPort>(new Action<ADSLPort>(this.attach_ADSLPorts1), new Action<ADSLPort>(this.detach_ADSLPorts1));
			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._Buchts1 = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts1), new Action<Bucht>(this.detach_Buchts1));
			this._BuchtSwitchings = new EntitySet<BuchtSwitching>(new Action<BuchtSwitching>(this.attach_BuchtSwitchings), new Action<BuchtSwitching>(this.detach_BuchtSwitchings));
			this._BuchtSwitchings1 = new EntitySet<BuchtSwitching>(new Action<BuchtSwitching>(this.attach_BuchtSwitchings1), new Action<BuchtSwitching>(this.detach_BuchtSwitchings1));
			this._BuchtSwitchings2 = new EntitySet<BuchtSwitching>(new Action<BuchtSwitching>(this.attach_BuchtSwitchings2), new Action<BuchtSwitching>(this.detach_BuchtSwitchings2));
			this._ChangeLocations = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations), new Action<ChangeLocation>(this.detach_ChangeLocations));
			this._ChangeLocationSpecialWires = new EntitySet<ChangeLocationSpecialWire>(new Action<ChangeLocationSpecialWire>(this.attach_ChangeLocationSpecialWires), new Action<ChangeLocationSpecialWire>(this.detach_ChangeLocationSpecialWires));
			this._ChangeLocationSpecialWires1 = new EntitySet<ChangeLocationSpecialWire>(new Action<ChangeLocationSpecialWire>(this.attach_ChangeLocationSpecialWires1), new Action<ChangeLocationSpecialWire>(this.detach_ChangeLocationSpecialWires1));
			this._E1s = new EntitySet<E1>(new Action<E1>(this.attach_E1s), new Action<E1>(this.detach_E1s));
			this._E1Links = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links), new Action<E1Link>(this.detach_E1Links));
			this._E1Links1 = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links1), new Action<E1Link>(this.detach_E1Links1));
			this._E1Links2 = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links2), new Action<E1Link>(this.detach_E1Links2));
			this._ExchangeBrokenPCMs = new EntitySet<ExchangeBrokenPCM>(new Action<ExchangeBrokenPCM>(this.attach_ExchangeBrokenPCMs), new Action<ExchangeBrokenPCM>(this.detach_ExchangeBrokenPCMs));
			this._ExchangeBrokenPCMs1 = new EntitySet<ExchangeBrokenPCM>(new Action<ExchangeBrokenPCM>(this.attach_ExchangeBrokenPCMs1), new Action<ExchangeBrokenPCM>(this.detach_ExchangeBrokenPCMs1));
			this._ExchangeCabinetInputConncetions = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions));
			this._ExchangeCabinetInputConncetions1 = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions1), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions1));
			this._ExchangeCentralCableMDFs = new EntitySet<ExchangeCentralCableMDF>(new Action<ExchangeCentralCableMDF>(this.attach_ExchangeCentralCableMDFs), new Action<ExchangeCentralCableMDF>(this.detach_ExchangeCentralCableMDFs));
			this._ExchangeCentralCableMDFs1 = new EntitySet<ExchangeCentralCableMDF>(new Action<ExchangeCentralCableMDF>(this.attach_ExchangeCentralCableMDFs1), new Action<ExchangeCentralCableMDF>(this.detach_ExchangeCentralCableMDFs1));
			this._ExchangeCentralCableMDFs2 = new EntitySet<ExchangeCentralCableMDF>(new Action<ExchangeCentralCableMDF>(this.attach_ExchangeCentralCableMDFs2), new Action<ExchangeCentralCableMDF>(this.detach_ExchangeCentralCableMDFs2));
			this._ExchangeCentralCableMDFs3 = new EntitySet<ExchangeCentralCableMDF>(new Action<ExchangeCentralCableMDF>(this.attach_ExchangeCentralCableMDFs3), new Action<ExchangeCentralCableMDF>(this.detach_ExchangeCentralCableMDFs3));
			this._ExchangeCentralCableMDFConncetions = new EntitySet<ExchangeCentralCableMDFConncetion>(new Action<ExchangeCentralCableMDFConncetion>(this.attach_ExchangeCentralCableMDFConncetions), new Action<ExchangeCentralCableMDFConncetion>(this.detach_ExchangeCentralCableMDFConncetions));
			this._ExchangeCentralCableMDFConncetions1 = new EntitySet<ExchangeCentralCableMDFConncetion>(new Action<ExchangeCentralCableMDFConncetion>(this.attach_ExchangeCentralCableMDFConncetions1), new Action<ExchangeCentralCableMDFConncetion>(this.detach_ExchangeCentralCableMDFConncetions1));
			this._InvestigatePossibilities = new EntitySet<InvestigatePossibility>(new Action<InvestigatePossibility>(this.attach_InvestigatePossibilities), new Action<InvestigatePossibility>(this.detach_InvestigatePossibilities));
			this._RefundDeposits = new EntitySet<RefundDeposit>(new Action<RefundDeposit>(this.attach_RefundDeposits), new Action<RefundDeposit>(this.detach_RefundDeposits));
			this._SpecialWires = new EntitySet<SpecialWire>(new Action<SpecialWire>(this.attach_SpecialWires), new Action<SpecialWire>(this.detach_SpecialWires));
			this._SpecialWireAddress = default(EntityRef<SpecialWireAddress>);
			this._SpecialWireAddresses = new EntitySet<SpecialWireAddress>(new Action<SpecialWireAddress>(this.attach_SpecialWireAddresses), new Action<SpecialWireAddress>(this.detach_SpecialWireAddresses));
			this._TranslationOpticalCabinetToNormalConncetions = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions));
			this._TranslationOpticalCabinetToNormalConncetions1 = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions1), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions1));
			this._VacateSpecialWires = new EntitySet<VacateSpecialWire>(new Action<VacateSpecialWire>(this.attach_VacateSpecialWires), new Action<VacateSpecialWire>(this.detach_VacateSpecialWires));
			this._VacateSpecialWirePoints = new EntitySet<VacateSpecialWirePoint>(new Action<VacateSpecialWirePoint>(this.attach_VacateSpecialWirePoints), new Action<VacateSpecialWirePoint>(this.detach_VacateSpecialWirePoints));
			this._Wirings = new EntitySet<Wiring>(new Action<Wiring>(this.attach_Wirings), new Action<Wiring>(this.detach_Wirings));
			this._ExchangeGSMConnections = new EntitySet<ExchangeGSMConnection>(new Action<ExchangeGSMConnection>(this.attach_ExchangeGSMConnections), new Action<ExchangeGSMConnection>(this.detach_ExchangeGSMConnections));
			this._Bucht1 = default(EntityRef<Bucht>);
			this._Bucht2 = default(EntityRef<Bucht>);
			this._BuchtType = default(EntityRef<BuchtType>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._CablePair = default(EntityRef<CablePair>);
			this._Center1 = default(EntityRef<Center>);
			this._City = default(EntityRef<City>);
			this._E1Number = default(EntityRef<E1Number>);
			this._PAPInfo = default(EntityRef<PAPInfo>);
			this._PCMPort = default(EntityRef<PCMPort>);
			this._PostContact = default(EntityRef<PostContact>);
			this._SwitchPort = default(EntityRef<SwitchPort>);
			this._Telephone = default(EntityRef<Telephone>);
			this._VerticalMDFRow = default(EntityRef<VerticalMDFRow>);
			  
		}
	}

	public partial class BuchtSwitching
	{
		public void Detach()
		{

			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			this._Bucht2 = default(EntityRef<Bucht>);
			this._CauseBuchtSwitching = default(EntityRef<CauseBuchtSwitching>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class BuchtType
	{
		public void Detach()
		{

			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._BuchtTypes = new EntitySet<BuchtType>(new Action<BuchtType>(this.attach_BuchtTypes), new Action<BuchtType>(this.detach_BuchtTypes));
			this._SpecialWires = new EntitySet<SpecialWire>(new Action<SpecialWire>(this.attach_SpecialWires), new Action<SpecialWire>(this.detach_SpecialWires));
			this._BuchtType1 = default(EntityRef<BuchtType>);
			  
		}
	}

	public partial class BuchtTypeNumberChange
	{
		public void Detach()
		{

			  
		}
	}

	public partial class Cabinet
	{
		public void Detach()
		{

			this._CabinetInputs = new EntitySet<CabinetInput>(new Action<CabinetInput>(this.attach_CabinetInputs), new Action<CabinetInput>(this.detach_CabinetInputs));
			this._CenterToCenterTranslations = new EntitySet<CenterToCenterTranslation>(new Action<CenterToCenterTranslation>(this.attach_CenterToCenterTranslations), new Action<CenterToCenterTranslation>(this.detach_CenterToCenterTranslations));
			this._CenterToCenterTranslations1 = new EntitySet<CenterToCenterTranslation>(new Action<CenterToCenterTranslation>(this.attach_CenterToCenterTranslations1), new Action<CenterToCenterTranslation>(this.detach_CenterToCenterTranslations1));
			this._ExchangeCabinetInputs = new EntitySet<ExchangeCabinetInput>(new Action<ExchangeCabinetInput>(this.attach_ExchangeCabinetInputs), new Action<ExchangeCabinetInput>(this.detach_ExchangeCabinetInputs));
			this._ExchangeCabinetInputs1 = new EntitySet<ExchangeCabinetInput>(new Action<ExchangeCabinetInput>(this.attach_ExchangeCabinetInputs1), new Action<ExchangeCabinetInput>(this.detach_ExchangeCabinetInputs1));
			this._ExchangePosts = new EntitySet<ExchangePost>(new Action<ExchangePost>(this.attach_ExchangePosts), new Action<ExchangePost>(this.detach_ExchangePosts));
			this._ExchangePosts1 = new EntitySet<ExchangePost>(new Action<ExchangePost>(this.attach_ExchangePosts1), new Action<ExchangePost>(this.detach_ExchangePosts1));
			this._InvestigatePossibilityWaitinglists = new EntitySet<InvestigatePossibilityWaitinglist>(new Action<InvestigatePossibilityWaitinglist>(this.attach_InvestigatePossibilityWaitinglists), new Action<InvestigatePossibilityWaitinglist>(this.detach_InvestigatePossibilityWaitinglists));
			this._Linesmans = new EntitySet<Linesman>(new Action<Linesman>(this.attach_Linesmans), new Action<Linesman>(this.detach_Linesmans));
			this._Posts = new EntitySet<Post>(new Action<Post>(this.attach_Posts), new Action<Post>(this.detach_Posts));
			this._TranslationOpticalCabinetToNormals = new EntitySet<TranslationOpticalCabinetToNormal>(new Action<TranslationOpticalCabinetToNormal>(this.attach_TranslationOpticalCabinetToNormals), new Action<TranslationOpticalCabinetToNormal>(this.detach_TranslationOpticalCabinetToNormals));
			this._TranslationOpticalCabinetToNormals1 = new EntitySet<TranslationOpticalCabinetToNormal>(new Action<TranslationOpticalCabinetToNormal>(this.attach_TranslationOpticalCabinetToNormals1), new Action<TranslationOpticalCabinetToNormal>(this.detach_TranslationOpticalCabinetToNormals1));
			this._TranslationPosts = new EntitySet<TranslationPost>(new Action<TranslationPost>(this.attach_TranslationPosts), new Action<TranslationPost>(this.detach_TranslationPosts));
			this._TranslationPostInputs = new EntitySet<TranslationPostInput>(new Action<TranslationPostInput>(this.attach_TranslationPostInputs), new Action<TranslationPostInput>(this.detach_TranslationPostInputs));
			this._TranslationPostInputs1 = new EntitySet<TranslationPostInput>(new Action<TranslationPostInput>(this.attach_TranslationPostInputs1), new Action<TranslationPostInput>(this.detach_TranslationPostInputs1));
			this._VisitPlacesCabinetAndPosts = new EntitySet<VisitPlacesCabinetAndPost>(new Action<VisitPlacesCabinetAndPost>(this.attach_VisitPlacesCabinetAndPosts), new Action<VisitPlacesCabinetAndPost>(this.detach_VisitPlacesCabinetAndPosts));
			this._ExchangeGSMConnections = new EntitySet<ExchangeGSMConnection>(new Action<ExchangeGSMConnection>(this.attach_ExchangeGSMConnections), new Action<ExchangeGSMConnection>(this.detach_ExchangeGSMConnections));
			this._AORBPostAndCabinet = default(EntityRef<AORBPostAndCabinet>);
			this._CabinetStatus = default(EntityRef<CabinetStatus>);
			this._CabinetType = default(EntityRef<CabinetType>);
			this._CabinetUsageType1 = default(EntityRef<CabinetUsageType>);
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class CabinetInput
	{
		public void Detach()
		{

			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._CablePairs = new EntitySet<CablePair>(new Action<CablePair>(this.attach_CablePairs), new Action<CablePair>(this.detach_CablePairs));
			this._CenterToCenterTranslations = new EntitySet<CenterToCenterTranslation>(new Action<CenterToCenterTranslation>(this.attach_CenterToCenterTranslations), new Action<CenterToCenterTranslation>(this.detach_CenterToCenterTranslations));
			this._CenterToCenterTranslations1 = new EntitySet<CenterToCenterTranslation>(new Action<CenterToCenterTranslation>(this.attach_CenterToCenterTranslations1), new Action<CenterToCenterTranslation>(this.detach_CenterToCenterTranslations1));
			this._CenterToCenterTranslations2 = new EntitySet<CenterToCenterTranslation>(new Action<CenterToCenterTranslation>(this.attach_CenterToCenterTranslations2), new Action<CenterToCenterTranslation>(this.detach_CenterToCenterTranslations2));
			this._CenterToCenterTranslations3 = new EntitySet<CenterToCenterTranslation>(new Action<CenterToCenterTranslation>(this.attach_CenterToCenterTranslations3), new Action<CenterToCenterTranslation>(this.detach_CenterToCenterTranslations3));
			this._ChangeLocationSpecialWires = new EntitySet<ChangeLocationSpecialWire>(new Action<ChangeLocationSpecialWire>(this.attach_ChangeLocationSpecialWires), new Action<ChangeLocationSpecialWire>(this.detach_ChangeLocationSpecialWires));
			this._ExchangeCabinetInputs = new EntitySet<ExchangeCabinetInput>(new Action<ExchangeCabinetInput>(this.attach_ExchangeCabinetInputs), new Action<ExchangeCabinetInput>(this.detach_ExchangeCabinetInputs));
			this._ExchangeCabinetInputs1 = new EntitySet<ExchangeCabinetInput>(new Action<ExchangeCabinetInput>(this.attach_ExchangeCabinetInputs1), new Action<ExchangeCabinetInput>(this.detach_ExchangeCabinetInputs1));
			this._ExchangeCabinetInputs2 = new EntitySet<ExchangeCabinetInput>(new Action<ExchangeCabinetInput>(this.attach_ExchangeCabinetInputs2), new Action<ExchangeCabinetInput>(this.detach_ExchangeCabinetInputs2));
			this._ExchangeCabinetInputs3 = new EntitySet<ExchangeCabinetInput>(new Action<ExchangeCabinetInput>(this.attach_ExchangeCabinetInputs3), new Action<ExchangeCabinetInput>(this.detach_ExchangeCabinetInputs3));
			this._ExchangeCabinetInputConncetions = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions));
			this._ExchangeCabinetInputConncetions1 = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions1), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions1));
			this._ExchangePosts = new EntitySet<ExchangePost>(new Action<ExchangePost>(this.attach_ExchangePosts), new Action<ExchangePost>(this.detach_ExchangePosts));
			this._ExchangePosts1 = new EntitySet<ExchangePost>(new Action<ExchangePost>(this.attach_ExchangePosts1), new Action<ExchangePost>(this.detach_ExchangePosts1));
			this._Malfuctions = new EntitySet<Malfuction>(new Action<Malfuction>(this.attach_Malfuctions), new Action<Malfuction>(this.detach_Malfuctions));
			this._RefundDeposits = new EntitySet<RefundDeposit>(new Action<RefundDeposit>(this.attach_RefundDeposits), new Action<RefundDeposit>(this.detach_RefundDeposits));
			this._TranslationOpticalCabinetToNormals = new EntitySet<TranslationOpticalCabinetToNormal>(new Action<TranslationOpticalCabinetToNormal>(this.attach_TranslationOpticalCabinetToNormals), new Action<TranslationOpticalCabinetToNormal>(this.detach_TranslationOpticalCabinetToNormals));
			this._TranslationOpticalCabinetToNormals1 = new EntitySet<TranslationOpticalCabinetToNormal>(new Action<TranslationOpticalCabinetToNormal>(this.attach_TranslationOpticalCabinetToNormals1), new Action<TranslationOpticalCabinetToNormal>(this.detach_TranslationOpticalCabinetToNormals1));
			this._TranslationOpticalCabinetToNormals2 = new EntitySet<TranslationOpticalCabinetToNormal>(new Action<TranslationOpticalCabinetToNormal>(this.attach_TranslationOpticalCabinetToNormals2), new Action<TranslationOpticalCabinetToNormal>(this.detach_TranslationOpticalCabinetToNormals2));
			this._TranslationOpticalCabinetToNormals3 = new EntitySet<TranslationOpticalCabinetToNormal>(new Action<TranslationOpticalCabinetToNormal>(this.attach_TranslationOpticalCabinetToNormals3), new Action<TranslationOpticalCabinetToNormal>(this.detach_TranslationOpticalCabinetToNormals3));
			this._TranslationOpticalCabinetToNormalConncetions = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions));
			this._TranslationOpticalCabinetToNormalConncetions1 = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions1), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions1));
			this._TranslationPostInputConnections = new EntitySet<TranslationPostInputConnection>(new Action<TranslationPostInputConnection>(this.attach_TranslationPostInputConnections), new Action<TranslationPostInputConnection>(this.detach_TranslationPostInputConnections));
			this._VacateSpecialWires = new EntitySet<VacateSpecialWire>(new Action<VacateSpecialWire>(this.attach_VacateSpecialWires), new Action<VacateSpecialWire>(this.detach_VacateSpecialWires));
			this._ExchangeGSMConnections = new EntitySet<ExchangeGSMConnection>(new Action<ExchangeGSMConnection>(this.attach_ExchangeGSMConnections), new Action<ExchangeGSMConnection>(this.detach_ExchangeGSMConnections));
			this._Cabinet = default(EntityRef<Cabinet>);
			this._CabinetInputDirection = default(EntityRef<CabinetInputDirection>);
			this._CabinetInputStatus = default(EntityRef<CabinetInputStatus>);
			  
		}
	}

	public partial class CabinetInputDirection
	{
		public void Detach()
		{

			this._CabinetInputs = new EntitySet<CabinetInput>(new Action<CabinetInput>(this.attach_CabinetInputs), new Action<CabinetInput>(this.detach_CabinetInputs));
			  
		}
	}

	public partial class CabinetInputStatus
	{
		public void Detach()
		{

			this._CabinetInputs = new EntitySet<CabinetInput>(new Action<CabinetInput>(this.attach_CabinetInputs), new Action<CabinetInput>(this.detach_CabinetInputs));
			  
		}
	}

	public partial class CabinetStatus
	{
		public void Detach()
		{

			this._Cabinets = new EntitySet<Cabinet>(new Action<Cabinet>(this.attach_Cabinets), new Action<Cabinet>(this.detach_Cabinets));
			  
		}
	}

	public partial class CabinetType
	{
		public void Detach()
		{

			this._Cabinets = new EntitySet<Cabinet>(new Action<Cabinet>(this.attach_Cabinets), new Action<Cabinet>(this.detach_Cabinets));
			  
		}
	}

	public partial class CabinetUsageType
	{
		public void Detach()
		{

			this._Cabinets = new EntitySet<Cabinet>(new Action<Cabinet>(this.attach_Cabinets), new Action<Cabinet>(this.detach_Cabinets));
			this._TranslationOpticalCabinetToNormals = new EntitySet<TranslationOpticalCabinetToNormal>(new Action<TranslationOpticalCabinetToNormal>(this.attach_TranslationOpticalCabinetToNormals), new Action<TranslationOpticalCabinetToNormal>(this.detach_TranslationOpticalCabinetToNormals));
			this._TranslationOpticalCabinetToNormals1 = new EntitySet<TranslationOpticalCabinetToNormal>(new Action<TranslationOpticalCabinetToNormal>(this.attach_TranslationOpticalCabinetToNormals1), new Action<TranslationOpticalCabinetToNormal>(this.detach_TranslationOpticalCabinetToNormals1));
			  
		}
	}

	public partial class Cable
	{
		public void Detach()
		{

			this._CablePairs = new EntitySet<CablePair>(new Action<CablePair>(this.attach_CablePairs), new Action<CablePair>(this.detach_CablePairs));
			this._CableType = default(EntityRef<CableType>);
			this._CableUsedChannel = default(EntityRef<CableUsedChannel>);
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class CableColor
	{
		public void Detach()
		{

			this._FailureForms = new EntitySet<FailureForm>(new Action<FailureForm>(this.attach_FailureForms), new Action<FailureForm>(this.detach_FailureForms));
			this._FailureForms1 = new EntitySet<FailureForm>(new Action<FailureForm>(this.attach_FailureForms1), new Action<FailureForm>(this.detach_FailureForms1));
			this._PostContacts = new EntitySet<PostContact>(new Action<PostContact>(this.attach_PostContacts), new Action<PostContact>(this.detach_PostContacts));
			this._PostContacts1 = new EntitySet<PostContact>(new Action<PostContact>(this.attach_PostContacts1), new Action<PostContact>(this.detach_PostContacts1));
			  
		}
	}

	public partial class CableDesignOffice
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class CablePair
	{
		public void Detach()
		{

			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._Cable = default(EntityRef<Cable>);
			  
		}
	}

	public partial class CableType
	{
		public void Detach()
		{

			this._Cables = new EntitySet<Cable>(new Action<Cable>(this.attach_Cables), new Action<Cable>(this.detach_Cables));
			  
		}
	}

	public partial class CableUsedChannel
	{
		public void Detach()
		{

			this._Cables = new EntitySet<Cable>(new Action<Cable>(this.attach_Cables), new Action<Cable>(this.detach_Cables));
			  
		}
	}

	public partial class CancelationRequestList
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class CauseBuchtSwitching
	{
		public void Detach()
		{

			this._BuchtSwitchings = new EntitySet<BuchtSwitching>(new Action<BuchtSwitching>(this.attach_BuchtSwitchings), new Action<BuchtSwitching>(this.detach_BuchtSwitchings));
			  
		}
	}

	public partial class CauseOfChangeNo
	{
		public void Detach()
		{

			this._ChangeNos = new EntitySet<ChangeNo>(new Action<ChangeNo>(this.attach_ChangeNos), new Action<ChangeNo>(this.detach_ChangeNos));
			  
		}
	}

	public partial class CauseOfCut
	{
		public void Detach()
		{

			this._CutAndEstablishes = new EntitySet<CutAndEstablish>(new Action<CutAndEstablish>(this.attach_CutAndEstablishes), new Action<CutAndEstablish>(this.detach_CutAndEstablishes));
			this._Telephones = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones), new Action<Telephone>(this.detach_Telephones));
			  
		}
	}

	public partial class CauseOfRefundDeposit
	{
		public void Detach()
		{

			this._RefundDeposits = new EntitySet<RefundDeposit>(new Action<RefundDeposit>(this.attach_RefundDeposits), new Action<RefundDeposit>(this.detach_RefundDeposits));
			  
		}
	}

	public partial class CauseOfTakePossession
	{
		public void Detach()
		{

			this._TakePossessions = new EntitySet<TakePossession>(new Action<TakePossession>(this.attach_TakePossessions), new Action<TakePossession>(this.detach_TakePossessions));
			this._Telephones = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones), new Action<Telephone>(this.detach_Telephones));
			  
		}
	}

	public partial class Center
	{
		public void Detach()
		{

			this._AdameEmkanats = new EntitySet<AdameEmkanat>(new Action<AdameEmkanat>(this.attach_AdameEmkanats), new Action<AdameEmkanat>(this.detach_AdameEmkanats));
			this._Addresses = new EntitySet<Address>(new Action<Address>(this.attach_Addresses), new Action<Address>(this.detach_Addresses));
			this._ADSLChangePlaces = new EntitySet<ADSLChangePlace>(new Action<ADSLChangePlace>(this.attach_ADSLChangePlaces), new Action<ADSLChangePlace>(this.detach_ADSLChangePlaces));
			this._ADSLChangePlaces1 = new EntitySet<ADSLChangePlace>(new Action<ADSLChangePlace>(this.attach_ADSLChangePlaces1), new Action<ADSLChangePlace>(this.detach_ADSLChangePlaces1));
			this._ADSLEquipments = new EntitySet<ADSLEquipment>(new Action<ADSLEquipment>(this.attach_ADSLEquipments), new Action<ADSLEquipment>(this.detach_ADSLEquipments));
			this._ADSLInstalCostCenters = new EntitySet<ADSLInstalCostCenter>(new Action<ADSLInstalCostCenter>(this.attach_ADSLInstalCostCenters), new Action<ADSLInstalCostCenter>(this.detach_ADSLInstalCostCenters));
			this._ADSLModemProperties = new EntitySet<ADSLModemProperty>(new Action<ADSLModemProperty>(this.attach_ADSLModemProperties), new Action<ADSLModemProperty>(this.detach_ADSLModemProperties));
			this._ADSLPAPCabinetAccuracies = new EntitySet<ADSLPAPCabinetAccuracy>(new Action<ADSLPAPCabinetAccuracy>(this.attach_ADSLPAPCabinetAccuracies), new Action<ADSLPAPCabinetAccuracy>(this.detach_ADSLPAPCabinetAccuracies));
			this._ADSLPAPPorts = new EntitySet<ADSLPAPPort>(new Action<ADSLPAPPort>(this.attach_ADSLPAPPorts), new Action<ADSLPAPPort>(this.detach_ADSLPAPPorts));
			this._ADSLServiceCenters = new EntitySet<ADSLServiceCenter>(new Action<ADSLServiceCenter>(this.attach_ADSLServiceCenters), new Action<ADSLServiceCenter>(this.detach_ADSLServiceCenters));
			this._ADSLServiceGroupCenters = new EntitySet<ADSLServiceGroupCenter>(new Action<ADSLServiceGroupCenter>(this.attach_ADSLServiceGroupCenters), new Action<ADSLServiceGroupCenter>(this.detach_ADSLServiceGroupCenters));
			this._ADSLTelephoneAccuracies = new EntitySet<ADSLTelephoneAccuracy>(new Action<ADSLTelephoneAccuracy>(this.attach_ADSLTelephoneAccuracies), new Action<ADSLTelephoneAccuracy>(this.detach_ADSLTelephoneAccuracies));
			this._ADSLTelephoneNoHistories = new EntitySet<ADSLTelephoneNoHistory>(new Action<ADSLTelephoneNoHistory>(this.attach_ADSLTelephoneNoHistories), new Action<ADSLTelephoneNoHistory>(this.detach_ADSLTelephoneNoHistories));
			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._Cabinets = new EntitySet<Cabinet>(new Action<Cabinet>(this.attach_Cabinets), new Action<Cabinet>(this.detach_Cabinets));
			this._Cables = new EntitySet<Cable>(new Action<Cable>(this.attach_Cables), new Action<Cable>(this.detach_Cables));
			this._CenterToCenterTranslations = new EntitySet<CenterToCenterTranslation>(new Action<CenterToCenterTranslation>(this.attach_CenterToCenterTranslations), new Action<CenterToCenterTranslation>(this.detach_CenterToCenterTranslations));
			this._CenterToCenterTranslations1 = new EntitySet<CenterToCenterTranslation>(new Action<CenterToCenterTranslation>(this.attach_CenterToCenterTranslations1), new Action<CenterToCenterTranslation>(this.detach_CenterToCenterTranslations1));
			this._ChangeLocations = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations), new Action<ChangeLocation>(this.detach_ChangeLocations));
			this._ChangeLocations1 = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations1), new Action<ChangeLocation>(this.detach_ChangeLocations1));
			this._E1DDFs = new EntitySet<E1DDF>(new Action<E1DDF>(this.attach_E1DDFs), new Action<E1DDF>(this.detach_E1DDFs));
			this._Failure117CabenitAccuracies = new EntitySet<Failure117CabenitAccuracy>(new Action<Failure117CabenitAccuracy>(this.attach_Failure117CabenitAccuracies), new Action<Failure117CabenitAccuracy>(this.detach_Failure117CabenitAccuracies));
			this._Failure117PostAccuracies = new EntitySet<Failure117PostAccuracy>(new Action<Failure117PostAccuracy>(this.attach_Failure117PostAccuracies), new Action<Failure117PostAccuracy>(this.detach_Failure117PostAccuracies));
			this._Failure117TelephoneAccuracies = new EntitySet<Failure117TelephoneAccuracy>(new Action<Failure117TelephoneAccuracy>(this.attach_Failure117TelephoneAccuracies), new Action<Failure117TelephoneAccuracy>(this.detach_Failure117TelephoneAccuracies));
			this._Fiches = new EntitySet<Fiche>(new Action<Fiche>(this.attach_Fiches), new Action<Fiche>(this.detach_Fiches));
			this._MDFs = new EntitySet<MDF>(new Action<MDF>(this.attach_MDFs), new Action<MDF>(this.detach_MDFs));
			this._MDFPersonnels = new EntitySet<MDFPersonnel>(new Action<MDFPersonnel>(this.attach_MDFPersonnels), new Action<MDFPersonnel>(this.detach_MDFPersonnels));
			this._PAPInfoSpaceandPowers = new EntitySet<PAPInfoSpaceandPower>(new Action<PAPInfoSpaceandPower>(this.attach_PAPInfoSpaceandPowers), new Action<PAPInfoSpaceandPower>(this.detach_PAPInfoSpaceandPowers));
			this._PCMRocks = new EntitySet<PCMRock>(new Action<PCMRock>(this.attach_PCMRocks), new Action<PCMRock>(this.detach_PCMRocks));
			this._PostGroups = new EntitySet<PostGroup>(new Action<PostGroup>(this.attach_PostGroups), new Action<PostGroup>(this.detach_PostGroups));
			this._Requests = new EntitySet<Request>(new Action<Request>(this.attach_Requests), new Action<Request>(this.detach_Requests));
			this._Rocks = new EntitySet<Rock>(new Action<Rock>(this.attach_Rocks), new Action<Rock>(this.detach_Rocks));
			this._RoundSaleInfos = new EntitySet<RoundSaleInfo>(new Action<RoundSaleInfo>(this.attach_RoundSaleInfos), new Action<RoundSaleInfo>(this.detach_RoundSaleInfos));
			this._SpecialPrivateCables = new EntitySet<SpecialPrivateCable>(new Action<SpecialPrivateCable>(this.attach_SpecialPrivateCables), new Action<SpecialPrivateCable>(this.detach_SpecialPrivateCables));
			this._SpecialWirePoints = new EntitySet<SpecialWirePoint>(new Action<SpecialWirePoint>(this.attach_SpecialWirePoints), new Action<SpecialWirePoint>(this.detach_SpecialWirePoints));
			this._Switches = new EntitySet<Switch>(new Action<Switch>(this.attach_Switches), new Action<Switch>(this.detach_Switches));
			this._SwitchPrecodes = new EntitySet<SwitchPrecode>(new Action<SwitchPrecode>(this.attach_SwitchPrecodes), new Action<SwitchPrecode>(this.detach_SwitchPrecodes));
			this._Telephones = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones), new Action<Telephone>(this.detach_Telephones));
			this._UserCenters = new EntitySet<UserCenter>(new Action<UserCenter>(this.attach_UserCenters), new Action<UserCenter>(this.detach_UserCenters));
			this._VacateSpecialWirePoints = new EntitySet<VacateSpecialWirePoint>(new Action<VacateSpecialWirePoint>(this.attach_VacateSpecialWirePoints), new Action<VacateSpecialWirePoint>(this.detach_VacateSpecialWirePoints));
			this._Failure117UBs = new EntitySet<Failure117UB>(new Action<Failure117UB>(this.attach_Failure117UBs), new Action<Failure117UB>(this.detach_Failure117UBs));
			this._Region = default(EntityRef<Region>);
			this._SubsidiaryCode = default(EntityRef<SubsidiaryCode>);
			this._SubsidiaryCode1 = default(EntityRef<SubsidiaryCode>);
			this._SubsidiaryCode2 = default(EntityRef<SubsidiaryCode>);
			  
		}
	}

	public partial class CenterToCenterTranslation
	{
		public void Detach()
		{

			this._Cabinet = default(EntityRef<Cabinet>);
			this._Cabinet1 = default(EntityRef<Cabinet>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._CabinetInput1 = default(EntityRef<CabinetInput>);
			this._CabinetInput2 = default(EntityRef<CabinetInput>);
			this._CabinetInput3 = default(EntityRef<CabinetInput>);
			this._Center = default(EntityRef<Center>);
			this._Center1 = default(EntityRef<Center>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class CenterToCenterTranslationPCM
	{
		public void Detach()
		{

			this._PCM = default(EntityRef<PCM>);
			this._PCM1 = default(EntityRef<PCM>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class CenterToCenterTranslationTelephone
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Telephone1 = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class CentralTelType
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ChangeAddress
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Address2 = default(EntityRef<Address>);
			this._Address3 = default(EntityRef<Address>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ChangeLocation
	{
		public void Detach()
		{

			this._Linesmans = new EntitySet<Linesman>(new Action<Linesman>(this.attach_Linesmans), new Action<Linesman>(this.detach_Linesmans));
			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Address2 = default(EntityRef<Address>);
			this._Address3 = default(EntityRef<Address>);
			this._Bucht = default(EntityRef<Bucht>);
			this._Center = default(EntityRef<Center>);
			this._Center1 = default(EntityRef<Center>);
			this._Counter = default(EntityRef<Counter>);
			this._PostContact = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class ChangeLocationSpecialWire
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._PostContact = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			this._SwitchPort = default(EntityRef<SwitchPort>);
			  
		}
	}

	public partial class ChangeLocationSpecialWirePoint
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ChangeName
	{
		public void Detach()
		{

			this._Cycle = default(EntityRef<Cycle>);
			this._Request = default(EntityRef<Request>);
			this._Customer = default(EntityRef<Customer>);
			this._Customer1 = default(EntityRef<Customer>);
			  
		}
	}

	public partial class ChangeNo
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._CauseOfChangeNo = default(EntityRef<CauseOfChangeNo>);
			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Telephone1 = default(EntityRef<Telephone>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class ChangeNoInstallLine
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ChangePreCode
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ChangeTelephoneType
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class City
	{
		public void Detach()
		{

			this._ADSLPAPFeasibilities = new EntitySet<ADSLPAPFeasibility>(new Action<ADSLPAPFeasibility>(this.attach_ADSLPAPFeasibilities), new Action<ADSLPAPFeasibility>(this.detach_ADSLPAPFeasibilities));
			this._ADSLSellerAgents = new EntitySet<ADSLSellerAgent>(new Action<ADSLSellerAgent>(this.attach_ADSLSellerAgents), new Action<ADSLSellerAgent>(this.detach_ADSLSellerAgents));
			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._Offices = new EntitySet<Office>(new Action<Office>(this.attach_Offices), new Action<Office>(this.detach_Offices));
			this._PAPInfoLimitations = new EntitySet<PAPInfoLimitation>(new Action<PAPInfoLimitation>(this.attach_PAPInfoLimitations), new Action<PAPInfoLimitation>(this.detach_PAPInfoLimitations));
			this._PAPInfoUsers = new EntitySet<PAPInfoUser>(new Action<PAPInfoUser>(this.attach_PAPInfoUsers), new Action<PAPInfoUser>(this.detach_PAPInfoUsers));
			this._Regions = new EntitySet<Region>(new Action<Region>(this.attach_Regions), new Action<Region>(this.detach_Regions));
			this._Province = default(EntityRef<Province>);
			  
		}
	}

	public partial class Contract
	{
		public void Detach()
		{

			this._Contracts = new EntitySet<Contract>(new Action<Contract>(this.attach_Contracts), new Action<Contract>(this.detach_Contracts));
			this._Contract1 = default(EntityRef<Contract>);
			this._Request = default(EntityRef<Request>);
			this._RequestDocument = default(EntityRef<RequestDocument>);
			this._TelRoundSale = default(EntityRef<TelRoundSale>);
			  
		}
	}

	public partial class Contractor
	{
		public void Detach()
		{

			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			  
		}
	}

	public partial class Control
	{
		public void Detach()
		{

			this._Notices = new EntitySet<Notice>(new Action<Notice>(this.attach_Notices), new Action<Notice>(this.detach_Notices));
			this._ParazitControls = new EntitySet<ParazitControl>(new Action<ParazitControl>(this.attach_ParazitControls), new Action<ParazitControl>(this.detach_ParazitControls));
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class Counter
	{
		public void Detach()
		{

			this._ChangeLocations = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations), new Action<ChangeLocation>(this.detach_ChangeLocations));
			this._InstallLines = new EntitySet<InstallLine>(new Action<InstallLine>(this.attach_InstallLines), new Action<InstallLine>(this.detach_InstallLines));
			this._Cycle = default(EntityRef<Cycle>);
			  
		}
	}

	public partial class CustomerGroup
	{
		public void Detach()
		{

			this._Telephones = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones), new Action<Telephone>(this.detach_Telephones));
			this._InstallRequests = new EntitySet<InstallRequest>(new Action<InstallRequest>(this.attach_InstallRequests), new Action<InstallRequest>(this.detach_InstallRequests));
			this._CustomerType = default(EntityRef<CustomerType>);
			  
		}
	}

	public partial class CustomerType
	{
		public void Detach()
		{

			this._CustomerGroups = new EntitySet<CustomerGroup>(new Action<CustomerGroup>(this.attach_CustomerGroups), new Action<CustomerGroup>(this.detach_CustomerGroups));
			this._Telephones = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones), new Action<Telephone>(this.detach_Telephones));
			this._InstallRequests = new EntitySet<InstallRequest>(new Action<InstallRequest>(this.attach_InstallRequests), new Action<InstallRequest>(this.detach_InstallRequests));
			  
		}
	}

	public partial class CutAndEstablish
	{
		public void Detach()
		{

			this._CauseOfCut = default(EntityRef<CauseOfCut>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class Cycle
	{
		public void Detach()
		{

			this._ChangeNames = new EntitySet<ChangeName>(new Action<ChangeName>(this.attach_ChangeNames), new Action<ChangeName>(this.detach_ChangeNames));
			this._Counters = new EntitySet<Counter>(new Action<Counter>(this.attach_Counters), new Action<Counter>(this.detach_Counters));
			  
		}
	}

	public partial class DamageLine
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class DataChangesLog
	{
		public void Detach()
		{

		}
	}

	public partial class DataGridColumnConfig
	{
		public void Detach()
		{

			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class DocumentRequestType
	{
		public void Detach()
		{

			this._RequestDocuments = new EntitySet<RequestDocument>(new Action<RequestDocument>(this.attach_RequestDocuments), new Action<RequestDocument>(this.detach_RequestDocuments));
			this._Announce = default(EntityRef<Announce>);
			this._DocumentType = default(EntityRef<DocumentType>);
			this._RequestType = default(EntityRef<RequestType>);
			  
		}
	}

	public partial class DocumentsFile
	{
		public void Detach()
		{

			  
		}
	}

	public partial class DocumentType
	{
		public void Detach()
		{

			this._DocumentRequestTypes = new EntitySet<DocumentRequestType>(new Action<DocumentRequestType>(this.attach_DocumentRequestTypes), new Action<DocumentRequestType>(this.detach_DocumentRequestTypes));
			  
		}
	}

	public partial class E1
	{
		public void Detach()
		{

			this._E1Links = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links), new Action<E1Link>(this.detach_E1Links));
			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Address2 = default(EntityRef<Address>);
			this._Bucht = default(EntityRef<Bucht>);
			this._E1CodeType = default(EntityRef<E1CodeType>);
			this._E1LinkType = default(EntityRef<E1LinkType>);
			this._E1Modem = default(EntityRef<E1Modem>);
			this._PostContact = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			this._Switch = default(EntityRef<Switch>);
			this._SwitchPrecode = default(EntityRef<SwitchPrecode>);
			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class E1Bay
	{
		public void Detach()
		{

			this._E1Positions = new EntitySet<E1Position>(new Action<E1Position>(this.attach_E1Positions), new Action<E1Position>(this.detach_E1Positions));
			this._E1DDF = default(EntityRef<E1DDF>);
			  
		}
	}

	public partial class E1CodeType
	{
		public void Detach()
		{

			this._E1s = new EntitySet<E1>(new Action<E1>(this.attach_E1s), new Action<E1>(this.detach_E1s));
			  
		}
	}

	public partial class E1DDF
	{
		public void Detach()
		{

			this._E1Bays = new EntitySet<E1Bay>(new Action<E1Bay>(this.attach_E1Bays), new Action<E1Bay>(this.detach_E1Bays));
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class E1Link
	{
		public void Detach()
		{

			this._E1Link2 = default(EntityRef<E1Link>);
			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			this._Bucht2 = default(EntityRef<Bucht>);
			this._E1 = default(EntityRef<E1>);
			this._E1Link1 = default(EntityRef<E1Link>);
			this._E1Number = default(EntityRef<E1Number>);
			this._E1Number1 = default(EntityRef<E1Number>);
			this._E1Number2 = default(EntityRef<E1Number>);
			this._InvestigatePossibility = default(EntityRef<InvestigatePossibility>);
			this._PostContact = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class E1LinkType
	{
		public void Detach()
		{

			this._E1s = new EntitySet<E1>(new Action<E1>(this.attach_E1s), new Action<E1>(this.detach_E1s));
			  
		}
	}

	public partial class E1Modem
	{
		public void Detach()
		{

			this._E1s = new EntitySet<E1>(new Action<E1>(this.attach_E1s), new Action<E1>(this.detach_E1s));
			  
		}
	}

	public partial class E1Number
	{
		public void Detach()
		{

			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._E1Links = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links), new Action<E1Link>(this.detach_E1Links));
			this._E1Links1 = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links1), new Action<E1Link>(this.detach_E1Links1));
			this._E1Links2 = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links2), new Action<E1Link>(this.detach_E1Links2));
			this._E1Numbers = new EntitySet<E1Number>(new Action<E1Number>(this.attach_E1Numbers), new Action<E1Number>(this.detach_E1Numbers));
			this._E1Number1 = default(EntityRef<E1Number>);
			this._E1Position = default(EntityRef<E1Position>);
			  
		}
	}

	public partial class E1Position
	{
		public void Detach()
		{

			this._E1Numbers = new EntitySet<E1Number>(new Action<E1Number>(this.attach_E1Numbers), new Action<E1Number>(this.detach_E1Numbers));
			this._E1Bay = default(EntityRef<E1Bay>);
			  
		}
	}

	public partial class Equipment
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ErrorLog
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ExchangeBrokenPCM
	{
		public void Detach()
		{

			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			this._PCM = default(EntityRef<PCM>);
			this._PCM1 = default(EntityRef<PCM>);
			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class ExchangeCabinetInput
	{
		public void Detach()
		{

			this._Cabinet = default(EntityRef<Cabinet>);
			this._Cabinet1 = default(EntityRef<Cabinet>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._CabinetInput1 = default(EntityRef<CabinetInput>);
			this._CabinetInput2 = default(EntityRef<CabinetInput>);
			this._CabinetInput3 = default(EntityRef<CabinetInput>);
			this._Post = default(EntityRef<Post>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ExchangeCabinetInputConncetion
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._CabinetInput1 = default(EntityRef<CabinetInput>);
			this._Post = default(EntityRef<Post>);
			this._Post1 = default(EntityRef<Post>);
			this._PostContact = default(EntityRef<PostContact>);
			this._PostContact1 = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class ExchangeCentralCableMDF
	{
		public void Detach()
		{

			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			this._Bucht2 = default(EntityRef<Bucht>);
			this._Bucht3 = default(EntityRef<Bucht>);
			  
		}
	}

	public partial class ExchangeCentralCableMDFConncetion
	{
		public void Detach()
		{

			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class ExchangePost
	{
		public void Detach()
		{

			this._Cabinet = default(EntityRef<Cabinet>);
			this._Cabinet1 = default(EntityRef<Cabinet>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._CabinetInput1 = default(EntityRef<CabinetInput>);
			this._Post = default(EntityRef<Post>);
			this._Post1 = default(EntityRef<Post>);
			this._PostContact = default(EntityRef<PostContact>);
			this._PostContact1 = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class ExchangeTelephoneNo
	{
		public void Detach()
		{

			this._ExchangeTelephoneNo2 = default(EntityRef<ExchangeTelephoneNo>);
			this._ExchangeTelephoneNo1 = default(EntityRef<ExchangeTelephoneNo>);
			this._Request = default(EntityRef<Request>);
			this._Switch = default(EntityRef<Switch>);
			this._Switch1 = default(EntityRef<Switch>);
			this._SwitchPrecode = default(EntityRef<SwitchPrecode>);
			this._SwitchPrecode1 = default(EntityRef<SwitchPrecode>);
			  
		}
	}

	public partial class Failure117
	{
		public void Detach()
		{

			this._FailureForms = new EntitySet<FailureForm>(new Action<FailureForm>(this.attach_FailureForms), new Action<FailureForm>(this.detach_FailureForms));
			this._Failure117FailureStatus = default(EntityRef<Failure117FailureStatus>);
			this._Failure117LineStatus = default(EntityRef<Failure117LineStatus>);
			this._MDFPersonnel = default(EntityRef<MDFPersonnel>);
			this._MDFPersonnel1 = default(EntityRef<MDFPersonnel>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class Failure117CabenitAccuracy
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class Failure117CableType
	{
		public void Detach()
		{

			this._FailureForms = new EntitySet<FailureForm>(new Action<FailureForm>(this.attach_FailureForms), new Action<FailureForm>(this.detach_FailureForms));
			  
		}
	}

	public partial class Failure117FailureStatus
	{
		public void Detach()
		{

			this._Failure117s = new EntitySet<Failure117>(new Action<Failure117>(this.attach_Failure117s), new Action<Failure117>(this.detach_Failure117s));
			this._Failure117FailureStatus2 = new EntitySet<Failure117FailureStatus>(new Action<Failure117FailureStatus>(this.attach_Failure117FailureStatus2), new Action<Failure117FailureStatus>(this.detach_Failure117FailureStatus2));
			this._FailureForms = new EntitySet<FailureForm>(new Action<FailureForm>(this.attach_FailureForms), new Action<FailureForm>(this.detach_FailureForms));
			this._Failure117FailureStatus1 = default(EntityRef<Failure117FailureStatus>);
			  
		}
	}

	public partial class Failure117LineStatus
	{
		public void Detach()
		{

			this._Failure117s = new EntitySet<Failure117>(new Action<Failure117>(this.attach_Failure117s), new Action<Failure117>(this.detach_Failure117s));
			  
		}
	}

	public partial class Failure117NetworkContractor
	{
		public void Detach()
		{

			this._Failure117NetworkContractorCenters = new EntitySet<Failure117NetworkContractorCenter>(new Action<Failure117NetworkContractorCenter>(this.attach_Failure117NetworkContractorCenters), new Action<Failure117NetworkContractorCenter>(this.detach_Failure117NetworkContractorCenters));
			this._Failure117NetworkContractorOfficers = new EntitySet<Failure117NetworkContractorOfficer>(new Action<Failure117NetworkContractorOfficer>(this.attach_Failure117NetworkContractorOfficers), new Action<Failure117NetworkContractorOfficer>(this.detach_Failure117NetworkContractorOfficers));
			  
		}
	}

	public partial class Failure117NetworkContractorCenter
	{
		public void Detach()
		{

			this._Failure117NetworkContractor = default(EntityRef<Failure117NetworkContractor>);
			  
		}
	}

	public partial class Failure117NetworkContractorOfficer
	{
		public void Detach()
		{

			this._FailureForms = new EntitySet<FailureForm>(new Action<FailureForm>(this.attach_FailureForms), new Action<FailureForm>(this.detach_FailureForms));
			this._Wirings = new EntitySet<Wiring>(new Action<Wiring>(this.attach_Wirings), new Action<Wiring>(this.detach_Wirings));
			this._Failure117NetworkContractor = default(EntityRef<Failure117NetworkContractor>);
			  
		}
	}

	public partial class Failure117PostAccuracy
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class Failure117TelephoneAccuracy
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class FailureForm
	{
		public void Detach()
		{

			this._CableColor = default(EntityRef<CableColor>);
			this._CableColor3 = default(EntityRef<CableColor>);
			this._Failure117 = default(EntityRef<Failure117>);
			this._Failure117CableType = default(EntityRef<Failure117CableType>);
			this._Failure117FailureStatus = default(EntityRef<Failure117FailureStatus>);
			this._Failure117NetworkContractorOfficer = default(EntityRef<Failure117NetworkContractorOfficer>);
			  
		}
	}

	public partial class Fiche
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class Filter
	{
		public void Detach()
		{

			this._FilterRules = new EntitySet<FilterRule>(new Action<FilterRule>(this.attach_FilterRules), new Action<FilterRule>(this.detach_FilterRules));
			  
		}
	}

	public partial class FilterColumn
	{
		public void Detach()
		{

			this._FilterRules = new EntitySet<FilterRule>(new Action<FilterRule>(this.attach_FilterRules), new Action<FilterRule>(this.detach_FilterRules));
			  
		}
	}

	public partial class FilterRule
	{
		public void Detach()
		{

			this._Filter = default(EntityRef<Filter>);
			this._FilterColumn = default(EntityRef<FilterColumn>);
			this._LogicalOperator = default(EntityRef<LogicalOperator>);
			this._RelationalOperator = default(EntityRef<RelationalOperator>);
			  
		}
	}

	public partial class FormTemplate
	{
		public void Detach()
		{

			this._RequestForms = new EntitySet<RequestForm>(new Action<RequestForm>(this.attach_RequestForms), new Action<RequestForm>(this.detach_RequestForms));
			  
		}
	}

	public partial class GH_TelephonePAPState
	{
		public void Detach()
		{

		}
	}

	public partial class Inquiry
	{
		public void Detach()
		{

			this._InquiryType1s = new EntitySet<InquiryType1>(new Action<InquiryType1>(this.attach_InquiryType1s), new Action<InquiryType1>(this.detach_InquiryType1s));
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class InquiryType
	{
		public void Detach()
		{

			this._InquiryType1s = new EntitySet<InquiryType1>(new Action<InquiryType1>(this.attach_InquiryType1s), new Action<InquiryType1>(this.detach_InquiryType1s));
			  
		}
	}

	public partial class InquiryType1
	{
		public void Detach()
		{

			this._Inquiry = default(EntityRef<Inquiry>);
			this._InquiryType = default(EntityRef<InquiryType>);
			  
		}
	}

	public partial class InstallLine
	{
		public void Detach()
		{

			this._Counter = default(EntityRef<Counter>);
			this._Status1 = default(EntityRef<Status>);
			this._Wiring = default(EntityRef<Wiring>);
			  
		}
	}

	public partial class Installment
	{
		public void Detach()
		{

			this._PaymentFiches = new EntitySet<PaymentFiche>(new Action<PaymentFiche>(this.attach_PaymentFiches), new Action<PaymentFiche>(this.detach_PaymentFiches));
			this._BaseCost = default(EntityRef<BaseCost>);
			this._OtherCost = default(EntityRef<OtherCost>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class InstallmentRequestPayment
	{
		public void Detach()
		{

			this._RequestPayment = default(EntityRef<RequestPayment>);
			  
		}
	}

	public partial class InstallmentRequestPayment_Temp
	{
		public void Detach()
		{

		}
	}

	public partial class InstallmentRequestPaymentCorrection
	{
		public void Detach()
		{

			  
		}
	}

	public partial class InvestigatePossibility
	{
		public void Detach()
		{

			this._E1Links = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links), new Action<E1Link>(this.detach_E1Links));
			this._InvestigatePossibilities = new EntitySet<InvestigatePossibility>(new Action<InvestigatePossibility>(this.attach_InvestigatePossibilities), new Action<InvestigatePossibility>(this.detach_InvestigatePossibilities));
			this._Wirings = new EntitySet<Wiring>(new Action<Wiring>(this.attach_Wirings), new Action<Wiring>(this.detach_Wirings));
			this._Bucht = default(EntityRef<Bucht>);
			this._InvestigatePossibility1 = default(EntityRef<InvestigatePossibility>);
			this._PostContact = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class InvestigatePossibilityWaitinglist
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Cabinet = default(EntityRef<Cabinet>);
			this._Post = default(EntityRef<Post>);
			this._Telephone = default(EntityRef<Telephone>);
			this._WaitingList = default(EntityRef<WaitingList>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class IssueWiring
	{
		public void Detach()
		{

			this._Wirings = new EntitySet<Wiring>(new Action<Wiring>(this.attach_Wirings), new Action<Wiring>(this.detach_Wirings));
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class JobGroup
	{
		public void Detach()
		{

			  
		}
	}

	public partial class KermanshahReportTemplateBackup
	{
		public void Detach()
		{

		}
	}

	public partial class Linesman
	{
		public void Detach()
		{

			this._Cabinet = default(EntityRef<Cabinet>);
			this._ChangeLocation = default(EntityRef<ChangeLocation>);
			this._Post = default(EntityRef<Post>);
			  
		}
	}

	public partial class Log
	{
		public void Detach()
		{

			  
		}
	}

	public partial class LogicalOperator
	{
		public void Detach()
		{

			this._FilterRules = new EntitySet<FilterRule>(new Action<FilterRule>(this.attach_FilterRules), new Action<FilterRule>(this.detach_FilterRules));
			  
		}
	}

	public partial class Malfuction
	{
		public void Detach()
		{

			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._PCMPort = default(EntityRef<PCMPort>);
			this._PostContact = default(EntityRef<PostContact>);
			  
		}
	}

	public partial class MatchPossibility
	{
		public void Detach()
		{

			this._SugesstionPossibility = default(EntityRef<SugesstionPossibility>);
			  
		}
	}

	public partial class MDF
	{
		public void Detach()
		{

			this._ADSLMDFRanges = new EntitySet<ADSLMDFRange>(new Action<ADSLMDFRange>(this.attach_ADSLMDFRanges), new Action<ADSLMDFRange>(this.detach_ADSLMDFRanges));
			this._MDFFrames = new EntitySet<MDFFrame>(new Action<MDFFrame>(this.attach_MDFFrames), new Action<MDFFrame>(this.detach_MDFFrames));
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class MDF1
	{
		public void Detach()
		{

			  
		}
	}

	public partial class MDFFrame
	{
		public void Detach()
		{

			this._VerticalMDFColumns = new EntitySet<VerticalMDFColumn>(new Action<VerticalMDFColumn>(this.attach_VerticalMDFColumns), new Action<VerticalMDFColumn>(this.detach_VerticalMDFColumns));
			this._MDF = default(EntityRef<MDF>);
			  
		}
	}

	public partial class MDFPersonnel
	{
		public void Detach()
		{

			this._Failure117s = new EntitySet<Failure117>(new Action<Failure117>(this.attach_Failure117s), new Action<Failure117>(this.detach_Failure117s));
			this._Failure117s1 = new EntitySet<Failure117>(new Action<Failure117>(this.attach_Failure117s1), new Action<Failure117>(this.detach_Failure117s1));
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class MDFWiring
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class MDFWorkingHour
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ModifyProfile
	{
		public void Detach()
		{

			  
		}
	}

	public partial class Mortgage
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Request1 = default(EntityRef<Request>);
			  
		}
	}

	public partial class NetworkWiring
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class Notice
	{
		public void Detach()
		{

			this._TakePossessions = new EntitySet<TakePossession>(new Action<TakePossession>(this.attach_TakePossessions), new Action<TakePossession>(this.detach_TakePossessions));
			this._Control = default(EntityRef<Control>);
			  
		}
	}

	public partial class Office
	{
		public void Detach()
		{

			this._OfficeEmployees = new EntitySet<OfficeEmployee>(new Action<OfficeEmployee>(this.attach_OfficeEmployees), new Action<OfficeEmployee>(this.detach_OfficeEmployees));
			this._City = default(EntityRef<City>);
			  
		}
	}

	public partial class OfficeEmployee
	{
		public void Detach()
		{

			this._Office = default(EntityRef<Office>);
			  
		}
	}

	public partial class ONULink
	{
		public void Detach()
		{

			  
		}
	}

	public partial class OtherCost
	{
		public void Detach()
		{

			this._Installments = new EntitySet<Installment>(new Action<Installment>(this.attach_Installments), new Action<Installment>(this.detach_Installments));
			this._RequestPayments = new EntitySet<RequestPayment>(new Action<RequestPayment>(this.attach_RequestPayments), new Action<RequestPayment>(this.detach_RequestPayments));
			  
		}
	}

	public partial class PAPInfo
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._ADSLPAPFeasibilities = new EntitySet<ADSLPAPFeasibility>(new Action<ADSLPAPFeasibility>(this.attach_ADSLPAPFeasibilities), new Action<ADSLPAPFeasibility>(this.detach_ADSLPAPFeasibilities));
			this._ADSLPAPPorts = new EntitySet<ADSLPAPPort>(new Action<ADSLPAPPort>(this.attach_ADSLPAPPorts), new Action<ADSLPAPPort>(this.detach_ADSLPAPPorts));
			this._ADSLPAPRequests = new EntitySet<ADSLPAPRequest>(new Action<ADSLPAPRequest>(this.attach_ADSLPAPRequests), new Action<ADSLPAPRequest>(this.detach_ADSLPAPRequests));
			this._ADSLTelephoneNoHistories = new EntitySet<ADSLTelephoneNoHistory>(new Action<ADSLTelephoneNoHistory>(this.attach_ADSLTelephoneNoHistories), new Action<ADSLTelephoneNoHistory>(this.detach_ADSLTelephoneNoHistories));
			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._PAPInfoSpaceandPowers = new EntitySet<PAPInfoSpaceandPower>(new Action<PAPInfoSpaceandPower>(this.attach_PAPInfoSpaceandPowers), new Action<PAPInfoSpaceandPower>(this.detach_PAPInfoSpaceandPowers));
			this._PAPInfoUsers = new EntitySet<PAPInfoUser>(new Action<PAPInfoUser>(this.attach_PAPInfoUsers), new Action<PAPInfoUser>(this.detach_PAPInfoUsers));
			this._Wirelesses = new EntitySet<Wireless>(new Action<Wireless>(this.attach_Wirelesses), new Action<Wireless>(this.detach_Wirelesses));
			this._PAPInfoOperatingStatus = default(EntityRef<PAPInfoOperatingStatus>);
			  
		}
	}

	public partial class PAPInfoCost
	{
		public void Detach()
		{

			this._PAPInfoCostHistories = new EntitySet<PAPInfoCostHistory>(new Action<PAPInfoCostHistory>(this.attach_PAPInfoCostHistories), new Action<PAPInfoCostHistory>(this.detach_PAPInfoCostHistories));
			  
		}
	}

	public partial class PAPInfoCostHistory
	{
		public void Detach()
		{

			this._PAPInfoCost = default(EntityRef<PAPInfoCost>);
			  
		}
	}

	public partial class PAPInfoLimitation
	{
		public void Detach()
		{

			this._City = default(EntityRef<City>);
			  
		}
	}

	public partial class PAPInfoOperatingStatus
	{
		public void Detach()
		{

			this._PAPInfos = new EntitySet<PAPInfo>(new Action<PAPInfo>(this.attach_PAPInfos), new Action<PAPInfo>(this.detach_PAPInfos));
			  
		}
	}

	public partial class PAPInfoPort1
	{
		public void Detach()
		{

		}
	}

	public partial class PAPInfoSpaceandPower
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			this._PAPInfo = default(EntityRef<PAPInfo>);
			  
		}
	}

	public partial class PAPInfoUser
	{
		public void Detach()
		{

			this._City = default(EntityRef<City>);
			this._PAPInfo = default(EntityRef<PAPInfo>);
			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class ParazitControl
	{
		public void Detach()
		{

			this._Control = default(EntityRef<Control>);
			  
		}
	}

	public partial class PaymentFiche
	{
		public void Detach()
		{

			this._RequestPayments = new EntitySet<RequestPayment>(new Action<RequestPayment>(this.attach_RequestPayments), new Action<RequestPayment>(this.detach_RequestPayments));
			this._Installment = default(EntityRef<Installment>);
			  
		}
	}

	public partial class PCM
	{
		public void Detach()
		{

			this._CenterToCenterTranslationPCMs = new EntitySet<CenterToCenterTranslationPCM>(new Action<CenterToCenterTranslationPCM>(this.attach_CenterToCenterTranslationPCMs), new Action<CenterToCenterTranslationPCM>(this.detach_CenterToCenterTranslationPCMs));
			this._CenterToCenterTranslationPCMs1 = new EntitySet<CenterToCenterTranslationPCM>(new Action<CenterToCenterTranslationPCM>(this.attach_CenterToCenterTranslationPCMs1), new Action<CenterToCenterTranslationPCM>(this.detach_CenterToCenterTranslationPCMs1));
			this._ExchangeBrokenPCMs = new EntitySet<ExchangeBrokenPCM>(new Action<ExchangeBrokenPCM>(this.attach_ExchangeBrokenPCMs), new Action<ExchangeBrokenPCM>(this.detach_ExchangeBrokenPCMs));
			this._ExchangeBrokenPCMs1 = new EntitySet<ExchangeBrokenPCM>(new Action<ExchangeBrokenPCM>(this.attach_ExchangeBrokenPCMs1), new Action<ExchangeBrokenPCM>(this.detach_ExchangeBrokenPCMs1));
			this._PCMPorts = new EntitySet<PCMPort>(new Action<PCMPort>(this.attach_PCMPorts), new Action<PCMPort>(this.detach_PCMPorts));
			this._PCMBrand = default(EntityRef<PCMBrand>);
			this._PCMShelf = default(EntityRef<PCMShelf>);
			this._PCMType = default(EntityRef<PCMType>);
			  
		}
	}

	public partial class PCMBrand
	{
		public void Detach()
		{

			this._PCMs = new EntitySet<PCM>(new Action<PCM>(this.attach_PCMs), new Action<PCM>(this.detach_PCMs));
			  
		}
	}

	public partial class PCMDevice
	{
		public void Detach()
		{

			  
		}
	}

	public partial class PCMPort
	{
		public void Detach()
		{

			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._Malfuctions = new EntitySet<Malfuction>(new Action<Malfuction>(this.attach_Malfuctions), new Action<Malfuction>(this.detach_Malfuctions));
			this._PCM = default(EntityRef<PCM>);
			  
		}
	}

	public partial class PCMRock
	{
		public void Detach()
		{

			this._PCMShelfs = new EntitySet<PCMShelf>(new Action<PCMShelf>(this.attach_PCMShelfs), new Action<PCMShelf>(this.detach_PCMShelfs));
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class PCMShelf
	{
		public void Detach()
		{

			this._PCMs = new EntitySet<PCM>(new Action<PCM>(this.attach_PCMs), new Action<PCM>(this.detach_PCMs));
			this._PCMRock = default(EntityRef<PCMRock>);
			  
		}
	}

	public partial class PCMType
	{
		public void Detach()
		{

			this._PCMs = new EntitySet<PCM>(new Action<PCM>(this.attach_PCMs), new Action<PCM>(this.detach_PCMs));
			  
		}
	}

	public partial class Post
	{
		public void Detach()
		{

			this._AdjacentPosts = new EntitySet<AdjacentPost>(new Action<AdjacentPost>(this.attach_AdjacentPosts), new Action<AdjacentPost>(this.detach_AdjacentPosts));
			this._AdjacentPosts1 = new EntitySet<AdjacentPost>(new Action<AdjacentPost>(this.attach_AdjacentPosts1), new Action<AdjacentPost>(this.detach_AdjacentPosts1));
			this._ExchangeCabinetInputs = new EntitySet<ExchangeCabinetInput>(new Action<ExchangeCabinetInput>(this.attach_ExchangeCabinetInputs), new Action<ExchangeCabinetInput>(this.detach_ExchangeCabinetInputs));
			this._ExchangeCabinetInputConncetions = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions));
			this._ExchangeCabinetInputConncetions1 = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions1), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions1));
			this._ExchangePosts = new EntitySet<ExchangePost>(new Action<ExchangePost>(this.attach_ExchangePosts), new Action<ExchangePost>(this.detach_ExchangePosts));
			this._ExchangePosts1 = new EntitySet<ExchangePost>(new Action<ExchangePost>(this.attach_ExchangePosts1), new Action<ExchangePost>(this.detach_ExchangePosts1));
			this._InvestigatePossibilityWaitinglists = new EntitySet<InvestigatePossibilityWaitinglist>(new Action<InvestigatePossibilityWaitinglist>(this.attach_InvestigatePossibilityWaitinglists), new Action<InvestigatePossibilityWaitinglist>(this.detach_InvestigatePossibilityWaitinglists));
			this._Linesmans = new EntitySet<Linesman>(new Action<Linesman>(this.attach_Linesmans), new Action<Linesman>(this.detach_Linesmans));
			this._PostContacts = new EntitySet<PostContact>(new Action<PostContact>(this.attach_PostContacts), new Action<PostContact>(this.detach_PostContacts));
			this._TranslationOpticalCabinetToNormalConncetions = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions));
			this._TranslationOpticalCabinetToNormalConncetions1 = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions1), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions1));
			this._TranslationPosts = new EntitySet<TranslationPost>(new Action<TranslationPost>(this.attach_TranslationPosts), new Action<TranslationPost>(this.detach_TranslationPosts));
			this._TranslationPosts1 = new EntitySet<TranslationPost>(new Action<TranslationPost>(this.attach_TranslationPosts1), new Action<TranslationPost>(this.detach_TranslationPosts1));
			this._TranslationPostInputs = new EntitySet<TranslationPostInput>(new Action<TranslationPostInput>(this.attach_TranslationPostInputs), new Action<TranslationPostInput>(this.detach_TranslationPostInputs));
			this._TranslationPostInputs1 = new EntitySet<TranslationPostInput>(new Action<TranslationPostInput>(this.attach_TranslationPostInputs1), new Action<TranslationPostInput>(this.detach_TranslationPostInputs1));
			this._VisitAddresses = new EntitySet<VisitAddress>(new Action<VisitAddress>(this.attach_VisitAddresses), new Action<VisitAddress>(this.detach_VisitAddresses));
			this._VisitPlacesCabinetAndPosts = new EntitySet<VisitPlacesCabinetAndPost>(new Action<VisitPlacesCabinetAndPost>(this.attach_VisitPlacesCabinetAndPosts), new Action<VisitPlacesCabinetAndPost>(this.detach_VisitPlacesCabinetAndPosts));
			this._ExchangeGSMConnections = new EntitySet<ExchangeGSMConnection>(new Action<ExchangeGSMConnection>(this.attach_ExchangeGSMConnections), new Action<ExchangeGSMConnection>(this.detach_ExchangeGSMConnections));
			this._AORBPostAndCabinet = default(EntityRef<AORBPostAndCabinet>);
			this._Cabinet = default(EntityRef<Cabinet>);
			this._PostGroup = default(EntityRef<PostGroup>);
			this._PostStatus = default(EntityRef<PostStatus>);
			this._PostType = default(EntityRef<PostType>);
			  
		}
	}

	public partial class PostContact
	{
		public void Detach()
		{

			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._ChangeLocations = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations), new Action<ChangeLocation>(this.detach_ChangeLocations));
			this._ChangeLocationSpecialWires = new EntitySet<ChangeLocationSpecialWire>(new Action<ChangeLocationSpecialWire>(this.attach_ChangeLocationSpecialWires), new Action<ChangeLocationSpecialWire>(this.detach_ChangeLocationSpecialWires));
			this._E1s = new EntitySet<E1>(new Action<E1>(this.attach_E1s), new Action<E1>(this.detach_E1s));
			this._E1Links = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links), new Action<E1Link>(this.detach_E1Links));
			this._ExchangeCabinetInputConncetions = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions));
			this._ExchangeCabinetInputConncetions1 = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions1), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions1));
			this._ExchangePosts = new EntitySet<ExchangePost>(new Action<ExchangePost>(this.attach_ExchangePosts), new Action<ExchangePost>(this.detach_ExchangePosts));
			this._ExchangePosts1 = new EntitySet<ExchangePost>(new Action<ExchangePost>(this.attach_ExchangePosts1), new Action<ExchangePost>(this.detach_ExchangePosts1));
			this._InvestigatePossibilities = new EntitySet<InvestigatePossibility>(new Action<InvestigatePossibility>(this.attach_InvestigatePossibilities), new Action<InvestigatePossibility>(this.detach_InvestigatePossibilities));
			this._Malfuctions = new EntitySet<Malfuction>(new Action<Malfuction>(this.attach_Malfuctions), new Action<Malfuction>(this.detach_Malfuctions));
			this._PostContactEquipments = new EntitySet<PostContactEquipment>(new Action<PostContactEquipment>(this.attach_PostContactEquipments), new Action<PostContactEquipment>(this.detach_PostContactEquipments));
			this._RefundDeposits = new EntitySet<RefundDeposit>(new Action<RefundDeposit>(this.attach_RefundDeposits), new Action<RefundDeposit>(this.detach_RefundDeposits));
			this._TranslationOpticalCabinetToNormalConncetions = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions));
			this._TranslationOpticalCabinetToNormalConncetions1 = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions1), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions1));
			this._TranslationPosts = new EntitySet<TranslationPost>(new Action<TranslationPost>(this.attach_TranslationPosts), new Action<TranslationPost>(this.detach_TranslationPosts));
			this._TranslationPosts1 = new EntitySet<TranslationPost>(new Action<TranslationPost>(this.attach_TranslationPosts1), new Action<TranslationPost>(this.detach_TranslationPosts1));
			this._TranslationPostInputConnections = new EntitySet<TranslationPostInputConnection>(new Action<TranslationPostInputConnection>(this.attach_TranslationPostInputConnections), new Action<TranslationPostInputConnection>(this.detach_TranslationPostInputConnections));
			this._TranslationPostInputConnections1 = new EntitySet<TranslationPostInputConnection>(new Action<TranslationPostInputConnection>(this.attach_TranslationPostInputConnections1), new Action<TranslationPostInputConnection>(this.detach_TranslationPostInputConnections1));
			this._VacateSpecialWires = new EntitySet<VacateSpecialWire>(new Action<VacateSpecialWire>(this.attach_VacateSpecialWires), new Action<VacateSpecialWire>(this.detach_VacateSpecialWires));
			this._ExchangeGSMConnections = new EntitySet<ExchangeGSMConnection>(new Action<ExchangeGSMConnection>(this.attach_ExchangeGSMConnections), new Action<ExchangeGSMConnection>(this.detach_ExchangeGSMConnections));
			this._CableColor = default(EntityRef<CableColor>);
			this._CableColor1 = default(EntityRef<CableColor>);
			this._Post = default(EntityRef<Post>);
			this._PostContactConnectionType = default(EntityRef<PostContactConnectionType>);
			this._PostContactStatus = default(EntityRef<PostContactStatus>);
			  
		}
	}

	public partial class PostContactConnectionType
	{
		public void Detach()
		{

			this._PostContacts = new EntitySet<PostContact>(new Action<PostContact>(this.attach_PostContacts), new Action<PostContact>(this.detach_PostContacts));
			  
		}
	}

	public partial class PostContactEquipment
	{
		public void Detach()
		{

			this._PostContact = default(EntityRef<PostContact>);
			this._UsedProduct = default(EntityRef<UsedProduct>);
			  
		}
	}

	public partial class PostContactStatus
	{
		public void Detach()
		{

			this._PostContacts = new EntitySet<PostContact>(new Action<PostContact>(this.attach_PostContacts), new Action<PostContact>(this.detach_PostContacts));
			  
		}
	}

	public partial class PostGroup
	{
		public void Detach()
		{

			this._Posts = new EntitySet<Post>(new Action<Post>(this.attach_Posts), new Action<Post>(this.detach_Posts));
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class PostStatus
	{
		public void Detach()
		{

			this._Posts = new EntitySet<Post>(new Action<Post>(this.attach_Posts), new Action<Post>(this.detach_Posts));
			  
		}
	}

	public partial class PostType
	{
		public void Detach()
		{

			this._Posts = new EntitySet<Post>(new Action<Post>(this.attach_Posts), new Action<Post>(this.detach_Posts));
			  
		}
	}

	public partial class PowerOffice
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class PowerType
	{
		public void Detach()
		{

			this._SpaceAndPowerPowerTypes = new EntitySet<SpaceAndPowerPowerType>(new Action<SpaceAndPowerPowerType>(this.attach_SpaceAndPowerPowerTypes), new Action<SpaceAndPowerPowerType>(this.detach_SpaceAndPowerPowerTypes));
			  
		}
	}

	public partial class Province
	{
		public void Detach()
		{

			this._Cities = new EntitySet<City>(new Action<City>(this.attach_Cities), new Action<City>(this.detach_Cities));
			  
		}
	}

	public partial class Query
	{
		public void Detach()
		{

		}
	}

	public partial class QuotaDiscount
	{
		public void Detach()
		{

			this._BaseCosts = new EntitySet<BaseCost>(new Action<BaseCost>(this.attach_BaseCosts), new Action<BaseCost>(this.detach_BaseCosts));
			this._Announce = default(EntityRef<Announce>);
			this._QuotaJobTitle = default(EntityRef<QuotaJobTitle>);
			this._RequestType = default(EntityRef<RequestType>);
			  
		}
	}

	public partial class QuotaJobTitle
	{
		public void Detach()
		{

			this._QuotaDiscounts = new EntitySet<QuotaDiscount>(new Action<QuotaDiscount>(this.attach_QuotaDiscounts), new Action<QuotaDiscount>(this.detach_QuotaDiscounts));
			  
		}
	}

	public partial class ReferenceDocument
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._RequestDocument = default(EntityRef<RequestDocument>);
			  
		}
	}

	public partial class RefundDeposit
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Bucht = default(EntityRef<Bucht>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._CauseOfRefundDeposit = default(EntityRef<CauseOfRefundDeposit>);
			this._PostContact = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			this._SwitchPort = default(EntityRef<SwitchPort>);
			  
		}
	}

	public partial class Region
	{
		public void Detach()
		{

			this._Centers = new EntitySet<Center>(new Action<Center>(this.attach_Centers), new Action<Center>(this.detach_Centers));
			this._City = default(EntityRef<City>);
			  
		}
	}

	public partial class RegularExpression
	{
		public void Detach()
		{

			  
		}
	}

	public partial class RelationalOperator
	{
		public void Detach()
		{

			this._FilterRules = new EntitySet<FilterRule>(new Action<FilterRule>(this.attach_FilterRules), new Action<FilterRule>(this.detach_FilterRules));
			  
		}
	}

	public partial class ReportAndAnnouncement
	{
		public void Detach()
		{

			this._ReportType = default(EntityRef<ReportType>);
			  
		}
	}

	public partial class ReportTemplate
	{
		public void Detach()
		{

			this._RoleReportTemplates = new EntitySet<RoleReportTemplate>(new Action<RoleReportTemplate>(this.attach_RoleReportTemplates), new Action<RoleReportTemplate>(this.detach_RoleReportTemplates));
			  
		}
	}

	public partial class ReportType
	{
		public void Detach()
		{

			this._ReportAndAnnouncements = new EntitySet<ReportAndAnnouncement>(new Action<ReportAndAnnouncement>(this.attach_ReportAndAnnouncements), new Action<ReportAndAnnouncement>(this.detach_ReportAndAnnouncements));
			  
		}
	}

	public partial class Request
	{
		public void Detach()
		{

			this._ADSLChangeCustomerOwnerCharacteristic = default(EntityRef<ADSLChangeCustomerOwnerCharacteristic>);
			this._ADSLChangeIPRequest = default(EntityRef<ADSLChangeIPRequest>);
			this._ADSLChangePlace = default(EntityRef<ADSLChangePlace>);
			this._ADSLChangePort1 = default(EntityRef<ADSLChangePort1>);
			this._ADSLChangeService = default(EntityRef<ADSLChangeService>);
			this._ADSLCutTemporary = default(EntityRef<ADSLCutTemporary>);
			this._ADSLDischarge = default(EntityRef<ADSLDischarge>);
			this._ADSLInstallRequest = default(EntityRef<ADSLInstallRequest>);
			this._ADSLPAPRequest = default(EntityRef<ADSLPAPRequest>);
			this._ADSLRequest = default(EntityRef<ADSLRequest>);
			this._ADSLSellerAgentUserCredits = new EntitySet<ADSLSellerAgentUserCredit>(new Action<ADSLSellerAgentUserCredit>(this.attach_ADSLSellerAgentUserCredits), new Action<ADSLSellerAgentUserCredit>(this.detach_ADSLSellerAgentUserCredits));
			this._ADSLSellTraffic = default(EntityRef<ADSLSellTraffic>);
			this._ADSLSetupContactInformations = new EntitySet<ADSLSetupContactInformation>(new Action<ADSLSetupContactInformation>(this.attach_ADSLSetupContactInformations), new Action<ADSLSetupContactInformation>(this.detach_ADSLSetupContactInformations));
			this._ADSLSupportRequest = default(EntityRef<ADSLSupportRequest>);
			this._AnnounceTo118 = default(EntityRef<AnnounceTo118>);
			this._BuchtSwitching = default(EntityRef<BuchtSwitching>);
			this._CableDesignOffices = new EntitySet<CableDesignOffice>(new Action<CableDesignOffice>(this.attach_CableDesignOffices), new Action<CableDesignOffice>(this.detach_CableDesignOffices));
			this._CancelationRequestList = default(EntityRef<CancelationRequestList>);
			this._CenterToCenterTranslation = default(EntityRef<CenterToCenterTranslation>);
			this._CenterToCenterTranslationPCMs = new EntitySet<CenterToCenterTranslationPCM>(new Action<CenterToCenterTranslationPCM>(this.attach_CenterToCenterTranslationPCMs), new Action<CenterToCenterTranslationPCM>(this.detach_CenterToCenterTranslationPCMs));
			this._CenterToCenterTranslationTelephones = new EntitySet<CenterToCenterTranslationTelephone>(new Action<CenterToCenterTranslationTelephone>(this.attach_CenterToCenterTranslationTelephones), new Action<CenterToCenterTranslationTelephone>(this.detach_CenterToCenterTranslationTelephones));
			this._ChangeAddress = default(EntityRef<ChangeAddress>);
			this._ChangeLocation = default(EntityRef<ChangeLocation>);
			this._ChangeLocationSpecialWire = default(EntityRef<ChangeLocationSpecialWire>);
			this._ChangeName = default(EntityRef<ChangeName>);
			this._ChangeNo = default(EntityRef<ChangeNo>);
			this._ChangeTelephoneType = default(EntityRef<ChangeTelephoneType>);
			this._Contracts = new EntitySet<Contract>(new Action<Contract>(this.attach_Contracts), new Action<Contract>(this.detach_Contracts));
			this._Control = default(EntityRef<Control>);
			this._CutAndEstablish = default(EntityRef<CutAndEstablish>);
			this._DamageLine = default(EntityRef<DamageLine>);
			this._E1 = default(EntityRef<E1>);
			this._E1Links = new EntitySet<E1Link>(new Action<E1Link>(this.attach_E1Links), new Action<E1Link>(this.detach_E1Links));
			this._ExchangeBrokenPCMs = new EntitySet<ExchangeBrokenPCM>(new Action<ExchangeBrokenPCM>(this.attach_ExchangeBrokenPCMs), new Action<ExchangeBrokenPCM>(this.detach_ExchangeBrokenPCMs));
			this._ExchangeCabinetInput = default(EntityRef<ExchangeCabinetInput>);
			this._ExchangeCabinetInputConncetions = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions));
			this._ExchangeCentralCableMDFConncetions = new EntitySet<ExchangeCentralCableMDFConncetion>(new Action<ExchangeCentralCableMDFConncetion>(this.attach_ExchangeCentralCableMDFConncetions), new Action<ExchangeCentralCableMDFConncetion>(this.detach_ExchangeCentralCableMDFConncetions));
			this._ExchangePost = default(EntityRef<ExchangePost>);
			this._ExchangeTelephoneNos = new EntitySet<ExchangeTelephoneNo>(new Action<ExchangeTelephoneNo>(this.attach_ExchangeTelephoneNos), new Action<ExchangeTelephoneNo>(this.detach_ExchangeTelephoneNos));
			this._Failure117 = default(EntityRef<Failure117>);
			this._Inquiries = new EntitySet<Inquiry>(new Action<Inquiry>(this.attach_Inquiries), new Action<Inquiry>(this.detach_Inquiries));
			this._Installment = default(EntityRef<Installment>);
			this._InvestigatePossibilities = new EntitySet<InvestigatePossibility>(new Action<InvestigatePossibility>(this.attach_InvestigatePossibilities), new Action<InvestigatePossibility>(this.detach_InvestigatePossibilities));
			this._IssueWirings = new EntitySet<IssueWiring>(new Action<IssueWiring>(this.attach_IssueWirings), new Action<IssueWiring>(this.detach_IssueWirings));
			this._MDFWiring = default(EntityRef<MDFWiring>);
			this._Mortgages = new EntitySet<Mortgage>(new Action<Mortgage>(this.attach_Mortgages), new Action<Mortgage>(this.detach_Mortgages));
			this._Mortgages1 = new EntitySet<Mortgage>(new Action<Mortgage>(this.attach_Mortgages1), new Action<Mortgage>(this.detach_Mortgages1));
			this._NetworkWiring = default(EntityRef<NetworkWiring>);
			this._PowerOffices = new EntitySet<PowerOffice>(new Action<PowerOffice>(this.attach_PowerOffices), new Action<PowerOffice>(this.detach_PowerOffices));
			this._ReferenceDocuments = new EntitySet<ReferenceDocument>(new Action<ReferenceDocument>(this.attach_ReferenceDocuments), new Action<ReferenceDocument>(this.detach_ReferenceDocuments));
			this._RefundDeposit = default(EntityRef<RefundDeposit>);
			this._Requests = new EntitySet<Request>(new Action<Request>(this.attach_Requests), new Action<Request>(this.detach_Requests));
			this._Requests1 = new EntitySet<Request>(new Action<Request>(this.attach_Requests1), new Action<Request>(this.detach_Requests1));
			this._Request4 = default(EntityRef<Request>);
			this._RequestForms = new EntitySet<RequestForm>(new Action<RequestForm>(this.attach_RequestForms), new Action<RequestForm>(this.detach_RequestForms));
			this._RequestLogs = new EntitySet<RequestLog>(new Action<RequestLog>(this.attach_RequestLogs), new Action<RequestLog>(this.detach_RequestLogs));
			this._RequestPayments = new EntitySet<RequestPayment>(new Action<RequestPayment>(this.attach_RequestPayments), new Action<RequestPayment>(this.detach_RequestPayments));
			this._SelectTelephone = default(EntityRef<SelectTelephone>);
			this._SpaceAndPower = default(EntityRef<SpaceAndPower>);
			this._SpecialCondition = default(EntityRef<SpecialCondition>);
			this._SpecialPrivateCables = new EntitySet<SpecialPrivateCable>(new Action<SpecialPrivateCable>(this.attach_SpecialPrivateCables), new Action<SpecialPrivateCable>(this.detach_SpecialPrivateCables));
			this._SpecialService = default(EntityRef<SpecialService>);
			this._SpecialWire = default(EntityRef<SpecialWire>);
			this._SpecialWirePoints = new EntitySet<SpecialWirePoint>(new Action<SpecialWirePoint>(this.attach_SpecialWirePoints), new Action<SpecialWirePoint>(this.detach_SpecialWirePoints));
			this._StatusLogs = new EntitySet<StatusLog>(new Action<StatusLog>(this.attach_StatusLogs), new Action<StatusLog>(this.detach_StatusLogs));
			this._SubFlowStatus = new EntitySet<SubFlowStatus>(new Action<SubFlowStatus>(this.attach_SubFlowStatus), new Action<SubFlowStatus>(this.detach_SubFlowStatus));
			this._SwapPCM = default(EntityRef<SwapPCM>);
			this._SwapTelephone = default(EntityRef<SwapTelephone>);
			this._SwitchOffices = new EntitySet<SwitchOffice>(new Action<SwitchOffice>(this.attach_SwitchOffices), new Action<SwitchOffice>(this.detach_SwitchOffices));
			this._SwitchTransitions = new EntitySet<SwitchTransition>(new Action<SwitchTransition>(this.attach_SwitchTransitions), new Action<SwitchTransition>(this.detach_SwitchTransitions));
			this._TakePossession = default(EntityRef<TakePossession>);
			this._TitleIn118 = default(EntityRef<TitleIn118>);
			this._TransferDepartmentOffices = new EntitySet<TransferDepartmentOffice>(new Action<TransferDepartmentOffice>(this.attach_TransferDepartmentOffices), new Action<TransferDepartmentOffice>(this.detach_TransferDepartmentOffices));
			this._TransferFileInfo = default(EntityRef<TransferFileInfo>);
			this._TranslationOpticalCabinetToNormal = default(EntityRef<TranslationOpticalCabinetToNormal>);
			this._TranslationOpticalCabinetToNormalConncetions = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions));
			this._TranslationOpticalCabinetToNormalTelephones = new EntitySet<TranslationOpticalCabinetToNormalTelephone>(new Action<TranslationOpticalCabinetToNormalTelephone>(this.attach_TranslationOpticalCabinetToNormalTelephones), new Action<TranslationOpticalCabinetToNormalTelephone>(this.detach_TranslationOpticalCabinetToNormalTelephones));
			this._TranslationPCMToNormal = default(EntityRef<TranslationPCMToNormal>);
			this._TranslationPost = default(EntityRef<TranslationPost>);
			this._TranslationPostInput = default(EntityRef<TranslationPostInput>);
			this._TranslationPostInputConnections = new EntitySet<TranslationPostInputConnection>(new Action<TranslationPostInputConnection>(this.attach_TranslationPostInputConnections), new Action<TranslationPostInputConnection>(this.detach_TranslationPostInputConnections));
			this._VacateE1s = new EntitySet<VacateE1>(new Action<VacateE1>(this.attach_VacateE1s), new Action<VacateE1>(this.detach_VacateE1s));
			this._VacateSpecialWire = default(EntityRef<VacateSpecialWire>);
			this._VacateSpecialWirePoints = new EntitySet<VacateSpecialWirePoint>(new Action<VacateSpecialWirePoint>(this.attach_VacateSpecialWirePoints), new Action<VacateSpecialWirePoint>(this.detach_VacateSpecialWirePoints));
			this._VisitAddresses = new EntitySet<VisitAddress>(new Action<VisitAddress>(this.attach_VisitAddresses), new Action<VisitAddress>(this.detach_VisitAddresses));
			this._WaitingLists = new EntitySet<WaitingList>(new Action<WaitingList>(this.attach_WaitingLists), new Action<WaitingList>(this.detach_WaitingLists));
			this._Wireless = default(EntityRef<Wireless>);
			this._WirelessChangeService = default(EntityRef<WirelessChangeService>);
			this._WirelessRequest = default(EntityRef<WirelessRequest>);
			this._WirelessSellTraffic = default(EntityRef<WirelessSellTraffic>);
			this._Wirings = new EntitySet<Wiring>(new Action<Wiring>(this.attach_Wirings), new Action<Wiring>(this.detach_Wirings));
			this._WorkFlowLogs = new EntitySet<WorkFlowLog>(new Action<WorkFlowLog>(this.attach_WorkFlowLogs), new Action<WorkFlowLog>(this.detach_WorkFlowLogs));
			this._ExchangeGSM = default(EntityRef<ExchangeGSM>);
			this._TelecomminucationServicePayments = new EntitySet<TelecomminucationServicePayment>(new Action<TelecomminucationServicePayment>(this.attach_TelecomminucationServicePayments), new Action<TelecomminucationServicePayment>(this.detach_TelecomminucationServicePayments));
			this._ZeroStatus = default(EntityRef<ZeroStatus>);
			this._InstallRequests = new EntitySet<InstallRequest>(new Action<InstallRequest>(this.attach_InstallRequests), new Action<InstallRequest>(this.detach_InstallRequests));
			this._Center = default(EntityRef<Center>);
			this._Request1 = default(EntityRef<Request>);
			this._Request2 = default(EntityRef<Request>);
			this._Request3 = default(EntityRef<Request>);
			this._RequestType = default(EntityRef<RequestType>);
			this._Status = default(EntityRef<Status>);
			this._Telephone = default(EntityRef<Telephone>);
			this._User = default(EntityRef<User>);
			this._User1 = default(EntityRef<User>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class RequestActionLog
	{
		public void Detach()
		{

			  
		}
	}

	public partial class RequestDocument
	{
		public void Detach()
		{

			this._Contracts = new EntitySet<Contract>(new Action<Contract>(this.attach_Contracts), new Action<Contract>(this.detach_Contracts));
			this._ReferenceDocuments = new EntitySet<ReferenceDocument>(new Action<ReferenceDocument>(this.attach_ReferenceDocuments), new Action<ReferenceDocument>(this.detach_ReferenceDocuments));
			this._DocumentRequestType = default(EntityRef<DocumentRequestType>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class RequestForm
	{
		public void Detach()
		{

			this._FormTemplate = default(EntityRef<FormTemplate>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class RequestLog
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._RequestType = default(EntityRef<RequestType>);
			  
		}
	}

	public partial class RequestPayment
	{
		public void Detach()
		{

			this._InstallmentRequestPayments = new EntitySet<InstallmentRequestPayment>(new Action<InstallmentRequestPayment>(this.attach_InstallmentRequestPayments), new Action<InstallmentRequestPayment>(this.detach_InstallmentRequestPayments));
			this._TelephoneConnectionInstallments = new EntitySet<TelephoneConnectionInstallment>(new Action<TelephoneConnectionInstallment>(this.attach_TelephoneConnectionInstallments), new Action<TelephoneConnectionInstallment>(this.detach_TelephoneConnectionInstallments));
			this._Bank = default(EntityRef<Bank>);
			this._BankBranch = default(EntityRef<BankBranch>);
			this._BaseCost = default(EntityRef<BaseCost>);
			this._PaymentFiche = default(EntityRef<PaymentFiche>);
			this._OtherCost = default(EntityRef<OtherCost>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class RequestRefund
	{
		public void Detach()
		{

		}
	}

	public partial class RequestRejectReason
	{
		public void Detach()
		{

			this._StatusLogs = new EntitySet<StatusLog>(new Action<StatusLog>(this.attach_StatusLogs), new Action<StatusLog>(this.detach_StatusLogs));
			this._RequestStep = default(EntityRef<RequestStep>);
			  
		}
	}

	public partial class RequestStep
	{
		public void Detach()
		{

			this._RequestRejectReasons = new EntitySet<RequestRejectReason>(new Action<RequestRejectReason>(this.attach_RequestRejectReasons), new Action<RequestRejectReason>(this.detach_RequestRejectReasons));
			this._RoleRequestSteps = new EntitySet<RoleRequestStep>(new Action<RoleRequestStep>(this.attach_RoleRequestSteps), new Action<RoleRequestStep>(this.detach_RoleRequestSteps));
			this._Status = new EntitySet<Status>(new Action<Status>(this.attach_Status), new Action<Status>(this.detach_Status));
			this._RequestType = default(EntityRef<RequestType>);
			  
		}
	}

	public partial class RequestType
	{
		public void Detach()
		{

			this._BaseCosts = new EntitySet<BaseCost>(new Action<BaseCost>(this.attach_BaseCosts), new Action<BaseCost>(this.detach_BaseCosts));
			this._DocumentRequestTypes = new EntitySet<DocumentRequestType>(new Action<DocumentRequestType>(this.attach_DocumentRequestTypes), new Action<DocumentRequestType>(this.detach_DocumentRequestTypes));
			this._QuotaDiscounts = new EntitySet<QuotaDiscount>(new Action<QuotaDiscount>(this.attach_QuotaDiscounts), new Action<QuotaDiscount>(this.detach_QuotaDiscounts));
			this._Requests = new EntitySet<Request>(new Action<Request>(this.attach_Requests), new Action<Request>(this.detach_Requests));
			this._RequestLogs = new EntitySet<RequestLog>(new Action<RequestLog>(this.attach_RequestLogs), new Action<RequestLog>(this.detach_RequestLogs));
			this._RequestSteps = new EntitySet<RequestStep>(new Action<RequestStep>(this.attach_RequestSteps), new Action<RequestStep>(this.detach_RequestSteps));
			this._TelecomminucationServices = new EntitySet<TelecomminucationService>(new Action<TelecomminucationService>(this.attach_TelecomminucationServices), new Action<TelecomminucationService>(this.detach_TelecomminucationServices));
			this._WaitingListReasons = new EntitySet<WaitingListReason>(new Action<WaitingListReason>(this.attach_WaitingListReasons), new Action<WaitingListReason>(this.detach_WaitingListReasons));
			this._WorkFlowRules = new EntitySet<WorkFlowRule>(new Action<WorkFlowRule>(this.attach_WorkFlowRules), new Action<WorkFlowRule>(this.detach_WorkFlowRules));
			this._InstallRequests = new EntitySet<InstallRequest>(new Action<InstallRequest>(this.attach_InstallRequests), new Action<InstallRequest>(this.detach_InstallRequests));
			  
		}
	}

	public partial class Resource
	{
		public void Detach()
		{

			this._Resources = new EntitySet<Resource>(new Action<Resource>(this.attach_Resources), new Action<Resource>(this.detach_Resources));
			this._RoleResources = new EntitySet<RoleResource>(new Action<RoleResource>(this.attach_RoleResources), new Action<RoleResource>(this.detach_RoleResources));
			this._Resource1 = default(EntityRef<Resource>);
			  
		}
	}

	public partial class Rock
	{
		public void Detach()
		{

			this._Shelfs = new EntitySet<Shelf>(new Action<Shelf>(this.attach_Shelfs), new Action<Shelf>(this.detach_Shelfs));
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class RoleReportTemplate
	{
		public void Detach()
		{

			this._ReportTemplate = default(EntityRef<ReportTemplate>);
			this._Role = default(EntityRef<Role>);
			  
		}
	}

	public partial class RoleRequestStep
	{
		public void Detach()
		{

			this._RequestStep = default(EntityRef<RequestStep>);
			this._Role = default(EntityRef<Role>);
			  
		}
	}

	public partial class RoleResource
	{
		public void Detach()
		{

			this._Resource = default(EntityRef<Resource>);
			this._Role = default(EntityRef<Role>);
			  
		}
	}

	public partial class RoundSaleInfo
	{
		public void Detach()
		{

			this._TelRoundSales = new EntitySet<TelRoundSale>(new Action<TelRoundSale>(this.attach_TelRoundSales), new Action<TelRoundSale>(this.detach_TelRoundSales));
			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class SavedLog
	{
		public void Detach()
		{

			  
		}
	}

	public partial class SelectTelephone
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._SwitchPort = default(EntityRef<SwitchPort>);
			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class ServiceMethod
	{
		public void Detach()
		{

			this._ServiceUserMethods = new EntitySet<ServiceUserMethod>(new Action<ServiceUserMethod>(this.attach_ServiceUserMethods), new Action<ServiceUserMethod>(this.detach_ServiceUserMethods));
			  
		}
	}

	public partial class ServiceUser
	{
		public void Detach()
		{

			this._ServiceUserMethods = new EntitySet<ServiceUserMethod>(new Action<ServiceUserMethod>(this.attach_ServiceUserMethods), new Action<ServiceUserMethod>(this.detach_ServiceUserMethods));
			  
		}
	}

	public partial class ServiceUserMethod
	{
		public void Detach()
		{

			this._ServiceMethod = default(EntityRef<ServiceMethod>);
			this._ServiceUser = default(EntityRef<ServiceUser>);
			  
		}
	}

	public partial class Setting
	{
		public void Detach()
		{

			  
		}
	}

	public partial class Shelf
	{
		public void Detach()
		{

			this._ADSLEquipments = new EntitySet<ADSLEquipment>(new Action<ADSLEquipment>(this.attach_ADSLEquipments), new Action<ADSLEquipment>(this.detach_ADSLEquipments));
			this._Rock = default(EntityRef<Rock>);
			  
		}
	}

	public partial class SMSService
	{
		public void Detach()
		{

			  
		}
	}

	public partial class SpaceAndPower
	{
		public void Detach()
		{

			this._SpaceAndPowerPowerTypes = new EntitySet<SpaceAndPowerPowerType>(new Action<SpaceAndPowerPowerType>(this.attach_SpaceAndPowerPowerTypes), new Action<SpaceAndPowerPowerType>(this.detach_SpaceAndPowerPowerTypes));
			this._Antennas = new EntitySet<Antenna>(new Action<Antenna>(this.attach_Antennas), new Action<Antenna>(this.detach_Antennas));
			this._Address = default(EntityRef<Address>);
			this._Request = default(EntityRef<Request>);
			this._User = default(EntityRef<User>);
			this._User1 = default(EntityRef<User>);
			this._User2 = default(EntityRef<User>);
			this._User3 = default(EntityRef<User>);
			this._User4 = default(EntityRef<User>);
			this._User5 = default(EntityRef<User>);
			this._User6 = default(EntityRef<User>);
			this._User7 = default(EntityRef<User>);
			this._User8 = default(EntityRef<User>);
			this._User9 = default(EntityRef<User>);
			this._User10 = default(EntityRef<User>);
			this._User11 = default(EntityRef<User>);
			this._User12 = default(EntityRef<User>);
			this._User13 = default(EntityRef<User>);
			this._User14 = default(EntityRef<User>);
			this._User15 = default(EntityRef<User>);
			this._User16 = default(EntityRef<User>);
			this._User17 = default(EntityRef<User>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class SpaceAndPowerCustomer
	{
		public void Detach()
		{

			  
		}
	}

	public partial class SpaceAndPowerPowerType
	{
		public void Detach()
		{

			this._PowerType = default(EntityRef<PowerType>);
			this._SpaceAndPower = default(EntityRef<SpaceAndPower>);
			  
		}
	}

	public partial class SpecialConditionPrioritize
	{
		public void Detach()
		{

			  
		}
	}

	public partial class SpecialCondition
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class SpecialPrivateCable
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			this._Request = default(EntityRef<Request>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class SpecialService
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class SpecialServiceType
	{
		public void Detach()
		{

			this._SwitchSpecialServices = new EntitySet<SwitchSpecialService>(new Action<SwitchSpecialService>(this.attach_SwitchSpecialServices), new Action<SwitchSpecialService>(this.detach_SwitchSpecialServices));
			this._TelephoneSpecialServiceTypes = new EntitySet<TelephoneSpecialServiceType>(new Action<TelephoneSpecialServiceType>(this.attach_TelephoneSpecialServiceTypes), new Action<TelephoneSpecialServiceType>(this.detach_TelephoneSpecialServiceTypes));
			  
		}
	}

	public partial class SpecialWire
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Bucht = default(EntityRef<Bucht>);
			this._BuchtType1 = default(EntityRef<BuchtType>);
			  
		}
	}

	public partial class SpecialWireAddress
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class SpecialWirePoint
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Center = default(EntityRef<Center>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class Status
	{
		public void Detach()
		{

			this._InstallLines = new EntitySet<InstallLine>(new Action<InstallLine>(this.attach_InstallLines), new Action<InstallLine>(this.detach_InstallLines));
			this._Requests = new EntitySet<Request>(new Action<Request>(this.attach_Requests), new Action<Request>(this.detach_Requests));
			this._Status2 = default(EntityRef<Status>);
			this._StatusLogs = new EntitySet<StatusLog>(new Action<StatusLog>(this.attach_StatusLogs), new Action<StatusLog>(this.detach_StatusLogs));
			this._StatusLogs1 = new EntitySet<StatusLog>(new Action<StatusLog>(this.attach_StatusLogs1), new Action<StatusLog>(this.detach_StatusLogs1));
			this._SubFlowStatus = new EntitySet<SubFlowStatus>(new Action<SubFlowStatus>(this.attach_SubFlowStatus), new Action<SubFlowStatus>(this.detach_SubFlowStatus));
			this._WaitingLists = new EntitySet<WaitingList>(new Action<WaitingList>(this.attach_WaitingLists), new Action<WaitingList>(this.detach_WaitingLists));
			this._Wirings = new EntitySet<Wiring>(new Action<Wiring>(this.attach_Wirings), new Action<Wiring>(this.detach_Wirings));
			this._Wirings1 = new EntitySet<Wiring>(new Action<Wiring>(this.attach_Wirings1), new Action<Wiring>(this.detach_Wirings1));
			this._Wirings2 = new EntitySet<Wiring>(new Action<Wiring>(this.attach_Wirings2), new Action<Wiring>(this.detach_Wirings2));
			this._WorkFlowRules = new EntitySet<WorkFlowRule>(new Action<WorkFlowRule>(this.attach_WorkFlowRules), new Action<WorkFlowRule>(this.detach_WorkFlowRules));
			this._WorkFlowRules1 = new EntitySet<WorkFlowRule>(new Action<WorkFlowRule>(this.attach_WorkFlowRules1), new Action<WorkFlowRule>(this.detach_WorkFlowRules1));
			this._RequestStep = default(EntityRef<RequestStep>);
			this._Status1 = default(EntityRef<Status>);
			  
		}
	}

	public partial class StatusLog
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._RequestRejectReason = default(EntityRef<RequestRejectReason>);
			this._Status = default(EntityRef<Status>);
			this._Status1 = default(EntityRef<Status>);
			  
		}
	}

	public partial class SubFlowStatus
	{
		public void Detach()
		{

			this._SubFlowStatus2 = new EntitySet<SubFlowStatus>(new Action<SubFlowStatus>(this.attach_SubFlowStatus2), new Action<SubFlowStatus>(this.detach_SubFlowStatus2));
			this._Request = default(EntityRef<Request>);
			this._Status = default(EntityRef<Status>);
			this._SubFlowStatus1 = default(EntityRef<SubFlowStatus>);
			  
		}
	}

	public partial class SubsidiaryCode
	{
		public void Detach()
		{

			this._Centers = new EntitySet<Center>(new Action<Center>(this.attach_Centers), new Action<Center>(this.detach_Centers));
			this._Centers1 = new EntitySet<Center>(new Action<Center>(this.attach_Centers1), new Action<Center>(this.detach_Centers1));
			this._Centers2 = new EntitySet<Center>(new Action<Center>(this.attach_Centers2), new Action<Center>(this.detach_Centers2));
			  
		}
	}

	public partial class SugesstionPossibility
	{
		public void Detach()
		{

			this._MatchPossibilities = new EntitySet<MatchPossibility>(new Action<MatchPossibility>(this.attach_MatchPossibilities), new Action<MatchPossibility>(this.detach_MatchPossibilities));
			this._VisitAddress = default(EntityRef<VisitAddress>);
			  
		}
	}

	public partial class SwapPCM
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Telephone1 = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class SwapTelephone
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Telephone1 = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class Switch
	{
		public void Detach()
		{

			this._E1s = new EntitySet<E1>(new Action<E1>(this.attach_E1s), new Action<E1>(this.detach_E1s));
			this._ExchangeTelephoneNos = new EntitySet<ExchangeTelephoneNo>(new Action<ExchangeTelephoneNo>(this.attach_ExchangeTelephoneNos), new Action<ExchangeTelephoneNo>(this.detach_ExchangeTelephoneNos));
			this._ExchangeTelephoneNos1 = new EntitySet<ExchangeTelephoneNo>(new Action<ExchangeTelephoneNo>(this.attach_ExchangeTelephoneNos1), new Action<ExchangeTelephoneNo>(this.detach_ExchangeTelephoneNos1));
			this._SwitchPorts = new EntitySet<SwitchPort>(new Action<SwitchPort>(this.attach_SwitchPorts), new Action<SwitchPort>(this.detach_SwitchPorts));
			this._SwitchPrecodes = new EntitySet<SwitchPrecode>(new Action<SwitchPrecode>(this.attach_SwitchPrecodes), new Action<SwitchPrecode>(this.detach_SwitchPrecodes));
			this._SwitchSpecialServices = new EntitySet<SwitchSpecialService>(new Action<SwitchSpecialService>(this.attach_SwitchSpecialServices), new Action<SwitchSpecialService>(this.detach_SwitchSpecialServices));
			this._SwitchTransitions = new EntitySet<SwitchTransition>(new Action<SwitchTransition>(this.attach_SwitchTransitions), new Action<SwitchTransition>(this.detach_SwitchTransitions));
			this._Center = default(EntityRef<Center>);
			this._SwitchType = default(EntityRef<SwitchType>);
			  
		}
	}

	public partial class SwitchOffice
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class SwitchPort
	{
		public void Detach()
		{

			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._ChangeLocationSpecialWires = new EntitySet<ChangeLocationSpecialWire>(new Action<ChangeLocationSpecialWire>(this.attach_ChangeLocationSpecialWires), new Action<ChangeLocationSpecialWire>(this.detach_ChangeLocationSpecialWires));
			this._RefundDeposits = new EntitySet<RefundDeposit>(new Action<RefundDeposit>(this.attach_RefundDeposits), new Action<RefundDeposit>(this.detach_RefundDeposits));
			this._SelectTelephones = new EntitySet<SelectTelephone>(new Action<SelectTelephone>(this.attach_SelectTelephones), new Action<SelectTelephone>(this.detach_SelectTelephones));
			this._Telephones = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones), new Action<Telephone>(this.detach_Telephones));
			this._VacateSpecialWires = new EntitySet<VacateSpecialWire>(new Action<VacateSpecialWire>(this.attach_VacateSpecialWires), new Action<VacateSpecialWire>(this.detach_VacateSpecialWires));
			this._Switch = default(EntityRef<Switch>);
			  
		}
	}

	public partial class SwitchPrecode
	{
		public void Detach()
		{

			this._E1s = new EntitySet<E1>(new Action<E1>(this.attach_E1s), new Action<E1>(this.detach_E1s));
			this._ExchangeTelephoneNos = new EntitySet<ExchangeTelephoneNo>(new Action<ExchangeTelephoneNo>(this.attach_ExchangeTelephoneNos), new Action<ExchangeTelephoneNo>(this.detach_ExchangeTelephoneNos));
			this._ExchangeTelephoneNos1 = new EntitySet<ExchangeTelephoneNo>(new Action<ExchangeTelephoneNo>(this.attach_ExchangeTelephoneNos1), new Action<ExchangeTelephoneNo>(this.detach_ExchangeTelephoneNos1));
			this._Telephones = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones), new Action<Telephone>(this.detach_Telephones));
			this._Center = default(EntityRef<Center>);
			this._Switch = default(EntityRef<Switch>);
			  
		}
	}

	public partial class SwitchSpecialService
	{
		public void Detach()
		{

			this._SpecialServiceType = default(EntityRef<SpecialServiceType>);
			this._Switch = default(EntityRef<Switch>);
			  
		}
	}

	public partial class SwitchTransition
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Switch = default(EntityRef<Switch>);
			  
		}
	}

	public partial class SwitchType
	{
		public void Detach()
		{

			this._Switches = new EntitySet<Switch>(new Action<Switch>(this.attach_Switches), new Action<Switch>(this.detach_Switches));
			  
		}
	}

	public partial class SystemChange
	{
		public void Detach()
		{

			  
		}
	}

	public partial class TakePossession
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._CauseOfTakePossession = default(EntityRef<CauseOfTakePossession>);
			this._Notice = default(EntityRef<Notice>);
			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class TelecomminucationService
	{
		public void Detach()
		{

			this._TelecomminucationServicePayments = new EntitySet<TelecomminucationServicePayment>(new Action<TelecomminucationServicePayment>(this.attach_TelecomminucationServicePayments), new Action<TelecomminucationServicePayment>(this.detach_TelecomminucationServicePayments));
			this._RequestType = default(EntityRef<RequestType>);
			this._UnitMeasure = default(EntityRef<UnitMeasure>);
			  
		}
	}

	public partial class Telephone
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._BlackLists = new EntitySet<BlackList>(new Action<BlackList>(this.attach_BlackLists), new Action<BlackList>(this.detach_BlackLists));
			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._CenterToCenterTranslationTelephones = new EntitySet<CenterToCenterTranslationTelephone>(new Action<CenterToCenterTranslationTelephone>(this.attach_CenterToCenterTranslationTelephones), new Action<CenterToCenterTranslationTelephone>(this.detach_CenterToCenterTranslationTelephones));
			this._CenterToCenterTranslationTelephones1 = new EntitySet<CenterToCenterTranslationTelephone>(new Action<CenterToCenterTranslationTelephone>(this.attach_CenterToCenterTranslationTelephones1), new Action<CenterToCenterTranslationTelephone>(this.detach_CenterToCenterTranslationTelephones1));
			this._ChangeNos = new EntitySet<ChangeNo>(new Action<ChangeNo>(this.attach_ChangeNos), new Action<ChangeNo>(this.detach_ChangeNos));
			this._ChangeNos1 = new EntitySet<ChangeNo>(new Action<ChangeNo>(this.attach_ChangeNos1), new Action<ChangeNo>(this.detach_ChangeNos1));
			this._E1s = new EntitySet<E1>(new Action<E1>(this.attach_E1s), new Action<E1>(this.detach_E1s));
			this._ExchangeBrokenPCMs = new EntitySet<ExchangeBrokenPCM>(new Action<ExchangeBrokenPCM>(this.attach_ExchangeBrokenPCMs), new Action<ExchangeBrokenPCM>(this.detach_ExchangeBrokenPCMs));
			this._ExchangeCabinetInputConncetions = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions));
			this._ExchangeCentralCableMDFConncetions = new EntitySet<ExchangeCentralCableMDFConncetion>(new Action<ExchangeCentralCableMDFConncetion>(this.attach_ExchangeCentralCableMDFConncetions), new Action<ExchangeCentralCableMDFConncetion>(this.detach_ExchangeCentralCableMDFConncetions));
			this._InvestigatePossibilityWaitinglists = new EntitySet<InvestigatePossibilityWaitinglist>(new Action<InvestigatePossibilityWaitinglist>(this.attach_InvestigatePossibilityWaitinglists), new Action<InvestigatePossibilityWaitinglist>(this.detach_InvestigatePossibilityWaitinglists));
			this._Requests = new EntitySet<Request>(new Action<Request>(this.attach_Requests), new Action<Request>(this.detach_Requests));
			this._SelectTelephones = new EntitySet<SelectTelephone>(new Action<SelectTelephone>(this.attach_SelectTelephones), new Action<SelectTelephone>(this.detach_SelectTelephones));
			this._SpecialWireAddresses = new EntitySet<SpecialWireAddress>(new Action<SpecialWireAddress>(this.attach_SpecialWireAddresses), new Action<SpecialWireAddress>(this.detach_SpecialWireAddresses));
			this._SwapPCMs = new EntitySet<SwapPCM>(new Action<SwapPCM>(this.attach_SwapPCMs), new Action<SwapPCM>(this.detach_SwapPCMs));
			this._SwapPCMs1 = new EntitySet<SwapPCM>(new Action<SwapPCM>(this.attach_SwapPCMs1), new Action<SwapPCM>(this.detach_SwapPCMs1));
			this._SwapTelephones = new EntitySet<SwapTelephone>(new Action<SwapTelephone>(this.attach_SwapTelephones), new Action<SwapTelephone>(this.detach_SwapTelephones));
			this._SwapTelephones1 = new EntitySet<SwapTelephone>(new Action<SwapTelephone>(this.attach_SwapTelephones1), new Action<SwapTelephone>(this.detach_SwapTelephones1));
			this._TakePossessions = new EntitySet<TakePossession>(new Action<TakePossession>(this.attach_TakePossessions), new Action<TakePossession>(this.detach_TakePossessions));
			this._Telephone2 = default(EntityRef<Telephone>);
			this._TelephonePBXes = new EntitySet<TelephonePBX>(new Action<TelephonePBX>(this.attach_TelephonePBXes), new Action<TelephonePBX>(this.detach_TelephonePBXes));
			this._TelephonePBXes1 = new EntitySet<TelephonePBX>(new Action<TelephonePBX>(this.attach_TelephonePBXes1), new Action<TelephonePBX>(this.detach_TelephonePBXes1));
			this._TelephoneSpecialServiceTypes = new EntitySet<TelephoneSpecialServiceType>(new Action<TelephoneSpecialServiceType>(this.attach_TelephoneSpecialServiceTypes), new Action<TelephoneSpecialServiceType>(this.detach_TelephoneSpecialServiceTypes));
			this._TelephoneStatusLogs = new EntitySet<TelephoneStatusLog>(new Action<TelephoneStatusLog>(this.attach_TelephoneStatusLogs), new Action<TelephoneStatusLog>(this.detach_TelephoneStatusLogs));
			this._TelRoundSales = new EntitySet<TelRoundSale>(new Action<TelRoundSale>(this.attach_TelRoundSales), new Action<TelRoundSale>(this.detach_TelRoundSales));
			this._TitleIn118s = new EntitySet<TitleIn118>(new Action<TitleIn118>(this.attach_TitleIn118s), new Action<TitleIn118>(this.detach_TitleIn118s));
			this._TranslationOpticalCabinetToNormalConncetions = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions));
			this._TranslationOpticalCabinetToNormalConncetions1 = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions1), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions1));
			this._TranslationOpticalCabinetToNormalTelephones = new EntitySet<TranslationOpticalCabinetToNormalTelephone>(new Action<TranslationOpticalCabinetToNormalTelephone>(this.attach_TranslationOpticalCabinetToNormalTelephones), new Action<TranslationOpticalCabinetToNormalTelephone>(this.detach_TranslationOpticalCabinetToNormalTelephones));
			this._TranslationOpticalCabinetToNormalTelephones1 = new EntitySet<TranslationOpticalCabinetToNormalTelephone>(new Action<TranslationOpticalCabinetToNormalTelephone>(this.attach_TranslationOpticalCabinetToNormalTelephones1), new Action<TranslationOpticalCabinetToNormalTelephone>(this.detach_TranslationOpticalCabinetToNormalTelephones1));
			this._WarningHistories = new EntitySet<WarningHistory>(new Action<WarningHistory>(this.attach_WarningHistories), new Action<WarningHistory>(this.detach_WarningHistories));
			this._Wirelesses = new EntitySet<Wireless>(new Action<Wireless>(this.attach_Wirelesses), new Action<Wireless>(this.detach_Wirelesses));
			this._ExchangeGSMs = new EntitySet<ExchangeGSM>(new Action<ExchangeGSM>(this.attach_ExchangeGSMs), new Action<ExchangeGSM>(this.detach_ExchangeGSMs));
			this._ExchangeGSMs1 = new EntitySet<ExchangeGSM>(new Action<ExchangeGSM>(this.attach_ExchangeGSMs1), new Action<ExchangeGSM>(this.detach_ExchangeGSMs1));
			this._ExchangeGSMConnections = new EntitySet<ExchangeGSMConnection>(new Action<ExchangeGSMConnection>(this.attach_ExchangeGSMConnections), new Action<ExchangeGSMConnection>(this.detach_ExchangeGSMConnections));
			this._ExchangeGSMConnections1 = new EntitySet<ExchangeGSMConnection>(new Action<ExchangeGSMConnection>(this.attach_ExchangeGSMConnections1), new Action<ExchangeGSMConnection>(this.detach_ExchangeGSMConnections1));
			this._GSMSimCard = default(EntityRef<GSMSimCard>);
			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._CauseOfCut = default(EntityRef<CauseOfCut>);
			this._CauseOfTakePossession = default(EntityRef<CauseOfTakePossession>);
			this._Center = default(EntityRef<Center>);
			this._CustomerGroup = default(EntityRef<CustomerGroup>);
			this._CustomerType = default(EntityRef<CustomerType>);
			this._SwitchPort = default(EntityRef<SwitchPort>);
			this._SwitchPrecode = default(EntityRef<SwitchPrecode>);
			this._Telephone1 = default(EntityRef<Telephone>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class TelephoneCycleFiche
	{
		public void Detach()
		{

			  
		}
	}

	public partial class TelephonePBX
	{
		public void Detach()
		{

			this._Telephone = default(EntityRef<Telephone>);
			this._Telephone1 = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class TelephoneSpecialServiceType
	{
		public void Detach()
		{

			this._SpecialServiceType = default(EntityRef<SpecialServiceType>);
			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class TelephoneStatusLog
	{
		public void Detach()
		{

			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class TelephoneTemp
	{
		public void Detach()
		{

			  
		}
	}

	public partial class TelRoundSale
	{
		public void Detach()
		{

			this._Contracts = new EntitySet<Contract>(new Action<Contract>(this.attach_Contracts), new Action<Contract>(this.detach_Contracts));
			this._RoundSaleInfo = default(EntityRef<RoundSaleInfo>);
			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class temppap
	{
		public void Detach()
		{

		}
	}

	public partial class temppap1
	{
		public void Detach()
		{

		}
	}

	public partial class temppap2
	{
		public void Detach()
		{

		}
	}

	public partial class temppap3
	{
		public void Detach()
		{

		}
	}

	public partial class TitleIn118
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class TransferDepartmentOffice
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class TransferFileInfo
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class TranslationOpticalCabinetToNormal
	{
		public void Detach()
		{

			this._Cabinet = default(EntityRef<Cabinet>);
			this._Cabinet1 = default(EntityRef<Cabinet>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._CabinetInput1 = default(EntityRef<CabinetInput>);
			this._CabinetInput2 = default(EntityRef<CabinetInput>);
			this._CabinetInput3 = default(EntityRef<CabinetInput>);
			this._CabinetUsageType = default(EntityRef<CabinetUsageType>);
			this._CabinetUsageType1 = default(EntityRef<CabinetUsageType>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class TranslationOpticalCabinetToNormalConncetion
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Bucht = default(EntityRef<Bucht>);
			this._Bucht1 = default(EntityRef<Bucht>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._CabinetInput1 = default(EntityRef<CabinetInput>);
			this._Post = default(EntityRef<Post>);
			this._Post1 = default(EntityRef<Post>);
			this._PostContact = default(EntityRef<PostContact>);
			this._PostContact1 = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Telephone1 = default(EntityRef<Telephone>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class TranslationOpticalCabinetToNormalTelephone
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Telephone1 = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class TranslationPCMToNormal
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class TranslationPost
	{
		public void Detach()
		{

			this._Cabinet = default(EntityRef<Cabinet>);
			this._Post = default(EntityRef<Post>);
			this._Post1 = default(EntityRef<Post>);
			this._PostContact = default(EntityRef<PostContact>);
			this._PostContact1 = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class TranslationPostInput
	{
		public void Detach()
		{

			this._TranslationPostInputConnections = new EntitySet<TranslationPostInputConnection>(new Action<TranslationPostInputConnection>(this.attach_TranslationPostInputConnections), new Action<TranslationPostInputConnection>(this.detach_TranslationPostInputConnections));
			this._Cabinet = default(EntityRef<Cabinet>);
			this._Cabinet1 = default(EntityRef<Cabinet>);
			this._Post = default(EntityRef<Post>);
			this._Post1 = default(EntityRef<Post>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class TranslationPostInputConnection
	{
		public void Detach()
		{

			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._PostContact = default(EntityRef<PostContact>);
			this._PostContact1 = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			this._TranslationPostInput = default(EntityRef<TranslationPostInput>);
			  
		}
	}

	public partial class UnitMeasure
	{
		public void Detach()
		{

			this._TelecomminucationServices = new EntitySet<TelecomminucationService>(new Action<TelecomminucationService>(this.attach_TelecomminucationServices), new Action<TelecomminucationService>(this.detach_TelecomminucationServices));
			  
		}
	}

	public partial class UsedProduct
	{
		public void Detach()
		{

			this._PostContactEquipments = new EntitySet<PostContactEquipment>(new Action<PostContactEquipment>(this.attach_PostContactEquipments), new Action<PostContactEquipment>(this.detach_PostContactEquipments));
			  
		}
	}

	public partial class User
	{
		public void Detach()
		{

			this._ADSLChangeServices = new EntitySet<ADSLChangeService>(new Action<ADSLChangeService>(this.attach_ADSLChangeServices), new Action<ADSLChangeService>(this.detach_ADSLChangeServices));
			this._ADSLChangeServices1 = new EntitySet<ADSLChangeService>(new Action<ADSLChangeService>(this.attach_ADSLChangeServices1), new Action<ADSLChangeService>(this.detach_ADSLChangeServices1));
			this._ADSLHistories = new EntitySet<ADSLHistory>(new Action<ADSLHistory>(this.attach_ADSLHistories), new Action<ADSLHistory>(this.detach_ADSLHistories));
			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._ADSLRequests1 = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests1), new Action<ADSLRequest>(this.detach_ADSLRequests1));
			this._ADSLRequests2 = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests2), new Action<ADSLRequest>(this.detach_ADSLRequests2));
			this._ADSLRequests3 = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests3), new Action<ADSLRequest>(this.detach_ADSLRequests3));
			this._ADSLSellerAgentRecharges = new EntitySet<ADSLSellerAgentRecharge>(new Action<ADSLSellerAgentRecharge>(this.attach_ADSLSellerAgentRecharges), new Action<ADSLSellerAgentRecharge>(this.detach_ADSLSellerAgentRecharges));
			this._ADSLSellerAgentUser = default(EntityRef<ADSLSellerAgentUser>);
			this._ADSLSellerAgentUserRecharges = new EntitySet<ADSLSellerAgentUserRecharge>(new Action<ADSLSellerAgentUserRecharge>(this.attach_ADSLSellerAgentUserRecharges), new Action<ADSLSellerAgentUserRecharge>(this.detach_ADSLSellerAgentUserRecharges));
			this._ADSLSetupContactInformations = new EntitySet<ADSLSetupContactInformation>(new Action<ADSLSetupContactInformation>(this.attach_ADSLSetupContactInformations), new Action<ADSLSetupContactInformation>(this.detach_ADSLSetupContactInformations));
			this._ADSLSupportCommnets = new EntitySet<ADSLSupportCommnet>(new Action<ADSLSupportCommnet>(this.attach_ADSLSupportCommnets), new Action<ADSLSupportCommnet>(this.detach_ADSLSupportCommnets));
			this._BlackLists = new EntitySet<BlackList>(new Action<BlackList>(this.attach_BlackLists), new Action<BlackList>(this.detach_BlackLists));
			this._BlackLists1 = new EntitySet<BlackList>(new Action<BlackList>(this.attach_BlackLists1), new Action<BlackList>(this.detach_BlackLists1));
			this._DataGridColumnConfigs = new EntitySet<DataGridColumnConfig>(new Action<DataGridColumnConfig>(this.attach_DataGridColumnConfigs), new Action<DataGridColumnConfig>(this.detach_DataGridColumnConfigs));
			this._PAPInfoUser = default(EntityRef<PAPInfoUser>);
			this._Requests = new EntitySet<Request>(new Action<Request>(this.attach_Requests), new Action<Request>(this.detach_Requests));
			this._Requests1 = new EntitySet<Request>(new Action<Request>(this.attach_Requests1), new Action<Request>(this.detach_Requests1));
			this._SpaceAndPowers = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers), new Action<SpaceAndPower>(this.detach_SpaceAndPowers));
			this._SpaceAndPowers1 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers1), new Action<SpaceAndPower>(this.detach_SpaceAndPowers1));
			this._SpaceAndPowers2 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers2), new Action<SpaceAndPower>(this.detach_SpaceAndPowers2));
			this._SpaceAndPowers3 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers3), new Action<SpaceAndPower>(this.detach_SpaceAndPowers3));
			this._SpaceAndPowers4 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers4), new Action<SpaceAndPower>(this.detach_SpaceAndPowers4));
			this._SpaceAndPowers5 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers5), new Action<SpaceAndPower>(this.detach_SpaceAndPowers5));
			this._SpaceAndPowers6 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers6), new Action<SpaceAndPower>(this.detach_SpaceAndPowers6));
			this._SpaceAndPowers7 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers7), new Action<SpaceAndPower>(this.detach_SpaceAndPowers7));
			this._SpaceAndPowers8 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers8), new Action<SpaceAndPower>(this.detach_SpaceAndPowers8));
			this._SpaceAndPowers9 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers9), new Action<SpaceAndPower>(this.detach_SpaceAndPowers9));
			this._SpaceAndPowers10 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers10), new Action<SpaceAndPower>(this.detach_SpaceAndPowers10));
			this._SpaceAndPowers11 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers11), new Action<SpaceAndPower>(this.detach_SpaceAndPowers11));
			this._SpaceAndPowers12 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers12), new Action<SpaceAndPower>(this.detach_SpaceAndPowers12));
			this._SpaceAndPowers13 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers13), new Action<SpaceAndPower>(this.detach_SpaceAndPowers13));
			this._SpaceAndPowers14 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers14), new Action<SpaceAndPower>(this.detach_SpaceAndPowers14));
			this._SpaceAndPowers15 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers15), new Action<SpaceAndPower>(this.detach_SpaceAndPowers15));
			this._SpaceAndPowers16 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers16), new Action<SpaceAndPower>(this.detach_SpaceAndPowers16));
			this._SpaceAndPowers17 = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers17), new Action<SpaceAndPower>(this.detach_SpaceAndPowers17));
			this._UserCenters = new EntitySet<UserCenter>(new Action<UserCenter>(this.attach_UserCenters), new Action<UserCenter>(this.detach_UserCenters));
			this._WirelessChangeServices = new EntitySet<WirelessChangeService>(new Action<WirelessChangeService>(this.attach_WirelessChangeServices), new Action<WirelessChangeService>(this.detach_WirelessChangeServices));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			this._WirelessRequests1 = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests1), new Action<WirelessRequest>(this.detach_WirelessRequests1));
			this._WirelessRequests2 = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests2), new Action<WirelessRequest>(this.detach_WirelessRequests2));
			this._WirelessRequests3 = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests3), new Action<WirelessRequest>(this.detach_WirelessRequests3));
			this._TelephoneConnectionInstallments = new EntitySet<TelephoneConnectionInstallment>(new Action<TelephoneConnectionInstallment>(this.attach_TelephoneConnectionInstallments), new Action<TelephoneConnectionInstallment>(this.detach_TelephoneConnectionInstallments));
			this._ShahkarWebApiLogs = new EntitySet<ShahkarWebApiLog>(new Action<ShahkarWebApiLog>(this.attach_ShahkarWebApiLogs), new Action<ShahkarWebApiLog>(this.detach_ShahkarWebApiLogs));
			this._Role = default(EntityRef<Role>);
			  
		}
	}

	public partial class UserCenter
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class VacateE1
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class VacateSpecialWire
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._Bucht = default(EntityRef<Bucht>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._PostContact = default(EntityRef<PostContact>);
			this._Request = default(EntityRef<Request>);
			this._SwitchPort = default(EntityRef<SwitchPort>);
			  
		}
	}

	public partial class VacateSpecialWirePoint
	{
		public void Detach()
		{

			this._Bucht = default(EntityRef<Bucht>);
			this._Center = default(EntityRef<Center>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class VerticalMDFColumn
	{
		public void Detach()
		{

			this._VerticalMDFRows = new EntitySet<VerticalMDFRow>(new Action<VerticalMDFRow>(this.attach_VerticalMDFRows), new Action<VerticalMDFRow>(this.detach_VerticalMDFRows));
			this._MDFFrame = default(EntityRef<MDFFrame>);
			  
		}
	}

	public partial class VerticalMDFRow
	{
		public void Detach()
		{

			this._Buchts = new EntitySet<Bucht>(new Action<Bucht>(this.attach_Buchts), new Action<Bucht>(this.detach_Buchts));
			this._VerticalMDFColumn = default(EntityRef<VerticalMDFColumn>);
			  
		}
	}

	public partial class VisitAddress
	{
		public void Detach()
		{

			this._SugesstionPossibilities = new EntitySet<SugesstionPossibility>(new Action<SugesstionPossibility>(this.attach_SugesstionPossibilities), new Action<SugesstionPossibility>(this.detach_SugesstionPossibilities));
			this._VisitAddresses = new EntitySet<VisitAddress>(new Action<VisitAddress>(this.attach_VisitAddresses), new Action<VisitAddress>(this.detach_VisitAddresses));
			this._VisitPlacesCabinetAndPosts = new EntitySet<VisitPlacesCabinetAndPost>(new Action<VisitPlacesCabinetAndPost>(this.attach_VisitPlacesCabinetAndPosts), new Action<VisitPlacesCabinetAndPost>(this.detach_VisitPlacesCabinetAndPosts));
			this._Post = default(EntityRef<Post>);
			this._Request = default(EntityRef<Request>);
			this._VisitAddress1 = default(EntityRef<VisitAddress>);
			this._Address = default(EntityRef<Address>);
			  
		}
	}

	public partial class VisitPlacesCabinetAndPost
	{
		public void Detach()
		{

			this._Cabinet = default(EntityRef<Cabinet>);
			this._Post = default(EntityRef<Post>);
			this._VisitAddress = default(EntityRef<VisitAddress>);
			  
		}
	}

	public partial class WaitingList
	{
		public void Detach()
		{

			this._InvestigatePossibilityWaitinglist = default(EntityRef<InvestigatePossibilityWaitinglist>);
			this._Request = default(EntityRef<Request>);
			this._Status1 = default(EntityRef<Status>);
			  
		}
	}

	public partial class WaitingListReason
	{
		public void Detach()
		{

			this._RequestType = default(EntityRef<RequestType>);
			  
		}
	}

	public partial class WarningHistory
	{
		public void Detach()
		{

			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class WarningMessage
	{
		public void Detach()
		{

			  
		}
	}

	public partial class Wireless
	{
		public void Detach()
		{

			this._PAPInfo = default(EntityRef<PAPInfo>);
			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._ADSLCustomerType = default(EntityRef<ADSLCustomerType>);
			this._ADSLGroupIP = default(EntityRef<ADSLGroupIP>);
			this._ADSLIP = default(EntityRef<ADSLIP>);
			this._ADSLModemProperty = default(EntityRef<ADSLModemProperty>);
			this._ADSLService = default(EntityRef<ADSLService>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class WirelessChangeService
	{
		public void Detach()
		{

			this._ADSLService = default(EntityRef<ADSLService>);
			this._ADSLService1 = default(EntityRef<ADSLService>);
			this._Request = default(EntityRef<Request>);
			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class WirelessRequest
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._ADSLCustomerGroup = default(EntityRef<ADSLCustomerGroup>);
			this._ADSLCustomerType = default(EntityRef<ADSLCustomerType>);
			this._ADSLGroupIP = default(EntityRef<ADSLGroupIP>);
			this._ADSLIP = default(EntityRef<ADSLIP>);
			this._ADSLModem = default(EntityRef<ADSLModem>);
			this._ADSLPort = default(EntityRef<ADSLPort>);
			this._ADSLSellerAgent = default(EntityRef<ADSLSellerAgent>);
			this._ADSLService = default(EntityRef<ADSLService>);
			this._Contractor = default(EntityRef<Contractor>);
			this._Request = default(EntityRef<Request>);
			this._User = default(EntityRef<User>);
			this._User1 = default(EntityRef<User>);
			this._User2 = default(EntityRef<User>);
			this._User3 = default(EntityRef<User>);
			this._Customer = default(EntityRef<Customer>);
			  
		}
	}

	public partial class WirelessSellTraffic
	{
		public void Detach()
		{

			this._ADSLService = default(EntityRef<ADSLService>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class Wiring
	{
		public void Detach()
		{

			this._InstallLines = new EntitySet<InstallLine>(new Action<InstallLine>(this.attach_InstallLines), new Action<InstallLine>(this.detach_InstallLines));
			this._Bucht = default(EntityRef<Bucht>);
			this._Failure117NetworkContractorOfficer = default(EntityRef<Failure117NetworkContractorOfficer>);
			this._IssueWiring = default(EntityRef<IssueWiring>);
			this._Status1 = default(EntityRef<Status>);
			this._Status2 = default(EntityRef<Status>);
			this._Status3 = default(EntityRef<Status>);
			this._InvestigatePossibility = default(EntityRef<InvestigatePossibility>);
			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class WorkFlowLog
	{
		public void Detach()
		{

			this._WorkFlowLogs = new EntitySet<WorkFlowLog>(new Action<WorkFlowLog>(this.attach_WorkFlowLogs), new Action<WorkFlowLog>(this.detach_WorkFlowLogs));
			this._WorkUnitActions = new EntitySet<WorkUnitAction>(new Action<WorkUnitAction>(this.attach_WorkUnitActions), new Action<WorkUnitAction>(this.detach_WorkUnitActions));
			this._Request = default(EntityRef<Request>);
			this._WorkFlowLog1 = default(EntityRef<WorkFlowLog>);
			this._WorkFlowRule = default(EntityRef<WorkFlowRule>);
			  
		}
	}

	public partial class WorkFlowRule
	{
		public void Detach()
		{

			this._WorkFlowLogs = new EntitySet<WorkFlowLog>(new Action<WorkFlowLog>(this.attach_WorkFlowLogs), new Action<WorkFlowLog>(this.detach_WorkFlowLogs));
			this._WorkFlowRules = new EntitySet<WorkFlowRule>(new Action<WorkFlowRule>(this.attach_WorkFlowRules), new Action<WorkFlowRule>(this.detach_WorkFlowRules));
			this._Status = default(EntityRef<Status>);
			this._Status1 = default(EntityRef<Status>);
			this._WorkFlowRule1 = default(EntityRef<WorkFlowRule>);
			this._RequestType = default(EntityRef<RequestType>);
			this._WorkFlowVersion = default(EntityRef<WorkFlowVersion>);
			this._WorkUnit = default(EntityRef<WorkUnit>);
			this._WorkUnit1 = default(EntityRef<WorkUnit>);
			  
		}
	}

	public partial class WorkFlowVersion
	{
		public void Detach()
		{

			this._WorkFlowRules = new EntitySet<WorkFlowRule>(new Action<WorkFlowRule>(this.attach_WorkFlowRules), new Action<WorkFlowRule>(this.detach_WorkFlowRules));
			  
		}
	}

	public partial class WorkUnit
	{
		public void Detach()
		{

			this._WorkFlowRules = new EntitySet<WorkFlowRule>(new Action<WorkFlowRule>(this.attach_WorkFlowRules), new Action<WorkFlowRule>(this.detach_WorkFlowRules));
			this._WorkFlowRules1 = new EntitySet<WorkFlowRule>(new Action<WorkFlowRule>(this.attach_WorkFlowRules1), new Action<WorkFlowRule>(this.detach_WorkFlowRules1));
			  
		}
	}

	public partial class WorkUnitAction
	{
		public void Detach()
		{

			this._WorkFlowLog = default(EntityRef<WorkFlowLog>);
			  
		}
	}

	public partial class ViewReservBucht
	{
		public void Detach()
		{

		}
	}

	public partial class ExchangeGSM
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Telephone1 = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class ExchangeGSMConnection
	{
		public void Detach()
		{

			this._Bucht = default(EntityRef<Bucht>);
			this._Cabinet = default(EntityRef<Cabinet>);
			this._CabinetInput = default(EntityRef<CabinetInput>);
			this._Post = default(EntityRef<Post>);
			this._PostContact = default(EntityRef<PostContact>);
			this._Telephone = default(EntityRef<Telephone>);
			this._Telephone1 = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class TelecomminucationServicePayment
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			this._TelecomminucationService = default(EntityRef<TelecomminucationService>);
			  
		}
	}

	public partial class Antenna
	{
		public void Detach()
		{

			this._SpaceAndPower = default(EntityRef<SpaceAndPower>);
			  
		}
	}

	public partial class ZeroStatus
	{
		public void Detach()
		{

			this._Request = default(EntityRef<Request>);
			  
		}
	}

	public partial class Failure117UB
	{
		public void Detach()
		{

			this._Center = default(EntityRef<Center>);
			  
		}
	}

	public partial class GSMSimCard
	{
		public void Detach()
		{

			this._Telephone = default(EntityRef<Telephone>);
			  
		}
	}

	public partial class Role
	{
		public void Detach()
		{

			this._RoleReportTemplates = new EntitySet<RoleReportTemplate>(new Action<RoleReportTemplate>(this.attach_RoleReportTemplates), new Action<RoleReportTemplate>(this.detach_RoleReportTemplates));
			this._RoleRequestSteps = new EntitySet<RoleRequestStep>(new Action<RoleRequestStep>(this.attach_RoleRequestSteps), new Action<RoleRequestStep>(this.detach_RoleRequestSteps));
			this._RoleResources = new EntitySet<RoleResource>(new Action<RoleResource>(this.attach_RoleResources), new Action<RoleResource>(this.detach_RoleResources));
			this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
			this._RoleWebServices = new EntitySet<RoleWebService>(new Action<RoleWebService>(this.attach_RoleWebServices), new Action<RoleWebService>(this.detach_RoleWebServices));
			  
		}
	}

	public partial class RoleWebService
	{
		public void Detach()
		{

			this._Role = default(EntityRef<Role>);
			this._WebService = default(EntityRef<WebService>);
			  
		}
	}

	public partial class WebService
	{
		public void Detach()
		{

			this._RoleWebServices = new EntitySet<RoleWebService>(new Action<RoleWebService>(this.attach_RoleWebServices), new Action<RoleWebService>(this.detach_RoleWebServices));
			  
		}
	}

	public partial class InstallRequest
	{
		public void Detach()
		{

			this._Address = default(EntityRef<Address>);
			this._Address1 = default(EntityRef<Address>);
			this._CustomerGroup = default(EntityRef<CustomerGroup>);
			this._CustomerType = default(EntityRef<CustomerType>);
			this._Request = default(EntityRef<Request>);
			this._RequestType = default(EntityRef<RequestType>);
			  
		}
	}

	public partial class TelephoneConnectionInstallment
	{
		public void Detach()
		{

			this._RequestPayment = default(EntityRef<RequestPayment>);
			this._User = default(EntityRef<User>);
			  
		}
	}

	public partial class Agent
	{
		public void Detach()
		{

			this._Customers = new EntitySet<Customer>(new Action<Customer>(this.attach_Customers), new Action<Customer>(this.detach_Customers));
			  
		}
	}

	public partial class Customer
	{
		public void Detach()
		{

			this._ADSLs = new EntitySet<ADSL>(new Action<ADSL>(this.attach_ADSLs), new Action<ADSL>(this.detach_ADSLs));
			this._ADSLChangeCustomerOwnerCharacteristics = new EntitySet<ADSLChangeCustomerOwnerCharacteristic>(new Action<ADSLChangeCustomerOwnerCharacteristic>(this.attach_ADSLChangeCustomerOwnerCharacteristics), new Action<ADSLChangeCustomerOwnerCharacteristic>(this.detach_ADSLChangeCustomerOwnerCharacteristics));
			this._ADSLChangeCustomerOwnerCharacteristics1 = new EntitySet<ADSLChangeCustomerOwnerCharacteristic>(new Action<ADSLChangeCustomerOwnerCharacteristic>(this.attach_ADSLChangeCustomerOwnerCharacteristics1), new Action<ADSLChangeCustomerOwnerCharacteristic>(this.detach_ADSLChangeCustomerOwnerCharacteristics1));
			this._ADSLChangePlaces = new EntitySet<ADSLChangePlace>(new Action<ADSLChangePlace>(this.attach_ADSLChangePlaces), new Action<ADSLChangePlace>(this.detach_ADSLChangePlaces));
			this._ADSLRequests = new EntitySet<ADSLRequest>(new Action<ADSLRequest>(this.attach_ADSLRequests), new Action<ADSLRequest>(this.detach_ADSLRequests));
			this._BlackLists = new EntitySet<BlackList>(new Action<BlackList>(this.attach_BlackLists), new Action<BlackList>(this.detach_BlackLists));
			this._ChangeLocations = new EntitySet<ChangeLocation>(new Action<ChangeLocation>(this.attach_ChangeLocations), new Action<ChangeLocation>(this.detach_ChangeLocations));
			this._ChangeNames = new EntitySet<ChangeName>(new Action<ChangeName>(this.attach_ChangeNames), new Action<ChangeName>(this.detach_ChangeNames));
			this._ChangeNames1 = new EntitySet<ChangeName>(new Action<ChangeName>(this.attach_ChangeNames1), new Action<ChangeName>(this.detach_ChangeNames1));
			this._ChangeNos = new EntitySet<ChangeNo>(new Action<ChangeNo>(this.attach_ChangeNos), new Action<ChangeNo>(this.detach_ChangeNos));
			this._ExchangeCabinetInputConncetions = new EntitySet<ExchangeCabinetInputConncetion>(new Action<ExchangeCabinetInputConncetion>(this.attach_ExchangeCabinetInputConncetions), new Action<ExchangeCabinetInputConncetion>(this.detach_ExchangeCabinetInputConncetions));
			this._InvestigatePossibilityWaitinglists = new EntitySet<InvestigatePossibilityWaitinglist>(new Action<InvestigatePossibilityWaitinglist>(this.attach_InvestigatePossibilityWaitinglists), new Action<InvestigatePossibilityWaitinglist>(this.detach_InvestigatePossibilityWaitinglists));
			this._Requests = new EntitySet<Request>(new Action<Request>(this.attach_Requests), new Action<Request>(this.detach_Requests));
			this._RequestDocuments = new EntitySet<RequestDocument>(new Action<RequestDocument>(this.attach_RequestDocuments), new Action<RequestDocument>(this.detach_RequestDocuments));
			this._SpaceAndPowers = new EntitySet<SpaceAndPower>(new Action<SpaceAndPower>(this.attach_SpaceAndPowers), new Action<SpaceAndPower>(this.detach_SpaceAndPowers));
			this._SpecialPrivateCables = new EntitySet<SpecialPrivateCable>(new Action<SpecialPrivateCable>(this.attach_SpecialPrivateCables), new Action<SpecialPrivateCable>(this.detach_SpecialPrivateCables));
			this._TakePossessions = new EntitySet<TakePossession>(new Action<TakePossession>(this.attach_TakePossessions), new Action<TakePossession>(this.detach_TakePossessions));
			this._Telephones = new EntitySet<Telephone>(new Action<Telephone>(this.attach_Telephones), new Action<Telephone>(this.detach_Telephones));
			this._TranslationOpticalCabinetToNormalConncetions = new EntitySet<TranslationOpticalCabinetToNormalConncetion>(new Action<TranslationOpticalCabinetToNormalConncetion>(this.attach_TranslationOpticalCabinetToNormalConncetions), new Action<TranslationOpticalCabinetToNormalConncetion>(this.detach_TranslationOpticalCabinetToNormalConncetions));
			this._Wirelesses = new EntitySet<Wireless>(new Action<Wireless>(this.attach_Wirelesses), new Action<Wireless>(this.detach_Wirelesses));
			this._WirelessRequests = new EntitySet<WirelessRequest>(new Action<WirelessRequest>(this.attach_WirelessRequests), new Action<WirelessRequest>(this.detach_WirelessRequests));
			this._Customer2 = default(EntityRef<Customer>);
			this._ShahkarWebApiLogs = new EntitySet<ShahkarWebApiLog>(new Action<ShahkarWebApiLog>(this.attach_ShahkarWebApiLogs), new Action<ShahkarWebApiLog>(this.detach_ShahkarWebApiLogs));
			this._Address = default(EntityRef<Address>);
			this._Agent = default(EntityRef<Agent>);
			this._Customer1 = default(EntityRef<Customer>);
			  
		}
	}

	public partial class ShaskamLog
	{
		public void Detach()
		{

			  
		}
	}

	public partial class ShahkarWebApiLog
	{
		public void Detach()
		{

			this._Customer = default(EntityRef<Customer>);
			this._User = default(EntityRef<User>);
			  
		}
	}

}