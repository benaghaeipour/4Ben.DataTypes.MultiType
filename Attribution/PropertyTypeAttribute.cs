using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4Ben.DataTypes.MultiType.Attribution
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PropertyTypeAttribute : Attribute
    {
        public PropertyTypeAttribute(string id, string name)
            : this(id, name, "Core")
        {
        }

        public PropertyTypeAttribute(string id, string name, string group)
        {
            Id = new Guid(id);
            Name = name;
            Group = Group;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Group { get; private set; }

    }
}