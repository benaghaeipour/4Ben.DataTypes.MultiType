<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiTypePrevalueEditorControl.ascx.cs" Inherits="_4Ben.DataTypes.MultiType.Usercontrols.MultiTypePrevalueEditorControl" %>

<%@ Register src="MultiTypePrevalueEditorItemControl.ascx" tagname="MultiTypePrevalueEditorItemControl" tagprefix="_4Ben" %>

<asp:Panel ID="pnlMultiType" runat="server">
    
    <div class="row clearfix">
        <asp:Label ID="lblLimit" runat="server" Text="MultiType Limit" AssociatedControlID="txtLimit" />
        <asp:TextBox ID="txtLimit" runat="server"></asp:TextBox>
    </div>

    <div class="row clearfix">
        <asp:Label ID="lblMacro" runat="server" Text="Render Macro" AssociatedControlID="ddlMacro"/>
        <asp:DropDownList ID="ddlMacro" runat="server"></asp:DropDownList>
    </div>

    <asp:Panel ID="pnlAddNewProperty" runat="server" GroupingText="Add New Property" CssClass="New">
        <div class="header">
            <h3>Click here to add a new Property</h3>
        </div>

        <_4Ben:MultiTypePrevalueEditorItemControl ID="newMultiType" runat="server"/>

    </asp:Panel>

    <asp:Panel ID="pnlEditProperties" runat="server" GroupingText="Edit Properties" CssClass="Edit">

    </asp:Panel>

</asp:Panel>