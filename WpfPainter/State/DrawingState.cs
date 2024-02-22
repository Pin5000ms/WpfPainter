using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using WpfPainter.Model;

namespace WpfPainter
{
    public class DrawingState : StateBase
    {
        public DrawingState(CanvasViewModel canvasVM) : base(canvasVM)
        {
        }

        private Stack<ModelBase> undoStack = new Stack<ModelBase>();
        private Stack<ModelBase> redoStack = new Stack<ModelBase>();

        public ModelBase newInstance;
        public override bool MouseDown(Point startPoint)
        {
            newInstance = currentShape.Create(startPoint);
            newInstance.SetProperty(fillColor, stroke, thickness);
            canvasVM.Objects.Add(newInstance);
            undoStack.Push(newInstance);
            return true;
        }
        public override bool MouseMove(Point mousePosition)
        {
            if (currentShape != null)
            {
                currentShape.AdjustSize(mousePosition);
            }
            return false;
        }
        public override bool MouseUp()
        {
            //畫完時要自動選取當前形狀
            foreach (var item in canvasVM.Objects)
            {
                item.IsSelected = false;
            }
            currentShape.EndCreate();
            return false;
        }

        public override void Undo()
        {
            if (undoStack.Count > 0)
            {
                ModelBase lastAction = undoStack.Pop();
                canvasVM.Objects.Remove(lastAction);
                redoStack.Push(lastAction);
            }
        }

        public override void Redo()
        {
            if (redoStack.Count > 0)
            {
                ModelBase redoAction = redoStack.Pop();
                canvasVM.Objects.Add(redoAction);
            }
        }
        public override void SetProperty(Brush _fillColor, SolidColorBrush _stroke, double _thickness)
        {
            fillColor = _fillColor;
            stroke = _stroke;
            thickness = _thickness;

            //當前畫完的項目改變顏色
            if (newInstance != null)
            {
                newInstance.SetProperty(fillColor, stroke, thickness);
            }
        }
    }
}
