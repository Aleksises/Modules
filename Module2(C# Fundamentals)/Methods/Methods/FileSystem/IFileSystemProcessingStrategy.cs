using Methods.Enums;
using Methods.EventArgs;
using System;
using System.IO;

namespace Methods.FileSystem
{
    public interface IFileSystemProcessingStrategy
    {
        ActionType ProcessItemFinded<TItemInfo>(
            TItemInfo itemInfo,
            Predicate<FileSystemInfo> filter,
            EventHandler<ItemFindedEventArgs<TItemInfo>> itemFinded,
            EventHandler<ItemFindedEventArgs<TItemInfo>> filteredItemFinded,
            Action<EventHandler<ItemFindedEventArgs<TItemInfo>>, ItemFindedEventArgs<TItemInfo>> action)
            where TItemInfo : FileSystemInfo;
    }
}
