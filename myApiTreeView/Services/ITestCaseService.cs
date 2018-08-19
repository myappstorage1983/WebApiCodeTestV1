using System.Collections.Generic;
using System.Threading.Tasks;
using myApiTreeView.API.Dtos;
using myApiTreeView.Models;

namespace myApiTreeView.Services
{
    public interface ITestCaseService
    {
        Task<TestCaseDto> GetTestCase(int testCaseId);

        Task<bool> AddTestCase(TestCaseDto testCaseDto);

        Task DeleteTestCase(TestCaseDto testCaseDto);

        Task<List<TestCaseDto>> GetTestCases(int folderId);
    }
}