using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WpfPainter.MVVM;

namespace WpfPainter.Model
{
    public class ModelBase : ObservableObject
    {
        private double x;

        public double X
        {
            get { return x; }
            set 
            { 
                x = value;
                OnPropertyChanged();
            }
        }

        private double y;

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }

        private double w;

        public double Width
        {
            get { return w; }
            set
            {
                w = value;
                OnPropertyChanged();
            }
        }

        private double h;

        public double Height
        {
            get { return h; }
            set
            {
                h = value;
                OnPropertyChanged();
            }
        }

        private Brush c;

        public Brush FillColor
        {
            get { return c; }
            set
            {
                c = value;
                OnPropertyChanged();
            }
        }


        private SolidColorBrush stroke;

        public SolidColorBrush Stroke
        {
            get { return stroke; }
            set
            {
                stroke = value;
                OnPropertyChanged();
            }
        }


        private double strokeThickness;

        public double StrokeThickness
        {
            get { return strokeThickness; }
            set
            {
                strokeThickness = value;
                OnPropertyChanged();
            }
        }


        public virtual void MoveBy(double deltaX, double deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }

        public virtual bool IsPointInside(Point point)
        {
            bool result = false;
            if(point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height)
            {
                result = true;
            }
            return result;
        }


        public virtual ModelBase Create(Point startPoint)
        {
            return null;
        }

        public virtual void AdjustSize(Point currPoint)
        {

        }

        public virtual void EndCreate()
        {

        }

    }
}
