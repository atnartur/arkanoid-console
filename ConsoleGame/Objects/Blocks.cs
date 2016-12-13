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
        /// центр босса
        /// </summary>
        public Vector2D BossCenter { get; private set; }

        /// <summary>
        /// Шаг анимации бота
        /// </summary>
        private int _bossMovingStep = 0;

        private Vector2D _bossRotate;

        /// <summary>
        /// Инициализация блоков
        /// </summary>
        /// <param name="ball">Шарик</param>
        public Blocks(Ball ball, Score score)
        {
            _ball = ball;
            _score = score;
            Dots = new List<Vector2D>();
            Renderer renderer = Renderer.Instance;
            BossCenter = new Vector2D(renderer.Width / 2, renderer.Height - 1);

            int rotate_step = renderer.Width / 10;
            if (rotate_step == 0)
                rotate_step = 1;

            _bossRotate = new Vector2D(rotate_step);
            GenerateBoss();
        }

        /// <summary>
        /// Отображение
        /// </summary>
        public void Render()
        {
            Renderer renderer = Renderer.Instance;

            if (_renderedDots != Dots)
            {
                foreach (Vector2D Dot in Dots)
                    renderer.FillRect('*', Dot);
                _renderedDots = Dots;
            }

            if (_bossMovingStep > 4500)
            {
                _bossRotate *= -1;

                for (int i = 0; i < Dots.Count; i++)
                {
                    renderer.FillRect(' ', Dots[i]);
                    Dots[i] += _bossRotate;
                }

                _renderedDots = new List<Vector2D>();

                _bossMovingStep = 0;
            }
            else
                _bossMovingStep++;

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

        private void BossLine(int x_start , int x_end, int y)
        {
            for(int x = x_start; x <= x_end; x++)
                Dots.Add(new Vector2D(x, y));
        }

        public void GenerateBoss()
        {
            Dots.Add(BossCenter);
            int y = BossCenter.Y;

            Renderer renderer = Renderer.Instance;

            int size1 = renderer.Width / 6;
            int size2 = renderer.Width / 5;
            int size3 = renderer.Width / 3;

            BossLine(BossCenter.X - size1, BossCenter.X + size1, y--);
            BossLine(BossCenter.X - size2, BossCenter.X + size2, y--);
            BossLine(BossCenter.X - size2, BossCenter.X + size2, y--);
            BossLine(BossCenter.X - size1, BossCenter.X + size1, y--);
            BossLine(BossCenter.X - size3, BossCenter.X + size3, y--);
            BossLine(BossCenter.X - size3, BossCenter.X + size3, y--);
            BossLine(BossCenter.X - size2, BossCenter.X + size2, y--);
            BossLine(BossCenter.X - size1, BossCenter.X + size1, y--);
        }
    }
}