using System;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml.Serialization;
using newtelligence.DasBlog.Runtime;
using newtelligence.DasBlog.Web.Core;
using newtelligence.DasBlog.Util;
using System.ComponentModel;

namespace newtelligence.DasBlog.Web.Core
{
	/// <summary>
	/// Summary description for ArchiveMonthsList.
	/// </summary>
	public class ArchiveMonthsList : System.Web.UI.WebControls.WebControl
	{
		private ArrayList _monthList;
		private StringDictionary _monthTable;
		private SharedBasePage _requestPage; 

		public ArchiveMonthsList()
		{
			this.Load += new EventHandler( this.ArchiveMonthList_Load);
		}

		void ArchiveMonthList_Load( object sender, EventArgs e )
		{
			DateTime[] daysWithEntries;
			_requestPage = this.Page as SharedBasePage;
			TimeZone timezone = null;
			if ( _requestPage.SiteConfig.AdjustDisplayTimeZone )
			{
				timezone = _requestPage.SiteConfig.GetConfiguredTimeZone();
			}
			else
			{
				timezone = new newtelligence.DasBlog.Util.UTCTimeZone();
			}
			daysWithEntries = _requestPage.DataService.GetDaysWithEntries(timezone);

			_monthTable = new StringDictionary();
			_monthList = new ArrayList();

			string languageFilter = Page.Request.Headers["Accept-Language"];
			foreach (DateTime date in daysWithEntries) 
			{
				if(date <= DateTime.UtcNow)
				{
					DateTime month = new DateTime(date.Year, date.Month, 1, 0, 0, 0);
					string monthKey = month.ToString("MMMM, yyyy");
					if (! _monthTable.ContainsKey(monthKey) ) 
					{
						EntryCollection entries = _requestPage.DataService.GetEntriesForMonth(month,timezone,languageFilter);
						if (entries != null)
						{
							_monthTable.Add(monthKey, entries.Count.ToString());
							_monthList.Add(month);
						}
					}
				}
			}

			_monthList.Sort();
			_monthList.Reverse();

		}
			
		protected override void Render(HtmlTextWriter writer)
		{
			HtmlGenericControl section = new HtmlGenericControl("div");
			section.Attributes["class"] = "archiveLinksContainerStyle";
			this.Controls.Add(section);
            
			Table list = new Table();
			list.CssClass = "archiveLinksTableStyle";
			section.Controls.Add(list);

			try
			{
				foreach (DateTime date in _monthList)
				{
					TableRow row = new TableRow();
					TableCell cell = new TableCell();
					//cell.CssClass ="archiveLinksCellStyle";
					list.Rows.Add(row);
					row.Cells.Add(cell);

					HyperLink monthLink = new HyperLink();
					//monthLink.CssClass = "archiveLinksLinkStyle";
					string monthKey = date.ToString("MMMM, yyyy");
					monthLink.Text = monthKey +" (" + _monthTable[monthKey] +")";
					monthLink.NavigateUrl = SiteUtilities.GetMonthViewUrl(_requestPage.SiteConfig, date);
					cell.Controls.Add(monthLink);
				}
			}
			catch( Exception exc )
			{
				ErrorTrace.Trace(System.Diagnostics.TraceLevel.Error,exc);
				section.Controls.Add(new LiteralControl("There was an error generating archive list<br />"));
			}

			section.RenderControl( writer );
		}

	
	}
}
