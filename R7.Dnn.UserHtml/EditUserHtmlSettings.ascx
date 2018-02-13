<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUserHtmlSettings.ascx.cs" Inherits="R7.Dnn.UserHtml.EditUserHtmlSettings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/R7.Dnn.UserHtml/R7.Dnn.UserHtml/admin.css" />
<div class="dnnForm dnnClear">
    <h2 class="dnnFormSectionHead"><a href="#"><asp:Label runat="server" ResourceKey="BasicSettings.Section" /></a></h2>
    <fieldset>  
        <div class="dnnFormItem">
            <dnn:Label id="lblTemplate" runat="server" ControlName="txtTemplate" />
            <asp:TextBox id="txtTemplate" runat="server" CssClass="NormalTextBox" Rows="10" Columns="30" TextMode="MultiLine" MaxLength="2000" />
        </div>
    </fieldset> 
</div>
