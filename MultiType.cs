using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _4Ben.DataTypes.MultiType.PropertyTypes;
using System.Xml;

namespace _4Ben.DataTypes.MultiType
{
    public class MultiType
    {
        public int Id { get; set; }
        public int SortId { get; set; }

        public string Name { get; set; }
        public string Alias { get; set; }
        public Guid TypeId { get; set; }
        private IPropertyType _PropertyType { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> AdditionalProperties { get; set; }

        public MultiType(){
            AdditionalProperties = new Dictionary<string, object>();
        }

        public void SetPropertyType()
        {
            var property = PropertyTypeFactory.PropertyTypesList.Where(x => x.Key == TypeId).Select(x => x.Value).FirstOrDefault();
            _PropertyType = Activator.CreateInstance(property.GetType()) as IPropertyType;
        }

        public IPropertyType PropertyType()
        {
            if (_PropertyType == null)
            {
                SetPropertyType();
            }
            return _PropertyType;
        }
    }
}