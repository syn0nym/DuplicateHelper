using DuplicateHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateHelper.Model
{
    public class DuplicateObject : IDuplicateObject
    {
        public string FilePath { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string MD5Hash { get; set; }        
    }
}
