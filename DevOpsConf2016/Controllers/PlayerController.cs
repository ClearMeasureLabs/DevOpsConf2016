using System.Web.Mvc;
using System.Configuration;

using DevOpsConf2016.Hubs;
using DevOpsConf2016.Models.ViewModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DevOpsConf2016.Controllers
{
    [System.Web.Mvc.Authorize]
    public class PlayerController : Controller
    {
        public ActionResult Index()
        {
            return View(new PlayerViewModel());
        }

        [HttpPost]
        public ActionResult Index(PlayerInputModel inputModel)
        {
            if (inputModel == null)
            {
                return View(new PlayerViewModel());
            }

            var password = ConfigurationManager.AppSettings["player.Password"];
            if (!string.Equals(inputModel.Password, password))
            {
                return View(new PlayerViewModel { InvalidPassword = true, VideoId = inputModel.VideoId});
            }

            var videoId = inputModel.VideoId;
            UpdateVideo(videoId);

            return View(new PlayerViewModel { UpdatedPlayer = true });
        }

        protected void UpdateVideo(string videoId)
        {
            var hubClients = GetHubClients();
            hubClients.All.PlayVideo(videoId);

            HttpContext.Cache["player.VideoId"] = videoId;
        }

        private static IHubConnectionContext<dynamic> GetHubClients()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<VideoHub>();
            return hubContext.Clients;
        }
    }
}
