using System.Collections.Generic;
using System.Threading.Tasks;
using myApiTreeView.Models;

namespace myApiTreeView.API.Data
{
    public interface IDataRepo
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();

        Task<Folder> GetFolderById(int? folderId);

        List<Folder> GetAllFolders(List<Folder> folders, ref List<TestCase> testcases);

        Task<TestCase> GetTestCase(int testCaseId);

        Task<List<TestCase>> GetTestCases(int folderId);

    }
}