# Блок Dependency Injection Container

## Описание

DI-контейнеры используется во многих команд Контура для управления зависимостями с соблюдением Dependency Inversion Principle.

Пройдя блок ты:

- научишься получать зависимости через контейнер
- поймешь как надо и как не надо использовать контейнер
- узнаешь как конфигурировать контейнер для зависимостей разных типов

Все это на примере контейнера Ninject, но полученные знания будут применимы и к другим контейнерам с точностью до нюансов синтаксиса.


## Необходимые знания

Рекомендуется пройти блоки [SOLID](https://github.com/kontur-csharper/solid-design) и [Тестирование](https://github.com/kontur-csharper/testing)


## Самостоятельная подготовка

1. Посмотри видеолекцию про [DI-контейнеры](https://ulearn.me/Course/cs2/Probliematika_69a66629-787b-4ef6-932b-25bafe6a4467) (~1.5 часов)


## Очная встреча

~ 4 часов


## Закрепление материала

1. Выполни задание [TagsCloudApp](TagsCloudApp)

2. Спецзадание __Death Injection__
Найди в своем проекте интересные места, которые сильно расходятся с идеями из видеолекций. Либо наоборот иллюстрируют и дополняют эти   идеи. Обрати внимание на:
  - Нарушение явного управления зависимостями / Dependency Injection. Чем это было обосновано, какую выгоду получили?
  - Активное, нестандартное, хитрое использование DI-контейнера. Чему могли бы научиться на этом примере другие?
  - Проекты, в которых есть сборка сложного графа зависимостей, но DI-контейнера нет. Почему нет? Есть ли причины его не использовать?
