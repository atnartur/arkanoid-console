using System;

namespace ConsoleGame
{
    internal class Start
    {

        public static void StartGame()
        {
            Renderer renderer = Renderer.Instance;
            renderer.DrawCanvas();
            renderer.Start();
        }
        public static void Main(string[] args)
        {
            Console.Clear();
            Renderer renderer = Renderer.Instance;
            Board board = new Board();
            Ball ball = new Ball(board);
            renderer.Scene.Add(ball);
            renderer.Scene.Add(board);

            Help help = new Help();
            help.Show();
        }
    }
}


