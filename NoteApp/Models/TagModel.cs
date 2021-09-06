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
        public int List_Id { get; set; }    //Foreign key--List Id
        public string TagItem { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("List_Id")]
        public virtual ListModel List { get; set; }
    }
}