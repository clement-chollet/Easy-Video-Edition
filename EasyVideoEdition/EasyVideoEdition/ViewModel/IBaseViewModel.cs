using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.ViewModel
{
    /// <summary>
    /// Base interface needed to be implemented by all of the ViewModel to allow them to be show in the button list.
    /// </summary>
    interface IBaseViewModel
    {
        String name { get; }
    }


    
}
