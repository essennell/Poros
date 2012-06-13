namespace Poros
{

	public static class SystemConfiguration< ConfigType > where ConfigType : class, new()
	{

		public static UniversalConfiguration< ConfigType > Config
		{

			get
			{
				return new UniversalConfiguration< ConfigType >()
					.Install( new SystemEnvironmentHandler() )
					.Install( new AppConfigHandler() )
					.Install( new CmdLineHandler() );
			}

		}

	}

}
