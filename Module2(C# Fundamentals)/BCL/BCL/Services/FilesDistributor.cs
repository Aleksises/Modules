using BCL.Abstraction;
using BCL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Resource = BCL.Resources.Resource;

namespace BCL.Services
{
    public class FilesDistributor : IDistributor<FileModel>
    {
        private const int FileCheckTimoutMiliseconds = 1000;
        
        private readonly List<Rule> _rules;
        private readonly string _defaultFolder;

        private readonly ILogger _logger;

        public FilesDistributor(IEnumerable<Rule> rules, string defaultFolder, ILogger logger)
        {
            _rules = rules.ToList();
            _logger = logger;
            _defaultFolder = defaultFolder;
        }

        public async Task MoveAsync(FileModel item)
        {
            var fromPlace = item.FullName;
            foreach (Rule rule in _rules)
            {
                var match = Regex.Match(item.Name, rule.FilePattern);

                if (match.Success && match.Length == item.Name.Length)
                {
                    rule.MatchesCount++;
                    var toPlace = FormDestinationPath(item, rule);
                    
                    _logger.Log(Resource.RuleMatch);
                    await MoveFileAsync(fromPlace, toPlace);
                    _logger.Log(string.Format(Resource.FileMovedTemplate, item.FullName, toPlace));
                    
                    return;
                }
            }

            var destination = Path.Combine(_defaultFolder, item.Name);
            _logger.Log(Resource.RuleNoMatch);
            
            await MoveFileAsync(fromPlace, destination);
            _logger.Log(string.Format(Resource.FileMovedTemplate, item.FullName, destination));
        }

        private string FormDestinationPath(FileModel file, Rule rule)
        {
            var extension = Path.GetExtension(file.Name);
            var filename = Path.GetFileNameWithoutExtension(file.Name);
            var destination = new StringBuilder();
            destination.Append(Path.Combine(rule.DestinationFolder, filename));

            if (rule.IsDateAppended)
            {
                var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                dateTimeFormat.DateSeparator = ".";
                destination.Append($"{destination}_{DateTime.Now.ToLocalTime().ToString(dateTimeFormat.ShortDatePattern)}");
            }

            if (rule.IsOrderAppended)
            {
                destination.Append($"_{rule.MatchesCount}");
            }

            destination.Append(extension);
            return destination.ToString();
        }

        private async Task MoveFileAsync(string from, string to)
        {
            var dir = Path.GetDirectoryName(to);
            Directory.CreateDirectory(dir);
            var cannotAccessFile = true;
            do
            {
                try
                {
                    if (File.Exists(to))
                    {
                        File.Delete(to);
                    }
                    File.Move(from, to);
                    cannotAccessFile = false;
                }
                catch (FileNotFoundException)
                {
                    _logger.Log(Resource.FileNotExists);
                    break;
                }
                catch (IOException)
                {
                    await Task.Delay(FileCheckTimoutMiliseconds);
                }
            } while (cannotAccessFile);
        }
    }
}
