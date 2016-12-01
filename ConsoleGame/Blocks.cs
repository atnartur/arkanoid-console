using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Xml.Serialization.Configuration;

namespace ConsoleGame
{
    public class Blocks : IObject
    {
        private Ball _ball;
        public List<Vector2D> Dots { get; private set; }
        private List<Vector2D> _renderedDots;

        public Blocks(Ball ball)
        {
            _ball = ball;
            Generate();
        }
        public void Render()
        {
            if (_renderedDots != Dots)
            {
                foreach (Vector2D Dot in Dots)
                    Renderer.Instance.FillRect('*', Dot);
                _renderedDots = Dots;
            }
        }

        public void Generate()
        {
            Dots = new List<Vector2D>();
            for (int x = 0; x < Renderer.Instance.Width; x++)
                for (int y = Renderer.Instance.Height - 1; y >= Renderer.Instance.Height * 3 / 4; y--)
                    Dots.Add(new Vector2D(x, y));
        }
    }
}