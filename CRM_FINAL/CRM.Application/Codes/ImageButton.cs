using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CRM.Application.UserControls
{
    public class ImageButton : Button
    {
        public ImageButton()
        {
            this.IsEnabledChanged += new DependencyPropertyChangedEventHandler(ImageButton_IsEnabledChanged);
        }

        void ImageButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
             if (!(bool)e.NewValue)
                 this.Effect = new GreyscaleEffect();
             else
                 this.Effect = null;
        }


        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ImageButton));

        public ImageSource Source
        {
            get { return base.GetValue(SourceProperty) as ImageSource; }
            set { base.SetValue(SourceProperty, value); }
        }

    }
}