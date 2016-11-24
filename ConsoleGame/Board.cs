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
        /// Центральная точка вывода доски
        /// </summary>
        private Vector2D center;

        /// <summary>
        /// Renderer
        /// </summary>
        private Renderer renderer;

        /// <summary>
        /// Инициализация доски
        /// </summary>
        /// <param name="renderer"></param>
        public Board(Renderer renderer)
        {
            center = new Vector2D(renderer.Width / 2, renderer.world.GetLength(0) - 1);

            this.renderer = renderer;
            renderer.Bindings.Add(ConsoleKey.UpArrow, Up);
            renderer.Bindings.Add(ConsoleKey.DownArrow, Down);
            renderer.Bindings.Add(ConsoleKey.LeftArrow, Left);
            renderer.Bindings.Add(ConsoleKey.RightArrow, Right);

            render();
        }


        private void fill()
        {
            Console.WriteLine(center);

            int right = center.X + size;
            int left = center.X - size;

            for(int i = left; i < right && right < renderer.Width - 1 && left > 0; i ++)
                renderer.world[center.Y, i] = new Dot(symbol);
        }

        /// <summary>
        /// Отрисовка доски
        /// </summary>
        private void render()
        {
            symbol = '=';
            fill();
        }


        /// <summary>
        /// Очистка отрисованной доски
        /// </summary>
        private void clean()
        {
            symbol = ' ';
            fill();
        }

        /// <summary>
        /// Обработчик нажатия на клавишу вверх
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private bool Up(Renderer renderer)
        {
            clean();
            center += new Vector2D(0, -1);
            render();
            return true;
        }


        /// <summary>
        /// Обработчик нажатия на клавишу вниз
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private bool Down(Renderer renderer)
        {
            clean();
            if(center.Y < renderer.Height - 1)
                center += new Vector2D(0, 1);
            render();
            return true;

        }


        /// <summary>
        /// Обработчик нажатия на клавишу влево
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private bool Left(Renderer renderer)
        {
            clean();
            center += new Vector2D(-1, 0);
            render();
            return true;
        }


        /// <summary>
        /// Обработчик нажатия клавиши вправо
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private bool Right(Renderer renderer)
        {
            clean();
            center += new Vector2D(1, 0);
            render();
            return true;
        }
    }
}