using Restaurant.InterfaceClasses;
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

        private const short EMPTY_WEIGHT = 100; // дефолтное значение веса

        public short Weight // геттер простой, сеттер с проверкой и установкой дефолта при некорректном вводе
        {
            get => weight;
            set
            {
                if (value <= 50 || value >= 3000)
                {
                    weight = EMPTY_WEIGHT;
                    Console.WriteLine($"Введено некорректное значение веса - {value}.\nЗначение установлено на {weight}.");
                    Kitchen.WaitForUser();
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
