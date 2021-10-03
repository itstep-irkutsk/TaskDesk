# DataBase - библиотека для Работы с Базой данных SQLite

## О библиотеке

Библиотека для работы с бд, для программы планировщик задач.

Что пока реализовано:
- Класс DBLib непосредственно для работы с бд:
	> загружает данные из таблицы table_task при создании своего экземпляра во внутреннюю переменную _tasks, которая имеет тип ObservableCollection<DBTask>.
	>> можно подписаться на события коллекции или использовать ее как источник данных в UI.
	>
	> содержит асинхронные методы для добавления, удаления, редактирования записей в таблице table_task и параллельно в переменную _tasks.
	>> AddTask(DBTask newTask).

	>> DeleteTask(int taskId).

	>> EditTask(int taskId, DBTask newTask).
- вспомогательные классы DBTask, DBPriority, DBStatus для работы с классом DBLib.

## Функции

- `AddDataInTableAsync(TaskData data)` асинхронный метод добавления задачи в Базу данных.
- `EditDataInTableAsync(TaskData data)` асинхронный метод изменения задачи в базе данных.
- `ReadDataInTableAsync()` асинхронный метод чтения задач из базы данных.
- `DeleteDataInTableAsync(TaskData data)` асинхронный метод удаления задачи.