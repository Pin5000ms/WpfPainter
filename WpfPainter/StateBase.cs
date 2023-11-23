using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPainter
{
    public class StateBase
    {
        protected CanvasViewModel _canvasVM;
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
    }
}
