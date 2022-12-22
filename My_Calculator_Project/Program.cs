using System.Globalization;

namespace My_Calculator_Project
{
    public static class Program
    {
        public static void Main() { }
    }



    public class Operation
    {
        public double Sum(double x, double y) { return x + y; }
        public double Def(double x, double y) { return x - y; }
        public double Mul(double x, double y) { return x * y; }
        public double Div(double x, double y) {
            if (y==0) throw new InvalidOperationException("Выражение содержит деление на 0");
            return x / y; }
    }



    public class Validator
    {
        //Проверка на подобные (*,(/,+),-),/),*)
        //проверка на ----,*+,/+,
        private const string operands = "+-/*()";
        private const string numbers = "0123456789.";

        public string Validator_String(string expression)
        {
            expression = expression.Replace(" ", "");
            expression = expression.Replace("--", "+");
            
            //++ -> +,/+ -> /,*+ -> *
            // (8)1=81
            int count_staples = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (!(operands.Contains(expression[i])) && !(numbers.Contains(expression[i])))
                    throw new InvalidOperationException("Выражение содержит недопустимые символы");
                if (expression[i] == '(') count_staples++;
                if (expression[i] == ')') count_staples--;
                if (i < expression.Length-1)
                {
                    if (expression[i] == '*' && expression[i + 1] == '*') throw new InvalidOperationException("Выражение записано неправильно");
                    if (expression[i] == '/' && expression[i + 1] == '/') throw new InvalidOperationException("Выражение записано неправильно");
                    if (expression[i] == '(' && expression[i + 1] == '/') throw new InvalidOperationException("Выражение записано неправильно");
                    if (expression[i] == '(' && expression[i + 1] == '*') throw new InvalidOperationException("Выражение записано неправильно");
                    if (expression[i] == '/' && expression[i + 1] == ')') throw new InvalidOperationException("Выражение записано неправильно");
                    if (expression[i] == '*' && expression[i + 1] == ')') throw new InvalidOperationException("Выражение записано неправильно");
                    if (expression[i] == '-' && expression[i + 1] == ')') throw new InvalidOperationException("Выражение записано неправильно");
                    if (expression[i] == '+' && expression[i + 1] == ')') throw new InvalidOperationException("Выражение записано неправильно");
                    if (expression[i] == '+' && expression[i + 1] == '+') { expression = expression.Replace("++", "+"); i--; }
                }
            }
            /* это дожен делать не валидатор так как 1/(1-1)=INF
            if (expression.IndexOf("/0")!=-1)
                throw new InvalidOperationException("Выражение содержит деление на 0");
            /**/
            if (count_staples != 0)
                throw new InvalidOperationException("Выражение записано неправильно");
            return expression;
        }
    }



    public class Parser
    {
        public string FindSubsetExpression(string expression)
        {
            int count_staples = 0;
            int open_staples = -1;
            for (int i = 0; i < expression.Length; i++)
            {
                if (open_staples == -1 && expression[i] == '(')
                    open_staples = i;
                if (expression[i] == '(') count_staples++;
                if (expression[i] == ')') count_staples--;
                if (open_staples != -1 && count_staples == 0 && expression[i] == ')')
                    return expression.Substring(open_staples + 1, i - open_staples - 1);
            }
            return "";
        }

        private const string operands = "+-/*";
        public double Calculation(string expression)
        {
            Operation cal = new();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            double result = 0;
            double accumulator = 0;
            double temp;
            char operand_accumulator = '+';
            char operand_last = '+';
            string string_argument = "";

            Console.WriteLine(expression);

            for (int i = 0; i < expression.Length; i++)
            {

                if (operands.Contains(expression[i]))
                {
                    if (string_argument != "") temp = double.Parse(string_argument);
                    else temp = 0;
                    string_argument = "";

                    switch (operand_last)
                    {
                        case '+':
                        case '-':
                            if (operand_accumulator == '+') result = cal.Sum(result, accumulator); //result += accumulator;
                            if (operand_accumulator == '-') result = cal.Def(result, accumulator); //result -= accumulator; 
                            accumulator = temp;
                            operand_accumulator = operand_last;
                            operand_last = expression[i];
                            break;

                        case '*':
                        case '/':
                            if ((expression[i] == '-' || expression[i] == '+') && (expression[i - 1] == '/' || expression[i - 1] == '*'))
                            {
                                if (expression[i] == '-') accumulator = cal.Mul(accumulator, -1); //accumulator*=-1;
                                operand_last = expression[i - 1];
                            }
                            else
                            {
                                if (operand_last == '*') accumulator = cal.Mul(accumulator, temp); //accumulator*=temp;
                                if (operand_last == '/') accumulator = cal.Div(accumulator, temp); //accumulator/=temp;
                                operand_last = expression[i];
                            }
                            break;
                    }
                    Console.WriteLine("\t" + " >" + result.ToString() + "< " + operand_accumulator + " {" + accumulator.ToString() + "} " + operand_last + " <" + temp.ToString() + "> " + expression[i..]); //expression.Substring(i, expression.Length - i) -> expression[i..]
                }
                else
                    string_argument += expression[i];
            }

            temp = double.Parse(string_argument);

            switch (operand_last)
            {
                case '+':
                case '-':
                    if (operand_accumulator == '+') result = cal.Sum(result, accumulator); //result += accumulator;
                    if (operand_accumulator == '-') result = cal.Def(result, accumulator); //result -= accumulator; 
                    accumulator = temp;
                    operand_accumulator = operand_last;
                    break;
                case '*':
                    accumulator = cal.Mul(accumulator, temp); //accumulator *= temp;
                    break;
                case '/':
                    accumulator = cal.Div(accumulator, temp); //accumulator /= temp;
                    break;
            }
            if (operand_accumulator == '+') result = cal.Sum(result, accumulator); //result += accumulator;
            if (operand_accumulator == '-') result = cal.Def(result, accumulator); //result -= accumulator;
            //Console.WriteLine(expression+"="+result.ToString());
            return result;
        }

        public double ParserExperession(string expression)
        {
            Validator valid = new();
            expression = valid.Validator_String(expression);
            string subset_expression = FindSubsetExpression(expression);
            double result;
            while (subset_expression != "")
            {
                //(8)1=81
                //Console.WriteLine("In " + expression + " find substring " + subset_expression);
                result = ParserExperession(subset_expression);
                expression = expression.Replace("(" + subset_expression + ")", result.ToString());
                expression = valid.Validator_String(expression);
                subset_expression = FindSubsetExpression(expression);
            }
            return Calculation(expression);
        }
    }
}







