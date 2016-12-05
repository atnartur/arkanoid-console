using System;
using System.IO;
using System.Runtime.Versioning;
using System.Threading;

namespace ConsoleGame
{
    /// <summary>
    /// Подсказка
    /// </summary>
    public class Help
    {
        /// <summary>
        /// Содержание подсказки
        /// </summary>
        private String[] _content;

        /// <summary>
        /// Инициализация
        /// </summary>
        public Help()
        {
            ReadFile();
            Renderer.Instance.Bindings.Add(ConsoleKey.F1, Show);

            // кнопка со знаком вопроса рядом с правым SHIFT
            Renderer.Instance.Bindings.Add(ConsoleKey.Divide, Show);
        }

        /// <summary>
        /// Отображение подсказки
        /// </summary>
        public void Show()
        {
            Renderer renderer = Renderer.Instance;
            renderer.Stop();

            int margin_top = (renderer.Height - _content.Length) / 2;

            if (margin_top < 0)
                margin_top = 0;

            renderer.DrawCanvas();

            Console.SetCursorPosition(0, margin_top);

            renderer.PrintLineOnCenter(_content[0], ConsoleColor.Green);

            for (int i = 1; i < _content.Length; i++)
            {
                Thread.Sleep(25);
                renderer.PrintLineWithMargin(_content[i], 3, ConsoleColor.Black);
            }

            renderer.PrintLineWithMargin("", 3, ConsoleColor.Black);

            bool waitingForStart = true;
            while (waitingForStart)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Spacebar:
                        waitingForStart = false;
                        Start.StartGame();
                        break;
                    case ConsoleKey.Q:
                    case ConsoleKey.Escape:
                        waitingForStart = false;
                        renderer.Clear();
                        break;
                }
            }
        }

        /// <summary>
        /// Чтение подсказки из файла
        /// </summary>
        private void ReadFile()
        {
            String file = "Арканоид\n"+
            "\n"+
            "Игрок контролирует небольшую платформу-ракетку, которую можно передвигать горизонтально от одной стенки до другой, подставляя её под шарик, предотвращая его падение вниз. Удар шарика по кирпичу приводит к разрушению кирпича.\n" +
            "\n" +
            "Клавиши:\n"+
            "Стрелки вверх, вниз, вправо, влево - передвижение платформы-ракетки\n"+
            "Esc, Q - выход из игры\n"+
            "F1, ? - вызов этой подсказки\n" +
            "\n" +
            "Нажмите пробел, чтобы начать/продолжить к игре\n"+
            "\n"+
            "Автор:\n" +
            "Атнагулов Артур, группа 11-607 КФУ ВШ ИТИС\n"+
            "i@atnartur.ru";
            _content = file.Split('\n');
        }
    }
}