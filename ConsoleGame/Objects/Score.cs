using System;

namespace ConsoleGame.Objects
{
    public class Score : IObject
    {
        public int Count { get; set; }
        public int Hp { get; set; }
        private bool _isInitialized = false;
        private Vector2D _countCursorPosition;
        private Vector2D _hpCursorPosition;

        public Score()
        {
            Count = 0;
            Hp = 3;
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

                int margin = 5;

                string score_line = "Your score: ";
                _countCursorPosition = new Vector2D(score_line.Length + margin, 0);

                string hp_line = "   Your HP: ";
                _hpCursorPosition = new Vector2D(score_line.Length + hp_line.Length + margin , 0);

                renderer.PrintLineWithMargin(score_line + hp_line, margin, ConsoleColor.Black, true);
                _isInitialized = true;
            }

            RenderCount();
            RenderHp();
        }

        private void RenderNumber(Vector2D cursor_position, int number)
        {
            Renderer renderer = Renderer.Instance;
            renderer.SetCursorPosition(cursor_position);
            renderer.BackgroundColor(ConsoleColor.Black);
            renderer.Write(number);
            renderer.ResetBackgroundColor();
        }

        private void RenderCount() => RenderNumber(_countCursorPosition, Count);
        private void RenderHp() => RenderNumber(_hpCursorPosition, Hp);
    }
}