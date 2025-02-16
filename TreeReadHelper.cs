using System.Collections.Generic;
using System.Text;

namespace TreeReadHelper
{
    public class TreeReader
    {
        public static void ReadTree(Node.Node root, StringBuilder stringBuilder, Queue<Node.Node> queue)
        {
            if (root != null)
            {
                if (root.LeftChild != null)
                    ReadTree(root.LeftChild, stringBuilder, queue);
                if (root.RightChild != null)
                    ReadTree(root.RightChild, stringBuilder, queue);
                queue.Enqueue(root);
                if (root.NodeType == Node.Node.NodeTypes.Operator)
                    stringBuilder.Append((char)root.Value);
                else
                    stringBuilder.Append(root.Value);
            }
            return;
        }
    }
}
