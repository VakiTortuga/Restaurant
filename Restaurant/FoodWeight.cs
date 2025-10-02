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

        public short Weight { get => weight; }

        // конструктор с значением веса
        public FoodWeight(short weight) => this.weight = weight;

        // получение строкового представления веса в предопределенном формате
        public override string ToString()
        {
            if (this.weight <= 0) return "вес не указан";
            return $"{weight} грамм";
        }
    }
}
