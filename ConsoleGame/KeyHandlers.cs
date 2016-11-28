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
        /// <param name="renderer"></param>
        /// <returns></returns>
        private static void Quit(Renderer renderer) => renderer.Stop();

        /// <summary>
        /// Инициализация привязки обработчиков
        /// </summary>
        /// <param name="bindings"></param>
        public static void Attach(KeyBindings bindings)
        {
            bindings.Add(ConsoleKey.Q, Quit);
        }
    }
}