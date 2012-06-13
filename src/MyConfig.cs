using System;
using System.Collections.Generic;
using System.Linq;

namespace Poros.Test
{

	public enum ServerType
	{
		Server, Worker
	}

	public class MyConfig
	{

		public string UseMyName;

		[ UniversalConfig ] public string Name;
		[ UniversalConfig ] public int Number;
		[ UniversalConfig ] public bool Present;
		[ UniversalConfig ] public short SmallNumber;
		[ UniversalConfig ] public double FloatingPoint;
		[ UniversalConfig ] public ServerType Role;

		[ UniversalConfig ] public string SetValue = "Hello";
	
		[ UniversalConfig( "Other" ) ] public string TestOtherName;
		[ UniversalConfig( "Short", "D" ) ] public string TestShortName;

		[ UniversalConfig ] public string DepSetting;
		[ UniversalConfig ] public string DepValue;
		[ UniversalConfig ] public string ActSetting;

	}


	public class DependentHandler : KeyValueHandler
	{

		public DependentHandler( IEnumerable< string > args, string key )
		{
			this.args = args.ToLookup( s => s.ToUpper(), s => key.ToUpper() );
			this.val = key;
		}
		

		public object Value( string key, object defaultValue )
		{

			return args.Contains( key ) ? args[ key ] : defaultValue;

		}


		public IDictionary< string, object > Settings
		{
			get { throw new NotImplementedException(); }
		}


		private readonly string val;
		private readonly ILookup< string, string > args;

	}

}
