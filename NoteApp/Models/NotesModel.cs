using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    [Table("NotesTbl")]
    public class NotesModel
    {
        [Key]
        public int NoteId { get; set; }
        public int List_Id { get; set; }
        public string Id { get; set; }         // Created By --User Id

        [Column(TypeName = "varchar(MAX)")]
        public string NoteDesciption { get; set; }    
        public string NoteFile { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }


        [ForeignKey("List_Id")]
        public virtual ListModel List { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
    }
}