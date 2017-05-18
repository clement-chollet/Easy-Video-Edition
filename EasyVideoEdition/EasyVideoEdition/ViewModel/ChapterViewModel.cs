using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.ViewModel
{
    class ChapterViewModel
    {
        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static ChapterViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        #region Attributes
        private static ChapterViewModel singleton = new ChapterViewModel();
        #endregion
    }
}
