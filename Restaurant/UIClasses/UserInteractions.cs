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

        public static void ProgressBar(byte ticks, byte millisecsPerTick)
        {
            const byte MAX_TICKS = 30;
            const byte MIN_MILLISECS_PER_TICK = 10;
            const byte MAX_MILLISECS_PER_TICK = 200;

            if (ticks > MAX_TICKS) ticks = MAX_TICKS;

            if (millisecsPerTick < MIN_MILLISECS_PER_TICK) millisecsPerTick = MIN_MILLISECS_PER_TICK;
            else if (millisecsPerTick > MAX_MILLISECS_PER_TICK) millisecsPerTick = MAX_MILLISECS_PER_TICK;

            var (left, top) = Console.GetCursorPosition();
            Console.Write('[');
            Console.SetCursorPosition(left + ticks + 1, top);
            Console.Write(']');
            Console.SetCursorPosition(left + 1, top);

            for (int i = 0; i < ticks; i++)
            {
                Thread.Sleep(millisecsPerTick);
                Console.Write('.');
            }

            Console.WriteLine();
        }

        

        public static int? GetMenuChoice(int amountOfChoices)
        {
            Console.Write("\nВаш выбор (цифра 1-{0}): ", amountOfChoices);
            string? choiceStr = Console.ReadLine(); // получаем строку с выбором

            return UserInteractions.CheckChoiceMenu(choiceStr, amountOfChoices);
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
