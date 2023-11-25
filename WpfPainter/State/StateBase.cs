using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WpfPainter.Model;

namespace WpfPainter
{
    public class StateBase
    {
        protected CanvasViewModel _canvasVM;
        public ModelBase currentShape;
        public Brush fillColor;
        public SolidColorBrush stroke;
        public double thickness;

        public StateBase(CanvasViewModel canvasVM)
        {
            _canvasVM = canvasVM;
        }

        public virtual void MouseDown(Point startPoint)
        {

        }
        public virtual void MouseMove(Point mousePosition)
        {

        }
        public virtual void MouseUp()
        {

        }
        public virtual void SetProperty(Brush _fillColor, SolidColorBrush _stroke, double _thickness)
        {
            
        }
    }
}
