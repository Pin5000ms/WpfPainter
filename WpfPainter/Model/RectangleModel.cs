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


        private RectangleModel currentRectangle = null;
        private Point _startPoint;
        public override ModelBase Create(Point startPoint)
        {
            _startPoint = startPoint;
            currentRectangle = new RectangleModel
            {
                PolygonPoints = new PointCollection()
                {
                    startPoint,
                    startPoint,
                    startPoint,
                    startPoint
                },
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                FillColor = Brushes.Red // You can set the initial fill color
            };
            return currentRectangle;
        }

        public override void AdjustSize(Point mousePoint)
        {
            // 更新多邊形的頂點，以實現矩形的調整
            currentRectangle.PolygonPoints = new PointCollection()
            {
                new Point(_startPoint.X, _startPoint.Y),
                new Point(mousePoint.X, _startPoint.Y),
                new Point(mousePoint.X, mousePoint.Y),
                new Point(_startPoint.X, mousePoint.Y)
            };
        }
        public override void EndCreate()
        {
            currentRectangle = null;
        }
    }
}
