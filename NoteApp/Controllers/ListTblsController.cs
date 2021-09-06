using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    public class ListTblsController : Controller
    {
        private NoteApp2Entities db = new NoteApp2Entities();

        // GET: ListTbls
        public ActionResult Index()
        {
            return View(db.ListTbls.ToList());
        }

        // GET: ListTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListTbl listTbl = db.ListTbls.Find(id);
            if (listTbl == null)
            {
                return HttpNotFound();
            }
            return View(listTbl);
        }

        // GET: ListTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListTbls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "List_Id,Id,ListName,ListDescription,CreatedDate,UpdatedDate,IsActive,FilePath")] ListTbl listTbl)
        {
            if (ModelState.IsValid)
            {
                db.ListTbls.Add(listTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(listTbl);
        }

        // GET: ListTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListTbl listTbl = db.ListTbls.Find(id);
            if (listTbl == null)
            {
                return HttpNotFound();
            }
            return View(listTbl);
        }

        // POST: ListTbls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "List_Id,Id,ListName,ListDescription,CreatedDate,UpdatedDate,IsActive,FilePath")] ListTbl listTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(listTbl);
        }

        // GET: ListTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListTbl listTbl = db.ListTbls.Find(id);
            if (listTbl == null)
            {
                return HttpNotFound();
            }
            return View(listTbl);
        }

        // POST: ListTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListTbl listTbl = db.ListTbls.Find(id);
            db.ListTbls.Remove(listTbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
