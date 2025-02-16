using System.Security.Cryptography.X509Certificates;

namespace Node
{
    public class Node
    {
        public enum NodeTypes
        {
            Number,
            Operator,
        }

        public NodeTypes NodeType;
        public double Value;
        public Node LeftChild;
        public Node RightChild;

        public Node()
        {
            
        }

        public Node(
            NodeTypes nodeType, 
            double value,
            Node leftChild,
            Node rightChild)
        {
            NodeType = nodeType;
            Value = value;
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        public Node(
            NodeTypes nodeType,
            double value)
        {
            NodeType = nodeType;
            Value = value;
        }

        public Node(double value)
        {
            Value = value;
        }
    }
}
