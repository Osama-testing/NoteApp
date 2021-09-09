using Microsoft.AspNet.Identity;
using NoteApp.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace NoteApp.Controllers
{
    public class ListController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        // GET: List
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetLists(int? page)
        {
            int pageSize = 06;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            string userId = User.Identity.GetUserId<string>();
            var a = dbContext.ListModel.ToList().Where(x=>x.Id==userId).OrderByDescending(d => d.List_Id).ToPagedList(pageIndex, pageSize);
            return PartialView("_GetLists", a);
        }
        public ActionResult AddList(ListModel listModel)
        {
            DateTime dateTime = DateTime.Now;
            string userId = User.Identity.GetUserId<string>();
            //////////////////////////////////////////////////
            listModel.CreatedDate = dateTime;
            listModel.UpdatedDate = dateTime;
            listModel.Id = userId;
            listModel.IsActive = true;
            dbContext.ListModel.Add(listModel);
            dbContext.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Index", "List");
        }
        public ActionResult DeleteList(int? Id, ListModel listModel)
        {
            listModel = dbContext.ListModel.Where(x => x.List_Id == Id).FirstOrDefault();
            dbContext.ListModel.Remove(listModel);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //[System.Web.Mvc.HttpGet]
        //public ActionResult AddNotes()
        //{
        //   // dbContext.ListModel.Where(x => x.List_Id == Id).FirstOrDefault()
        //    return View();
        //}
        //[System.Web.Mvc.HttpPost]
        //public ActionResult AddNotes(ListModel listModel, int? Id, HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
        //        file.SaveAs(path);
        //    }
        //    string userId = User.Identity.GetUserId<string>();
        //    var a = dbContext.ListModel.AsNoTracking().FirstOrDefault(x => x.List_Id == Id);
        //    listModel.List_Id = a.List_Id;
        //    listModel.ListName = a.ListName;
        //    listModel.CreatedDate = a.CreatedDate;
        //    listModel.UpdatedDate = a.UpdatedDate;
        //    listModel.IsActive = a.IsActive;
        //    listModel.Id = userId;
        //   // listModel.FilePath = file.FileName;
        //    dbContext.Entry(listModel).State = EntityState.Modified;
        //    dbContext.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [System.Web.Mvc.HttpGet]
        public ActionResult AddNotes()
        {
            
            return View();
        }
        [System.Web.Http.HttpPost]
        public ActionResult AddNotes(ListViewModel listViewModel, int Id)
        {
            DateTime dateTime = DateTime.Now;
            string userId = User.Identity.GetUserId<string>();
            //For List Notes 
            NotesModel notesModel = new NotesModel();
            notesModel.Id = userId;
            notesModel.NoteDesciption = listViewModel.NotesModel.NoteDesciption;
            notesModel.IsActive = true;
            notesModel.List_Id = Id;
            notesModel.CreatedDate = dateTime;
            if (listViewModel.File != null)
            {
                string str = listViewModel.File.FileName;
                string Image = "/UploadedFiles/" + str.ToString();
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(listViewModel.File.FileName));
                listViewModel.File.SaveAs(path);
                notesModel.NoteFile = Image;
            }
            dbContext.NotesModel.Add(notesModel);
            dbContext.SaveChanges();
            int Note_Id = int.Parse(dbContext.NotesModel
                            .OrderByDescending(p => p.NoteId)
                            .Select(r => r.NoteId)
                            .First().ToString());
            //For List Tags
            TagModel tagModel = new TagModel();
            NoteTag noteTag = new NoteTag();
            if (listViewModel.Tags != null)
            {
                for (int i = 0; i < listViewModel.Tags.Count(); i++)
                {
                    var tags = listViewModel.Tags.ElementAt(i);
                    tagModel.TagItem = tags;
                    tagModel.IsActive = true;
                    dbContext.TagModel.Attach(tagModel);
                    //dbContext.TagModel.Add(tagModel);
                    //dbContext.SaveChanges();
                    ////////////////////////////////////////////////////////
                    int Tag_Id = int.Parse(dbContext.TagModel
                           .OrderByDescending(p => p.TagId)
                           .Select(r => r.TagId)
                           .First().ToString());
                    noteTag.NoteId = Note_Id;
                    noteTag.TagId = Tag_Id;
                    dbContext.NoteTag.Add(noteTag);
                    dbContext.SaveChanges();


                }
            }
            return View("AddNotes");
        }
        [HttpGet]
        public ActionResult EditList(int Id)
        {
                var a = dbContext.ListModel.Where(x => x.List_Id == Id).FirstOrDefault();
                return View(a);
            
        }
        [System.Web.Http.HttpPut]
        public ActionResult EditLists(ListModel listModel)
        {
            DateTime dateTime = DateTime.Now;
            listModel.Id = User.Identity.GetUserId<string>();
            listModel.UpdatedDate = dateTime;
            dbContext.Entry(listModel).State = EntityState.Modified;
            dbContext.SaveChanges();
            return View("Index");

        }

        public ActionResult ShowNotes(int Id)
        {
            var note = dbContext.NotesModel.Where(x => x.List_Id == Id).ToList().OrderByDescending(x => x.NoteId);
            var note_Id = dbContext.NotesModel
                  .Where(p => p.List_Id == Id)
                  .Select(p => p.NoteId).ToList();
            for (int i = 0; i < note_Id.Count; i++)
            {
                int note_id = note_Id.ElementAt(i);
                // var testTag = dbContext.TagModel.Where(x => x.NoteId == note_id).Select(x => x.TagItem).ToList();
                //ViewBag.Tags = testTag;
                //  List<string> fundList = dbContext.TagModel.Where(x=> x.NoteId==note_id).Select(x=>x.TagItem).ToList();            
                //  ViewBag.Funds = fundList;
            }

            // var Note_Id=dbContext.NotesModel.FirstOrDefault(x => x.NoteId ==listViewModel.GetNotes. );
            // var b=dbContext.TagModel.Where(x=>x.NoteId==a.)
            return View(note);
        }
        public PartialViewResult RecentNotes()
        {
            string userId = User.Identity.GetUserId<string>();
            var notes = dbContext.NotesModel.Where(x => x.Id == userId).OrderByDescending(x => x.NoteId).Take(4);
            return PartialView("_RecentNotes",notes);
        }

        public ActionResult SearchByTags(String Tag)
        {
          //  var tags = dbContext.TagModel.ToList().Where(x => x.TagItem == Tag).Select(x => x.NoteId);
            return View();
        }


    }
}
