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
        Adidas,
    }

    class Program
    {
        static void Main(string[] args)
        {
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
            customer.Pay(clothes);
        }
    }

    abstract class Person
    {
        #region Properties
        public Random _rnd = new Random();
        #endregion
    }

    class Customer : Person
    {
        public string name = "Покупець";
        public int Money = 3000;
        #region Methods 
        public void Pay()
        {
            Console.WriteLine($"У {name} в гаманцi {Money} гривень");
        }
        public void Pay(Clothes clothes)
        {
            this.Money = Money - clothes.Price;
            if (Money >= 0)
            {
            Console.WriteLine($"{name} заплатив {clothes.Price} гривень");
            Console.WriteLine($"У {name} лишилося {Money} гривень");
            Console.ReadLine();
            }
            else
            {
                this.Money = clothes.Price / 10;
                Console.WriteLine($"{name} взяв розтрочку на товар та буде винен {Money} гривень протягом 10 мiсяцiв");
                Console.ReadLine();
            }
        }
        #endregion
    }
    abstract class Employer : Person
    {
        public enum Names
        {
            Кирило,
            Iлля,
            Данило,
            Софiя,
            Дiана,
            Богданна
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
                    Console.WriteLine($"Пошук...");
                    return nike;
                case Sportswear.Puma:
                    Puma puma = new Puma();
                    Console.WriteLine($"Пошук...");
                    return puma;
                case Sportswear.Adidas:
                    Adidas adidas = new Adidas();
                    Console.WriteLine($"Пошук...");
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
                    Console.WriteLine($"Ваше замовлення спортивний костьюм Nike {_clothes.Years} року, знаходиться на склад,   цiна:{_clothes.Price} грн");
                    break;
                case Sportswear.Puma:
                    _clothes = new Puma();
                    Console.WriteLine($"Ваше замовлення спортивний костьюм Puma {_clothes.Years} року, знаходиться на склад,   цiна:{_clothes.Price} грн");
                    break;
                case Sportswear.Adidas:
                    _clothes = new Adidas();
                    Console.WriteLine($"Ваше замовлення спортивний костьюм Adidas {_clothes.Years} року, знаходиться на склад, цiна:{_clothes.Price} грн");
                    break;
            }
            Console.WriteLine($"продавець викликає консультанта та передає замовлення");
        }
        public Clothes Return(Clothes clothes)
        {
            Console.WriteLine($"{Name} приносить ваше замовлення");
            return clothes;
        }
        public void Check(Clothes clothes)
        {
            Console.WriteLine($"З вас до сплати {clothes.Price} гривень");
        }
        #endregion

    }
    class Dialogues
    {
        Seller _seller;

        public void Begin()
        {
            Customer customer = new Customer();
            Console.WriteLine($"{customer.name} заходить в магазин одягу, у нього в гаманцi {customer.Money} гривень");
        }

        public Dialogues(Seller seller)
        {
            _seller = seller;
        }
        public void Welcome()
        {
            Console.WriteLine($"Добрий день, мене звати {_seller.Name}, який бренд ви полюбляєте?");
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
                    clotname = "костьюм Nike";
                    break;
                case Sportswear.Puma:
                    clotname = "костьюм Puma";
                    break;
                case Sportswear.Adidas:
                    clotname = "костьюм Adidas";
                    break;
            }
            Console.WriteLine($"Консультант {Name} отримав ваше замовлення, а саме {clotname}");
            _ordername = clothesname;
        }

        public Clothes Choose()
        {
            Console.WriteLine($"{Name} приступає до пошуку");
            switch (_ordername)
            {
                case Sportswear.Nike:
                    Console.WriteLine($"Працiвник вiдправився на склад щоб знайти ваш товар, спортивний костьюм Nike");
                    return LetsWork(_ordername);

                case Sportswear.Puma:
                    Console.WriteLine($"Працiвник вiдправився на склад щоб знайти ваш товар, спортивний костьюм Puma");
                    return LetsWork(_ordername);

                case Sportswear.Adidas:
                    Console.WriteLine($"Працiвник вiдправився на склад щоб знайти ваш товар, спортивний костьюм Adidas"); 
                    return LetsWork(_ordername);
                default:
                    return null;
            }
        }
        #endregion
    }
    abstract class Clothes 
    {
        #region Properties
        public int Price { get; set; }
        public int Years { get; set; }
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
    }
}