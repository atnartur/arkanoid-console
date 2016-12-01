using System;

namespace ConsoleGame
{
    /// <summary>
    /// Двухмерный вектор
    /// </summary>
    public class Vector2D
    {
        /// <summary>
        /// Координата по Х
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата по Y
        /// </summary>
        public int Y { get; set; }

        public Vector2D(int x = 0, int y = 0)
        {
            Set(x, y);
        }

        public Vector2D(Vector2D a)
        {
            Set(a.X, a.Y);
        }

        /// <summary>
        /// Проверка равенства векторов
        /// </summary>
        /// <param name="x">координата по X</param>
        /// <param name="y">координата по Y</param>
        /// <returns>true, если вектор равен, false, если не равен</returns>
        public bool Equals(int x, int y) => (x == X && y == Y);


        /// <summary>
        /// Проверка равенства векторов
        /// </summary>
        /// <param name="v">Вектор, с которым сравниванием текущий вектор</param>
        /// <returns>true, если вектор равен, false, если не равен</returns>
        public bool Equals(Vector2D v) => Equals(v.X, v.Y);

        /// <summary>
        /// Установка значений вектора
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }


        public override String ToString() => "(" + X + "; " + Y + ")";

        /// <summary>
        /// Сложение векторов
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2D Sum(Vector2D a, Vector2D b)
        {
            Vector2D res = new Vector2D(a);
            res.X += b.X;
            res.Y += b.Y;
            return res;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b) => Sum(a, b);
    }
}