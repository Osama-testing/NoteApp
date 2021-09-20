using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    [Table("tblTags")]
    public class TagModel
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        public string TagItem { get; set; }
        public bool IsActive { get; set; }
    }
}