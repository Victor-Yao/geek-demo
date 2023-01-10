namespace Basic;

/** 
* @Desc: Trie Tree
*/
public class Trie
{
    private TrieNode root = new TrieNode('/');

    /// Summary: Insert a string into Trie tree
    public void Insert(string text)
    {
        TrieNode p = root;
        for (int i = 0; i < text.Length; i++)
        {
            int index = text[i] - 'a';
            if (p.Children[index] == null)
            {
                var newNode = new TrieNode(text[i]);
                p.Children[index] = newNode;
            }

            p = p.Children[index];
        }
        p.isEndingChar = true;
    }

    /// Summary: Find a string into Trie tree
    public bool Find(string pattern)
    {
        TrieNode p = root;
        for (int i = 0; i < pattern.Length; i++)
        {
            int index = pattern[i] - 'a';
            if (p.Children[index] == null)
            {
                return false; // non-existence
            }

            p = p.Children[index];
        }

        if (p.isEndingChar == false)
            return false; // only matched prefix, not full string
        else
            return true; // matched
    }
}

public class TrieNode
{
    public char Data { get; set; }
    public TrieNode[] Children { get; set; } = new TrieNode[26];
    public bool isEndingChar { get; set; } = false;
    public TrieNode(char data) => Data = data;
}

