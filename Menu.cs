/*
    Простое меню для терминальной программы на C#
    Реализация

    Как использовать - см.метод Main() в файлах Demo*.cs
    
    Леонид Кузьмин
*/

using System;

namespace SimpleMenu_for_ConsoleApplication
{
    #region Меню

    /// <summary>
    /// Делегат - ссылка на метод, не возвращающий значения и без параметров.
    /// (подходит и метод Show() класса Menu => можно вызывать вложенные меню)
    /// </summary>
    delegate void PoinerToMethod();

    /// <summary>
    /// Структура "Пункт меню"
    /// </summary>
    struct MenuItem
    {
        string name;            // Название пункта меню
        PoinerToMethod method;  // Ссылка на метод, вызываемый при выборе этого пункта меню. Если null - выход из меню/программы

        public string Name { get { return name; } }
        public PoinerToMethod Method { get { return method; } }

        public MenuItem(string name, PoinerToMethod method = null)
        {
            this.name = name;
            this.method = method;
        }
    }

    /// <summary>
    /// Класс, реализующий меню
    /// </summary>
    class Menu
    {
        string[] header;      // Заголовок - выводится над пунктами меню
        MenuItem[] menuItems; // Пункты меню - массив структур "Пункт меню"
        string[] footer;      // "Подвал" - выводится под пунктами меню

        public Menu(MenuItem[] menuItems, string[] header = null, string[] footer = null)
        {
            this.header = header;
            this.menuItems = menuItems;
            this.footer = footer;
        }

        public void Show()
        {
            Console.CursorVisible = false;
            ConsoleColor bgColorStandard = Console.BackgroundColor;
            ConsoleColor bgColorOfCurrentItem = ConsoleColor.DarkCyan;

            int currentItem = 0;  // Номер текущего пункта меню

            while (true)  // Цикл отрисовки меню.  Выход - по выбору п.меню без соответствующего ему метода, т.е. с pMethod == null
            {
                Console.Clear();

                for (int i = 0; i < header.Length; i++)  // Вывод текста над меню
                {
                    Console.CursorTop = i;     // в верхних строках экрана
                    WriteCentered(header[i]);  // центрируя по горизонтали
                }

                for (int i = 0; i < footer.Length; i++)  // Вывод текста под меню
                {
                    Console.CursorTop = Console.WindowHeight - footer.Length + i;  // в нижних строках экрана
                    WriteCentered(footer[i]);  // центрируя по горизонтали
                }

                // Вывод пунктов меню
                // центрируя по вертикали с учётом места, занятого заголовком и "подвалом"
                Console.CursorTop = header.Length + (Console.WindowHeight - (header.Length + menuItems.Length + footer.Length)) / 2;

                for (int i = 0; i < menuItems.Length; i++)
                {
                    // Выделение цветом выбранного пункта меню (с номером currentItem)
                    Console.BackgroundColor = i == currentItem ? bgColorOfCurrentItem : bgColorStandard;

                    WriteLineCentered(menuItems[i].Name);  // Вывод текста, центрируя по горизонтали
                    Console.BackgroundColor = bgColorStandard;
                }

                // Обработка нажатия клавиш
                switch (Console.ReadKey(true).Key)  // true - для подавления вывода символа на экран
                {
                    case ConsoleKey.UpArrow:    // Перейти на один пункт меню выше, но не выше самого верхнего
                        if (currentItem > 0) currentItem--;
                        break;
                    case ConsoleKey.DownArrow:  // Перейти на один пункт меню ниже, но не ниже самого нижнего
                        if (currentItem < menuItems.Length - 1) currentItem++;
                        break;
                    case ConsoleKey.PageUp:     // Перейти к самому верхнему пункту меню
                    case ConsoleKey.Home:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.Escape:
                        currentItem = 0;
                        break;
                    case ConsoleKey.PageDown:   // Перейти к самому нижнему пункту меню
                    case ConsoleKey.End:
                    case ConsoleKey.RightArrow:
                        currentItem = menuItems.Length - 1;
                        break;
                    case ConsoleKey.Enter:      // Активировать текущий пункт меню
                    case ConsoleKey.Spacebar:
                        // Выход, если для текущего пункта меню не указан вызываемый метод
                        if (menuItems[currentItem].Method == null) return;

                        // Если для текущего пункта меню указан вызываемый метод
                        Console.Clear();
                        Console.CursorVisible = true;
                        menuItems[currentItem].Method();  // Вызов метода, соответствующего текущему пункту меню
                        Console.CursorVisible = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Вывод строки, центрируя текст по горизонтали  с учётом ширины окна консоли
        /// </summary>
        /// <param name="s">Выводимая строка</param>
        static public void WriteCentered(string s)
        {
            Console.CursorLeft = (Console.WindowWidth - s.Length) / 2;
            Console.Write(s);
        }

        /// <summary>
        /// Вывод строки, центрируя текст по горизонтали  с учётом ширины окна консоли;  а также вывод признака конца строки.
        /// </summary>
        /// <param name="s">Выводимая строка</param>
        static public void WriteLineCentered(string s)
        {
            Console.CursorLeft = (Console.WindowWidth - s.Length) / 2;
            Console.WriteLine(s);
        }
    }
    #endregion Меню
}
