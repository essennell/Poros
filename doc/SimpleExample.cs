using System;
using Poros;

public class Config
{
	public string Message;
	public int Count;
}

public static class SimpleExample
{
	public static void Execute( string [] args )
	{
		var settings = new UniversalConfiguration< Config >()
			.Install( new CmdLineHandler( args ) )
			.Config;

		for( int i = 0; i != settings.Count; ++i )
		{
			Console.WriteLine( settings.Message );
		}
	}
}
