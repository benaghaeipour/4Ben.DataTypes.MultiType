using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    public interface IPropertyType
    {
        Guid Id { get; }
        string Name { get; }
        string Group { get; }

        bool hasPrevalueControls();
        Control PrevalueControls(int MultiTypeId, Dictionary<string, object> properties);
        Dictionary<string, object> SavePrevalueControl(int MultiTypeId);
        string PrevalueCSS();

        bool hasDataEditorControls();
        Control DataEditorControls(XmlNode xml, Dictionary<string, object> properties);
        XmlNode SaveDataEditorControl(ref XmlDocument doc);
        void LoadDataEditorControl(XmlNode item);
        void ResetDataEditorConrol();
        string DataEditorCSS();

        bool IsValid(Dictionary<string, object> properties = null);
    }
}