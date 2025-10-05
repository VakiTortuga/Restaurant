using Restaurant.InterfaceClasses;
using Restaurant.UIClasses;
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
        public byte Slices { get => slices; }

        // значения по умолчанию
        private const string EMPTY_NAME = "Блюдо";
        private const int MIN_AMOUNT_OF_SLICES = 1;
        private const int MAX_AMOUNT_OF_SLICES = 16;

        protected Food(string? name) : this(name, FoodWeight.MIN_WEIGHT) { }

        // конструктор
        protected Food(string? name, short weight)
        {
            if (name != null) this.name = name;
            else this.name = EMPTY_NAME;
            
            if (name == "") this.name = EMPTY_NAME;

            this.weight = new FoodWeight(weight);
            slices = MIN_AMOUNT_OF_SLICES;

            foodIsBaked = false;
            foodIsCut = false;
            foodIsEaten = false;
        }

        public static Food operator --(Food food)
        {
            short newWeight = (short)(food.weight.Weight - 50);
            if (newWeight < FoodWeight.MIN_WEIGHT) newWeight = FoodWeight.MIN_WEIGHT; // Минимальный вес

            return new Food(food.name, newWeight);
        }

        public static Food operator ++(Food food)
        {
            short newWeight = (short)(food.weight.Weight + 50);
            if (newWeight > FoodWeight.MAX_WEIGHT) newWeight = FoodWeight.MAX_WEIGHT; // Максимальный вес

            return new Food(food.name, newWeight);
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
                Console.WriteLine($"\"{name}\" печется.");
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(150);
                    Console.Write('.');
                }
                
                foodIsBaked = true;
                Console.WriteLine();
                Console.WriteLine($"\"{name}\" запечено! ");
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
                Console.WriteLine($"\"{name}\" нарезано на {this.slices} кусочка(ов)!");
            }
            else
                Console.WriteLine($"\"{name}\" можно нарезать на {this.slices + 1}-{MAX_AMOUNT_OF_SLICES} кусочков.");
        }

        // съесть блюдо
        public virtual void Eat(byte pieces = 1)
        {
            if (foodIsBaked != true) Console.WriteLine($"\"{name}\" еще не испечено.");
            else if (foodIsEaten == true) Console.WriteLine($"\"{name}\" уже съедено.");
            else
            {
                if (pieces < 0)
                {
                    Console.WriteLine("Можно съесть только положительное количество кусочков!");

                }
                else if (pieces > this.slices)
                {
                    Console.WriteLine($"От \"{name}\" осталось только {this.slices} кусочка(ов)!");
                }
                else if (pieces == this.slices)
                {
                    this.slices = 0;
                    foodIsEaten = true;
                    Console.WriteLine($"\"{name}\" съедено! ");
                }
                else
                {
                    this.slices -= pieces;
                    Console.WriteLine($"Вы съели {pieces} кусочка(ов), осталось {this.slices}.");
                }
            }
        }

        public virtual void PrintFoodShort()
        {
            Console.WriteLine($"\"{name}\". Вес - {weight.ToString()}.");
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
