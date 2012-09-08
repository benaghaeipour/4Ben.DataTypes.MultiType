using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using _4Ben.DataTypes.MultiType.PropertyTypes;

namespace _4Ben.DataTypes.MultiType.Usercontrols
{
    public partial class MultiTypePrevalueEditorItemControl : System.Web.UI.UserControl
    {
        public delegate void DeleteMultiType(int MultiTypeId);
        public event DeleteMultiType DeleteClicked;

        private MultiType _MultiType;

        public MultiType MultiType {
            get
            {
                var newMultiType = new MultiType();
                if (_MultiType != null)
                {
                    newMultiType.Id = _MultiType.Id;
                }
                newMultiType.SortId = int.Parse(hdnSortId.Value);
                newMultiType.Name = txtName.Text;
                newMultiType.Alias = txtAlias.Text;
                newMultiType.Description = txtDescription.Text;

                Guid guidType;
                newMultiType.TypeId = Guid.TryParse(ddlType.SelectedValue, out guidType) ? guidType : Guid.Empty;

                if (hdnId.Value != "-1")
                {
                    if (newMultiType.TypeId != _MultiType.TypeId)
                    {
                        if (newMultiType.PropertyType() != null && newMultiType.PropertyType().hasPrevalueControls())
                        {
                            pnlAdditionalProperties.Controls.Clear();
                            newMultiType.AdditionalProperties = null;

                            pnlAdditionalProperties.Controls.Add(newMultiType.PropertyType().PrevalueControls(newMultiType.Id, newMultiType.AdditionalProperties));
                            pnlAdditionalProperties.Visible = true;
                        }
                        else
                        {
                            pnlAdditionalProperties.Visible = false;
                        }
                    }
                    else
                    {
                        if (_MultiType.PropertyType() != null && _MultiType.PropertyType().hasPrevalueControls())
                        {
                            newMultiType.AdditionalProperties = _MultiType.PropertyType().SavePrevalueControl(newMultiType.Id);
                        }
                    }
                }
            
                return newMultiType;
            }
            set
            {
                if (value != null)
                {
                    _MultiType = value;

                    hdnId.Value = value.Id.ToString();
                    hdnSortId.Value = value.SortId.ToString();
                    txtName.Text = value.Name;
                    txtAlias.Text = value.Alias;
                    txtDescription.Text = value.Description;
                    ddlType.SelectedValue = value.TypeId.ToString();

                    if (value.Id != -1)
                    {
                        litHeader.Text = value.Name + " (" + value.Alias + ") " + value.PropertyType().Name;
                        ibtnDelete.Visible = true;
                    }
                    if (value.PropertyType() != null && value.PropertyType().hasPrevalueControls())
                    {
                        Dictionary<string, object> properties = null;
                        if (!Page.IsPostBack)
                        {
                            properties = value.AdditionalProperties;
                        }
                        pnlAdditionalProperties.Controls.Add(value.PropertyType().PrevalueControls(value.Id, properties));
                        pnlAdditionalProperties.Visible = true;
                    }
                    else
                    {
                        pnlAdditionalProperties.Visible = false;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ddlType.DataSource = _4Ben.DataTypes.MultiType.PropertyTypes.PropertyTypeFactory.PropertyTypes.OrderBy(propertyType => propertyType.Value);
            ddlType.DataValueField = "Key";
            ddlType.DataTextField = "Value";
            ddlType.DataBind();
        }

        public MultiType Save()
        {
            return MultiType;
        }

        public void Reset()
        {
            txtName.Text = "";
            txtAlias.Text = "";
            txtDescription.Text = "";
            ddlType.SelectedIndex = 0;
            pnlAdditionalProperties.Controls.Clear();
            pnlAdditionalProperties.Visible = false;
        }

        public bool isValid()
        {
            if(!String.IsNullOrEmpty(txtName.Text) &&
               !String.IsNullOrEmpty(txtAlias.Text))
                return true;
            return false;
        }

        public void CSSClass(string cssClass){
            pnlProperty.CssClass = cssClass;
        }

        protected void ibtnDelete_Click(object sender, EventArgs e)
        {
            if (DeleteClicked != null)
            {
                pnlProperty.Visible = false;
                DeleteClicked(this.MultiType.Id);
            }
        }

    }
}