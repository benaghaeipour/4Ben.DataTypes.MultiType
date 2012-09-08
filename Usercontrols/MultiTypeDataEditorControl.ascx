<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiTypeDataEditorControl.ascx.cs" Inherits="_4Ben.DataTypes.MultiType.Usercontrols.MultiTypeDataEditorControl" %>

<%@ Register src="MultiTypeDataEditorEditControl.ascx" tagname="MultiTypeDataEditorEditControl" tagprefix="_4Ben" %>

<asp:Panel ID="pnlMultiTypeControl" runat="server" CssClass="MultiTypeContainer">

    <asp:Panel ID="pnlMultiTypes" runat="server" CssClass="MultiTypes">

    </asp:Panel>

    <asp:LinkButton ID="lnkAddMultiType" runat="server" CssClass="Add" Visible="True" OnClick="lnkAddMultiType_Click" Text="Add"/>

    <asp:Panel ID="pnlMultiType" runat="server">

        <_4Ben:MultiTypeDataEditorEditControl ID="editMultiTypeControl" runat="server" Visible="false" />
          
        <asp:Panel ID="pnlMultiTypeButtons" runat="server">

            <asp:Button ID="btnAddMultiTypeItem" runat="server" Visible="false" Text="Add" onclick="btnAddMultiTypeItem_Click" />
            <asp:Button ID="btnUpdateMultiTypeItem" runat="server" Visible="false" Text="Update" onclick="btnUpdateMultiTypeItem_Click" />
            <asp:Button ID="btnCloseMultiTypeItem" runat="server" Visible="false" Text="Close" onclick="btnCloseMultiTypeItem_Click" />

        </asp:Panel>

    </asp:Panel>

</asp:Panel>