using Restaurant.InterfaceClasses;
using Restaurant.UIClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.FoodClasses
{
    /// <summary>
    /// Класс блюда.
    /// </summary>
    /// <remarks>Для использования в меню или списке заказов</remarks>
    internal class FoodItem
    {
        // Поля

        /// <summary>
        /// Название блюда.
        /// </summary>
        /// <remarks>У объекта значение ненулевое, строка не пустая.</remarks>
        protected string name;

        /// <summary>
        /// Вес блюда.
        /// </summary>
        protected FoodWeight weight;

        /// <summary>
        /// Кол-во кусочков.
        /// </summary>
        protected byte amountOfSlices;

        /// <summary>
        /// Флаг нарезанности блюда.
        /// </summary>
        protected bool isCut;

        /// <summary>
        /// Флаг готовности блюда.
        /// </summary>
        protected bool isBaked;

        /// <summary>
        /// Флаг съеденности блюда
        /// </summary>
        protected bool isEaten;


        // Константы
        
        /// <summary>
        /// Название по умолчанию.
        /// </summary>
        private const string DEFAULT_NAME = "Блюдо";

        /// <summary>
        /// Минимальное кол-во кусочков
        /// </summary>
        public const int MIN_AMOUNT_OF_SLICES = 1;

        /// <summary>
        /// Максимальное кол-во кусочков
        /// </summary>
        public const int MAX_AMOUNT_OF_SLICES = 16;


        // Свойства

        public bool IsCut { get => isCut; }
        public bool IsBaked { get => isBaked; }
        public bool IsEaten { get => isEaten; }
        public byte AmountOfSlices { get => amountOfSlices; }


        // Конструкторы

        /// <summary>
        /// Конструктор блюда без параметра веса.
        /// </summary>
        /// <param name="name"></param>
        /// <remarks>Устанавливает минимальное значение веса.</remarks>
        protected FoodItem(string? name) : this(name, FoodWeight.MIN_WEIGHT) { }

        /// <summary>
        /// Конструктор блюда.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weight"></param>
        /// <remarks>При выходе имя и вес объекта всегда будут корректными.</remarks>
        protected FoodItem(string? name, short weight)
        {
            if (name == null) this.name = DEFAULT_NAME;
            else this.name = name;
            
            if (name == string.Empty) this.name = DEFAULT_NAME;

            this.weight = new FoodWeight(weight);
            amountOfSlices = MIN_AMOUNT_OF_SLICES;

            isBaked = false;
            isCut = false;
            isEaten = false;
        }

        // Арифметические операции

        /// <summary>
        /// Уменьшение веса блюда на 50 грамм
        /// </summary>
        /// <param name="food">Блюдо, вес которого уменьшаем.</param>
        /// <returns>Блюдо с уменьшенным весом.</returns>
        public static FoodItem operator --(FoodItem food)
        {
            short newWeight = (short)(food.weight.WeightInGramms - 50);
            if (newWeight < FoodWeight.MIN_WEIGHT) newWeight = FoodWeight.MIN_WEIGHT;

            return new FoodItem(food.name, newWeight);
        }

        /// <summary>
        /// Увеличение веса блюда на 50 грамм
        /// </summary>
        /// <param name="food">Блюдо, вес которого увеличиваем.</param>
        /// <returns>Блюдо с увеличенным весом.</returns>
        public static FoodItem operator ++(FoodItem food)
        {
            short newWeight = (short)(food.weight.WeightInGramms + 50);
            if (newWeight > FoodWeight.MAX_WEIGHT) newWeight = FoodWeight.MAX_WEIGHT;

            return new FoodItem(food.name, newWeight);
        }

        // Методы

        /// <summary>
        /// Деконструктор блюда.
        /// </summary>
        /// <param name="name">Название блюда.</param>
        /// <param name="weight">Вес блюда в граммах.</param>
        public virtual void Deconstruct(out string name, out short weight)
        {
            name = this.name;
            weight = this.weight.WeightInGramms;
        }

        /// <summary>
        /// Запечь блюдо.
        /// </summary>
        public virtual void Bake()
        {
            byte TICKS_BAKE = 10;
            byte TICK_MILLISECS = 200;

            if (isBaked == true)
            {
                Console.WriteLine($"\"{name}\" уже готово.");
                return;
            }

            Console.WriteLine($"\"{name}\" печется.");
            UserInteractions.ProgressBar(TICKS_BAKE, TICK_MILLISECS);
            Console.WriteLine($"\"{name}\" запечено! ");

            isBaked = true;
        }

        // нарезать блюдо
        public virtual void Cut(byte slices = 2)
        {
            if (isBaked != true)
                Console.WriteLine($"\"{name}\" еще не испечено.");
            else if (isCut == true)
                Console.WriteLine($"\"{name}\" уже нарезано.");
            else if (slices > this.amountOfSlices && slices <= MAX_AMOUNT_OF_SLICES)
            {
                isCut = true;
                this.amountOfSlices = slices;
                Console.WriteLine($"\"{name}\" нарезано на {this.amountOfSlices} кусочка(ов)!");
            }
            else
                Console.WriteLine($"\"{name}\" можно нарезать на {this.amountOfSlices + 1}-{MAX_AMOUNT_OF_SLICES} кусочков.");
        }

        // съесть блюдо
        public virtual void Eat(byte pieces = 1)
        {
            if (isBaked != true) Console.WriteLine($"\"{name}\" еще не испечено.");
            else if (isEaten == true) Console.WriteLine($"\"{name}\" уже съедено.");
            else
            {
                if (pieces < 0)
                {
                    Console.WriteLine("Можно съесть только положительное количество кусочков!");

                }
                else if (pieces > this.amountOfSlices)
                {
                    Console.WriteLine($"От \"{name}\" осталось только {this.amountOfSlices} кусочка(ов)!");
                }
                else if (pieces == this.amountOfSlices)
                {
                    this.amountOfSlices = 0;
                    isEaten = true;
                    Console.WriteLine($"\"{name}\" съедено! ");
                }
                else
                {
                    this.amountOfSlices -= pieces;
                    Console.WriteLine($"Вы съели {pieces} кусочка(ов), осталось {this.amountOfSlices}.");
                }
            }
        }

        public virtual void PrintFoodShort()
        {
            Console.WriteLine($"\"{name}\". Вес - {weight.ToString()}.");
        }

        public virtual void PrintFoodStatus()
        {
            if (isCut) Console.WriteLine($"Кол-во кусочков - {amountOfSlices}.");
            Console.WriteLine("Блюдо {0}запечено.", isBaked ? "" : "не ");
            Console.WriteLine("Блюдо {0}нарезано.", isCut ? "" : "не ");
            Console.WriteLine("Блюдо {0}съедено.", isEaten ? "" : "не ");
        }

        public virtual void PrintFoodFull()
        {
            PrintFoodShort();
            PrintFoodStatus();
        }
    }
}
