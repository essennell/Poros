using System;
using System.Collections.Generic;

namespace Poros
{

	public class UniversalConfigAttribute : Attribute
	{

		public UniversalConfigAttribute( params string [] names )
		{

			Names = new List< string >( names );

		}


		public List< string > Names { get; private set; }

	}

}
