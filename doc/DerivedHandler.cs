using System.Collections.Generic;
using Poros;


class DerivedHandler : KeyValueHandler
{
	public DerivedHandler( string path )
	{
		this.path = path;
	}

	public IDictionary< string, object > Settings
	{
		get { return new Dictionary< string, object >{ { "Filename", string.Format( "{0}/filename", path ) } }; }
	}

	readonly string path;
}