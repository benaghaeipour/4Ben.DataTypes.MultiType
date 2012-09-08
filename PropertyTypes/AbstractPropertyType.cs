using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    public abstract class AbstractPropertyType : IPropertyType
    {
        protected AbstractPropertyType()
        {
            var propertyTypeAttributes = GetType().GetCustomAttributes(typeof(PropertyTypeAttribute), true).OfType<PropertyTypeAttribute>();

            if (!propertyTypeAttributes.Any())
                throw new InvalidOperationException(String.Format("The PropertyType of type {0} is missing the {1} attribute", GetType().FullName, typeof(PropertyTypeAttribute).FullName));

            var attr = propertyTypeAttributes.First();
            
            Id = attr.Id;
            Name = attr.Name;
            Group = attr.Group;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Group { get; private set; }

        public virtual bool hasPrevalueControls() { return false; }
        public abstract Control PrevalueControls(int MultiTypeId, Dictionary<string, object> properties);
        public abstract Dictionary<string, object> SavePrevalueControl(int MultiTypeId);
        public abstract string PrevalueCSS();

        public virtual bool hasDataEditorControls() { return false; }
        public abstract Control DataEditorControls(XmlNode xml, Dictionary<string, object> properties);
        public abstract XmlNode SaveDataEditorControl(ref XmlDocument doc);
        public abstract void LoadDataEditorControl(XmlNode item);
        public abstract void ResetDataEditorConrol();
        public abstract string DataEditorCSS();

        public virtual bool IsValid(Dictionary<string, object> properties = null)
        {
            return true;
        }
        
    }
}