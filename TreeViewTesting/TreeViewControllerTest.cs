using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using myApiTreeView.API.Dtos;
using myApiTreeView.Controllers;
using myApiTreeView.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace TreeViewTesting
{
    [TestClass]
    public class TreeViewControllerTest
    {
        private Mock<IFolderService> _folderServiceMock;
        private Mock<ITestCaseService> _testCaseServiceMock;
        private TreeViewController _controller;

        [TestInitialize]
        public void Initialize()
        {
            var mocker = new AutoMocker();
            _folderServiceMock = mocker.GetMock<IFolderService>();
            _testCaseServiceMock = mocker.GetMock<ITestCaseService>();
            _controller = new TreeViewController(_folderServiceMock.Object, _testCaseServiceMock.Object);
        }

        [TestMethod]
        public async Task GetTestCasesInsideFolderTest_Success()
        {
            //Arrange
            _testCaseServiceMock.Setup(service => service.GetTestCases(It.IsAny<int>()))
                .ReturnsAsync(getMockTestCaseDtoList());

            // Act
            var response = await _controller.GetTestCasesInsideFolder(1);

            //var actionResult = (ActionResult<List<TestCaseDto>>)response.Result;
            //var result = (OkObjectResult)((ActionResult<List<TestCaseDto>>)response.Result).Result;

            ////Assert

            //Assert.IsNotNull(response);
            //_testCaseServiceMock.Verify(t => t.GetTestCases(1), Times.Once);
            //Assert.IsTrue(result.StatusCode == (int)HttpStatusCode.OK);

            var okResult = response.Result as OkObjectResult;
            //Assert
            Assert.IsInstanceOfType(okResult.Value, typeof(List<TestCaseDto>));
        }

        [TestMethod]
        public async Task GetTestCasesInsideFolderTest_FailureNotFound()
        {
            //Arrange
            _testCaseServiceMock.Setup(service => service.GetTestCases(It.IsAny<int>()))
                .ReturnsAsync(new List<TestCaseDto>());

            // ACT
            var response = await _controller.GetTestCasesInsideFolder(101);
            var actionResult = (ActionResult<List<TestCaseDto>>)response.Result;
            var result = (NotFoundResult)((ActionResult)response.Result);

            //Assert
            Assert.IsTrue(result.StatusCode == (int)HttpStatusCode.NotFound);
        }

      
        private static List<TestCaseDto> getMockTestCaseDtoList()
        {
            return new List<TestCaseDto>() {
                        new TestCaseDto(){
                            TestCaseId = 11,
                            Name = "TestCase11",
                            StepCount = 11,
                            FolderId = 1
                        },
                        new TestCaseDto(){
                           TestCaseId = 12,
                            Name = "TestCase12",
                            StepCount = 12,
                            FolderId = 1
                        }
                };
        }
    }
}
