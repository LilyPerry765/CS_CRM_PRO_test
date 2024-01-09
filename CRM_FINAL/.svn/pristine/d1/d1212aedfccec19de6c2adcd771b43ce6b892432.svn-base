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
    public partial class RequestFlowChart : UserControl
    {
        public RequestFlowChart()
        {
            InitializeComponent();
        }

        public int RequestStepID { get; set; }

        public void DrawStates()
        {
            Brush activeColor = new SolidColorBrush(Color.FromRgb(11, 71, 157));
            Brush inactiveColor = new SolidColorBrush(Color.FromRgb(153, 153, 153));
            /// Active      0b479d
            /// Inactive    999999
            /// Date        8b9dca
            /// 

            double canvasMargin = 50;
            double canvasWidth = StatusCanvas.ActualWidth - canvasMargin * 2;
            double canvasHeight = StatusCanvas.ActualHeight - canvasMargin * 2;
            double canvasHorizontalCenter = canvasWidth / 2;

            StatusCanvas.Children.Clear();

            List<RequestStep> requestSteps = WorkFlowDB.GetRequestSteps();

            double eachPartHeight = (canvasHeight / (requestSteps.Count - 1));

            for (int i = 0; i < requestSteps.Count; i++)
            {
                RequestStep item = requestSteps[i];

                Brush lineColor = (item.ID < RequestStepID) ? activeColor : inactiveColor;
                Brush circleColor = (item.ID <= RequestStepID) ? activeColor : inactiveColor;

                if (i < requestSteps.Count - 1)
                {
                    Border line = DrawLine(lineColor, eachPartHeight);
                    StatusCanvas.Children.Add(line);
                    Canvas.SetLeft(line, eachPartHeight * i + canvasMargin);
                    Canvas.SetTop(line, 30);
                }

                Border circle = DrawCircle(circleColor);
                StatusCanvas.Children.Add(circle);
                Canvas.SetTop(circle, eachPartHeight * i + canvasMargin);
                Canvas.SetLeft(circle, canvasHorizontalCenter - 24);

                Label title = new Label();
                title.Width = 200;
                title.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                title.Content = item.StepTitle;
                title.FontSize = 11;
                title.Foreground = circleColor;
                StatusCanvas.Children.Add(title);
                Canvas.SetTop(title, eachPartHeight * i - 92 + canvasMargin);
                Canvas.SetLeft(title, canvasHorizontalCenter - 2);
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DrawStates();
        }

    }
}
