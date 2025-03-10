﻿using System;
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
        public DataTemplate TriangleTemplate { get; set; }
        public DataTemplate EllipseTemplate { get; set; }
        public DataTemplate PolylineTemplate { get; set; }

        public DataTemplate LineTemplate { get; set; }



        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is RectangleModel)
            {
                return RectangleTemplate;
            }
            else if (item is TriangleModel)
            {
                return TriangleTemplate;
            }
            else if (item is EllipseModel)
            {
                return EllipseTemplate;
            }
            else if(item is PolylineModel) 
            {
                return PolylineTemplate;
            }
            else if (item is LineModel)
            {
                return LineTemplate;
            }

            // Return a default template if needed
            return base.SelectTemplate(item, container);
        }
    }
}
