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
        Small,
        Medium,
        Large,
        ExtraLarge
    }

    

    internal class Pizza : FoodItem
    {
        private PizzaSize size; // размер пиццы (предопределяет ее вес)

        // делегат, который будут слушать все пиццы
        public static event Action? OnPineappleReminder;

        // конструктор
        public Pizza(string? name, PizzaSize size) : base(name, PizzaSizeToWeight(size))
        {
            this.size = size;
        }

        public Pizza(string? name, short weight) : base(name, weight)
        {
            this.size = WeightToPizzaSize(base.weight.WeightInGramms);
        }

        public static short PizzaSizeToWeight(PizzaSize size)
        {
            switch (size)
            {
                case PizzaSize.Small: return 300;
                case PizzaSize.Medium: return 500;
                case PizzaSize.Large: return 800;
                case PizzaSize.ExtraLarge: return 1000;
                default: return 0;
            }
        }

        public static PizzaSize WeightToPizzaSize(short weight)
        {
            if (weight < 400) return PizzaSize.Small;
            if (weight < 600) return PizzaSize.Medium;
            if (weight < 900) return PizzaSize.Large;
            return PizzaSize.ExtraLarge;
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
