using System;


namespace Lab9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Одиничні лямбда-вирази 
            Console.Write("Введите x: ");
            double inputX = Convert.ToDouble(Console.ReadLine()); 

            Func<double, double> F1 = value => 4 * value - 1; 
            Func<double, double> F2 = value => 25 * value + 10; 

            double result = inputX <= 0 ? F1(inputX) : F2(inputX);
            Console.WriteLine($"F({inputX}) = {result}");


            //2. Одиничні лямбда-вирази в делегаті Funс
            Console.Write("Введіть довжину першого катета: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введіть довжину другого катета: ");
            double b = Convert.ToDouble(Console.ReadLine());

            Func<double, double, double> area = (x, y) => 0.5 * x * y;

            double triangleArea = area(a, b);
            Console.WriteLine($"Площа трикутника: {triangleArea}");



            //3. Одиничні лямбда-вирази в делегаті Predicate
            Console.Write("Введіть рядок: ");
            string input = Console.ReadLine();

            Predicate<string> isNumber = str => double.TryParse(str, out _);

            if (isNumber(input))
            {
                Console.WriteLine($"{input} є числом.");
            }
            else
            {
                Console.WriteLine($"{input} не є числом.");
            }



            //4. Блокові лямбда-вирази
            Console.Write("Введіть перший символ: ");
            char c1 = Console.ReadKey().KeyChar;
            Console.Write("Введіть другий символ: ");
            char c2 = Console.ReadKey().KeyChar;
            Console.Write("Введіть третій символ: ");
            char c3 = Console.ReadKey().KeyChar;

            
            Func<char, char, char, bool> isItNumber = (ch1, ch2, ch3) =>
            {
                string combined = $"{ch1}{ch2}{ch3}";
                return double.TryParse(combined, out _);
            };

            Console.WriteLine($"Утворюють число: {isItNumber(c1, c2, c3)}");
        }
    }
}
