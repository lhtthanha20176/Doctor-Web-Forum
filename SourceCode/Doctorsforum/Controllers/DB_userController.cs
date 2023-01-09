using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Doctorsforum.Models;

namespace Doctorsforum.Controllers {
  public class DB_userController : Controller {
    private DB_DoctorsforumEntities db = new DB_DoctorsforumEntities();

    // GET: DB_user
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
    public ActionResult Index() {
      return View(db.DB_user.ToList());
    }

    // GET: DB_user/Details/5
    public ActionResult Details(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      List<Post_Detail> Post_Detail = new List<Post_Detail>();
      foreach (var post in db.Post_Detail) {
        if (post.Userpost == id) {
          Post_Detail.Add(post);
        }
      }
      DB_user dB_user = db.DB_user.Find(id);
      if (dB_user == null) {
        return HttpNotFound();
      }
      return View(Post_Detail);
    }

    // GET: DB_user/Create
    public ActionResult Create() {
      return View();
    }

    // POST: DB_user/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "User_id,Password,First_name,Last_name,Image,Address,Phone_number,Email_address,Sex,Age,Experience,profession,public_profile,Status")] DB_user dB_user) {
      string _FileName = "";
      string _path = "";
      //string ms = "Creat account unsuccessful";
      if (ModelState.IsValid) {
        HttpPostedFileBase file = Request.Files["image"];
        if (file.FileName != "") {
          string name = file.FileName;
          string tail = name.Substring(name.IndexOf("."));
          _FileName = dB_user.User_id + tail;
          _path = Path.Combine(Server.MapPath("~/image"), _FileName);
          file.SaveAs(_path);
        } else {
          _FileName = "Df.png";
        }
        // ms = "Creat account successfully";
        dB_user.Status = "Distable";
        dB_user.Image = _FileName;
        db.DB_user.Add(dB_user);
        db.SaveChanges();
        TempData["meseger"] = "ms";
        return RedirectToAction("Index");
      }

      return View(dB_user);
    }
    public ActionResult profile(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      DB_user dB_user = db.DB_user.Find(id);
      if (dB_user == null) {
        return HttpNotFound();
      }
      return View(dB_user);
    }
    public ActionResult profile([Bind(Include = "User_id,Password,First_name,Last_name,Image,Address,Phone_number,Email_address,Sex,Age,Experience,profession,public_profile,Status")] DB_user dB_user) {
      if (ModelState.IsValid) {
        db.Entry(dB_user).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(dB_user);
    }


    public ActionResult Edit(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      DB_user dB_user = db.DB_user.Find(id);
      if (dB_user == null) {
        return HttpNotFound();
      }
      return View(dB_user);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "User_id,Password,First_name,Last_name,Image,Address,Phone_number,Email_address,Sex,Age,Experience,profession,public_profile,Status")] DB_user dB_user) {
      string _FileName = "";
      string _path = "";
      //string ms = "Update account unsuccessful";
      if (ModelState.IsValid) {
        HttpPostedFileBase file = Request.Files["image"];
        if (file.FileName != "") {
          string name = file.FileName;
          string tail = name.Substring(name.IndexOf("."));
          _FileName = dB_user.User_id + tail;
          _path = Path.Combine(Server.MapPath("~/image"), _FileName);
          file.SaveAs(_path);
        } else {
          _FileName = dB_user.Image;
        }
        // ms= "Update account successfully";
        dB_user.Image = _FileName;
        db.Entry(dB_user).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(dB_user);
    }

    // GET: DB_user/Delete/5
    public ActionResult Delete(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      DB_user dB_user = db.DB_user.Find(id);
      if (dB_user == null) {
        return HttpNotFound();
      }
      return View(dB_user);
    }

    // POST: DB_user/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id) {
      DB_user dB_user = db.DB_user.Find(id);
      db.DB_user.Remove(dB_user);
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
