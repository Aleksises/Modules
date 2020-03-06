using System.Configuration;
using System.Globalization;

namespace BCL.Configuration
{
    internal class FileSystemWatcherConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("culture", DefaultValue = "en-EN", IsRequired = false)]
        internal CultureInfo Culture => (CultureInfo)this["culture"];

        [ConfigurationCollection(typeof(DirectoryElement), AddItemName = "directory")]
        [ConfigurationProperty("directories", IsRequired = false)]
        internal DirectoryElementCollection Directories => (DirectoryElementCollection)this["directories"];

        [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
        [ConfigurationProperty("rules", IsRequired = true)]
        internal RuleElementCollection Rules => (RuleElementCollection)this["rules"];
    }
}
