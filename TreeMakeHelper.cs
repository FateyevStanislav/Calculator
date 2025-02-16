using System;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;

namespace TreeMakeHelper
{
    public class TreeMakeHelper
    {
        public static Node.Node GetTree(string str, ref int index)
        {
            var root = GetSum(str, ref index);
            if (str[index] != '$' || double.IsNaN(root.Value))
                throw new InvalidOperationException("Некорректный ввод");
            return root;
        }

        private static Node.Node GetSum(string str, ref int index)
        {
            var value1 = GetMul(str, ref index);
            while (str[index] == '+' || str[index] == '-')
            {
                var operation = str[index];
                index++;
                var value2 = GetMul(str, ref index);
                if (double.IsNaN(value2.Value))
                    return new Node.Node(double.NaN);
                if (double.IsNaN(value1.Value))
                {
                    if (operation == '+')
                        return new Node.Node(
                            Node.Node.NodeTypes.Operator,
                            '*',
                            new Node.Node(Node.Node.NodeTypes.Number, 1),
                            value2);
                    else
                        return new Node.Node(
                            Node.Node.NodeTypes.Operator,
                            '*',
                            new Node.Node(Node.Node.NodeTypes.Number, -1),
                            value2);
                }
                else
                {
                    value1 = new Node.Node(
                        Node.Node.NodeTypes.Operator,
                        operation,
                        value1,
                        value2);
                }
            }
            return value1;
        }

        private static Node.Node GetMul(string str, ref int index)
        {
            var value1 = GetPow(str, ref index);
            while (str[index] == '*' || str[index] == '/')
            {
                var operation = str[index];
                index++;
                var value2 = GetPow(str, ref index);
                if (double.IsNaN(value1.Value) || double.IsNaN(value2.Value))
                    return new Node.Node(double.NaN);
                value1 = new Node.Node(
                    Node.Node.NodeTypes.Operator,
                    operation,
                    value1,
                    value2);
            }
            return value1;
        }

        private static Node.Node GetPow(string str, ref int index)
        {
            var value1 = GetFunc(str, ref index);
            while (str[index] == '^')
            {
                index++;
                var value2 = GetFunc(str, ref index);
                if (double.IsNaN(value1.Value) || double.IsNaN(value2.Value))
                    return new Node.Node(double.NaN);
                value1 = new Node.Node(
                    Node.Node.NodeTypes.Operator,
                    '^',
                    value1,
                    value2);
            }
            return value1;
        }

        private static Node.Node GetFunc(string str, ref int index)
        {
            var operation = str[index];
            if (operation == 'l')
            {
                index += 2;
                var value1 = GetSum(str, ref index);
                index += 2;
                var value2 = GetSum(str, ref index);
                index++;
                return new Node.Node(
                    Node.Node.NodeTypes.Operator,
                    operation,
                    value1,
                    value2);
            }
            else if (operation == 's' || operation == 'c' || operation == 't')
            {
                index += 2;
                var value = GetSum(str, ref index);
                index++;
                return new Node.Node(
                    Node.Node.NodeTypes.Operator,
                    operation,
                    value,
                    null);
            }
            return GetBrackets(str, ref index);
        }

        private static Node.Node GetBrackets(string str, ref int index)
        {
            if (str[index] == '(')
            {
                index++;
                var value = GetSum(str, ref index);
                if (str[index] != ')')
                    return new Node.Node(double.NaN);
                index++;
                return value;
            }
            else
                return GetNumber(str, ref index);
        }

        private static Node.Node GetNumber(string str, ref int index)
        {
            var value = 0.0;
            var intPart = 0;
            var oldIndex = index;
            while (char.IsDigit(str[index]))
            {
                intPart = intPart * 10 + str[index] - '0';
                index++;
            }
            value = intPart;
            if (str[index] == '.' || str[index] == ',')
            {
                var fractionalPart = 0.0;
                var countDigitsAfterPont = 0;
                if (index == oldIndex)
                    return new Node.Node(double.NaN);
                index++;
                while (char.IsDigit(str[index]))
                {
                    fractionalPart = fractionalPart * 10 + str[index] - '0';
                    index++;
                    countDigitsAfterPont++;
                }
                value += fractionalPart / (Math.Pow(10, countDigitsAfterPont));
            }
            if (index == oldIndex)
                return new Node.Node(double.NaN);
            return new Node.Node(Node.Node.NodeTypes.Number, value);
        }
    }
}
