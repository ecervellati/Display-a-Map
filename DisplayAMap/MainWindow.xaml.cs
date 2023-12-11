using DisplayAMap.WPF.ViewModel;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using System.Composition;
using Esri.ArcGISRuntime.UI;

namespace DisplayAMap.WPF
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IServiceProvider ServiceProvider { get; set; }
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            ServiceProvider = serviceProvider;
            DataContext = ServiceProvider.GetService<MainViewModel>();
        }
    }
}
