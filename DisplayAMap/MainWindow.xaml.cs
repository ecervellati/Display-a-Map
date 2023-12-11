using DisplayAMap.API.Results;
using DisplayAMap.API;
using DisplayAMap.API.Services;
using DisplayAMap.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using Microsoft.Extensions.DependencyInjection;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI.Controls;
using DisplayAMap.WPF.Utilities;
using System.Composition;
using Esri.ArcGISRuntime.UI;
using System.Reflection;

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
