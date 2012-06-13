using System;
using Poros;

class RootConfig
{
	public string Path;
}
class AppSettings
{
	public string Filename;
	public string User;
}


public static class SettingsForSettings
{
	public static void Execute( string [] args )
	{
		var rootArgs = new[] { "--Path=/foo/bar", "--User=local" };
			
		var rootConfig = new UniversalConfiguration< RootConfig >()
			.Install( new CmdLineHandler( rootArgs ) )
			.Config;

		var settings = new UniversalConfiguration< AppSettings >()
			.Install( new DerivedHandler( rootConfig.Path ) )
			.Install( new CmdLineHandler( rootArgs ) )
			.Config;

		Console.WriteLine(settings.Filename);
		Console.WriteLine(settings.User);
	}
}


