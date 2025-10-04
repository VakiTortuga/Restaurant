using Restaurant.FoodClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.InterfaceClasses
{
    internal class Kitchen : IKitchen
    {
        private FoodMenu foodMenu;
        private OrdersMenu ordersMenu;

        public Kitchen()
        {
            foodMenu = new FoodMenu();
            ordersMenu = new OrdersMenu();
        }

        public static void WaitForUser()
        {
            Console.Write("...");
            Console.ReadKey();
        }

        public static int CheckChoiseMenu(string? choiceStr, int amountOfChoices)
        {
            int menuChoice;

            if (choiceStr == null) return amountOfChoices; // Если строка с выбором пуста, выбрать выход из меню

            try
            {
                menuChoice = int.Parse(choiceStr); // записываем выбор в целочисленную переменную
                if (menuChoice > 0 && menuChoice <= amountOfChoices) return menuChoice;
                else Console.Write("Не соблюден интервал!");
            }
            catch (FormatException)
            {
                Console.Write("Некорректный формат ввода!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Необрабатываемое исключение!\n" + e.Message);
            }

            WaitForUser();
            return amountOfChoices; // Если строка с выбором пуста, выбрать выход из меню
        }

        // выводит меню кухни (добавление и просмотр заказов)
        public void ShowMainKitchenMenu() 
        {
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
                        foodMenu.ShowFoodMenu(ordersMenu);
                        break;
                    case "2":
                        ordersMenu.ShowOrdersMenu();
                        break;
                    case "3":
                        return;
                    default:
                        Console.Write("Некорректный выбор.");
                        WaitForUser();
                        break;
                }
            }
        }
    }
}
