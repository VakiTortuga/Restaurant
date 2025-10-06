using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.UIClasses.Interface
{
    /// <summary>
    /// Интерфейс кухни
    /// </summary>
    internal interface IKitchen
    {
        /// <summary>
        /// Вывод меню кухни (добавление и просмотр заказов)
        /// </summary>
        void ShowKitchenMenu();
    }
}
