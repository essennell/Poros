using System;
using Poros;

class DefaultValueConfig
{
	public string Message = "Hi";
	public int Count = 5;
}

public class DefaultValueExample
{
	public static void Execute( string [] args )
	{
		var settings = new UniversalConfiguration< DefaultValueConfig >()
			.Install( new CmdLineHandler( args ) )
			.Config;

		for( int i = 0; i != settings.Count; ++i )
		{
			Console.WriteLine( settings.Message );
		}
	}
}
