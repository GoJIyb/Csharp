using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Необхідно сворити додаток за допомогою якого можна буде імітувати роботу обмінника валют.

namespace Lab2
{
    class Program
    {
        static void Main(string[] args) // Point of enter
        {
            #region Password

            Random rnd = new Random();
            int x = rnd.Next(3, 20);

            string[] truePassw = { x.ToString() };//вірний пароль
            Lab1.Program.Main(truePassw);
            Console.Write("Ключ = " + x + ". Пароль = ");//ключ = x. Введіть пароль
            double customerPassw = Convert.ToDouble(Console.ReadLine());//пароль, отриманий від клієнта

            if (Convert.ToDouble(truePassw[0]) == customerPassw)        //дія при вірному паролю
            {
                Computer computer = new Computer();             // комп'ютер
                Employee employee = new Employee(computer);     // працівник
                Customer customer = new Customer();             // клієнт
                customer.StartExchange(employee);               // перехід в клас обмінника
            }
            else
            {
                Console.WriteLine("Вибачайте пароль не вiрний, будь ласка покиньте примiщення");//дія при не вірному паролю
            }
            #endregion
        }

    }
    class Customer //клієнт
    {

        #region Properties
        string _name = "Студент Студент Студентович";
        #endregion

        #region Employee
        public void StartExchange(Employee employee)    //початок обміну
        {
            int punct;
            double customerStartMoney;      //початкові гроші клієнта
            double? customerResultMoney;    //результуючі гроші клієнта
            string[] names = { "Андрiй Курдза", "Валерiй Вишнивецький", "Давид Шевченко" };
            
            Console.WriteLine();
            foreach (var employee1 in names) 
            {
                Console.WriteLine("На робочому мiсцi " + employee1);
            };
            Console.WriteLine();

            employee.Question1();       //питання 1
            punct = Convert.ToInt32(Console.ReadLine());
            switch (punct)
            {
                case 0:
                    break;
                case 1:     // гривні доллари
                    employee.Question2("гривень в доллари", ref names);    //питання 2
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("grn to dollar", customerStartMoney);
                    if (customerResultMoney != null) {
                    Console.WriteLine(_name + " дав " + customerStartMoney + " гривень i обмiняв їх на " + customerResultMoney + " долларiв");
                    Console.ReadLine();
                    }
                    break;
                case 2:     // гривні євро
                    employee.Question2("гривень в євро", ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("grn to euro", customerStartMoney);
                    if (customerResultMoney != null) {
                    Console.WriteLine(_name + " дав " + customerStartMoney + " гривень i обмiняв їх на " + customerResultMoney + " євро");
                    Console.ReadLine();
                    }
                    break;
                case 3:     // доллари гривні
                    employee.Question2("долларiв в гривнi", ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("dollar to grn", customerStartMoney);
                    if (customerResultMoney != null) {
                    Console.WriteLine(_name + " дав " + customerStartMoney + " долларiв i обмiняв їх на " + customerResultMoney + " гривень ");
                    Console.ReadLine();
                    }
                    break;
                case 4:     // євро гривні
                    employee.Question2("євро в гривнi", ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("euro to grn", customerStartMoney);
                    if (customerResultMoney != null) {
                    Console.WriteLine(_name + " дав " + customerStartMoney + " євро i обмiняв їх на " + customerResultMoney + " гривень");
                    Console.ReadLine();
                    }
                    break;
                case 5:     // леї гривні
                    employee.Question2("леїв в гривнi", ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("lei to grn", customerStartMoney);
                    if (customerResultMoney != null) {
                    Console.WriteLine(_name + " дав " + customerStartMoney + " леїв i обмiняв їх на " + customerResultMoney + " гривень");
                    Console.ReadLine();
                    }
                    break;

                case 6:     // гривні леї
                    employee.Question2("гривень в леї", ref names);
                    customerStartMoney = Convert.ToDouble(Console.ReadLine());
                    customerResultMoney = employee.Exchange("grn to lei", customerStartMoney);
                    if (customerResultMoney != null) {
                    Console.WriteLine(_name + " дав " + customerStartMoney + " гривнiв i обмiняв їх на " + customerResultMoney + " леїв");
                    Console.ReadLine();
                    }
                    break;
                default:
                    Console.WriteLine("Ви зробили неправильний вибip, будь ласка спробуйте все спочатку");      //це неможливо
                    Console.ReadLine();
                    return;
            }
        }
        #endregion
    }
    class Employee //працівник обмінника
    {
        #region Properties //властивості
        public string _name;
        Computer _computer; 

        float? _grnAmount = 999999;          // кількість гривень в касі
        double? _dollarAmount = 9999;        // кількість доларів в касі
        double? _euroAmount = 9999;          // кількість євро в касі
        double? _leiAmount = 9999;           // кількість леїв в касі
        #endregion

        #region Methods
        public Employee(Computer computer)
        {
            _computer = computer;
        }
        public Employee(Computer computer,string name )
        {
            _computer = computer;
            _name = name;
        }
        public double? Exchange(string currencyOut, double customerStartMoney)
        {
            double? resultAmount = _computer.Exchange(currencyOut, customerStartMoney);
            switch (currencyOut)
            {
                case "grn to dollar":
                    if (_dollarAmount - resultAmount > 0)
                    {
                        _dollarAmount -= resultAmount;
                        _grnAmount += (float)customerStartMoney;
                    }
                    else{
                        Console.WriteLine("Вибачайте в касi недостатньо долларiв, вам повертають " + customerStartMoney + " гривень");
                        Console.ReadLine();
                        return null;
                    }
                    break;

                case "grn to euro":
                    if (_euroAmount - resultAmount > 0 )
                    {
                        _euroAmount -= resultAmount;
                        _grnAmount += (float)customerStartMoney;
                    }
                    else {
                        Console.WriteLine("Вибачайте в касi недостатньо євро, вам повертають " + customerStartMoney + " гривень");
                        Console.ReadLine();
                        return null;
                    }
                    break;

                case "dollar to grn":
                    if (_grnAmount - resultAmount > 0)
                    { 
                        _grnAmount -= (float)resultAmount;
                        _dollarAmount += customerStartMoney;
                    }
                    else {
                        Console.WriteLine("Вибачайте в касi недостатньо гривень, вам повертають " + customerStartMoney + " долларiв");
                        Console.ReadLine();
                        return null;
                    }
                    break;

                case "euro to grn":
                    if (_grnAmount - resultAmount > 0)
                    {
                        _grnAmount -= (float)resultAmount;
                        _euroAmount += customerStartMoney;
                    }
                    else {
                        Console.WriteLine("Вибачайте в касi недостатньо гривень, вам повертають " + customerStartMoney + " євро");
                        Console.ReadLine(); 
                        return null;
                    }
                    break;

                case "lei to grn":
                    if (_grnAmount - resultAmount > 0)
                    {
                        _grnAmount -= (float)resultAmount;
                        _leiAmount += customerStartMoney;
                    }
                    else {
                        Console.WriteLine("Вибачайте в касi недостатньо гривень, вам повертають " + customerStartMoney + " леїв"); 
                        Console.ReadLine();
                        return null;
                    }
                    break;

                case "grn to lei":
                    if (_leiAmount - resultAmount > 0)
                    {
                        _grnAmount += (float)resultAmount;
                        _leiAmount -= customerStartMoney;
                    }
                    else {
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
        public string GetName() { return _name; }
        public void Question1()
        {
            Console.WriteLine("Виберiть пункт:");                    //виберіть пункт
            Console.WriteLine("0. вийти з обмiнника");               //завершення обміну
            Console.WriteLine("1. обмiняти гривнi на доллари");      //обміняти гривні на долари
            Console.WriteLine("2. обмiняти гривнi на євро");         //обміняти гривні на євро
            Console.WriteLine("3. обмiняти доллари на гривнi");      //обміняти долари на гривні
            Console.WriteLine("4. обмiняти євро на гривнi");         //обміняти євро на гривні
            Console.WriteLine("5. обмiняти долари на гривнi");       //обміняти долари на гривні
            Console.WriteLine("6. обмiняти євро на гривнi");         //обміняти євро на гривні
            



        }
        public void Question2(string currencyOut, ref string[] names)
        {
            Random rnd = new Random();
           
            string name = names[rnd.Next(0, names.Length - 1)];

            Console.WriteLine("З вами працюватиме " + name + ". ");                     //працівник для вас.
            Console.WriteLine("Скiльки " + currencyOut + " ви хочете обмiняти?");   //Скільки currencyOut Ви хочете обміняти
        }
        #endregion

    }
    class Computer //комп'ютер
    {
        #region Properties
        double _dollarRateSell = 38.6;      //ціна продажу
        double _dollarRateBuy = 38.5;       //ціна купівлі
        double _euroRateSell = 39.9;        //ціна продажу
        double _euroRateBuy = 39.8;         //ціна купівлі
        double _leiRateSell = 14.7;         //ціна продажу
        double _leiRateBuy = 14.6;          //ціна купівлі

        public Computer(double dollarRateSell = 41.6, double dollarRateBuy = 41.5, double euroRateSell = 40.9, double euroRateBuy = 40.8, double leiRateSell = 1.9,double leiRateBuy = 1.8)
        {
            _dollarRateSell = dollarRateSell;
            _dollarRateBuy = dollarRateBuy;
            _euroRateSell = euroRateSell;
            _euroRateBuy = euroRateBuy;
            _leiRateBuy = leiRateBuy;
            _leiRateSell = leiRateSell;
        }

                #endregion

        #region Methods
        public double? Exchange(string currencyOut, double customerStartMoney)
        {
            switch (currencyOut)
            {
                case "grn to dollar":
                    return customerStartMoney / _dollarRateBuy;
                case "grn to euro":
                    return customerStartMoney / _euroRateBuy;
                case "dollar to grn":
                    return customerStartMoney * _dollarRateSell;
                case "euro to grn":
                    return customerStartMoney * _euroRateSell;
                case "lei to grn":
                    return customerStartMoney * _leiRateSell;
                case "grn to lei":
                    return customerStartMoney / _leiRateBuy;

                default:
                    return null;
            }
        }
        #endregion
    }
}
