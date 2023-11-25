using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPainter
{
    public class EraseState : StateBase
    {
        public EraseState(CanvasViewModel canvasVM) : base(canvasVM)
        {
             
        }

        public override void MouseDown(Point startPoint)
        {
            foreach (var item in _canvasVM.Objects)
            {
                if (item.IsPointInside(startPoint))
                {
                    currentShape = item;
                }
            }
            _canvasVM.Objects.Remove(currentShape);
        }


        public override void MouseMove(Point mousePosition)
        {
            foreach (var item in _canvasVM.Objects)
            {
                if (item.IsPointInside(mousePosition))
                {
                    currentShape = item;
                }
            }
            _canvasVM.Objects.Remove(currentShape);
        }
        public override void MouseUp()
        {
            currentShape = null;
        }
    }
}
