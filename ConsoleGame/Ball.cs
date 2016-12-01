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
        private readonly char _symbol = '@';
        private Vector2D _direction = new Vector2D(1, 1);
        private readonly Board _board;
        public Vector2D Center { get; private set; }

        public Ball(Board board)
        {
            _board = board;
            Center = _board.Center + new Vector2D(1, 0);

            Renderer.Instance.Bindings.Add(ConsoleKey.Spacebar, StartMoving);
        }
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


            if (!vector_2.Equals(Center))
            {
                Renderer.Instance.FillRect(' ', Center);
                Renderer.Instance.FillRect(_symbol, vector_2);
                Center = vector_2;
            }

        }

        private void StartMoving()
        {
            _state = State.Moving;
        }
    }
}