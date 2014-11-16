#include <fstream>
#include <algorithm>
#include <set>
#include "Boggle.h"
#include "Trie.h"

Boggle::Boggle( const char * a_DictionaryFile )
{
	_trie = new Trie();

	std::ifstream in(a_DictionaryFile);
	if (in.is_open())
	{
		std::string line;
		while (getline(in, line))
		{
			_trie->addWord(line);
		}
		in.close();	
	}
}

Boggle::~Boggle()
{
	delete _trie;
}

unsigned int Boggle::GetSize() const
{
	return _trie->getSize();
}

void Boggle::Solve( const char* a_Grid, std::vector<std::string>& a_Results ) const
{
	if (!a_Grid) throw std::exception("Empty grid is not allowed");

	unsigned int letters = strlen(a_Grid);
	if (letters == 0) throw std::exception("Empty grid is not allowed");

	unsigned int squareSize = sqrt(letters);
	double intpart;
	if (modf(sqrt(letters), &intpart) > 0) throw std::exception("A square grid is expected (e.g. 2 x 2, 3 x 3, etc.");

	std::set<std::string> foundWords;
	// start from 1st to last in the grid using recursion:
	for (int i = 0; i < letters; i++)
	{
		std::vector<int> path;
		path.push_back(i);
		Solve(a_Grid, squareSize, foundWords, path);
	}
	
	for (std::set<std::string>::iterator it = foundWords.begin(); it != foundWords.end(); ++it)
		a_Results.push_back(*it);
}

void Boggle::Solve(const char* a_Grid, size_t squareSize, std::set<std::string>& found, std::vector<int>& path) const
{
	const size_t MIN_WORD_SIZE = 3;

	// reconstruct the current prefix string:
	std::string prefix;
	prefix.reserve(path.size());
	for (std::vector<int>::iterator it = path.begin(); it != path.end(); ++it)
		prefix += a_Grid[*it];

	if (_trie->searchWord(prefix)) 
	{
		// word found:
		if (prefix.length() >= MIN_WORD_SIZE)
			found.insert(prefix);
	}
	else if (!_trie->searchPath(prefix))
	{
		// wrong path, no use to continue:
		return;
	}

	// calculate next edges in the graph:
	std::vector<int> nextMoves;
	int currentPos = path.back();
	int col = currentPos % squareSize;

	if (col > 0) nextMoves.push_back(currentPos - squareSize - 1);
	nextMoves.push_back(currentPos - squareSize);
	if (col < (squareSize - 1)) nextMoves.push_back(currentPos - squareSize + 1);
	if (col > 0) nextMoves.push_back(currentPos - 1);
	if (col < (squareSize - 1)) nextMoves.push_back(currentPos + 1);
	if (col > 0) nextMoves.push_back(currentPos + squareSize - 1);
	nextMoves.push_back(currentPos + squareSize);
	if (col < (squareSize - 1)) nextMoves.push_back(currentPos + squareSize + 1);

	for (std::vector<int>::iterator it = nextMoves.begin(); it != nextMoves.end(); ++it)
	{
		int index = *it;
		// Filter out coordinates out of bounds and used indices:
		if (index >= 0 && index < (squareSize * squareSize))
			if(find(path.begin(), path.end(), index) == path.end())
			{
				std::vector<int> newPath = std::vector<int>(path.begin(), path.end());
				newPath.push_back(index);
				Solve(a_Grid, squareSize, found, newPath);
			}
	}
}
