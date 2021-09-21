using Microsoft.AspNet.Identity;
using NoteApp.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteApp.Controllers
{
    public class NoteController : Controller
    {
        readonly ApplicationDbContext dbContext = new ApplicationDbContext();
        public int pageSize = 06;
        DateTime dateTime = DateTime.Now;
        public int pageIndex = 1;
        string userId;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            userId = User.Identity.GetUserId<string>();
        }
        #region AddNotes

        [System.Web.Mvc.HttpGet]
        public ActionResult AddNotes()
        {
            ViewBag.Tags = dbContext.TagModel
                            .OrderByDescending(p => p.TagId)
                            .Select(p => p.TagItem).Take(10)
                            .ToArray();
            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult AddNotes(ListViewModel listViewModel, int Id)
        {

            ListModel existingList;
            using (var context = new ApplicationDbContext())
            {
                existingList = (from d in context.ListModel
                                where d.List_Id == Id && d.UserId == userId
                                select d).SingleOrDefault();
            }
            if (existingList != null)
            {
                //For List Notes 
                NotesModel notesModel = new NotesModel();
                notesModel.UserId = userId;
                notesModel.NoteDesciption = listViewModel.NotesModel.NoteDesciption;
                notesModel.IsActive = true;
                notesModel.List_Id = Id;
                notesModel.CreatedDate = dateTime;
                notesModel.UpdatedDate = dateTime;
                if (listViewModel.File != null)
                {
                    string str = listViewModel.File.FileName;
                    string mediaFile = "/UploadedFiles/" + str.ToString();
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(listViewModel.File.FileName));
                    listViewModel.File.SaveAs(path);
                    notesModel.NoteFile = mediaFile;
                }
                dbContext.NotesModel.Add(notesModel);
                dbContext.SaveChanges();
                ModelState.Clear();
                int Note_Id = int.Parse(dbContext.NotesModel
                                .OrderByDescending(p => p.NoteId)
                                .Select(r => r.NoteId)
                                .First().ToString());
                //For List Tags
                TagModel tagModel = new TagModel();
                NoteTag noteTag = new NoteTag();
                NoteTag noteTags = new NoteTag();
                if (listViewModel.Tags != null)
                {
                    for (int i = 0; i < listViewModel.Tags.Count(); i++)
                    {
                        var tags = listViewModel.Tags.ElementAt(i);
                        TagModel existingTag;
                        using (var context = new ApplicationDbContext())
                        {
                            existingTag = (from d in context.TagModel
                                           where d.TagItem == tags
                                           select d).SingleOrDefault();
                        }
                        if (existingTag != null)
                        {
                            tagModel.TagItem = tags;
                            tagModel.IsActive = true;
                            dbContext.TagModel.Attach(tagModel);
                            dbContext.SaveChanges();
                            noteTag.NoteId = Note_Id;
                            noteTag.TagId = existingTag.TagId;
                            dbContext.NoteTag.Add(noteTag);
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            using (var context = new ApplicationDbContext())
                            {
                                tagModel.TagItem = tags;
                                tagModel.IsActive = true;
                                context.TagModel.Add(tagModel);
                                context.SaveChanges();
                                var tagId = dbContext.TagModel
                                   .OrderByDescending(p => p.TagId)
                                   .Select(r => r.TagId)
                                   .First();
                                noteTags.TagId = tagId;
                                noteTags.NoteId = Note_Id;
                                context.NoteTag.Add(noteTags);
                                context.SaveChanges();
                            }
                        }
                    }
                }
                return RedirectToAction("ShowNotes", new { Id = Id });
            }
            return Content("Something going wrong !");
        }
        #endregion

        public ActionResult ShowNotes(int Id, int? Page)
        {
            //Show Note of Specific List
            pageIndex = Page.HasValue ? Convert.ToInt32(Page) : 1;
            var note = dbContext.NotesModel.Where(x => x.List_Id == Id && x.UserId == userId).OrderByDescending(x => x.NoteId).ToPagedList(pageIndex, pageSize); ;
            return View(note);
        }

        public PartialViewResult RecentNotes()
        {
            //Recent Notes
            var notes = dbContext.NotesModel.Where(x => x.UserId == userId).OrderByDescending(x => x.UpdatedDate).Take(4);
            return PartialView("_RecentNotes", notes);
        }

        public ActionResult SearchByTags(String Tag)
        {
            //Search Notes by Tags 
            int tagId = dbContext.TagModel.Where(x => x.TagItem == Tag).Select(x => x.TagId).FirstOrDefault();
            var noteId = dbContext.NoteTag
                                  .Where(d => d.TagId == tagId).ToList()
                                  .Select(d => (Int32)d.NoteId);
            List<NotesModel> notesModel = new List<NotesModel>();
            for (int j = 0; j < noteId.Count(); j++)
            {
                int _noteId = noteId.ElementAt(j);
                var note = dbContext.NotesModel.Where(x => x.NoteId == _noteId && x.UserId == userId).FirstOrDefault();
                if (note != null)
                {
                    notesModel.Add(note);
                }
            }
            return View(notesModel);
        }

        [HttpGet]
        public ActionResult DeleteNote(int Id)
        {
            //Delete Notes
            NotesModel notesModel = new NotesModel();
            notesModel = dbContext.NotesModel.Where(x => x.NoteId == Id).FirstOrDefault();
            var ListId = dbContext.NotesModel.Where(x => x.NoteId == Id).Select(x => x.List_Id).ToList();
            dbContext.NotesModel.Remove(notesModel);
            dbContext.SaveChanges();
            return RedirectToAction("ShowNotes", new { id = notesModel.List_Id });
        }
        [HttpGet]
        public JsonResult NoteDetail(int Id)
        {
            var ListId = dbContext.NotesModel.Where(x => x.NoteId == Id).Select(x => x.NoteDesciption).ToList();
            return Json(ListId.ElementAt(0), JsonRequestBehavior.AllowGet);
        }

        #region EditNote
        [HttpGet]
        public ActionResult EditNote(int Id)
        {
            //Edit Notes--Get 
            var note = dbContext.NotesModel.Where(x => x.NoteId == Id && x.UserId == userId).FirstOrDefault();
            if (note == null)
            {
                return Content("Something Going Wrong ...");
            }
            else
            {
                var tagIds = dbContext.NoteTag.Where(x => x.NoteId == note.NoteId).Select(x => x.TagId).ToList();
                List<string> existingTags = new List<string>();
                ViewBag.existingTags = existingTags;
                for (int i = 0; i < tagIds.Count; i++)
                {
                    int tagId = tagIds.ElementAt(i);
                    var tagItem = dbContext.TagModel.Where(x => x.TagId == tagId).Select(x => x.TagItem).FirstOrDefault();
                    existingTags.Add(tagItem);

                }
            }
            return View(note);
        }
        [System.Web.Http.HttpPost]
        public ActionResult EditNotes(NotesModel notesModel, List<string> Tags)
        {
            //Edit Notes--Get 
            DateTime dateTime = DateTime.Now;
            notesModel.UpdatedDate = dateTime;
            dbContext.Entry(notesModel).State = EntityState.Modified;
            dbContext.SaveChanges();
            TagModel tagModel = new TagModel();
            NoteTag noteTag = new NoteTag();
            NoteTag notetags = new NoteTag();

            var tagIds = dbContext.NoteTag.Where(x => x.NoteId == notesModel.NoteId).Select(x => x.Id).ToList();
            for (int i = 0; i < tagIds.Count; i++)
            {
                int tagId = tagIds.ElementAt(i);
                noteTag = dbContext.NoteTag.Where(x => x.NoteId == notesModel.NoteId).FirstOrDefault();
                dbContext.NoteTag.Remove(noteTag);
                dbContext.SaveChanges();
            }
            noteTag = dbContext.NoteTag.Where(x => x.NoteId == notesModel.NoteId).FirstOrDefault();
            for (int i = 0; i < Tags.Count(); i++)
            {
                var tags = Tags.ElementAt(i);
                TagModel existingTag;
                NoteTag noteTags = new NoteTag();
                using (var context = new ApplicationDbContext())
                {
                    existingTag = (from d in context.TagModel
                                   where d.TagItem == tags
                                   select d).SingleOrDefault();
                }
                if (existingTag != null)
                {
                    tagModel.TagItem = tags;
                    tagModel.IsActive = true;
                    dbContext.TagModel.Attach(tagModel);
                    dbContext.SaveChanges();
                    noteTags.NoteId = notesModel.NoteId;
                    noteTags.TagId = existingTag.TagId;
                    dbContext.NoteTag.Add(noteTags);
                    dbContext.SaveChanges();
                }
                else
                {
                    using (var context = new ApplicationDbContext())
                    {
                        tagModel.TagItem = tags;
                        tagModel.IsActive = true;
                        context.TagModel.Add(tagModel);
                        context.SaveChanges();
                        noteTags.NoteId = notesModel.NoteId;
                        var tagId = dbContext.TagModel
                                  .OrderByDescending(p => p.TagId)
                                  .Select(r => r.TagId)
                                  .First();
                        notetags.TagId = tagId;
                        notetags.NoteId = notesModel.NoteId;
                        context.NoteTag.Add(notetags);
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("ShowNotes", new { id = notesModel.List_Id });

        }
        #endregion
    }
}