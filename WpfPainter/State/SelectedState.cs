using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using WpfPainter.Model;

namespace WpfPainter
{
    public class SelectedState : StateBase
    {
        public SelectedState(CanvasViewModel canvasVM) : base(canvasVM)
        {
        }
        double preX;
        double preY;
        double startX;
        double startY;

        private Stack<Move> undoStack = new Stack<Move>();
        private Stack<Move> redoStack = new Stack<Move>();

        public override bool MouseDown(Point startPoint)
        {
            currentShape = null;
            foreach (var item in canvasVM.Objects)
            {
                item.IsSelected = false;
            }
            foreach (var item in canvasVM.Objects)
            {
                if (item.IsPointInside(startPoint))
                {
                    currentShape = item;
                    currentShape.IsSelected = true;
                    startX = startPoint.X;
                    startY = startPoint.Y;
                    preX = startPoint.X;
                    preY = startPoint.Y;
                    break;
                }
            }
            return false;
        }


        public override bool MouseMove(Point mousePosition)
        {
            if (currentShape != null)
            {
                double deltaX = mousePosition.X - preX;
                double deltaY = mousePosition.Y - preY;

                currentShape.MoveBy(deltaX, deltaY);

                preX = mousePosition.X;
                preY = mousePosition.Y;
            }
            return false;
        }
        public override bool MouseUp()
        {
            bool result = false;
            if (preX - startX != 0 || preY - startY != 0)
            {
                Move delta = new Move()
                {
                    objMoved = currentShape,
                    deltaX = preX - startX,
                    deltaY = preY - startY
                };
                undoStack.Push(delta);
                result = true;
            }
            return result;
        }

        public override void Undo()
        {
            if (undoStack.Count > 0)
            {
                Move lastAction = undoStack.Pop();
                lastAction.objMoved.MoveBy(-lastAction.deltaX, -lastAction.deltaY);
                redoStack.Push(lastAction);
            }
        }

        public override void Redo()
        {
            if (redoStack.Count > 0)
            {
                Move redoAction = redoStack.Pop();
                redoAction.objMoved.MoveBy(redoAction.deltaX, redoAction.deltaY);
            }
        }

        public override void SetProperty(Brush _fillColor, SolidColorBrush _stroke, double _thickness)
        {
            fillColor = _fillColor;
            stroke = _stroke;
            thickness = _thickness;

            //當前選擇的項目改變顏色
            if (currentShape != null)
            {
                currentShape.SetProperty(fillColor, stroke, thickness);
            }
        }

        public class Move
        {
            public ModelBase objMoved;
            public double deltaX;
            public double deltaY;
        }
    }
}
