﻿using System;

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

        private readonly Score _score;

        /// <summary>
        /// Место нахождения шарика
        /// </summary>
        public Vector2D Center { get; private set; }

        /// <summary>
        /// Инициализация шарика
        /// </summary>
        /// <param name="board">Доска</param>
        public Ball(Board board, Score score)
        {
            _board = board;
            _score = score;
            ResetPosition();
            Renderer.Instance.Bindings.Add(ConsoleKey.Spacebar, StartMoving);
        }

        public void ResetPosition() => Center = _board.Center + new Vector2D(1, 0);

        public void Clear() => Renderer.Instance.FillRect(' ', Center);

        /// <summary>
        /// Отображение
        /// </summary>
        public void Render()
        {
            Vector2D vector_2 = new Vector2D(Center);

//            bool firstFrame = true;

            switch (_state)
            {
                case State.StandOnBoard:
                    vector_2 = _board.Center + new Vector2D(0, 1);
                    break;
                case State.Moving:
                    if (_movingStep > 1000)
                    {
                        // если мы уткнулись в правую или левую стенки
                        if(vector_2.X >= Renderer.Instance.Width - 1 || vector_2.X <= 0)
                            _direction *= new Vector2D(-1, 1); // инвертируем координату направления движения по Х

                        // если мы уткнулись вверх
                        else if (vector_2.Y >= Renderer.Instance.Height - 1)
                            _direction *= new Vector2D(1, -1); // инвертируем координату направления движения по У

                        // если мы приблизились к низу
                        else if (vector_2.Y <= 3)
                        {
                            // если мы воткнулись в доску
                            if (
                                vector_2.Y == _board.Center.Y + 1 &&
                                vector_2.X >= _board.Center.X - _board.Size &&
                                vector_2.X <= _board.Center.X + _board.Size
                            )
                            {
                                if (_direction.Equals(new Vector2D(1, -1)))
                                    _direction = new Vector2D(1, 1);
                                else if (_direction.Equals(new Vector2D(-1, -1)))
                                    _direction = new Vector2D(-1, 1);
                            }
                            else // если мы воткнулись в низ
                            {
                                _direction = new Vector2D(0, 0);
                                // @TODO: окно проигрыша

                                _score.Hp--;
                                _state = State.Stop;

                                if (_score.Hp > 0)
                                {
                                    _board.Clear();
                                    Clear();

                                    _board.ResetPosition();
                                    ResetPosition();
                                }
                            }
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

        public void ChangeDirection()
        {
            Vector2D v = new Vector2D(_direction);

            if(v.X == 1 && v.Y == 1)
                v = new Vector2D(1, -1);
            else if(v.X == -1 && v.Y == 1)
                v = new Vector2D(-1, 1);
            else if(v.X == -1 && v.Y == -1)
                v = new Vector2D(1, 1);
            else
                v = new Vector2D(-1, -1);

            _direction = v;
        }

        /// <summary>
        /// Запуск движения
        /// </summary>
        private void StartMoving() => _state = State.Moving;
    }
}