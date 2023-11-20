using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfPainter.Model;

namespace WpfPainter
{
    public class ShapeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RectangleTemplate { get; set; }
        public DataTemplate EllipseTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is RectangleModel)
            {
                return RectangleTemplate;
            }
            else if (item is EllipseModel)
            {
                return EllipseTemplate;
            }

            // Return a default template if needed
            return base.SelectTemplate(item, container);
        }
    }
}
