using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal struct FoodWeight
    {
        private short weight; // вес в граммах

        public short Weight
        {
            get => weight;
            set
            {
                if (weight <= 0 || weight >= 3000) weight = 0;
                else weight = value;
            }
        }

        // конструктор с значением веса
        public FoodWeight(short weight) => this.weight = weight;

        // получение строкового представления веса в предопределенном формате
        public override string ToString()
        {
            if (this.weight <= 0) return "не указан";
            return $"{weight} грамм";
        }
    }
}
