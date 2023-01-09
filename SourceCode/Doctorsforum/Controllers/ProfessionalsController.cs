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
  public class ProfessionalsController : Controller {
    private DB_DoctorsforumEntities db = new DB_DoctorsforumEntities();

    // GET: Professionals
    public ActionResult Index(string id) {
      List<Professional> achievements = new List<Professional>();
      foreach (Professional av in db.Professionals) {
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
    // GET: Professionals/Details/5


    // GET: Professionals/Create
    public ActionResult Create() {
      return View();
    }

    // POST: Professionals/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ProID,User_id,ProName,Date_Range,Issued_by")] Professional professional, string usid) {
      ModelState.Remove("User_id");
      if (ModelState.IsValid) {
        //Random random = new Random();
        //professional.ProID = random.Next().ToString();
        professional.User_id = usid;
        db.Professionals.Add(professional);
        db.SaveChanges();
        return RedirectToAction("Index", "Professionals", new { id = usid });
      }
      return View(professional);
    }

    // GET: Professionals/Edit/5
    public ActionResult Edit(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Professional professional = db.Professionals.Find(int.Parse(id));
      if (professional == null) {
        return HttpNotFound();
      }
      ViewBag.User_id = new SelectList(db.DB_user, "User_id", "Password", professional.User_id);
      return View(professional);
    }

    // POST: Professionals/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ProID,User_id,ProName,Date_Range,Issued_by")] Professional professional) {
      if (ModelState.IsValid) {
        db.Entry(professional).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index", "Professionals", new { id = professional.User_id });
      }
      ViewBag.User_id = new SelectList(db.DB_user, "User_id", "Password", professional.User_id);
      return View(professional);
    }

    // GET: Professionals/Delete/5
    public ActionResult Delete(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Professional professional = db.Professionals.Find(int.Parse(id));
      if (professional == null) {
        return HttpNotFound();
      }
      return View(professional);
    }

    // POST: Professionals/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id) {
      Professional professional = db.Professionals.Find(int.Parse(id));
      db.Professionals.Remove(professional);
      db.SaveChanges();
      return RedirectToAction("Index", "Professionals", new { id = professional.User_id });
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
