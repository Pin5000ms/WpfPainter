using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using WpfPainter.Model;

namespace WpfPainter
{
    /// <summary>
    /// CanvasView.xaml 的互動邏輯
    /// </summary>
    /// 
    public partial class CanvasView : UserControl
    {
        CanvasViewModel canvasVM = new CanvasViewModel();
        public CanvasView()
        {
            InitializeComponent();
            this.DataContext = canvasVM;
            canvasVM.Objects.Add(new PolylineModel() { Stroke = Brushes.Red, StrokeThickness = 2, PolylinePoints = new PointCollection() { new Point(100, 100) } });
        }
        private ModelBase currentShape = new PolylineModel();
        private void drawCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point startPoint = e.GetPosition(drawCanvas);
                canvasVM.Objects.Add(currentShape.Create(startPoint));
            }
        }

        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentShape != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point mousePosition = e.GetPosition(drawCanvas);
                    currentShape.AdjustSize(mousePosition);    
                }
            }
        }

        private void drawCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            currentShape.EndCreate();
            //currentShape = null;
        }
    }
}
