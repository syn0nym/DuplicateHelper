using DuplicateHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuplicateHelper.Model;
using System.IO;
using System.Security.Cryptography;

namespace DuplicateHelper.Logic
{
    public class FileDuplicateCheck : IFileDuplicateCheck
    {
        #region Methods
        public IEnumerable<IDuplicate> LookupCandidate(string path)
        {
            return LookupCandidate(path, FileCompareMode.FileSizeFileName);
        }

        public IEnumerable<IDuplicate> LookupCandidate(string path, FileCompareMode mode)
        {
            List<Duplicate> duplicates = new List<Duplicate>();

            try
            {
                List<string> dirs = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories).ToList<string>();

                foreach (string filePath in dirs)
                {
                    FileInfo fileInfo = new FileInfo(filePath);

                    Duplicate duplicate = new Duplicate();
                    duplicate.DuplicateObjects = new List<DuplicateObject>() {
                        new DuplicateObject{
                            FilePath = filePath,
                            FileSize = fileInfo.Length,
                            FileName = fileInfo.Name,
                            MD5Hash = string.Empty
                        }
                    };
                    duplicates.Add(duplicate);
                }
                duplicates = checkByMode(duplicates, mode.Equals(FileCompareMode.FileSize) ? FileCompareMode.FileSize : FileCompareMode.FileSizeFileName);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

            return CheckCandidates(duplicates);
        }

        public IEnumerable<IDuplicate> CheckCandidates(IEnumerable<IDuplicate> candidates)
        {
            candidates = CheckMD5(candidates);
            return candidates;
        }

        public IEnumerable<IDuplicate> CheckMD5(IEnumerable<IDuplicate> candidates)
        {

            List<Duplicate> tempList = new List<Duplicate>();
            foreach (Duplicate item in candidates)
            {
                foreach (DuplicateObject duplicateObject in item.DuplicateObjects)
                {
                    duplicateObject.MD5Hash = calculateMD5(duplicateObject);
                }

                var filteredDuplicatesGroupedByMD5 = item.DuplicateObjects
                        .GroupBy(x => x.MD5Hash)
                        .Where(grp => grp.Count() > 1);

                foreach (var MD5Group in filteredDuplicatesGroupedByMD5.Select(grp => grp.ToList()))
                {
                    tempList.Add(new Duplicate() { DuplicateObjects = MD5Group });
                }
            }

            return tempList;
        }
        #endregion
        #region Helper Methods
        private List<Duplicate> checkByMode(List<Duplicate> duplicates, FileCompareMode fileCompareMode)
        {
            List<Duplicate> filteredDuplicates = new List<Duplicate>();

            switch (fileCompareMode)
            {
                case FileCompareMode.FileSize:
                    var filteredDuplicatesGroupedBySize = duplicates
                        .GroupBy(x => x.DuplicateObjects.First().FileSize)
                        .Where(grp => grp.Count() > 1);

                    foreach (var group in filteredDuplicatesGroupedBySize)
                    {
                        Duplicate duplicate = new Duplicate();
                        List<DuplicateObject> duplicateObjects = new List<DuplicateObject>();

                        foreach (var item in group)
                        {
                            duplicateObjects.Add(new DuplicateObject()
                            {
                                FileName = item.DuplicateObjects.First().FileName,
                                FilePath = item.DuplicateObjects.First().FilePath,
                                FileSize = item.DuplicateObjects.First().FileSize,
                                MD5Hash = item.DuplicateObjects.First().MD5Hash
                            });
                        }
                        duplicate.DuplicateObjects = duplicateObjects;
                        filteredDuplicates.Add(duplicate);
                    }
                    break;
                case FileCompareMode.FileSizeFileName:
                    var filteredDuplicatesGroupedBySizeName = duplicates
                        .GroupBy(x => new { x.DuplicateObjects.First().FileName, x.DuplicateObjects.First().FileSize })
                        .Where(grp => grp.Count() > 1);
                    foreach (var group in filteredDuplicatesGroupedBySizeName)
                    {
                        Duplicate duplicate = new Duplicate();
                        List<DuplicateObject> duplicateObjects = new List<DuplicateObject>();
                        foreach (var item in group)
                        {
                            duplicateObjects.Add(new DuplicateObject()
                            {
                                FileName = item.DuplicateObjects.First().FileName,
                                FilePath = item.DuplicateObjects.First().FilePath,
                                FileSize = item.DuplicateObjects.First().FileSize,
                                MD5Hash = item.DuplicateObjects.First().MD5Hash
                            });
                        }
                        duplicate.DuplicateObjects = duplicateObjects;
                        filteredDuplicates.Add(duplicate);
                    }
                    break;
            }
            return filteredDuplicates;
        }
        private string calculateMD5(DuplicateObject duplicateObject)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(duplicateObject.FilePath))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        #endregion
    }
}
