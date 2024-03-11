using DatabaseAccessLayer;
using JobsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace JobsPortal.Controllers
{
    public class JobController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(JobController));
        private JobsPortalDbEntities db = new JobsPortalDbEntities();
        // GET: Job
        public ActionResult PostJob()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var job = new PostJobMV();
            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    "0");
            ViewBag.JobNatureID = new SelectList(
                                    db.JobNatureTables.ToList(),
                                    "JobNatureID",
                                    "JobNature",
                                    "0");
            return View(job);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJob(PostJobMV postJobMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            int userId = 0;
            int companyId = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userId);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyId);
            postJobMV.UserID = userId;
            postJobMV.CompanyID = companyId;

            if(ModelState.IsValid)
            {
                try
                {
                    var post = new PostJobTable();
                    post.UserID = postJobMV.UserID;
                    post.CompanyID = postJobMV.CompanyID;
                    post.JobCategoryID = postJobMV.JobCategoryID;
                    post.JobTitle = postJobMV.JobTitle;
                    post.JobDescription = postJobMV.JobDescription;
                    post.MinSalary = postJobMV.MinSalary;
                    post.MaxSalary = postJobMV.MaxSalary;
                    post.Location = postJobMV.Location;
                    post.Vacancy = postJobMV.Vacancy;
                    post.JobNatureID = postJobMV.JobNatureID;
                    post.PostDate = DateTime.Now;
                    post.ApplicationDeadline = postJobMV.ApplicationDeadline;
                    post.LastDate = postJobMV.ApplicationDeadline;
                    post.JobStatusID = 1;
                    //post.WebUrl = postJobMV.WebUrl;
                    db.PostJobTables.Add(post);
                    db.SaveChanges();
                    log.Info($"PostJob - Successfully posted job by UserId: {userId} and CompanyID: {companyId}");
                    return RedirectToAction("CompanyJobsList");
                }
                catch (Exception ex)
                {

                    log.Error($"PostJob - Error while posting job by UserID: {userId}. Exception: {ex.Message}", ex);
                }
            }
            else
            {
                log.Warn("PostJob - ModelState is invalid.");
            }

            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    "0");
            ViewBag.JobNatureID = new SelectList(
                                    db.JobNatureTables.ToList(),
                                    "JobNatureID",
                                    "JobNature",
                                    "0");
            var allPost = db.PostJobTables.Where(c => c.CompanyID == companyId && c.UserID == userId).ToList();
           
            return View(allPost);
            return RedirectToAction("CompanyJobsList");
        }

        //This is for postjob after page
        public ActionResult CompanyJobsList()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            int userId = 0;
            int companyId = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userId);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyId);
            var allPost = db.PostJobTables.Where(c=>c.CompanyID==companyId && c.UserID==userId).ToList();
            return View(allPost);
        }

        //This is for admin allcompany post page
        public ActionResult AllCompanyPendingJobs()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            int userId = 0;
            int companyId = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userId);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyId);
            var allPost = db.PostJobTables.ToList();
            if (allPost.Count > 0)
            {
                allPost = allPost.OrderByDescending(o => o.PostJobID).ToList();
            }
            return View(allPost);
        }

        public ActionResult AddJobReq(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var details = db.JobRequirementDetailsTables.Where(j=>j.PostJobID == id).ToList();
            if (details.Count() > 0)
            {
                details = details.OrderBy(r => r.JobRequirementID).ToList();
            }
            var requirements = new JobReqMV();
            requirements.Details = details;
            requirements.PostJobID = (int)id;

            ViewBag.JobRequirementID = new SelectList(db.JobRequirementsTables.ToList(), "JobRequirementID", "JobRequirement", "0");
            return View(requirements);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddJobReq(JobReqMV jobReqMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            try
            {
                var requirements = new JobRequirementDetailsTable();
                requirements.JobRequirementID = jobReqMV.JobRequirementID;
                requirements.JobRequirementDetails = jobReqMV.JobRequirementDetails;
                requirements.PostJobID = jobReqMV.PostJobID;
                db.JobRequirementDetailsTables.Add(requirements);
                db.SaveChanges();
                return RedirectToAction("AddJobReq", new { id = requirements.PostJobID });
            }
            catch (Exception ex)
            {
                var details = db.JobRequirementDetailsTables.Where(j => j.PostJobID == jobReqMV.PostJobID).ToList();
                if (details.Count() > 0)
                {
                    details = details.OrderBy(r => r.JobRequirementID).ToList();
                }
                jobReqMV.Details = details;
                ModelState.AddModelError("JobRequirementID", "Required*");
            }
            ViewBag.JobRequirementID = new SelectList(db.JobRequirementsTables.ToList(), "JobRequirementID", "JobRequirement", jobReqMV.JobRequirementID);
            return View(jobReqMV);
        }

        public ActionResult DeleteReq(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var jobPostId = db.JobRequirementDetailsTables.Find(id).PostJobID;
            var requirements = db.JobRequirementDetailsTables.Find(id);
            db.Entry(requirements).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            log.Info($"DeleteReq - Successfully deleted job requirement with ID: {id}");
            return RedirectToAction("AddJobReq", new { id = jobPostId });
        }

        public ActionResult DeleteJobPost(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            // Find the job post
            var jobPost = db.PostJobTables.Find(id);

            // Check if the job post exists
            if (jobPost == null)
            {
                return HttpNotFound(); // or handle the case where the job post is not found
            }

            // Delete related records in JobRequirementDetailsTables
            var relatedDetails = db.JobRequirementDetailsTables.Where(d => d.PostJobID == id).ToList();
            foreach (var detail in relatedDetails)
            {
                db.JobRequirementDetailsTables.Remove(detail);
            }

            // Save changes to remove related details
            db.SaveChanges();

            // Delete the job post
            db.PostJobTables.Remove(jobPost);

            // Save changes to remove the job post
            db.SaveChanges();

            return RedirectToAction("CompanyJobsList");
        }

        public ActionResult ApprovedPost(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var jobPost = db.PostJobTables.Find(id);
            jobPost.JobStatusID = 2;
            db.Entry(jobPost).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllCompanyPendingJobs");
        }

        public ActionResult CancelledPost(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var jobPost = db.PostJobTables.Find(id);
            jobPost.JobStatusID = 3;
            db.Entry(jobPost).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllCompanyPendingJobs");
        }

        public ActionResult JobDetails(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var getpostJob = db.PostJobTables.Find(id);
            var postjob = new PostJobDetailMV();
            postjob.PostJobID = getpostJob.PostJobID;
            postjob.Company = getpostJob.CompanyTable.CompanyName;
            postjob.JobCategory = getpostJob.JobCategoryTable.JobCategory;
            postjob.JobTitle = getpostJob.JobTitle;
            postjob.JobDescription = getpostJob.JobDescription;
            postjob.MinSalary = getpostJob.MinSalary;
            postjob.MaxSalary = getpostJob.MaxSalary;
            postjob.Location = getpostJob.Location;
            postjob.Vacancy = getpostJob.Vacancy;
            postjob.JobNature = getpostJob.JobNatureTable.JobNature;
            postjob.PostDate = getpostJob.PostDate;
            postjob.ApplicationDeadline = getpostJob.ApplicationDeadline;
            //postjob.WebUrl = getpostJob.WebUrl;

            getpostJob.JobRequirementDetailsTables = getpostJob.JobRequirementDetailsTables.OrderBy(d => d.JobRequirementID).ToList();
            int jobrequirementid = 0;
            var jobreq = new JobRequirementsMV();
            foreach(var detail in getpostJob.JobRequirementDetailsTables)
            {
                var jobreqdetails = new JobRequirementDetailsMV();
                if (jobrequirementid == 0)
                {
                    jobreq.JobRequirementID = detail.JobRequirementID;
                    jobreq.JobRequirement = detail.JobRequirementsTable.JobRequirement;
                    jobreqdetails.JobRequirementID = detail.JobRequirementID;
                    jobreqdetails.JobRequirementDetails = detail.JobRequirementDetails;
                    jobreq.Details.Add(jobreqdetails);
                    jobrequirementid = detail.JobRequirementID;
                } 
                else if(jobrequirementid == detail.JobRequirementID)
                {
                    jobreqdetails.JobRequirementID = detail.JobRequirementID;
                    jobreqdetails.JobRequirementDetails = detail.JobRequirementDetails;
                    jobreq.Details.Add(jobreqdetails);
                    jobrequirementid = detail.JobRequirementID;
                }
                else if (jobrequirementid != detail.JobRequirementID)
                {
                    postjob.Requirements.Add(jobreq);
                    jobreq = new JobRequirementsMV();
                    jobreq.JobRequirementID = detail.JobRequirementID;
                    jobreq.JobRequirement = detail.JobRequirementsTable.JobRequirement;
                    jobreqdetails.JobRequirementID = detail.JobRequirementID;
                    jobreqdetails.JobRequirementDetails = detail.JobRequirementDetails;
                    jobreq.Details.Add(jobreqdetails);
                    jobrequirementid = detail.JobRequirementID;
                }
            }
            postjob.Requirements.Add(jobreq);
            return View(postjob);
        }

        public ActionResult FilterJob()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var obj = new FilterJobMV();
            var date = DateTime.Now;
            var result = db.PostJobTables.Where(r => r.ApplicationDeadline >= date && r.JobStatusID==2).ToList();
            obj.Result = result;
            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    "0");
            ViewBag.JobNatureID = new SelectList(
                                    db.JobNatureTables.ToList(),
                                    "JobNatureID",
                                    "JobNature",
                                    "0");
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FilterJob(FilterJobMV filterJobMV)
        {
            var date = DateTime.Now;
            var result = db.PostJobTables.Where(r => r.ApplicationDeadline >= date && r.JobStatusID == 2 && (r.JobCategoryID == filterJobMV.JobCategoryID && r.JobNatureID == filterJobMV.JobNatureID)).ToList();
            filterJobMV.Result = result;
            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    filterJobMV.JobCategoryID);
            ViewBag.JobNatureID = new SelectList(
                                    db.JobNatureTables.ToList(),
                                    "JobNatureID",
                                    "JobNature",
                                    filterJobMV.JobNatureID);
            return View(filterJobMV);
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