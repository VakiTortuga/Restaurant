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
        public Pizza(string name, PizzaSize size) : base(name, (short)size)
        {
            this.slices = 1;
            this.size = size;
        }

        // напомнить про ананасы
        public static void RemindPineapples() => Console.WriteLine("Не забудь добавить ананасы!");

        // выпекать пиццу
        new public void Bake()
        {
            if (base.foodIsBaked == true)
            {
                Console.WriteLine($"Пицца уже готова.");
                return;
            }
            base.Bake();
        }

        // порезать пиццу на кусочки
        public void Cut(byte slices = 1)
        {
            if (base.foodIsCut == true)
            {
                Console.WriteLine($"Пицца уже нарезана на {slices} части(ей).");
                return;
            }
            this.slices = slices;
            base.Cut();
            Console.WriteLine($"Кол-во кусочков - {slices}.");
        }
        
        // сесть пиццу
        new public void Eat()
        {
            if (base.foodIsEaten == true)
            {
                Console.WriteLine($"Пицца уже съедена.");
                return;
            }
            base.Eat();
        }
    }
}
