using DuplicateHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateHelper.Model
{
    public class Duplicate : IDuplicate
    {
       public  IEnumerable<IDuplicateObject> DuplicateObjects { get; set; }       
    }
}
