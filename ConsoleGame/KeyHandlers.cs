using System;

namespace ConsoleGame
{
    /// <summary>
    /// Обработка нажатий базовых клавиш
    /// </summary>
    public class KeyHandlers
    {
        /// <summary>
        /// Выход из игры при нажатии на Q
        /// </summary>
        private static void Quit() => Renderer.Instance.Stop();

        /// <summary>
        /// Инициализация привязки обработчиков
        /// </summary>
        /// <param name="bindings"></param>
        public static void Attach(KeyBindings bindings)
        {
            bindings.Add(ConsoleKey.Q, Quit);
            bindings.Add(ConsoleKey.Escape, Quit);
        }
    }
}