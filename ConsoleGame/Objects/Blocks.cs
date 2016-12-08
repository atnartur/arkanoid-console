using System;
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
        /// Очки
        /// </summary>
        private Score _score;

        /// <summary>
        /// Массив точек
        /// </summary>
        public List<Vector2D> Dots { get; private set; }

        /// <summary>
        /// Отображенные точки
        /// </summary>
        private List<Vector2D> _renderedDots;

        /// <summary>
        /// Позиция нижней линии
        /// </summary>
        private readonly int _bottomLine = Renderer.Instance.Height * 3 / 4;

        /// <summary>
        /// Инициализация блоков
        /// </summary>
        /// <param name="ball">Шарик</param>
        public Blocks(Ball ball, Score score)
        {
            _ball = ball;
            _score = score;
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

            if (_ball.Center.Y >= _bottomLine - 1)
            {
                for (int i = Dots.Count - 1; i >= 0; i--)
                {
                    if (_ball.Center.X == Dots[i].X && _ball.Center.Y + 1 == Dots[i].Y){
                        _ball.ChangeDirection();
                        Renderer.Instance.FillRect(' ', Dots[i]);
                        _score.Count++;
                        Dots.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// генерация точек
        /// </summary>
        public void Generate()
        {
            Dots = new List<Vector2D>();
            for (int x = 0; x < Renderer.Instance.Width; x++)
                for (int y = Renderer.Instance.Height - 1; y >= _bottomLine; y--)
                    Dots.Add(new Vector2D(x, y));
        }
    }
}