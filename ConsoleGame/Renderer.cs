using System;

namespace ConsoleGame
{
    public class Renderer
    {
        readonly Vector2D CursorCurrentPosition = new Vector2D();
        private int width;
        private int height;
        private bool is_animation_start = false;
        private Dot[,] world;
        public KeyBindings Bindings = new KeyBindings();

        public Renderer()
        {
            width = Console.WindowWidth;
            height = Console.WindowHeight-2;

//            CursorCurrentPosition.Set(width / 2, height / 2);
            SetBackgroundColor(ConsoleColor.DarkBlue);

            world = new Dot[height, width];

            for(int i = 0; i < world.GetLength(0); i++)
                for (int j = 0; j < world.GetLength(1); j++)
                    world[i, j] = new Dot();

            KeyHandlers.Attach(Bindings);
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

//            switch (Console.ReadKey(true).Key)
//            {
////                case ConsoleKey.UpArrow:
////                    if(CursorCurrentPosition.Y < Console.WindowHeight - 1)
////                        CursorCurrentPosition.Y++;
////                    break;
////                case ConsoleKey.DownArrow:
////                    if(CursorCurrentPosition.Y > 0)
////                        CursorCurrentPosition.Y--;
////                    break;
////                case ConsoleKey.RightArrow:
////                    if(CursorCurrentPosition.X < Console.WindowWidth - 1)
////                        CursorCurrentPosition.X++;
////                    break;
////                case ConsoleKey.LeftArrow:
////                    if(CursorCurrentPosition.X > 0)
////                        CursorCurrentPosition.X--;
////                    break;
//                case ConsoleKey.Q:
//                    is_animation_start = false;
//                    break;
//                default:
//                    break;
//            }
            Draw();
        }

        public void Stop() => is_animation_start = false;

        /// <summary>
        /// Перерисовка
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
//            Console.Clear();
//            UpdateCursorPosition();
            DrawCursorAsSymbol();
        }

        /// <summary>
        /// Перерисовка символа
        /// </summary>
        public void DrawCursorAsSymbol()
        {

            for(int i = 0; i < world.GetLength(0); i++)
            {
                for (int j = 0; j < world.GetLength(1); j++){
                    Console.Write(world[i, j]);
                }
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