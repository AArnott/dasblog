<%@ Control language="c#" Codebehind="ActivityBox.ascx.cs" AutoEventWireup="True" Inherits="newtelligence.DasBlog.Web.ActivityBox" targetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<DIV class="adminNavbarStyle" id="DIV1" runat="server">
	<UL>
		<LI class="first" id="eventlog" runat="server">
			<asp:HyperLink id=hyperLinkEventlog Text='<%# resmgr.GetString("text_admin_events")%>' runat="server" NavigateUrl="Eventlog.aspx">
			</asp:HyperLink></LI><LI id="referrers" runat="server">
			<asp:HyperLink id=hyperLinkReferrers Text='<%# resmgr.GetString("text_admin_referrers")%>' runat="server" NavigateUrl="Referrers.aspx">
			</asp:HyperLink></LI><%if (siteConfig.EnableAggregatorBugging){%><LI id="aggbugs" runat="server">
			<asp:HyperLink id=hyperlinkAggBugs Text='<%# resmgr.GetString("text_admin_aggbugs")%>' runat="server" NavigateUrl="AggBugs.aspx">
			</asp:HyperLink></LI><%}%><%if (siteConfig.EnableClickThrough) {%><LI id="clickthrough" runat="server">
			<asp:HyperLink id=hyperlinkClickThroughs Text='<%# resmgr.GetString("text_admin_clickthrough")%>' runat="server" NavigateUrl="ClickThroughs.aspx">
			</asp:HyperLink></LI><LI id="userclickthrough" runat="server">
			<asp:HyperLink id="hyperlinkUserClickThroughs" Text='<%# resmgr.GetString("text_admin_userclickthrough")%>' runat="server" NavigateUrl="UserClickThroughs.aspx">
			</asp:HyperLink></LI><%}%><%if (siteConfig.EnableCrossposts) {%><LI id="crosspost" runat="server">
			<asp:HyperLink id=hyperlinkCrosspostReferrers Text='<%# resmgr.GetString("text_admin_crosspost")%>' runat="server" NavigateUrl="CrosspostReferrers.aspx">
			</asp:HyperLink></LI>
			<%}%>
		
	</UL>
</DIV>
