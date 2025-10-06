using Restaurant.FoodClasses;
using Restaurant.UIClasses.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.UIClasses.menu
{
    /// <summary>
    /// Класс меню заказов
    /// </summary>
    internal class OrdersMenu
    {
        // Поля

        /// <summary>
        /// Список заказов, элементы списка - еда
        /// </summary>
        private List<FoodItem> orders = new List<FoodItem>();


        // Методы

        /// <summary>
        /// Добавляет блюдо в список заказов
        /// </summary>
        /// <param name="foodItemToOrder">Добавляемое блюдо</param>
        /// <remarks>При добавлении выполняется глубокое копирование.
        /// Добавляемые пиццы подписываются на событие OnPineappleReminder.
        /// </remarks>
        internal void MakeOrder(FoodItem foodItemToOrder)
        {
            const byte amountOfTicks = 10;
            const byte orderTickMillisecs = 120;

            switch (foodItemToOrder) // выполняем инструкции для соответствующего типа еды
            {
                case Pizza pizza:
                    string pizzaName;
                    short weight;
                    pizza.Deconstruct(out pizzaName, out weight);

                    Pizza newPizza = new Pizza(pizzaName, weight); // Создаем новую пиццу
                    newPizza.SubscribeToPineapples(); // Подписываем пиццу на событие
                    orders.Add(newPizza); // Добавляем пиццу в заказы
                    break;
                case Calzone calzone:
                    var (calzoneName, calzoneWeight) = calzone;
                    orders.Add(new Calzone(calzoneName, calzoneWeight)); // Добавляем новый кальцоне в заказы
                    break;
                default: // Если нет подходящего типа
                    Console.WriteLine("Невозможно заказать блюдо!");
                    UserInteractions.WaitForUser();
                    return;
            }

            var (name, _) = foodItemToOrder;

            Console.Write("Оформляем заказ.");
            UserInteractions.ProgressBar(amountOfTicks, orderTickMillisecs);
            Console.WriteLine($"\"{name}\" заказано!");
        }

        /// <summary>
        /// вывод меню взаимодействия с списком заказов
        /// </summary>
        public void ShowOrdersMenu()
        {
            int? menuChoice; // выбор пользователя, null для пропуска итерации с некорректным вводом
            int amountOfChoices = default; // кол-во пунктов меню

            /*
             * Флаг, показывающий необходимость перерисовать меню
             * после выхода из других методов, содержащих меню.
             * На первой итерации необходимо вывести меню.
             */
            bool needToRedrawMenu = true;

            while (true)
            {
                if (needToRedrawMenu) // Выводим меню при необходимости
                {
                    Console.Clear();
                    amountOfChoices = ShowOptionsForOrdersMenu();
                    if (amountOfChoices <= 1) return;
                    needToRedrawMenu = false;
                }

                menuChoice = UserInteractions.GetMenuChoice(amountOfChoices); // получаем выбор пользователя
                if (menuChoice == null) continue; // если не выбран пункт - следующая итерация

                // Обработка выбора
                if (menuChoice <= orders.Count) // Если выбран заказ
                {
                    ShowOneOrderMenu((int)menuChoice - 1); // Меню отдельного заказа
                    needToRedrawMenu = true;
                }
                else if (menuChoice == amountOfChoices - 1) // Если выбран пункт напомнить про ананасы
                {
                    Pizza.RemindOfPineapples();
                    needToRedrawMenu = true;
                }
                else if (menuChoice == amountOfChoices) return; // Если выбран выход из меню
                else Console.Write("Некорректный выбор.");
            }
        }

        /// <summary>
        /// Меню заказов. Вывод заказов и прочих опций.
        /// </summary>
        /// <returns>Количество опций меню.</returns>
        private int ShowOptionsForOrdersMenu()
        {
            int i = 0;

            if (orders.Count == 0) // если список заказов пуст
            {
                Console.WriteLine("Заказов нет.");
                UserInteractions.WaitForUser();
                return i;
            }

            Console.WriteLine("Меню заказов.");
            Console.WriteLine("Опции для взаимодействия:");
            for (; i < orders.Count; i++) // выводим опции (заказы)
            {
                Console.Write((i + 1) + ".");
                orders[i].PrintFoodShort();
            } // при выходе из цикла i будет равен кол-ву заказов

            // вывод дополнительных опций
            Console.WriteLine();
            Console.WriteLine(++i + ".Напомнить про ананасы.");
            Console.WriteLine(++i + ".Выйти в основное меню.");

            return i;
        }

        /// <summary>
        /// Вывод меню взаимодействия с заказом
        /// </summary>
        /// <param name="index">Индекс заказа в списке</param>
        private void ShowOneOrderMenu(int index)
        {
            int? menuChoice; // выбор пользователя, null для пропуска итерации с некорректным вводом
            const int amountOfChoices = 3; // кол-во опций меню

            /*
             * Флаг, показывающий необходимость перерисовать меню
             * после выхода из других методов, содержащих меню.
             * На первой итерации необходимо вывести меню.
             */
            bool needToRedrawMenu = true;

            while (true)
            {
                if (needToRedrawMenu) // Выводим меню при необходимости
                {
                    Console.Clear();
                    Console.WriteLine("Заказ:");
                    orders[index].PrintFoodShort(); // краткая инфа про заказ
                    Console.WriteLine("\nМеню заказа:"); // меню взаимодействий с заказом
                    Console.WriteLine("1.Вывести подробную информацию");
                    Console.WriteLine("2.Взаимодействовать с заказом");
                    Console.WriteLine("3.Выйти в меню заказов");
                    needToRedrawMenu = false;
                }

                menuChoice = UserInteractions.GetMenuChoice(amountOfChoices); // получаем выбор пользователя
                if (menuChoice == null) continue; // если не выбран пункт - следующая итерация

                // Обрабатываем ввод
                switch (menuChoice)
                {
                    case 1: // Вывод подробной информации
                        ShowFullOrderInfo(index);
                        needToRedrawMenu = true;
                        break;
                    case 2: // Взаимодействие с заказом
                        /*
                         * Флаг съеденности необходим для выхода из меню заказа при его удалении (съедании).
                         * Если этого не выполнять, функция откроет другой заказ
                         * или вызовет исключение из-за выхода индекса за пределы массива
                         */
                        bool isEaten = false; 

                        ShowOrderInteractionMenu(index, ref isEaten); // Вывод меню взаимодействия
                        if (isEaten) return;
                        needToRedrawMenu = true;
                        break;
                    case 3: // Выход из меню
                        return;
                    default:
                        Console.Write("Некорректный выбор.");
                        break;
                }
            }
        }

        /// <summary>
        /// Вывод полной информации о заказе
        /// </summary>
        /// <param name="index">Индекс заказа в списке</param>
        private void ShowFullOrderInfo(int index)
        {
            Console.Clear();
            Console.WriteLine("Полная информация о заказе:");
            orders[index].PrintFoodFull();
            UserInteractions.WaitForUser();
        }

        /// <summary>
        /// Вывести меню взаимодействия с заказом
        /// </summary>
        /// <param name="index">Индекс заказа в списке</param>
        /// <param name="isEaten">Флаг съеденности заказа</param>
        private void ShowOrderInteractionMenu(int index, ref bool isEaten)
        {
            int? menuChoice; // выбор пользователя, null для пропуска итерации с некорректным вводом
            const int amountOfChoices = 4; // кол-во опций меню

            /*
             * Флаг, показывающий необходимость перерисовать меню
             * после выхода из других методов, содержащих меню.
             * На первой итерации необходимо вывести меню.
             */
            bool needToRedraw = true;

            FoodItem order = orders[index]; // записываем заказ в переменную для удобства

            while (true)
            {
                if (needToRedraw) // Выводим меню при необходимости
                {
                    Console.Clear();
                    Console.WriteLine("Заказ:");
                    order.PrintFoodShort();
                    Console.WriteLine("\nВарианты взаимодействия:");
                    Console.WriteLine("1.Запечь");
                    Console.WriteLine("2.Нарезать");
                    Console.WriteLine("3.Съесть");
                    Console.WriteLine("4.Выйти");
                    needToRedraw = false;
                }

                menuChoice = UserInteractions.GetMenuChoice(amountOfChoices); // получаем выбор пользователя
                if (menuChoice == null) continue; // если не выбран пункт - следующая итерация

                // Обрабатываем ввод
                switch (menuChoice)
                {
                    case 1: // Запекание
                        order.Bake();
                        break;
                    case 2: // Нарезка
                        if (order.IsBaked == false)
                        {
                            Console.WriteLine("Сначала нужно запечь блюдо.");
                            continue;
                        }

                        if (order.IsCut == true)
                        {
                            Console.WriteLine("Блюдо уже нарезано.");
                            continue;
                        }

                        byte slices;
                        Console.Write("Введите кол-во кусочков для нарезки: ");
                        UserInteractions.ChooseAmount(out slices); // Получаем ввод пользователя

                        order.Cut(slices);
                        break;
                    case 3: // Поедание
                        if (order.IsBaked != true)
                        {
                            Console.WriteLine("Сначала нужно запечь блюдо.");
                            continue;
                        }

                        byte pieces;
                        Console.Write($"Введите кол-во кусочков, которое хотите съесть (максимум {order.AmountOfSlices}): ");
                        UserInteractions.ChooseAmount(out pieces); // Получаем ввод пользователя

                        order.Eat(pieces);

                        if (order.IsEaten)
                        {
                            if (order is Pizza) ((Pizza)order).Unsubscribe(); // отписываемся от события
                            orders.RemoveAt(index); // удаляем заказ
                            isEaten = true; // возвращаем признак съеденности для вызывающей функции
                            UserInteractions.WaitForUser();
                            return;
                        }

                        break;
                    case 4: // Выход
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор");
                        break;
                }
            }
        }
    }
}
