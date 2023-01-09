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
  public class QualificationsController : Controller {
    private DB_DoctorsforumEntities db = new DB_DoctorsforumEntities();

    public ActionResult Index(string id) {
      List<Qualification> achievements = new List<Qualification>();
      foreach (Qualification av in db.Qualifications) {
        if (av.User_id == id) {
          achievements.Add(av);
        }
      }
      return View(achievements);
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
    public ActionResult Create() {
      //ViewBag.User_id = new SelectList(db.DB_user, "User_id", "Password");
      return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "QualiID,User_id,QualiName,Date_Range,Issued_by")] Qualification qualification, string usid) {
      ModelState.Remove("User_id");
      if (ModelState.IsValid) {
        //Random random = new Random();
        //qualification.QualiID = random.Next().ToString();
        qualification.User_id = usid;
        db.Qualifications.Add(qualification);
        db.SaveChanges();
        return RedirectToAction("Index", "Qualifications", new { id = usid });
      }

      ViewBag.User_id = new SelectList(db.DB_user, "User_id", "Password", qualification.User_id);
      return View(qualification);
    }

    public ActionResult Edit(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Qualification qualification = db.Qualifications.Find(int.Parse(id));
      if (qualification == null) {
        return HttpNotFound();
      }
      ViewBag.User_id = new SelectList(db.DB_user, "User_id", "Password", qualification.User_id);
      return View(qualification);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "QualiID,User_id,QualiName,Date_Range,Issued_by")] Qualification qualification) {
      if (ModelState.IsValid) {
        db.Entry(qualification).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index", "Qualifications", new { id = qualification.User_id });
      }
      ViewBag.User_id = new SelectList(db.DB_user, "User_id", "Password", qualification.User_id);
      return View(qualification);
    }


    public ActionResult Delete(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Qualification qualification = db.Qualifications.Find(int.Parse(id));

      if (qualification == null) {
        return HttpNotFound();
      }
      return View(qualification);
    }

    // POST: Qualifications/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id) {
      Qualification qualification = db.Qualifications.Find(int.Parse(id));

      db.Qualifications.Remove(qualification);
      db.SaveChanges();
      return RedirectToAction("Index", "Qualifications", new { id = qualification.User_id });
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
