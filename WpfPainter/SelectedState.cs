using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfPainter.Model;

namespace WpfPainter
{
    public class SelectedState: StateBase
    {
        private ModelBase currentShape;

        public SelectedState(CanvasViewModel canvasVM) : base(canvasVM)
        {
        }
        double preX;
        double preY;

        public override void MouseDown(Point startPoint)
        {
            foreach (var item in _canvasVM.Objects)
            {
                if (item.IsPointInside(startPoint))
                {
                    currentShape = item;
                    preX = startPoint.X;
                    preY = startPoint.Y;
                }
            }
        }
        

        public override void MouseMove(Point mousePosition)
        {
            if (currentShape != null)
            {
                double deltaX = mousePosition.X - preX;
                double deltaY = mousePosition.Y - preY;
                
                currentShape.MoveBy(deltaX, deltaY);

                preX = mousePosition.X;
                preY = mousePosition.Y;
            }
        }
        public override void MouseUp()
        {
            currentShape = null;
        }
    }
}
