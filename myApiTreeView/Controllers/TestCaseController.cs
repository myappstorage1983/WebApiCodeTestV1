using Microsoft.AspNetCore.Mvc;
using myApiTreeView.API.Dtos;
using myApiTreeView.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace myApiTreeView.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCaseController : ControllerBase
    {
        private ITestCaseService _testCaseService = null;

        public TestCaseController(ITestCaseService testCaseService)
        {
            _testCaseService = testCaseService;        
        }


        /// <summary>
        /// This method will fetch all the underlying test cases with in the given folder id  and corresponding subfolders.
        /// </summary>
        /// <param name="id">Folder Id</param>
        /// <returns>List of TestCases</returns>
        [HttpGet("{folderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<TestCaseDto>>> GetTestCasesInsideFolder(int folderId)
        {
            var testCasesResult = await _testCaseService.GetTestCases(folderId);
            if (testCasesResult == null || testCasesResult.Count == 0)
            {
                return NotFound();
            }
           
            return Ok(testCasesResult);
        }

        /// <summary>
        /// This method adds testcase to a folder.
        /// </summary>
        /// <param name="testCaseDto"></param>
        /// <returns>True/False</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TestCaseDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddTestCase([FromBody]TestCaseDto testCaseDto)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _testCaseService.AddTestCase(testCaseDto);
                if(isSuccess)
                    return StatusCode((int)HttpStatusCode.Created);
                else
                    return StatusCode((int)HttpStatusCode.BadRequest);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });
            }
        }


        /// <summary>
        /// This method deletes testcase from the given folder.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True/False</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTestCase(int id)
        {
            var testcase = _testCaseService.GetTestCase(id).Result;

            if (testcase == null)
                return NotFound();

            await _testCaseService.DeleteTestCase(testcase);

            return StatusCode((int)HttpStatusCode.Accepted);
        }

    }
}
