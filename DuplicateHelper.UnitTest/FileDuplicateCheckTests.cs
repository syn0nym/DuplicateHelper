using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DuplicateHelper.Interface;
using DuplicateHelper.Logic;
using System.Collections.Generic;
using DuplicateHelper.Model;

namespace DuplicateHelper.UnitTest
{
    [TestClass]
    public class FileDuplicateCheckTests
    {
        [TestMethod]
        public void LookupCandidate_ModeFileSize_Returns5Duplicates()
        {
            //ARRANGE
            IFileDuplicateCheck fileDuplicateChecker = new FileDuplicateCheck();
            //ACT
            IEnumerable<IDuplicate> result = fileDuplicateChecker.LookupCandidate(@"TestDirectory",Model.FileCompareMode.FileSize);
            //ASSERT
            Assert.IsTrue(result.Count() == 5);            
        }

        [TestMethod]
        public void LookupCandidate_ModeFileSizeName_Returns4Duplicates()
        {
            //ARRANGE
            IFileDuplicateCheck fileDuplicateChecker = new FileDuplicateCheck();
            //ACT
            IEnumerable<IDuplicate> result = fileDuplicateChecker.LookupCandidate(@"TestDirectory", Model.FileCompareMode.FileSizeFileName);
            //ASSERT
            Assert.IsTrue(result.Count() == 4);
        }
        [TestMethod]
        public void LookupCandidate_SingleFile_NeverReturnsAsDuplicate()
        {
            //ARRANGE
            IFileDuplicateCheck fileDuplicateChecker = new FileDuplicateCheck();
            //ACT
            IEnumerable<IDuplicate> result = fileDuplicateChecker.LookupCandidate(@"TestDirectory", Model.FileCompareMode.FileSizeFileName);
            //ASSERT
            Assert.IsTrue(!result.Any(x => x.DuplicateObjects.Any(y => y.FileName.Equals("SINGLE_FILE.txt"))));
        }
    }
}
