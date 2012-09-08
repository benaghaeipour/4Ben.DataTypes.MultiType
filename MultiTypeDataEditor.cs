using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml;

using umbraco;

using _4Ben.DataTypes.MultiType.Helpers;
using _4Ben.DataTypes.MultiType.Helpers.PrevalueEditors;
using _4Ben.DataTypes.MultiType.Usercontrols;
using _4Ben.DataTypes.MultiType.PropertyTypes;
using _4Ben.DataTypes.MultiType.Helpers.Data;
using umbraco.cms.businesslogic.propertytype;
using umbraco.BusinessLogic;

[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.Usercontrols.js.MultiTypeDataEditorControl.js", "text/js")]
[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.Usercontrols.css.MultiTypeDataEditorControl.css", "text/css", PerformSubstitution = true)]
[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.Usercontrols.css.MultiTypeDataEditorItemControl.css", "text/css", PerformSubstitution = true)]
namespace _4Ben.DataTypes.MultiType
{
    [ValidationProperty("IsValid")]
    public class MultiTypeDataEditor : Panel, umbraco.interfaces.IDataEditor
    {
        private umbraco.interfaces.IData _data;
        private MultiTypeOptions _options;

        private XmlNodeList _Items;
        private MultiTypeDataEditorControl multiTypeDataEditorControl;

        public MultiTypeDataEditor(umbraco.interfaces.IData data, MultiTypeOptions options)
        {
            _data = data;
            _options = options.GetOptions();
        }

        public Control Editor
        {
            get { return this; }
        }

        public bool ShowLabel
        {
            get { return true; }
        }

        public bool TreatAsRichTextEditor
        {
            get { return false; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(_data.Value.ToString());
            }
            catch
            {
                doc = createBaseXmlDocument();
            }

            _Items = doc.SelectNodes("//Items/Item");

            this.Page.AddResourceToClientDependency(this.GetType(), "_4Ben.DataTypes.MultiType.Usercontrols.js.MultiTypeDataEditorControl.js", ClientDependency.Core.ClientDependencyType.Javascript, 100);
            this.Page.AddResourceToClientDependency(this.GetType(), "_4Ben.DataTypes.MultiType.Usercontrols.css.MultiTypeDataEditorControl.css", ClientDependency.Core.ClientDependencyType.Css, 100);
            this.Page.AddResourceToClientDependency(this.GetType(), "_4Ben.DataTypes.MultiType.Usercontrols.css.MultiTypeDataEditorItemControl.css", ClientDependency.Core.ClientDependencyType.Css, 100);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            multiTypeDataEditorControl = (MultiTypeDataEditorControl)Page.LoadControl("~/umbraco/plugins/MultiType4Ben/MultiTypeDataEditorControl.ascx");

            multiTypeDataEditorControl.Limit = _options.Limit;
            multiTypeDataEditorControl.MacroId = _options.MacroId;
            multiTypeDataEditorControl.MultiTypes = _options.MultiTypes;
            multiTypeDataEditorControl.Items = _Items;
            multiTypeDataEditorControl.SaveDataEditor += new MultiTypeDataEditorControl.SaveDataEditorControl(multiTypeDataEditorControl_SaveDataEditor);

            this.Controls.Add(multiTypeDataEditorControl);
        }

        void multiTypeDataEditorControl_SaveDataEditor()
        {
            Save();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Dictionary<Guid, IPropertyType> objMultiTypeTypes = new Dictionary<Guid, IPropertyType>();

            foreach (MultiType objMultiType in _options.MultiTypes)
            {
                if (!objMultiTypeTypes.ContainsKey(objMultiType.TypeId))
                {
                    objMultiTypeTypes.Add(objMultiType.TypeId, objMultiType.PropertyType());
                }
            }

            foreach (var kvpPropertyType in objMultiTypeTypes)
            {
                if (kvpPropertyType.Value.hasDataEditorControls() && !string.IsNullOrEmpty(kvpPropertyType.Value.DataEditorCSS()))
                {
                    this.Page.AddResourceToClientDependency(kvpPropertyType.Value.GetType(), kvpPropertyType.Value.DataEditorCSS(), ClientDependency.Core.ClientDependencyType.Css, 100);
                }
            }
        }

        public void Save()
        {
            XmlDocument doc = createBaseXmlDocument();

            XmlNode root = doc.DocumentElement;
            XmlNode itemNode;

            foreach (var item in multiTypeDataEditorControl.Save())
            {
                itemNode = doc.ImportNode(item, true);
                root.AppendChild(itemNode);
            }
            
            this._data.Value = doc.InnerXml;
        }

        private static XmlDocument createBaseXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("Items");
            doc.AppendChild(root);
            return doc;
        }
        
        public string IsValid
        {
            get
            {
                return "Valid";
            }
        }
    }
}