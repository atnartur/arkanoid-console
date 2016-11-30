using System;
using System.Collections.Generic;
using System.Xml.Xsl;

namespace ConsoleGame
{
    internal class Start
    {

        public static void Test()
        {
            Console.WriteLine(Console.BackgroundColor);
            Console.WriteLine();

            Console.WriteLine(Console.BufferHeight);
            Console.WriteLine(Console.BufferWidth);
            Console.WriteLine();

            Console.WriteLine(Console.CursorLeft);
            Console.WriteLine(Console.CursorSize);
            Console.WriteLine(Console.CursorTop);
            Console.WriteLine(Console.CursorVisible);
            Console.WriteLine();

            Console.WriteLine(Console.ForegroundColor);
            Console.WriteLine();

            Console.WriteLine(Console.LargestWindowHeight);
            Console.WriteLine(Console.LargestWindowWidth);
            Console.WriteLine();

            Console.WriteLine(Console.WindowHeight);
            Console.WriteLine(Console.WindowWidth);
            Console.WriteLine(Console.WindowTop);
            Console.WriteLine(Console.WindowLeft);

            Console.WriteLine(Console.ReadKey(true));
            Console.WriteLine(Console.WindowLeft);

            while (true)
            {
                ConsoleKeyInfo keycode = Console.ReadKey(true);
                Console.WriteLine(keycode.Key);

            }
        }
        public static void Main(string[] args)
        {
            Console.Clear();
            Renderer renderer = Renderer.Instance;
            renderer.DrawCanvas();
            renderer.Scene.Add(new Board());


            renderer.Start();
        }
    }
}


