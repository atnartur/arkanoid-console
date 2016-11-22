using System;

namespace ConsoleGame
{
    public class KeyBinding
    {
        public ConsoleKey Key { get; private set; }
        public Func<Dot[,]> Callback { get; private set; }

        public KeyBinding(ConsoleKey key, Func<Dot[,]> callback)
        {
            Key = key;
            Callback = callback;
        }
    }
}