using Restaurant.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.FoodClasses
{
    internal class Food
    {
        // поля класса Food
        protected string name; // название блюда
        protected FoodWeight weight; // вес блюда
        protected byte slices; // количество кусков
        protected bool foodIsCut; // флаг нарезанности блюда
        protected bool foodIsBaked; // флаг готовности блюда
        protected bool foodIsEaten; // флаг съеденности блюда

        public bool FoodIsCut { get => foodIsCut; }
        public bool FoodIsBaked { get => foodIsBaked; }
        public bool FoodIsEaten { get => foodIsEaten; }

        // значения по умолчанию
        private const string EMPTY_NAME = "Блюдо";
        private const int MIN_AMOUNT_OF_SLICES = 1;
        private const int MAX_AMOUNT_OF_SLICES = 16;

        // конструктор
        protected Food(string? name, short weight)
        {
            if (name != null) this.name = name;
            else this.name = EMPTY_NAME;

            this.weight = new FoodWeight(weight);
            slices = MIN_AMOUNT_OF_SLICES;

            foodIsBaked = false;
            foodIsCut = false;
            foodIsEaten = false;
        }

        // деконструктор для свойств
        public virtual void Deconstruct(out string name, out short weight)
        {
            name = this.name;
            weight = this.weight.Weight;
        }

        // запечь блюдо
        public virtual void Bake()
        {
            if (foodIsBaked == true)
                Console.WriteLine($"\"{name}\" уже готово.");
            else
            {
                foodIsBaked = true;
                Console.Write($"\"{name}\" запечено! ");
            }
        }

        // нарезать блюдо
        public virtual void Cut(byte slices = 2)
        {
            if (foodIsBaked != true)
                Console.WriteLine($"\"{name}\" еще не испечено.");
            else if (foodIsCut == true)
                Console.WriteLine($"\"{name}\" уже нарезано.");
            else if (slices > this.slices && slices <= MAX_AMOUNT_OF_SLICES)
            {
                foodIsCut = true;
                this.slices = slices;
                Console.Write($"\"{name}\" нарезано на {this.slices} кусочка(ов)!");
            }
            else
                Console.WriteLine($"\"{name}\" можно нарезать на {this.slices + 1}-{MAX_AMOUNT_OF_SLICES} кусочков.");
        }

        // съесть блюдо
        public virtual void Eat()
        {
            if (foodIsBaked != true) Console.WriteLine($"\"{name}\" еще не испечено.");
            else if (foodIsEaten == true) Console.WriteLine($"\"{name}\" уже съедено.");
            else
            {
                foodIsEaten = true;
                Console.Write($"\"{name}\" съедено! ");
            }
        }

        public virtual void PrintFoodShort()
        {
            Console.WriteLine($"\"{name}\". Вес - {weight}.");
        }

        public virtual void PrintFoodStatus()
        {
            if (foodIsCut) Console.WriteLine($"Кол-во кусочков - {slices}.");
            Console.WriteLine("Блюдо {0}запечено.", foodIsBaked ? "" : "не ");
            Console.WriteLine("Блюдо {0}нарезано.", foodIsCut ? "" : "не ");
            Console.WriteLine("Блюдо {0}съедено.", foodIsEaten ? "" : "не ");
        }

        public virtual void PrintFoodFull()
        {
            PrintFoodShort();
            PrintFoodStatus();
        }
    }
}
