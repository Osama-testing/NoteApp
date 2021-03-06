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
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using AutoMapper;

namespace NoteApp.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
         readonly ApplicationDbContext dbContext = new ApplicationDbContext();
         public   int pageSize = 06;
         public int pageIndex = 1;
         readonly DateTime dateTime = DateTime.Now;
        string userId;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            userId = User.Identity.GetUserId<string>();
        }
        public ActionResult Index()
        {
            return View();
        }
        #region CRUD_List

        public PartialViewResult GetLists(int? page)
        {
            //Get All lists of login User
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var getLists = dbContext.ListModel.Where(x=>x.UserId==userId).OrderByDescending(d => d.List_Id).ToPagedList(pageIndex, pageSize);
            return PartialView("_GetLists", getLists);
        }
        [ValidateAntiForgeryToken]
        public ActionResult AddList(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                ListModel existingName;
                using (var context = new ApplicationDbContext())
                {
                    existingName = (from d in context.ListModel
                                    where d.Name == listModel.Name && d.UserId == userId
                                    select d).SingleOrDefault();
                }
                if (existingName == null)
                {
                    //Create New List
                    listModel.CreatedDate = dateTime;
                    listModel.UpdatedDate = dateTime;
                    listModel.UserId = userId;
                    listModel.IsActive = true;
                    dbContext.ListModel.Add(listModel);
                    dbContext.SaveChanges();
                    ModelState.Clear();
                }
                else
                {
                    TempData["list"] = "AlreadyExists";
                }

                return RedirectToAction("Index", "List");
            }
            return View("Index",listModel);
        }
        public ActionResult DeleteList(int? Id, ListModel listModel)
        {
            //Delete List
            listModel = dbContext.ListModel.Where(x => x.List_Id == Id).FirstOrDefault();
            dbContext.ListModel.Remove(listModel);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
   
        [HttpGet]
        public ActionResult EditList(int Id)
        {
            //Edit List
            var a = dbContext.ListModel.Where(x => x.List_Id == Id && x.UserId==userId).FirstOrDefault();
            if (a!=null)
            {
                return View(a);
            }
            else
            {
                TempData["list"] = "WrongId";
            }
            return View();

        }
 
        [System.Web.Http.HttpPut]
        public ActionResult EditLists(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                //Edit List
                if (listModel.List_Id != 0)
                {
                    listModel.UserId = User.Identity.GetUserId<string>();
                    listModel.UpdatedDate = dateTime;
                    dbContext.Entry(listModel).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return View("Index");
                }
                else
                {
                    return View("Index");
                }
            }
            return View("Index");


        }
        #endregion

      
    }
}
