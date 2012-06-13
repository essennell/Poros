using System;
using Xunit;

namespace Poros.Test
{

	public class TestCmdLineHandler
	{

		[ Fact ]
		public void Value_can_be_retrieved_in_long_format()
		{
		
			var args = new CmdLineHandler( new[] { "--test=yes" } );
			Assert.Equal( "yes", args.Settings[ "test" ] );

		}


		[ Fact ]
		public void Value_can_be_retrieved_in_short_format()
		{
			
			var args = new CmdLineHandler( new[] { "-test:yes" } );
			Assert.Equal( "yes", args.Settings[ "test" ] );

		}


		[ Fact ]
		public void Value_can_be_retrieved_in_short_format_with_slash()
		{
			
			var args = new CmdLineHandler( new[] { "/test:yes" } );
			Assert.Equal( "yes", args.Settings[ "test" ] );

		}


		[ Fact ]
		public void Value_is_empty_if_no_value_in_string()
		{
			
			var args = new CmdLineHandler( new[] { "--test=" } );
			Assert.Equal( "", args.Settings[ "test" ] );

		}


		[ Fact ]
		public void Value_is_true_if_no_assignment_after_key()
		{
			
			var args = new CmdLineHandler( new[] { "--test" } );
			Assert.Equal( "true", args.Settings[ "test" ] );

		}


		[ Fact ]
		public void Value_can_be_enclosed_in_quotes_to_embed_spaces()
		{
			
			var args = new CmdLineHandler( new[] { "--test=\"foo bar\"" } );
			Assert.Equal( "foo bar", args.Settings[ "test" ] );

		}


		[ Fact ]
		public void Value_can_contain_embedded_equals()
		{
			
			var args = new CmdLineHandler( new[] { "--test=foo=bar" } );
			Assert.Equal( "foo=bar", args.Settings[ "test" ] );

		}


		[ Fact ]
		public void Handler_uses_environment_if_no_cmdline_provided()
		{
			
			var args = new CmdLineHandler();

		}


		[ Fact ]
		public void Handler_trims_leading_spaces_in_long_format()
		{
			
			var args = new CmdLineHandler( new[] { " --test=foo" } );
			Assert.Equal( "foo", args.Settings[ "test" ] );

		}


		[ Fact ]
		public void Handler_trims_trailing_spaces_in_long_format()
		{
			
			var args = new CmdLineHandler( new[] { "--test=foo " } );
			Assert.Equal( "foo", args.Settings[ "test" ] );

		}


		[ Fact ]
		public void Handler_trims_leading_spaces_in_short_format()
		{

			var args = new CmdLineHandler( new[] { " -t:foo" } );
			Assert.Equal( "foo", args.Settings[ "t" ] );

		}


		[ Fact ]
		public void Handler_trims_trailing_spaces_in_short_format()
		{
			
			var args = new CmdLineHandler( new[] { "-t:foo " } );
			Assert.Equal( "foo", args.Settings[ "t" ] );

		}


		[Fact]
		public void Duplicate_parameter_keys_are_handled_gracefully()
		{
			
			Assert.Throws< ArgumentException >( () => new CmdLineHandler( new[] { "-t:foo", "--t=foobar" } ) );

		}

	}

}
