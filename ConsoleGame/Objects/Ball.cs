using System;

namespace ConsoleGame.Objects
{
    /// <summary>
    /// Шарик
    /// </summary>
    public class Ball : IObject
    {
        /// <summary>
        /// Состояния шарика
        /// </summary>
        enum State
        {
            StandOnBoard,
            Moving,
            Stop
        }

        /// <summary>
        /// Текущее состояние
        /// </summary>
        private State _state = State.StandOnBoard;

        /// <summary>
        /// Номер кадра для движения
        /// </summary>
        private int _movingStep = 0;

        /// <summary>
        /// Символ шарика
        /// </summary>
        private readonly char _symbol = '@';

        /// <summary>
        /// Направление движения шарика
        /// </summary>
        private Vector2D _direction = new Vector2D(1, 1);

        /// <summary>
        ///
        /// </summary>
        private readonly Board _board;

        /// <summary>
        /// Место нахождения шарика
        /// </summary>
        public Vector2D Center { get; private set; }

        /// <summary>
        /// Инициализация шарика
        /// </summary>
        /// <param name="board">Доска</param>
        public Ball(Board board)
        {
            _board = board;
            Center = _board.Center + new Vector2D(1, 0);

            Renderer.Instance.Bindings.Add(ConsoleKey.Spacebar, StartMoving);
        }

        /// <summary>
        /// Отображение
        /// </summary>
        public void Render()
        {
            Vector2D vector_2 = new Vector2D(Center);

            switch (_state)
            {
                case State.StandOnBoard:
                    vector_2 = _board.Center + new Vector2D(0, 1);
                    break;
                case State.Moving:
                    if (_movingStep > 1000)
                    {
                        // ограничение движений по оси X
                        if(vector_2.X >= Renderer.Instance.Width - 1)
                            _direction = new Vector2D(-1, 1);
                        else if(vector_2.X <= 0)
                            _direction = new Vector2D(1, -1);

                        // ограничение движений по оси Y
                        if(vector_2.Y >= Renderer.Instance.Height - 1)
                            _direction = new Vector2D(-1, -1);
                        else if (vector_2.Y <= 2)
                        {
                            _direction = new Vector2D(0, 0);
                            _state = State.Stop;

                        }

                        vector_2 += _direction;
                        _movingStep = 0;
                    }
                    else
                        _movingStep++;
                    break;
            }


            if (!vector_2.Equals(Center))
            {
                Renderer.Instance.FillRect(' ', Center);
                Renderer.Instance.FillRect(_symbol, vector_2);
                Center = vector_2;
            }
        }

        /// <summary>
        /// Запуск движения
        /// </summary>
        private void StartMoving() => _state = State.Moving;
    }
}