namespace ConsoleGame.Objects
{
    /// <summary>
    /// Интерфейс для графических объектов
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// Метод, запускающийся в цикле отображения
        /// </summary>
        void Render();
    }
}