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
    public class SelectedState: StateBase
    {
        public SelectedState(CanvasViewModel canvasVM) : base(canvasVM)
        {
        }
        double preX;
        double preY;

        public override void MouseDown(Point startPoint)
        {
            currentShape = null;
            foreach (var item in _canvasVM.Objects)
            {
                item.IsSelected = false;
            }
            foreach (var item in _canvasVM.Objects)
            {
                if (item.IsPointInside(startPoint))
                {
                    currentShape = item;
                    currentShape.IsSelected = true;
                    preX = startPoint.X;
                    preY = startPoint.Y;
                    break;
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
            //currentShape = null;
        }

        public override void SetProperty(Brush _fillColor, SolidColorBrush _stroke, double _thickness)
        {
            fillColor = _fillColor;
            stroke = _stroke;
            thickness = _thickness;

            //當前選擇的項目改變顏色
            if (currentShape != null)
            {
                currentShape.Stroke = stroke;
                currentShape.StrokeThickness = thickness;
                currentShape.FillColor = fillColor;
            }
        }
    }
}
