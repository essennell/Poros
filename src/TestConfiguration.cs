using System;
using Xunit;

namespace Poros.Test
{

	public class TestConfiguration
	{

		[ Fact ]
		public void Unattributed_field_gets_set_from_field_name()
		{

			var args = new[] { "--UseMyName=Yes" };
			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args ) );
			MyConfig cfg = config.Config;

			const string expected = "Yes";

			Assert.Equal( expected, cfg.UseMyName );

		}


		[ Fact ]
		public void Setting_string_val_gets_value()
		{

			var args = new[] { "--Name=foo" };
			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args ) );
			MyConfig cfg = config.Config;

			const string expected = "foo";

			Assert.Equal( expected, cfg.Name );

		}


		[ Fact ]
		public void Setting_string_val_gets_value_ignoring_case()
		{
			
			var args = new[] { "--name=foo" };
			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args ) );
			MyConfig cfg = config.Config;

			const string expected = "foo";

			Assert.Equal( expected, cfg.Name );

		}


		[ Fact ]
		public void Setting_int_val_gets_value()
		{
		
			var args = new[] { "--Number=1" };
			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args ) );
			MyConfig cfg = config.Config;

			const int expected = 1;

			Assert.Equal( expected, cfg.Number );
		
		}


		[ Fact ]
		public void Setting_bool_val_gets_value()
		{
			
			var args = new[] { "--Present=true" };
			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args ) );
			MyConfig cfg = config.Config;

			const bool expected = true;

			Assert.Equal( expected, cfg.Present );

		}


		[Fact]
		public void Setting_double_val_gets_value()
		{

			var args = new[] { "--FloatingPoint=3.142" };
			var config = new UniversalConfiguration< MyConfig >().Install( new CmdLineHandler( args ) ).Config;

			Assert.Equal( Math.PI, config.FloatingPoint, 2 );

		}


		[Fact]
		public void Setting_enum_val_gets_value()
		{

			var args = new[] { "--Role=Worker" };
			var config = new UniversalConfiguration< MyConfig >().Install( new CmdLineHandler( args ) );

			Assert.Equal( ServerType.Worker, config.Config.Role );

		}


		[Fact]
		public void Invalid_enumeration_value_throws_exception()
		{
			
			var args = new[] { "--Role=NotPresent" };
			Assert.Throws< ArgumentException >( () => new UniversalConfiguration< MyConfig >().Install( new CmdLineHandler( args ) ).Config );

		}

		
		[Fact]
		public void Numeric_value_out_of_range_throws_exception()
		{

			var args = new[] { "--smallnumber=" + int.MaxValue.ToString() };
			Assert.Throws< OverflowException >( () => new UniversalConfiguration< MyConfig >().Install( new CmdLineHandler( args ) ).Config );

		}

	
		[ Fact ]
		public void Setting_int_val_with_bad_type_throws_error()
		{
			
			var args = new[] { "--Number=foo" };
			Assert.Throws< FormatException >( () => new UniversalConfiguration< MyConfig >().Install( new CmdLineHandler( args ) ).Config );

		}


		[ Fact ]
		public void Setting_with_value_set_in_config_retains_it()
		{
			
			var args = new[] { "--Defaulted=50" };
			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args ) );

			Assert.Equal( "Hello", config.Config.SetValue );

		}


		[ Fact ]
		public void Setting_with_value_set_in_config_gets_overwritten()
		{
			
			var args = new[] { "--SetValue=Changed" };
			var config = new UniversalConfiguration< MyConfig >().Install( new CmdLineHandler( args ) );
			
			Assert.Equal( "Changed", config.Config.SetValue );

		}


		[ Fact ]
		public void Setting_with_custom_name_correctly_set()
		{
			
			var args = new[] { "--Other=test" };
			var config = new UniversalConfiguration< MyConfig >().Install( new CmdLineHandler( args ) );

			Assert.Equal( "test", config.Config.TestOtherName );

		}


		[ Fact ]
		public void Setting_with_custom_name_correctly_set_case_insensitively()
		{

			var args = new[] { "--other=test" };
			var config = new UniversalConfiguration< MyConfig >().Install( new CmdLineHandler( args ) );

			Assert.Equal( "test", config.Config.TestOtherName );

		}


		[ Fact ]
		public void Setting_present_in_override_set_gets_overwritten()
		{
			var args1 = new[] { "--Name=foo" };
			var args2 = new[] { "--Name=bar" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args1 ) )
				.Install( new CmdLineHandler( args2 ) );

			Assert.Equal( "bar", config.Config.Name );

		}


		[ Fact ]
		public void Setting_not_present_in_override_set_remains_as_original()
		{

			var args1 = new[] { "--Name=foo" };
			var args2 = new[] { "--Number=1" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args1 ) )
				.Install( new CmdLineHandler( args2 ) );

			Assert.Equal( "foo", config.Config.Name );

		}


		[ Fact ]
		public void Setting_finds_first_alternate_name()
		{
			var args = new[] { "-D:foo " };

			var config = new UniversalConfiguration< MyConfig >().Install( new CmdLineHandler( args ) );

			Assert.Equal( "foo", config.Config.TestShortName );

		}


		[ Fact ]
		public void Forward_references_to_settings_in_later_handlers_gets_correct_value()
		{

			var args1 = new[] { "--depvalue=10" };
			var args2 = new[] { "--actsetting=$(depvalue)" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args2 ) )
				.Install( new CmdLineHandler( args1 ) )
				.Config;

			Assert.Equal( config.DepValue, config.ActSetting );

		}


		[ Fact ]
		public void Variable_references_get_substituted_with_value()
		{
			
			var args1 = new[] { "--depvalue=10" };
			var args2 = new[] { "--actsetting=$(depvalue)" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args1 ) )
				.Install( new CmdLineHandler( args2 ) )
				.Config;

			Assert.Equal( config.DepValue, config.ActSetting );

		}


		[ Fact ]
		public void Escaped_variable_references_retains_literal_value()
		{
			
			var args1 = new[] { "--depvalue=10" };
			var args2 = new[] { "--actsetting=\\$(depvalue)" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args1 ) )
				.Install( new CmdLineHandler( args2 ) )
				.Config;

			Assert.Equal( "$(depvalue)", config.ActSetting );

		}


		[ Fact ]
		public void Variable_references_in_single_item_get_substituted_with_value()
		{
			
			var args1 = new[] { "--depvalue=10", "--actsetting=$(depvalue)" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args1 ) )
				.Config;

			Assert.Equal( config.DepValue, config.ActSetting );

		}


		[ Fact ]
		public void Multiple_Variable_References_Get_Assigned_From_Correct_Values()
		{

			var args1 = new[] { "--Name=thename", "--actsetting=actual", "--depvalue=$(name)_$(actsetting)" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args1 ) )
				.Config;

			Assert.Equal( "thename_actual", config.DepValue );

		}


		[ Fact ]
		public void Multi_level_variable_references_resolve_to_final_value()
		{
			
			var args1 = new[] { "--Name=thename", "--actsetting=$(name)", "--depvalue=$(actsetting)" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args1 ) )
				.Config;

			Assert.Equal( "thename", config.DepValue );

		}


		[ Fact(Timeout = 100) ]
		public void Circular_variable_references_cause_exception()
		{
			
			var args1 = new[] { "--depvalue=$(actsetting)" };
			var args2 = new[] { "--actsetting=$(depvalue)" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args2 ) )
				.Install( new CmdLineHandler( args1 ) );

			Assert.Throws< ArgumentException >( () => config.Config );

		}


		[ Fact(Timeout = 100) ]
		public void Multilevel_circular_variable_references_cause_exception()
		{
			
			var args1 = new[] { "--depvalue=$(actsetting)" };
			var args2 = new[] { "--actsetting=$(depsetting)" };
			var args3 = new[] { "--depsetting=$(depvalue)" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args3 ) )
				.Install( new CmdLineHandler( args2 ) )
				.Install( new CmdLineHandler( args1 ) );

			Assert.Throws< ArgumentException >( () => config.Config );

		}


		[ Fact(Timeout = 100) ]
		public void Multilevel_nested_circular_variable_references_cause_exception()
		{
			
			var args1 = new[] { "--depvalue=$(actsetting)", "--actsetting=$(depsetting)", "--depsetting=$(actsetting)" };

			var config = new UniversalConfiguration< MyConfig >()
				.Install( new CmdLineHandler( args1 ) );

			Assert.Throws< ArgumentException >( () => config.Config );

		}

	}

}
