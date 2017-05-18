using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Interface that describe the structure of a file. Used for the storyboard and the class Video and Picture
    /// </summary>
    interface IFile
    {
        String filePath { get; set; }
        String fileName { get; set; }
        long fileSize { get; set; }
        String miniatPath { get; set; }
        double duration { get; set; }
    }
}
