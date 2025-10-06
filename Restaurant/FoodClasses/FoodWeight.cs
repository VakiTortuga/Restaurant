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
    /// Класс для хранения веса блюда
    /// </summary>
    internal struct FoodWeight
    {
        // Поля

        /// <summary>
        /// Значение веса в граммах
        /// </summary>
        private short weight;

        // Константы

        /// <summary>
        /// Минимальный вес блюда.
        /// </summary>
        public const short MIN_WEIGHT = 100;
        /// <summary>
        /// Максимальный вес блюда.
        /// </summary>
        public const short MAX_WEIGHT = 2000;

        // Свойства

        /// <summary>
        /// Свойство для установки значения веса
        /// </summary>
        public short WeightInGramms
        {
            get => weight;
            set // сеттер с проверкой и установкой дефолта при некорректном значении
            {
                if (value <= MIN_WEIGHT || value >= MAX_WEIGHT)
                {
                    weight = MIN_WEIGHT;
                    Console.WriteLine($"Введено некорректное значение веса - {value}.\nЗначение установлено на {weight}.");
                }
                else weight = value;
            }
        }

        /// <summary>
        /// Конструктор веса блюда
        /// </summary>
        /// <param name="weight">Вес блюда в граммах.</param>
        /// <remarks>Значение веса в объекте всегда будет корректным.</remarks>
        public FoodWeight(short weight) => WeightInGramms = weight;


        /// <summary>
        /// Перевод веса в строковое представление.
        /// </summary>
        /// <returns>Строковое представление веса блюда.</returns>
        public override string ToString() => $"{WeightInGramms} грамм";
    }
}
