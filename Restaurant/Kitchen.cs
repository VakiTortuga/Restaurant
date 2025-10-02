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

        public void InitMenu()
        {
            menu.Add(new Pizza("Средняя Пепперони", PizzaSize.Medium));
            menu.Add(new Pizza("Маленькая Ананасовая", PizzaSize.Small));
            menu.Add(new Pizza("Большая Песто", PizzaSize.Large));
            menu.Add(new Calzone("Кольцоне с грибами", 250));
            menu.Add(new Calzone("Кольцоне с яблоком", 200));
        }

        // управляет интерфейсом и всеми взаимодействиями
        public void KitchenManager()
        {

        }

        // выводит меню кухни (добавление и просмотр заказов)
        private void ShowMainMenu() 
        {

        }

        // вывод меню блюд, с возможностью сделать заказ, добавить блюдо и вернуться в главное меню
        private void ShowFoodMenu()
        {
            // переменные для вывода блюд меню
            string name;
            FoodWeight weight;
            int menuChoice, i = 1;

            Console.WriteLine("Меню блюд:");
            for (; i <= menu.Count; i++) // выводим элементы меню
            {
                this.menu[i - 1].Deconstruct(out name, out weight); // получаем значения полей имени и веса
                Console.Write(i + "." + name + ". Вес - " + weight.ToString()); // выводим информацию о блюде
            }
            Console.Write(i + ".Добавить блюдо в меню"); // выводим вариант для создания и заказа новой опции меню
            Console.Write((++i) + ".Вернуться в главное меню"); // выход в главное меню кухни

            Console.Write("Ваш выбор: ");
            string? choiceStr = Console.ReadLine(); // получаем строку с выбором
            if (choiceStr != null) menuChoice = int.Parse(choiceStr); // записываем выбор в целочисленную переменную
            else menuChoice = i; // Если строка с выбором пуста, выбрать выход из меню

            if (menuChoice <= menu.Count) MakeOrderByIndex(menuChoice); // если выбор не вышел за пределы списка меню
            if (menuChoice == (i - 1)) CreateOptionForOrder(); // создание опции
            else ShowMainMenu();
        }

        // выход из кухни
        private void ExitKitchenManager() { }

        // добавляет блюдо из списка меню по индексу в список заказов
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

        // добавляет блюдо в очередь заказов
        private void CreateOptionForOrder() { }

        // меню взаимодействия с заказами
        private void ShowOrders() { }

        // вывести информацию о заказе
        private void ShowOrderInfo(int number) { }

        // вывести меню взаимодействия с заказами
        private void ShowOrderInteractions(int number) { }


    }
}
