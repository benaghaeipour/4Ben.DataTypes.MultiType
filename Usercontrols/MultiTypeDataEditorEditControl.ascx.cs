using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace _4Ben.DataTypes.MultiType.Usercontrols
{
    public partial class MultiTypeDataEditorEditControl : System.Web.UI.UserControl
    {
        public Dictionary<string, MultiType> MultiTypes { get; set; }
        public int Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (var kvpMultiType in MultiTypes)
            {
                Dictionary<string, object> parameters;

                if (kvpMultiType.Value.PropertyType().hasDataEditorControls())
                {
                    parameters = kvpMultiType.Value.AdditionalProperties;
                    parameters.Add("Name", kvpMultiType.Value.Name);
                    parameters.Add("Alias", kvpMultiType.Value.Alias);
                    parameters.Add("Description", kvpMultiType.Value.Description);

                    pnlEditMultiType.Controls.Add(kvpMultiType.Value.PropertyType().DataEditorControls(null, parameters));
                }
            }
        }

        public void SetValues(XmlNode item)
        {
            foreach (XmlNode xmlMultiType in item)
            {
                if (MultiTypes.ContainsKey(xmlMultiType.Name))
                {
                    if (MultiTypes[xmlMultiType.Name].PropertyType().hasDataEditorControls())
                    {
                        MultiTypes[xmlMultiType.Name].PropertyType().LoadDataEditorControl(xmlMultiType);
                    }
                }
            }
        }

        public void ResetValues()
        {
            foreach (var multiType in MultiTypes.Values)
            {
                if (multiType.PropertyType().hasDataEditorControls())
                {
                    multiType.PropertyType().ResetDataEditorConrol();
                }
            }
        }

        public bool IsValid()
        {
            // check each multiType to see if their controls are valid
            foreach (MultiType multiType in MultiTypes.Values)
            {
                if (multiType.PropertyType().hasDataEditorControls())
                {
                    if (!multiType.PropertyType().IsValid())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public XmlNode Save(XmlNode itemNode)
        {
            if (IsValid())
            {
                XmlDocument doc = new XmlDocument();
                XmlNode newItemNode = doc.ImportNode(itemNode, true);

                foreach (MultiType multiType in MultiTypes.Values)
                {
                    var aliasNode = doc.CreateElement(multiType.Alias);
                    aliasNode.AppendChild(multiType.PropertyType().SaveDataEditorControl(ref doc));
                    newItemNode.AppendChild(aliasNode);
                }
                ResetValues();
                return newItemNode;
            }
            return null;
        }
    }
}