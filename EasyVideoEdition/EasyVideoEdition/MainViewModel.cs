using System.Windows;
using System.Windows.Controls;
using EasyVideoEdition.ViewModel;
using System.Collections.ObjectModel;

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
            SaveFileViewModel SaveFileViewModel = SaveFileViewModel.INSTANCE;
            SubtitlesViewModel SubtitlesViewModel = SubtitlesViewModel.INSTANCE;
            NullViewModel NullViewModel = NullViewModel.INSTANCE;
            VisualAddingViewModel VisualAddingViewModel = VisualAddingViewModel.INSTANCE;

            _items.Add(new TabItem { Header = "Ouvrir", Content = FileOpeningViewModel });
            _items.Add(new TabItem { Header = "Ajout Visuel", Content = VisualAddingViewModel });
            _items.Add(new TabItem { Header = "Ajout de sous titre", Content =  SubtitlesViewModel });
            _items.Add(new TabItem { Header = "Enregistrer", Content = SaveFileViewModel });

            _items.Add(new TabItem { Header = "Ajout Visuel", Content = "", Visibility = Visibility.Hidden, Height = 50 });
        }
    }
}
