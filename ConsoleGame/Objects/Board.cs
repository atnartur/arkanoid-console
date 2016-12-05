using System;

namespace ConsoleGame.Objects
{
    /// <summary>
    /// Доска
    /// </summary>
    public class Board : IObject
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
        public Vector2D Center { get; private set; }

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
            Center = new Vector2D(renderer.Width / 2, 3);
            renderer.Bindings.Add(ConsoleKey.UpArrow, Up);
            renderer.Bindings.Add(ConsoleKey.DownArrow, Down);
            renderer.Bindings.Add(ConsoleKey.LeftArrow, Left);
            renderer.Bindings.Add(ConsoleKey.RightArrow, Right);

            Render();
        }

        /// <summary>
        /// Отображение
        /// </summary>
        public void Render()
        {
            Renderer renderer = Renderer.Instance;

            if(renderer.debug)
                Console.WriteLine(Center);

            Vector2D a = Center + new Vector2D(-size, 0);
            Vector2D b = Center + new Vector2D(size, 0);

            renderer.FillRect(symbol, a, b);
        }

        /// <summary>
        /// Обработчик нажатия на клавишу вверх
        /// </summary>
        private void Up()
        {
            symbol = ' ';

            Render();
            Renderer renderer = Renderer.Instance;

            if(Center.Y < max_height)
                Center += new Vector2D(0, 1);
            symbol = '=';

            Render();
        }


        /// <summary>
        /// Обработчик нажатия на клавишу вниз
        /// </summary>
        private void Down()
        {
            symbol = ' ';
            Render();
            Renderer renderer = Renderer.Instance;

            if(Center.Y > 0)
                Center += new Vector2D(0, -1);
            symbol = '=';
            Render();
        }


        /// <summary>
        /// Обработчик нажатия на клавишу влево
        /// </summary>
        private void Left()
        {
            symbol = ' ';
            Render();

            if(Center.X > size + 1)
                Center += new Vector2D(-3, 0);
            symbol = '=';

            Render();
        }


        /// <summary>
        /// Обработчик нажатия клавиши вправо
        /// </summary>
        private void Right()
        {
            symbol = ' ';

            Render();

            Renderer renderer = Renderer.Instance;

            if(Center.X < renderer.Width - 2 - size)
                Center += new Vector2D(3, 0);
            symbol = '=';

            Render();
        }
    }
}