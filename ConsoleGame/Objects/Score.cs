namespace ConsoleGame.Objects
{
    public class Score : IObject
    {
        public int Count { get; private set; }
        private bool _isInitialized = false;
        public Score()
        {
            Count = 0;
        }

        public void Render()
        {
            Renderer renderer = Renderer.Instance;

            if (!_isInitialized)
            {
                renderer.FillRect(
                    '-',
                    new Vector2D(0, 2),
                    new Vector2D(renderer.Width - 1, 2)
                );
                _isInitialized = true;
            }
        }
    }
}