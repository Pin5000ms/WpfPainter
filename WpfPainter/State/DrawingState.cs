using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfPainter.Model;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfPainter
{
    public class DrawingState : StateBase
    {
        public DrawingState(CanvasViewModel canvasVM) : base(canvasVM)
        {
        }

        public ModelBase newInstance;
        public override void MouseDown(Point startPoint)
        {
            newInstance = currentShape.Create(startPoint);
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
            foreach (var item in _canvasVM.Objects)
            {
                item.IsSelected = false;
            }
            //畫完時要自動選取當前形狀
            newInstance.IsSelected = true;
            currentShape.EndCreate();
        }


        public override void SetProperty(Brush _fillColor, SolidColorBrush _stroke, double _thickness)
        {
            fillColor = _fillColor;
            stroke = _stroke;
            thickness = _thickness;

            //當前畫完的項目改變顏色
            if (newInstance != null)
            {
                newInstance.Stroke = stroke;
                newInstance.StrokeThickness = thickness;
                newInstance.FillColor = fillColor;
            }
        }
    }
}
