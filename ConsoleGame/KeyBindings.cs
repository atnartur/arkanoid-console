using System;
using System.Collections;

namespace ConsoleGame
{
    public class KeyBindings
    {
        public ArrayList[] Bindings { get; private set; }

        public bool Exec(ConsoleKey key)
        {
            return false;
        }
    }
}