using Methods.Enums;
using System.IO;

namespace Methods.EventArgs
{
    public class ItemFindedEventArgs<T> : System.EventArgs
        where T : FileSystemInfo
    {
        public T FindedItem { get; set; }

        public ActionType ActionType { get; set; }
    }
}
