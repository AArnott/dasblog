using System;
using System.Collections;

namespace newtelligence.DasBlog.Runtime
{
	public static class LoggingDataServiceFactory
	{
		static readonly Hashtable services = new Hashtable();

		public static ILoggingDataService GetService(string logLocation)
		{
			ILoggingDataService service;

			lock (services.SyncRoot)
			{
				service = services[logLocation.ToUpper()] as ILoggingDataService;
				if (service == null)
				{
					service = new LoggingDataServiceXml(logLocation);
					services.Add(logLocation.ToUpper(), service);
				}
			}
			return service;
		}
	}
}