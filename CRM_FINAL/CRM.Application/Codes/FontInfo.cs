using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CRM.Application.Codes
{
    //TODO:rad
    public class FontInfo
    {
        #region Properties and Fields

        public FontFamily Family { get; set; }

        public double Size { get; set; }

        public FontStyle Style { get; set; }

        public TextDecorationCollection TextDecorations { get; set; }

        public FontWeight Weight { get; set; }

        public SolidColorBrush BrushColor { get; set; }

        #endregion

        #region Static Utils

        public static void ApplyFont(Control control, FontInfo font)
        {
            control.FontFamily = font.Family;
            control.FontSize = font.Size;
            control.FontStyle = font.Style;
            control.FontWeight = font.Weight;
            control.Foreground = font.BrushColor;
        }

        public static FontInfo GetControlFont(Control control)
        {
            FontInfo font = new FontInfo();
            font.Family = control.FontFamily;
            font.Size = control.FontSize;
            font.Style = control.FontStyle;
            font.Weight = control.FontWeight;
            font.BrushColor = (SolidColorBrush)control.Foreground;
            return font;
        }

        #endregion

        #region Constructor

        public FontInfo()
        {

        }

        public FontInfo(FontFamily fm, double sz, FontStyle style, TextDecorationCollection decorations, FontWeight weight, SolidColorBrush c)
        {
            this.Family = fm;
            this.Size = sz;
            this.Style = style;
            this.TextDecorations = decorations;
            this.Weight = weight;
            this.BrushColor = c;
        }

        #endregion
    }
}
