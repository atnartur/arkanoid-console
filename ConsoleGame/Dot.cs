using System;

namespace ConsoleGame
{
    public class Dot
    {
        public char Symbol { get; set; }

        public Dot(char symbol = ' ')
        {
            Symbol = symbol;
        }

        public override String ToString()
        {
            return Symbol.ToString();
        }
    }
}