using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.ViewModel
{
    class NullViewModel : ObjectBase, IBaseViewModel
    {
        #region Attributes
        private static NullViewModel singleton = new NullViewModel();
        public String name
        {
            get
            {
                return "NULL VIEW MODEL";
            }
        }

        #endregion

        #region Get/Set
        public static NullViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        } 
        #endregion

        private NullViewModel()
        {

        }
    }
}
