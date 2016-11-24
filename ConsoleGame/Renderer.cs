using System;

namespace ConsoleGame
{
    /// <summary>
    /// Консольный ренделрер
    /// </summary>
    public class Renderer
    {
        /// <summary>
        /// Текущая позиция курсора
        /// </summary>
        readonly Vector2D CursorCurrentPosition = new Vector2D();

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
        /// Массив точек для вывода
        /// </summary>
        public Dot[,] world;

        /// <summary>
        /// Объект для привязки обработчиков нажатий клавиш
        /// </summary>
        public KeyBindings Bindings = new KeyBindings();

        /// <summary>
        /// Флаг отладки
        /// </summary>
        public bool debug = false;

        public Renderer()
        {
//            debug = true;

            Width = Console.WindowWidth;
            Height = Console.WindowHeight;

            if (debug)
                Height -= 2;

            SetBackgroundColor(ConsoleColor.DarkBlue);

            world = new Dot[Height, Width];

            for(int i = 0; i < world.GetLength(0); i++)
                for (int j = 0; j < world.GetLength(1); j++)
                    world[i, j] = new Dot();

            KeyHandlers.Attach(Bindings);

            // @TODO: подумать насчет хранения объектов. Dependency Injection?
            new Board(this);

            Draw();
            Update();
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
            Draw();
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
            ConsoleKey key = Console.ReadKey(true).Key;
            Bindings.Exec(key, this);
            Draw();
        }

        public void Stop() => is_animation_start = false;

        /// <summary>
        /// Перерисовка
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);

            for(int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    Console.Write(world[i, j]);
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
        public void UpdateCursorPosition() => Console.SetCursorPosition(CursorCurrentPosition.X, Console.WindowHeight - CursorCurrentPosition.Y);
    }
}