using System;
using System.Resources;
using System.ComponentModel;

namespace Bat.Core
{
    public class LocalizeDescriptionAttribute : DescriptionAttribute
    {
        readonly string _resourceKey;
        readonly ResourceManager _resource;

        public LocalizeDescriptionAttribute(string Name, Type ResourceType)
        {
            _resourceKey = Name;
            _resource = new ResourceManager(ResourceType);
        }

        public override string Description
        {
            get
            {
                string displayName = _resource.GetString(_resourceKey);
                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", _resourceKey)
                    : displayName;
            }
        }
    }
}
