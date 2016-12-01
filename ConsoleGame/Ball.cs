using System;

namespace ConsoleGame
{
    public class Ball : IObject
    {
        enum State
        {
            StandOnBoard,
            Moving,
            Stop
        }

        private State _state = State.StandOnBoard;
        private int _movingStep = 0;
        private char _symbol = '@';
        private Vector2D _direction = new Vector2D(1, 1);
        private Board _board;
        private Vector2D _center;

        public Ball(Board board)
        {
            _board = board;
            _center = _board.Center + new Vector2D(1, 0);

            Renderer.Instance.Bindings.Add(ConsoleKey.Spacebar, StartMoving);
        }
        public void Render()
        {
            Vector2D vector_2 = new Vector2D(_center);

            switch (_state)
            {
                case State.StandOnBoard:
                    vector_2 = _board.Center + new Vector2D(0, 1);
                    break;
                case State.Moving:
                    if (_movingStep > 1000)
                    {
                        if(vector_2.X >= Renderer.Instance.Width - 1)
                            _direction = new Vector2D(-1, 1);
                        else if(vector_2.X <= 0)
                            _direction = new Vector2D(1, -1);

                        if(vector_2.Y >= Renderer.Instance.Height - 1)
                            _direction = new Vector2D(-1, -1);
                        else if(vector_2.Y <= 0)
                            _direction = new Vector2D(1, 1);

                        vector_2 += _direction;
                        _movingStep = 0;
                    }
                    else
                    {
                        _movingStep++;
                    }
                    break;
            }


            if (!vector_2.Equals(_center))
            {
                Renderer.Instance.FillRect(' ', _center);
                Renderer.Instance.FillRect(_symbol, vector_2);
                _center = vector_2;
            }

        }

        private void StartMoving()
        {
            _state = State.Moving;
        }
    }
}