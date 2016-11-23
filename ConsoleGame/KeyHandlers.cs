using System;

namespace ConsoleGame
{
    public class KeyHandlers
    {
        private static bool Quit(Renderer renderer)
        {
//            Console.WriteLine(1111);
            renderer.Stop();
            return true;
        }
        public static void Attach(KeyBindings bindings)
        {
            bindings.Add(ConsoleKey.Q, Quit);
        }
    }
}