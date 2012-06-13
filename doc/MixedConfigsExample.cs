using System;
using Poros;

class MixedConfigsConfig
{
	[UniversalConfig( "M" )]
	public string Message;

	[UniversalConfig( "C", "Number" )]
	public int Count;

	public string Path;
}

public static class MixedConfigsExample
{
	public static void Execute( string [] args )
	{
		var settings = new UniversalConfiguration< MixedConfigsConfig >()
			.Install( new SystemEnvironmentHandler() )
			.Install( new AppConfigHandler() )
			.Install( new CmdLineHandler( args ) )
			.Config;

		for( int i = 0; i != settings.Count; ++i )
		{
			Console.WriteLine( settings.Message );
		}
		Console.WriteLine(settings.Path);
	}
}
