using Basic;

string[] words = { "geek", "geometry" };
var root = new TreeNode('\0', string.Empty, string.Empty);

var ret = DepthFirstSearch.BuildDictionaryTree(words[0].ToCharArray(), root);
if (ret == true)
{
    Console.WriteLine($"build tree for {words[0]} successed");
}

ret = DepthFirstSearch.BuildDictionaryTree(words[1].ToCharArray(), root);
if (ret == true)
{
    Console.WriteLine($"build tree for {words[1]} successed");
}

// var str = Lesson11.SearchDictTree("geek".ToCharArray(), root);
// var str = Lesson11.SearchDictTree_Recursion("geometry".ToCharArray(), root);

// Console.WriteLine($"result: {str}");

DepthFirstSearch.DfsByStack(root);