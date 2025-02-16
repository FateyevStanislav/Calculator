using System;
using System.Collections.Generic;
using System.Text;
using TreeReadHelper;

namespace PostfixExpressionHelper
{
    public class PostfixExpressionHelper
    {
        private static Queue<Node.Node> Queue = new Queue<Node.Node>();

        public static string MakePostfixExpression(string str)
        {
            var index = 0;
            var tree = TreeMakeHelper.TreeMakeHelper.GetTree(str + '$', ref index);
            var stringBuilder = new StringBuilder();
            TreeReader.ReadTree(tree, stringBuilder, Queue);
            return stringBuilder.ToString();
        }

        public static double Calculate()
        {
            var result = 0.0;
            var a = Queue.Count;
            while (Queue.Count != 0)
            {
                if (Queue.Count < a)
                {
                    var value = Queue.Dequeue();
                    if (value.NodeType == Node.Node.NodeTypes.Operator)
                    {
                        switch (value.Value)
                        {
                            case 's':
                                result = Math.Sin(result);
                                break;
                            case 'c':
                                result = Math.Cos(result);
                                break;
                            case 't':
                                result = Math.Tan(result);
                                break;
                        }
                    }
                    else
                    {
                        var op = Queue.Dequeue().Value;
                        switch (op)
                        {
                            case '+':
                                result += value.Value;
                                break;
                            case '-':
                                result -= value.Value;
                                break;
                            case '*':
                                result *= value.Value;
                                break;
                            case '/':
                                result /= value.Value;
                                break;
                            case '^':
                                result = Math.Pow(result, value.Value);
                                break;

                        }
                    }
                }
                else
                {
                    var value1 = Queue.Dequeue();
                    var value2 = Queue.Dequeue();
                    if (value2.NodeType == Node.Node.NodeTypes.Operator)
                    {
                        switch (value2.Value)
                        {
                            case 's':
                                result += Math.Sin(value1.Value);
                                break;
                            case 'c':
                                result += Math.Cos(value1.Value);
                                break;
                            case 't':
                                result += Math.Tan(value1.Value);
                                break;
                        }
                    }
                    var operation = Queue.Dequeue().Value;
                    switch (operation)
                    {
                        case '+':
                            result += value1.Value + value2.Value;
                            break;
                        case '-':
                            result += value1.Value - value2.Value;
                            break;
                        case '*':
                            result += value1.Value * value2.Value;
                            break;
                        case '/':
                            result += value1.Value / value2.Value;
                            break;
                        case '^':
                            result += Math.Pow(value1.Value, value2.Value);
                            break;
                        case 'l':
                            result += Math.Log(value2.Value, value1.Value);
                            break;
                    }
                }
            }
            return result;
        }
    }
}
