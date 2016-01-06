using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using DevOpsConf2016.Contexts;
using DevOpsConf2016.Models;
using DevOpsConf2016.Models.ViewModels;
using Microsoft.Ajax.Utilities;

namespace DevOpsConf2016.Controllers
{
    [Route("/Attendee")]
    public class AttendeesController : Controller
    {
        private DevOpsContext db = new DevOpsContext();

        public AttendeesController()
        {
            Mapper.CreateMap<AttendeeVM, Attendee>();
            Mapper.CreateMap<SpeakerVM, Speaker>();
            Mapper.CreateMap<SessionInfoVM, SessionInfo>();
            Mapper.CreateMap<Attendee, AttendeeVM>();
            Mapper.CreateMap<Speaker, SpeakerVM>();
            Mapper.CreateMap<SessionInfo, SessionInfoVM>();
        }

        // GET: Attendees
        public async Task<ActionResult> Index()
        {
            var attendees = db.Attendees;
            var myList = await attendees.ToListAsync();
            var result = Mapper.Map<List<Attendee>, List<AttendeeVM>>(myList).ToList();
            return View(result);
        }

        // GET: Attendees/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendee attendee = await db.Attendees.FindAsync(id);
            if (attendee == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Attendee, AttendeeVM>(attendee));
        }

        // GET: Attendees/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Speakers, "AttendeeId", "TwitterHandle");
            return View(new AttendeeVM() {SpeakerInfo = new SpeakerVM()});
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Title,EMail")] AttendeeVM attendeeVM)
        {
            if (ModelState.IsValid)
            {
                var attendee = Mapper.Map<AttendeeVM, Attendee>(attendeeVM);
                attendee.Id = Guid.NewGuid();
                db.Attendees.Add(attendee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Speakers, "AttendeeId", "TwitterHandle", attendeeVM.Id);
            return View(attendeeVM);
        }

        // GET: Attendees/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendee attendee = await db.Attendees.FindAsync(id);
            if (attendee == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<Attendee, AttendeeVM>(attendee);
            ViewBag.Id = new SelectList(db.Speakers, "AttendeeId", "TwitterHandle", attendee.Id);
            return View(attendee);
        }

        // POST: Attendees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Title,EMail")] AttendeeVM attendeeVM)
        {
            if (ModelState.IsValid)
            {
                var attendee = Mapper.Map<AttendeeVM, Attendee>(attendeeVM);
                db.Entry(attendee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Speakers, "AttendeeId", "TwitterHandle", attendeeVM.Id);
            return View(attendeeVM);
        }

        // GET: Attendees/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendee attendee = await db.Attendees.FindAsync(id);
            if (attendee == null)
            {
                return HttpNotFound();
            }
            return View(attendee);
        }

        // POST: Attendees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Attendee attendee = await db.Attendees.FindAsync(id);
            db.Attendees.Remove(attendee);
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
