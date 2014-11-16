// test2.cpp : Defines the entry point for the console application.
//

#include "Boggle.h"
#include "UnitTest++/src/UnitTest++.h"
#include "UnitTest++/src/TestReporterStdout.h"
#include <vector>

static Boggle s_Boggle( "crosswd.txt" );

TEST( InputSize )
{
	CHECK_EQUAL( s_Boggle.GetSize(), 113809 );
}

TEST( 2x2Test )
{
	const char * strs[] = {
		"ape", "apes", "apse", "asp", "pas", "pase", "pea",
		"peas", "pes", "sae", "sap", "sea", "spa", "spae"
    };

	std::vector< std::string > expected( strs, strs + 14 );
	std::vector< std::string > results;
	s_Boggle.Solve( "pesa", results );

	CHECK_EQUAL( 14, results.size() );
	CHECK( expected == results );
}

TEST( 3x3Test )
{
	const char * strs[] = {
		"des", "due", "dues", "dug", "dugs", "duke", "ems", "emu",
		"emus", "gude", "gudes", "gum", "gummed", "gums", "kue",
		"kues", "mem", "mems", "mud", "muds", "mug", "mugs", "mum",
		"mums", "mus", "muse", "mused", "sedum", "smug", "sue",
		"sued", "suede", "sum", "summed", "uke", "use", "used"
    };

	std::vector< std::string > expected( strs, strs + 37 );
	std::vector< std::string > results;
	s_Boggle.Solve( "mmgeuskde", results );

	CHECK_EQUAL( 37, results.size() );
	CHECK( expected == results );
}

int main( int argc, const char * argv[])
{
	return UnitTest::RunAllTests();
}

