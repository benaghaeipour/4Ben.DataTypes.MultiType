using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using umbraco.cms.businesslogic.macro;

namespace _4Ben.DataTypes.MultiType.Usercontrols
{
    public partial class MultiTypePrevalueEditorControl : System.Web.UI.UserControl
    {
        private int _limit;
        private int _macroId;

        public delegate void DeleteMultiType();
        public event DeleteMultiType DeletedMultiType;

        private Dictionary<int, MultiTypePrevalueEditorItemControl> multiTypePrevalueEditorItemControls;

        public int Limit
        {
            get
            {
                return int.TryParse(txtLimit.Text, out _limit) ? _limit : 0;
            }
            set
            {
                _limit = value;
            }
        }
        public int MacroId
        {
            get
            {
                return int.TryParse(ddlMacro.SelectedValue, out _macroId) ? _macroId : 0;
            }
            set
            {
                _macroId = value;
            }
        }

        public List<MultiType> MultiTypes
        {
            get {
                var newMultiTypesList = new Dictionary<int, MultiType>();

                foreach (var kvpMultiTypeControl in multiTypePrevalueEditorItemControls)
                {
                    newMultiTypesList.Add(kvpMultiTypeControl.Key, kvpMultiTypeControl.Value.Save());
                }

                if (newMultiType.isValid())
                {

                    var multiType = newMultiType.Save();

                    multiType.Id = NewId();
                    multiType.SortId = NewSortId();

                    newMultiTypesList.Add(multiType.Id, multiType);

                    NewMultiTypePrevalueEditorItemControl(multiType.Id, multiType);

                    newMultiType.Reset();
                }

                return newMultiTypesList.Values.ToList();
            }
            set
            {
                foreach (MultiType objMultiType in value)
                {
                    NewMultiTypePrevalueEditorItemControl(objMultiType.Id, objMultiType);
                }
            }
        }

        public MultiTypePrevalueEditorControl()
        {
            multiTypePrevalueEditorItemControls = new Dictionary<int, MultiTypePrevalueEditorItemControl>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var objMacros = Macro.GetAll();
            var objMacrosDS = new Dictionary<int, string>();

            foreach (Macro objMacro in objMacros.OrderBy(macro => macro.Name))
            {
                objMacrosDS.Add(objMacro.Id, objMacro.Name);
            }

            ddlMacro.DataSource = objMacrosDS;
            ddlMacro.DataValueField = "Key";
            ddlMacro.DataTextField = "Value";
            ddlMacro.DataBind();

            newMultiType.CSSClass("newFields");
            
            if (!Page.IsPostBack)
            {
                ddlMacro.SelectedValue = _macroId.ToString();
                txtLimit.Text = _limit.ToString();
            }
        }

        public List<MultiType> Save()
        {
            return MultiTypes;
        }

        private void NewMultiTypePrevalueEditorItemControl(int id, MultiType MultiType)
        {
            var multiTypeEdit = (MultiTypePrevalueEditorItemControl)Page.LoadControl("~/umbraco/plugins/MultiType4Ben/MultiTypePrevalueEditorItemControl.ascx");

            multiTypeEdit.MultiType = MultiType;
            multiTypeEdit.CSSClass("editFields");

            multiTypeEdit.DeleteClicked += Delete;

            multiTypePrevalueEditorItemControls.Add(id, multiTypeEdit);
            pnlEditProperties.Controls.Add(multiTypeEdit);
        }

        private int NewId(int y = 0)
        {
            var x = y;
            if (multiTypePrevalueEditorItemControls.Count > 0)
            {
                foreach (var id in multiTypePrevalueEditorItemControls.Keys)
                {
                    if (x == id)
                    {
                        x++;
                        return NewId(x);
                    }
                }
            }
            return x;
        }

        private int NewSortId(int y = 0)
        {
            var x = y;
            if (multiTypePrevalueEditorItemControls.Count > 0)
            {
                foreach (var kvpMultiTypeControl in multiTypePrevalueEditorItemControls.Values)
                {
                    if (x == kvpMultiTypeControl.MultiType.SortId)
                    {
                        x++;
                        NewSortId(x);
                    }
                }
            }
            return x;
        }

        private void Delete(int MultiTypeId)
        {
            if (multiTypePrevalueEditorItemControls.ContainsKey(MultiTypeId))
            {
                multiTypePrevalueEditorItemControls.Remove(MultiTypeId);
                if (DeletedMultiType != null)
                {
                    DeletedMultiType();
                }
            }
        }
    }
}