using System;
using System.Collections.Generic;

namespace ConsoleGame
{
    public class KeyBindings
    {
        private readonly Dictionary<ConsoleKey, Func<Renderer, bool>> _bindings = new Dictionary<ConsoleKey, Func<Renderer, bool>>();

        public bool Exec(ConsoleKey key, Renderer renderer)
        {
            Func<Renderer, bool> callback;
            Console.WriteLine(key);

            _bindings.TryGetValue(key, out callback);

            try{
                callback.Invoke(renderer);
            }
            catch (NullReferenceException e)
            {
                return false;
            }
            return true;
        }

        public void Add(ConsoleKey key, Func<Renderer, bool> callback) => _bindings.Add(key, callback);
        public void Remove(ConsoleKey key) => _bindings.Remove(key);
    }
}