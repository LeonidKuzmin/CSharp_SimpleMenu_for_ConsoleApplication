/*
    Простое меню для терминальной программы на C#
    Использование. Демо №1 - только главное меню.

    Реализация меню - в файле Menu.cs
    
    Леонид Кузьмин
*/

using System;

namespace SimpleMenu_for_ConsoleApplication
{
    class Program
    {
        static void Main()
        {
            // Заголовок меню. Не обязателен
            string[] menuHeader = { "Главное меню. Заголовок. Строка 1",
                                    "Главное меню. Заголовок. Строка 2" };

            // Массив пунктов меню
            MenuItem[] menuItems = { new MenuItem("Задание 1", Task1),
                                     new MenuItem("Задание 2", Task2),
                                     new MenuItem("Задание 3", Task3.Run),
                                     new MenuItem("Задание 4", Task4.Run),
                                     new MenuItem("Задание 5", Task5.Run),
                                     new MenuItem("Выход    ") };

            // "Подвал" меню. Не обязателен
            string[] menuFooter = { "Выбор пункта меню - клавиши со стрелками, <PageUp>, <PageDown>.",
                                    "Активация пункта меню - <Enter> или <Space>." };

            Menu menu = new Menu(menuItems, menuHeader, menuFooter);  // Создание объекта "Меню"
            menu.Show();  // Отображение меню

            // Т.к. здесь пусто - при выходе из меню будет выход из программы
        }

        // Вызываемые из меню методы могут быть в том же классе, что и метод Main()
        static void Task1() { Console.WriteLine("Task1()"); Console.ReadKey(); }
        static void Task2() { Console.WriteLine("Task2()"); Console.ReadKey(); }
    }

    // Вызываемые из меню методы могут быть в других классах
    class Task3
    {
        static public void Run() { Console.WriteLine("Task3.Run()"); Console.ReadKey(); }
    }

    class Task4
    {
        static public void Run() { Console.WriteLine("Task4.Run()"); Console.ReadKey(); }
    }
}
