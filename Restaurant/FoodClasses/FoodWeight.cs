using Restaurant.InterfaceClasses;
using Restaurant.UIClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.FoodClasses
{
    internal struct FoodWeight
    {
        private short weight; // вес в граммах

        public const short MIN_WEIGHT = 100; // минимальное значение веса
        public const short MAX_WEIGHT = 2000; // максимальное значение веса

        public short Weight // геттер простой, сеттер с проверкой и установкой дефолта при некорректном вводе
        {
            get => weight;
            set
            {
                if (value <= MIN_WEIGHT || value >= MAX_WEIGHT)
                {
                    weight = MIN_WEIGHT;
                    Console.WriteLine($"Введено некорректное значение веса - {value}.\nЗначение установлено на {weight}.");
                }
                else weight = value;
            }
        }

        // конструктор с значением веса
        public FoodWeight(short weight) => Weight = weight;

        // получение строкового представления веса в предопределенном формате
        public override string ToString() => $"{Weight} грамм";
    }
}
