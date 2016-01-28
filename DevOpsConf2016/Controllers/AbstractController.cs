using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevOpsConf2016.Contexts;
using DevOpsConf2016.Models;

namespace DevOpsConf2016.Controllers
{
    [Authorize(Roles = "Admin, Chair")]
    public class AbstractController : Controller
    {
        private DevOpsContext db = new DevOpsContext();

        // GET: Abstract
        public async Task<ActionResult> Index()
        {
            return View(await db.Abstracts.ToListAsync());
        }

        // GET: Abstract/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbstractSubmission abstractSubmission = await db.Abstracts.FindAsync(id);
            if (abstractSubmission == null)
            {
                return HttpNotFound();
            }
            return View(abstractSubmission);
        }

        [AllowAnonymous]
        public ActionResult Thankyou()
        {
            return View();
        }

        [AllowAnonymous]
        // GET: Abstract/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Abstract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,EMail,ContactNumber,Title,Description")] AbstractSubmission abstractSubmission)
        {
            if (ModelState.IsValid)
            {
                db.Abstracts.Add(abstractSubmission);
                await db.SaveChangesAsync();
                return RedirectToAction("Thankyou");
            }

            return View(abstractSubmission);
        }

        // GET: Abstract/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbstractSubmission abstractSubmission = await db.Abstracts.FindAsync(id);
            if (abstractSubmission == null)
            {
                return HttpNotFound();
            }
            return View(abstractSubmission);
        }

        // POST: Abstract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,EMail,ContactNumber,Title,Description")] AbstractSubmission abstractSubmission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abstractSubmission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(abstractSubmission);
        }

        // GET: Abstract/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbstractSubmission abstractSubmission = await db.Abstracts.FindAsync(id);
            if (abstractSubmission == null)
            {
                return HttpNotFound();
            }
            return View(abstractSubmission);
        }

        // POST: Abstract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AbstractSubmission abstractSubmission = await db.Abstracts.FindAsync(id);
            db.Abstracts.Remove(abstractSubmission);
            await db.SaveChangesAsync();
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
