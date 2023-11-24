using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfPainter.Model;
using System.Windows.Input;

namespace WpfPainter
{
    public class DrawingState : StateBase
    {
        public DrawingState(CanvasViewModel canvasVM) : base(canvasVM)
        {
        }

        public override void MouseDown(Point startPoint)
        {
            var newInstance = currentShape.Create(startPoint);
            newInstance.FillColor = fillColor;
            newInstance.Stroke = stroke;
            newInstance.StrokeThickness = thickness;
            _canvasVM.Objects.Add(newInstance);
        }
        public override void MouseMove(Point mousePosition)
        {
            if (currentShape != null)
            {
                currentShape.AdjustSize(mousePosition);
            }
        }
        public override void MouseUp()
        {
            currentShape.EndCreate();
        }
    }
}
