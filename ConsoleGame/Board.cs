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
        /// Максимальная высота подъема доски
        /// </summary>
        private int max_height = 5;

        /// <summary>
        /// Инициализация доски
        /// </summary>
        /// <param name="renderer"></param>
        public Board()
        {
            Renderer renderer = Renderer.Instance;
            center = new Vector2D(renderer.Width / 2, renderer.world.GetLength(0) - 1);

            renderer.Bindings.Add(ConsoleKey.UpArrow, Up);
            renderer.Bindings.Add(ConsoleKey.DownArrow, Down);
            renderer.Bindings.Add(ConsoleKey.LeftArrow, Left);
            renderer.Bindings.Add(ConsoleKey.RightArrow, Right);

            Render();
        }


        private void Fill()
        {
            Renderer renderer = Renderer.Instance;

            if(renderer.debug)
                Console.WriteLine(center);

            int right = center.X + size;
            int left = center.X - size;

            for(int i = left; i < right && right < renderer.Width - 1 && left > 0; i ++)
                renderer.world[center.Y, i] = new Dot(symbol);
        }

        /// <summary>
        /// Отрисовка доски
        /// </summary>
        private void Render()
        {
            symbol = '=';
            Fill();
        }


        /// <summary>
        /// Очистка отрисованной доски
        /// </summary>
        private void Clean()
        {
            symbol = ' ';
            Fill();
        }

        /// <summary>
        /// Обработчик нажатия на клавишу вверх
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private void Up()
        {
            Renderer renderer = Renderer.Instance;

            Clean();

            if(center.Y > renderer.Height - max_height)
                center += new Vector2D(0, -1);

            Render();
        }


        /// <summary>
        /// Обработчик нажатия на клавишу вниз
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private void Down()
        {
            Renderer renderer = Renderer.Instance;

            Clean();
            if(center.Y < renderer.Height - 1)
                center += new Vector2D(0, 1);
            Render();

        }


        /// <summary>
        /// Обработчик нажатия на клавишу влево
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private void Left()
        {

            Clean();
            if(center.X > size + 1)
                center += new Vector2D(-1, 0);

            Render();
        }


        /// <summary>
        /// Обработчик нажатия клавиши вправо
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private void Right()
        {
            Renderer renderer = Renderer.Instance;

            Clean();
            if(center.X < renderer.Width - 2 - size)
                center += new Vector2D(1, 0);
            Render();
        }
    }
}