using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Collections.ObjectModel;

namespace CRM.Data
{
    public static class ADSLEquipmentDB
    {
        public static List<ADSLEquipmentInfo> SearchADSLEquipment(
            List<int> centerIDs,
            //List<int> shelfIDs,
            List<int> portType,
            //List<int> aAAType,
            string equipment,
            List<int> equipmentType,
            List<int> product
            //List<int> locationInstall,
            //string site
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLEquipments.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                         //(shelfIDs.Count == 0 || shelfIDs.Contains(t.ShelfID)) &&
                                                         (portType.Count == 0 || portType.Contains((int)t.PortTypeID)) &&
                                                         //(aAAType.Count == 0 || aAAType.Contains(t.AAAType)) &&
                                                         (equipmentType.Count == 0 || equipmentType.Contains((int)t.EquipmentType)) &&
                                                         (product.Count == 0 || product.Contains((int)t.Product)) &&
                                                         //(locationInstall.Count == 0 || locationInstall.Contains((int)t.LocationInstall)) &&
                                                         (string.IsNullOrWhiteSpace(equipment) || t.Equipment.Contains(equipment)))
                                             .Select(t => new ADSLEquipmentInfo
                                             {
                                                 ID = t.ID,
                                                 CenterID = t.CenterID,
                                                 //RockShelf = "رک : " + t.Shelf.Rock.No + " شلف : " + t.Shelf.No,
                                                 //PortTypeID = t.PortTypeID,
                                                 //AAAType = t.AAAType,
                                                 Equipment = t.Equipment,
                                                 EquipmentType = t.EquipmentType,
                                                 //LocationInstall = t.LocationInstall,
                                                 Product = t.Product,
                                                 //Site = t.Site,
                                             }).ToList();
            }
        }

        public static ADSLEquipment GetADSLEquipmentByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLEquipments
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetADSLEquipmentCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLEquipments
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Equipment,
                        IsChecked = false
                    }).ToList();
            }
        }

        public static List<ADSLEquipment> GetAllADSLEquipment()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLEquipments.ToList();
            }
        }

        public static List<ADSLEquipment> GetAllADSLEquipmentbyCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLEquipments.Where(t => t.CenterID == centerID).ToList();
            }
        }

        public static List<ADSLEquipment> GetFreeADSLEquipment()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLEquipments.ToList();
            }
        }

        public static List<ADSLEquipment> GetFreeADSLEquipmentByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLEquipments.Where(t => t.CenterID == centerID).ToList();
            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLEquipments.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static int GetRockID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLEquipments.Where(t => t.ID == id).SingleOrDefault().Shelf.Rock.ID;
            }
        }
    }
}