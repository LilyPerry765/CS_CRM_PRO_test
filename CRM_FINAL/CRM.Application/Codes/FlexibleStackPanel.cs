using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CRM.Application.Codes
{

    public class FlexibleStackPanel : StackPanel
    {
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var itemsHeight = arrangeSize.Height / Children.Count;
            for (var i = 0; i < Children.Count; i++)
                Children[i].Arrange(new Rect(0, i * itemsHeight, arrangeSize.Width, itemsHeight));
            return arrangeSize;
        }
    }


}
