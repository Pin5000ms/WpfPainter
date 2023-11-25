using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using WpfPainter.Model;
using WpfPainter.MVVM;

namespace WpfPainter
{
    public class MainWindowViewModel : ObservableObject
    {
        CanvasView _canvasView;
        public MainWindowViewModel(CanvasView canvasView)
        {
            _canvasView = canvasView;
            SelectCommand = new RelayCommand(o => SelectMode());
            RectangleCommand = new RelayCommand(o=> RectangleMode());
            TriangleCommand = new RelayCommand(o => TriangleMode());
            EllipseCommand = new RelayCommand(o => EllipseMode());
            BrushCommand = new RelayCommand(o => BrushMode());
            EraseCommand = new RelayCommand(o => EraseMode());
        }

        public RelayCommand SelectCommand { get; set; }
        public RelayCommand RectangleCommand { get; set; }

        public RelayCommand TriangleCommand { get; set; }
        public RelayCommand EllipseCommand { get; set; }
        public RelayCommand BrushCommand { get; set; }

        public RelayCommand EraseCommand { get; set; }


        private void SelectMode()
        {
            _canvasView.SelectMode();
        }

        private void RectangleMode()
        {
            _canvasView.RectangleMode();
            _canvasView.SetPen(Brushes.Red, Brushes.Black, 2);
        }

        private void TriangleMode()
        {
            _canvasView.TriangleMode();
            _canvasView.SetPen(Brushes.Red, Brushes.Black, 2);
        }

        private void EllipseMode()
        {
            _canvasView.EllipseMode();
            _canvasView.SetPen(Brushes.Transparent, Brushes.Black, 2);
        }

        private void BrushMode()
        {
            _canvasView.PolylineMode();
            _canvasView.SetPen(Brushes.Transparent, Brushes.Black, 2);
        }

        private void EraseMode()
        {
            _canvasView.EraseMode();
        }
    }
}
