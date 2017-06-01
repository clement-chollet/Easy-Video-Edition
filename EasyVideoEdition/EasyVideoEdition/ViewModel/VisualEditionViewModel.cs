﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.ViewModel
{
    /// <summary>
    /// ViewModel corresponding to the VisualEdition View
    /// </summary>
    class VisualEditionViewModel
    {
        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static VisualEditionViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        #region Attributes
        private static VisualEditionViewModel singleton = new VisualEditionViewModel();
        #endregion
    }
}
