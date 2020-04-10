using CommandLine;
    using WebDownloader.Enums;

namespace ApplicationDemo.Configuration
{
    public class Options
    {
        [Option('t', "transitionPermission", Default = 1, HelpText = "Set type of cross domain transitions (1 - All, 2 - Current domain, 3 - Descendant urls only.")]
        public CrossDomainTransition CrossDomainTransition { get; set; }

        [Option('e', "availableExtensions", Default = null, HelpText = "List of extensions, example: \"pdf,css,js\".")]
        public string AvailableExtensions { get; set; }

        [Option('l', "deepLevel", Default = 2, HelpText = "Max deep level of site links analyze.")]
        public int DeepLevel { get; set; }

        [Option('v', "verbose", Default = true, HelpText = "Prints currently processing urls to standart output.")]
        public bool Verbose { get; set; }

        [Option('u', "url", Required = true, HelpText = "Start point for downloading.")]
        public string Url { get; set; }

        [Option('d', "directory", Required = true, HelpText = "Output directory path.")]
        public string OutputDirectoryPath { get; set; }
    }
}
