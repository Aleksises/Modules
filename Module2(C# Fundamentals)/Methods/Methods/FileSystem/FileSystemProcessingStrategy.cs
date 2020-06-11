using Methods.Enums;
using Methods.EventArgs;
using System;
using System.IO;

namespace Methods.FileSystem
{
    public class FileSystemProcessingStrategy : IFileSystemProcessingStrategy
    {
        public ActionType ProcessItemFinded<TItemInfo>(TItemInfo itemInfo,
            Predicate<FileSystemInfo> filter,
            EventHandler<ItemFindedEventArgs<TItemInfo>> itemFinded,
            EventHandler<ItemFindedEventArgs<TItemInfo>> filteredItemFinded,
            Action<EventHandler<ItemFindedEventArgs<TItemInfo>>, ItemFindedEventArgs<TItemInfo>> action)
            where TItemInfo : FileSystemInfo
        {
            var itemFindedArgs = new ItemFindedEventArgs<TItemInfo>
            {
                FindedItem = itemInfo,
                ActionType = ActionType.ContinueSearch
            };

            action(itemFinded, itemFindedArgs);

            if (itemFindedArgs.ActionType != ActionType.ContinueSearch || filter == null)
            {
                return itemFindedArgs.ActionType;
            }

            if (filter(itemInfo))
            {
                itemFindedArgs = new ItemFindedEventArgs<TItemInfo>
                {
                    FindedItem = itemInfo,
                    ActionType = ActionType.ContinueSearch
                };

                action(filteredItemFinded, itemFindedArgs);

                return itemFindedArgs.ActionType;
            }

            return ActionType.SkipElement;
        }
    }
}
