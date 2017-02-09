using EasyVideoEdition.Model;
using EasyVideoEdition.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EasyVideoEdition.ViewModel;
using System.Collections.ObjectModel;

namespace EasyVideoEdition
{
    /// <summary>
    /// Main View Model, this one control all of the other view model, and allow to switch between them by the use of a button.
    /// </summary>
    class MainViewModel : ObjectBase
    {
        #region Attributes
        private ObservableCollection<TabItem> _items = new ObservableCollection<TabItem>();
        private static List<BaseViewModel> _viewModelList = new List<BaseViewModel>();
        #endregion

        #region Get/Set
        public ObservableCollection<TabItem> Items
        {
            get
            {
                return _items;
            }
        }
        public static List<BaseViewModel> viewModelList
        {
            get
            {
                return _viewModelList;
            }
        }
        #endregion

        /// <summary>
        /// Main View Model, manage all the other view model, and allow each viewModel to now eatch other.
        /// This class also manage the list for the tabItem.
        /// </summary>
        public MainViewModel()
        {
            _viewModelList.Add(new FileOpeningViewModel());
            _viewModelList.Add(new nullViewModel());
            _viewModelList.Add(new SubtitlesViewModel());
            _viewModelList.Add(new SaveFileViewModel());


            _items.Add(new TabItem { Header = "Ouvrir", Content = _viewModelList.ElementAt(0) });
            _items.Add(new TabItem { Header = "Ajout Visuel", Content = _viewModelList.ElementAt(1) });
            _items.Add(new TabItem { Header = "Ajout de sous titre", Content = _viewModelList.ElementAt(2) });
            _items.Add(new TabItem { Header = "Enregistrer", Content = _viewModelList.ElementAt(3) });

            _items.Add(new TabItem { Header = "Ajout Visuel", Content = "", Visibility = System.Windows.Visibility.Hidden, Height = 50 });
        }
    }
}
