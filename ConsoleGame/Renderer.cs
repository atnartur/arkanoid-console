using System;

namespace ConsoleGame
{
    public class Renderer
    {
        readonly Vector2D CursorCurrentPosition = new Vector2D();
        private bool is_start = false;

        public Renderer()
        {
            CursorCurrentPosition.Set(Console.WindowWidth / 2, Console.WindowHeight / 2);
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Draw();
            Update();
        }

        /// <summary>
        /// Запуск перерисовки
        /// </summary>
        public void Start()
        {
            Draw();
            is_start = true;

            while (true)
            {
                if (!is_start)
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
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    if(CursorCurrentPosition.Y < Console.WindowHeight - 1)
                        CursorCurrentPosition.Y++;
                    break;
                case ConsoleKey.DownArrow:
                    if(CursorCurrentPosition.Y > 0)
                        CursorCurrentPosition.Y--;
                    break;
                case ConsoleKey.RightArrow:
                    if(CursorCurrentPosition.X < Console.WindowWidth - 1)
                        CursorCurrentPosition.X++;
                    break;
                case ConsoleKey.LeftArrow:
                    if(CursorCurrentPosition.X > 0)
                        CursorCurrentPosition.X--;
                    break;
                case ConsoleKey.Q:
                    is_start = false;
                    break;
                default:
                    break;
            }
            Draw();
        }

        /// <summary>
        /// Перерисовка
        /// </summary>
        public void Draw()
        {
            Console.Clear();
//            UpdateCursorPosition();
            DrawCursorAsSymbol();
        }

        /// <summary>
        /// Перерисовка символа
        /// </summary>
        public void DrawCursorAsSymbol()
        {
            for(int y = Console.WindowHeight - 1; y > 0; y--)
            {
                for(int x = 0; x < Console.WindowWidth; x++)
                {
                    if(CursorCurrentPosition.Equals(x, y))
                        Console.Write("=");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
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