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
    }
}