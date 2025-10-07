using Restaurant.FoodClasses;
using Restaurant.UIClasses.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.UIClasses.menu
{
    /// <summary>
    /// Класс меню блюд
    /// </summary>
    internal class FoodMenu
    {
        // Поля

        /// <summary>
        /// Список меню блюд
        /// </summary>
        private List<FoodItem> menu = new List<FoodItem>();


        // Конструкторы

        /// <summary>
        /// Конструктор с инициализацией списка меню
        /// </summary>
        public FoodMenu()
        {
            InitFoodMenu();
        }


        // Прочие методы

        /// <summary>
        /// Инициализация списка меню
        /// </summary>
        private void InitFoodMenu()
        {
            menu.Add(new Pizza("Средняя Пепперони", PizzaSize.Medium));
            menu.Add(new Pizza("Маленькая Ананасовая", PizzaSize.Small));
            menu.Add(new Pizza("Большая Песто", PizzaSize.Large));
            menu.Add(new Calzone("Кольцоне с грибами", 250));
            menu.Add(new Calzone("Кольцоне с яблоком", 200));
            
            ++menu[0]; // демонстрация работы арифметических операций
            --menu[2];
        }

        /// <summary>
        /// Вывод меню блюд для добавления заказов и элементов в меню
        /// </summary>
        /// <param name="ordersMenu">список заказов, куда добавляем еще один</param>
        public void ShowFoodMenu(in OrdersMenu ordersMenu)
        {
            int? menuChoice; // вывбор пользователя
            int amountOfChoices = default; // кол-во опций меню

            /*
             *Флаг, показывающий необходимость перерисовать меню
             *после выхода из других методов, содержащих меню.
             * На первой итерации необходимо вывести меню.
             */
            bool needToRedrawMenu = true;

            while (true)
            {
                if (needToRedrawMenu) // Выводим меню при необходимости
                {
                    Console.Clear();
                    amountOfChoices = ShowOptionsFromFoodMenu(); // выводит вывод меню, запись кол-ва опций в переменную
                    needToRedrawMenu = false;
                }

                menuChoice = UserInteractions.GetMenuChoice(amountOfChoices); // получаем выбор пользователя
                if (menuChoice == null) continue; // выход при некорректном вводе

                // Обрабатываем ввод
                if (menuChoice <= menu.Count) ordersMenu.MakeOrder(menu[(int)menuChoice - 1]); // если выбор в пределах списка меню
                else if (menuChoice == amountOfChoices - 1) // создание опции
                {
                    CreateOptionForOrder(); // создание опции (не заказывая)
                    needToRedrawMenu = true; // показываем обновленное меню;
                }
                else if (menuChoice == amountOfChoices) return; // выход из меню
                else Console.Write("Некорректный выбор.");
            }
        }

        /// <summary>
        /// Меню блюд. Вывод блюд меню и прочих опций.
        /// </summary>
        /// <returns>Количество опций меню.</returns>
        private int ShowOptionsFromFoodMenu()
        {
            int i = 0;

            Console.WriteLine("Создание заказа.\n\nМеню блюд:");
            for (; i < menu.Count; i++) // выводим опции меню блюд
            {
                Console.Write((i + 1) + ".");
                menu[i].PrintFoodShort(); 
            } // при выходе из цикла i будет равен кол-ву заказов

            Console.WriteLine(++i + ".Добавить блюдо в меню"); // вариант создания новой опции
            Console.WriteLine(++i + ".Вернуться в главное меню"); // выход в главное меню кухни
            return i;
        }

        /// <summary>
        /// Создание опции для меню еды
        /// </summary>
        private void CreateOptionForOrder()
        {
            // Вывод меню
            Console.Clear();
            Console.WriteLine("Добавление блюда в меню.");
            Console.WriteLine("\nВыберите тип блюда:");
            Console.WriteLine("1.Пицца");
            Console.WriteLine("2.Кальцоне");
            Console.WriteLine("3.Отмена создания блюда");

            while (true)
            {
                Console.Write("\nВаш выбор (1-3): ");
                string? choice = Console.ReadLine(); // прием ввода

                FoodItem newFood; // Новый элемент меню

                // обрабатываем ввод
                switch (choice)
                {
                    case "1":
                        newFood = CreatePizza(); // создаем пиццу
                        break;
                    case "2":
                        newFood = CreateCalzone(); // создаем кальцоне
                        break;
                    case "3":
                        Console.WriteLine("Создание блюда было отменено.");
                        UserInteractions.WaitForUser();
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод");
                        continue;
                }

                menu.Add(newFood); // добавляем блюдо в меню
                Console.WriteLine("Блюдо успешно добавлено в меню!");
                UserInteractions.WaitForUser();
                return;
            }

        }

        /// <summary>
        /// Создаение пиццы
        /// </summary>
        /// <returns>Новый объект пиццы</returns>
        private Pizza CreatePizza()
        {
            Console.Write("\nВведите название пиццы: ");
            string? name = Console.ReadLine();

            PizzaSize size = SelectPizzaSize(); // выбор размера пиццы

            return new Pizza(name, size);
        }

        /// <summary>
        /// Создаение кальцоне
        /// </summary>
        /// <returns>Новый объект кальцоне</returns>
        private Calzone CreateCalzone()
        {
            Console.Write("\nВведите название кальцоне: ");
            string? name = Console.ReadLine();

            short weight; // вес кальцоне

            Console.Write($"Введите вес в граммах ({FoodWeight.MIN_WEIGHT}-{FoodWeight.MAX_WEIGHT}): ");
            UserInteractions.ChooseAmount(out weight); // ввод кол-ва грамм

            return new Calzone(name, weight);
        }

        /// <summary>
        /// Выбор размера пиццы
        /// </summary>
        /// <returns>Выбранный размер пиццы</returns>
        private PizzaSize SelectPizzaSize()
        {
            // меню выбора
            Console.WriteLine("Выберите размер пиццы:");
            Console.WriteLine("1. Маленькая");
            Console.WriteLine("2. Средняя");
            Console.WriteLine("3. Большая");
            Console.WriteLine("4. Огромная");

            while (true)
            {
                Console.Write("Введите номер (1-4): ");
                string? input = Console.ReadLine();

                // обрабатываем ввод, возвращаем предустановленные значения
                switch (input)
                {
                    case "1":
                        return PizzaSize.Small;
                    case "2":
                        return PizzaSize.Medium;
                    case "3":
                        return PizzaSize.Large;
                    case "4":
                        return PizzaSize.ExtraLarge;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}
