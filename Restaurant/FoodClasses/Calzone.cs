using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.FoodClasses
{
    /// <summary>
    /// Класс кальцоне
    /// </summary>
    internal class Calzone : FoodItem
    {
        // Константы

        /// <summary>
        /// Название кальцоне по умолчанию.
        /// </summary>
        private const string DEFAULT_CALZONE_NAME = "Кальцоне";


        // Конструкторы

        /// <summary>
        /// Создает кальцоне с указанным весом
        /// </summary>
        /// <param name="name">Название кальцоне</param>
        /// <param name="weight">Вес кальцоне</param>
        public Calzone(string? name, short weight) : base(name, weight)
        {
            if (name == null || name == string.Empty) this.name = DEFAULT_CALZONE_NAME;
        }


        // Деконструкторы

        /// <summary>
        /// Деконструктор кальцоне (название и вес)
        /// </summary>
        /// <param name="name">Название кальцоне</param>
        /// <param name="weight">Вес в граммах</param>
        public override void Deconstruct(out string name, out short weight)
        {
            base.Deconstruct(out name, out weight);
        }


        // Методы вывода информации

        /// <summary>
        /// Вывод краткого описания кальцоне
        /// </summary>
        public override void PrintFoodShort() => base.PrintFoodShort();

        /// <summary>
        /// Выводит дополнительную информацию к краткому описанию кальцоне
        /// </summary>
        public override void PrintFoodStatus()
        {
            Console.WriteLine("Тип блюда - кальцоне.");
            base.PrintFoodStatus();
        }

        /// <summary>
        /// Вывод полной информации о кальцоне
        /// </summary>
        public override void PrintFoodFull()
        {
            PrintFoodShort();
            PrintFoodStatus();
        }


        // Прочие методы

        /// <summary>
        /// Выпекать кальцоне
        /// </summary>
        public override void Bake() => base.Bake();

        /// <summary>
        /// Нарезать кальцоне
        /// </summary>
        /// <param name="slices">Запрашиваемое кол-во кусочков</param>
        public override void Cut(byte slices) => base.Cut(slices);

        /// <summary>
        /// Съесть кальцоне
        /// </summary>
        /// <param name="pieces">Запрашиваемое кол-во кусочков</param>
        public override void Eat(byte pieces) => base.Eat(pieces);
    }
}
