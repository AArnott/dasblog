<%@ Register TagPrefix="ftb" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="EditUserBox.ascx.cs" Inherits="newtelligence.DasBlog.Web.EditUserBox" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="bodyContentStyle">
	<div class="pageTopic"><asp:label id=labelConfig Text='<%# resmgr.GetString("text_config_title") %>' runat="server">
		</asp:label></div>
	<asp:validationsummary id=validationSummary runat="server" HeaderText='<%# resmgr.GetString("text_config_errors_title") %>'>
	</asp:validationsummary>
	<DIV class="configSectionStyle">
		<TABLE cellSpacing="0" cellPadding="2" border="0">
		    <tr>
		        <TD class="configLabelColumnStyle" noWrap><asp:label id=label1 Text='<%# resmgr.GetString("text_displayname") %>' runat="server" CssClass="configLabelStyle"></asp:label></TD>
				<TD><asp:textbox id="textDisplayName" runat="server" CssClass="configControlStyle" Columns="40" MaxLength="120"></asp:textbox>&nbsp;</TD>
		    </tr>
			<TR>
				<TD class="configLabelColumnStyle" noWrap><asp:label id=labelContact Text='<%# resmgr.GetString("text_person_email") %>' runat="server" CssClass="configLabelStyle"></asp:label>
					<asp:requiredfieldvalidator id="validatorRFContact" runat="server" ControlToValidate="textEMail" Display="Dynamic"
						ErrorMessage="'E-mail' is a required field and cannot be empty.">*</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="validatorREContact" runat="server" ControlToValidate="textEMail" Display="Dynamic"
						ErrorMessage="The given Email address does not appear to be valid." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator>
				</TD>
				<TD><asp:textbox id="textEMail" runat="server" CssClass="configControlStyle" Columns="40" MaxLength="120"></asp:textbox>&nbsp;</TD>
			</TR>
			<tr>
		        <TD class="configLabelColumnStyle" noWrap><asp:label id=label2 Text='<%# resmgr.GetString("text_OpenIdIdentifier") %>' runat="server" CssClass="configLabelStyle"></asp:label></TD>
				<TD><asp:textbox id="textOpenIdIdentifier" runat="server" CssClass="configControlStyle" Columns="40" MaxLength="120"></asp:textbox>&nbsp;</TD>
		    </tr>
			<TR>
				<TD class="configLabelColumnStyle" noWrap><font class="configLabelStyle"><asp:label id=labelNotifications Text='<%# resmgr.GetString("text_config_notifications") %>' runat="server" CssClass="configLabelStyle"></asp:label></font>
				</TD>
				<TD><asp:checkbox id=checkboxNewPost Text='<%# resmgr.GetString("text_config_notification_newpost") %>' runat="server" CssClass="configControlStyle"></asp:checkbox><br>
					<asp:checkbox id=checkboxAllComment Text='<%# resmgr.GetString("text_config_notification_allcomment") %>' runat="server" CssClass="configControlStyle">
					</asp:checkbox><br>
					<asp:checkbox id=checkboxOwnComment Text='<%# resmgr.GetString("text_config_notification_owncomment") %>' runat="server" CssClass="configControlStyle">
					</asp:checkbox><br>
				</TD>
			</TR>
			<TR>
				<TD class="configLabelColumnStyle" noWrap><asp:label id=labelTextPassword Text='<%# resmgr.GetString("text_password")%>' runat="server" CssClass="configLabelStyle">
					</asp:label><asp:comparevalidator id="ComparevalidatorPassword" runat="server" ControlToValidate="textConfirmPassword"
						Display="Dynamic" ErrorMessage="The passwords do not match" ControlToCompare="textPassword">*</asp:comparevalidator></TD>
				</TD>
				<TD><asp:textbox id="textPassword" accessKey="Y" runat="server" CssClass="configControlStyle" Columns="10"
						MaxLength="40" TextMode="Password"></asp:textbox>&nbsp;
					<asp:label id=labelTextConfirmPassword Text='<%# resmgr.GetString("text_password_repeat")%>' runat="server" CssClass="configLabelStyle">
					</asp:label><asp:textbox id="textConfirmPassword" runat="server" CssClass="configControlStyle" Columns="10"
						MaxLength="40" TextMode="Password"></asp:textbox></TD>
			</TR>
			<TR>
				<TD class="configLabelColumnStyle" noWrap><font class="configLabelStyle"><asp:label id=labelProfilePage Text='<%# resmgr.GetString("text_person_profilepage") %>' runat="server" CssClass="configLabelStyle"></asp:label></font></TD>
				<TD width="100%">
					<DIV class="FreeTextboxStyle"><asp:placeholder id="editControlHolder" Runat="server"></asp:placeholder></DIV>
				</TD>
			</TR>
		</TABLE>
	</DIV>
	<div style="CLEAR: both"><asp:button id=buttonSave Text='<%# resmgr.GetString("text_save_settings") %>' runat="server" onclick="buttonSave_Click">
		</asp:button></div>
</div>
