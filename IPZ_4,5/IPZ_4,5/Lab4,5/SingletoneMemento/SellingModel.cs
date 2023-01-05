using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class SellingModel // Модель продажу
    {
        public string Name { get; set; }


        public SellingModel(string name)
        {
            Name = name;
        }
        internal SellingsList SellingsList
        {
            get => default;
            set
            {
            }
        }

    }
}
