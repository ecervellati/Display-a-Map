using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Geometry;
using DisplayAMap.API.Results;
using System.Windows;
using System.Windows.Media;
using Esri.ArcGISRuntime.UI.Controls;
using DisplayAMap.WPF;
using DisplayAMap.WPF.Utilities;

namespace DisplayAMap.WPF.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        private Map _map;
        public Map Map
        {
            get { return _map; }
            set
            {
                _map = value;
                OnPropertyChanged(nameof(Map));
            }
        }
      
        private GraphicsOverlayCollection _graphicsOverlays;
        public GraphicsOverlayCollection GraphicsOverlays
        {
            get { return _graphicsOverlays; }
            set
            {
                _graphicsOverlays = value;
                OnPropertyChanged(nameof(GraphicsOverlays));
            }
        }

        public MapViewModel()
        {
            SetupMap();
        }

        private void SetupMap()
        {
            // Create a new map with a 'topographic vector' basemap.
            Map = new Map(BasemapStyle.ArcGISTopographic);
        }

        public void ProcessSelectedPhoto(Location photoLocation)
        {
            try
            {
                CreateGraphics(photoLocation.Longitude, photoLocation.Latitude);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid format");
                return;
            }
        }

        private void CreateGraphics(double longitude, double latitude)
        {
            // Create a new graphics overlay to contain a variety of graphics.
            var targetGraphicsOverlay = new GraphicsOverlay();

            // Add the overlay to a graphics overlay collection.
            GraphicsOverlayCollection overlays = new GraphicsOverlayCollection
            {
                targetGraphicsOverlay
            };

            // Set the view model's "GraphicsOverlays" property (will be consumed by the map view).
            this.GraphicsOverlays = overlays;

            // Create a point geometry.
            var targetPoint = new MapPoint(longitude, latitude, SpatialReferences.Wgs84);

            Viewpoint vPoint= new Viewpoint(targetPoint);
            // Create a symbol to define how the point is displayed.
            var pointSymbol = new SimpleMarkerSymbol
            {
                Style = SimpleMarkerSymbolStyle.Circle,
                Color = System.Drawing.Color.Orange,
                Size = 10.0
            };

            // Add an outline to the symbol.
            pointSymbol.Outline = new SimpleLineSymbol
            {
                Style = SimpleLineSymbolStyle.Solid,
                Color = System.Drawing.Color.Blue,
                Width = 2.0
            };

            // Create a point graphic with the geometry and symbol.
            var pointGraphic = new Graphic(targetPoint, pointSymbol);

            // Add the point graphic to graphics overlay.
            targetGraphicsOverlay.Graphics.Add(pointGraphic);

        }
    }
}
