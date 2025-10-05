using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.UIClasses
{
    internal static class UserInteractions
    {
        public static void WaitForUser()
        {
            Console.WriteLine("Продолжить...");
            Console.ReadKey();
        }

        public static int? CheckChoiceMenu(string? choiceStr, int amountOfChoices)
        {
            int menuChoice;

            if (choiceStr == null) return null; // Если строка с выбором пуста, выбрать выход из меню

            try
            {
                menuChoice = int.Parse(choiceStr); // записываем выбор в целочисленную переменную
                if (menuChoice > 0 && menuChoice <= amountOfChoices) return menuChoice;
                else Console.WriteLine("Не соблюден интервал!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный формат ввода!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Необрабатываемое исключение!\n" + e.Message);
            }

            // WaitForUser();
            return null; // Если строка с выбором пуста, выбрать выход из меню
        }

        public static void ChooseAmount(out byte amount)
        {
            while (true)
            {
                try
                {
                    amount = Convert.ToByte(Console.ReadLine());
                    break;
                }
                catch (OverflowException)
                {
                    amount = byte.MaxValue;
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
        }

        public static void ChooseAmount(out short amount)
        {
            while (true)
            {
                try
                {
                    amount = Convert.ToInt16(Console.ReadLine());
                    break;
                }
                catch (OverflowException)
                {
                    amount = short.MaxValue;
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
        }
    }
}
