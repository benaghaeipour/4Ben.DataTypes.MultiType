<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiTypeDataEditorItemControl.ascx.cs" Inherits="_4Ben.DataTypes.MultiType.Usercontrols.MultiTypeDataEditorItemControl" %>

<asp:Panel ID="pnlMultiTypeItem" runat="server" CssClass="multiType">
    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="sortId"><asp:HiddenField ID="hdnSortId" runat="server"/></div>

    <asp:Panel ID="pnlMultiTypeItems" runat="server">
        
        

    </asp:Panel>

    <asp:Panel ID="pnlMultiTypeItemActions" runat="server" CssClass="multiTypeActions">

        <asp:ImageButton ID="ibtnDelete" runat="server" CssClass="deleteMultiType" ImageUrl="~/umbraco/plugins/MultiType4Ben/images/cross-script.png" onclick="ibtnDelete_Click" OnClientClick="return confirm('Are you sure?');"/>
        <asp:ImageButton ID="ibtnEdit" runat="server" CssClass="editMultiType" ImageUrl="~/umbraco/plugins/MultiType4Ben/images/pencil.png" onclick="ibtnEdit_Click" />

    </asp:Panel>

</asp:Panel>