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
            LineCommand = new RelayCommand(o => LineMode());
            EllipseCommand = new RelayCommand(o => EllipseMode());
            BrushCommand = new RelayCommand(o => BrushMode());
            EraseCommand = new RelayCommand(o => EraseMode());
            SaveCommand = new RelayCommand(o => Save());
            Stroke = new SolidColorBrush(Color.FromRgb(stroke_R, stroke_G, stroke_B));
            FillColor = new SolidColorBrush(Color.FromArgb(255, fill_R, fill_G, fill_B));
            Load();
        }

        public RelayCommand SelectCommand { get; set; }
        public RelayCommand RectangleCommand { get; set; }
        public RelayCommand TriangleCommand { get; set; }
        public RelayCommand LineCommand { get; set; }
        public RelayCommand EllipseCommand { get; set; }
        public RelayCommand BrushCommand { get; set; }
        public RelayCommand EraseCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }


        #region FillColor
        private Brush c;
        public Brush FillColor
        {
            get { return c; }
            set
            {
                c = value;
                OnPropertyChanged();
                _canvasView.SetProperty(FillColor, Stroke, strokeThickness);
            }
        }


        private byte fill_R;
        public byte Fill_R
        {
            get { return fill_R; }
            set
            {
                fill_R = value;
                OnPropertyChanged();
                if (nofill)
                {
                    FillColor = new SolidColorBrush(Color.FromArgb(0, fill_R, fill_G, fill_B));
                }
                else
                {
                    FillColor = new SolidColorBrush(Color.FromArgb(255, fill_R, fill_G, fill_B));
                }
            }
        }


        private byte fill_G;
        public byte Fill_G
        {
            get { return fill_G; }
            set
            {
                fill_G = value;
                OnPropertyChanged();
                if (nofill)
                {
                    FillColor = new SolidColorBrush(Color.FromArgb(0, fill_R, fill_G, fill_B));
                }
                else
                {
                    FillColor = new SolidColorBrush(Color.FromArgb(255, fill_R, fill_G, fill_B));
                }
            }
        }

        private byte fill_B;
        public byte Fill_B
        {
            get { return fill_B; }
            set
            {
                fill_B = value;
                OnPropertyChanged();
                if (nofill)
                {
                    FillColor = new SolidColorBrush(Color.FromArgb(0, fill_R, fill_G, fill_B));
                }
                else
                {
                    FillColor = new SolidColorBrush(Color.FromArgb(255, fill_R, fill_G, fill_B));
                }
            }
        }

        private bool nofill;
        public bool NoFill
        {
            get { return nofill; }
            set
            {
                nofill = value;
                OnPropertyChanged();
                if (nofill)
                {
                    FillColor = new SolidColorBrush(Color.FromArgb(0, fill_R, fill_G, fill_B));
                }
                else
                {
                    FillColor = new SolidColorBrush(Color.FromArgb(255, fill_R, fill_G, fill_B));
                }

            }
        }
        #endregion


        #region Stroke
        private int strokeThickness = 1;
        public int StrokeThickness
        {
            get { return strokeThickness; }
            set
            {
                strokeThickness = value;
                _canvasView.SetProperty(Brushes.Transparent, Stroke, strokeThickness);
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
                _canvasView.SetProperty(FillColor, Stroke, strokeThickness);
                OnPropertyChanged();
            }
        }

        private byte stroke_R;
        public byte Stroke_R
        {
            get { return stroke_R; }
            set
            {
                stroke_R = value;
                OnPropertyChanged();
                Stroke = new SolidColorBrush(Color.FromRgb(stroke_R, stroke_G, stroke_B));
            }
        }

        private byte stroke_G;
        public byte Stroke_G
        {
            get { return stroke_G; }
            set
            {
                stroke_G = value;
                OnPropertyChanged();
                Stroke = new SolidColorBrush(Color.FromRgb(stroke_R, stroke_G, stroke_B));
            }
        }

        private byte stroke_B;
        public byte Stroke_B
        {
            get { return stroke_B; }
            set
            {
                stroke_B = value;
                OnPropertyChanged();
                Stroke = new SolidColorBrush(Color.FromRgb(stroke_R, stroke_G, stroke_B));
            }
        }
        #endregion


        private void SelectMode()
        {
            _canvasView.SelectMode();
        }

        private void RectangleMode()
        {
            _canvasView.RectangleMode();
        }

        private void TriangleMode()
        {
            _canvasView.TriangleMode();
        }

        private void LineMode()
        {
            _canvasView.LineMode();
        }

        private void EllipseMode()
        {
            _canvasView.EllipseMode();
        }

        private void BrushMode()
        {
            _canvasView.PolylineMode();
        }

        private void EraseMode()
        {
            _canvasView.EraseMode();
        }

        private void Save()
        {
            _canvasView.Save();
        }

        private void Load()
        {
            _canvasView.Load();
        }
    }
}
