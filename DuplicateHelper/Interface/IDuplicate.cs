using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateHelper.Interface
{
    public interface IDuplicate
    {
        IEnumerable<IDuplicateObject> DuplicateObjects { get; set; }        
    }
}
