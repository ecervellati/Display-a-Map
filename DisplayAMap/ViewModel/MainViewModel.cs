using DisplayAMap.API.Results;
using DisplayAMap.API.Services;
using DisplayAMap.WPF.Commands;
using DisplayAMap.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Windows.ApplicationModel.VoiceCommands;

namespace DisplayAMap.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Properties for the button associated with each individual image
        /// </summary>
        private ICommand _selectImageCommand;
        public ICommand SelectImageCommand
        {
            get
            {
                return _selectImageCommand ?? (_selectImageCommand = new RelayCommand(GetPhotoLocation));
            }
            set
            {

            }
        }

        /// <summary>
        /// Properties for rendering images in the main window
        /// </summary>
        private ObservableCollection<Photo> _imageUrls;
        public ObservableCollection<Photo> ImageUrls
        {
            get { return _imageUrls; }
            set
            {
                if (_imageUrls != value)
                {
                    _imageUrls = value;
                    OnPropertyChanged(nameof(ImageUrls));
                }
            }
        }

        /// <summary>
        /// Properties for the text box component
        /// </summary>
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        /// <summary>
        /// FlickrService to consume the APIs
        /// </summary>
        public IFlickrService FlickrService { get; set; }
        /// <summary>
        /// Searh button property
        /// </summary>
        public ICommand ButtonCommand { get; set; }
        public ICommand MouseEnterCommand { get; }
        public ICommand MouseLeaveCommand { get; }
        public List<Photo> PhotoList { get; set; }
        public MapViewModel MapViewModel { get; set; }
        public Grid ActualGrid { get; set; }

        public MainViewModel(IFlickrService _flickrService)
        {
            FlickrService = _flickrService;
            ButtonCommand = new RelayCommand(GetPhotos, CanSearch);
            SelectImageCommand = new RelayCommand(GetPhotoLocation);
            ImageUrls = new ObservableCollection<Photo>();
            MapViewModel = new MapViewModel();
            MouseEnterCommand = new RelayCommand(ExecuteMouseEnter);
            MouseLeaveCommand = new RelayCommand(ExecuteMouseLeave);
        }

        private bool CanSearch(object parameter)
        {
            return !string.IsNullOrEmpty(SearchText);
        }

        /// <summary>
        /// Method to get photos by tag
        /// </summary>
        private async void GetPhotos(object parameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchText))
                {
                    List<Photo> photoColl = await FlickrService.GetPhotoByTag(SearchText);
                    PhotoList = photoColl;
                    BuildPhotoUrl(photoColl);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Method for getting the location of the single photo the user clicked on
        /// </summary>
        private async void GetPhotoLocation(object selectedImage)
        {
            try
            {
                if (selectedImage != null && selectedImage is Photo)
                {
                    Photo photoGeoInfo = await FlickrService.GetGeoInformationPhoto((Photo)selectedImage);
                    if (photoGeoInfo != null && photoGeoInfo.Location != null)
                    {
                        MapViewModel.ProcessSelectedPhoto(photoGeoInfo.Location);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Method to build the static url of the photos
        /// </summary>
        /// <param name="photoList"></param>
        private void BuildPhotoUrl(List<Photo> photoList)
        {
            if (ImageUrls != null)
                ImageUrls.Clear();

            foreach (Photo photo in photoList)
            {
                string url = "https://live.staticflickr.com/";

                if (photo != null && photo.Server != null &&
                    photo.Id != null && photo.Secret != null)
                {
                    photo.Url = url + $"{photo.Server}/{photo.Id}_{photo.Secret}_w.jpg";
                    ImageUrls.Add(photo);
                }
            }
        }

        /// <summary>
        /// Method to show the "select image" button
        /// </summary>
        private void ExecuteMouseEnter(object gridElement)
        {
            ActualGrid = gridElement != null ? (Grid)gridElement : null;
            if (ActualGrid != null)
            {
                var button = ActualGrid.Children.OfType<Button>().FirstOrDefault();
                if (button != null)
                {
                    button.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Method to hide the "select image" button
        /// </summary>
        private void ExecuteMouseLeave(object gridElement)
        {
            ActualGrid = gridElement != null ? (Grid)gridElement : null;
            if (ActualGrid != null)
            {
                var button = ActualGrid.Children.OfType<Button>().FirstOrDefault();
                if (button != null)
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
