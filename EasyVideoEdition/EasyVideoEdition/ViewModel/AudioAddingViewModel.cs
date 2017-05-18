using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.ViewModel
{
    class AudioAddingViewModel
    {
        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static AudioAddingViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        #region Attributes
        private static AudioAddingViewModel singleton = new AudioAddingViewModel();
        #endregion
    }
}
