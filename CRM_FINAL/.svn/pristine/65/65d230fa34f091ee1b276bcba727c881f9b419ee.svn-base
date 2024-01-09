using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for ProposaledCabinetAndPost.xaml
    /// </summary>
    public partial class ProposaledCabinetAndPost : UserControl
    {
        
        #region Properties and Fields

        static long _RequestID = 0;
        static long _NearestTelephon = 0;

        #endregion

        #region Constructors

        public ProposaledCabinetAndPost()
        {
            InitializeComponent();
            Initialize();
        }

        public ProposaledCabinetAndPost(long requestID, long? nearestTelephon)
        {
            _RequestID = requestID;
            _NearestTelephon = nearestTelephon ?? -1;
            Initialize();
        } 

        #endregion

        #region Methods

        private void Initialize()
        {

        }

        #endregion

        #region EventHandlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ProposedFacilityTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ProposedFacilityType));

            List<VisitPlacesCabinetAndPostClass> visitPlacesCabinetAndPostClass = new List<VisitPlacesCabinetAndPostClass>();
            Request request = Data.RequestDB.GetRequestByID(_RequestID);

            if (request != null)
            {
                if (request.RequestTypeID == (byte)DB.RequestType.Dayri)
                {
                    InstallRequest installRequest = Data.InstallRequestDB.GetInstallRequestByRequestID(_RequestID);
                    visitPlacesCabinetAndPostClass = Data.VisitAddressDB.GetVisitAddressCabinetAndPost((long)installRequest.InstallAddressID);
                    _NearestTelephon = installRequest.NearestTelephon ?? -1;
                }
                else if (request.RequestTypeID == (byte)DB.RequestType.Reinstall)
                {
                    InstallRequest installRequest = Data.InstallRequestDB.GetInstallRequestByRequestID(_RequestID);
                    visitPlacesCabinetAndPostClass = Data.VisitAddressDB.GetVisitAddressCabinetAndPost((long)installRequest.InstallAddressID);

                    // اطلاعات آبونه تلفن تخلیه شده را از لاگ استخراج می کند
                    RequestLog requestLog = RequestLogDB.GetLastTelephoneNoRequestLogByRequestType(installRequest.PassTelephone ?? -1, DB.RequestType.Dischargin);
                    if (requestLog != null)
                    {
                        CRM.Data.Schema.DischargeTelephone dischargeTelephone = LogSchemaUtility.Deserialize<CRM.Data.Schema.DischargeTelephone>(requestLog.Description.ToString());
                        if (dischargeTelephone != null)
                        {
                            Center center = Data.CenterDB.GetCenterById(dischargeTelephone.CenterID);

                            CabinetInput cabinetInput = Data.CabinetInputDB.GetCabinetInputByID(dischargeTelephone.CabinetInputID);
                            PostContact postContact = Data.PostContactDB.GetPostContactByID(dischargeTelephone.PostContactID);


                            Post post = Data.PostDB.GetPostByID(postContact != null ? postContact.PostID : 0);

                            Cabinet cabinet = Data.CabinetDB.GetCabinetByID(cabinetInput != null ? cabinetInput.CabinetID : 0);

                            visitPlacesCabinetAndPostClass = visitPlacesCabinetAndPostClass.Union(new List<VisitPlacesCabinetAndPostClass>
                                                              { new VisitPlacesCabinetAndPostClass{
                                                                 PostNumber = post != null ? post.Number.ToString() : string.Empty,
                                                                 CabinetNumber = cabinet != null ? cabinet.CabinetNumber.ToString() : string.Empty,
                                                                 CabinetInput = cabinetInput != null ? cabinetInput.InputNumber.ToString() : string.Empty,
                                                                 ConnectionNo = postContact != null ? postContact.ConnectionNo : 0,
                                                                 TelephonNo = dischargeTelephone.TelephoneNo,
                                                                  ProposedFacilityType = (int)DB.ProposedFacilityType.PassTelephone,
                                                                 Center = center != null ? center.CenterName.ToString() : string.Empty}
                                                             }).ToList();
                        }
                    }
                }
                else if (request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterToCenter)
                {
                    ChangeLocation changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(request.ID);
                    _NearestTelephon = changeLocation.NearestTelephon ?? -1;

                    visitPlacesCabinetAndPostClass = Data.VisitAddressDB.GetVisitAddressCabinetAndPost(changeLocation.NewInstallAddressID ?? 0);
                    visitPlacesCabinetAndPostClass = visitPlacesCabinetAndPostClass.Union(Data.AssignmentDB.GetNearestTelephonVisitPlacesCabinetAndPostClass(request.TelephoneNo, (int)DB.ProposedFacilityType.CurrentTelephone)).ToList();
                }
                else if (request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterInside)
                {
                    ChangeLocation changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(request.ID);
                    _NearestTelephon = changeLocation.NearestTelephon ?? -1;

                    visitPlacesCabinetAndPostClass = Data.VisitAddressDB.GetVisitAddressCabinetAndPost(changeLocation.NewInstallAddressID ?? 0);
                    visitPlacesCabinetAndPostClass = visitPlacesCabinetAndPostClass.Union(Data.AssignmentDB.GetNearestTelephonVisitPlacesCabinetAndPostClass(request.TelephoneNo, (int)DB.ProposedFacilityType.CurrentTelephone)).ToList();
                }
                else if (request.RequestTypeID == (byte)DB.RequestType.E1)
                {
                    CRM.Data.E1 e1 = Data.E1DB.GetE1ByRequestID(request.ID);
                    visitPlacesCabinetAndPostClass = Data.VisitAddressDB.GetVisitAddressCabinetAndPost((long)e1.InstallAddressID);
                }
                else if (request.RequestTypeID == (byte)DB.RequestType.SpecialWire)
                {
                    SpecialWire specialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(request.ID);
                    visitPlacesCabinetAndPostClass = Data.VisitAddressDB.GetVisitAddressCabinetAndPost(specialWire.InstallAddressID ?? 0);
                    _NearestTelephon = specialWire.NearestTelephone ?? -1;
                }
                else if (request.RequestTypeID == (byte)DB.RequestType.ChangeLocationSpecialWire)
                {
                    ChangeLocationSpecialWire changeLocationSpecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(request.ID);
                    visitPlacesCabinetAndPostClass = Data.VisitAddressDB.GetVisitAddressCabinetAndPost(changeLocationSpecialWire.InstallAddressID ?? 0);
                    visitPlacesCabinetAndPostClass = visitPlacesCabinetAndPostClass.Union(Data.AssignmentDB.GetNearestTelephonVisitPlacesCabinetAndPostClass(request.TelephoneNo, (int)DB.ProposedFacilityType.CurrentTelephone)).ToList();
                    _NearestTelephon = changeLocationSpecialWire.NearestTelephone ?? 0;
                }
                visitPlacesCabinetAndPostClass = visitPlacesCabinetAndPostClass.Union(Data.AssignmentDB.GetNearestTelephonVisitPlacesCabinetAndPostClass(_NearestTelephon, (int)DB.ProposedFacilityType.NearestTelephone)).ToList();
                CabinetAndPostDataGrid.ItemsSource = visitPlacesCabinetAndPostClass;
            }
        } 

        #endregion

    }
}
