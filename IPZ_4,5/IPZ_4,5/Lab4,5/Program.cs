using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab5
{
    public enum Sportswear // 19 магазин одягу
    {
        Nike,
        Puma,
        Adidas
    }

    class Program
    {
        static void Main(string[] args)
        {
            SellingsList clients = SellingsList.GetInstance();
            Customer customer = new Customer();
            Seller seller = new Seller();
            Consult consult = new Consult();
            Dialogues dialouges = new Dialogues(seller);

            dialouges.Begin();
            dialouges.Welcome();
            dialouges.Ask1();

            Sportswear clothesname = dialouges.Answer1();
            seller.Order(clothesname);
            consult.TakeOrder(clothesname);

            Clothes clothes = consult.Choose();
            seller.Return(clothes);
            seller.Check(clothes);
            customer.Pay(clothes, clothesname);

            Console.ReadKey();
        }
    }

    abstract class Person
    {
        #region Properties
        public Random _rnd = new Random();
        #endregion

        internal Program Program 
        {
            get => default;
            set
            {
            }
        }
    }

    class Customer : Person
    {
        public string name = "Buyer";
        public int Money = 3000;
        #region Methods 
        public void Pay()
        {
            Console.WriteLine($"The {name} has {Money} UAH in his wallet");
        }
        public void Pay(Clothes clothes, Sportswear swName)
        {
            this.Money = Money - clothes.Price;
            if (Money >= 0)
            {
            Console.WriteLine($"The {name} paid {clothes.Price} UAH");
            Console.WriteLine($"The {name} has {Money} UAH remained");
            }
            else
            {
                this.Money = clothes.Price / 10;
                Console.WriteLine($"The {name} took an installment plan for the goods and will pay {Money} UAH within 10 months");
            }
            SellingsList.GetInstance().AddSelling(clothes.Price.ToString());
            var swpkg = new SportsWearPackage().MakePackage(clothes, new Package("Red"));
            Console.WriteLine(name + " received " + swpkg.pkg.Color + " a package with his goods from " + swName.ToString());
        }
        #endregion
    }
    abstract class Employer : Person
    {
        public enum Names
        {
            Kiril,
            Ilia,
            Danilo,
            Sofia,
            Diana,
            Polina
        }

        public string Name = "";
        public Employer()
        {
            Random rnd = new Random();
            var names = (Names)rnd.Next(0, 5);
            Name = names.ToString();
        }

        public Clothes LetsWork(Sportswear _ordername)
        {
            switch (_ordername)
            {
                case Sportswear.Nike:
                    Nike nike = new Nike();
                    Console.WriteLine($"Search...");
                    return nike;
                case Sportswear.Puma:
                    Puma puma = new Puma();
                    Console.WriteLine($"Search...");
                    return puma;
                case Sportswear.Adidas:
                    Adidas adidas = new Adidas();
                    Console.WriteLine($"Search...");
                    return adidas;
                default:
                    return null;

            }
        }
    }
    class Seller : Employer
    {

        #region Construct
        public Seller() { }

        #endregion
        #region Methods
        public void Order(Sportswear clothesname)
        {
            Clothes _clothes;
            switch (clothesname)
            {
                case Sportswear.Nike:
                    _clothes = new Nike();
                    Console.WriteLine($"Your order is a sports suit Nike {_clothes.Years}, in stock, price:{_clothes.Price} UAH");
                    break;
                case Sportswear.Puma:
                    _clothes = new Puma();
                    Console.WriteLine($"Your order is a sports suit Puma {_clothes.Years}, in stock, price:{_clothes.Price} UAH");
                    break;
                case Sportswear.Adidas:
                    _clothes = new Adidas();
                    Console.WriteLine($"Your order is a sports suit Adidas {_clothes.Years}, in stock, price:{_clothes.Price} UAH");
                    break;
            }
            Console.WriteLine($"The seller gives the order to the consultant and submits the order");
        }
        public Clothes Return(Clothes clothes)
        {
            Console.WriteLine($"{Name} brings your order");
            return clothes;
        }
        public void Check(Clothes clothes)
        {
            Console.WriteLine($"You owe {clothes.Price} UAH");
        }
        #endregion

    }
    class Dialogues
    {
        Seller _seller;

        public void Begin()
        {
            Customer customer = new Customer();
            Console.WriteLine($"A {customer.name} enters a clothing store, he has {customer.Money} UAH in his wallet");
        }

        public Dialogues(Seller seller)
        {
            _seller = seller;
        }
        public void Welcome()
        {
            Console.WriteLine($"Good afternoon, my name is {_seller.Name}, which brand do you like?");
        }
        public void Ask1()
        {
            Console.WriteLine("1. Nike");
            Console.WriteLine("2. Puma");
            Console.WriteLine("3. Adidas");

        }
        public Sportswear Answer1()
        {
            int index = int.Parse(Console.ReadLine()) - 1;
            return (Sportswear)index;
        }

        internal Seller Seller
        {
            get => default;
            set
            {
            }
        }
    }

    class Consult : Employer
    {
        Sportswear _ordername;

        #region Methods
        public void TakeOrder(Sportswear clothesname)
        {
            string clotname = "";
            switch (clothesname)
            {
                case Sportswear.Nike:
                    clotname = "suit Nike";
                    break;
                case Sportswear.Puma:
                    clotname = "suit Puma";
                    break;
                case Sportswear.Adidas:
                    clotname = "suit Adidas";
                    break;
            }
            Console.WriteLine($"Consultant {Name} has received your order, which is a {clotname} suit");
            _ordername = clothesname;
        }



        public Clothes Choose()
        {
            Console.WriteLine($"{Name} begins the search");
            switch (_ordername)
            {
                case Sportswear.Nike:
                    Console.WriteLine($"The employee went to the warehouse to find your product, a sports suit Nike");
                    return LetsWork(_ordername);

                case Sportswear.Puma:
                    Console.WriteLine($"The employee went to the warehouse to find your product, a sports suit Puma");
                    return LetsWork(_ordername);

                case Sportswear.Adidas:
                    Console.WriteLine($"The employee went to the warehouse to find your product, a sports suit Adidas");
                    return LetsWork(_ordername);
                default:
                    return null;
            }
        }
        #endregion
    }
    public abstract class Clothes 
    {
        #region Properties
        public int Price { get; set; }
        public int Years { get; set; }

        internal Program Program
        {
            get => default;
            set
            {
            }
        }

        public Clothes()
        { }
        #endregion
    }
    class Nike : Clothes 
    {
        #region Construct
        public Nike()
        {
            Price = 2500;
            Years = 2023;
        }
        #endregion

        public Sportswear Sportswear
        {
            get => default;
            set
            {
            }
        }
    }
    class Puma : Clothes
    {
        #region Construct
        public Puma()
        {
            Price = 2300;
            Years = 2022;
        }
        #endregion

        public Sportswear Sportswear
        {
            get => default;
            set
            {
            }
        }
    }
    class Adidas : Clothes 
    {
        #region Construct
        public Adidas()

        {
            Price = 2200;
            Years = 2022;
        }
        #endregion

        public Sportswear Sportswear
        {
            get => default;
            set
            {
            }
        }
    }
    public class Package : SportsWearPackage// Фасад реалізований у пакувальнику та запакованому спортивному одягу
    {
        public string Color { get; set; }

        public Package(string color)
        {
            Color = color;
        }
        internal Customer Customer
        {
            get => default;
            set
            {
            }
        }

    }

    public class SportsWearPackage // Фасад той що був згаданий вище
    {
        public Clothes ct { get; set; }
        public Package pkg { get; set; }
        public SportsWearPackage() { }

        public SportsWearPackage MakePackage(Clothes ct, Package pkg)
        {
            this.ct = ct;
            this.pkg = pkg;

            return this;
        }
    }
}