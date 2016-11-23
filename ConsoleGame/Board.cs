namespace ConsoleGame
{
    public class Board
    {
        private char symbol = '=';
        private int size = 3;
        public Board(Renderer renderer)
        {
            int center = renderer.Width / 2;

            int last_line = renderer.world.GetLength(0) - 1;

            for(int i = center - size; i < center + size; i ++)
                renderer.world[last_line, i] = new Dot(symbol);
        }
    }
}