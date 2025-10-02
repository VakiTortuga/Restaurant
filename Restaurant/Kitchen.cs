using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Kitchen : IKitchen
    {
        private List<Food> menu = new List<Food>();
        private List<Food> orders = new List<Food>();

        public void InitFoodMenu()
        {
            menu.Add(new Pizza("Средняя Пепперони", PizzaSize.Medium));
            menu.Add(new Pizza("Маленькая Ананасовая", PizzaSize.Small));
            menu.Add(new Pizza("Большая Песто", PizzaSize.Large));
            menu.Add(new Calzone("Кольцоне с грибами", 250));
            menu.Add(new Calzone("Кольцоне с яблоком", 200));
        }

        // выводит меню кухни (добавление и просмотр заказов)
        public void ShowMainKitchenMenu() 
        {
            this.InitFoodMenu(); // или вынести это в конструктор

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню кухни:");
                Console.WriteLine("1.Сделать заказ");
                Console.WriteLine("2.Просмотреть заказы");
                Console.WriteLine("3.Выйти из меню");

                Console.Write("Ваш выбор (цифра 1-3): ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        this.ShowFoodMenu();
                        break;
                    case "2":
                        this.ShowOrders();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // вывод меню блюд, с возможностью сделать заказ, добавить блюдо и вернуться в главное меню
        private void ShowFoodMenu()
        {
            // переменные для вывода блюд меню
            int menuChoice, amountOfChoices;

            amountOfChoices = ShowOptionsFromFoodMenu(); // выводит доступный хавчик, записывает кол-вариантов в переменную

            Console.Write("Ваш выбор (цифра 1-{0}): ", amountOfChoices);
            string? choiceStr = Console.ReadLine(); // получаем строку с выбором

            menuChoice = CheckChoiseFromFoodMenu(choiceStr, amountOfChoices);

            if (menuChoice <= menu.Count && menuChoice > 0) MakeOrderByIndex(menuChoice - 1); // если выбор не вышел за пределы списка меню
            else if (menuChoice == (amountOfChoices - 1))
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
            string name;
            FoodWeight weight;
            int i = 1;
            Console.Clear();
            Console.WriteLine("Создание заказа.\nМеню блюд:");
            for (; i <= menu.Count; i++) // выводим элементы меню
            {
                this.menu[i - 1].Deconstruct(out name, out weight); // получаем значения полей имени и веса
                Console.WriteLine(i + "." + name + ". Вес - " + weight.ToString()); // выводим информацию о блюде
            }
            Console.WriteLine($"\n{i}.Добавить блюдо в меню"); // выводим вариант для создания и заказа новой опции меню
            Console.WriteLine((++i) + ".Вернуться в главное меню"); // выход в главное меню кухни
            return i;
        }

        
        private int CheckChoiseFromFoodMenu(string? choiceStr, int amountOfChoices)
        {
            int menuChoice;
            if (choiceStr != null)
            {
                try
                {
                    menuChoice = int.Parse(choiceStr); // записываем выбор в целочисленную переменную
                    if (menuChoice > 0 && menuChoice <= amountOfChoices)
                        return menuChoice;
                    else
                        Console.WriteLine("Некорректный выбор.");
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Некорректный выбор. " + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Необрабатываемое исключение. " + e.Message);
                }
                Console.Write("...");
                Console.ReadKey();
            }
            return amountOfChoices; // Если строка с выбором пуста, выбрать выход из меню
        }

        // добавляет блюдо в список заказов из списка меню по индексу
        private void MakeOrderByIndex(int index)
        {
            Food foodItem = this.menu[index];

            switch (foodItem)
            {
                case Pizza pizza:
                    var (pizzaName, size) = pizza;
                    orders.Add(new Pizza(pizzaName, size));
                    break;
                case Calzone calzone:
                    var (calzoneName, calzoneWeight) = calzone;
                    orders.Add(new Calzone(calzoneName, calzoneWeight.Weight));
                    break;
                default:
                    break;
            }
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

        // вывод меню взаимодействия с заказами
        private void ShowOrders()
        {
            Console.WriteLine("Опция временно недоступна...");
            Console.ReadKey();
        }

        // вывести информацию о заказе
        private void ShowOrderInfo(int number) { }

        // вывести меню взаимодействия с заказами
        private void ShowOrderInteractions(int number) { }


    }
}
