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
            Console.Write("Блюдо заказано!");
            Kitchen.WaitForUser();
        }

        // вывод меню взаимодействия с заказами
        public void ShowOrdersMenu()
        {
            while (true)
            {
                int menuChoice, amountOfChoices;

                Console.Clear();
                amountOfChoices = ShowOptionsFromOrdersMenu(); // выводим заказы на выбор

                if (amountOfChoices <= 1)
                {
                    Console.Write("Продолжить.");
                    Kitchen.WaitForUser();
                    return;
                }
                else
                {
                    Console.Write("Ваш выбор (цифра 1-{0}): ", amountOfChoices);

                    string? choiceStr = Console.ReadLine(); // получаем строку с выбором
                    menuChoice = Kitchen.CheckChoiseMenu(choiceStr, amountOfChoices);
                }
                    

                if (menuChoice <= orders.Count) ShowOrderMenuByIndex(menuChoice - 1); // если выбор не вышел за пределы списка меню
                else if (menuChoice == amountOfChoices) return;
                else
                {
                    Console.Write("Некорректный выбор.");
                    Kitchen.WaitForUser();
                }
            }
        }

        // вывод заказов и прочих опций меню
        private int ShowOptionsFromOrdersMenu()
        {
            int i = 0;

            if (orders.Count == 0)
            {
                Console.WriteLine("Заказов нет.");
                return i;
            }

            Console.WriteLine("Меню взаимодействия с заказами:");
            for (; i < orders.Count; i++)
            {
                Console.Write(i + 1 + ".");
                orders[i].PrintFoodShort(); // выводим элементы меню
            }
            Console.WriteLine(++i + ".Выйти в основное меню.");

            return i;
        }


        // Вывод информации и действия с заказом
        private void ShowOrderMenuByIndex(int index)
        {
            while (true)
            {
                int menuChoice, amountOfChoices = 3;

                Console.Clear();
                orders[index].PrintFoodShort(); // краткая инфа про заказ
                Console.WriteLine("Меню заказа:"); // меню взаимодействий с заказом
                Console.WriteLine("1.Вывести подробную информацию");
                Console.WriteLine("2.Взаимодействовать с заказом");
                Console.WriteLine("3.Выйти в главное меню");
                Console.Write("Ваш выбор (1-{0}): ", amountOfChoices);

                string? choiceStr = Console.ReadLine(); // получаем строку с выбором
                menuChoice = Kitchen.CheckChoiseMenu(choiceStr, amountOfChoices);

                if (menuChoice == 1) ShowFullOrderInfo(index);
                else if (menuChoice == 2)
                {
                    bool isEaten = false;
                    ShowInteractionMenuByIndex(index, ref isEaten);
                    if (isEaten) return;
                }
                else if (menuChoice == 3) return;
                else
                {
                    Console.Write("Некорректный выбор.");
                    Kitchen.WaitForUser();
                }
            }
        }

        // вывести информацию о заказе
        private void ShowFullOrderInfo(int index)
        {
            Console.Clear();
            Console.WriteLine("Полная информация о заказе:");
            orders[index].PrintFoodFull();
            Kitchen.WaitForUser();
        }

        // вывести меню взаимодействия с заказом
        private void ShowInteractionMenuByIndex(int index, ref bool isEaten)
        {
            int menuChoice, amountOfChoices = 4;
            Food order = orders[index];
            string? choiceStr;

            while (true)
            {
                Console.Clear();
                order.PrintFoodShort();
                Console.WriteLine("Варианты взаимодействия:");
                Console.WriteLine("1.Запечь");
                Console.WriteLine("2.Нарезать");
                Console.WriteLine("3.Съесть");
                Console.WriteLine("4.Выйти из меню");
                Console.Write("Ваш выбор (цифра 1-{0}): ", amountOfChoices);

                choiceStr = Console.ReadLine(); // получаем строку с выбором
                menuChoice = Kitchen.CheckChoiseMenu(choiceStr, amountOfChoices);

                if (menuChoice == 1) order.Bake();
                else if (menuChoice == 2)
                {
                    byte slices;
                    Console.Write("Введите кол-во кусочков: ");

                    while (true)
                    {
                        try
                        {
                            slices = Convert.ToByte(Console.ReadLine());
                            break;
                        }
                        catch (OverflowException)
                        {
                            slices = byte.MaxValue;
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.Write("Неверный формат, попробуйте еще раз: ");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Необрабатываемое исключение.\n" + e.Message);
                        }
                    }

                    order.Cut(slices);
                }
                else if (menuChoice == 3)
                {
                    order.Eat(); // пытаемся съесть

                    if (order.FoodIsEaten) // если получилось съесть
                    {
                        orders.RemoveAt(index); // удаляем заказ
                        isEaten = true; // возвращаем признак вызывающей функции
                        return;
                    }
                }
                else if (menuChoice == 4) return;
                else
                {
                    Console.Write("Некорректный выбор.");
                    Kitchen.WaitForUser();
                }  
            }
        }
    }
}
