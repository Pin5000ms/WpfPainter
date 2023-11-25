using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
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
        StateBase State;
        CanvasViewModel canvasVM = new CanvasViewModel();

        public Dictionary<string ,StateBase> States = new Dictionary<string, StateBase>();
        public CanvasView()
        {
            InitializeComponent();
            this.DataContext = canvasVM;
            States.Add("Select", new SelectedState(canvasVM));
            States.Add("Draw", new DrawingState(canvasVM));
            States.Add("Erase", new EraseState(canvasVM));
            State = States["Select"];
        }

        private void drawCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point startPoint = e.GetPosition(drawCanvas);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                State.MouseDown(startPoint);
            }
            //if(State == States["Select"])
            //{
            //    AddResizeGrip(State.currentShape);
            //}
        }
        //private Rectangle resizeGrip;
        //private void AddResizeGrip(ModelBase shape)
        //{
        //    resizeGrip = new Rectangle
        //    {
        //        Width = shape.Width,
        //        Height = shape.Height,
        //        Fill = Brushes.Transparent,
        //        Stroke = Brushes.Red,
        //        StrokeThickness = 1,
        //        StrokeDashArray = new DoubleCollection() { 2, 2 },
        //    };

        //    Canvas.SetLeft(resizeGrip, shape.X);
        //    Canvas.SetTop(resizeGrip, shape.Y);

        //    resizeGrip.MouseLeftButtonDown += ResizeGrip_MouseLeftButtonDown;

        //    drawCanvas.Children.Add(resizeGrip);
        //}

        //private void ResizeGrip_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    // Handle the resizing logic here
        //    // For example, you can change the size of the selected shape based on the position of the resize grip
        //    // and update the position of the resize grip accordingly
        //}

        //private void ResetResizeGrip()
        //{
        //    if (resizeGrip != null)
        //    {
        //        drawCanvas.Children.Remove(resizeGrip);
        //        resizeGrip = null;
        //    }
        //}

        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(drawCanvas);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                State.MouseMove(mousePosition);
            }
        }

        private void drawCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            State.MouseUp();
        }


        public void SetProperty(Brush fillColor, SolidColorBrush stroke, double thickness)
        {
            States["Draw"].SetProperty(fillColor, stroke, thickness);
            States["Select"].SetProperty(fillColor, stroke, thickness);
        }

        public void RectangleMode()
        {
            State = States["Draw"];
            Cursor = Cursors.Cross;
            State.currentShape = new RectangleModel();
            ResetSelect();
        }

        public void TriangleMode()
        {
            State = States["Draw"];
            Cursor = Cursors.Cross;
            State.currentShape = new TriangleModel();
            ResetSelect();
        }

        public void EllipseMode()
        {
            State = States["Draw"];
            Cursor = Cursors.Cross;
            State.currentShape = new EllipseModel();
            ResetSelect();
        }

        public void PolylineMode()
        {
            State = States["Draw"];
            Cursor = Cursors.Pen;
            State.currentShape = new PolylineModel();
            ResetSelect();
        }

        public void SelectMode()
        {
            State = States["Select"];
            Cursor = Cursors.Arrow;
            ResetSelect();
        }

        public void EraseMode()
        {
            State = States["Erase"];
            ResetSelect();
        }


        public void ResetSelect()
        {
            States["Select"].currentShape = null;
            foreach (var item in canvasVM.Objects)
            {
                item.IsSelected = false;
            }
        }
    }
}
