using Restaurant.FoodClasses;
using Restaurant.UIClasses;
using Restaurant.UIClasses.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.UIClasses.menu
{
    /// <summary>
    /// Класс кухни для управления заказами в ресторане
    /// </summary>
    internal class Kitchen : IKitchen
    {
        // Поля

        /// <summary>
        /// Меню блюд
        /// </summary>
        private FoodMenu foodMenu;

        /// <summary>
        /// Меню заказов
        /// </summary>
        private OrdersMenu ordersMenu;


        // Конструкторы

        /// <summary>
        /// Конструктор кухни, создает меню блюд и меню заказов
        /// </summary>
        public Kitchen()
        {
            foodMenu = new FoodMenu();
            ordersMenu = new OrdersMenu();
        }

        // Методы

        /// <summary>
        /// Вывод меню кухни (добавление заказов и управление ими)
        /// </summary>
        public void ShowKitchenMenu() 
        {
            /*
             * Флаг, показывающий необходимость перерисовать меню
             * после выхода из других методов, содержащих меню.
             * На первой итерации необходимо вывести меню.
             */
            bool needToRedraw = true; 

            while (true)
            {
                if (needToRedraw) // Выводим меню при необходимости
                {
                    Console.Clear();
                    Console.WriteLine("Меню кухни:");
                    Console.WriteLine("1.Сделать заказ");
                    Console.WriteLine("2.Просмотреть заказы");
                    Console.WriteLine("3.Выйти из меню");
                    needToRedraw = false;
                }

                Console.Write("\nВаш выбор (цифра 1-3): ");
                string? choice = Console.ReadLine(); // Ввод пользователя

                switch (choice) // Обрабатываем ввод
                {
                    case "1":
                        // Открываем меню блюд (передаем меню заказов для возможности заказывать блюда)
                        foodMenu.ShowFoodMenu(ordersMenu);
                        needToRedraw = true; // перерисовываем меню после метода 
                        break;
                    case "2":
                        // Открываем меню заказов
                        ordersMenu.ShowOrdersMenu();
                        needToRedraw = true; // перерисовываем меню после метода 
                        break;
                    case "3":
                        // Выход из меню кухни
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
        }
    }
}
