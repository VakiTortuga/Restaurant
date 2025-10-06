using Restaurant.UIClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.FoodClasses
{
    /// <summary>
    /// Размер пиццы.
    /// </summary>
    enum PizzaSize
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }

    /// <summary>
    /// Класс пиццы
    /// </summary>
    internal class Pizza : FoodItem
    {
        // Поля

        /// <summary>
        /// Размер пиццы.
        /// </summary>
        /// <remarks>Размер предопределяет вес пиццы и наооборот.</remarks>
        private PizzaSize size;

        /// <summary>
        /// Количество подписчиков события OnPineappleReminder
        /// </summary>
        private static byte amountOfPineappleSubs = 0;

        // Константы

        /// <summary>
        /// Название пиццы по умолчанию.
        /// </summary>
        private const string DEFAULT_PIZZA_NAME = "Пицца";

        /// <summary>
        /// Время одного тика добавления ананасов в миллисекундах
        /// </summary>
        private const int ADD_PINEAPPLE_TICK_MILLISECS = 160;


        // Делегаты

        /// <summary>
        /// Событие для реакции подписанных пицц на напоминание о ананасах
        /// </summary>
        /// <remarks>
        /// <para>
        /// Используется для массового добавления ананасов ко всем заказанным пиццам.
        /// </para>
        /// <para>
        /// Подписка происходит при добавлении пиццы в заказы, отписка - при удалении.
        /// Это предотвращает изменение пицц в меню и утечки памяти.
        /// </para>
        /// <example>
        /// Pizza.OnPineappleReminder += pizza.AddPineapplesToName; // Подписка
        /// Pizza.OnPineappleReminder?.Invoke(); // Вызов события
        /// </example>
        /// </remarks>
        private static event Action? OnPineappleReminder;


        // Конструкторы

        /// <summary>
        /// Создает пиццу с укзанны весом (размер вычисляется автоматически)
        /// </summary>
        /// <param name="name">Название пиццы.</param>
        /// <param name="weight">Вес пиццы.</param>
        public Pizza(string? name, short weight) : base(name, weight)
        {
            if (name == null || name == string.Empty) this.name = DEFAULT_PIZZA_NAME;
            this.size = WeightToPizzaSize(base.weight.WeightInGramms);
        }

        /// <summary>
        /// Создает пиццу с указанным размером (вес вычисляется автоматически)
        /// </summary>
        /// <param name="name">Название пиццы.</param>
        /// <param name="size">Размер пиццы.</param>
        public Pizza(string? name, PizzaSize size) : this(name, PizzaSizeToWeight(size)) { }


        // Методы для enum PizzaSize

        /// <summary>
        /// Перевод размера в вес в граммах.
        /// </summary>
        /// <param name="size">Размер пиццы.</param>
        /// <returns>Предустановленные значения веса для каждого размера.</returns>
        private static short PizzaSizeToWeight(PizzaSize size)
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

        /// <summary>
        /// Перевод веса в размер пиццы.
        /// </summary>
        /// <param name="weight">Вес в граммах.</param>
        /// <returns>Перечислимый тип данных для размера</returns>
        private static PizzaSize WeightToPizzaSize(short weight)
        {
            if (weight < 400) return PizzaSize.Small;
            if (weight < 600) return PizzaSize.Medium;
            if (weight < 900) return PizzaSize.Large;
            return PizzaSize.ExtraLarge;
        }


        // Методы, связанные с обработкой события OnPineappleReminder

        /// <summary>
        /// Подписывает пиццу на событие и назначает метод для его обработки
        /// </summary>
        /// <remarks>
        /// Подписываются все пиццы, добавляемые в список заказов.
        /// <example>
        /// Pizza pizza = new Pizza("Маргарита", PizzaSize.Medium);
        /// pizza.Subscribe(); // Теперь пицца будет реагировать на RemindPineapples()
        /// </example>
        /// </remarks>
        public void Subscribe()
        {
            OnPineappleReminder -= AddPineapplesToName; // Отписываем пиццу во избежание двойной подписки
            OnPineappleReminder += AddPineapplesToName; // Подписываем пиццу на событие
            amountOfPineappleSubs++;
        }

        /// <summary>
        /// Отписывает пиццу от события (убирает обработчик).
        /// </summary>
        /// <remarks>Нужен для избежания утечки памяти.</remarks>
        public void Unsubscribe()
        {
            OnPineappleReminder -= AddPineapplesToName;
            amountOfPineappleSubs--;
        }

        /// <summary>
        /// Добавление суффикса "с ананасами" к названию пиццы
        /// </summary>
        private void AddPineapplesToName()
        {
            // Добавляем ананасы к названию, если их еще нет
            if (!name.Contains("ананас", StringComparison.OrdinalIgnoreCase)) name += " с ананасами";
        }

        /// <summary>
        /// Добавление ананасов к подписанным пиццам
        /// </summary>
        public static void RemindPineapples()
        {
            const byte minTicks = 4; // минимальное кол-во тиков добавления ананасов

            byte ticks = (amountOfPineappleSubs < minTicks) ? minTicks : amountOfPineappleSubs;

            Console.WriteLine("Не забудь добавить ананасы!!!");

            Console.WriteLine("Добавляем ананасы.");
            UserInteractions.ProgressBar(ticks, ADD_PINEAPPLE_TICK_MILLISECS);
            OnPineappleReminder?.Invoke(); // Создаем ананасовое событие
        }


        // Деконструкторы

        /// <summary>
        /// Деконструктор пиццы (название и вес)
        /// </summary>
        /// <param name="name">Название пиццы.</param>
        /// <param name="weight">Вес пиццы в граммах.</param>
        public override void Deconstruct(out string name, out short weight)
        {
            base.Deconstruct(out name, out weight);
        }

        /// <summary>
        /// Деконструктор пиццы (название и размер)
        /// </summary>
        /// <param name="name">Название пиццы.</param>
        /// <param name="size">Размер пиццы.</param>
        public void Deconstruct(out string name, out PizzaSize size)
        {
            base.Deconstruct(out name, out _);
            size = this.size;
        }


        // Методы вывода информации

        /// <summary>
        /// Вывод краткого описания пиццы.
        /// </summary>
        public override void PrintFoodShort() => base.PrintFoodShort();

        /// <summary>
        /// Выводит дополнительную информацию к краткому описанию пиццы
        /// </summary>
        public override void PrintFoodStatus()
        {
            Console.WriteLine("Тип блюда - пицца.");
            Console.WriteLine($"Размер - {size}.");
            base.PrintFoodStatus();
        }

        /// <summary>
        /// Вывод полной информации о пицце
        /// </summary>
        public override void PrintFoodFull()
        {
            PrintFoodShort();
            PrintFoodStatus();
        }

        // Прочие методы

        /// <summary>
        /// Выпекать пиццу
        /// </summary>
        public override void Bake() => base.Bake();

        /// <summary>
        /// Нарезка пиццы
        /// </summary>
        /// <param name="slices">Запрашиваемое кол-во кусочков</param>
        public override void Cut(byte slices) => base.Cut(slices);
        
        /// <summary>
        /// Съесть пиццу
        /// </summary>
        /// <param name="pieces">Запрашиваемое кол-во кусочков</param>
        public override void Eat(byte pieces) => base.Eat(pieces);
        
    }
}
