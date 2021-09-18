using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    [Table("tblLists")]
    public class ListModel
    {
        [Key]
        public int List_Id { get; set; }          
        public string UserId { get; set; }                  //Foreign key--Created By
        [Required]
        public string Name { get; set; }    
        public DateTime CreatedDate { get; set; }     //When user create the List
        public DateTime UpdatedDate { get; set; }     //When user update the List
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}