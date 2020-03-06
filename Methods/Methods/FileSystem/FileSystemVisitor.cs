using Methods.Enums;
using Methods.EventArgs;
using System;
using System.Collections.Generic;
using System.IO;
using StandardEventArgs = System.EventArgs;

namespace Methods.FileSystem
{
    public class FileSystemVisitor
    {
        //private class CurrentAction
        //{
        //    public ActionType Action { get; set; }

        //    public static CurrentAction ContinueSearch => new CurrentAction { Action = ActionType.ContinueSearch };
        //}

        private readonly DirectoryInfo _startDirectory;
        private readonly Predicate<FileSystemInfo> _filter;
        private readonly IFileSystemProcessingStrategy _fileSystemProcessingStrategy;

        public event EventHandler<StandardEventArgs> Start;
        public event EventHandler<StandardEventArgs> Finish;
        public event EventHandler<ItemFindedEventArgs<FileInfo>> FileFinded;
        public event EventHandler<ItemFindedEventArgs<FileInfo>> FilteredFileFinded;
        public event EventHandler<ItemFindedEventArgs<DirectoryInfo>> DirectoryFinded;
        public event EventHandler<ItemFindedEventArgs<DirectoryInfo>> FilteredDirectoryFinded;

        public FileSystemVisitor(
            string path,
            IFileSystemProcessingStrategy fileSystemProcessingStrategy,
            Predicate<FileSystemInfo> filter = null)
            : this(new DirectoryInfo(path), fileSystemProcessingStrategy, filter)
        {
        }

        public FileSystemVisitor(
            DirectoryInfo startDirectory,
            IFileSystemProcessingStrategy fileSystemProcessingStrategy,
            Predicate<FileSystemInfo> filter = null)
        {
            _startDirectory = startDirectory;
            _filter = filter;
            _fileSystemProcessingStrategy = fileSystemProcessingStrategy;
        }

        public IEnumerable<FileSystemInfo> GetFileSystemInfos()
        {
            OnEvent(Start, new StandardEventArgs());
            foreach (var fileSystemInfo in GetInfos(_startDirectory))
            {
                yield return fileSystemInfo;
            }
            OnEvent(Finish, new StandardEventArgs());
        }

        private IEnumerable<FileSystemInfo> GetInfos(DirectoryInfo directory)
        {
            foreach (var fileSystemInfo in directory.EnumerateFileSystemInfos())
            {
                var resultAction = ActionType.ContinueSearch;

                if (fileSystemInfo is FileInfo file)
                {
                    resultAction = ProcessFileInfo(file);
                }

                if (fileSystemInfo is DirectoryInfo dir)
                {
                    resultAction = ProcessDirectoryInfo(dir);
                    if (resultAction == ActionType.ContinueSearch)
                    {
                        yield return dir;
                        foreach (var innerInfo in GetInfos(dir))
                        {
                            yield return innerInfo;
                        }
                        continue;
                    }
                }

                if (resultAction == ActionType.StopSearch)
                {
                    yield break;
                }

                yield return fileSystemInfo;
            }
        }

        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
        {
            someEvent?.Invoke(this, args);
        }

        private ActionType ProcessFileInfo(FileInfo file)
        {
            return _fileSystemProcessingStrategy
                .ProcessItemFinded(file, _filter, FileFinded, FilteredFileFinded, OnEvent);
        }

        private ActionType ProcessDirectoryInfo(DirectoryInfo directory)
        {
            return _fileSystemProcessingStrategy
                .ProcessItemFinded(directory, _filter, DirectoryFinded, FilteredDirectoryFinded, OnEvent);
        }
    }
}
