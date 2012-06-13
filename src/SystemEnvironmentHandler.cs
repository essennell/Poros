using System;
using System.Collections;
using System.Collections.Generic;

namespace Poros
{

	public class SystemEnvironmentHandler : KeyValueHandler
	{

		public SystemEnvironmentHandler()
		{

			vars = new Dictionary< string, object >( StringComparer.InvariantCultureIgnoreCase );
			foreach( DictionaryEntry kv in Environment.GetEnvironmentVariables() )
			{
				vars.Add( ( string )kv.Key, kv.Value );
			}

		}


		public IDictionary< string, object > Settings
		{
			get { return vars; }
		}


		private readonly Dictionary< string, object > vars;

	}

}
