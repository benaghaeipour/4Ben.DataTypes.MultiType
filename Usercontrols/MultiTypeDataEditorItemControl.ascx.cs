using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using umbraco.cms.businesslogic.macro;

namespace _4Ben.DataTypes.MultiType.Usercontrols
{
    public partial class MultiTypeDataEditorItemControl : System.Web.UI.UserControl
    {
        public delegate void DataEditorItemButton(int MultiTypeId);

        public event DataEditorItemButton Delete;
        public event DataEditorItemButton Edit;
        
        private int _sortId;

        public int Id { get; set; }
        public int SortId {
            get {
                var x = -1;
                int.TryParse(hdnSortId.Value, out x);
                return x;
            }
            set { _sortId = value; }
        }
        public int MacroId { get; set; }
        public XmlNode Items { get; set; }
        public Dictionary<string, MultiType> MultiTypes { get; set; }
        public Dictionary<string, object> Parameters { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            hdnSortId.Value = _sortId.ToString();
        }

        public void SetupItem(XmlNode items = null)
        {
            Parameters = new Dictionary<string, object>();

            if (items == null)
            {
                items = Items;
            }
            
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("MultiTypes");
            doc.AppendChild(root);
            XmlNode xsltItem, xsltAlias;
            XmlAttribute xsltSort;

            if (items.HasChildNodes)
            {
                foreach (XmlNode xmlNode in items.ChildNodes)
                {
                    if (MultiTypes.ContainsKey(xmlNode.Name))
                    {

                        xsltAlias = doc.CreateElement(xmlNode.Name);

                        xsltItem = doc.CreateElement("Name");
                        xsltItem.InnerText = MultiTypes[xmlNode.Name].Name;
                        xsltAlias.AppendChild(xsltItem);

                        xsltSort = doc.CreateAttribute("sortId");
                        xsltSort.Value = MultiTypes[xmlNode.Name].SortId.ToString();
                        xsltAlias.Attributes.Append(xsltSort);

                        xsltItem = doc.CreateElement("Value");
                        xsltItem.InnerText = xmlNode.InnerText;
                        xsltAlias.AppendChild(xsltItem);

                        root.AppendChild(xsltAlias);
                    }
                }

                Parameters.Add("MultiTypes", root.SelectSingleNode("/MultiTypes"));
                
                pnlMultiTypeItems.Controls.Add(new Literal()
                {
                    Text = umbraco.macro.GetXsltTransformResult(new XmlDocument(), umbraco.macro.getXslt(new Macro(MacroId).Xslt), Parameters)
                });

            }
        }

        public XmlNode Save(XmlNode itemNode){

            XmlNode newItemNode = null;

            if (Items.HasChildNodes){

                XmlDocument doc = new XmlDocument();

                newItemNode = doc.ImportNode(itemNode, true);

                foreach (XmlNode xmlNode in Items.ChildNodes)
                {
                    var aliasNode = doc.CreateElement(xmlNode.Name);
                    aliasNode.InnerText = xmlNode.InnerText;
                    newItemNode.AppendChild(aliasNode);
                }
            }

            return newItemNode;
        }

        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (Delete != null)
            {
                Delete(Id);
            }
        }

        protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            if (Edit != null)
            {
                Edit(Id);
            }
        }
    }
}