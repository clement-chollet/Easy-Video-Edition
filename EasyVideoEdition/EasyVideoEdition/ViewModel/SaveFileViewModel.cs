using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.ViewModel
{
    class SaveFileViewModel : ObjectBase, IBaseViewModel
    {
        #region Attributes
        private static SaveFileViewModel singleton = new SaveFileViewModel();

        public String name
        {
            get
            {
                return "Save File/Project";
            }
        }
        #endregion

        #region Get/Set
        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static SaveFileViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        }
        #endregion

        private SaveFileViewModel()
        {

        }
    }
}
