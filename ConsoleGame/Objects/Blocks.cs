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
        private readonly int _bottomLine = Renderer.Instance.Height * 3 / 5;

        /// <summary>
        /// Инициализация блоков
        /// </summary>
        /// <param name="ball">Шарик</param>
        public Blocks(Ball ball, Score score)
        {
            _ball = ball;
            _score = score;
            Dots = new List<Vector2D>();
            GenerateBoss();
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
                        Renderer.Instance.Beep();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// генерация точек
        /// </summary>
        public void GenerateField()
        {
            for (int x = 0; x < Renderer.Instance.Width; x++)
                for (int y = Renderer.Instance.Height - 1; y >= _bottomLine; y--)
                    Dots.Add(new Vector2D(x, y));
        }

        public void GenerateTree()
        {
            Renderer renderer = Renderer.Instance;

            int[] linesLength = new int[renderer.Height - 1 - _bottomLine];

            linesLength[0] = 1;

            for (int i = 1; i < linesLength.Length - 1; i++)
                linesLength[i] = linesLength[i - 1] + (renderer.Width - 1) / linesLength.Length * i;

            for (int i = 0; i < linesLength.Length - 1; i++)
            {
                int y = renderer.Height - 1 - i;
                int left  = (renderer.Width - 1) / 2 - linesLength[i] / 2;
                int right = (renderer.Width - 1) / 2 + linesLength[i] / 2;

                if (left < 0)
                    left = 0;
                if (right > renderer.Width - 1)
                    right = renderer.Width - 1;

                for(int x = left; x <= right; x++)
                    Dots.Add(new Vector2D(x, y));
            }
        }

        public void GenerateBoss()
        {
            Renderer renderer = Renderer.Instance;

            Dots.Add(new Vector2D(renderer.Width / 2, renderer.Height - 1));

            int y = renderer.Height - 2;
            for(int x = renderer.Width * 2 / 6; x <= renderer.Width * 4 / 6; x++)
                Dots.Add(new Vector2D(x, y));

            y = renderer.Height - 3;
            for(int x = renderer.Width * 1 / 6; x <= renderer.Width * 5 / 6; x++)
                Dots.Add(new Vector2D(x, y));

            y = renderer.Height - 4;
            for(int x = renderer.Width * 2 / 6; x <= renderer.Width * 4 / 6; x++)
                Dots.Add(new Vector2D(x, y));

            y = renderer.Height - 5;
            for(int x = renderer.Width * 5 / 12; x <= renderer.Width * 7 / 12; x++)
                Dots.Add(new Vector2D(x, y));

            Dots.Add(new Vector2D(renderer.Width / 2, renderer.Height - 6));
        }
    }
}