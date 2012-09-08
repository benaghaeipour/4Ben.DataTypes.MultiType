<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiTypePrevalueEditorItemControl.ascx.cs" Inherits="_4Ben.DataTypes.MultiType.Usercontrols.MultiTypePrevalueEditorItemControl" %>

<asp:Panel ID="pnlProperty" runat="server">

    <div class="header">
        <h3><asp:Literal ID="litHeader" runat="server"></asp:Literal></h3>
        <span class="delete" runat="server">
            <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/umbraco/plugins/MultiType4Ben/images/cross-script.png" Visible="false" OnClick="ibtnDelete_Click" OnClientClick="return confirm('Are you sure?');" />
        </span>
        <div class="editFields">
            <asp:HiddenField ID="hdnId" runat="server" Value="-1" />
            <asp:HiddenField ID="hdnSortId" runat="server" Value="-1" />

            <div class="row clearfix">
                <asp:Label ID="lblName" runat="server" CssClass="label" AssociatedControlID="txtName">Name: </asp:Label>
                <div class="field">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row clearfix">
                <asp:Label ID="lblAlias" runat="server" CssClass="label" AssociatedControlID="txtAlias">Alias: </asp:Label>
                <div class="field">
                    <asp:TextBox ID="txtAlias" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row clearfix">
                <asp:Label ID="lblType" runat="server" CssClass="label" AssociatedControlID="ddlType">Type: </asp:Label>
                <div class="field">
                    <asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="row clearfix">
                <asp:Label ID="lblDescription" runat="server" CssClass="label" AssociatedControlID="txtDescription">Description: </asp:Label>
                <div class="field">
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>

            <asp:Panel ID="pnlAdditionalProperties" runat="server" GroupingText="Additional Properties:" Visible="false">
            
            </asp:Panel>

        </div>
    </div>

</asp:Panel>