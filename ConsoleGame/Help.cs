using System;
using System.IO;
using System.Threading;

namespace ConsoleGame
{
    public class Help
    {
        private String[] _content;

        public Help()
        {
            ReadFile();
            Renderer.Instance.Bindings.Add(ConsoleKey.F1, Show);
        }

        public void Show()
        {
            Renderer renderer = Renderer.Instance;
            renderer.Stop();

            int margin_top = (renderer.Height - _content.Length) / 2;

            if (margin_top < 0)
                margin_top = 0;

            renderer.DrawCanvas();

            Console.SetCursorPosition(0, margin_top);

            renderer.PrintLineOnCenter(_content[0], ConsoleColor.Green);

            for (int i = 1; i < _content.Length; i++)
            {
                Thread.Sleep(50);
                renderer.PrintLineWithMargin(_content[i], 3, ConsoleColor.Black);
            }

            renderer.PrintLineWithMargin("", 3, ConsoleColor.Black);

            bool waitingForStart = true;
            while (waitingForStart)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Spacebar:
                        waitingForStart = false;
                        Start.StartGame();
                        break;
                    case ConsoleKey.Q:
                    case ConsoleKey.Escape:
                        waitingForStart = false;
                        renderer.Clear();
                        break;
                }
            }
        }

        private void ReadFile()
        {
            using (StreamReader sr = new StreamReader("./help.txt"))
            {
                String file = sr.ReadToEnd();
                _content = file.Split('\n');
            }
        }
    }
}