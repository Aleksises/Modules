using ApplicationDemo.Configuration;
using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebDownloader.Constraints;
using WebDownloader.Interfaces;
using WebDownloader.Services;

namespace ApplicationDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new Options();
            var result = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(x =>
                {
                    options.Url = x.Url;
                    options.OutputDirectoryPath = x.OutputDirectoryPath;
                    options.DeepLevel = x.DeepLevel;
                    options.AvailableExtensions = x.AvailableExtensions;
                    options.CrossDomainTransition = x.CrossDomainTransition;
                    options.Verbose = x.Verbose;
                });

            var rootDirectory = new DirectoryInfo(options.OutputDirectoryPath);
            var contentSaver = new ContentKeeper(rootDirectory);
            var logger = new Logger(options.Verbose);
            var constraints = GetConstraintsFromOptions(options);
            var downloader = new Downloader(logger, contentSaver, constraints, options.DeepLevel);

            try
            {
                downloader.LoadFromUrl(options.Url);
            }
            catch (Exception ex)
            {
                logger.Log($"Error during site downloading: {ex.Message}");
            }
        }

        private static List<IConstraint> GetConstraintsFromOptions(Options options)
        {
            List<IConstraint> constraints = new List<IConstraint>();
            if (options.AvailableExtensions != null)
            {
                constraints.Add(new FileExtensionConstraint(options.AvailableExtensions.Split(',').Select(e => "." + e)));
            }

            constraints.Add(new CrossDomainConstraint(options.CrossDomainTransition, new Uri(options.Url)));

            return constraints;
        }
    }
}
