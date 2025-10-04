using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.FoodClasses
{
    enum PizzaSize
    {
        Small = 300,
        Medium = 500,
        Large = 800,
        ExtraLarge = 1000
    }

    internal class Pizza : Food
    {
        private PizzaSize size; // размер пиццы (предопределяет ее вес)

        // конструктор
        public Pizza(string? name, PizzaSize size) : base(name, (short)size)
        {
            this.size = size;
        }

        public override void Deconstruct(out string name, out short weight)
        {
            base.Deconstruct(out name, out weight);
        }

        public void Deconstruct(out string name, out PizzaSize size)
        {
            base.Deconstruct(out name, out _);
            size = this.size;
        }

        // напомнить про ананасы
        public static void RemindPineapples() => Console.WriteLine("Не забудь добавить ананасы!");

        // выпекать пиццу
        public override void Bake() => base.Bake();

        // порезать пиццу на кусочки
        public override void Cut(byte slices) => base.Cut(slices);
        
        // сесть пиццу
        public override void Eat() => base.Eat();

        public override void PrintFoodShort() => base.PrintFoodShort();

        public override void PrintFoodStatus()
        {
            Console.WriteLine("Тип блюда - пицца.");
            Console.WriteLine($"Размер - {size}.");
            base.PrintFoodStatus();
        }

        public override void PrintFoodFull()
        {
            PrintFoodShort();
            PrintFoodStatus();
        }
    }
}
