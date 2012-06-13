using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Poros.Test
{
	
	public class TestExtensions
	{
		
		[ Fact ]
		public void OverlayWith_selects_no_items_for_empty_collections()
		{

			var d1 = new Dictionary< int, int >();
			var d2 = new Dictionary< int, int >();

			var d3 = d1.OverlayWith( d2 );

			Assert.Equal( 0, d3.Count() );

		}


		[ Fact ]
		public void OverlayWith_returns_only_first_collection_when_other_is_empty()
		{

			var d1 = new Dictionary< int, int > { { 1, 1 } };
			var d2 = new Dictionary< int, int >();

			var d3 = d1.OverlayWith( d2 );

			Assert.True( d3.SequenceEqual( d1 ) );

		}


		[ Fact ]
		public void OverlayWith_returns_only_second_collection_when_source_is_empty()
		{

			var d1 = new Dictionary< int, int > { { 1, 1 } };
			var d2 = new Dictionary< int, int >();

			var d3 = d2.OverlayWith( d1 );

			Assert.True( d3.SequenceEqual( d1 ) );

		}


		[ Fact ]
		public void OverlayWith_overwrites_source_values_when_present_in_other()
		{

			var d1 = new Dictionary< int, int > { { 1, 1 } };
			var d2 = new Dictionary< int, int > { { 1, 10 } };

			var d3 = d1.OverlayWith( d2 );

			Assert.Equal( 1, d3.Count() );
			Assert.Equal( d2.First().Value, d3.First().Value );

		}


		[ Fact ]
		public void OverlayWith_leaves_values_in_place_when_not_present_in_other()
		{
			
			var d1 = new Dictionary< int, int > { { 1, 1 }, { 2, 2 } };
			var d2 = new Dictionary< int, int > { { 1, 10 } };

			var d3 = d1.OverlayWith( d2 );

			Assert.Equal( 2, d3.Count() );
			Assert.True( new Dictionary< int, int >{ { 1, 10 }, { 2, 2 } }.SequenceEqual( d3 ) );

		}


		[ Fact ]
		public void OverlayWith_adds_new_values_when_not_present_in_source()
		{
			
			var d1 = new Dictionary< int, int > { { 1, 1 } };
			var d2 = new Dictionary< int, int > { { 1, 10 }, { 2, 2 } };

			var d3 = d1.OverlayWith( d2 );

			Assert.Equal( 2, d3.Count() );
			Assert.True( new Dictionary< int, int >{ { 1, 10 }, { 2, 2 } }.SequenceEqual( d3 ) );

		}


		[ Fact ]
		public void OverlayWith_operates_case_insensitively()
		{

			var d1 = new Dictionary< string, int > { { "one", 1 } };
			var d2 = new Dictionary< string, int > { { "ONE", 10 } };

			var d3 = d1.OverlayWith( d2 );

			Assert.Equal( 1, d3.Count() );
			Assert.Equal( 10, d3.First().Value );

		}

	}

}
