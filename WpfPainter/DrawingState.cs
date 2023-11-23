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
        private ModelBase currentShape = new PolylineModel();

        public DrawingState(CanvasViewModel canvasVM) : base(canvasVM)
        {
        }

        public override void MouseDown(Point startPoint)
        {
            _canvasVM.Objects.Add(currentShape.Create(startPoint));
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
            //currentShape = null;
        }
    }
}
