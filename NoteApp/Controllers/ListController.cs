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
            ViewBag.GetList = dbContext.ListModel.ToList();
            ViewBag.GetNotes = dbContext.ListModel.ToList();
            ViewBag.GetTags = dbContext.TagModel.ToList();

           
            return View();
        }
        public PartialViewResult GetLists(int? page)
        {
            int pageSize = 06;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var a = dbContext.ListModel.ToList().OrderByDescending(d => d.List_Id).ToPagedList(pageIndex, pageSize);
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

        [System.Web.Mvc.HttpGet]
        public ActionResult AddNotes()
        {
           // dbContext.ListModel.Where(x => x.List_Id == Id).FirstOrDefault()
            return View();
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult AddNotes(ListModel listModel, int? Id, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
            }
            string userId = User.Identity.GetUserId<string>();
            var a = dbContext.ListModel.AsNoTracking().FirstOrDefault(x => x.List_Id == Id);
            listModel.List_Id = a.List_Id;
            listModel.ListName = a.ListName;
            listModel.CreatedDate = a.CreatedDate;
            listModel.UpdatedDate = a.UpdatedDate;
            listModel.IsActive = a.IsActive;
            listModel.Id = userId;
           // listModel.FilePath = file.FileName;
            dbContext.Entry(listModel).State = EntityState.Modified;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult testing()
        {
            
            return View();
        }
        [System.Web.Http.HttpPost]
        public ActionResult testing(ListViewModel listViewModel, int Id)
        {
            //For List Notes 
            NotesModel notesModel = new NotesModel();
            notesModel.NoteDesciption = listViewModel.NotesModel.NoteDesciption;
            notesModel.IsActive = true;
            notesModel.List_Id = Id;
            if (listViewModel.File != null)
            {
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(listViewModel.File.FileName));
                listViewModel.File.SaveAs(path);
                notesModel.NoteFile = listViewModel.File.FileName;
            }
            dbContext.NotesModel.Add(notesModel);
            dbContext.SaveChanges();

            //For List Tags
            TagModel tagModel = new TagModel();
            for (int i = 0; i < listViewModel.Tags.Count(); i++)
            {
                var tags = listViewModel.Tags.ElementAt(i);
                tagModel.List_Id = Id;
                tagModel.TagItem = tags;
                tagModel.IsActive = true;

                dbContext.TagModel.Add(tagModel);
                dbContext.SaveChanges();

            }

            return View("testing");
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


    }
}
