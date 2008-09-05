using System;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Web;
using System.Web.UI.WebControls;
using newtelligence.DasBlog.Web.Core;

namespace newtelligence.DasBlog.Web
{
    public class StatisticsItem
    {
        internal string identifier;
        internal int count;
        internal StatisticsItem(string identifier, int count)
        {
            this.identifier = identifier;
            this.count = count;
        }

        public class Comparer : IComparer
        {
            public int Compare(object left, object right)
            {
            	StatisticsItem leftItem = (StatisticsItem)left;
				StatisticsItem rightItem = (StatisticsItem)right;

				int leftValue = leftItem.count;
            	int rightValue = rightItem.count;
                if (leftValue > rightValue)
                {
                    return -1;
                }
                else if (leftValue < rightValue)
                {
                    return 1;
                }
				
                return String.Compare(leftItem.identifier, rightItem.identifier, StringComparison.InvariantCultureIgnoreCase);
            }
        }

    	public class SearchStringComparer : IComparer
    	{
    		public int Compare(object left, object right)
    		{
    			StatisticsItem leftItem = (StatisticsItem) left;
    			StatisticsItem rightItem = (StatisticsItem) right;

    			int leftValue = leftItem.count;
    			int rightValue = rightItem.count;
    			if (leftValue > rightValue)
    			{
    				return -1;
    			}
    			else if (leftValue < rightValue)
    			{
    				return 1;
    			}

    			return
    				String.Compare(SiteUtilities.ParseSearchString(leftItem.identifier).Text,
    				               SiteUtilities.ParseSearchString(rightItem.identifier).Text,
    				               StringComparison.InvariantCultureIgnoreCase);
    		}
		}
    }

    public delegate void StatisticsBuilderCallback( TableRow currentRow, StatisticsItem item, object args );
    
    public class StatisticsListBase : System.Web.UI.UserControl
	{
        protected ResourceManager resmgr;

		public StatisticsListBase()
		{
            resmgr = ApplicationResourceTable.Get();
		}

		protected Table BuildStatisticsTable( 
			IEnumerable items, 
			string itemColumnName, 
			string numberColumnName, 
			StatisticsBuilderCallback buildFn, 
			object args )
		{
			string total = string.Empty;
			return BuildStatisticsTable(items,itemColumnName,numberColumnName,buildFn,out total,args);
		}

        protected Table BuildStatisticsTable( 
            IEnumerable items, 
            string itemColumnName, 
            string numberColumnName, 
            StatisticsBuilderCallback buildFn, 
			out string totalString,
            object args )
        {
            int total = 0;
            Table table = new Table();
            table.CssClass = "statsTableStyle";
            TableRow row = new TableRow();
            row.CssClass = "statsTableHeaderRowStyle";
            row.Cells.Add(new TableCell());
            row.Cells.Add(new TableCell());
            row.Cells[0].CssClass = "statsTableHeaderColumnStyle";
            row.Cells[1].CssClass = "statsTableHeaderNumColumnStyle";
            row.Cells[0].Text = itemColumnName;
            row.Cells[1].Text = numberColumnName;
            table.Rows.Add(row);

            foreach (StatisticsItem item in items)
            {
                row = new TableRow();
                row.CssClass = "statsTableRowStyle";
                row.Cells.Add(new TableCell());
                row.Cells.Add(new TableCell());
                row.Cells[0].CssClass = "statsTableColumnStyle";
                row.Cells[1].CssClass = "statsTableNumColumnStyle";

                buildFn( row, item, args );

				if (row.Cells[1].Text.Length > 0)
				{
					total += item.count;
					table.Rows.Add(row);
				}
            }

            TableRow totalRow = new TableRow();
            totalRow.CssClass="statsTableFooterRowStyle";
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells[0].CssClass = "statsTableFooterColumnStyle";
            totalRow.Cells[1].CssClass = "statsTableFooterNumColumnStyle";
            totalRow.Cells[0].Text = resmgr.GetString("text_activity_total");
			totalString = total.ToString();
            totalRow.Cells[1].Text = totalString;
            table.Rows.Add(totalRow);
            return table;
        }

        protected void BuildAgentsRow( TableRow row, StatisticsItem item, object unused )
        {
            Label agentLabel = new Label();
            string text = SiteUtilities.ClipString(item.identifier,80);
            agentLabel.Text = text;
            row.Cells[0].Controls.Add(agentLabel);
            row.Cells[1].Text = item.count.ToString();
        }

        protected void BuildReferrerRow( TableRow row, StatisticsItem item, object unused )
        {
            HyperLink link = new HyperLink();
            string text = SiteUtilities.ClipString(item.identifier,80);
            link.Text = System.Web.HttpUtility.HtmlEncode(text);
            link.NavigateUrl = item.identifier.ToString();
            row.Cells[0].Controls.Add(link);
            row.Cells[1].Text = item.count.ToString();
        }
		
		protected void BuildSearchesRow(TableRow row, StatisticsItem item, object unused)
		{
			HyperLink link = SiteUtilities.ParseSearchString(item.identifier);

			if (link != null)
			{
				row.Cells[0].Controls.Add(link);
				row.Cells[1].Text = item.count.ToString();
			}
		}

		protected void BuildUserDomainRow(TableRow row, StatisticsItem item, object unused)
		{
			Label userDomainLabel = new Label();
			string text = SiteUtilities.ClipString(item.identifier, 80);
			userDomainLabel.Text = text;
			row.Cells[0].Controls.Add(userDomainLabel);
			row.Cells[1].Text = item.count.ToString();
		}

        protected ArrayList GenerateSortedItemList( IDictionary dict )
        {
            ArrayList listItems = new ArrayList(dict.Count);
            foreach (DictionaryEntry de in dict)
            {
                listItems.Add(new StatisticsItem(de.Key.ToString(), (int)de.Value));
            }
            listItems.Sort(new StatisticsItem.Comparer());
            return listItems;
        }

		protected ArrayList GenerateSortedSearchStringItemList(IDictionary dict)
		{
			ArrayList listItems = new ArrayList(dict.Count);
			foreach (DictionaryEntry de in dict)
			{
				listItems.Add(new StatisticsItem(de.Key.ToString(), (int)de.Value));
			}
			listItems.Sort(new StatisticsItem.SearchStringComparer());
			return listItems;
		}
	}
}
