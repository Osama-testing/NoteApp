using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    public class ListViewModel
    {
        //public IEnumerable<ListModel> ListModels { get; set; }
        //public IEnumerable<NotesModel> NotesModel { get; set; }
        //public IEnumerable<TagModel> TagModel { get; set; }
        public ListModel ListModels { get; set; }
        public NotesModel NotesModel { get; set; }
        public TagModel TagModel { get; set; }

        public HttpPostedFileBase File { get; set; }
        public List<string> Tags { get; set; }
        //  public IEnumerable<ListModel> ListModel { get; set; }
        public List<ListModel> GetListModels { get; set; }
        public List<NotesModel> GetNotesModels { get; set; }
        public List<NotesModel> GetNotes { set; get; }

    }
}