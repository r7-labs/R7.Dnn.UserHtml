<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewUserHtml.ascx.cs" Inherits="R7.Dnn.UserHtml.ViewUserHtml" %>
<asp:ListView id="lstContent" ItemType="R7.Dnn.UserHtml.Models.UserHtmlInfo" runat="server">
    <LayoutTemplate>
        <ul runat="server" class="r7-userhtml-content-list">
            <asp:Placeholder runat="server" id="itemPlaceholder" />
        </ul>
    </LayoutTemplate>
    <ItemTemplate>
        <li class="r7-userhtml-content-item">
            <asp:HyperLink id="linkEdit" runat="server" Visible="<%# IsEditable %>"
                    NavigateUrl='<%# EditUrl ("UserHtmlId".ToLowerInvariant (), Item.UserHtmlId.ToString (), "Edit") %>'>
                <asp:Image id="imageEdit" runat="server" IconKey="Edit" ResourceKey="Edit" />
            </asp:HyperLink>
            <asp:Literal runat="server" Text="<%# Item.FillTemplate (Settings.Template) %>" />
        </li>
    </ItemTemplate>
</asp:ListView>
