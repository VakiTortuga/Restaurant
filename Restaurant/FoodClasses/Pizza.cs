using Restaurant.UIClasses;
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

        // делегат, который будут слушать все пиццы
        public static event Action? OnPineappleReminder;

        // конструктор
        public Pizza(string? name, PizzaSize size) : base(name, (short)size)
        {
            this.size = size;
        }

        public void Subscribe()
        {
            OnPineappleReminder -= AddPineapplesToName;
            OnPineappleReminder += AddPineapplesToName;
        }

        public void Unsubscribe()
        {
            OnPineappleReminder -= AddPineapplesToName;
        }

        // Метод, который будет вызываться при событии
        private void AddPineapplesToName()
        {
            // Добавляем ананасы к названию, если их еще нет
            if (!name.Contains("ананас", StringComparison.OrdinalIgnoreCase))
            {
                name += " с ананасами";
            }
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
        public static void RemindPineapples()
        {
            Console.WriteLine("Не забудь добавить ананасы!");

            Console.WriteLine("Добавляем ананасы.");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(150);
                Console.Write('.');
            }
            OnPineappleReminder?.Invoke();
        }

        // выпекать пиццу
        public override void Bake() => base.Bake();

        // порезать пиццу на кусочки
        public override void Cut(byte slices) => base.Cut(slices);
        
        // сесть пиццу
        public override void Eat(byte pieces) => base.Eat(pieces);

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
