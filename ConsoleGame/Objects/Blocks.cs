using System.Collections.Generic;

namespace ConsoleGame.Objects
{
    /// <summary>
    /// Блоки
    /// </summary>
    public class Blocks : IObject
    {
        /// <summary>
        /// Шарик
        /// </summary>
        private Ball _ball;

        /// <summary>
        /// Массив точек
        /// </summary>
        public List<Vector2D> Dots { get; private set; }

        /// <summary>
        /// Отображенные точки
        /// </summary>
        private List<Vector2D> _renderedDots;

        /// <summary>
        /// Инициализация блоков
        /// </summary>
        /// <param name="ball">Шарик</param>
        public Blocks(Ball ball)
        {
            _ball = ball;
            Generate();
        }

        /// <summary>
        /// Отображение
        /// </summary>
        public void Render()
        {
            if (_renderedDots != Dots)
            {
                foreach (Vector2D Dot in Dots)
                    Renderer.Instance.FillRect('*', Dot);
                _renderedDots = Dots;
            }
        }

        /// <summary>
        /// генерация точек
        /// </summary>
        public void Generate()
        {
            Dots = new List<Vector2D>();
            for (int x = 0; x < Renderer.Instance.Width; x++)
                for (int y = Renderer.Instance.Height - 1; y >= Renderer.Instance.Height * 3 / 4; y--)
                    Dots.Add(new Vector2D(x, y));
        }
    }
}