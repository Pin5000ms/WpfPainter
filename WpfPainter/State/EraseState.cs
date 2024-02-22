using System.Collections.Generic;
using System.Windows;
using WpfPainter.Model;

namespace WpfPainter
{
    public class EraseState : StateBase
    {
        public EraseState(CanvasViewModel canvasVM) : base(canvasVM)
        {

        }

        private Stack<ModelBase> undoStack = new Stack<ModelBase>();
        private Stack<ModelBase> redoStack = new Stack<ModelBase>();
        public override bool MouseDown(Point startPoint)
        {
            currentShape = null;
            bool result = false;
            foreach (var item in canvasVM.Objects)
            {
                if (item.IsPointInside(startPoint))
                {
                    currentShape = item;
                }
            }
            if (currentShape != null)
            {
                canvasVM.Objects.Remove(currentShape);
                undoStack.Push(currentShape);
                result = true;
            }
            return result;
        }


        public override bool MouseMove(Point mousePosition)
        {
            currentShape = null;
            bool result = false;
            foreach (var item in canvasVM.Objects)
            {
                if (item.IsPointInside(mousePosition))
                {
                    currentShape = item;
                }
            }
            if (currentShape != null)
            {
                canvasVM.Objects.Remove(currentShape);
                undoStack.Push(currentShape);
                result = true;
            }
            return result;
        }
        public override bool MouseUp()
        {
            currentShape = null;
            return false;
        }


        public override void Undo()
        {
            if (undoStack.Count > 0)
            {
                ModelBase lastAction = undoStack.Pop();
                canvasVM.Objects.Add(lastAction);
                redoStack.Push(lastAction);
            }
        }

        public override void Redo()
        {
            if (redoStack.Count > 0)
            {
                ModelBase redoAction = redoStack.Pop();
                canvasVM.Objects.Remove(redoAction);
            }
        }
    }
}
