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
        protected Food(string name, short weight)
        {
            this.name = name;
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
        protected void Bake()
        {
            this.foodIsBaked = true;
            Console.Write($"Блюдо \"{name}\" запечено! ");
        }

        // нарезать блюдо
        protected void Cut()
        {
            this.foodIsCut = true;
            Console.Write($"Блюдо \"{name}\" нарезано! ");
        }

        // съесть блюдо
        protected void Eat()
        {
            this.foodIsEaten = true;
            Console.Write($"Блюдо \"{name}\" съедено! ");
        }

        //public virtual void PrintFood()
        //{

        //}
    }
}
