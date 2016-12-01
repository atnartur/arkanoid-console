using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame
{
    /// <summary>
    /// Консольный ренделрер
    /// </summary>
    public sealed class Renderer
    {
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
        /// Текущая позиция курсора
        /// </summary>
//        readonly Vector2D CursorCurrentPosition = new Vector2D();

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

        public List<IObject> Scene = new List<IObject>();

        private ConsoleColor _background_color;

        /// <summary>
        /// Флаг отладки
        /// </summary>
        public bool debug = false;

        private Renderer()
        {
//            Console.WriteLine(DateTime.Now);

//            debug = true;

            Width = Console.WindowWidth;
            Height = Console.WindowHeight;

            if (debug)
                Height -= 2;

            SetBackgroundColor(ConsoleColor.DarkBlue);
//            Console.ForegroundColor = ConsoleColor.Green;

            KeyHandlers.Attach(Bindings);
            Console.CursorVisible = false;
            // @TODO: подумать насчет хранения объектов. Dependency Injection?

//            Draw();
//            Update();
        }


        /// <summary>
        /// Установка фонового цвета консоли
        /// </summary>
        /// <param name="color">цвет из System.ConsoleColor</param>
        public void SetBackgroundColor(ConsoleColor color)
        {
            _background_color = color;
            ResetBackgroundColor();
        }
        public void ResetBackgroundColor() => Console.BackgroundColor = _background_color;
        /// <summary>
        /// Запуск перерисовки
        /// </summary>
        public void Start()
        {
            is_animation_start = true;
            List<Action> a = new List<Action>();
            a.Add(UpdateRender);
            a.Add(UpdateKeys);
            UpdateRender();
//            Parallel.Invoke(UpdateRender, UpdateKeys);
        }

        /// <summary>
        /// Перерисовка
        /// </summary>
        public void UpdateRender()
        {
            while (true)
            {
                if (!is_animation_start)
                {
                    Clear();
                    break;
                }

                for (int i = 0; i < Scene.Count; i++)
                {
                    ((IObject) Scene[i]).Render();
                }
                UpdateKeys();
            }


        }


        public void UpdateKeys()
        {
            while (Console.KeyAvailable)
            {
                if (!is_animation_start)
                    break;

                ConsoleKey key = Console.ReadKey(true).Key;
                Bindings.Exec(key);
            }
        }

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

        public void PrintLineWithMargin(String line, int margin_left, ConsoleColor bg_color = 0)
        {
            if (margin_left < 0)
                margin_left = 0;

            if (bg_color != _background_color)
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
        public void PrintLineOnCenter(String line, ConsoleColor bg_color = 0)
            => PrintLineWithMargin(line, (this.Width - line.Length) / 2, bg_color);
    }
}