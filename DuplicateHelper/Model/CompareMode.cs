using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateHelper.Model
{
    /// <summary>
    /// Compare modes for different types of duplicate checks
    /// </summary> 
    public enum FileCompareMode
    {
        Default =  0000,
        FileSize = 0001,
        FileSizeFileName = 0002,        
    }
}
