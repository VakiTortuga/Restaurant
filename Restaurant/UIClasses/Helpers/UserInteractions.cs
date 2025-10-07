using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.UIClasses.Helpers
{
    /// <summary>
    /// Класс вспомогательных функций для взаимодействия с пользователем
    /// </summary>
    internal static class UserInteractions
    {
        /// <summary>
        /// Функция ожидания ввода пользователя.
        /// </summary>
        /// <remarks>Когда какая-либо функция выводит  информацию,
        /// а после возврата из нее перерисовывается меню, пользователь ничего не успеет прочесть.</remarks>
        public static void WaitForUser()
        {
            Console.WriteLine("Продолжить...");
            Console.ReadKey();
        }

        /// <summary>
        /// Функция, выводящая пользователю индикатор выполнения
        /// </summary>
        /// <param name="ticks">Количество делений прогресса выполнения.</param>
        /// <param name="millisecsPerTick">Милисекунды на одно деление.</param>
        /// <remarks>Индикатор ради индикатора, но дает пользоателю отдохнуть.
        /// Да и кухня, в которой все выполняется не моментально, выглядит поживее</remarks>
        public static void ProgressBar(byte ticks, byte millisecsPerTick)
        {
            const byte MAX_TICKS = 30; // Максимальное число делений
            const byte MIN_MILLISECS_PER_TICK = 10; // Минимальное время отображения одного деления
            const byte MAX_MILLISECS_PER_TICK = 200; // Максимальное

            // проверки значений параметров
            if (ticks > MAX_TICKS) ticks = MAX_TICKS;
            if (millisecsPerTick < MIN_MILLISECS_PER_TICK) millisecsPerTick = MIN_MILLISECS_PER_TICK;
            else if (millisecsPerTick > MAX_MILLISECS_PER_TICK) millisecsPerTick = MAX_MILLISECS_PER_TICK;

            // Выводим границы индикатора
            var (left, top) = Console.GetCursorPosition();
            Console.Write('[');
            Console.SetCursorPosition(left + ticks + 1, top);
            Console.Write(']');
            Console.SetCursorPosition(left + 1, top);

            // Выводим индикатор
            for (int i = 0; i < ticks; i++)
            {
                Thread.Sleep(millisecsPerTick); // Интервал между точками
                Console.Write('.');
            }

            Console.WriteLine();
        }


        /// <summary>
        /// Функция для получения выбора пользователя.
        /// </summary>
        /// <param name="amountOfChoices">Число доступных опций меню.</param>
        /// <returns>Номер выбранной опции, или null при некорректном вводе.</returns>
        /// <remarks>Функционал по сути тот же, что и у CheckMenuChoice,
        /// но функция сама запрашивает у пользователя ввод.</remarks>
        public static int? GetMenuChoice(int amountOfChoices)
        {
            Console.Write("\nВаш выбор (цифра 1-{0}): ", amountOfChoices);
            string? choiceStr = Console.ReadLine(); // получаем строку с выбором

            return CheckMenuChoice(choiceStr, amountOfChoices);
        }

        /// <summary>
        /// Функция для проверки выбора в меню.
        /// </summary>
        /// <param name="choiceStr">Ввод пользователя.</param>
        /// <param name="amountOfChoices">Число доступных опций меню.</param>
        /// <returns>Номер выбранной опции, или null при некорректном вводе.</returns>
        private static int? CheckMenuChoice(string? choiceStr, int amountOfChoices)
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

            return null; // Если строка с выбором пуста, выбрать выход из меню
        }

        /// <summary>
        /// Функция для получения от пользователя целочисленного ввода типа байт
        /// </summary>
        /// <param name="amount">Кол-во, выбранное пользователем</param>
        /// <remarks>Функция не проверяет границы разрешенного диапазова,
        /// ее задача - просто принять число, а не какой-либо другой символ.</remarks>
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

        /// <summary>
        /// Функция для получения от пользователя целочисленного ввода типа шорт
        /// </summary>
        /// <param name="amount">Кол-во, выбранное пользователем</param>
        /// <remarks>Функция не проверяет границы разрешенного диапазова,
        /// ее задача - просто принять число, а не какой-либо другой символ.</remarks>
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
