using System.Configuration;

namespace BCL.Configuration
{
    internal class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("filePattern", IsRequired = true, IsKey = true)]
        internal string FilePattern => (string)this["filePattern"];

        [ConfigurationProperty("destFolder", IsRequired = true)]
        internal string DestinationFolder => (string)this["destFolder"];

        [ConfigurationProperty("isOrderAppended", IsRequired = false, DefaultValue = false)]
        internal bool IsOrderAppended => (bool)this["isOrderAppended"];

        [ConfigurationProperty("isDateAppended", IsRequired = false, DefaultValue = false)]
        internal bool IsDateAppended => (bool)this["isDateAppended"];
    }
}
