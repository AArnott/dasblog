using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;

namespace newtelligence.DasBlog.Web.Core
{

    public class TitleMapperModule : IHttpModule
    {

        /// <summary>
        ///  A description of the regular expression:
        ///  
        ///  [Year]: A named capture group. [(?:\d{4})]
        ///      Match expression but don't capture it. [\d{4}]
        ///          Any digit, exactly 4 repetitions
        ///  /
        ///  [Month]: A named capture group. [\d{1,2}]
        ///      Any digit, between 1 and 2 repetitions
        ///  /
        ///  [Day]: A named capture group. [\d{1,2}]
        ///      Any digit, between 1 and 2 repetitions
        /// </summary>
        static readonly Regex _yearMonthDayPattern =
            new Regex(@"(?<Year>(?:\d{4}))/(?<Month>\d{1,2})/(?<Day>\d{1,2})",
                      RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace |
                      RegexOptions.Compiled);


        //AGB - 10 June 2008
        static readonly string[] HttpHandlerStrings = TryGetExclusions();

        //AGB - 10 June 2008 - helper method to initialize the HttpHandlerStrings array - falling back to the 
        //original values if the new TitleMapperModuleSectionHandler configuration section is not present.
        private static string[] TryGetExclusions()
        {
            string[] exclusions;

            if (TitleMapperModuleSectionHandler.Settings != null && TitleMapperModuleSectionHandler.Settings.Exclusions.Count > 0)
            {
                exclusions = new string[TitleMapperModuleSectionHandler.Settings.Exclusions.Count];
                for (int i = 0; i < exclusions.Length; i++)
                {
                    exclusions[i] = TitleMapperModuleSectionHandler.Settings.Exclusions[i].Path;
                }
            }
            else
            {
                exclusions = new string[]
			    {
				    "CaptchaImage.aspx",
				    "aggbug.ashx",
				    "blogger.aspx",
				    "pingback.aspx",
				    "trackback.aspx",
				    "get_aspx_ver.aspx"                    
			    };
            }

            return exclusions;
        }

        #region IHttpModule Members
        public void Init(HttpApplication context)
        {
            SiteConfig config = SiteConfig.GetSiteConfig();
            if (config.EnableTitlePermaLink)
            {
                context.BeginRequest += HandleBeginRequest;
            }
        }

        public void Dispose()
        {
        }
        #endregion

        static void HandleBeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (app == null)
            {
                return;
            }

            string requestUrl = app.Context.Request.Url.LocalPath;
            if (requestUrl.Contains(",") || requestUrl.Contains("?"))
            {
                return;
            }

            // TODO: there has to be a way to also see if the request is from an HttpHander since File.Exists won't work.
            if (!requestUrl.EndsWith(".aspx", StringComparison.InvariantCultureIgnoreCase) ||
                File.Exists(app.Context.Server.MapPath(app.Context.Request.FilePath)))
            {
                return;
            }

            string title = app.Context.Request.Url.Segments[app.Context.Request.Url.Segments.Length - 1];

            if (Array.Exists(HttpHandlerStrings, delegate(string x)
            {
                // use case insensitive compare to check for match
                return String.Compare(x, title, StringComparison.OrdinalIgnoreCase) == 0;
            }))
            {
                return;
            }

            title = title.Replace(".aspx", "");
            title = title.Replace("+", "");
            title = title.Replace(" ", "");
            title = title.Replace("%2b", "");
            title = title.Replace("%20", "");

            string requestParams = String.Format("title={0}", title);

            // Try to be more specific by matching the date pattern.
            Match match = _yearMonthDayPattern.Match(app.Context.Request.Url.LocalPath);
            if (match.Groups.Count == 4)
            {
                try
                {
                    int year = Convert.ToInt32(match.Groups["Year"].Value);
                    int month = Convert.ToInt32(match.Groups["Month"].Value);
                    int day = Convert.ToInt32(match.Groups["Day"].Value);

                    DateTime dateTime = new DateTime(year, month, day);
                    if (dateTime != DateTime.MinValue)
                    {
                        requestParams = String.Format("{0}&date={1}", requestParams, dateTime.ToString("yyyy-MM-dd"));
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            // Append original query to the list of parameters, skip the leading "?".
            if (!String.IsNullOrEmpty(app.Context.Request.Url.Query))
            {
                requestParams = String.Format("{0}&{1}", requestParams, app.Context.Request.Url.Query.Substring(1));
            }

            SiteConfig config = SiteConfig.GetSiteConfig();
            if (config.EnableComments && config.ShowCommentsWhenViewingEntry)
            {
                requestUrl = String.Format("~/CommentView.aspx?{0}", requestParams);
            }
            else
            {
                requestUrl = String.Format("~/Permalink.aspx?{0}", requestParams);
            }

            app.Context.RewritePath(requestUrl);
        }
    }
}