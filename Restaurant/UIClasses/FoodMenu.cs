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
            // переменные для вывода блюд меню
            int menuChoice, amountOfChoices;

            amountOfChoices = ShowOptionsFromFoodMenu(); // выводит доступный хавчик, записывает кол-вариантов в переменную

            Console.Write("Ваш выбор (цифра 1-{0}): ", amountOfChoices);
            string? choiceStr = Console.ReadLine(); // получаем строку с выбором

            menuChoice = Kitchen.CheckChoiseMenu(choiceStr, amountOfChoices);

            if (menuChoice <= menu.Count) ordersMenu.MakeOrderByItem(menu[menuChoice - 1]); // если выбор не вышел за пределы списка меню
            else if (menuChoice == amountOfChoices - 1)
            {
                try
                {
                    CreateOptionForOrder(); // создание опции
                    return;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message + "Создание опции меню отменено.");
                }
                catch (Exception e)
                {
                    Console.Write("Необрабатываемое исключение. ");
                    Console.WriteLine(e.Message);
                }
                Console.Write("...");
                Console.ReadKey();
            }
        }

        private int ShowOptionsFromFoodMenu()
        {
            int i = 1;
            Console.Clear();
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
        public void CreateOptionForOrder()
        {
            Console.Clear();
            Console.WriteLine("Выберите тип блюда:");
            Console.WriteLine("1.Пицца");
            Console.WriteLine("2.Кальцоне");
            Console.WriteLine("3.Отмена создания блюда");
            Console.Write("Ваш выбор: ");

            string? choice = Console.ReadLine();
            if (choice == "3")
            {
                Console.WriteLine("Создание блюда было отменено...");
                Console.ReadKey();
                return;
            }

            Food newFood = choice switch
            {
                "1" => CreatePizza(),
                "2" => CreateCalzone(),
                _ => throw new ArgumentException("Некорректный выбор.")
            };

            menu.Add(newFood);
            Console.WriteLine("Блюдо успешно добавлено в меню!..");
            Console.ReadKey();
        }

        // Заказ пиццы
        private Pizza CreatePizza()
        {
            Console.Write("Введите название пиццы: ");
            string? name = Console.ReadLine();

            PizzaSize size = SelectPizzaSize();

            return new Pizza(name, size);
        }

        // Заказ кальцоне
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
