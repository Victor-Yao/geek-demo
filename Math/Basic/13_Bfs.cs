using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Basic
{
    /** 
    * @Description: lesson 13 abd lesson 14 广度优先搜索
    **/
    public static class BreadthFirstSearch
    {
        public static List<string> Results = new List<string>();
        /** 
        * @Description: 列出定路径下所有的文件和目录，基于队列的广度优先遍历
        * @param: path - 给定的检索路径
        * @return: add files and directores into List
        * @conditions: 
        */
        public static void BfsDirectoryByQueue(string rootPath)
        {
            if (!Path.Exists(rootPath))
            {
                throw new DirectoryNotFoundException($"{rootPath} don't exist");
            }

            var queue = new Queue<string>();
            queue.Enqueue(rootPath);

            while (queue.Count != 0)
            {
                var path = queue.Dequeue();
                Results.Add(path);

                var attr = File.GetAttributes(path);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    foreach (var entry in Directory.EnumerateFileSystemEntries(path))
                    {
                        queue.Enqueue(entry);
                    }
                }
            }
        }

        /** 
        * @Description: 列出定路径下所有的文件和目录，基于栈的深度优先遍历
        * @param: path - 给定的检索路径
        * @return: add files and directores into List
        * @conditions: 
        */
        public static void DfsDirectoryByStack(string rootPath)
        {
            if (!Path.Exists(rootPath))
            {
                throw new DirectoryNotFoundException($"{rootPath} don't exist");
            }

            var stack = new Stack<string>();
            stack.Push(rootPath);
            while (stack.Count != 0)
            {
                var path = stack.Pop();
                Results.Add(path);

                var attr = File.GetAttributes(path);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    foreach (var entry in Directory.EnumerateFileSystemEntries(path))
                    {
                        stack.Push(entry);
                    }
                }
            }
        }

        /** 
        * @Description: 列出定路径下所有的文件和目录，利用递归实现，
        * @param: path - 给定的检索路径
        * @return: add files and directores into List
        * @conditions: 
        */
        public static void DfsDirectoryByRecursion(string path)
        {
            if (!Path.Exists(path))
            {
                throw new DirectoryNotFoundException($"{path} don't exist");
            }
            // Results.Add(path); //1. 先序遍历

            var attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                // recursive when directory is folder
                foreach (var item in Directory.EnumerateFileSystemEntries(path))
                {
                    DfsDirectoryByRecursion(item);
                }
            }

            Results.Add(path); //2. 后序遍历
        }
    }
}