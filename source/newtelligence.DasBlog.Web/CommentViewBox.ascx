<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register TagPrefix="openid" Namespace="DotNetOpenId.RelyingParty" Assembly="DotNetOpenId" %>
<%@ Control language="c#" Codebehind="CommentViewBox.ascx.cs" AutoEventWireup="True" Inherits="newtelligence.DasBlog.Web.CommentViewBox" targetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div id="content" runat="server" class="bodyContentStyle">
<!-- BEGIN ID SELECTOR -->
<script type="text/javascript">
       idselector_input_id = '<%= openid_identifier.ClientID %>';
</script>
<script type="text/javascript" id="__openidselector" src="https://www.idselector.com/selector/f2b823bfcf61d5c04bd1d839321fa4e360d307f3" charset="utf-8"></script>
<!-- END ID SELECTOR -->
	<div runat="server" id="commentViewContent">
	</div>
	<div id="commentViewEntry" class="commentViewBoxStyle">
		<span id="commentsClosed" style="TEXT-ALIGN: center" runat="server">
			<asp:Label id="labelCommentsClosed" runat="server" CssClass="commentViewLabelStyle" Text='<%# resmgr.GetString("text_comments_closed") %>'>
			</asp:Label>
		</span><span id="commentsModerated" style="COLOR: red;TEXT-ALIGN: center" runat="server">
			<asp:label id="labelCommentsModerated" runat="server" CssClass="commentViewLabelStyle" Text='<%# resmgr.GetString("text_comments_require_approval") %>' />
		</span>
		<TABLE class="commentViewTableStyle" id="openIdTable" cellSpacing="1" cellPadding="1" border="0" runat="server" width="100%">
			
		<TR>
				<TD nowrap><asp:Label id="Label4" runat="server" CssClass="commentViewLabelStyle" Text='<%# resmgr.GetString("text_openid_name") %>'></asp:Label>
					<asp:RequiredFieldValidator ValidationGroup="OpenId" id="RequiredFieldValidator1" runat="server" ErrorMessage='<%# resmgr.GetString("text_error_openid_name_rf")%>'
						Display="Dynamic" ControlToValidate="openid_identifier">*</asp:RequiredFieldValidator></TD>
				<TD width="100%">
					<openid:OpenIdTextBox RequestEmail=Require RequestNickname=Require RequestFullName=Request OnLoggedIn="openid_identifier_LoggedIn"
					  CssClass="openidtextbox" ValidationGroup="OpenId" id="openid_identifier" MaxLength="96" runat="server" Columns="40" /></TD>
			</TR>
			<tr><td colspan="2"><%=resmgr.GetString("text_openid_instructions") %></td></tr>
		</table>
		<TABLE class="commentViewTableStyle" id="commentViewTable" cellSpacing="1" cellPadding="1" border="0" runat="server" width="100%">
			<TR>
				<TD nowrap><asp:Label id="Label1" runat="server" CssClass="commentViewLabelStyle" Text='<%# resmgr.GetString("text_person_name") %>'></asp:Label>
					<asp:RequiredFieldValidator id="validatorRFName" ValidationGroup="Normal" runat="server" ErrorMessage='<%# resmgr.GetString("text_error_person_name_rf")%>'
						Display="Dynamic" ControlToValidate="name">*</asp:RequiredFieldValidator></TD>
				<TD width="100%">
					<asp:TextBox id="name" MaxLength="32" runat="server" ValidationGroup="Normal" Columns="40" CssClass="commentViewControlStyle"
						Width="99%"></asp:TextBox></TD>
			</TR>
			<TR>
				<TD nowrap><asp:Label id="Label2" runat="server" CssClass="commentViewLabelStyle" Text='<%# resmgr.GetString("text_person_email") %>'></asp:Label>
					<asp:RegularExpressionValidator id="validatorREEmail" ValidationGroup="Normal" runat="server" ErrorMessage='<%# resmgr.GetString("text_error_person_email_re")%>'
						Display="Dynamic" ControlToValidate="email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></TD>
				<TD nowrap>
					<asp:TextBox MaxLength="40" id="email" runat="server" ValidationGroup="Normal" Columns="60" CssClass="commentViewControlStyle"
						Width="99%"></asp:TextBox>
					<span id="commentsGravatarEnabled" class="gravatarLabel" runat="server">
						<br>
						<asp:Label ID="labelGravatarEnabled" Runat="server" CssClass="commentViewLabelStyle" Text='<%# resmgr.GetString("text_comment_gravatarable") %>' >
						</asp:Label>
					</span>
				</TD>
			</TR>
			<TR>
				<TD nowrap><asp:Label id="Label3" runat="server" CssClass="commentViewLabelStyle" Text='<%# resmgr.GetString("text_person_homepage") %>'></asp:Label></TD>
				<TD>
					<asp:TextBox MaxLength="64" id="homepage" runat="server"  Columns="60" CssClass="commentViewControlStyle"
						Width="99%"></asp:TextBox></TD>
			</TR>
			<TR>
				<TD noWrap colspan="2">
				</TD>
			</TR>
			<TR>
				<TD></TD>
				<TD>
					<P><asp:CheckBox id="rememberMe" runat="server" Text='<%# resmgr.GetString("text_person_remember") %>' Checked="True" CssClass="commentViewControlStyle"></asp:CheckBox></P>
				</TD>
			</TR>
			<TR>
				<TD colSpan="2" height="24"><asp:Label id="labelComment" runat="server" Text='<%# resmgr.GetString("text_comment_content") %>' CssClass="commentViewLabelStyle"></asp:Label>
					<asp:Label id="labelCommentHtml" runat="server" CssClass="commentViewLabelStyle"></asp:Label>
					<asp:RequiredFieldValidator id="validatorRFComment" runat="server" ErrorMessage='<%# resmgr.GetString("text_comment_content_rf")%>'
						ControlToValidate="comment">*</asp:RequiredFieldValidator></TD>
			</TR>
			<TR>
				<TD colSpan="2" noWrap>
					<asp:TextBox Rows="12" Columns="60" id="comment" runat="server" TextMode="MultiLine" CssClass="commentViewControlStyle livepreview"
						Width="99%"></asp:TextBox>
					<P></P>
					<cc1:CaptchaControl CaptchaFontWarping="Extreme" id=CaptchaControl1 runat="server" Text='<%# resmgr.GetString("text_captcha")%>' ShowSubmitButton="False" CaptchaTimeout="300" CaptchaFont="Verdana">
					</cc1:CaptchaControl>
					<asp:CustomValidator id="cvCaptcha" runat="server" Display="None" ErrorMessage='<%# resmgr.GetString("text_captcha_error")%>'></asp:CustomValidator>
				</TD>
			</TR>
			<TR>
				<TD colSpan="2" height="24"><asp:Label id="lblCommentPreview" runat="server" Text='<%# resmgr.GetString("text_comment_livepreview") %>' CssClass="commentViewLabelStyle"></asp:Label>
				</TD>
			</TR>
			<TR>
				<TD colSpan="2"><div class="commentBodyStyle livepreview"></div>
				</TD>
			</TR>
			<TR>
				<TD colSpan="2" noWrap>
					<div style="WIDTH:50%;TEXT-ALIGN:left"><asp:ValidationSummary id="ValidationSummary1" runat="server"></asp:ValidationSummary></div>
					<asp:Button id="add" OnClick="add_Click" runat="server" Text='<%# resmgr.GetString("text_save_comment") %>' CssClass="commentViewControlStyle" >
					</asp:Button>
				</TD>
			</TR>
		</TABLE>
	</div>
</div>
