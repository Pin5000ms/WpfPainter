using System.Windows;
using System.Windows.Media;
using WpfPainter.Model;

namespace WpfPainter
{
    public class StateBase
    {
        protected CanvasViewModel canvasVM;
        public ModelBase currentShape;
        public Brush fillColor;
        public SolidColorBrush stroke;
        public double thickness;

        public StateBase(CanvasViewModel _canvasVM)
        {
            canvasVM = _canvasVM;
        }

        public virtual bool MouseDown(Point startPoint)
        {
            return false;//true: �����X�ܧ�A�ݭn�[��Undo Stack
        }
        public virtual bool MouseMove(Point mousePosition)
        {
            return false;//true: �����X�ܧ�A�ݭn�[��Undo Stack
        }
        public virtual bool MouseUp()
        {
            return false;//true: �����X�ܧ�A�ݭn�[��Undo Stack
        }

        public virtual void Undo()
        {

        }

        public virtual void Redo()
        {

        }
        public virtual void SetProperty(Brush _fillColor, SolidColorBrush _stroke, double _thickness)
        {

        }
    }
}
