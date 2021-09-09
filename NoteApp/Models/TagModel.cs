using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    [Table("TagTbl")]
    public class TagModel
    {
        [Key]
        public int TagId { get; set; }
    //    public int NoteId { get; set; }    //Foreign key--Note Id
        public string TagItem { get; set; }
        public bool IsActive { get; set; }

        //[ForeignKey("NoteId")]
        //public virtual NotesModel Notes{ get; set; }
    }
}