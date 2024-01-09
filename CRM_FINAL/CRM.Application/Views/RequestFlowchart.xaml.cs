using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System.Data.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace CRM.Application.Views
{
    public partial class RequestFlowchart : Local.TabWindow
    {
        Brush _ActiveColor = new BrushConverter().ConvertFromString("#0B479D") as SolidColorBrush;
        Brush _InactiveColor = new BrushConverter().ConvertFromString("#999999") as SolidColorBrush;

        public RequestFlowchart()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {

        }

        public long RequestID { get; set; }

        public void DrawStates()
        {
            double canvasMargin = 50;
            double canvasWidth = StatusCanvas.ActualWidth - canvasMargin * 2;
            double canvasHeight = StatusCanvas.ActualHeight - canvasMargin * 2;
            double canvasHorizontalCenter = StatusCanvas.ActualWidth / 2;
            //StatusCanvas.Children.Clear();

            // TODO: Get only requestTypeID
            Request request = RequestDB.GetRequestByID(RequestID);
            
            List<WorkFlowNode> workFlowNodes = Local.Workflow.GetRequestProgressAndLevels(request.RequestTypeID);

            int nodesCount = workFlowNodes.Max(t => t.Level);

            double eachPartHeight = canvasHeight / nodesCount;
            double childNodesMargin = 200;

            List<long> drawnIDs = new List<long>();

            foreach (WorkFlowNode item in workFlowNodes)
            {
                int siblingsCount = workFlowNodes.Where(t => t.Level == item.Level).Count();
                int nodePosition = workFlowNodes.Where(t => t.Level == item.Level && !drawnIDs.Contains(t.ID)).Count() - 1;

                double top = eachPartHeight * item.Level + canvasMargin;
                double left = canvasHorizontalCenter + (nodePosition - siblingsCount / 2.0) * childNodesMargin;

                Draw(top, left, eachPartHeight, item);
                drawnIDs.Add(item.ID);
            }

            DrawLines();
        }

        private void DrawLines()
        {
            foreach (Border circle in StatusCanvas.Children.Cast<UIElement>().Where(t => t.GetType() == typeof(Border)).ToList())
                foreach (Border nextCircle in StatusCanvas.Children.Cast<UIElement>().Where(t => t.GetType() == typeof(Border)).ToList())
                    if ((int)nextCircle.Tag == (int)circle.Tag + 1)
                        DrawLine(_ActiveColor, new Point(Canvas.GetLeft(circle) + circle.Width / 2, Canvas.GetTop(circle) + circle.Height / 2), new Point(Canvas.GetLeft(nextCircle) + circle.Width / 2, Canvas.GetTop(nextCircle) + circle.Height / 2));
        }

        private void Draw(double top, double left, double eachPartHeight, WorkFlowNode item, bool isLastNode = false)
        {
            Brush circleColor = (item.ID <= RequestID) ? _ActiveColor : _InactiveColor;

            Border circle = DrawCircle(circleColor);
            circle.Tag = item.Level;
            Canvas.SetTop(circle, top);
            Canvas.SetLeft(circle, left - 8);

            Label title = SetTitle(item.Name, circleColor);
            Canvas.SetTop(title, top - 5);
            Canvas.SetLeft(title, left - 208);
        }


        void circle_MouseLeave(object sender, MouseEventArgs e)
        {
            InfoPopup.IsOpen = false;
        }

        void circle_MouseEnter(object sender, MouseEventArgs e)
        {
            #region Polygon runtime creation
            /*
            Polygon polygon = new Polygon();

            Point clickPoint = e.GetPosition(StatusCanvas);

            double h = 150;//UserLogBoxPolygon.Height - 2;
            double w = 250;//UserLogBoxPolygon.Width - 12;
            double x = 0;//clickPoint.X + 10;
            double y = 0;// clickPoint.Y - h / 2;
            double m = 20;

            PointCollection pointCollection = new PointCollection();
            pointCollection.Add(new Point(x, y + h / 2));
            pointCollection.Add(new Point(x + m, y + h / 2 - m));
            pointCollection.Add(new Point(x + m, y));
            pointCollection.Add(new Point(x + m + w, y));
            pointCollection.Add(new Point(x + m + w, y + h));
            pointCollection.Add(new Point(x + m, y + h));
            pointCollection.Add(new Point(x + m, y + h / 2 + m));

            polygon.Fill = new BrushConverter().ConvertFromString("#FFE8B6") as SolidColorBrush;
            polygon.Stroke = new BrushConverter().ConvertFromString("#E6A107") as SolidColorBrush;
            polygon.StrokeThickness = 1;
            polygon.Points.Clear();
            polygon.Points = pointCollection;

            InfoPopup.Child = polygon;
            */
            #endregion

            InfoPopup.PlacementTarget = sender as Border;
            InfoPopup.Placement = PlacementMode.Custom;
            InfoPopup.CustomPopupPlacementCallback = new CustomPopupPlacementCallback(placePopup);
            InfoPopup.IsOpen = true;
        }

        public CustomPopupPlacement[] placePopup(Size popupSize, Size targetSize, Point offset)
        {
            double circleWidth = 16;
            double polygonHeight = 150;

            CustomPopupPlacement placement1 = new CustomPopupPlacement(new Point(circleWidth, circleWidth / 2 - polygonHeight / 2), PopupPrimaryAxis.Vertical);
            CustomPopupPlacement placement2 = new CustomPopupPlacement(new Point(0, circleWidth / 2 + 1), PopupPrimaryAxis.Horizontal);
            CustomPopupPlacement[] ttplaces = new CustomPopupPlacement[] { placement1, placement2 };
            return ttplaces;
        }

        private void DrawLine(Brush color, Point p1, Point p2)
        {
            Line line = new Line();
            line.X1 = p1.X;
            line.Y1 = p1.Y;
            line.X2 = p2.X;
            line.Y2 = p2.Y;
            line.Stroke = color;
            line.StrokeThickness = 3;
            StatusCanvas.Children.Add(line);
            Canvas.SetZIndex(line, 0);
        }

        private Border DrawCircle(Brush color)
        {
            Border circle = new Border();
            circle.Width = 16;
            circle.Height = 16;
            circle.BorderThickness = new Thickness(4);
            circle.BorderBrush = color;
            circle.Background = Brushes.White;
            circle.CornerRadius = new CornerRadius(8);
            circle.MouseEnter += new MouseEventHandler(circle_MouseEnter);
            circle.MouseLeave += new MouseEventHandler(circle_MouseLeave);
            StatusCanvas.Children.Add(circle);
            Canvas.SetZIndex(circle, 1);
            return circle;
        }

        private Label SetTitle(string name, Brush circleColor)
        {
            Label title = new Label();
            title.Width = 200;
            title.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            title.Content = name;
            title.FontSize = 11;
            title.Foreground = circleColor;
            StatusCanvas.Children.Add(title);
            return title;
        }

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawStates();
        }
    }
}
