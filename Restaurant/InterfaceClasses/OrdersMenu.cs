using Restaurant.FoodClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.InterfaceClasses
{
    internal class OrdersMenu
    {
        private List<Food> orders = new List<Food>();

        // добавляет блюдо в список заказов из списка меню по индексу
        internal void MakeOrderByItem(Food foodItem)
        {
            switch (foodItem)
            {
                case Pizza pizza:
                    var (pizzaName, size) = pizza;
                    orders.Add(new Pizza(pizzaName, size));
                    break;
                case Calzone calzone:
                    var (calzoneName, calzoneWeight) = calzone;
                    orders.Add(new Calzone(calzoneName, calzoneWeight));
                    break;
                default:
                    break;
            }
        }

        // вывод меню взаимодействия с заказами
        public void ShowOrdersMenu()
        {
            int i = 0, menuChoice;

            Console.Clear();
            Console.WriteLine("Меню взаимодействия с заказами:");
            for (; i < orders.Count; i++)
            {
                Console.Write(i + 1 + ".");
                orders[i].PrintFoodShort(); // выводим элементы списка заказов
            }
            Console.WriteLine(++i + ".Выйти в основное меню.");

            Console.Write("Ваш выбор (цифра 1-{0}): ", i);
            string? choiceStr = Console.ReadLine(); // получаем строку с выбором

            menuChoice = Kitchen.CheckChoiseMenu(choiceStr, i);

            // if (menuChoice <= menu.Count) MakeOrderByIndex(menuChoice - 1); // если выбор не вышел за пределы списка меню
            if (menuChoice <= orders.Count) ShowOrderMenuByIndex(menuChoice - 1); // если выбор не вышел за пределы списка меню
        }



        // Вывод информации и действия с заказом
        private void ShowOrderMenuByIndex(int index)
        {
            int menuChoice, i = 3;

            Console.Clear();
            orders[index].PrintFoodShort();
            Console.WriteLine("Меню заказа:");
            Console.WriteLine("1.Вывести подробную информацию");
            Console.WriteLine("2.Взаимодействовать с заказом");
            Console.WriteLine("3.Выйти в главное меню");

            Console.Write("Ваш выбор (цифра 1-{0}): ", i);
            string? choiceStr = Console.ReadLine(); // получаем строку с выбором

            menuChoice = Kitchen.CheckChoiseMenu(choiceStr, i);

            if (menuChoice == 1) ShowFullOrderInfo(index);
            else if (menuChoice == 2) ShowInteractionMenuByIndex(index);
            else if (menuChoice == 3) return;
        }

        // вывести информацию о заказе
        private void ShowFullOrderInfo(int index)
        {
            Console.Clear();
            Console.WriteLine("Полная информация о заказе:");
            orders[index].PrintFoodFull();
            Console.WriteLine("Далее...");
            Console.ReadKey();
        }



        // вывести меню взаимодействия с заказами
        private void ShowInteractionMenuByIndex(int index)
        {
            int menuChoice, i = 4;
            Food order = orders[index];

            Console.Clear();
            order.PrintFoodShort();
            Console.WriteLine("Варианты взаимодействия:");
            Console.WriteLine("1.Запечь");
            Console.WriteLine("2.Нарезать");
            Console.WriteLine("3.Съесть");
            Console.WriteLine("4.Выйти из меню");

            Console.Write("Ваш выбор (цифра 1-{0}): ", i);
            string? choiceStr = Console.ReadLine(); // получаем строку с выбором

            menuChoice = Kitchen.CheckChoiseMenu(choiceStr, i);

            if (menuChoice == 1) order.Bake();
            else if (menuChoice == 2) order.Cut();
            else if (menuChoice == 3)
            {
                order.Eat();

                if (order.FoodIsEaten) orders.RemoveAt(index);

                Kitchen.WaitForUser();
            }


            else if (menuChoice == 4) return;
        }
    }
}
