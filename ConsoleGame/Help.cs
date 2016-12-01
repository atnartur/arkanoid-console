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
            Renderer.Instance.Bindings.Add(ConsoleKey.F1, ShowHelp);
        }

        public void ShowHelp()
        {
            Renderer renderer = Renderer.Instance;
            renderer.Stop();

            int margin_top = (renderer.Height - _content.Length) / 2;

            if (margin_top < 0)
                margin_top = 0;

            renderer.DrawCanvas();

            Console.SetCursorPosition(0, margin_top);

            renderer.PrintLineOnCenter(_content[0]);

            for (int i = 1; i < _content.Length; i++)
            {
                Thread.Sleep(100);
                renderer.PrintLineWithMargin(_content[i], 10);
            }

            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar){}

            renderer.DrawCanvas();
            renderer.Start();
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