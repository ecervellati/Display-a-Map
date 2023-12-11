using DisplayAMap.API.Services;
using DisplayAMap.WPF.ViewModel;
using DisplayAMap.WPF;
using System;
using System.Configuration;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using DisplayAMap.API;
using System.Text;

namespace DisplayAMap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider FlickrService { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public App()
        {
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = ConfigurationManager.AppSettings.Get("EsriApiKey");
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            string apiKey = ConfigurationManager.AppSettings.Get("FlickrApiKey");
            serviceCollection.AddTransient(typeof(MainWindow));
            serviceCollection.AddSingleton<IFlickrService>(new FlickrService(new FlickrHttpClientFactory(apiKey)));
            serviceCollection.AddTransient<MainViewModel>(serviceProvider =>
            {
                // Resolve the IFlickrService from the container
                var flickrService = serviceProvider.GetRequiredService<IFlickrService>();

                // Create MainViewModel and inject IFlickrService
                return new MainViewModel(flickrService);
            });
        }
    }
}
