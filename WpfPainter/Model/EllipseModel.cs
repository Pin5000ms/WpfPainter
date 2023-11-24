using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace WpfPainter.Model
{
    public class EllipseModel : ModelBase
    {

        //private double radiusX;
        //public double RadiusX
        //{
        //    get { return radiusX; }
        //    set
        //    {
        //        radiusX = value;
        //        OnPropertyChanged();
        //    }
        //}


        //private double radiusY;
        //public double RadiusY
        //{
        //    get { return radiusY; }
        //    set
        //    {
        //        radiusY = value;
        //        OnPropertyChanged();
        //    }
        //}


        private EllipseModel currentEllipse = null;
        private Point _startPoint;

        public override ModelBase Create(Point startPoint)
        {
            _startPoint = startPoint;
            currentEllipse = new EllipseModel
            {
                X = startPoint.X,
                Y = startPoint.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                FillColor = Brushes.Red
            };
            return currentEllipse;
        }

        public override void AdjustSize(Point mousePoint)
        {
            if (currentEllipse != null)
            {
                double width = Math.Abs(mousePoint.X - _startPoint.X);
                double height = Math.Abs(mousePoint.Y - _startPoint.Y);

                // 計算橢圓的位置
                double x = Math.Min(_startPoint.X, mousePoint.X);
                double y = Math.Min(_startPoint.Y, mousePoint.Y);

                currentEllipse.X = x;
                currentEllipse.Y = y;

                // 設定橢圓的寬度和高度
                currentEllipse.Width = width;
                currentEllipse.Height = height;

            }
        }
        public override void EndCreate()
        {
            currentEllipse = null;
        }

        public override bool IsPointInside(Point point)
        {
            // 橢圓的中心座標
            Point ellipseCenter = new Point(X + Width / 2,Y + Height / 2);

            // 檢查點到橢圓中心的距離是否小於等於橢圓的半長軸和半短軸的平均值
            double distance = Math.Sqrt(Math.Pow((point.X - ellipseCenter.X) / (Width / 2), 2) + Math.Pow((point.Y - ellipseCenter.Y) / (Height / 2), 2));

            return distance <= 1.0;
        }
    }
}
