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
        private ObservableCollection<TabItem> _items = new ObservableCollection<TabItem>();
        public ObservableCollection<TabItem> Items
        {
            get
            {
                return _items;
            }
        }

        private static List<BaseViewModel> _viewModelList = new List<BaseViewModel>();
        public static List<BaseViewModel> viewModelList
        {
            get
            {
                return _viewModelList;
            }
        }

        public MainViewModel()
        {
            _viewModelList.Add(new FileOpeningViewModel());
            _viewModelList.Add(new nullViewModel());
            _viewModelList.Add(new SubtitlesViewModel());
            _viewModelList.Add(new SaveFileViewModel());

            _items.Add(new TabItem { Header = "Ouvrir", Content = new FileOpeningViewModel() });
            _items.Add(new TabItem { Header = "Ajout Visuel", Content = new nullViewModel() });
            _items.Add(new TabItem { Header = "Ajout de sous titre", Content = new SubtitlesViewModel()});
            _items.Add(new TabItem { Header = "Enregistrer", Content = new SaveFileViewModel() });

            _items.Add(new TabItem { Header = "Ajout Visuel", Content = "", Visibility = System.Windows.Visibility.Hidden, Height = 50 });
        }
    }
}
