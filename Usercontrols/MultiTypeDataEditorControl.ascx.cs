using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4Ben.DataTypes.MultiType;
using System.Xml;

namespace _4Ben.DataTypes.MultiType.Usercontrols
{
    public class XmlItem
    {
        public int Id;
        public int SortId;
        public XmlNode Aliases;

        public XmlNode GetXmlItem()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode item = doc.CreateElement("Item");

            XmlAttribute xmlId = doc.CreateAttribute("id");
            xmlId.Value = Id.ToString();
            item.Attributes.Append(xmlId);

            XmlAttribute xmlSortId = doc.CreateAttribute("sortId");
            xmlSortId.Value = SortId.ToString();
            item.Attributes.Append(xmlSortId);

            return item;
        }
    }

    public partial class MultiTypeDataEditorControl : System.Web.UI.UserControl
    {
        public delegate void SaveDataEditorControl();
        public event SaveDataEditorControl SaveDataEditor;

        Dictionary<string, MultiType> MultiTypeByAlias;
        Dictionary<int, XmlItem> XmlItems;
        Dictionary<int, MultiTypeDataEditorItemControl> MultiTypeDataEditorItemControls;

        public XmlNodeList Items { get; set; }
        public List<MultiType> MultiTypes { get; set; }
        public int Limit { get; set; }
        public int MacroId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            MultiTypeByAlias = new Dictionary<string, MultiType>();
            XmlItems = new Dictionary<int, XmlItem>();
            //MultiTypeDataEditorItemControls = new Dictionary<int, MultiTypeDataEditorItemControl>();
            foreach (MultiType multiType in MultiTypes)
            {
                MultiTypeByAlias.Add(multiType.Alias, multiType);
            }

            foreach (XmlNode item in Items)
            {
                var id = int.Parse(item.Attributes["id"].Value);
                var sortId = int.Parse(item.Attributes["sortId"].Value);
                XmlItems.Add(id, new XmlItem()
                {
                    Id = id,
                    SortId = sortId,
                    Aliases = item
                });
            }

            editMultiTypeControl.MultiTypes = MultiTypeByAlias;
            MultiTypeDataEditorItemControls = new Dictionary<int, MultiTypeDataEditorItemControl>();

            foreach (XmlItem item in XmlItems.Values.OrderBy(item => item.SortId))
            {
                var multiTypeDataEditorItemControl = (MultiTypeDataEditorItemControl)Page.LoadControl("~/umbraco/plugins/MultiType4Ben/MultiTypeDataEditorItemControl.ascx");

                multiTypeDataEditorItemControl.Id = item.Id;
                multiTypeDataEditorItemControl.SortId = item.SortId;
                multiTypeDataEditorItemControl.MacroId = MacroId;
                multiTypeDataEditorItemControl.Items = item.Aliases;
                multiTypeDataEditorItemControl.MultiTypes = MultiTypeByAlias;
                multiTypeDataEditorItemControl.Delete += new MultiTypeDataEditorItemControl.DataEditorItemButton(Delete);
                multiTypeDataEditorItemControl.Edit +=new MultiTypeDataEditorItemControl.DataEditorItemButton(Edit);
                multiTypeDataEditorItemControl.Visible = false;

                MultiTypeDataEditorItemControls.Add(item.Id, multiTypeDataEditorItemControl);
                pnlMultiTypes.Controls.Add(multiTypeDataEditorItemControl);
            }

            if (Limit == 0 || XmlItems.Count < Limit)
            {
                lnkAddMultiType.Visible = true;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var multiTypeDataEditorItemControlDummy = (MultiTypeDataEditorItemControl)Page.LoadControl("~/umbraco/plugins/MultiType4Ben/MultiTypeDataEditorItemControl.ascx");

            foreach (Control multiTypeDataEditorItemControl in pnlMultiTypes.Controls)
            {
                if (multiTypeDataEditorItemControl.GetType() == multiTypeDataEditorItemControlDummy.GetType())
                {
                    var id = ((MultiTypeDataEditorItemControl)multiTypeDataEditorItemControl).Id;
                    if (XmlItems.ContainsKey(id))
                    {
                        ((MultiTypeDataEditorItemControl)multiTypeDataEditorItemControl).SetupItem(XmlItems[id].Aliases);
                        multiTypeDataEditorItemControl.Visible = true;
                    }
                    else
                    {
                        multiTypeDataEditorItemControl.Visible = false;
                    }
                }
            }
        }

        public List<XmlNode> Save()
        {
            List<XmlNode> items = new List<XmlNode>();

            foreach (XmlItem xmlItem in XmlItems.Values)
            {
                if (MultiTypeDataEditorItemControls.ContainsKey(xmlItem.Id))
                {
                    xmlItem.SortId = MultiTypeDataEditorItemControls[xmlItem.Id].SortId;

                    xmlItem.Aliases = XmlItems[xmlItem.Id].Aliases;

                    items.Add(xmlItem.Aliases);

                }
            }

            return items;
        }

        protected void Add()
        {
            int id = NewId();
            int sortId = NewSortId();

            XmlItem xmlItem = new XmlItem() { 
                Id = id, 
                SortId = sortId
            };

            xmlItem.Aliases = editMultiTypeControl.Save(xmlItem.GetXmlItem());

            XmlItems.Add(id, xmlItem);

            var multiTypeDataEditorItemControl = (MultiTypeDataEditorItemControl)Page.LoadControl("~/umbraco/plugins/MultiType4Ben/MultiTypeDataEditorItemControl.ascx");

            multiTypeDataEditorItemControl.Id = id;
            multiTypeDataEditorItemControl.SortId = sortId;
            multiTypeDataEditorItemControl.MacroId = MacroId;
            multiTypeDataEditorItemControl.Items = XmlItems[id].Aliases;
            multiTypeDataEditorItemControl.MultiTypes = MultiTypeByAlias;
            multiTypeDataEditorItemControl.Delete += new MultiTypeDataEditorItemControl.DataEditorItemButton(Delete);
            multiTypeDataEditorItemControl.Edit += new MultiTypeDataEditorItemControl.DataEditorItemButton(Edit);
            multiTypeDataEditorItemControl.Visible = false;

            MultiTypeDataEditorItemControls.Add(id, multiTypeDataEditorItemControl);

            pnlMultiTypes.Controls.Add(multiTypeDataEditorItemControl);

            if (SaveDataEditor != null)
            {
                SaveDataEditor();
            }

            Close();
        }

        protected void Delete(int id)
        {
            if (XmlItems.ContainsKey(id))
            {
                XmlItems.Remove(id);
            }

            if (SaveDataEditor != null)
            {
                SaveDataEditor();
            }

            Close();
        }

        protected void Edit(int id)
        {
            if(XmlItems.ContainsKey(id)){
                lnkAddMultiType.Visible = false;
                editMultiTypeControl.SetValues(XmlItems[id].Aliases);
                editMultiTypeControl.Visible = true;
                btnUpdateMultiTypeItem.CommandArgument = id.ToString();
                btnUpdateMultiTypeItem.Visible = true;
                btnCloseMultiTypeItem.Visible = true;
            }
        }

        protected void Update()
        {
            int id = -1;
            int.TryParse(btnUpdateMultiTypeItem.CommandArgument, out id);

            if (XmlItems.ContainsKey(id)){
                var xmlItem = XmlItems[id];

                XmlDocument doc = new XmlDocument();
                
                XmlNode item = doc.CreateElement("Item");

                XmlAttribute xmlId = doc.CreateAttribute("id");
                xmlId.Value = xmlItem.Id.ToString();
                item.Attributes.Append(xmlId);

                XmlAttribute xmlSortId = doc.CreateAttribute("sortId");
                xmlSortId.Value = xmlItem.SortId.ToString();
                item.Attributes.Append(xmlSortId);

                XmlItems[id] = new XmlItem()
                {
                    Id = xmlItem.Id,
                    SortId = xmlItem.SortId,
                    Aliases = editMultiTypeControl.Save(item)
                };

                if (SaveDataEditor != null)
                {
                    SaveDataEditor();
                }

                Close();
            }
        }   

        protected void Close()
        {
            editMultiTypeControl.Visible = false;
            btnAddMultiTypeItem.Visible = false;
            btnUpdateMultiTypeItem.Visible = false;
            btnCloseMultiTypeItem.Visible = false;
            if (Limit == 0 || XmlItems.Count < Limit)
            {
                lnkAddMultiType.Visible = true;
            }
        }

        protected void lnkAddMultiType_Click(object sender, EventArgs e)
        {
            editMultiTypeControl.ResetValues();
            editMultiTypeControl.Visible = true;
            btnAddMultiTypeItem.Visible = true;
            btnCloseMultiTypeItem.Visible = true;
            lnkAddMultiType.Visible = false;
        }

        protected void btnAddMultiTypeItem_Click(object sender, EventArgs e)
        {
            Add();
        }

        protected void btnUpdateMultiTypeItem_Click(object sender, EventArgs e)
        {
            Update();
        }

        protected void btnCloseMultiTypeItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int NewId(int y = 0)
        {
            var x = y;
            if (XmlItems.Count > 0)
            {
                foreach (var xmlItem in XmlItems.Values.OrderBy(item => item.Id))
                {
                    if (x == xmlItem.Id)
                    {
                        x++;
                        NewId(x);
                    }
                }
            }
            return x;
        }

        private int NewSortId(int y = 1)
        {
            var x = y;
            if (XmlItems.Count > 0)
            {
                foreach (var xmlItem in XmlItems.Values.OrderBy(item => item.SortId))
                {
                    if (x == xmlItem.SortId)
                    {
                        x++;
                        NewSortId(x);
                    }
                }
            }
            return x;
        }
    }
}