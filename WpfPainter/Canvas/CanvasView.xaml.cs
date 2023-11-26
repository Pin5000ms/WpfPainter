using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

        public Dictionary<string, StateBase> States = new Dictionary<string, StateBase>();
        public CanvasView()
        {
            InitializeComponent();
            this.DataContext = canvasVM;
            States.Add("Select", new SelectedState(canvasVM));
            States.Add("Draw", new DrawingState(canvasVM));
            States.Add("Erase", new EraseState(canvasVM));
            State = States["Select"];
        }


        private Stack<StateBase> undoStack = new Stack<StateBase>();//紀錄有做出變更的State
        private Stack<StateBase> redoStack = new Stack<StateBase>();
        private void drawCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point startPoint = e.GetPosition(drawCanvas);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                bool addRedo = State.MouseDown(startPoint);//如果當前State有做出變更，addRedo=true
                if (addRedo)
                {
                    undoStack.Push(State);
                }
            }
        }


        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(drawCanvas);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                bool addRedo = State.MouseMove(mousePosition);//如果當前State有做出變更，addRedo=true
                if (addRedo)
                {
                    undoStack.Push(State);
                }
            }
        }

        private void drawCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool addRedo = State.MouseUp();
            if (addRedo)
            {
                undoStack.Push(State);
            }
        }

        public void Undo()
        {
            if (undoStack.Count != 0)
            {
                var lastState = undoStack.Pop();//拿出上一個有做出變更的State
                lastState.Undo();
                redoStack.Push(lastState);
            }
        }

        public void Redo()
        {
            if (redoStack.Count != 0)
            {
                var redoState = redoStack.Pop();
                redoState.Redo();
            }
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
