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

        PointCollection buffer_points = new PointCollection();
        public override ModelBase Create(Point startPoint)
        {
            buffer_points.Clear();
            buffer_points.Add(startPoint);
            currentModel = new PolylineModel
            {
                PolylinePoints = buffer_points,
            };
            return currentModel;
        }

        public override void AdjustSize(Point mousePoint)
        {
            buffer_points.Add(mousePoint);
            (currentModel as PolylineModel).PolylinePoints = new PointCollection(buffer_points);
        }


        public override bool IsPointInside(Point point)
        {
            bool result = false;
            for (int i = 0; i < PolylinePoints.Count; i++)
            {
                double dist = (point.X - PolylinePoints[i].X) * (point.X - PolylinePoints[i].X) + (point.Y - PolylinePoints[i].Y) * (point.Y - PolylinePoints[i].Y);
                if (dist < 100)
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

        public override void SetProperty(Brush _fillColor, SolidColorBrush _stroke, double _thickness)
        {
            base.SetProperty(_fillColor, _stroke, _thickness);
            OutlineThickness = StrokeThickness + 2;
        }
    }
}
