using System;

namespace ConsoleGame.Objects
{
    public class Score : IObject
    {
        public int Count { get; set; }
        private bool _isInitialized = false;
        private Vector2D _numberCursorPosition;

        public Score()
        {
            Count = 0;
        }

        public void Render()
        {
            Renderer renderer = Renderer.Instance;

            if (!_isInitialized)
            {
                renderer.FillRect(
                    '-',
                    new Vector2D(0, 0),
                    new Vector2D(renderer.Width - 1, 0)
                );

                string line = "Your score: ";

                int margin = 5;

                renderer.PrintLineWithMargin(line, margin, ConsoleColor.Black, true);

                _numberCursorPosition = new Vector2D(line.Length + margin, 0);

                _isInitialized = true;
            }

            RenderCount();
        }

        private void RenderCount()
        {
            Renderer renderer = Renderer.Instance;
            renderer.SetCursorPosition(_numberCursorPosition);
            renderer.BackgroundColor(ConsoleColor.Black);
            renderer.Write(Count);
            renderer.ResetBackgroundColor();
        }
    }
}