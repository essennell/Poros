using System;
using System.Collections.Generic;
using System.Linq;

namespace Poros
{

	public static class CollectionExtensions
	{
	
		public static IEnumerable< KeyValuePair< string, Value > > OverlayWith< Value >( this IEnumerable< KeyValuePair< string, Value > > source, IEnumerable< KeyValuePair< string, Value > > other )
		{

			return other.Union( source ).ToLookup( item => item.Key, StringComparer.InvariantCultureIgnoreCase ).Select( item => item.First() );

		}

		public static IEnumerable< KeyValuePair< Key, Value > > OverlayWith< Key, Value >( this IEnumerable< KeyValuePair< Key, Value > > source, IEnumerable< KeyValuePair< Key, Value > > other )
		{

			return other.Union( source ).ToLookup( item => item.Key ).Select( item => item.First() );

		}

	}

}
