using System.Configuration;

namespace BCL.Configuration
{
    internal class RuleElementCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultDir", IsRequired = true)]
        internal string DefaultDirectory => (string)this["defaultDir"];

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).FilePattern;
        }
    }
}
