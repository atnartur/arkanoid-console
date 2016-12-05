using System;

namespace ConsoleGame.Objects
{
    /// <summary>
    /// Доска с результатами
    /// </summary>
    public class Score : IObject
    {
        /// <summary>
        /// Количество очков
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Количество жизней
        /// </summary>
        public int Hp { get; set; }

        /// <summary>
        /// Флаг инициализации
        /// </summary>
        private bool _isInitialized = false;

        /// <summary>
        /// Позиция курсора для очков
        /// </summary>
        private Vector2D _countCursorPosition;

        /// <summary>
        /// Позиция курсора для HP
        /// </summary>
        private Vector2D _hpCursorPosition;

        public Score()
        {
            Count = 0;
            Hp = 3;
        }

        /// <summary>
        /// Отображение
        /// </summary>
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

        /// <summary>
        /// Отображение цифры на позиции курсора
        /// </summary>
        /// <param name="cursor_position"></param>
        /// <param name="number"></param>
        private void RenderNumber(Vector2D cursor_position, int number)
        {
            Renderer renderer = Renderer.Instance;
            renderer.SetCursorPosition(cursor_position);
            renderer.BackgroundColor(ConsoleColor.Black);
            renderer.Write(number);
            renderer.ResetBackgroundColor();
        }

        /// <summary>
        /// Отображение количества очков
        /// </summary>
        private void RenderCount() => RenderNumber(_countCursorPosition, Count);

        /// <summary>
        /// Отображение жизней
        /// </summary>
        private void RenderHp() => RenderNumber(_hpCursorPosition, Hp);
    }
}