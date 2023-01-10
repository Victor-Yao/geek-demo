namespace Basic;

/** 
* @Desc: Aho-Corasick
*/
public class AC
{
    // public accessor for unit test
    public AcNode root = new AcNode('/');

    public void BuildFailurePointer()
    {
        var queue = new Queue<AcNode>();
        root.Fail = null;
        queue.Enqueue(root);
        while (queue.Count != 0)
        {
            AcNode p = queue.Dequeue();
            for (int i = 0; i < 26; i++)
            {
                AcNode pc = p.Children[i];
                if (pc == null)
                    continue;

                if (p == root)
                {
                    pc.Fail = root; // the Fail of root's child point to root  
                }
                else
                {
                    var q = p.Fail;
                    while (q != null)
                    {
                        AcNode qc = q.Children[pc.Data - 'a'];
                        if (qc != null)
                        { // pc == qc
                            pc.Fail = qc;
                            break;
                        }
                        q = q.Fail;
                    }
                    if (q == null)
                    {
                        pc.Fail = root;
                    }
                }
                queue.Enqueue(pc);
            }
        }
    }

    public void Match(string text) //main string
    {
        int n = text.Length;
        AcNode p = root;

        for (int i = 0; i < n; i++)
        {
            int idx = text[i] - 'a';
            while (p.Children[idx] == null && p != root)
            {
                p = p.Fail; // search in Failure pointer
            }

            p = p.Children[idx];
            if (p == null)
                p = root;

            AcNode tmp = p;
            while (tmp != root)
            {
                if (tmp.isEndingChar == true)
                {
                    int pos = i - tmp.Length + 1;
                    Console.WriteLine($"matched pos: {pos}; length: {tmp.Length}");
                }
                tmp = tmp.Fail;
            }
        }
    }

    public void BuildTrieTree(string[] words)
    {
        foreach (var word in words)
        {
            InsertWord(word);
        }
    }

    private void InsertWord(string word)
    {
        AcNode p = root;
        for (int i = 0; i < word.Length; i++)
        {
            int index = word[i] - 'a';
            if (p.Children[index] == null)
            {
                var newNode = new AcNode(word[i]);
                p.Children[index] = newNode;
            }

            p = p.Children[index];
        }
        p.Length = word.Length;
        p.isEndingChar = true;
    }
}

public class AcNode
{
    public char Data;
    public AcNode[] Children = new AcNode[26]; // string set only contains a~z
    public bool isEndingChar = false; //end char is true
    public int Length = -1;//the length of pattern string when isEndingChar =true
    public AcNode? Fail; // failure pointer
    public AcNode(char data)
    {
        Data = data;
    }
}