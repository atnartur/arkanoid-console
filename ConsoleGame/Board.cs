using System;

namespace ConsoleGame
{
    /// <summary>
    /// Доска
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Символ доски
        /// </summary>
        private char symbol = '=';

        /// <summary>
        /// Размер доски (вернее, максимальное смещение от центрального символа
        /// </summary>
        private int size = 3;

        /// <summary>
        /// Инициализация доски
        /// </summary>
        /// <param name="renderer"></param>
        public Board(Renderer renderer)
        {
            int center = renderer.Width / 2;

            int last_line = renderer.world.GetLength(0) - 1;

            for(int i = center - size; i < center + size; i ++)
                renderer.world[last_line, i] = new Dot(symbol);

            renderer.Bindings.Add(ConsoleKey.UpArrow, Up);
            renderer.Bindings.Add(ConsoleKey.DownArrow, Down);
            renderer.Bindings.Add(ConsoleKey.LeftArrow, Left);
            renderer.Bindings.Add(ConsoleKey.RightArrow, Right);
        }

        /// <summary>
        /// Обработчик нажатия на клавишу вверх
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private static bool Up(Renderer renderer)
        {
            return true;
        }


        /// <summary>
        /// Обработчик нажатия на клавишу вниз
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private static bool Down(Renderer renderer)
        {
            return true;

        }


        /// <summary>
        /// Обработчик нажатия на клавишу влево
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private static bool Left(Renderer renderer)
        {
            return true;
        }


        /// <summary>
        /// Обработчик нажатия клавиши вправо
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private static bool Right(Renderer renderer)
        {
            return true;
        }
    }
}