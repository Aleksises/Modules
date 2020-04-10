namespace WebDownloader.Interfaces
{
    public interface IWebDownloader
    {
        int Depth { get; set; }

        void LoadFromUrl(string url);
    }
}
