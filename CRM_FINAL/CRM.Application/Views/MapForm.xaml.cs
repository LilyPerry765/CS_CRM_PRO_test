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
using System.Windows.Shapes;
using Telerik.Windows.Controls.Map;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for MapForm.xaml
    /// </summary>
    public partial class MapForm : Local.TabWindow
    {
        public MapForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            Location DefultLocation = new Location();
            if (DB.City == "tehran")
            {
                DefultLocation = new Location(35.683334, 51.416668);
            }
            else if (DB.City == "kermanshah")
            {
                DefultLocation = new Location(34.308159, 47.05732);
            }
            else if (DB.City == "semnan")
            {
                DefultLocation = new Location(35.572269, 53.396049);
            }

            RadMap.Center = DefultLocation;
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void RadMap_MapMouseClick(object sender, Telerik.Windows.Controls.Map.MapMouseRoutedEventArgs eventArgs)
        {

        }

        private void RadMap_MapMouseDoubleClick(object sender, Telerik.Windows.Controls.Map.MapMouseRoutedEventArgs eventArgs)
        {


                  
        }

        private void LoadData()
        {
            InformationLayer.Items.Clear();
            List<Center> centers = Data.CenterDB.GetCentersHaveLocation();
            centers.ToList().ForEach(t =>
            {
                Location location = new Location((double)t.Latitude , (double)t.Longitude);
                MapPinPoint mapPinPoint = new MapPinPoint();
                mapPinPoint.Width = 48;
                mapPinPoint.Height = 48;
                mapPinPoint.Tag = new MapItemInfo { ID = t.ID , Type = DB.MapShapeType.Center };
                mapPinPoint.SetValue(MapLayer.LocationProperty, location);
                mapPinPoint.ImageSource =new BitmapImage( new Uri("pack://application:,,,/CRM.Application;component/Images/Center_48x48.png"));
                InformationLayer.Items.Add(mapPinPoint);
            });


            List<Cabinet> cabinets = Data.CabinetDB.GetCabinetsHaveLocation();
            cabinets.ToList().ForEach(t =>
            {
                Location location = new Location((double)t.Latitude, (double)t.Longitude);
                MapPinPoint mapPinPoint = new MapPinPoint();
                mapPinPoint.Width = 32;
                mapPinPoint.Height = 32;
                mapPinPoint.Tag = new MapItemInfo { ID = t.ID, Type = DB.MapShapeType.Cabinet };
                mapPinPoint.SetValue(MapLayer.LocationProperty, location);
                mapPinPoint.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/Cabinet_32x32.png"));
                InformationLayer.Items.Add(mapPinPoint);

                if (centers.Any(p => p.ID == t.CenterID))
                {
                    MapLine line = new MapLine();
                    line.Point1 = new Location((double)t.Latitude, (double)t.Longitude);
                    line.Point2 = new Location((double)centers.Find(p => p.ID == t.CenterID).Latitude, (double)centers.Find(p => p.ID == t.CenterID).Longitude);
                    line.ShapeFill = new MapShapeFill() { Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 3 };
                    this.InformationLayer.Items.Add(line);
                }
            });


            List<Post> posts = Data.PostDB.GetPostsHaveLocation();
            posts.ToList().ForEach(t =>
            {
                Location location = new Location((double)t.Latitude, (double)t.Longitude);
                MapPinPoint mapPinPoint = new MapPinPoint();
                mapPinPoint.Width = 16;
                mapPinPoint.Height = 16;
                mapPinPoint.Tag = new MapItemInfo { ID = t.ID, Type = DB.MapShapeType.Post };
                mapPinPoint.SetValue(MapLayer.LocationProperty, location);
                mapPinPoint.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/post16x16.png"));
                InformationLayer.Items.Add(mapPinPoint);

                if(cabinets.Any(p=>p.ID == t.CabinetID))
                {
                    MapLine line = new MapLine();
                    line.Point1 = new Location((double)t.Latitude, (double)t.Longitude);
                    line.Point2 = new Location((double)cabinets.Find(p => p.ID == t.CabinetID).Latitude, (double)cabinets.Find(p => p.ID == t.CabinetID).Longitude);
                    line.ShapeFill = new MapShapeFill() { Stroke = new SolidColorBrush(Colors.Blue), StrokeThickness = 1 };
                    this.InformationLayer.Items.Add(line);
                }
            });

        }

        #region Create ContextMenu
        private Location menuLocation = Location.Empty;
        private void RadMapContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            this.menuLocation = Location.GetCoordinates(this.RadMap, Mouse.GetPosition(this.RadMap));

            bool close = true;
            IEnumerable<object> list = this.InformationLayer.GetItemsInLocation(this.menuLocation);
            foreach (object item in list)
            {
                MapPinPoint shape = item as MapPinPoint;
                if (shape != null)
                {
                    close = false;
                    break;
                }
            }

            if (!close)
            {
                this.SetupShapeContextMenuItems(this.contextMenu);
            }
            else
            {
                this.SetupContextMenuItems(this.contextMenu);
            }
        }



        private void SetupShapeContextMenuItems(ContextMenu menu)
        {
            menu.Items.Clear();

            MenuItem baseItem = new MenuItem();
            baseItem.Width = 150;
            baseItem.Header = "حذف";
            //baseItem.Icon = new System.Windows.Controls.Image
            //{
            //    Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/delete2_16x16.png"))
            //};
            baseItem.Click += new RoutedEventHandler(this.DeleteShapeMenuItemClicked);
            menu.Items.Add(baseItem);


            MenuItem DetailItem = new MenuItem();
            DetailItem.Width = 150;
            DetailItem.Header = "جزئیات";
            //DetailItem.Icon = new System.Windows.Controls.Image
            //{
            //    Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/pencil_16x16.png"))
            //};
            DetailItem.Click += new RoutedEventHandler(this.DetailShapeMenuItemClicked);
            menu.Items.Add(DetailItem);

        }

        private void DetailShapeMenuItemClicked(object sender, RoutedEventArgs e)
        {
            if (!this.menuLocation.IsEmpty)
            {
                IEnumerable<object> list = this.InformationLayer.GetItemsInLocation(this.menuLocation);

                foreach (object item in list)
                {
                    MapPinPoint shape = item as MapPinPoint;
                    if (shape != null)
                    {
                        MapItemInfo mapItemInfo = (MapItemInfo)shape.Tag;
                        if (mapItemInfo.Type == DB.MapShapeType.Center)
                        {
                            CenterForm window = new CenterForm((int)mapItemInfo.ID);
                            window.ShowDialog();
                        }
                        else if (mapItemInfo.Type == DB.MapShapeType.Post)
                        {
                            PostForm window = new PostForm((int)mapItemInfo.ID);
                            window.ShowDialog();
                        }
                        else if (mapItemInfo.Type == DB.MapShapeType.Cabinet)
                        {
                            CabinetForm window = new CabinetForm((int)mapItemInfo.ID);
                            window.ShowDialog();
                        }
                        LoadData();
                        break;
                    }
                }


            }
        }

         private void SetupContextMenuItems(ContextMenu menu)
        {
            menu.Items.Clear();

            MenuItem addMenuItem = new MenuItem();
            addMenuItem.Width = 150;
            addMenuItem.Header = "اضافه";
            //addMenuItem.Icon = new System.Windows.Controls.Image
            //{
            //    Source = new BitmapImage(new Uri("pack://application:,,,/CRM.Application;component/Images/add_16x16.png"))
            //};
            addMenuItem.Click += new RoutedEventHandler(this.MenuItemAddClick);
            menu.Items.Add(addMenuItem);

        }

         
        #endregion CreateContextMenu
         private void MenuItemAddClick(object sender, RoutedEventArgs e)
         {
             MapItemForm mapItemForm = new MapItemForm(this.menuLocation);
             mapItemForm.ShowDialog();
             LoadData();
         }
        private void DeleteShapeMenuItemClicked(object sender, RoutedEventArgs e)
        {
            if (!this.menuLocation.IsEmpty)
            {
                IEnumerable<object> list = this.InformationLayer.GetItemsInLocation(this.menuLocation);


                foreach (object item in list)
                {
                    MapPinPoint shape = item as MapPinPoint;
                    if (shape != null)
                    {
                        MapItemInfo mapItemInfo = (MapItemInfo)shape.Tag;
                        if (mapItemInfo.Type == DB.MapShapeType.Center)
                        {
                            Center center = Data.CenterDB.GetCenterById((int)mapItemInfo.ID);
                            center.Latitude = null;
                            center.Longitude = null;
                            center.Detach();
                            DB.Save(center);



                        }
                        else if (mapItemInfo.Type == DB.MapShapeType.Post)
                        {
                            Post post = Data.PostDB.GetPostByID((int)mapItemInfo.ID);
                            post.Latitude = null;
                            post.Longitude = null;
                            post.Detach();
                            DB.Save(post);
                        }
                        else if (mapItemInfo.Type == DB.MapShapeType.Cabinet)
                        {
                            Cabinet cabinet = Data.CabinetDB.GetCabinetByID((int)mapItemInfo.ID);
                            cabinet.Latitude = null;
                            cabinet.Longitude = null;
                            cabinet.Detach();
                            DB.Save(cabinet);
                        }
                        LoadData();
                        break;
                    }
                }


            }
        }
    }
}
