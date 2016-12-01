using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame
{
    /// <summary>
    /// Консольный рендерер
    /// </summary>
    public sealed class Renderer
    {
        /// <summary>
        /// Singleton патерн
        /// </summary>
        private static Renderer _instance;

        public static Renderer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Renderer();
                return _instance;
            }
        }

        /// <summary>
        /// Ширина экрана
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Высота экрана
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Флаг продолжения анимации. true - анимация продолжается, false - останавливается
        /// </summary>
        private bool is_animation_start = false;

        /// <summary>
        /// Объект для привязки обработчиков нажатий клавиш
        /// </summary>
        public KeyBindings Bindings = new KeyBindings();

        /// <summary>
        /// Объекты на сцвене
        /// </summary>
        public List<IObject> Scene = new List<IObject>();

        /// <summary>
        /// Фоновый цвет
        /// </summary>
        private ConsoleColor _backgroundColor;

        /// <summary>
        /// Флаг отладки
        /// </summary>
        public bool debug = false;

        private Renderer()
        {
//            debug = true;

            Width = Console.WindowWidth;
            Height = Console.WindowHeight;

            if (debug)
                Height -= 2;

            SetBackgroundColor(ConsoleColor.DarkBlue);

            KeyHandlers.Attach(Bindings);
            Console.CursorVisible = false;
            // @TODO: подумать насчет хранения объектов. Dependency Injection?
        }


        /// <summary>
        /// Установка фонового цвета консоли
        /// </summary>
        /// <param name="color">цвет из System.ConsoleColor</param>
        public void SetBackgroundColor(ConsoleColor color)
        {
            _backgroundColor = color;
            ResetBackgroundColor();
        }

        /// <summary>
        /// Установка фонового цвета из переменной объекта
        /// </summary>
        public void ResetBackgroundColor() => Console.BackgroundColor = _backgroundColor;

        /// <summary>
        /// Запуск перерисовки
        /// </summary>
        public void Start()
        {
            is_animation_start = true;

            while (true)
            {
                if (!is_animation_start) // выход из перерисовки
                {
                    Clear();
                    break;
                }

                for (int i = 0; i < Scene.Count; i++)
                {
                    ((IObject) Scene[i]).Render();
                }


                // если в данный момент была нажата клавиша - обрабатываем нажатия
                while (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    Bindings.Exec(key);
                }
            }
        }

        /// <summary>
        /// Остановка перерисовки
        /// </summary>
        public void Stop() => is_animation_start = false;

        /// <summary>
        /// Перерисовка
        /// </summary>
        public void DrawCanvas()
        {
            for(int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    Console.Write(' ');
        }

        /// <summary>
        /// Очистка консоли во время остановки
        /// </summary>
        public void Clear()
        {
            Console.ResetColor();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Заполняет прямоугольную область символом
        /// </summary>
        /// <param name="symbol">символ</param>
        /// <param name="a">левый верхний угол</param>
        /// <param name="b">правый нижний угол</param>
        public void FillRect(char symbol, Vector2D a, Vector2D b = null)
        {
            if (b == null || a.Equals(b))
            {
                Console.SetCursorPosition(a.X, this.Height - 1 - a.Y);
                Console.Write(symbol);
            }
            else
            {
                for (int y = a.Y; y <= b.Y; y++)
                {
                    for (int x = a.X; x <= b.X; x++)
                    {
                        Console.SetCursorPosition(x, this.Height - 1 - y);
                        Console.Write(symbol);
                    }
                }
            }
        }

        /// <summary>
        /// Вывести текст с отступом
        /// </summary>
        /// <param name="line">Текст</param>
        /// <param name="margin_left">Отступ от краев консоли</param>
        /// <param name="bg_color">фоновый цвет</param>
        public void PrintLineWithMargin(String line, int margin_left, ConsoleColor bg_color = 0)
        {
            if (margin_left < 0)
                margin_left = 0;

            if (bg_color != _backgroundColor)
                Console.BackgroundColor = bg_color;

            if (line.Length == 0)
            {
                for (int i = 0; i < Width; i++)
                    Console.Write(' ');
                Console.WriteLine();
            }
            else if (line.Length > Width - margin_left * 2)
            {
                List<String> words = new List<string>(line.Split(' '));

                StringBuilder str = new StringBuilder();
                int str_length = 0;

                while (words.Count > 0)
                {
                    while (str_length < Width - margin_left * 2 && words.Count > 0)
                    {
                        String word = words[0];

                        if (word.Length > Width - margin_left * 2 - str_length)
                            break;

                        words.RemoveAt(0);
                        str_length += word.Length + 1;
                        str.Append(word + ' ');
                    }

                    for(int i = 0; i < margin_left; i++)
                        Console.Write(' ');

                    Console.Write(str);

                    for(int i = 0; i < Width - margin_left - str_length; i++)
                        Console.Write(' ');

                    Console.WriteLine();
                    str.Clear();
                    str_length = 0;
                }
            }
            else
            {
                for(int i = 0; i < margin_left; i++)
                    Console.Write(' ');

                Console.Write(line);

                for(int i = 0; i < Width - margin_left - line.Length; i++)
                    Console.Write(' ');

                Console.WriteLine();
            }

            ResetBackgroundColor();

        }

        /// <summary>
        /// Вывести текст по центру
        /// </summary>
        /// <param name="line"></param>
        /// <param name="bg_color"></param>
        public void PrintLineOnCenter(String line, ConsoleColor bg_color = 0)
            => PrintLineWithMargin(line, (this.Width - line.Length) / 2, bg_color);
    }
}