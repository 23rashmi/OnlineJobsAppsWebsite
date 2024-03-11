using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccessLayer;
using log4net;

namespace JobsPortal.Controllers
{
    public class JobCategoryTablesController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private JobsPortalDbEntities db = new JobsPortalDbEntities();

        // GET: JobCategoryTables
        public ActionResult Index()
        {
            log.Info("Accessing JobCategoryTables Index action.");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            return View(db.JobCategoryTables.ToList());
        }

        // GET: JobCategoryTables/Create
        public ActionResult Create()
        {
            log.Info("Accessing JobCategoryTables Create action (GET).");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            return View(new JobCategoryTable());
        }

        // POST: JobCategoryTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobCategoryID,JobCategory,Description")] JobCategoryTable jobCategoryTable)
        {
            log.Info("Submitting JobCategoryTables Create action (POST).");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                log.Debug("ModelState is valid. Adding new JobCategoryTable to database.");
                db.JobCategoryTables.Add(jobCategoryTable);
                db.SaveChanges();
                log.Info("Successfully added new JobCategoryTable. Redirecting to Index action.");
                return RedirectToAction("Index");
            }
            log.Warn("ModelState is invalid. Returning to Create view.");
            return View(jobCategoryTable);
        }

        // GET: JobCategoryTables/Edit/5
        public ActionResult Edit(int? id)
        {
            log.Info($"Accessing JobCategoryTables Edit action (GET) for ID: {id}.");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            if (id == null)
            {
                log.Error("JobCategoryTable ID is null. Returning BadRequest.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCategoryTable jobCategoryTable = db.JobCategoryTables.Find(id);
            if (jobCategoryTable == null)
            {
                log.Error($"No JobCategoryTable found with ID: {id}. Returning HttpNotFound.");
                return HttpNotFound();
            }
            return View(jobCategoryTable);
        }

        // POST: JobCategoryTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobCategoryID,JobCategory,Description")] JobCategoryTable jobCategoryTable)
        {
            log.Info($"Submitting JobCategoryTables Edit action (POST) for ID: {jobCategoryTable.JobCategoryID}.");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                log.Warn("UserTypeID session is empty or null. Redirecting to User Login action.");
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                log.Debug("ModelState is valid. Modifying JobCategoryTable in database.");
                db.Entry(jobCategoryTable).State = EntityState.Modified;
                db.SaveChanges();
                log.Info($"Successfully modified JobCategoryTable with ID: {jobCategoryTable.JobCategoryID}. Redirecting to Index action.");
                return RedirectToAction("Index");
            }
            log.Warn($"ModelState is invalid for JobCategoryTable with ID: {jobCategoryTable.JobCategoryID}. Returning to Edit view.");
            return View(jobCategoryTable);
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
