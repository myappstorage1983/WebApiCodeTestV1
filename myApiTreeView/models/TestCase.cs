using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myApiTreeView.Models
{

    public class TestCase
    {  
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Column("Id")]  
        public int TestCaseId { get; set; }         

        [Required]
        public string Name {get;set;}
        [Required]
        public int  StepCount {get;set;}
       
        public int? FolderId {get; set;}
       
        public Folder folder {get;set;}

    }
}