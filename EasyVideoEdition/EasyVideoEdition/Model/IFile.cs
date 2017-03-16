using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.Model
{
    interface IFile
    {
        String filePath { get; set; }
        String fileName { get; set; }
        long fileSize { get; set; }
        String miniatPath { get; set; }
        int duration { get; set; }
    }
}
