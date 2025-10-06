using Restaurant.FoodClasses;
using Restaurant.UIClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.InterfaceClasses
{
    internal class FoodMenu
    {
        private List<FoodItem> menu = new List<FoodItem>();

        public FoodMenu()
        {
            this.InitFoodMenu();
        }

        private void InitFoodMenu()
        {
            menu.Add(new Pizza("Средняя Пепперони", PizzaSize.Medium));
            menu.Add(new Pizza("Маленькая Ананасовая", PizzaSize.Small));
            menu.Add(new Pizza("Большая Песто", PizzaSize.Large));
            menu.Add(new Calzone("Кольцоне с грибами", 250));
            menu.Add(new Calzone("Кольцоне с яблоком", 200));
            
            ++menu[0];
            --menu[2];
        }

        // вывод меню блюд, с возможностью сделать заказ, добавить блюдо и вернуться в главное меню
        public void ShowFoodMenu(in OrdersMenu ordersMenu)
        {
            int? menuChoice;
            int amountOfChoices = default;
            bool needToRedrawMenu = true;

            while (true)
            {
                if (needToRedrawMenu)
                {
                    Console.Clear();
                    amountOfChoices = ShowOptionsFromFoodMenu(); // выводит доступный хавчик, записывает кол-вариантов в переменную
                    needToRedrawMenu = false;
                }

                menuChoice = UserInteractions.GetMenuChoice(amountOfChoices); // получаем выбор пользователя
                if (menuChoice == null) continue;

                if (menuChoice <= menu.Count) ordersMenu.MakeOrderByItem(menu[(int)menuChoice - 1]); // если выбор не вышел за пределы списка меню
                else if (menuChoice == amountOfChoices - 1) // создание опции
                {
                    CreateOptionForOrder(); // создание опции (не заказывая)
                    needToRedrawMenu = true;
                    continue; // показываем обновленное меню;
                }
                else if (menuChoice == amountOfChoices) return;
                else
                {
                    Console.Write("Некорректный выбор.");
                    UserInteractions.WaitForUser();
                }
            }
        }

        // вывод хавчика и прочих опций меню
        private int ShowOptionsFromFoodMenu()
        {
            int i = 1;
            Console.WriteLine("Создание заказа.\n\nМеню блюд:");
            for (; i <= menu.Count; i++)
            {
                Console.Write(i + ".");
                menu[i - 1].PrintFoodShort(); // выводим элементы меню
            }

            Console.WriteLine($"\n{i}.Добавить блюдо в меню"); // выводим вариант для создания и заказа новой опции меню
            Console.WriteLine(++i + ".Вернуться в главное меню"); // выход в главное меню кухни
            return i;
        }

        // Создание опции меню еды
        private void CreateOptionForOrder()
        {
            Console.Clear();
            Console.WriteLine("Добавление блюда в меню.");
            Console.WriteLine("\nВыберите тип блюда:");
            Console.WriteLine("1.Пицца");
            Console.WriteLine("2.Кальцоне");
            Console.WriteLine("3.Отмена создания блюда");

            while (true)
            {
                Console.Write("\nВаш выбор (1-3): ");
                string? choice = Console.ReadLine();

                FoodItem newFood;
                switch (choice)
                {
                    case "1":
                        newFood = CreatePizza();
                        break;
                    case "2":
                        newFood = CreateCalzone();
                        break;
                    case "3":
                        Console.WriteLine("Создание блюда было отменено.");
                        UserInteractions.WaitForUser();
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод");
                        continue;
                }

                menu.Add(newFood);
                Console.WriteLine("Блюдо успешно добавлено в меню!");
                UserInteractions.WaitForUser();
                return;
            }

        }

        // Создание пиццы
        private Pizza CreatePizza()
        {
            Console.Write("\nВведите название пиццы: ");
            string? name = Console.ReadLine();

            PizzaSize size = SelectPizzaSize();

            return new Pizza(name, size);
        }

        // Создание кальцоне
        private Calzone CreateCalzone()
        {
            Console.Write("\nВведите название кальцоне: ");
            string? name = Console.ReadLine();

            short weight;
            Console.Write($"Введите вес в граммах ({FoodWeight.MIN_WEIGHT}-{FoodWeight.MAX_WEIGHT}): ");
            UserInteractions.ChooseAmount(out weight);

            return new Calzone(name, weight);
        }

        // выбор размера пиццы
        private PizzaSize SelectPizzaSize()
        {
            Console.WriteLine("Выберите размер пиццы:");
            Console.WriteLine("1. Маленькая");
            Console.WriteLine("2. Средняя");
            Console.WriteLine("3. Большая");
            Console.WriteLine("4. Огромная");

            while (true)
            {
                Console.Write("Введите номер (1-4): ");
                string? input = Console.ReadLine();

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
