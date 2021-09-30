# DataBase - библиотека для Работы с Базой данных SQLite

*Проект создан в учебных целях*

* © Компьтерная Академия ШАГ. Россия (филиал Иркутск)
* Версия: 0.1 (Сентябрь 2021)

## О библиотеке

Написана на `C#` с использованием `System`, `System.Collections.Generic`, `System.Data.SQLite`, `System.Threading.Tasks`
Используется асинхронная запись\Чтение из базы данных.

### Функции

- `AddDataInTableAsync(TaskData data)` асинхронный метод добавления задачи в Базу данных.
- `EditDataInTableAsync(TaskData data)` асинхронный метод изменения задачи в базе данных.
- `ReadDataInTableAsync()` асинхронный метод чтения задач из базы данных.
- `DeleteDataInTableAsync(TaskData data)` асинхронный метод удаления задачи.

### Возвращаемые значения функций
- `AddDataInTableAsync(TaskData data)` Возвращает полную Задачу с ID из базы данных добавленной Задачи.
- `ReadDataInTableAsync()` Возвращает Лист Задач из базы данных.

### Пример
- `Console.WriteLine($"{data.id},{data.name},{data.description},{TaskData.statusDic[data.status]},{TaskData.priorityDic[data.priority]}");`
- 1,Имя,Описание,not_completed,medium
- 2,будильник,Встать,not_completed,high

