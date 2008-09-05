<%@ Register TagPrefix="uc1" TagName="EditBlogRollItem" Src="EditBlogRollItem.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EditBlogRollEditItem" Src="EditBlogRollEditItem.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="newtelligence.DasBlog.Web.Core.WebControls" Assembly="newtelligence.DasBlog.Web.Core" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="EditBlogRollBox.ascx.cs" Inherits="newtelligence.DasBlog.Web.EditBlogRollBox" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<DIV class="bodyContentStyle">
	<div class="pageTopic">
		<asp:Label id="Label1" runat="server" Text='<%# resmgr.GetString("text_blogrolls_title")%>'>
		</asp:Label>
	</div>
	<div class="configSectionStyle">
	<TABLE id="Table2" cellSpacing="0" cellPadding="2" width="100%" border="0">
		<TR>
			<TD noWrap><asp:label id="labelOpmlList" runat="server" Text='<%# resmgr.GetString("text_opml_files") %>'></asp:label></TD>
			<TD width="98%"><asp:dropdownlist id="listFiles" runat="server" Width="100%"></asp:dropdownlist></TD>
			<TD><asp:button id="buttonSelect" runat="server" Width="60px" Font-Size="8pt" Text='<%# resmgr.GetString("text_select") %>' onclick="buttonSelect_Click"></asp:button></TD>
		</TR>
		<TR>
			<TD noWrap></TD>
			<TD width="98%"><asp:textbox id="textNewFileName" runat="server" Width="99%"></asp:textbox></TD>
			<TD><asp:button id="buttonCreate" runat="server" Width="60px" Font-Size="8pt" Text='<%# resmgr.GetString("text_create") %>' onclick="buttonCreate_Click"></asp:button></TD>
		</TR>
	</TABLE>
	<cc1:ShadowBox id="shadowBox" style="MARGIN-TOP:10px" Width="100%" runat="server" ShadowDepth="6">
<asp:datagrid id=blogRollGrid runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1" CellPadding="4">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#000099"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<HeaderStyle Font-Bold="True" CssClass="sidetitle"></HeaderStyle>
		<Columns>
			<asp:TemplateColumn HeaderText="Blogroll Feeds">
				<HeaderStyle Width="98%"></HeaderStyle>
				<HeaderTemplate>
					<SPAN class="configLabelStyle">OPML</SPAN>
				</HeaderTemplate>
				<ItemTemplate>
					<uc1:EditBlogRollItem id=editBlogRollItem title='<%# DataBinder.Eval(Container,"DataItem.title")%>' runat="server" Description='<%# DataBinder.Eval(Container,"DataItem.description")%>' XmlUrl='<%# DataBinder.Eval(Container,"DataItem.xmlUrl")%>' HtmlUrl='<%# DataBinder.Eval(Container,"DataItem.htmlUrl")%>' Outline='<%# DataBinder.Eval(Container,"DataItem.outline")%>'>
					</uc1:EditBlogRollItem>
				</ItemTemplate>
				<EditItemTemplate>
					<uc1:EditBlogRollEditItem id=editBlogRollEditItem title='<%# DataBinder.Eval(Container,"DataItem.title")%>' runat="server" Description='<%# DataBinder.Eval(Container,"DataItem.description")%>' XmlUrl='<%# DataBinder.Eval(Container,"DataItem.xmlUrl")%>' HtmlUrl='<%# DataBinder.Eval(Container,"DataItem.htmlUrl")%>' Outline='<%# DataBinder.Eval(Container,"DataItem.outline")%>'>
					</uc1:EditBlogRollEditItem>
				</EditItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn>
				<HeaderStyle Width="55px"></HeaderStyle>
				<ItemStyle VerticalAlign="Top"></ItemStyle>
				<HeaderTemplate>
					<DIV style="white-space: nowrap;">
						<asp:ImageButton id=buttonAddItem runat="server" ImageUrl='<%# ((newtelligence.DasBlog.Web.Core.SharedBasePage)Page).GetThemedImageUrl("addbutton-list") %>' CommandName="AddItem" ToolTip='<%# resmgr.GetString("tooltip_item_add") %>'>
						</asp:ImageButton></DIV>
				</HeaderTemplate>
				<ItemTemplate>
				<DIV style="white-space: nowrap;">
						<asp:ImageButton id=ImageButton1 runat="server" ImageUrl='<%# ((newtelligence.DasBlog.Web.Core.SharedBasePage)Page).GetThemedImageUrl("editbutton-list") %>' CommandName="Edit" ToolTip='<%# resmgr.GetString("tooltip_item_edit") %>' AlternateText="Edit" CausesValidation="False">
						</asp:ImageButton>&nbsp;
						<asp:ImageButton id=ImageButton2 runat="server" ImageUrl='<%# ((newtelligence.DasBlog.Web.Core.SharedBasePage)Page).GetThemedImageUrl("deletebutton-list") %>' CommandName="Delete" ToolTip='<%# resmgr.GetString("tooltip_item_delete") %>' AlternateText="Delete" CausesValidation="False">
						</asp:ImageButton></DIV>
				</ItemTemplate>
				<EditItemTemplate>
					<DIV style="white-space: nowrap;">
						<asp:ImageButton id=ImageButton3 runat="server" ImageUrl='<%# ((newtelligence.DasBlog.Web.Core.SharedBasePage)Page).GetThemedImageUrl("okbutton-list") %>' CommandName="Update" ToolTip='<%# resmgr.GetString("tooltip_item_ok") %>' AlternateText="Ok" CausesValidation="False">
						</asp:ImageButton>&nbsp;
						<asp:ImageButton id=ImageButton4 runat="server" ImageUrl='<%# ((newtelligence.DasBlog.Web.Core.SharedBasePage)Page).GetThemedImageUrl("undobutton-list") %>' CommandName="Cancel" ToolTip='<%# resmgr.GetString("tooltip_item_cancel") %>' AlternateText="Cancel" CausesValidation="False">
						</asp:ImageButton></DIV>
				</EditItemTemplate>
			</asp:TemplateColumn>
		</Columns>
		<PagerStyle HorizontalAlign="Center" Mode="NumericPages"></PagerStyle>
	</asp:datagrid>
	</cc1:ShadowBox>
	</div>
</DIV>
