using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using WpfPainter.Model;
using WpfPainter.MVVM;

namespace WpfPainter
{
    public  class CanvasViewModel: ObservableObject
    {

        public CanvasViewModel()
        {
            // 初始化 Objects，可以從資料庫、檔案或其他來源載入資料
            Objects = new ObservableCollection<ModelBase>
            {
                new RectangleModel { X = 70, Y = 100, Width = 40, Height = 50, FillColor = Brushes.Blue },
                new EllipseModel { X = 100, Y = 200, Width = 20, Height = 20, FillColor = Brushes.Red }
                // Add more objects as needed
            };
        }

        private ObservableCollection<ModelBase> objects;

        public ObservableCollection<ModelBase> Objects
        {
            get { return objects; }
            set
            {
                if (value != objects)
                {
                    objects = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Test()
        {
            Objects.Add(new EllipseModel { X = 100, Y = 200, Width = 20, Height = 20, FillColor = Brushes.Red });
        }

    }
}
