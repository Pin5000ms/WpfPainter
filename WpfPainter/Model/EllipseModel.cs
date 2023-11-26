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
        private Point _startPoint;

        public override ModelBase Create(Point startPoint)
        {
            _startPoint = startPoint;
            currentModel = new EllipseModel
            {
                X = startPoint.X,
                Y = startPoint.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                FillColor = Brushes.Red
            };
            return currentModel;
        }

        public override void AdjustSize(Point mousePoint)
        {
            if (currentModel != null)
            {
                double width = Math.Abs(mousePoint.X - _startPoint.X);
                double height = Math.Abs(mousePoint.Y - _startPoint.Y);

                // 計算橢圓的位置
                double x = Math.Min(_startPoint.X, mousePoint.X);
                double y = Math.Min(_startPoint.Y, mousePoint.Y);

                currentModel.X = x;
                currentModel.Y = y;

                // 設定橢圓的寬度和高度
                currentModel.Width = width;
                currentModel.Height = height;

            }
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
