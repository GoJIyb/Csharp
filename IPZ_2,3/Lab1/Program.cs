using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)//варіант 19
        {
            double Beta = 0.1;
            double x = Convert.ToDouble(args[0]);

            double b = (Math.Pow((Beta + x), -Math.E))
                - (Math.Pow(Math.Cos(Math.Pow(x, 4)), 3) + Math.Pow(Math.Tan(Math.Pow((Beta - 1), -2)), 4) - 0.03)
                / (6.51 + Math.Sqrt(Math.Abs(Beta - x)) + Math.Log(Math.Abs(x - 1)));   // Обчислення виразу
            b = x; //
            args[0] = b + "";
            Console.WriteLine("Пароль якщо забули: " + b);

        }
    }
}
