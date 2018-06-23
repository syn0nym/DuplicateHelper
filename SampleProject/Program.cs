using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuplicateHelper.Interface;
using DuplicateHelper.Logic;
using DuplicateHelper.Model;

namespace SampleProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            string samplePath = @"C:\Users\Hendrik\Documents\Visual Studio 2015\Projects\DuplicateHelper\DuplicateHelper.UnitTest\TestDirectory";
            IFileDuplicateCheck duplicateChecker = new FileDuplicateCheck();          
            List<IDuplicate> duplicates = duplicateChecker.LookupCandidate(samplePath, FileCompareMode.FileSizeFileName).ToList();
            Console.ReadKey();
        }
    }
}
