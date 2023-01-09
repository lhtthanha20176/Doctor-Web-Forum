using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Doctorsforum.Models;

namespace Doctorsforum.Controllers {
  public class Post_DetailController : Controller {

    private DB_DoctorsforumEntities db = new DB_DoctorsforumEntities();

    // GET: Post_Detail
    public ActionResult Index() {
      var post_Detail = db.Post_Detail.Include(p => p.Topic);
      return View(post_Detail.ToList());
    }
    public ActionResult logout(string id) {
      DB_user user = db.DB_user.Find(id);
      Session.Remove("id");
      Session.Remove("img");
      Session.Remove("name");
      changestatus(user, "Distable");
      return RedirectToAction("Index", "Home", new { id = user.User_id });
    }
    private void changestatus(DB_user user, string status) {
      user.Status = status;
      db.Entry(user).State = EntityState.Modified;
      db.SaveChanges();

    }
    // GET: Post_Detail/Details/5
    public ActionResult Details(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Post_Detail post_Detail = db.Post_Detail.Find(int.Parse(id));
      if (post_Detail == null) {
        return HttpNotFound();
      }
      return View(post_Detail);
    }

    // GET: Post_Detail/Create
    public ActionResult Create() {
      ViewBag.Topic_id = new SelectList(db.Topics, "Topic_id", "TopicName");
      return View();
    }

    // POST: Post_Detail/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Post_id,Level,forid,Image,NumericalOrder,Topic_id,Email,TimePost,Content,Userpost")] Post_Detail post_Detail) {
      if (ModelState.IsValid) {
        db.Post_Detail.Add(post_Detail);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.Topic_id = new SelectList(db.Topics, "Topic_id", "TopicName", post_Detail.Topic_id);
      return View(post_Detail);
    }

    // GET: Post_Detail/Edit/5
    public ActionResult Edit(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Post_Detail post_Detail = db.Post_Detail.Find(int.Parse(id));
      if (post_Detail == null) {
        return HttpNotFound();
      }
      ViewBag.Topic_id = new SelectList(db.Topics, "Topic_id", "TopicName", post_Detail.Topic_id);
      return View(post_Detail);
    }

    // POST: Post_Detail/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Post_id,Level,forid,Image,NumericalOrder,Topic_id,Email,TimePost,Content,Userpost")] Post_Detail post_Detail) {
      if (ModelState.IsValid) {
        db.Entry(post_Detail).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.Topic_id = new SelectList(db.Topics, "Topic_id", "TopicName", post_Detail.Topic_id);
      return View(post_Detail);
    }
    // GET: Post_Detail/Delete/5
    public ActionResult Delete(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Post_Detail post_Detail = db.Post_Detail.Find(int.Parse(id));
      if (post_Detail == null) {
        return HttpNotFound();
      }
      return View(post_Detail);
    }

    // POST: Post_Detail/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id) {
      Post_Detail post_Detail = db.Post_Detail.Find(int.Parse(id));
      db.Post_Detail.Remove(post_Detail);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
