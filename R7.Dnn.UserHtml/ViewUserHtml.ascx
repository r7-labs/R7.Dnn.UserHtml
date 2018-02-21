<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewUserHtml.ascx.cs" Inherits="R7.Dnn.UserHtml.ViewUserHtml" %>
<asp:Panel id="pnlUser" runat="server">
	<asp:Panel id="pnlSearchUser" runat="server" CssClass="form-group" DefaultButton="btnSearchUser">
		<label for="<%= txtSearchUser.ClientID %>"><%= LocalizeString ("lblSearchUser.Text") %></label>
        <div class="input-group">
		    <asp:TextBox id="txtSearchUser" runat="server" CssClass="form-control" MaxLength="255" />
			<div class="input-group-append">
		        <asp:LinkButton id="btnSearchUser" runat="server" role="button" CssClass="btn btn-primary"
		                OnClick="btnSearchUser_Click">
					<span class="fa fa-search"></span> <%= LocalizeString ("btnSearchUser.Text") %>
				</asp:LinkButton>
			</div>
		</div>
        <asp:Label id="lblSearchResult" runat="server" CssClass="small" />
	</asp:Panel>
	<asp:Panel id="pnlSelectUser" runat="server">
    	<div class="form-group">
            <label for="<%= selUser.ClientID %>"><%= LocalizeString ("lblUser.Text") %></label>
    		<asp:DropDownList id="selUser" runat="server" CssClass="form-control"
    			DataTextField="UserString"
    			DataValueField="UserId"
    		    AutoPostBack="true"
    		    OnSelectedIndexChanged="selUser_SelectedIndexChanged" />
    	</div>
    	<div class="form-group">
            <asp:HyperLink id="lnkEditUserHtml" runat="server" role="button" CssClass="btn btn-secondary">
                <span class="fa fa-edit"></span> <%= LocalizeString ("lnkEditUserHtml.Text") %>
            </asp:HyperLink>
        </div>
	</asp:Panel>
	<hr />
</asp:Panel>
<asp:Panel id="pnlUserHtml" runat="server">
    <asp:Literal id="litUserHtml" runat="server" />
</asp:Panel>