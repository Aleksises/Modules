using System;
using System.IO;

namespace WebDownloader.Interfaces
{
    public interface IContentKeeper
    {
        void SaveFile(Uri uri, Stream fileStream);

        void SaveHtmlDocument(Uri uri, string name, Stream documentStream);
    }
}
