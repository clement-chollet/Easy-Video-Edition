﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.ViewModel
{
    class AudioEditionViewModel
    {
        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static AudioEditionViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        #region Attributes
        private static AudioEditionViewModel singleton = new AudioEditionViewModel();
        #endregion
    }
}
