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
        [Column(TypeName = "varchar(MAX)")]
        public string NoteDesciption { get; set; }    
        public string NoteFile { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("List_Id")]
        public virtual ListModel List { get; set; }
    }
}