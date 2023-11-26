using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        }
        

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

        public void LineMode()
        {
            State = States["Draw"];
            Cursor = Cursors.Cross;
            State.currentShape = new LineModel();
            ResetSelect();
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
            (States["Draw"] as DrawingState).newInstance = null;
            State = States["Select"];
            Cursor = Cursors.Arrow;
            ResetSelect();
        }

        public void EraseMode()
        {
            State = States["Erase"];
            Cursor customCursor = new Cursor(Application.GetResourceStream(new Uri("Resource/eraser.cur", UriKind.Relative)).Stream); 
            Cursor = customCursor;
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
        public void Save()
        {
            var rectangleList = canvasVM.Objects.OfType<RectangleModel>().ToList();
            var triangleList = canvasVM.Objects.OfType<TriangleModel>().ToList();
            var ellipseList = canvasVM.Objects.OfType<EllipseModel>().ToList();
            var polylineList = canvasVM.Objects.OfType<PolylineModel>().ToList();
            var lineList = canvasVM.Objects.OfType<LineModel>().ToList();

            SaveToJsonFile(rectangleList, "Rectangle.json");
            SaveToJsonFile(triangleList, "Triangle.json");
            SaveToJsonFile(ellipseList, "Ellipse.json");
            SaveToJsonFile(polylineList, "Polyline.json");
            SaveToJsonFile(lineList, "Line.json");
        }


        public void Load()
        {
            LoadJsonFile<RectangleModel>("Rectangle.json");
            LoadJsonFile<TriangleModel>("Triangle.json");
            LoadJsonFile<EllipseModel>("Ellipse.json");
            LoadJsonFile<PolylineModel>("Polyline.json");
            LoadJsonFile<LineModel>("Line.json");
        }

        private void SaveToJsonFile<T>(List<T> list, string fileName)
        {
            string jsonString = JsonConvert.SerializeObject(list);
            File.WriteAllText(fileName, jsonString);
        }


        //where T : ModelBase 約束確保 T 必須是 ModelBase 或其子類型
        private void LoadJsonFile<T>(string fileName) where T : ModelBase
        {
            string filePath = fileName;
            string jsonString = File.ReadAllText(filePath);
            var objects = JsonConvert.DeserializeObject<List<T>>(jsonString);
            foreach (var item in objects)
            {
                canvasVM.Objects.Add(item);
            }
        }
        

        
    }
}
