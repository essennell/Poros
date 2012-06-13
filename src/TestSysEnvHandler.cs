using System;
using Xunit;

namespace Poros.Test
{

	public class TestSysEnvHandler
	{

		[ Fact ]
		public void String_value_can_be_retrieved()
		{
		
			var args = new SystemEnvironmentHandler();
			string key = "SystemDrive";

			Assert.Equal( Environment.GetEnvironmentVariable( key ), args.Settings[ key ] );

		}

	}

}
