namespace ConsoleGame
{
    public class Ball : IObject
    {
        enum State
        {
            StandOnBoard,
            Moving
        }

        private State _state = State.StandOnBoard;
        private char _symbol = '@';
        private Vector2D _direction = new Vector2D();
        private Board _board;
        private Vector2D _center;

        public Ball(Board board)
        {
            _board = board;
            _center = _board.Center + new Vector2D(1, 0);
        }
        public void Render()
        {
            Renderer.Instance.FillRect(' ', _center);

            switch (_state)
            {
                case State.StandOnBoard:
                    _center = _board.Center + new Vector2D(0, -1);
                    break;
                case State.Moving:
                    _center += _direction;
                    break;
            }

            Renderer.Instance.FillRect(_symbol, _center);
        }
    }
}