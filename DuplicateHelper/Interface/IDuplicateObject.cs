using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateHelper.Interface
{    public interface IDuplicateObject
    {
        string FilePath { get; set; }

        string FileName { get; set; }

        long FileSize { get; set; }

        string MD5Hash { get; set; }
    }
}
