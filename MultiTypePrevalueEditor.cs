using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

using _4Ben.DataTypes.MultiType.Helpers;
using _4Ben.DataTypes.MultiType.Helpers.PrevalueEditors;
using _4Ben.DataTypes.MultiType.Usercontrols;
using _4Ben.DataTypes.MultiType.PropertyTypes;

using umbraco.cms.businesslogic.datatype;

[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.Usercontrols.js.MultiTypePrevalueEditorControl.js", "text/js")]
[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.Usercontrols.css.MultiTypePrevalueEditorControl.css", "text/css", PerformSubstitution = true)]
[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.Usercontrols.css.MultiTypePrevalueEditorItemControl.css", "text/css", PerformSubstitution = true)]
namespace _4Ben.DataTypes.MultiType
{
    public class MultiTypePrevalueEditor : AbstractJsonPrevalueEditor
    {
        private MultiTypeOptions _options;
        private MultiTypePrevalueEditorControl multiTypePrevalueEditorControl;

        public MultiTypePrevalueEditor(BaseDataType dataType)
            : base(dataType, DBTypes.Ntext){}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Page.AddResourceToClientDependency(this.GetType(), "_4Ben.DataTypes.MultiType.Usercontrols.js.MultiTypePrevalueEditorControl.js", ClientDependency.Core.ClientDependencyType.Javascript, 100);
            this.Page.AddResourceToClientDependency(this.GetType(), "_4Ben.DataTypes.MultiType.Usercontrols.css.MultiTypePrevalueEditorControl.css", ClientDependency.Core.ClientDependencyType.Css, 100);
            this.Page.AddResourceToClientDependency(this.GetType(), "_4Ben.DataTypes.MultiType.Usercontrols.css.MultiTypePrevalueEditorItemControl.css", ClientDependency.Core.ClientDependencyType.Css, 100);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            MultiTypeOptions options = this.GetPreValueOptions<MultiTypeOptions>();
            if (options == null)
            {
                options = new MultiTypeOptions(true);
            }
            _options = options.GetOptions();

            multiTypePrevalueEditorControl = (MultiTypePrevalueEditorControl)Page.LoadControl("~/umbraco/plugins/MultiType4Ben/MultiTypePrevalueEditorControl.ascx");

            multiTypePrevalueEditorControl.Limit = _options.Limit;
            multiTypePrevalueEditorControl.MacroId = _options.MacroId;
            multiTypePrevalueEditorControl.MultiTypes = _options.MultiTypes;
            multiTypePrevalueEditorControl.DeletedMultiType += new MultiTypePrevalueEditorControl.DeleteMultiType(multiTypePrevalueEditorControl_DeletedMultiType);

            this.Controls.Add(multiTypePrevalueEditorControl);
        }

        void multiTypePrevalueEditorControl_DeletedMultiType()
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
                if (kvpPropertyType.Value.hasPrevalueControls() && !string.IsNullOrEmpty(kvpPropertyType.Value.PrevalueCSS()))
                {
                    this.Page.AddResourceToClientDependency(kvpPropertyType.Value.GetType(), kvpPropertyType.Value.PrevalueCSS(), ClientDependency.Core.ClientDependencyType.Css, 100);
                }
            }
        }

        public override void Save()
        {
            _options.Limit = multiTypePrevalueEditorControl.Limit;
            _options.MacroId = multiTypePrevalueEditorControl.MacroId;
            _options.MultiTypes = multiTypePrevalueEditorControl.Save();

            this.SaveAsJson(_options);
        }
    }
}