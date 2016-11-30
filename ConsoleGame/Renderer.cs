using System;
using System.Collections.Generic;

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
        public void SetBackgroundColor(ConsoleColor color) => Console.BackgroundColor = color;

        /// <summary>
        /// Запуск перерисовки
        /// </summary>
        public void Start()
        {
            is_animation_start = true;
            while (true)
            {
                if (!is_animation_start)
                {
                    Clear();
                    break;
                }

                Update();
            }
        }

        /// <summary>
        /// Перерисовка
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < Scene.Count; i++)
            {
                ((IObject) Scene[i]).Render();
            }
            ConsoleKey key = Console.ReadKey(true).Key;
            Bindings.Exec(key);
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
        }

        /// <summary>
        /// Обновление позиции курсора
        /// </summary>
//        public void UpdateCursorPosition() => Console.SetCursorPosition(CursorCurrentPosition.X, Console.WindowHeight - CursorCurrentPosition.Y);


        public void FillRect(char symbol, Vector2D a, Vector2D b = null)
        {
            if (b == null || a.Equals(b))
            {
                Console.SetCursorPosition(a.X, a.Y);
                Console.Write(symbol);
            }
            else
            {
                for (int y = a.Y; y <= b.Y; y++)
                {
                    for (int x = a.X; x <= b.X; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(symbol);
                    }
                }
            }

        }
    }
}