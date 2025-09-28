using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Kitchen : IKitchen
    {
        List<Food> menu = new List<Food>();
        Queue<Food> orders = new Queue<Food>();

        public void InitMenu()
        {
            menu.Add(new Pizza("Средняя Пепперони", PizzaSize.Medium));
            menu.Add(new Pizza("Маленькая Ананасовая", PizzaSize.Small));
            menu.Add(new Pizza("Большая Песто", PizzaSize.Large));
            menu.Add(new Calzone("Кольцоне с грибами", 250));
            menu.Add(new Calzone("Кольцоне с яблоком", 200));
        }

        public void ShowMenu()
        {
            
        }

        public void ExitMenu() { }

        public void MakeOrder() { }

        public void ShowOrders() { }

        public void OrderInfo(int number) { }

        public void OrderInteractions(int number) { }
    }
}
