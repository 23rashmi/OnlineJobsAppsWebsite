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
    public class JobNatureTablesController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private JobsPortalDbEntities db = new JobsPortalDbEntities();

        // GET: JobNatureTables
        public ActionResult Index()
        {
            log.Info("Accessing JobNatureTables Index action.");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            return View(db.JobNatureTables.ToList());
        }

        // GET: JobNatureTables/Create
        public ActionResult Create()
        {
            log.Info("Accessing JobNatureTables Create action (GET).");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            return View(new JobNatureTable());
        }

        // POST: JobNatureTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobNatureID,JobNature")] JobNatureTable jobNatureTable)
        {
            log.Info("Submitting JobNatureTables Create action (POST).");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                log.Debug("ModelState is valid. Adding new JobNatureTable to database.");
                db.JobNatureTables.Add(jobNatureTable);
                db.SaveChanges();
                log.Info("Successfully added new JobNatureTable. Redirecting to Index action.");
                return RedirectToAction("Index");
            }
            log.Warn("ModelState is invalid. Returning to Create view.");
            return View(jobNatureTable);
        }

        // GET: JobNatureTables/Edit/5
        public ActionResult Edit(int? id)
        {
            log.Info($"Accessing JobNatureTables Edit action (GET) for ID: {id}.");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            if (id == null)
            {
                log.Error("JobNatureTable ID is null. Returning BadRequest.");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobNatureTable jobNatureTable = db.JobNatureTables.Find(id);
            if (jobNatureTable == null)
            {
                log.Error($"No JobNatureTable found with ID: {id}. Returning HttpNotFound.");
                return HttpNotFound();
            }
            return View(jobNatureTable);
        }

        // POST: JobNatureTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobNatureID,JobNature")] JobNatureTable jobNatureTable)
        {
            log.Info($"Submitting JobNatureTables Edit action (POST) for ID: {jobNatureTable.JobNatureID}.");
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                log.Debug("ModelState is valid. Modifying JobNatureTable in database.");
                db.Entry(jobNatureTable).State = EntityState.Modified;
                db.SaveChanges();
                log.Info($"Successfully modified JobNatureTable with ID: {jobNatureTable.JobNatureID}. Redirecting to Index action.");
                return RedirectToAction("Index");
            }
            log.Warn($"ModelState is invalid for JobNatureTable with ID: {jobNatureTable.JobNatureID}. Returning to Edit view.");
            return View(jobNatureTable);
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
