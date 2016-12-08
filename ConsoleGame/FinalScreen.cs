using System;
using System.IO;
using System.Runtime.Versioning;
using System.Threading;
using ConsoleGame.Objects;

namespace ConsoleGame
{
    /// <summary>
    /// Финальный экран
    /// </summary>
    public class FinalScreen
    {
        private Score _score;

        /// <summary>
        /// Инициализация
        /// </summary>
        public FinalScreen(Score score)
        {
            _score = score;
        }

        /// <summary>
        /// Отображение подсказки
        /// </summary>
        public void Show()
        {
            Renderer renderer = Renderer.Instance;
            renderer.Stop();

            int margin_top = (renderer.Height - 6) / 2;

            if (margin_top < 0)
                margin_top = 0;

            renderer.SetCursorPosition(new Vector2D(0, renderer.Height - margin_top));
            renderer.PrintLineOnCenter("Арканоид", ConsoleColor.Green);
            renderer.PrintLineOnCenter("G A M E   O V E R", ConsoleColor.Red);
            renderer.PrintLineOnCenter("Your scores: " + _score.Count, ConsoleColor.Black);
            renderer.PrintLineOnCenter("", ConsoleColor.Black);
            renderer.PrintLineOnCenter("Press any key to exit", ConsoleColor.Black);
            renderer.PrintLineOnCenter("", ConsoleColor.Black);

            Console.Read();
        }
    }
}