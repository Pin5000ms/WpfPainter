using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;

namespace WpfPainter.Model
{
    public class RectangleModel: ModelBase
    {

        private PointCollection polygonPoints;

        public PointCollection PolygonPoints
        {
            get { return polygonPoints; }
            set
            {
                if (value != polygonPoints)
                {
                    polygonPoints = value;
                    OnPropertyChanged();
                }
            }
        }

        private Point _startPoint;
        public override ModelBase Create(Point startPoint)
        {
            _startPoint = startPoint;
            currentModel = new RectangleModel
            {
                PolygonPoints = new PointCollection()
                {
                    startPoint,
                    startPoint,
                    startPoint,
                    startPoint
                },
            };
            return currentModel;
        }

        public override void AdjustSize(Point mousePoint)
        {
            // 更新多邊形的頂點，以實現矩形的調整
            (currentModel as RectangleModel).PolygonPoints = new PointCollection()
            {
                new Point(_startPoint.X, _startPoint.Y),
                new Point(mousePoint.X, _startPoint.Y),
                new Point(mousePoint.X, mousePoint.Y),
                new Point(_startPoint.X, mousePoint.Y)
            };
            //用以繪製包圍的矩形
            currentModel.X = Math.Min(_startPoint.X, mousePoint.X) - 8;
            currentModel.Y = Math.Min(_startPoint.Y, mousePoint.Y) - 8;
            currentModel.Width = Math.Abs(mousePoint.X - _startPoint.X) + 16;
            currentModel.Height = Math.Abs(mousePoint.Y - _startPoint.Y) + 16;
        }


        public override bool IsPointInside(Point point)
        {
            int intersectCount = 0;
            for (int i = 0; i < polygonPoints.Count; i++)
            {
                Point p1 = polygonPoints[i];
                Point p2 = polygonPoints[(i + 1) % polygonPoints.Count];

                // 檢查射線是否和多邊形的邊有交點
                if ((p1.Y <= point.Y && point.Y < p2.Y) || (p2.Y <= point.Y && point.Y < p1.Y))
                {
                    // 計算交點的 x 坐標
                    double intersectionX = (point.Y - p1.Y) / (p2.Y - p1.Y) * (p2.X - p1.X) + p1.X;

                    // 如果交點在射線右側，增加交點數
                    if (point.X < intersectionX)
                    {
                        intersectCount++;
                    }
                }
            }

            // 判斷交點數奇偶性
            return intersectCount % 2 == 1;
        }

        public override void MoveBy(double deltaX, double deltaY)
        {
            List<Point> newPoints = new List<Point>();
            foreach (var p in PolygonPoints)
            {
                newPoints.Add(new Point(p.X + deltaX, p.Y + deltaY));
            }
            PolygonPoints = new PointCollection(newPoints);

            //用以繪製包圍的矩形
            X += deltaX;
            Y += deltaY;
        }
    }
}
