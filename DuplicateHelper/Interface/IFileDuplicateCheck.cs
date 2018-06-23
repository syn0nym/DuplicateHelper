using DuplicateHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateHelper.Interface
{
    public interface IFileDuplicateCheck
    {
        IEnumerable<IDuplicate> LookupCandidate(string path);
        IEnumerable<IDuplicate> LookupCandidate(string path, FileCompareMode mode);
        IEnumerable<IDuplicate> CheckCandidates(IEnumerable<IDuplicate> candidates);
        IEnumerable<IDuplicate> CheckMD5(IEnumerable<IDuplicate> candidates);
    }
}