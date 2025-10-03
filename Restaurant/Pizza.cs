using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
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
        // поля и свойства
        private byte slices; // количество кусочков пиццы
        private PizzaSize size; // размер пиццы (предопределяет ее вес)

        // конструктор
        public Pizza(string? name, PizzaSize size) : base(name, (short)size)
        {
            this.slices = 1;
            this.size = size;
        }

        public override void Deconstruct(out string name, out FoodWeight weight)
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
        public override void Bake()
        {
            if (base.foodIsBaked == true)
            {
                Console.WriteLine($"Пицца уже готова.");
                return;
            }
            base.Bake();
        }

        // порезать пиццу на кусочки
        public override void Cut(byte slices)
        {
            if (base.foodIsCut == true)
            {
                Console.WriteLine($"Пицца уже нарезана на {slices} части(ей).");
                return;
            }
            this.slices = slices;
            base.Cut(slices);
            Console.WriteLine($"Кол-во кусочков - {slices}.");
        }
        
        // сесть пиццу
        public override void Eat()
        {
            if (base.foodIsEaten == true)
            {
                Console.WriteLine($"Пицца уже съедена.");
                return;
            }
            base.Eat();
        }

        public override void PrintFoodShort()
        {
            base.PrintFoodShort();
        }

        public override void PrintFoodStatus()
        {
            Console.WriteLine("Пицца.");
            base.PrintFoodStatus();
            Console.WriteLine($"Размер - {this.size}.");
            Console.WriteLine($"Кол - во кусочков - {this.slices}.");
        }

        public override void PrintFoodFull()
        {
            this.PrintFoodShort();
            this.PrintFoodStatus();
        }
    }
}
