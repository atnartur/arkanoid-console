﻿using System;

namespace ConsoleGame
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
            center = new Vector2D(renderer.Width / 2, renderer.Height - 1);
            renderer.Bindings.Add(ConsoleKey.UpArrow, Up);
            renderer.Bindings.Add(ConsoleKey.DownArrow, Down);
            renderer.Bindings.Add(ConsoleKey.LeftArrow, Left);
            renderer.Bindings.Add(ConsoleKey.RightArrow, Right);

            Render();
        }


        public void Render()
        {
            Renderer renderer = Renderer.Instance;

            if(renderer.debug)
                Console.WriteLine(center);

            Vector2D a = center + new Vector2D(-size, 0);
            Vector2D b = center + new Vector2D(size, 0);
//
//            int right = center.X + size;
//            int left = center.X - size;
            renderer.FillRect(symbol, a, b);
//            for(int i = left; i < right && right < renderer.Width - 1 && left > 0; i ++)
//                renderer.world[center.Y, i] = new Dot(symbol);
        }

        /// <summary>
        /// Обработчик нажатия на клавишу вверх
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private void Up()
        {
            symbol = ' ';

            Render();
            Renderer renderer = Renderer.Instance;

            if(center.Y > renderer.Height - max_height)
                center += new Vector2D(0, -1);
            symbol = '=';

            Render();
        }


        /// <summary>
        /// Обработчик нажатия на клавишу вниз
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private void Down()
        {
            symbol = ' ';
            Render();
            Renderer renderer = Renderer.Instance;

            if(center.Y < renderer.Height - 1)
                center += new Vector2D(0, 1);
            symbol = '=';
            Render();
        }


        /// <summary>
        /// Обработчик нажатия на клавишу влево
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private void Left()
        {
            symbol = ' ';
            Render();

            if(center.X > size + 1)
                center += new Vector2D(-1, 0);
            symbol = '=';

            Render();
        }


        /// <summary>
        /// Обработчик нажатия клавиши вправо
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private void Right()
        {
            symbol = ' ';

            Render();

            Renderer renderer = Renderer.Instance;

            if(center.X < renderer.Width - 2 - size)
                center += new Vector2D(1, 0);
            symbol = '=';

            Render();
        }
    }
}