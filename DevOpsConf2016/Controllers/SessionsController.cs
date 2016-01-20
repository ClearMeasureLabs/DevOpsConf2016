using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using DevOpsConf2016.Contexts;
using DevOpsConf2016.Models;
using DevOpsConf2016.Models.ViewModels;

namespace DevOpsConf2016.Controllers
{
    [Authorize]
    public class SessionsController : ControllerBase
    {
        private DevOpsContext db = new DevOpsContext();

        public SessionsController()
        {
            Mapper.CreateMap<SessionInfo, SessionInfoVM>();
            Mapper.CreateMap<Speaker, SpeakerVM>();
            Mapper.CreateMap<Attendee, AttendeeVM>();
            Mapper.CreateMap<Login, LoginVM>();
            Mapper.CreateMap<SpeakerVM, Speaker>();
            Mapper.CreateMap<SessionInfoVM,SessionInfo>();
            Mapper.CreateMap<AttendeeVM, Attendee>();
            Mapper.CreateMap<LoginVM, Login>();
        }

        // GET: Sessions
        public async Task<ActionResult> Index()
        {
            var data = await db.Speakers.FirstOrDefaultAsync(x => x.Id == User.Id);
            var sessions = Mapper.Map<Speaker, SpeakerVM>(data);
            return View(sessions);
        }

        // GET: Sessions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionInfo sessionInfo = await db.Sessions.FindAsync(id);
            if (sessionInfo == null)
            {
                return HttpNotFound();
            }
            return View(sessionInfo);
        }

        // GET: Sessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,Abstract,Objectives,Level,Requirements,Accepted")] SessionInfo sessionInfo)
        {
            if (ModelState.IsValid)
            {
                db.Sessions.Add(sessionInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sessionInfo);
        }

        // GET: Sessions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionInfo sessionInfo = await db.Sessions.FindAsync(id);
            if (sessionInfo == null)
            {
                return HttpNotFound();
            }
            return View(sessionInfo);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Abstract,Objectives,Level,Requirements,Accepted")] SessionInfo sessionInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sessionInfo);
        }

        // GET: Sessions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionInfo sessionInfo = await db.Sessions.FindAsync(id);
            if (sessionInfo == null)
            {
                return HttpNotFound();
            }
            return View(sessionInfo);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SessionInfo sessionInfo = await db.Sessions.FindAsync(id);
            db.Sessions.Remove(sessionInfo);
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
