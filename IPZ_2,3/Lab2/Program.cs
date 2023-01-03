using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Необхідно сворити додаток за допомогою якого можна буде імітувати роботу обмінника валют.

namespace Lab2
{
    class Program
    {
            public enum Services
        {
            EXIT,
            UAHTOUSD,
            UAHTOEUR,
            USDTOUAH,
            EURTOUAH,
            LEITOUAH,
            UAHTOLEI
        }

        static void Main(string[] args) // Point of enter
        {
            #region Password
                Computer computer = new Computer();             // комп'ютер
                Employee employee = new Employee(computer);     // працівник
                Customer customer = new Customer();             // клієнт
                customer.StartExchange(employee);               // перехід в клас обмінника
            #endregion
        }

    }
    class Customer //клієнт
    {

        #region Properties
        string name = "Студент Студент Студентович";
        #endregion

        #region Employee
        public void StartExchange(Employee employee)    //початок обміну
        {
            int punct;
            double customerStartMoney;      //початкові гроші клієнта
            double? customerResultMoney;    //результуючі гроші клієнта
            string[] names = { "Андрiй Курдза", "Валерiй Вишнивецький", "Давид Шевченко" };
            Console.WriteLine();

            employee.Question1("Виберiть пункт:", "0. вийти з обмiнника", "1. обмiняти гривнi в доллари", "2. обмiняти гривнi в євро",
                "3. обмiняти доллари в гривнi", "4. обмiняти євро в гривнi", "5. обмiняти леї в гривнi", "6. обмiняти гривнi в леї"); //питання 1

            Console.WriteLine();
            for (int i=0; i < names.Length; i++)
            {
                Console.WriteLine("На робочому мiсцi " + names[i]);
            };
            Console.WriteLine();

            employee.Question1();       // відповідь на питання 1
            punct = Convert.ToInt32(Console.ReadLine());
            switch (punct)
            {
                case 0:
                    break;
                case 1:     // гривні доллари
                    employee.Question2(Convert.ToString(Program.Services.UAHTOUSD), ref names);    //питання 2
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("grn to dollar", customerStartMoney);
                    if (customerResultMoney != null) {
                        Console.WriteLine();
                        Console.WriteLine(name + " дав " + customerStartMoney + " гривень i обмiняв їх на " + customerResultMoney + " долларiв");
                        Console.ReadLine();
                    }
                    break;
                case 2:     // гривні євро
                    employee.Question2(Convert.ToString(Program.Services.UAHTOEUR), ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("grn to euro", customerStartMoney);
                    if (customerResultMoney != null) {
                        Console.WriteLine();
                        Console.WriteLine(name + " дав " + customerStartMoney + " гривень i обмiняв їх на " + customerResultMoney + " євро");
                        Console.ReadLine();
                    }
                    break;
                case 3:     // доллари гривні
                    employee.Question2(Convert.ToString(Program.Services.USDTOUAH), ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("dollar to grn", customerStartMoney);
                    if (customerResultMoney != null) {
                        Console.WriteLine();
                        Console.WriteLine(name + " дав " + customerStartMoney + " долларiв i обмiняв їх на " + customerResultMoney + " гривень ");
                        Console.ReadLine();
                    }
                    break;
                case 4:     // євро гривні
                    employee.Question2(Convert.ToString(Program.Services.EURTOUAH), ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("euro to grn", customerStartMoney);
                    if (customerResultMoney != null) {
                        Console.WriteLine();
                        Console.WriteLine(name + " дав " + customerStartMoney + " євро i обмiняв їх на " + customerResultMoney + " гривень");
                        Console.ReadLine();
                    }
                    break;
                case 5:     // леї гривні
                    employee.Question2(Convert.ToString(Program.Services.LEITOUAH), ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("lei to grn", customerStartMoney);
                    if (customerResultMoney != null) {
                        Console.WriteLine();
                        Console.WriteLine(name + " дав " + customerStartMoney + " леїв i обмiняв їх на " + customerResultMoney + " гривень");
                        Console.ReadLine();
                    }
                    break;

                case 6:     // гривні леї
                    employee.Question2(Convert.ToString(Program.Services.UAHTOLEI), ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("grn to lei", customerStartMoney);
                    if (customerResultMoney != null) {
                        Console.WriteLine();
                        Console.WriteLine(name + " дав " + customerStartMoney + " гривнiв i обмiняв їх на " + customerResultMoney + " леїв");
                        Console.ReadLine();
                    }
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Ви зробили неправильний вибip, будь ласка спробуйте все спочатку");      //це неможливо
                    Console.ReadLine();
                    return;
            }
        }
        #endregion
    }
    class Employee //працівник обмінника
    {
        #region Properties
        public string name;
        Computer _computer;

        float? grnAmount = 999999;          // кількість гривень в касі
        double? dollarAmount = 9999;        // кількість доларів в касі
        double? euroAmount = 9999;          // кількість євро в касі
        double? leiAmount = 9999;           // кількість леїв в касі
        #endregion

        #region Methods
        public Employee(Computer computer)
        {
            _computer = computer;
        }
        public Employee(Computer computer, string name)
        {
            _computer = computer;
            this.name = name;
            
        }
        public double? Exchange(string currencyOut, double customerStartMoney)
        {
            double? resultAmount = _computer.Exchange(currencyOut, customerStartMoney);
            switch (currencyOut)
            {
                case "grn to dollar":
                    if (dollarAmount - resultAmount > 0)
                    {
                        dollarAmount -= resultAmount;
                        grnAmount += (float)customerStartMoney;
                    }
                    else {
                        Console.WriteLine();
                        Console.WriteLine("Вибачайте в касi недостатньо долларiв, вам повертають " + customerStartMoney + " гривень");
                        Console.ReadLine();
                        return null;
                    }
                    break;

                case "grn to euro":
                    if (euroAmount - resultAmount > 0)
                    {
                        euroAmount -= resultAmount;
                        grnAmount += (float)customerStartMoney;
                    }
                    else {
                        Console.WriteLine();
                        Console.WriteLine("Вибачайте в касi недостатньо євро, вам повертають " + customerStartMoney + " гривень");
                        Console.ReadLine();
                        return null;
                    }
                    break;

                case "dollar to grn":
                    if (grnAmount - resultAmount > 0)
                    {
                        grnAmount -= (float)resultAmount;
                        dollarAmount += customerStartMoney;
                    }
                    else {
                        Console.WriteLine();
                        Console.WriteLine("Вибачайте в касi недостатньо гривень, вам повертають " + customerStartMoney + " долларiв");
                        Console.ReadLine();
                        return null;
                    }
                    break;

                case "euro to grn":
                    if (grnAmount - resultAmount > 0)
                    {
                        grnAmount -= (float)resultAmount;
                        euroAmount += customerStartMoney;
                    }
                    else {
                        Console.WriteLine();
                        Console.WriteLine("Вибачайте в касi недостатньо гривень, вам повертають " + customerStartMoney + " євро");
                        Console.ReadLine();
                        return null;
                    }
                    break;

                case "lei to grn":
                    if (grnAmount - resultAmount > 0)
                    {
                        grnAmount -= (float)resultAmount;
                        leiAmount += customerStartMoney;
                    }
                    else {
                        Console.WriteLine();
                        Console.WriteLine("Вибачайте в касi недостатньо гривень, вам повертають " + customerStartMoney + " леїв");
                        Console.ReadLine();
                        return null;
                    }
                    break;

                case "grn to lei":
                    if (leiAmount - resultAmount > 0)
                    {
                        grnAmount += (float)resultAmount;
                        leiAmount -= customerStartMoney;
                    }
                    else {
                        Console.WriteLine();
                        Console.WriteLine("Вибачайте в касi недостатньо леїв, вам повертають " + customerStartMoney + " гривень");
                        Console.ReadLine();
                        return null;
                    }
                    break;

                default:
                    return null;
            }



            return resultAmount;
        }
        public string GetName() { return name; }

        public void Question1(params string[] questions )
        {
            foreach (string question in questions)
            {
                Console.WriteLine(question);
            }
        }

        public void Question2(string currencyOut, ref string[] names)
        {
            Random rnd = new Random();

            string name = names[rnd.Next(0, names.Length - 1)];
            Console.WriteLine();
            Console.WriteLine("З вами працюватиме " + name + ". ");                     //працівник для вас.
            Console.WriteLine("Скiльки " + currencyOut + " ви хочете обмiняти?");       //Скільки currencyOut Ви хочете обміняти
        }
        #endregion

    }    

    class Computer //комп'ютер
    {

        #region Properties

        const double dollarRateBuy = 38.5;       //ціна купівлі
        const double dollarRateSell = 38.6;      //ціна продажу
        const double euroRateSell = 39.9;        //ціна продажу
        const double euroRateBuy = 39.8;         //ціна купівлі
        const double leiRateSell = 14.7;         //ціна продажу
        const double leiRateBuy = 14.6;          //ціна купівлі

        #endregion

        #region Methods
        static public double? Exchange(string currencyOut, double customerStartMoney)
        {
            switch (currencyOut)
            {
                case "grn to dollar":
                    return Math.Round(customerStartMoney / dollarRateBuy, 2);
                case "grn to euro":
                    return Math.Round(customerStartMoney / euroRateBuy, 2);
                case "dollar to grn":
                    return Math.Round(customerStartMoney * dollarRateSell, 2);
                case "euro to grn":
                    return Math.Round(customerStartMoney * euroRateSell, 2);
                case "lei to grn":
                    return Math.Round(customerStartMoney * leiRateSell, 2);
                case "grn to lei":
                    return Math.Round(customerStartMoney / leiRateBuy, 2);

                default:
                    return null;
            }
        }
        #endregion
    }


}
