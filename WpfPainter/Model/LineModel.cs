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


        private LineModel currentLine = null;
        private Point _startPoint;
        public override ModelBase Create(Point startPoint)
        {
            _startPoint = startPoint;
            currentLine = new LineModel
            {
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = startPoint.X,
                Y2 = startPoint.Y
            };
            return currentLine;
        }

        public override void AdjustSize(Point mousePoint)
        {
            // 更新多邊形的頂點，以實現矩形的調整
            currentLine.X2 = mousePoint.X;
            currentLine.Y2 = mousePoint.Y;
            //用以繪製包圍的矩形
            currentLine.X = Math.Min(_startPoint.X, mousePoint.X) - 8;
            currentLine.Y = Math.Min(_startPoint.Y, mousePoint.Y) - 8;
            currentLine.Width = Math.Abs(mousePoint.X - _startPoint.X) + 16;
            currentLine.Height = Math.Abs(mousePoint.Y - _startPoint.Y) + 16;
        }
        public override void EndCreate()
        {
            currentLine = null;
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
    }
}
