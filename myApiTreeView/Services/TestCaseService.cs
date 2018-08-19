using AutoMapper;
using myApiTreeView.API.Data;
using myApiTreeView.API.Dtos;
using myApiTreeView.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myApiTreeView.Services
{
    public class TestCaseService : ITestCaseService
    {
        private readonly IDataRepo _repo;

        public TestCaseService(IDataRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddTestCase(TestCaseDto testCaseDto)
        {
            var testCase = Mapper.Map<TestCase>(testCaseDto);
            _repo.Add<TestCase>(testCase);
            return await _repo.SaveAll();
        }

        public async Task DeleteTestCase(TestCaseDto testCaseDto)
        {
            var testCase = Mapper.Map<TestCase>(testCaseDto);
            _repo.Delete<TestCase>(testCase);
             await _repo.SaveAll();
        }

        public async Task<TestCaseDto> GetTestCase(int testCaseId)
        {
            var testCase = await _repo.GetTestCase(testCaseId);
            return Mapper.Map<TestCaseDto>(testCase);
        }
        
        public async Task<List<TestCaseDto>> GetTestCases(int folderId)
        {
            var testcases = await _repo.GetTestCases(folderId);
            return Mapper.Map<List<TestCaseDto>>(testcases);
        }
    }
}
