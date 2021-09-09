﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    [Table("NoteTag")]
    public class NoteTag
    {
        public int Id { get; set; }


   
        public int TagId { get; set; }
        public int NoteId { get; set; }
        [ForeignKey("TagId")]
        public virtual TagModel TagModel { get; set; }
        [ForeignKey("NoteId")]
        public virtual NotesModel NotesModel { get; set; }

    }
}