using System;

namespace ConsoleGame
{
    /// <summary>
    /// Точка, которая выводится на консоль
    /// </summary>
    public class Dot
    {
        /// <summary>
        /// Символ этой точки
        /// </summary>
        public char Symbol { get; set; }


        /// <summary>
        /// Создаем точку
        /// </summary>
        /// <param name="symbol">Символ, который выведется на консоль</param>
        public Dot(char symbol = ' ')
        {
            Symbol = symbol;
        }

        public override String ToString() => Symbol.ToString();
    }
}