using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Poros
{

	public class UniversalConfiguration< ConfigType > where ConfigType : class, new()
	{

		public UniversalConfiguration()
		{
			keys = new Dictionary< string, object >( StringComparer.InvariantCultureIgnoreCase );
		}


		public ConfigType Config
		{
			get
			{
				return ApplySettings();
			}
		}


		public UniversalConfiguration< ConfigType > Install( KeyValueHandler handler )
		{

			keys = keys.OverlayWith( handler.Settings );
			return this;

		}


		private ConfigType ApplySettings()
		{

			var config = new ConfigType();
			var settings = ExpandReferences();

			foreach( var field in config.GetType().GetFields() )
			{

				var value = settings.ContainsKey( field.Name ) ? settings[ field.Name ] : field.GetValue( config );

				foreach( UniversalConfigAttribute setting in field.GetCustomAttributes( typeof( UniversalConfigAttribute ), true ) )
				{
					foreach( var name in setting.Names.Where( settings.ContainsKey ) )
					{
						value = settings[ name ];
					}
				}

				field.SetValue( config,
					field.FieldType.IsEnum
						? Enum.Parse( field.FieldType, value.ToString(), true )
						: Convert.ChangeType( value, field.FieldType ) );

			}

			return config;

		}


		private Dictionary< string, object > ExpandReferences()
		{

			var reset = keys.ToDictionary( k => k.Key, v => v.Value, StringComparer.InvariantCultureIgnoreCase );

			var match = new Regex( @"(?<!\\)\$\((.+?)\)" );

			foreach( var setting in keys )
			{

				var current = setting.Value.ToString();
				var done = false;

				var seen = new HashSet< string >();
				while( !done )
				{
					var groups = match.Matches( current );
					done = groups.Count == 0;
					foreach( Match g in groups )
					{
						var reference = g.Groups[ 1 ].Value;
						if( seen.Contains( reference ) )
							throw new ArgumentException("Referred value already defined {0}", reference );

						seen.Add( reference );
						if( reset.ContainsKey( reference ) )
							current = match.Replace( current, matched => reset[ reference ].ToString(), 1 );
					}
				}

				current = Regex.Replace( current, @"\\(\$\(.+?\))", matched => matched.Groups[ 1 ].Value );
				reset[ setting.Key ] = current;

			}

			return reset;

		}


		private IEnumerable< KeyValuePair< string, object > > keys;

	}

}
