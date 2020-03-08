using BCL.Abstraction;
using BCL.EventArgs;
using BCL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Resource = BCL.Resources.Resource;

namespace BCL.Services
{
    public class FilesWatcher : IWatcher<FileModel>
    {
        private readonly List<FileSystemWatcher> _fileSystemWatchers;
        
        private readonly ILogger _logger;

        public FilesWatcher(IEnumerable<string> directories, ILogger logger)
        {
            _logger = logger;
            _fileSystemWatchers = directories.Select(CreateWatcher).ToList();
        }

        public event EventHandler<CreatedEventArgs<FileModel>> Created;

        private void OnCreated(FileModel file)
        {
            Created?.Invoke(this, new CreatedEventArgs<FileModel> { CreatedItem = file });
        }

        private FileSystemWatcher CreateWatcher(string path)
        {
            var fileSystemWatcher = new FileSystemWatcher(path)
            {
                NotifyFilter = NotifyFilters.FileName,
                IncludeSubdirectories = false,
                EnableRaisingEvents = true
            };

            fileSystemWatcher.Created += (sender, fileSystemEventArgs) =>
            {
                _logger.Log(string.Format(Resource.FileFoundTemplate, fileSystemEventArgs.Name));
                OnCreated(new FileModel { FullName = fileSystemEventArgs.FullPath, Name = fileSystemEventArgs.Name });
            };

            return fileSystemWatcher;
        }
    }
}
