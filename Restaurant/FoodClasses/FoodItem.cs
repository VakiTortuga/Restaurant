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
        /// Название блюда по умолчанию.
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

        /// <summary>
        /// Время одного тика готовки в миллисекундах.
        /// </summary>
        const byte BAKE_TICK_MILLISECS = 220;

        /// <summary>
        /// Время одного тика нарезки в миллисекундах.
        /// </summary>
        const byte CUT_TICK_MILLISECS = 150;

        /// <summary>
        /// Время одного тика поедания в миллисекундах.
        /// </summary>
        const byte EAT_TICK_MILLISECS = 180;


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
            if (name == null || name == string.Empty) this.name = DEFAULT_NAME;
            else this.name = name;

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

            FoodItem newFood;
            if (food is Pizza) newFood = new Pizza(food.name, newWeight);
            else if (food is Calzone) newFood = new Calzone(food.name, newWeight);
            else newFood = new FoodItem(food.name, newWeight);
            return newFood;
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

            FoodItem newFood;
            if (food is Pizza) newFood = new Pizza(food.name, newWeight);
            else if (food is Calzone) newFood = new Calzone(food.name, newWeight);
            else newFood = new FoodItem(food.name, newWeight);
            return newFood;
        }

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
            // расчет времени готовки от параметров пиццы
            byte ticksBake = (byte)(5 + this.weight.WeightInGramms / 100); 
            
            if (isBaked == true)
            {
                Console.WriteLine($"\"{name}\" уже готово.");
                return;
            }

            Console.WriteLine($"\"{name}\" печется.");
            UserInteractions.ProgressBar(ticksBake, BAKE_TICK_MILLISECS);
            isBaked = true;

            Console.WriteLine($"\"{name}\" запечено! ");
        }

        /// <summary>
        /// Нарезать блюдо на кусочки.
        /// </summary>
        /// <param name="slicesToCut">Запрашиваемое кол-во кусочковю</param>
        public virtual void Cut(byte slicesToCut = 2)
        {
            byte ticksCut = slicesToCut; // время нарезки пропорционально кол-ву кусочков

            if (isBaked == false)
            {
                Console.WriteLine($"\"{name}\" еще не испечено.");
                return;
            }
                
            if (isCut == true)
            {
                Console.WriteLine($"\"{name}\" уже нарезано.");
                return;
            }
            
            // проверяем запрашиваемое кол-во кусочков на соответствие интервалу корректных значений
            if (slicesToCut > amountOfSlices && slicesToCut <= MAX_AMOUNT_OF_SLICES)
            {
                Console.WriteLine($"Нарезаем \"{name}\".");
                UserInteractions.ProgressBar(ticksCut, CUT_TICK_MILLISECS);
                
                amountOfSlices = slicesToCut;
                isCut = true;

                Console.WriteLine($"\"{name}\" нарезано на {amountOfSlices} кусочка(ов)!");
            }
            else Console.WriteLine($"\"{name}\" можно нарезать на {amountOfSlices + 1}-{MAX_AMOUNT_OF_SLICES} кусочков.");
        }

        /// <summary>
        /// Съесть некоторое кол-во кусочков блюда.
        /// </summary>
        /// <param name="piecesToEat"></param>
        public virtual void Eat(byte piecesToEat = 1)
        {
            byte ticksEat = piecesToEat; // время поедания пропорционально кол-ву кусочков

            if (isBaked == false)
            {
                Console.WriteLine($"\"{name}\" еще не испечено.");
                return;
            }
                
            if (isEaten == true)
            {
                Console.WriteLine($"\"{name}\" уже съедено.");
                return;
            }
                
            if (piecesToEat < 0)
            {
                Console.WriteLine("Можно съесть только положительное количество кусочков!");
                return;
            }

            if (piecesToEat > this.amountOfSlices)
            {
                Console.WriteLine($"От \"{name}\" осталось только {this.amountOfSlices} кусочка(ов)!");
                return;
            }
            
            // обрабатываем поедание кусочков
            if (piecesToEat == 0) Console.WriteLine($"Вы передумали есть \"{name}\" :(");
            else
            {
                Console.WriteLine($"Часть \"{name}\" таинственно исчезает...");
                UserInteractions.ProgressBar(ticksEat, EAT_TICK_MILLISECS);

                this.amountOfSlices -= piecesToEat;
            }

            // информируем о результате
            Console.WriteLine($"Вы съели {piecesToEat} кусочка(ов), осталось {this.amountOfSlices}.");

            // проверяем съеденность пиццы
            if (this.amountOfSlices == 0)
            {
                isEaten = true;
                Console.WriteLine($"\"{name}\" съедено! ");
            }
        }

        /// <summary>
        /// Выводит краткое описание блюда.
        /// </summary>
        public virtual void PrintFoodShort()
        {
            Console.WriteLine($"\"{name}\". Вес - " + weight.ToString());
        }

        /// <summary>
        /// Выводит дополнительную информацию к краткому описанию блюда.
        /// </summary>
        public virtual void PrintFoodStatus()
        {
            if (isCut) Console.WriteLine($"Кол-во кусочков - {amountOfSlices}.");
            Console.WriteLine("Блюдо {0}запечено.", isBaked ? "" : "не ");
            Console.WriteLine("Блюдо {0}нарезано.", isCut ? "" : "не ");
            Console.WriteLine("Блюдо {0}съедено.", isEaten ? "" : "не ");
        }

        /// <summary>
        /// Выводит подробное описание блюда.
        /// </summary>
        public virtual void PrintFoodFull()
        {
            PrintFoodShort();
            PrintFoodStatus();
        }
    }
}
