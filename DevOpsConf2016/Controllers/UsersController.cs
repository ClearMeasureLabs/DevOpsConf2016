using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DevOpsConf2016.Contexts;
using DevOpsConf2016.Models;
using DevOpsConf2016.Models.ViewModels;

namespace DevOpsConf2016.Controllers
{
    [Authorize(Roles = "Admin, Chair")]
    public class UsersController : Controller
    {
        private DevOpsContext db = null;

        public UsersController()
        {
            db = new DevOpsContext();
            Mapper.CreateMap<Login, LoginVM>();
            Mapper.CreateMap<Attendee, AttendeeVM>();
            Mapper.CreateMap<Speaker, SpeakerVM>();
            //Mapper.CreateMap<>()
        }
        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = db.Users.Include(l => l.UserInfo).Include(l => l.UserInfo.SpeakerInfo);
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = await db.Users.FindAsync(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Attendees, "Id", "FirstName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EMail,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                login.Id = Guid.NewGuid();
                db.Users.Add(login);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Attendees, "Id", "FirstName", login.Id);
            return View(login);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = await db.Users.FindAsync(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Attendees, "Id", "FirstName", login.Id);
            return View(login);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EMail,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Attendees, "Id", "FirstName", login.Id);
            return View(login);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = await db.Users.FindAsync(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Login login = await db.Users.FindAsync(id);
            db.Users.Remove(login);
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
