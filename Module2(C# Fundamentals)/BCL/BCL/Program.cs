using BCL.Abstraction;
using BCL.Configuration;
using BCL.EventArgs;
using BCL.Models;
using BCL.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Resource = BCL.Resources.Resource;

namespace BCL
{
    public static class Program
    {
        private static List<string> _directories;
        private static List<Rule> _rules;
        private static IDistributor<FileModel> _distributor;

        [STAThread]
        public static void Main()
        {
            var config = ConfigurationManager.GetSection("fileSystemSection") as FileSystemWatcherConfigSection;

            if (config != null)
            {
                ReadConfiguration(config);
            }
            else
            {
                Console.Write(Resource.ConfigNotFound);
                return;
            }

            Console.WriteLine(config.Culture.DisplayName);

            var watcher = InitializeWatcher(config);

            watcher.Created += OnCreated;

            var source = new CancellationTokenSource();

            Console.CancelKeyPress += (o, e) =>
            {
                watcher.Created -= OnCreated;
                source.Cancel();
            };

            Task.Delay(TimeSpan.FromMilliseconds(-1), source.Token).Wait();
        }

        private static async void OnCreated(object sender, CreatedEventArgs<FileModel> args)
        {
            await _distributor.MoveAsync(args.CreatedItem);
        }

        private static IWatcher<FileModel> InitializeWatcher(FileSystemWatcherConfigSection config)
        {
            var logger = new Logger();
            _distributor = new FilesDistributor(_rules, config.Rules.DefaultDirectory, logger);
            var watcher = new FilesWatcher(_directories, logger);

            return watcher;
        }

        private static void ReadConfiguration(FileSystemWatcherConfigSection config)
        {
            _directories = new List<string>(config.Directories.Count);
            _rules = new List<Rule>();

            foreach (DirectoryElement directory in config.Directories)
            {
                _directories.Add(directory.Path);
            }

            foreach (RuleElement rule in config.Rules)
            {
                _rules.Add(new Rule
                {
                    FilePattern = rule.FilePattern,
                    DestinationFolder = rule.DestinationFolder,
                    IsDateAppended = rule.IsDateAppended,
                    IsOrderAppended = rule.IsOrderAppended
                });
            }

            CultureInfo.DefaultThreadCurrentCulture = config.Culture;
            CultureInfo.DefaultThreadCurrentUICulture = config.Culture;
            CultureInfo.CurrentUICulture = config.Culture;
            CultureInfo.CurrentCulture = config.Culture;
        }
    }
}
