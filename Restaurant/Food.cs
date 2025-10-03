using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Food
    {
        // поля класса Food
        protected string name; // название блюда
        protected FoodWeight weight; // вес блюда
        protected bool foodIsCut; // флаг нарезанности блюда
        protected bool foodIsBaked; // флаг готовности блюда
        protected bool foodIsEaten; // флаг съеденности блюда

        // значения по умолчанию
        private const string EMPTY_NAME = "без названия";
        private const short EMPTY_WEIGHT = 0;

        // конструкторы

        //protected Food() : this(EMPTY_NAME, EMPTY_WEIGHT)
        //{ }
        //protected Food(string name) : this(name, EMPTY_WEIGHT)
        //{ }
        //protected Food(short weight) : this(EMPTY_NAME, weight)
        //{ }
        protected Food(string? name, short weight)
        {
            if (name != null) this.name = name;
            else this.name = EMPTY_NAME;

            this.weight = new FoodWeight(weight);
            this.foodIsCut = false;
            this.foodIsBaked = false;
            this.foodIsEaten = false;
        }

        // деконструктор для свойств
        public virtual void Deconstruct(out string name, out FoodWeight weight)
        {
            name = this.name;
            weight = this.weight;
        }

        // запечь блюдо
        public virtual void Bake()
        {
            this.foodIsBaked = true;
            Console.Write($"Блюдо \"{name}\" запечено! ");
        }

        // нарезать блюдо
        public virtual void Cut(byte slices = 2)
        {
            this.foodIsCut = true;
            Console.Write($"Блюдо \"{name}\" нарезано! ");
        }

        // съесть блюдо
        public virtual void Eat()
        {
            this.foodIsEaten = true;
            Console.Write($"Блюдо \"{name}\" съедено! ");
        }

        public virtual void PrintFoodShort()
        {
            Console.WriteLine($"{this.name}. Вес: {this.weight}.");
        }

        public virtual void PrintFoodStatus()
        {
            Console.WriteLine("Блюдо {0}запечено.", (this.foodIsBaked ? "" : "не "));
            Console.WriteLine("Блюдо {0}нарезано.", (this.foodIsCut ? "" : "не "));
            Console.WriteLine("Блюдо {0}съедено.", (this.foodIsEaten ? "" : "не "));
        }

        public virtual void PrintFoodFull()
        {
            this.PrintFoodShort();
            this.PrintFoodStatus();
        }
    }
}
