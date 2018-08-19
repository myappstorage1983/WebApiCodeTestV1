using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myApiTreeView.Models;

namespace myApiTreeView.API.Data
{
    public class DataRepo : IDataRepo
    {
        private readonly DataContext _context;
        public DataRepo(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Folder> GetFolderById(int? folderId)
        {
            return await _context.folders.AsNoTracking().Include(y => y.SubFolders)
                           .Where(x => x.FolderId == folderId).FirstOrDefaultAsync();

        }

        //Recursive function to fetch all testcases with in the folder and subfolder.
        //Recursive function to fetch all subfolders with in the folder.
        public List<Folder> GetAllFolders(List<Folder> folders, ref List<TestCase> testcases)
        {
            int i = 0;
            List<Folder> foldersList = new List<Folder>();

            if (folders.Count > 0)
            {
                foldersList.AddRange(folders);
            }

            foreach (Folder x in folders)
            {
                Folder folder = _context.folders.AsNoTracking().Include(y => y.SubFolders).Include(t => t.TestCases)
                                .Where(f => f.FolderId == x.FolderId)
                                .Select(f => new Folder { FolderId = f.FolderId, Name = f.Name, ParentFolderId = f.ParentFolderId, SubFolders = f.SubFolders, TestCases = f.TestCases }).First();
                if (folder.SubFolders == null)
                {
                    i++;
                    continue;
                }

                if (folder.TestCases.Count > 0)
                {
                    testcases.AddRange(folder.TestCases);
                }
                List<Folder> subfolder = folder.SubFolders.ToList();
                folder.SubFolders = GetAllFolders(subfolder, ref testcases);
                foldersList[i] = folder;
                i++;
            }


            return foldersList;
        }

        public async Task<TestCase> GetTestCase(int testCaseId)
        {
            return await _context.testCases.AsNoTracking().Where(x => x.TestCaseId == testCaseId).FirstOrDefaultAsync();
        }

        public async Task<List<TestCase>> GetTestCases(int folderId)
        {
            var folder = await this.GetFolderById(folderId);
            if (folder != null)
            {
                var folders = new List<Folder>();
                folders.Add(folder);
                var testCases = new List<TestCase>();
                this.GetAllFolders(folders, ref testCases);
                return testCases;
            }
            return null;
        }
    }
}