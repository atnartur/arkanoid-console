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
        private static bool Quit(Renderer renderer)
        {
            renderer.Stop();
            return true;
        }


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