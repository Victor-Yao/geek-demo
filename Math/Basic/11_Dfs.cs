using System.Collections;
using System.Collections.Generic;

namespace Basic
{
    public class TreeNode
    {
        public char? Label; //node name, the character of a word
        public Dictionary<char, TreeNode>? Sons;
        public string? Prefix;
        public string? Explaination;

        public TreeNode(char l, string pre, string exp)
        {
            Label = l;
            Prefix = pre;
            Explaination = exp;
            Sons = new Dictionary<char, TreeNode>();
        }
    }

    public static class DepthFirstSearch
    {
        /** 
        * @Description: 构建字典树 
        * @param:   chrArr - 未添加的字符串
                    parent - 已构建的字典树
        * @return: true - 构建成功；false - 构建失败
        * @conditions: if sons contains (chr)
        *   yes
        *       parent = sons
                add prefix
        *   no
        *       new a son node and add to parent
        *       setup prefix
        *       parent = sons
        *   last chr
        *       setup explaination
        */
        public static bool BuildDictionaryTree(char[] chrArr, TreeNode parent)
        {
            if (parent == null)
            {
                Console.WriteLine("Error: parent is null");
                return false;
            }

            if (chrArr == null)
            {
                Console.WriteLine("Error: chrArr is null");
                return false;
            }

            if (chrArr.Length == 0)
            {
                parent.Explaination = "has explaination";
                return true;
            }

            var c = chrArr[0];
            if (parent.Sons.ContainsKey(c))
            {
                // found
                parent = parent.Sons[c];
            }
            else
            {
                // didn't find in parent.Sons
                var son = new TreeNode(c, parent.Prefix + parent.Label, string.Empty);
                parent.Sons.Add(c, son);
                parent = parent.Sons[c];
            }

            var childArr = SubCharArray(chrArr, 0);
            return BuildDictionaryTree(childArr, parent);
        }

        /** 
        * @Description: 用递归实现深度优先遍历，查字典 
        * @param    chrArr-待查单词的字符数组
                    parent-父节点
        * @return 单词的解释; string.empty
        * @conditions: if(parent.Sons.Contain(c))
        *   contain
        *       word not end, parent = son
        *       word end 
        *           parent has exp, success
        *           parent has no exp, faied
        *   don't contain
        *       tree end but word not, fail
        *   tree end, word not end, faied
        */
        public static string DfsByRecursion(char[] chrArr, TreeNode parent)
        {
            if (parent == null)
            {
                Console.WriteLine("Error: parent is null");
                return string.Empty;
            }

            if (chrArr == null)
            {
                Console.WriteLine("Error: chrArr is null");
                return string.Empty;
            }

            var c = chrArr[0];
            if (parent.Sons.ContainsKey(c))
            {
                parent = parent.Sons[c];
                if (chrArr.Length > 1)
                {
                    // word not end, continue
                    var childArr = SubCharArray(chrArr, 0);
                    return DfsByRecursion(childArr, parent);
                }
                else
                {
                    // word end, check if node is leaf node
                    if (string.IsNullOrEmpty(parent.Explaination))
                    {
                        // parent has no exp, faied
                        return string.Empty;
                    }
                    else
                    {
                        // parent has exp, success
                        return parent.Explaination;
                    }
                }
            }
            else
            {
                //tree end but word not, fail
                return string.Empty;
            }
        }

        /** 
        * @Description: 用栈实现深度优先遍历，遍历字典树 
        * @param root - 根节点
        * @return 直接输出单词
        * @conditions: is leaf node? Yes, print; No, continue;
        */
        public static void DfsByStack(TreeNode root)
        {
            var stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                if (node.Sons.Count == 0)
                {
                    Console.WriteLine($"word: {node.Prefix + node.Label}");
                }
                else
                {
                    //stackTemp是为了保持遍历的顺序，和递归遍历的顺序是一致的
                    //如果不要求顺序一致，可以直接压入Stack
                    var stackTemp = new Stack<TreeNode>();
                    foreach (var son in node.Sons.Values)
                    {
                        stack.Push(son);
                    }

                    while (stackTemp.Count != 0)
                    {
                        stack.Push(stackTemp.Pop());
                    }
                }
            }
        }


        private static char[]? SubCharArray(char[] arr, int index)
        {
            if (arr == null)
            {
                Console.WriteLine("Error: arr of SubCharArray is null");
                return null;
            }

            if (index > arr.Length - 1)
            {
                Console.WriteLine("Error: index is out of array boundary");
                return null;
            }
            // {1},0
            // len = 0
            var len = arr.Length - index - 1;
            var childs = new char[len];
            for (int i = 0; i < len; i++)
            {
                childs[i] = arr[i + index + 1];
            }

            return childs;
        }
    }


}