using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfPainter.Model
{
    public class PolylineModel : ModelBase
    {
        private PointCollection polylinePoints;

        public PointCollection PolylinePoints
        {
            get { return polylinePoints; }
            set
            {
                if (value != polylinePoints)
                {
                    polylinePoints = value;
                    OnPropertyChanged();
                }
            }
        }

        PolylineModel currentModel = null;

        PointCollection buffer_points = new PointCollection();
        public override ModelBase Create(Point startPoint)
        {
            buffer_points.Clear();
            buffer_points.Add(startPoint);
            currentModel = new PolylineModel
            {
                PolylinePoints = buffer_points,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
            };
            return currentModel;
        }

        public override void AdjustSize(Point mousePoint)
        {
            buffer_points.Add(mousePoint);
            currentModel.PolylinePoints = new PointCollection(buffer_points);
        }
        public override void EndCreate()
        {
            currentModel = null;
        }

        public override bool IsPointInside(Point point)
        {
            bool result = false;
            for (int i = 0; i < PolylinePoints.Count; i++)
            {
                double dist = (point.X - PolylinePoints[i].X) * (point.X - PolylinePoints[i].X) + (point.Y - PolylinePoints[i].Y) * (point.Y - PolylinePoints[i].Y);
                if (dist < 25)
                {
                    return true;
                }
            }
            return result;
        }

        public override void MoveBy(double deltaX, double deltaY)
        {
            List<Point> newPoints = new List<Point>();
            foreach (var p in PolylinePoints)
            {
                newPoints.Add(new Point(p.X + deltaX, p.Y + deltaY));
            }
            PolylinePoints = new PointCollection(newPoints);
        }
    }
}
