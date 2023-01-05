using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal sealed class SellingsList// Список продажів
: Program
    {
        private List<SellingModel> Sellings = new List<SellingModel>();
        private SellingHistory sellingHistory = new SellingHistory();
        private static SellingsList _instance = null;

        private SellingsList()
        { }

        public static SellingsList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SellingsList();
            }
            return _instance;
        }

        public void AddSelling(string name)
        {
            SellingModel cl = new SellingModel(name);
            Sellings.Add(cl);
            sellingHistory.History.Push(cl);
            Console.ReadLine();
            Console.WriteLine($"Your list of expenses has been replenished by UAH {cl.Name}.");
            Console.ReadLine();
        }

        public void AddLastSelling()
        {
            SellingModel cl = sellingHistory.History.Pop();
            if (!Sellings.Contains(cl))
            {
                AddSelling(cl.Name);
            }
        }

        public void RemoveLastSelling()
        {
            SellingModel cl = sellingHistory.History.Pop();
            Sellings.Remove(cl);
            sellingHistory.History.Push(cl);
        }

        public string GetSellings()
        {
            string result = "List of sales";
            if (Sellings.Count == 0) { result += " empty."; return result; }
            for (int i = 0; i < Sellings.Count; i++)
            {
                result += Environment.NewLine;
                result += Sellings.ElementAt(i).Name;
            }
            return result;
        }
    }
}
