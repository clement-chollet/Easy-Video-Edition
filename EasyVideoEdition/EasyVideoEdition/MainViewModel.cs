using System.Windows;
using System.Windows.Controls;
using EasyVideoEdition.ViewModel;
using System.Collections.ObjectModel;
using System;
using System.Windows.Media;

namespace EasyVideoEdition
{
    /// <summary>
    /// Main View Model, this one control all of the other view model, and allow to switch between them by the use of a button.
    /// SINGLETON
    /// </summary>
    class MainViewModel : ObjectBase
    {
        #region Attributes
        private static MainViewModel singleton = new MainViewModel();
        private ObservableCollection<TabItem> _items = new ObservableCollection<TabItem>();
        private int _actualViewIndex = 0;
        #endregion

        #region Get/Set
        /// <summary>
        /// Get the list of Items in the tab
        /// </summary>
        public ObservableCollection<TabItem> Items
        {
            get
            {
                return _items;
            }
        }

        /// <summary>
        /// Index of the viewModel use in the viewModelList. Allow switch between tab programaticaly
        /// </summary>
        public int actualViewIndex
        {
            get
            {
                return _actualViewIndex;
            }

            set
            {
                _actualViewIndex = value;
                RaisePropertyChanged("actualViewIndex");
            }
        }

        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static MainViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        }
        #endregion

        /// <summary>
        /// Main View Model, manage all the other view model, and allow each viewModel to know eatch other.
        /// This class also manage the list for the tabItem.
        /// </summary>
        private MainViewModel()
        {
            OpeningViewModel FileOpeningViewModel = OpeningViewModel.INSTANCE;
            VisualAddingViewModel VisualAddingViewModel = VisualAddingViewModel.INSTANCE;
            VisualEditionViewModel VisualEditionViewModel = VisualEditionViewModel.INSTANCE;
            AudioAddingViewModel AudioAddingViewModel = AudioAddingViewModel.INSTANCE;
            AudioEditionViewModel AudioEditionViewModel = AudioEditionViewModel.INSTANCE;
            ChapterViewModel ChapterViewModel = ChapterViewModel.INSTANCE;
            SubtitlesViewModel SubtitlesViewModel = SubtitlesViewModel.INSTANCE;
            SaveFileViewModel SaveFileViewModel = SaveFileViewModel.INSTANCE;
            NullViewModel NullViewModel = NullViewModel.INSTANCE;
            
            //"tabItemTemplate"
            var tabDictionary = new ResourceDictionary();
            tabDictionary.Source = new Uri("Dictionnary/Tabs/Tabs.xaml", UriKind.RelativeOrAbsolute);


            _items.Add(new TabItem { Header = "Début", Content = FileOpeningViewModel, Template = tabDictionary["tabItemTemplate"] as ControlTemplate, Foreground = Brushes.White} );
            _items.Add(new TabItem { Header = "Ajout visuel", Content = VisualAddingViewModel, Template = tabDictionary["tabItemTemplate"] as ControlTemplate, Foreground = Brushes.White });
            _items.Add(new TabItem { Header = "Edition visuel", Content = VisualEditionViewModel, Template = tabDictionary["tabItemTemplate"] as ControlTemplate, Foreground = Brushes.White });
            _items.Add(new TabItem { Header = "Ajout audio", Content = AudioAddingViewModel, Template = tabDictionary["tabItemTemplate"] as ControlTemplate, Foreground = Brushes.White });
            _items.Add(new TabItem { Header = "Edition audio", Content = AudioEditionViewModel, Template = tabDictionary["tabItemTemplate"] as ControlTemplate, Foreground = Brushes.White });
            _items.Add(new TabItem { Header = "Ajout sous-titres", Content = SubtitlesViewModel, Template = tabDictionary["tabItemTemplate"] as ControlTemplate, Foreground = Brushes.White });
            _items.Add(new TabItem { Header = "Ajout chapitres", Content = ChapterViewModel, Template = tabDictionary["tabItemTemplate"] as ControlTemplate, Foreground = Brushes.White });
            _items.Add(new TabItem { Header = "Enregistrer", Content = SaveFileViewModel, Template = tabDictionary["tabItemTemplate"] as ControlTemplate, Foreground = Brushes.White });

        }
    }
}
