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
  public class AchievementsController : Controller {
    private DB_DoctorsforumEntities db = new DB_DoctorsforumEntities();

    // GET: Achievements
    public ActionResult Index(string id) {
      List<Achievement> achievements = new List<Achievement>();
      foreach (Achievement av in db.Achievements) {
        if (av.User_id == id) {
          achievements.Add(av);
        }
      }
      return View(achievements);
    }

    public ActionResult Create() {
      return View();
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
    // POST: Achievements/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "AchID,User_id,AchName,Issued_by,Date_Range")] Achievement achievement, string usid) {
      ModelState.Remove("User_id");
      if (ModelState.IsValid) {
        //Random random = new Random();
        //achievement.AchID = random.Next().ToString();
        achievement.User_id = usid;
        db.Achievements.Add(achievement);
        db.SaveChanges();
        return RedirectToAction("Index", "Achievements", new { id = achievement.User_id });
      }
      return View(achievement);
    }

    // GET: Achievements/Edit/5
    public ActionResult Edit(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Achievement achievement = db.Achievements.Find(int.Parse(id));
      if (achievement == null) {
        return HttpNotFound();
      }
      ViewBag.User_id = new SelectList(db.DB_user, "User_id", "Password", achievement.User_id);

      return View(achievement);
    }

    // POST: Achievements/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "AchID,User_id,AchName,Issued_by,Date_Range")] Achievement achievement) {
      if (ModelState.IsValid) {
        db.Entry(achievement).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index", "Achievements", new { id = achievement.User_id });
      }
      ViewBag.User_id = new SelectList(db.DB_user, "User_id", "Password", achievement.User_id);

      return View(achievement);
    }

    // GET: Achievements/Delete/5
    public ActionResult Delete(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Achievement achievement = db.Achievements.Find(int.Parse(id));
      if (achievement == null) {
        return HttpNotFound();
      }
      return View(achievement);
    }

    // POST: Achievements/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id) {
      Achievement achievement = db.Achievements.Find(int.Parse(id));
      db.Achievements.Remove(achievement);
      db.SaveChanges();
      return RedirectToAction("Index", "Achievements", new { id = achievement.User_id });
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
