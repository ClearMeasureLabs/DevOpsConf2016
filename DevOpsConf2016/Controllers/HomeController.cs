using System.Web.Mvc;
using System.Configuration;
using DevOpsConf2016.Models.ViewModels;

namespace DevOpsConf2016.Controllers
{    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = GetViewModel();
            return View(viewModel);
        }

        protected virtual HomeViewModel GetViewModel()
        {
            var videoId = HttpContext.Cache["player.VideoId"] as string;
            if (string.IsNullOrWhiteSpace(videoId))
            {
                videoId = ConfigurationManager.AppSettings["player.DefaultVideoId"];
            }

            return new HomeViewModel {VideoId = videoId};
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Sessions()
        {
            ViewBag.Message = "Sessions";

            return View();
        }

        public ActionResult ConferenceChairs()
        {
            ViewBag.Message = "Conference Chairs";

            return View();
        }
    }
}