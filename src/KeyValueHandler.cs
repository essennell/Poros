using System.Collections.Generic;

namespace Poros
{

	public interface KeyValueHandler
	{

		IDictionary< string, object > Settings { get; }

	}

}
