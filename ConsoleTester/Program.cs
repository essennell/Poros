using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTester
{
	static class Program
	{
		static void Main( string [] args )
		{
			SimpleExample.Execute( args );
			DefaultValueExample.Execute( args );
			AltNamesExample.Execute( args );
			MixedConfigsExample.Execute( args );
			SettingsForSettings.Execute( args );
		}
	}
}
