﻿# Игра Арканоид

Запуск в консоли: 
```
cd ConsoleGame
./run.sh
```

Ветки:
- master - основная ветка разработки
- stable - стабильная версия игры

## Changelog

- Версия 0 (22 ноября)
    + Консольный рендерер на матринце
    + Выход по нажатию на Q
- Версия 1 (24 ноября)
    + Реализован обработчик нажатий клавиш через словарь (KeyBindings)
    + Вывод дощечки, от которой будут отбиваться шарики
    + Дощечка не выходит за границы экрана и не поднимается выше определенной границы
    + Спроектированны и реализованы классы Renderer, KeyBindings, Vector2D, Dot
    + Реализовано сложение векторов, в том числе через операторы (Vector2D)
- Версия 2 (29 ноября)
    + Обработчики клавиш исправлены - убран костыль с возвращаемым булевым значением
    + Renderer Singleton
    + Renderer переписан на через SetCursorPosition
    + Реализован выход через Escape
- Версия 3 (1 декабря)
    + Паралельный рендеринг картинки и обработка нажатий клавиш (через `Console.KeyAvailable`)
    + Привязка шарика к доске
    + Движение шарика по экрану под углом в 45 градусов и отскакивание его от границ экрана
    + Методы вывода текста: по центру, с отступом от краев. Можно также задать фоновый цвет текста
    + Вывод красивой текстовой подсказки в начале игры и переход к игре
    + Описано взаимодействие между объектами
    + Добавлены блоки
- Версия 4 (6 декабря)
    + Скорость движения доски увеличена
    + Графические объекты перенесены в отдельный namespace `Objects`
    + Шарик останавливается при вылете вниз
    + Умножение `Vector2D`
    + Информационная панель с количеством очков
    + Мячик разбивает блоки и прибавляет количество очков
    + Мячик отскакивает от доски
    + Мячик останавливается, если не попадает в доску
    + Исправлены баги с изменением направления движения мячика при отскоке от стенок и доски
    + Добавлены Health Points
    + Вычитание HP при вылете за нижнюю границу экрана, перезапуск игры
    + Подсказка перенесена в код
- Версия 5 (8 декабря)
    + Исправлено: баг удаления нескольких блоков при направлении налево вверх
    + Исправлено: изменение направления шарика неверное рядом с границей консоли и вызывает исключения
    + Исправлено: При нажатии на пробел после проигрыша мячик двигается
    + Отбивание мячика от ракетки выше позиции по умолчанию
    + Отображение текстового интерфейса после проигрыша
    + Воспроизведение звуков в процессе игры

## Компоненты игры

**Процесс работы игры**

1. Инициализация объекта `Renderer` (через Singleton), заполнение консоли фоновым цветом, привязка обработчиков клавиш (`KeyBinginds`) установка размеров экрана.
2. Инициализация основных составляющихигры (доска - `Board`, шарик - `Ball`, блоки - `Blocks`...) и привязка их к сцене (свойство `Renderer.Scene`). Все составляющие реализуют интерфейс `IObject`.
3. Происходит вывод подсказки (`Help.Show()`)
4. При нажатии на `Enter` подсказка запускает `Renderer.Start()`: происходит запуск цикла отрисовки и обработки клавиш, появляется доска, шарик, блоки
5. Во время нажатий клавиш запускаются методы, которые привязываются в объектах графических объектов к `KeyBinginds`. 
6. Основной цикл отрисовки не блокируются: в каждый момент времени по очереди у каждого объекта запускаются методы `Render()`.

**Предназначения классов**

- `Renderer` - главный объект отрисовки. Отвечает за вывод всех элементов на экран и передачу базовой информации другим объектам. Хранит в себе список всех объектов, которые надо отрисовывать
- `Help` - вывод подсказок
- `FinalScreen` - финальный экран, который выводит сообщение о заверешнии игры и информацию об очках
- `KeyBindings` - обработка нажатий клавиш через словарь. Инициализируется в `Renderer`
- `KeyHandlers` - статический класс для привязки общих обработчиков клавиш
- `Start` - класс запуска
- `Vector2D` - двухмерный вектор с реализованными методами сравнения и сложения (через оператор)
- `Objects` - namespace с графическими объектами
    + `IObject` - интерфейс, который реализовывают все графические объекты
    + `Ball` - шарик, передвигающийся по экрану
    + `Blocks` - блоки, которые должен разбить шарик
    + `Board` - доска-ракетка
    + `Score` - информационная панель с очками и жизнями

## Условные обозначения в коммитах
- `+` добавлено
- `-` удалено
- `=` изменение в текущем функционале
- `!` исправлено
- `Х%` сделано на Х процентов