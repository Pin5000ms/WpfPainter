using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using WpfPainter.Model;

namespace WpfPainter
{
    public class LineModel : ModelBase
    {
        private double x1;
        public double X1
        {
            get { return x1; }
            set
            {
                x1 = value;
                OnPropertyChanged();
            }
        }

        private double y1;
        public double Y1
        {
            get { return y1; }
            set
            {
                y1 = value;
                OnPropertyChanged();
            }
        }

        private double x2;
        public double X2
        {
            get { return x2; }
            set
            {
                x2 = value;
                OnPropertyChanged();
            }
        }

        private double y2;
        public double Y2
        {
            get { return y2; }
            set
            {
                y2 = value;
                OnPropertyChanged();
            }
        }


        private double outlineThickness;

        public double OutlineThickness
        {
            get { return outlineThickness; }
            set
            {
                outlineThickness = value;
                OnPropertyChanged();
            }
        }

        private Point _startPoint;
        public override ModelBase Create(Point startPoint)
        {
            _startPoint = startPoint;
            currentModel = new LineModel
            {
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = startPoint.X,
                Y2 = startPoint.Y
            };
            return currentModel;
        }

        public override void AdjustSize(Point mousePoint)
        {
            // 更新多邊形的頂點，以實現矩形的調整
            (currentModel as LineModel).X2 = mousePoint.X;
            (currentModel as LineModel).Y2 = mousePoint.Y;
            //用以繪製包圍的矩形
            currentModel.X = Math.Min(_startPoint.X, mousePoint.X) - 8;
            currentModel.Y = Math.Min(_startPoint.Y, mousePoint.Y) - 8;
            currentModel.Width = Math.Abs(mousePoint.X - _startPoint.X) + 16;
            currentModel.Height = Math.Abs(mousePoint.Y - _startPoint.Y) + 16;
        }

        public override bool IsPointInside(Point point)
        {
            double a = Y2 - Y1;
            double b = X1 - X2;
            double c = X2 * Y1 - X1 * Y2;

            double distance = Math.Abs(a * point.X + b * point.Y + c) / Math.Sqrt(a * a + b * b);

            // 設定一個閾值，小於這個閾值視為在直線上
            double epsilon = 10;

            return distance < epsilon;
        }

        public override void MoveBy(double deltaX, double deltaY)
        {
            X1 += deltaX;
            X2 += deltaX;
            Y1 += deltaY;
            Y2 += deltaY;

            //用以繪製包圍的矩形
            X += deltaX;
            Y += deltaY;
        }

        public override void SetProperty(Brush _fillColor, SolidColorBrush _stroke, double _thickness)
        {
            base.SetProperty(_fillColor, _stroke, _thickness);
            OutlineThickness = StrokeThickness + 2;
        }
    }
}
