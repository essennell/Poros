using System.Text.RegularExpressions;
using Xunit;

namespace Poros.Test
{

	public class TestAppConfigHandler
	{

		[ Fact ]
		public void Default_application_config_can_be_retrieved()
		{

			var args = new AppConfigHandler();
			Assert.Equal( "The App", args.Settings[ "AppSetting" ] );

			var m = Regex.Match( "foo$(bar)", @"\$\((.+?)\)" );
			var v = m.Groups[ 1 ].Value;
			Assert.True( m.Success );

		}

	}

}
