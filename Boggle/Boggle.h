#ifndef BOGGLE_H
#define BOGGLE_H

#include <string>
#include <vector>
#include <set>

class Trie;

// internally stores a dictionary of words and can then return a list of
// all of the dictionary's words found in a given boggle grid
class Boggle
{
public:

	// takes the given filepath and loads a dictionary file from it
	Boggle( const char * a_DictionaryFile );

	~Boggle();

	// returns the number of words in the dictionary
	unsigned int GetSize() const;

	// finds all words in the given grid and puts them in the results vector
	void Solve( const char * a_Grid, std::vector< std::string > & a_Results ) const;

private:
	Trie* _trie;
	void Solve(const char* a_Grid, size_t gridSize, std::set<std::string>& found, std::vector<int>& path) const;
};

#endif //BOGGLE_H
