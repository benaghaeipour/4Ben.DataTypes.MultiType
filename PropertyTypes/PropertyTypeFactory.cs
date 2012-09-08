using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using umbraco.BusinessLogic.Utils;

namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    public class PropertyTypeFactory
    {
        public static readonly Dictionary<Guid, IPropertyType> PropertyTypesList = new Dictionary<Guid, IPropertyType>();
        public static readonly Dictionary<Guid, string> PropertyTypes = new Dictionary<Guid, string>();

        static PropertyTypeFactory()
        {
            RegisterPropertyType();
        }

        private static void RegisterPropertyType()
        {
            var types = TypeFinder.FindClassesOfType<IPropertyType>(true);
            foreach (var t in types)
            {
                var typeInstance = Activator.CreateInstance(t) as IPropertyType;
                if (typeInstance != null)
                {
                    PropertyTypesList.Add(typeInstance.Id, typeInstance);
                    PropertyTypes.Add(typeInstance.Id, typeInstance.Name);
                }
            }
        }
    }
}