<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUserHtmlSettings.ascx.cs" Inherits="R7.Dnn.UserHtml.EditUserHtmlSettings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Picker" Src="~/controls/filepickeruploader.ascx" %> 
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/R7.Dnn.UserHtml/R7.Dnn.UserHtml/admin.css" />
<asp:Panel id="pnlGeneralSettings" runat="server" CssClass="dnnForm dnnClear">
    <h2 class="dnnFormSectionHead"><a href="#"><asp:Label runat="server" ResourceKey="GeneralSettings.Section" /></a></h2>
    <fieldset> 
		<div class="dnnFormItem">
            <dnn:Label id="lblRoles" runat="server" ControlName="txtRoles" />
            <asp:TextBox id="txtRoles" runat="server" />
        </div>
        <div class="dnnFormItem">
			<dnn:Label id="lblEmptyHtml" runat="server" ControlName="txtEmptyHtml" />
            <asp:TextBox id="txtEmptyHtml" runat="server" TextMode="MultiLine" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label id="lblDefaultHtml" runat="server" ControlName="txtDefaultHtml" />
            <asp:TextBox id="txtDefaultHtml" runat="server" TextMode="MultiLine" Rows="20" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label id="lblTemplatesFile" runat="server" ControlName="fpuTemplatesFile" />
			<dnn:Picker id="fpuTemplatesFile" runat="server" />
        </div>
    </fieldset> 
</asp:Panel>
