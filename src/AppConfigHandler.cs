using System;
using System.Collections.Generic;
using System.Configuration;

namespace Poros
{

	public class AppConfigHandler : KeyValueHandler
	{

		public AppConfigHandler()
		{

			Configuration app =	ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );
			Configuration local = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.PerUserRoamingAndLocal );
			Configuration roaming = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.PerUserRoaming );

			var map = new ExeConfigurationFileMap();
			map.ExeConfigFilename = app.FilePath;
			map.RoamingUserConfigFilename = roaming.FilePath;
			map.LocalUserConfigFilename = local.FilePath;

			var cfg = ConfigurationManager.OpenMappedExeConfiguration( map, ConfigurationUserLevel.None );

			this.settings = new Dictionary< string, object >( StringComparer.InvariantCultureIgnoreCase );

			foreach( var setting in cfg.AppSettings.Settings.AllKeys )
			{
				settings.Add( setting, cfg.AppSettings.Settings[ setting ].Value );
			}

		}


		public IDictionary< string, object > Settings
		{
			get { return settings; }
		}


		private readonly Dictionary< string, object > settings;

	}

}
