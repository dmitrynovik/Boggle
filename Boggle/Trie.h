#ifndef _TRIE_H_
#define _TRIE_H_

#include <vector>

class Node {
friend class Trie;
public:
    Node() : mContent(' '), mMarker(false) {  }
    ~Node() {}
    char content() { return mContent; }
    void setContent(char c) { mContent = c; }
    bool wordMarker() { return mMarker; }
    void setWordMarker() { mMarker = true; }
    Node* findChild(char c);
    void appendChild(Node* child) { mChildren.push_back(child); }
    std::vector<Node*> children() { return mChildren; }
private:
    char mContent;
    bool mMarker;
    std::vector<Node*> mChildren;
	void deleteChilren();
};

class Trie {
public:
    Trie();
    ~Trie();
    void addWord(const std::string& s);
    bool searchPath(const std::string& s);
    bool searchWord(const std::string& s);
	unsigned int getSize() const;
private:
    Node root;
	size_t nSize;
	Node* getNode(const std::string& s);
};

Node* Node::findChild(char c)
{
    for ( int i = 0; i < mChildren.size(); i++ )
    {
        Node* tmp = mChildren.at(i);
        if ( tmp->content() == c )
        {
            return tmp;
        }
    }
    return NULL;
}

void Node::deleteChilren()
{
	for (std::vector<Node*>::iterator it = mChildren.begin(); it != mChildren.end(); ++it)
	{
		(*it)->deleteChilren();
		delete (*it);
	}
}

Trie::Trie()
{
	nSize = 0;
}

Trie::~Trie()
{
	root.deleteChilren();
}

unsigned int Trie::getSize() const
{
	return nSize;
}

void Trie::addWord(const std::string& s)
{
    Node* current = &root;

    if (s.empty())
    {
        current->setWordMarker(); // an empty word
        return;
    }

    for ( int i = 0; i < s.length(); i++ )
    {        
        Node* child = current->findChild(s[i]);
        if ( child != NULL )
        {
            current = child;
        }
        else
        {
            Node* tmp = new Node();
            tmp->setContent(s[i]);
            current->appendChild(tmp);
            current = tmp;
        }

        if ( i == s.length() - 1 )
		{
			if (!current->wordMarker())
			{
				// do not add the same word twice:
				current->setWordMarker();
				nSize++;
			}
		}
    }
}

Node* Trie::getNode(const std::string& s)
{
    Node* current = &root;
    while ( current != NULL )
    {
        for ( int i = 0; i < s.length(); i++ )
        {
            Node* tmp = current->findChild(s[i]);
            if ( tmp == NULL )
                return NULL;
            current = tmp;
        }
		return current;
    }
    return NULL;
}

bool Trie::searchPath(const std::string& s)
{
	return getNode(s);
}

bool Trie::searchWord(const std::string& s)
{
	Node* n = getNode(s);
	return n && n->wordMarker();
}
#endif // _TRIE_H_
