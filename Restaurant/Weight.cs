using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    // реализовать проверку корректности значений
    // и насколько логично иметь конструктор без параметров
    internal struct Weight
    {
        private readonly short weight; // вес в граммах

        // конструктор с значением веса
        public Weight(short weight) => this.weight = weight;

        // получение строкового представления веса в предопределенном формате
        public override string ToString()
        {
            if (this.weight <= 0) return "вес не указан";
            return $"{weight} грамм";
        }
    }
}
