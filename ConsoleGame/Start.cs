using System;
using ConsoleGame.Objects;

namespace ConsoleGame
{
    internal class Start
    {
        /// <summary>
        /// Запуск игры
        /// </summary>
        public static void StartGame()
        {
            Renderer renderer = Renderer.Instance;
            renderer.DrawCanvas();
            renderer.Start();
        }

        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.Clear();

            Renderer renderer = Renderer.Instance;

            Board board = new Board();
            Ball ball = new Ball(board);
            Blocks blocks = new Blocks(ball);

            renderer.Scene.Add(ball);
            renderer.Scene.Add(board);
            renderer.Scene.Add(blocks);

            Help help = new Help();
            help.Show();
        }
    }
}


