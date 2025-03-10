﻿using System;
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
            // 初始化 Objects
            Objects = new ObservableCollection<ModelBase>();
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

    }
}
