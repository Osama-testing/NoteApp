//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoteApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ListTbl
    {
        public int List_Id { get; set; }
        public string Id { get; set; }
        public string ListName { get; set; }
        public string ListDescription { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string FilePath { get; set; }
    }
}
