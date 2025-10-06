using Restaurant.FoodClasses;
using Restaurant.UIClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.InterfaceClasses
{
    internal class OrdersMenu
    {
        private List<FoodItem> orders = new List<FoodItem>();

        // добавляет блюдо в список заказов из списка меню по индексу
        internal void MakeOrderByItem(FoodItem foodItem)
        {

            switch (foodItem)
            {
                case Pizza pizza:
                    string pizzaName;
                    short weight;
                    pizza.Deconstruct(out pizzaName, out weight);
                    Pizza newPizza = new Pizza(pizzaName, weight);
                    newPizza.Subscribe();
                    orders.Add(newPizza);
                    break;
                case Calzone calzone:
                    var (calzoneName, calzoneWeight) = calzone;
                    orders.Add(new Calzone(calzoneName, calzoneWeight));
                    break;
                default:
                    break;
            }
            var (name, _) = foodItem;
            Console.Write($"Оформляем заказ.");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                Console.Write('.');
            }
            Console.WriteLine();
            Console.WriteLine($"\"{name}\" заказано!");
        }

        // вывод меню взаимодействия с заказами
        public void ShowOrdersMenu()
        {
            int? menuChoice;
            int amountOfChoices = default;
            bool needToRedrawMenu = true;

            while (true)
            {
                if (needToRedrawMenu)
                {
                    Console.Clear();
                    amountOfChoices = ShowOptionsFromOrdersMenu(); // выводим заказы на выбор
                    if (amountOfChoices <= 1) return;
                    needToRedrawMenu = false;
                }

                Console.Write("\nВаш выбор (цифра 1-{0}): ", amountOfChoices);
                string? choiceStr = Console.ReadLine(); // получаем строку с выбором
                menuChoice = UserInteractions.CheckChoiceMenu(choiceStr, amountOfChoices);
                if (menuChoice == null) continue;

                // Обработка выбора
                if (menuChoice <= orders.Count)
                {
                    ShowOrderMenuByIndex((int)menuChoice - 1); // если выбор не вышел за пределы списка меню
                    needToRedrawMenu = true;
                }
                else if (menuChoice == amountOfChoices - 1)
                {
                    Pizza.RemindPineapples();
                    needToRedrawMenu = true;
                }
                else if (menuChoice == amountOfChoices) return;
                else
                {
                    Console.Write("Некорректный выбор.");
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
                UserInteractions.WaitForUser();
                return i;
            }

            Console.WriteLine("Выберите заказ для взаимодействия:");
            for (; i < orders.Count; i++)
            {
                Console.Write(i + 1 + ".");
                orders[i].PrintFoodShort(); // выводим элементы меню
            }

            Console.WriteLine();
            Console.WriteLine(++i + ".Напомнить про ананасы.");
            Console.WriteLine(++i + ".Выйти в основное меню.");

            return i;
        }


        // Вывод информации и действия с заказом
        private void ShowOrderMenuByIndex(int index)
        {
            int? menuChoice;
            int amountOfChoices = 3;
            bool needToRedrawMenu = true;

            while (true)
            {
                if (needToRedrawMenu)
                {
                    Console.Clear();
                    Console.WriteLine("Заказ:");
                    orders[index].PrintFoodShort(); // краткая инфа про заказ
                    Console.WriteLine("\nМеню заказа:"); // меню взаимодействий с заказом
                    Console.WriteLine("1.Вывести подробную информацию");
                    Console.WriteLine("2.Взаимодействовать с заказом");
                    Console.WriteLine("3.Выйти в главное меню");
                    needToRedrawMenu= false;
                }
                
                Console.Write("\nВаш выбор (1-{0}): ", amountOfChoices);

                string? choiceStr = Console.ReadLine(); // получаем строку с выбором
                menuChoice = UserInteractions.CheckChoiceMenu(choiceStr, amountOfChoices);

                if (menuChoice == null) continue;

                if (menuChoice == 1)
                {
                    ShowFullOrderInfo(index);
                    needToRedrawMenu = true;
                }
                else if (menuChoice == 2)
                {
                    bool isEaten = false;
                    ShowInteractionMenuByIndex(index, ref isEaten);
                    if (isEaten) return;
                    needToRedrawMenu = true;
                }
                else if (menuChoice == 3) return;
                else
                {
                    Console.Write("Некорректный выбор.");
                    UserInteractions.WaitForUser();
                }
            }
        }

        // вывести информацию о заказе
        private void ShowFullOrderInfo(int index)
        {
            Console.Clear();
            Console.WriteLine("Полная информация о заказе:");
            orders[index].PrintFoodFull();
            UserInteractions.WaitForUser();
        }

        // вывести меню взаимодействия с заказом
        private void ShowInteractionMenuByIndex(int index, ref bool isEaten)
        {
            int? menuChoice;
            int amountOfChoices = 4;
            bool needToRedraw = true;

            FoodItem order = orders[index];
            string? choiceStr;

            while (true)
            {
                if (needToRedraw)
                {
                    Console.Clear();
                    Console.WriteLine("Заказ:");
                    order.PrintFoodShort();
                    Console.WriteLine("\nВарианты взаимодействия:");
                    Console.WriteLine("1.Запечь");
                    Console.WriteLine("2.Нарезать");
                    Console.WriteLine("3.Съесть");
                    Console.WriteLine("4.Выйти из меню");
                    needToRedraw = false;
                }
                
                Console.Write("\nВаш выбор (1-{0}): ", amountOfChoices);
                choiceStr = Console.ReadLine(); // получаем строку с выбором
                menuChoice = UserInteractions.CheckChoiceMenu(choiceStr, amountOfChoices);
                if (menuChoice == null) continue;

                if (menuChoice == 1) order.Bake();   
                else if (menuChoice == 2)
                {
                    if (order.IsBaked == false)
                    {
                        Console.WriteLine("Сначала нужно запечь блюдо.");
                        continue;
                    }

                    if (order.IsCut == true)
                    {
                        Console.WriteLine("Блюдо уже нарезано.");
                        continue;
                    }

                    Console.Write("Введите кол-во кусочков для нарезки: ");
                    byte slices;
                    UserInteractions.ChooseAmount(out slices);

                    order.Cut(slices);

                }
                else if (menuChoice == 3)
                {
                    if (order.IsBaked != true)
                    {
                        Console.WriteLine("Сначала нужно запечь блюдо.");
                        continue;
                    }

                    byte pieces;
                    Console.Write($"Введите кол-во кусочков, которое хотите съесть (максимум {order.AmountOfSlices}): ");

                    UserInteractions.ChooseAmount(out pieces);

                    order.Eat(pieces); // пытаемся съесть

                    if (order.IsEaten) // если получилось съесть
                    {
                        if (order is Pizza) ((Pizza)order).Unsubscribe();
                        orders.RemoveAt(index); // удаляем заказ
                        isEaten = true; // возвращаем признак вызывающей функции
                        UserInteractions.WaitForUser();
                        return;
                    }
                }
                else if (menuChoice == 4) return;
                else
                {
                    Console.WriteLine("Некорректный выбор");
                }  
            }
        }
    }
}
