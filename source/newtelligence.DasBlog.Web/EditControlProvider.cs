using System;
using newtelligence.DasBlog.Runtime;
using newtelligence.DasBlog.Web.Core;
using System.Web;

namespace newtelligence.DasBlog.Web
{
    /// <summary>
    /// Responsible for creating edit controls
    /// </summary>
    public class EditControlProvider
    {
        public EditControlProvider()
        {
            // ...
        }

        /// <summary>
        /// Creates and returns instance of a <see cref="Core.EditControlAdapter"/> based on the site configuration.
        /// </summary>
        /// <returns>An instance of <see cref="Core.EditControlAdapter"/></returns>
        public Core.EditControlAdapter CreateEditControl()
        {
            SiteConfig siteConfig = SiteConfig.GetSiteConfig();
            string configuredEditControl = siteConfig.EntryEditControl;

            Core.EditControlAdapter editControl = null;

            if (configuredEditControl != null && configuredEditControl.Length > 0)
            {
                try
                {
                    Type editControlType = Type.GetType(configuredEditControl, /* throwOnError */ true, /*ignoreCase*/ false);
                    if (editControlType != null)
                    {
                        //TODO: it might be better to cache the ConstructorInfo reference in a static field

                        editControl = Activator.CreateInstance(editControlType) as Core.EditControlAdapter;
                    }
                }
                catch (Exception e)
                {
                    // prevents the editentry page from failing when the configured editor is not (or no longer) supported
                    ILoggingDataService loggingService = LoggingDataServiceFactory.GetService(SiteConfig.GetLogPathFromCurrentContext());
                    loggingService.AddEvent(new EventDataItem(EventCodes.Error, "Failed to load configured editor.", HttpContext.Current.Request.Url.ToString()));
                }
            }

            // if the configured control cannot be loaded, default to the plain textbox, since that doesn't depend on external assemblies.
            if (editControl == null)
            {
                editControl = new TextBoxAdapter();
            }
            return editControl;
        }
    }
}
