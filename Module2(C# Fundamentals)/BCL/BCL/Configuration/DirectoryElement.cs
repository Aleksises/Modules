using System.Configuration;

namespace BCL.Configuration
{
    internal class DirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true, IsKey = true)]
        internal string Path => (string)this["path"];
    }
}
