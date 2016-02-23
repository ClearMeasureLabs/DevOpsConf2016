using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

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
            var viewModel = new PlayerViewModel {Videos = GetVideoList()};
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(PlayerInputModel inputModel)
        {
            var viewModel = new PlayerViewModel { Videos = GetVideoList() };

            if (inputModel == null)
            {
                return View(viewModel);
            }

            viewModel.VideoId = inputModel.VideoId;

            var password = ConfigurationManager.AppSettings["player.Password"];
            if (!string.Equals(inputModel.Password, password))
            {
                viewModel.InvalidPassword = true;
                return View(viewModel);
            }

            var videoId = inputModel.VideoId;
            UpdateVideo(videoId);

            viewModel.UpdatedPlayer = true;
            viewModel.VideoId = null;

            return View(viewModel);
        }

        protected void UpdateVideo(string videoId)
        {
            if (videoId.StartsWith(@"http://youtu.be/") || videoId.StartsWith(@"https://youtu.be/"))
            {
                videoId = videoId.Replace(@"http://youtu.be/", string.Empty);
                videoId = videoId.Replace(@"https://youtu.be/", string.Empty);
            }

            var hubClients = GetHubClients();
            hubClients.All.PlayVideo(videoId);

            HttpContext.Cache["player.VideoId"] = videoId;
        }

        private static IHubConnectionContext<dynamic> GetHubClients()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<VideoHub>();
            return hubContext.Clients;
        }

        protected virtual VideoViewModel[] GetVideoList()
        {
            var videoList = HttpContext.Cache["player.Videos"] as VideoViewModel[];
            if (videoList != null)
            {
                return videoList;
            }

            var videoFile = HttpContext.Server.MapPath("~/App_Data/videos.txt");
            var lines = System.IO.File.ReadAllLines(videoFile);
            videoList = lines.Select(CreateVideoModel).Where(model => model != null).ToArray();
            HttpContext.Cache["player.Videos"] = videoList;

            return videoList;
        }

        protected virtual VideoViewModel CreateVideoModel(string videoLine)
        {
            if (string.IsNullOrWhiteSpace(videoLine))
            {
                return null;
            }

            var parts = videoLine.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                return null;
            }

            var video = new VideoViewModel
            {
                Title = parts[0].Trim(),
                Url = parts[1].Trim()
            };

            return video;
        }
    }
}
