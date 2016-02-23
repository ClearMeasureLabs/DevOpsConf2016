namespace DevOpsConf2016.Models.ViewModels
{
    public class PlayerViewModel
    {
        public string VideoId { get; set; }
        public bool InvalidPassword { get; set; }
        public bool UpdatedPlayer { get; set; }

        public VideoViewModel[] Videos { get; set; }
    }
}