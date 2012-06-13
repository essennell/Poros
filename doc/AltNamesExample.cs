using System;
using Poros;

class AltNamesConfig
{
	[UniversalConfig( "M" )] 			
	public string Message;

	[UniversalConfig( "C", "Number" )]
	public int Count;
}

public static class AltNamesExample
{
	public static void Execute( string [] args )
	{
		var settings = new UniversalConfiguration< AltNamesConfig >()
			.Install( new CmdLineHandler( args ) )
			.Config;

		for( int i = 0; i != settings.Count; ++i )
		{
			Console.WriteLine( settings.Message );
		}
	}
}
