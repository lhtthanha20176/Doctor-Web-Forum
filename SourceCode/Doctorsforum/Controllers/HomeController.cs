using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Doctorsforum.Models;

namespace Doctorsforum.Controllers {
  public class HomeController : Controller {
    private DB_DoctorsforumEntities db = new DB_DoctorsforumEntities();
    public ActionResult Index() {
      Session["register"] = db.DB_user.Count() - 1;
      Session["online"] = db.DB_user.Where(p => p.Status == "Enable").Count();
      ViewBag.Post = db.Post_Detail;
      return View();
    }


    public ActionResult search() {
      string key = Request["search"];
      ViewBag.search = db.Post_Detail.SqlQuery("Select * from Post_Detail where Content like '%" + key + "%'").ToList();
      int count = db.Post_Detail.SqlQuery("Select * from Post_Detail where Content like '%" + key + "%'").Count();
      if (count == 0) {
        TempData["mes"] = "No results were found: " + key;
      }
      return View();
    }
    public ActionResult editprofile(string id) {
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
    public ActionResult editprofile([Bind(Include = "User_id,Password,First_name,Last_name,Image,Address,Phone_number,Email_address,Sex,Age,Experience,profession,public_profile,Status")] DB_user dB_user) {
      string _FileName = "";
      string _path = "";
      // string ms = "Update account unsuccessful";
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
        // ms = "Update account successfully";
        dB_user.Image = _FileName;
        db.Entry(dB_user).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Profileuser", "Home", new { id = dB_user.User_id });
      }
      return View(dB_user);
    }
    public ActionResult deletecoment(string id) {
      Post_Detail detail = db.Post_Detail.Find(int.Parse(id));
      db.Post_Detail.Remove(detail);
      db.SaveChanges();
      return RedirectToAction("PostDetal", "Home", new { id = detail.NumericalOrder });
    }
    public ActionResult delete(string id, string idus) {
      int count = db.Post_Detail.Where(p => p.NumericalOrder.ToString() == id).Count();
      for (int i = 0; i < count; i++) {
        Post_Detail detail = db.Post_Detail.Where(p => p.NumericalOrder.ToString() == id).FirstOrDefault();
        db.Post_Detail.Remove(detail);
        db.SaveChanges();
      }
      return RedirectToAction("Profileuser", "Home", new { id = idus });
    }
    public ActionResult editpost(string id) {
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
    public ActionResult editpost([Bind(Include = "Post_id,Level,forid,Image,NumericalOrder,Topic_id,Email,TimePost,Content,Userpost")] Post_Detail post_Detail) {
      string _FileName = "";
      string _path = "";
      // string ms = "Update account unsuccessful";
      if (ModelState.IsValid) {
        HttpPostedFileBase file = Request.Files["image"];
        if (file.FileName != "") {
          string name = file.FileName;
          string tail = name.Substring(name.IndexOf("."));
          _FileName = post_Detail.Post_id + tail;
          _path = Path.Combine(Server.MapPath("~/image"), _FileName);
          file.SaveAs(_path);
        } else {
          _FileName = post_Detail.Image;
        }
        post_Detail.Image = _FileName;
        db.Entry(post_Detail).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Profileuser", "Home", new { id = post_Detail.Userpost });
      }
      ViewBag.Topic_id = new SelectList(db.Topics, "Topic_id", "TopicName", post_Detail.Topic_id);
      return View(post_Detail);
    }
    public string imguser(string id) {
      string img = "";
      DB_user dB_User = db.DB_user.Find(id);
      img = "/image/" + dB_User.Image;
      return img;
    }
    public string nameuser(string id) {
      string name = "";
      DB_user dB_User = db.DB_user.Find(id);
      name = dB_User.First_name + " " + dB_User.Last_name;
      return name;
    }
    public ActionResult Profileuser(string id) {
      ViewBag.Post = db.Post_Detail.ToList();
      ViewBag.user = db.DB_user.Find(id);

      return View();
    }
    public ActionResult certifications(string id) {
      List<Qualification> achievements = new List<Qualification>();
      foreach (Qualification av in db.Qualifications) {
        if (av.User_id == id) {
          achievements.Add(av);
        }
      }
      List<Achievement> achievements1 = new List<Achievement>();
      foreach (Achievement av in db.Achievements) {
        if (av.User_id == id) {
          achievements1.Add(av);
        }
      }
      List<Professional> achievements2 = new List<Professional>();
      foreach (Professional av in db.Professionals) {
        if (av.User_id == id) {
          achievements2.Add(av);
        }
      }
      ViewBag.Qualification = achievements;
      ViewBag.Achievement = achievements1;
      ViewBag.Professional = achievements2;
      return View();
    }
    public ActionResult Login() {
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login([Bind(Include = "User_id,Password")] DB_user dB_User) {
      TempData["us"] = dB_User.User_id;
      DB_user user = db.DB_user.Find(dB_User.User_id);
      ViewBag.message = "User ID or Password wrong!";
      if (user != null) {
        if (user.User_id.ToLower() == "admin" && user.Password == dB_User.Password) {
          changestatus(user, "Enable");
          Session["id"] = user.User_id;
          Session["name"] = user.First_name + " " + user.Last_name;
          Session["img"] = "/image/" + user.Image;
          return RedirectToAction("Index", "DB_user", new { id = user.User_id });
        } else if (user.Password == dB_User.Password) {
          changestatus(user, "Enable");
          Session["id"] = user.User_id;
          Session["name"] = user.First_name + " " + user.Last_name;
          Session["img"] = "/image/" + user.Image;

          return RedirectToAction("Index");
        } else {
          return RedirectToAction("Login");
        }
      } else {
        return RedirectToAction("Login");
      }
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
    public ActionResult Register() {
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Register([Bind(Include = "User_id,Password,First_name,Last_name,Image,Address,Phone_number,Email_address,Sex,Age,Experience,profession,public_profile,Status")] DB_user dB_user) {
      string _FileName = "";
      string _path = "";
      string ms = "Register account unsuccessful because account is exist";
      if (ModelState.IsValid) {

        if (checkedid(dB_user.User_id) == true) {
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
          ms = "Register account successfully";
          dB_user.Status = "Distable";
          dB_user.Image = _FileName;
          db.DB_user.Add(dB_user);
          db.SaveChanges();
          TempData["meseger"] = ms;
          return RedirectToAction("Index");
        } else {
          ms = "Register account unsuccessful because account is exist";
          TempData["meseger"] = ms;
          return RedirectToAction("Register");
        }
      }

      return View(dB_user);
    }
    private bool checkedid(string id) {
      bool check = true;
      DB_user user = db.DB_user.Find(id);
      if (user != null) {
        check = false;
      }
      return check;
    }
    public ActionResult PostDetal(string id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var postDetail = db.Post_Detail.Find(int.Parse(id));
      if (postDetail == null) {
        return HttpNotFound();
      }

      ViewBag.Postdetall = postDetail;
      ViewBag.Comment = db.Post_Detail.Where(pd => pd.NumericalOrder == postDetail.Post_id).ToList();

      return View();
    }
    [HttpPost]
    public ActionResult PostDetal(string conten, string iduser, int postid, int topic_id, string idusp) {
      // Random random = new Random();
      Post_Detail detal = new Post_Detail();
      // detal.Post_id = random.Next().ToString();
      detal.NumericalOrder = postid;
      detal.Topic_id = topic_id;
      detal.Content = conten;
      detal.forid = iduser;
      detal.Userpost = idusp;
      detal.Level = 1;
      detal.TimePost = DateTime.Now;
      db.Post_Detail.Add(detal);
      db.SaveChanges();
      return RedirectToAction("PostDetal");
    }
    public ActionResult Post() {
      ViewBag.Message = "Your application description page.";

      return View();
    }
    [HttpPost]
    public ActionResult Post([Bind(Include = "Post_id,Level,forid,Image,NumericalOrder,Topic_id,Email,TimePost,Content,Userpost")] Post_Detail post_Detail, string userid, string topicname) {
      string _FileName = "";
      string _path = "";
      HttpPostedFileBase file = Request.Files["image"];

      Topic topic = new Topic();
      //topic.Topic_id = id;
      topic.TopicName = topicname;
      db.Topics.Add(topic);
      try {
        db.SaveChanges();
      } catch (DbEntityValidationException e) {
        ViewBag.topicname = topicname;
        ViewBag.topicError = e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage;
        return View(post_Detail);
      }

      if (file.FileName != "") {
        string name = file.FileName;
        string tail = name.Substring(name.IndexOf("."));
        _FileName = topic.Topic_id.ToString() + tail;
        _path = Path.Combine(Server.MapPath("~/image"), _FileName);
        file.SaveAs(_path);
      }

      ModelState.Remove("Userpost");
      if (ModelState.IsValid) {
        Random random = new Random();

        post_Detail.NumericalOrder = random.Next();
        post_Detail.Topic_id = topic.Topic_id;
        post_Detail.Level = 0;
        //post_Detail.Post_id = id.ToString();
        post_Detail.TimePost = DateTime.Now;
        post_Detail.Userpost = userid;
        post_Detail.Image = _FileName;
        db.Post_Detail.Add(post_Detail);
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(post_Detail);
    }
    public string topicname(int id) {
      string name = "";
      Topic topic = db.Topics.Find(id);
      if (topic != null) {
        name = topic.TopicName;
      }
      return name;
    }

  }
}