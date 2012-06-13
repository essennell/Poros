using System;
using System.Collections.Generic;
using System.Linq;

namespace Poros
{

	public class CmdLineHandler : KeyValueHandler
	{

		public CmdLineHandler()
			: this( Environment.GetCommandLineArgs() )
		{
		}

		public CmdLineHandler( IEnumerable< string > args )
		{

			options = args.Select( arg => arg.Trim() )
				.Select( arg => arg.StartsWith( "--" ) ? LongFormat( arg ) : ShortFormat( arg ) )
				.ToDictionary( kv => kv[ 0 ], kv => kv.Length > 1 ? (object)kv[ 1 ].Trim( '\"' ) : "true", StringComparer.InvariantCultureIgnoreCase );

		}


		public IDictionary< string, object > Settings
		{
			get { return options; }
		}


		private static string[] LongFormat( string val )
		{
			
			return val.TrimStart( '-' ).Split( new[] { '=' }, 2 );

		}


		private static string[] ShortFormat( string val )
		{
			
			return val.TrimStart( '-', '/' ).Split( new[] { ':' }, 2 );

		}


		private readonly Dictionary< string, object > options;

	}

}
