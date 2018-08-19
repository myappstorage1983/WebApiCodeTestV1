using System.ComponentModel.DataAnnotations;

namespace myApiTreeView.API.Dtos
{
    public class TestCaseDto
    {
        public TestCaseDto()
        {
            FolderId = 0;
        }
        public int TestCaseId { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public int StepCount { get; set; }
        public int? FolderId { get; set; }
    }
}