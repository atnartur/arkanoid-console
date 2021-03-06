﻿using System;
using System.Collections.Generic;

namespace ConsoleGame
{
    /// <summary>
    /// Класс для привязки функций к нажатиям клавиш
    /// </summary>
    public class KeyBindings
    {
        // @TODO: сделать возможность привязки нескольких обработчиков на одно событие

        /// <summary>
        /// Словарь: ключ - ConsoleKey (клавиша), которая была нажата;
        /// значение - функция, принимающая объект типа Renderer и возвращающаяя булево значение, вызовется при нажатии клавиши
        /// </summary>
        private readonly Dictionary<ConsoleKey, Action> _bindings = new Dictionary<ConsoleKey, Action>();

        /// <summary>
        /// Метод вызова функции при нажатии клавиши
        /// </summary>
        /// <param name="key">клавиша, которая была нажата</param>
        /// <param name="renderer">объект Renderer</param>
        /// <returns>true, если обработчик был вызван, и false, если обработчик не был вызван</returns>
        public bool Exec(ConsoleKey key)
        {
            Action callback;

            _bindings.TryGetValue(key, out callback);

            try{
                callback.Invoke();
            }
            catch (NullReferenceException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Привязка обработчика
        /// </summary>
        /// <param name="key">Клавиша</param>
        /// <param name="callback">Функция, которая вызовется при нажатии на эту клавишу</param>
        public void Add(ConsoleKey key, Action callback) => _bindings.Add(key, callback);


        /// <summary>
        /// Удаление обработчика
        /// </summary>
        /// <param name="key">Клавиша</param>
        public void Remove(ConsoleKey key) => _bindings.Remove(key);
    }
}