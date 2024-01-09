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
using CRM.Data;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for StatusLine.xaml
    /// </summary>
    public partial class StatusLine : UserControl
    {
        
        #region Properties and Fields

        public int RequestStepID { get; set; }

        #endregion

        #region Constructors

        public StatusLine()
        {
            InitializeComponent();
        } 

        #endregion

        #region Methods

        public void DrawStates(long requestID)
        {
            Brush activeColor = new SolidColorBrush(Color.FromRgb(11, 71, 157));
            Brush inactiveColor = new SolidColorBrush(Color.FromRgb(153, 153, 153));
            /// Active      0b479d
            /// Inactive    999999
            /// Date        8b9dca
            /// 

            //فاصله ابتدا و انتهای نمودار خطی مراحل یک روال از دیواره ی دیتا گرید
            double lineMargin = 50;

            double statusLineWidth = StatusLineCanvas.ActualWidth - lineMargin * 2;

            StatusLineCanvas.Children.Clear();

            Request request = Data.RequestDB.GetRequestByID(requestID);
            List<RequestStep> requestSteps = new List<RequestStep>();

            if (DB.IsFixRequest(request.RequestTypeID) || request.RequestTypeID == (int)DB.RequestType.Wireless)
            {
                requestSteps = WorkFlowDB.GetRecursiveRequestSteps(request.RequestTypeID);
                double eachPartWidth = (statusLineWidth / (requestSteps.Count - 1));

                for (int i = 0; i < requestSteps.Count; i++)
                {
                    RequestStep requestStep = requestSteps[i];
                    int currentindex = requestSteps.IndexOf(requestSteps.Where(t => t.ID == RequestStepID).SingleOrDefault());
                    int itemIndex = requestSteps.IndexOf(requestSteps.Where(t => t.ID == requestStep.ID).SingleOrDefault());

                    Brush lineColor = (itemIndex < currentindex) ? activeColor : inactiveColor;
                    Brush circleColor = (itemIndex <= currentindex) ? activeColor : inactiveColor;

                    if (i < requestSteps.Count - 1)
                    {
                        Border line = DrawLine(lineColor, eachPartWidth);
                        StatusLineCanvas.Children.Add(line);
                        Canvas.SetLeft(line, eachPartWidth * i + lineMargin);
                        Canvas.SetTop(line, 30);
                    }

                    Border circle = DrawCircle(circleColor);
                    StatusLineCanvas.Children.Add(circle);
                    Canvas.SetLeft(circle, eachPartWidth * i + lineMargin);
                    Canvas.SetTop(circle, 24);

                    Label title = new Label();
                    title.Width = 200;
                    title.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    title.Content = requestStep.StepTitle;
                    title.FontSize = 11;
                    title.Foreground = circleColor;
                    StatusLineCanvas.Children.Add(title);
                    Canvas.SetLeft(title, eachPartWidth * i - 92 + lineMargin);
                    Canvas.SetTop(title, 2);
                }
            }
            else
            {
                requestSteps = WorkFlowDB.GetRequestSteps(request.RequestTypeID).Where(t => t.IsVisible == true).ToList();

                double eachPartWidth = (statusLineWidth / (requestSteps.Count - 1));

                for (int i = 0; i < requestSteps.Count; i++)
                {
                    RequestStep requestStep = requestSteps[i];

                    Brush lineColor = (requestStep.ID < RequestStepID) ? activeColor : inactiveColor;
                    Brush circleColor = (requestStep.ID <= RequestStepID) ? activeColor : inactiveColor;

                    if (i < requestSteps.Count - 1)
                    {
                        Border line = DrawLine(lineColor, eachPartWidth);
                        StatusLineCanvas.Children.Add(line);
                        Canvas.SetLeft(line, eachPartWidth * i + lineMargin);
                        Canvas.SetTop(line, 30);
                    }

                    Border circle = DrawCircle(circleColor);
                    StatusLineCanvas.Children.Add(circle);
                    Canvas.SetLeft(circle, eachPartWidth * i + lineMargin);
                    Canvas.SetTop(circle, 24);

                    Label title = new Label();
                    title.Width = 200;
                    title.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    title.Content = requestStep.StepTitle;
                    title.FontSize = 11;
                    title.Foreground = circleColor;
                    StatusLineCanvas.Children.Add(title);
                    Canvas.SetLeft(title, eachPartWidth * i - 92 + lineMargin);
                    Canvas.SetTop(title, 2);
                }
            }
        }

        private static Border DrawLine(Brush inactiveColor, double width)
        {
            Border line = new Border();
            line.CornerRadius = new CornerRadius(3);
            line.BorderThickness = new Thickness(0.5);
            line.Background = inactiveColor;
            line.Height = 5.0;
            line.Width = width + 3;
            line.Margin = new Thickness(0);
            return line;
        }

        private static Border DrawCircle(Brush color)
        {
            Border circle = new Border();
            circle.Width = 16;
            circle.Height = 16;
            circle.BorderThickness = new Thickness(4);
            circle.BorderBrush = color;
            circle.Background = Brushes.White;
            circle.CornerRadius = new CornerRadius(8);
            return circle;
        }

        #endregion

        #region EventHandlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // DrawStates();
        } 

        #endregion

    }
}
