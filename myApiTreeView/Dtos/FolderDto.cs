using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using myApiTreeView.Models;

namespace myApiTreeView.API.Dtos
{

 public class FolderDto
    {
        public FolderDto()
        {
            ParentFolderId = 0; 
        }
       
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }

        public int? ParentFolderId { get; set; }

        public ICollection<TestCaseDto> TestCases {get;set;} = new List<TestCaseDto>();
    }
}