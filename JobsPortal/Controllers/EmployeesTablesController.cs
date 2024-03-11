using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccessLayer;
using JobsPortal.Helper;
using JobsPortal.Models;

namespace JobsPortal.Controllers
{
    public class EmployeesTablesController : Controller
    {
        private JobsPortalDbEntities db = new JobsPortalDbEntities();

        // GET: EmployeesTables
        public ActionResult Index()
        {
            int uid = Convert.ToInt32(Session["UserID"]);
            var result = db.EmployeesTables.Where(u => u.UserId == uid).ToList();
            return View(result);
        }

        // GET: EmployeesTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeesTable employeesTable = db.EmployeesTables.Find(id);
            if (employeesTable == null)
            {
                return HttpNotFound();
            }
            return View(employeesTable);
        }

        // GET: EmployeesTables/Create
        public ActionResult Create(int? Id)
        {
            int uid = Convert.ToInt32(Session["UserID"]);
            var result = db.UserTables.Where(u => u.UserID == uid).ToList();
            ViewBag.UserId = new SelectList(result, "UserID", "UserName",uid);
            //ViewBag.UserId = new SelectList(result, "UserID", "UserName");
            var result2 = db.PostJobTables.Where(p => p.PostJobID == Id).ToList();
            ViewBag.PostJobID = new SelectList(result2, "PostJobID","JobTitle",Id);
            return View();
        }

        // POST: EmployeesTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,UserId,EmployeeName,DOB,Education,WorkExperience,Skills,EmailAddress,Gender,Photo,Qualification,PermanentAddress,JobReference,Description,Resume,PostJobID")] EmployeesTable employeesTable, HttpPostedFileBase photo1, HttpPostedFileBase resume1)
        {
            
                if (ModelState.IsValid)
                {
                    ViewBag.UserId = new SelectList(db.UserTables, "UserID", "UserName", employeesTable.UserId);
                ViewBag.PostJobID = new SelectList(db.PostJobTables, "PostJobID", "JobTitle", employeesTable.PostJobID);
                if (photo1 != null && photo1.ContentLength > 0)
                    {
                        var supportedTypes = new[] { ".png", ".jpg",".jpeg" };
                        
                        string photoname = Path.GetFileNameWithoutExtension(photo1.FileName);
                        string extension = Path.GetExtension(photo1.FileName);
                        string uniquePhotoname = $"{photoname}_{employeesTable.UserId}{extension}";
                        string photopath = Path.Combine(Server.MapPath("~/UploadImages/"), uniquePhotoname);
                    if (supportedTypes.Contains(extension))
                    {
                        photo1.SaveAs(photopath);

                        employeesTable.Photo = photopath;
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "please upload a valid file jpg or png or jpeg ");
                        
                        return View(employeesTable);
                    }
                    }
                var dob = DateTime.Parse(employeesTable.DOB.ToString());
                if (dob.AddYears(21) > DateTime.Now) {
                    ModelState.AddModelError("DOB", "Minimum age required is 21years");
                    return View(employeesTable);
                }
                    // Resume Upload
                    if (resume1 != null && resume1.ContentLength > 0)
                    {
                    var supportedTypes = new[] { ".docx", ".doc", ".pdf" };
                    string filename = Path.GetFileNameWithoutExtension(resume1.FileName);
                        string extension = Path.GetExtension(resume1.FileName);
                        string uniqueFilename = $"{filename}_{employeesTable.UserId}{extension}";
                        string filepath = Path.Combine(Server.MapPath("~/UploadResumes/"), uniqueFilename);
                    if (supportedTypes.Contains(extension))
                    {
                        resume1.SaveAs(filepath);


                        employeesTable.Resume = filepath;
                    }
                    else
                    {
                        ModelState.AddModelError("Resume", "please upload a valid format resume docx or doc or pdf ");

                        return View(employeesTable);
                    }
                    
                    }
                var result = db.EmployeesTables.Where(e => e.UserId == employeesTable.UserId && e.PostJobID == employeesTable.PostJobID).FirstOrDefault();
                if(result != null)
                {
                    ModelState.AddModelError("UserID", "You  had already appiled for this job");

                    return View(employeesTable);
                }
                else
                {
                    db.EmployeesTables.Add(employeesTable);
                    db.SaveChanges();
                    UserTable userdata = db.UserTables.AsNoTracking().Where(x => x.UserID == employeesTable.UserId).FirstOrDefault();
                    Email.Emailsend(userdata.EmailAddress, "Regarding Application", "Congrats  your  application has been submitted successfully", false);
                }
                   

                    return RedirectToAction("Index");
                }





                


                return View(employeesTable);
            
            
            }


        // GET: EmployeesTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeesTable employeesTable = db.EmployeesTables.Find(id);
            if (employeesTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserTables, "UserID", "UserName", employeesTable.UserId);
            //ViewBag.UserId = new SelectList(db.PostJobTables, "UserID", "UserName", employeesTable.UserId);
            ViewBag.PostJobID = new SelectList(db.PostJobTables, "PostJobID", "JobTitle", employeesTable.PostJobID);
            return View(employeesTable);
        }

        // POST: EmployeesTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,UserId,EmployeeName,DOB,Education,WorkExperience,Skills,EmailAddress,Gender,Qualification,PermanentAddress,JobReference,Description,PostJobID")] EmployeesTable employeesTable, HttpPostedFileBase photo1, HttpPostedFileBase resume1)
        {
            if (ModelState.IsValid)
            {
                if (photo1 != null && photo1.ContentLength > 0)
                {
                    string photoname = Path.GetFileNameWithoutExtension(photo1.FileName);
                    string extension = Path.GetExtension(photo1.FileName);
                    string uniquePhotoname = $"{photoname}_{employeesTable.UserId}{extension}";
                    string photopath = Path.Combine(Server.MapPath("~/UploadImages/"), uniquePhotoname);
                    photo1.SaveAs(photopath);

                    employeesTable.Photo = photopath;
                }
                else
                {
                    EmployeesTable employeesdata = db.EmployeesTables.AsNoTracking().Where(x => x.EmployeeID == employeesTable.EmployeeID).FirstOrDefault();

                    if (employeesdata == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        employeesTable.Photo=employeesdata.Photo;
                    }
                }

                // Resume Upload
                if (resume1 != null && resume1.ContentLength > 0)
                {
                    string filename = Path.GetFileNameWithoutExtension(resume1.FileName);
                    string extension = Path.GetExtension(resume1.FileName);
                    string uniqueFilename = $"{filename}_{employeesTable.UserId}{extension}";
                    string filepath = Path.Combine(Server.MapPath("~/UploadResumes/"), uniqueFilename);
                    resume1.SaveAs(filepath);

                    employeesTable.Resume = filepath;
                }
                else
                {
                    EmployeesTable employeesdata = db.EmployeesTables.AsNoTracking().Where(x => x.EmployeeID == employeesTable.EmployeeID).FirstOrDefault();
                    
                    if (employeesdata == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        employeesTable.Resume = employeesdata.Resume;
                    }
                }
                db.Entry(employeesTable).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserTables, "UserID", "UserName", employeesTable.UserId);
            ViewBag.PostJobID = new SelectList(db.PostJobTables, "PostJobID", "JobTitle", employeesTable.PostJobID);
            return View(employeesTable);
        }
       
        public FileResult Download(string path)
        {
            byte[] file = System.IO.File.ReadAllBytes(path);
            string filename = Path.GetFileName(path);
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            //return RedirectToAction("Index");
        }

        // GET: EmployeesTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeesTable employeesTable = db.EmployeesTables.Find(id);
            if (employeesTable == null)
            {
                return HttpNotFound();
            }
            return View(employeesTable);
        }

        // POST: EmployeesTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeesTable employeesTable = db.EmployeesTables.Find(id);
            db.EmployeesTables.Remove(employeesTable);
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
