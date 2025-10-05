using Restaurant.FoodClasses;
using Restaurant.UIClasses;
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

        // выводит меню кухни (добавление и просмотр заказов)
        public void ShowMainKitchenMenu() 
        {
            bool needToRedraw = true;
            while (true)
            {
                if (needToRedraw)
                {
                    Console.Clear();
                    Console.WriteLine("Меню кухни:");
                    Console.WriteLine("1.Сделать заказ");
                    Console.WriteLine("2.Просмотреть заказы");
                    Console.WriteLine("3.Выйти из меню");
                    needToRedraw = false;
                }

                Console.Write("\nВаш выбор (цифра 1-3): ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        foodMenu.ShowFoodMenu(ordersMenu);
                        needToRedraw = true;
                        break;
                    case "2":
                        ordersMenu.ShowOrdersMenu();
                        needToRedraw = true;
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
        }
    }
}
