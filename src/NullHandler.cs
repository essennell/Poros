using System.Collections.Generic;

namespace Poros
{

	public class NullHandler : KeyValueHandler
	{

		public IDictionary< string, object > Settings
		{
			get { return new Dictionary< string, object >(); }
		}

	}

}
