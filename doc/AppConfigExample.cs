using System;
using Poros;

public static class AppConfigExample
{
	public static void Execute()
	{
		var settings = new UniversalConfiguration< Config >()
			.Install( new AppConfigHandler() )
			.Config;

		for( int i = 0; i != settings.Count; ++i )
		{
			Console.WriteLine( settings.Message );
		}
	}
}
