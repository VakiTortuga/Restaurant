using Restaurant.FoodClasses;
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
        private List<Food> menu = new List<Food>();

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
        }

        // вывод меню блюд, с возможностью сделать заказ, добавить блюдо и вернуться в главное меню
        public void ShowFoodMenu(in OrdersMenu ordersMenu)
        {
            while (true)
            {
                int menuChoice, amountOfChoices;

                Console.Clear();
                amountOfChoices = ShowOptionsFromFoodMenu(); // выводит доступный хавчик, записывает кол-вариантов в переменную
                Console.Write("Ваш выбор (цифра 1-{0}): ", amountOfChoices);

                string? choiceStr = Console.ReadLine(); // получаем строку с выбором
                menuChoice = Kitchen.CheckChoiseMenu(choiceStr, amountOfChoices); // получаем целочисленный выбор


                if (menuChoice <= menu.Count) ordersMenu.MakeOrderByItem(menu[menuChoice - 1]); // если выбор не вышел за пределы списка меню
                else if (menuChoice == amountOfChoices - 1) // создание опции
                {
                    try
                    {
                        CreateOptionForOrder(); // создание опции (не заказывая)
                        continue; // показываем обновленное меню
                    }
                    catch (ArgumentException)
                    {
                        Console.Write("Некорректный формат ввода!\nСоздание опции меню отменено.");
                    }
                    catch (Exception e)
                    {
                        Console.Write("Необрабатываемое исключение.\nСоздание опции меню отменено.");
                    }
                    Kitchen.WaitForUser();
                }
                else if (menuChoice == amountOfChoices) return;
                else
                {
                    Console.Write("Некорректный выбор.");
                    Kitchen.WaitForUser();
                }
            }
        }

        // вывод хавчика и прочих опций меню
        private int ShowOptionsFromFoodMenu()
        {
            int i = 1;
            Console.WriteLine("Создание заказа.\nМеню блюд:");
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
            Console.WriteLine("Выберите тип блюда:");
            Console.WriteLine("1.Пицца");
            Console.WriteLine("2.Кальцоне");
            Console.WriteLine("3.Отмена создания блюда");
            Console.Write("Ваш выбор (1-3): ");

            string? choice = Console.ReadLine();
            if (choice == "3")
            {
                Console.WriteLine("Создание блюда было отменено.");
                Kitchen.WaitForUser();
                return;
            }

            Food newFood = choice switch
            {
                "1" => CreatePizza(),
                "2" => CreateCalzone(),
                _ => throw new ArgumentException("Некорректный выбор.")
            };

            menu.Add(newFood);
            Console.Write("Блюдо успешно добавлено в меню!");
            Kitchen.WaitForUser();
        }

        // Создание пиццы
        private Pizza CreatePizza()
        {
            Console.Write("Введите название пиццы: ");
            string? name = Console.ReadLine();

            PizzaSize size = SelectPizzaSize();

            return new Pizza(name, size);
        }

        // Создание кальцоне
        private Calzone CreateCalzone()
        {
            Console.Write("Введите название кальцоне: ");
            string? name = Console.ReadLine();

            short weight;
            Console.Write("Введите вес в граммах: ");
            string? weightStr = Console.ReadLine();
            if (weightStr != null) weight = short.Parse(weightStr); // лучше использовать TryParse
            else weight = 0;

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
