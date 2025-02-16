using System;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                "Выражение вводить в инфиксной записи, без пробелов.\n" +
                "Отделитель дробной части - . или ," +
                "Умножение - *\n" +
                "Деление - /\n" +
                "Логарифм - l()()\n" +
                "Синус - s()\n" +
                "Косинус - c()\n" +
                "Тангенс - t()\n");
            Console.Write("Введите математическое выражение : ");
            var str = Console.ReadLine();
            Console.WriteLine("Постфиксная запись вашего выражения : {0}",
                PostfixExpressionHelper.
                PostfixExpressionHelper.
                MakePostfixExpression(str));
            Console.WriteLine("Результат вашего выражения : {0}",
                PostfixExpressionHelper.
                PostfixExpressionHelper.
                Calculate());
        }
    }
}
